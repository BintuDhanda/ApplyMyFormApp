using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Razorpay.Api;
using rojgar.Models;
using rojgar.ModelsApi;
using rojgar.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace rojgar.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "User")]
    public class ApiController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;
        private AppSettings _appsettings;
        private readonly EmailSettings emailSettings;
        private readonly SMSSettings smsSettings;
        private readonly PaymentGateway _paymentGateway;
        private readonly IHttpContextAccessor _httpContext;
        public ApiController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext, IOptions<AppSettings> appsettings, IOptions<EmailSettings> _emailSettings,
            IOptions<SMSSettings> _smsSettings, IHttpContextAccessor httpContextAccessor, IOptions<PaymentGateway> paymentGateway)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _appsettings = appsettings.Value;
            _roleManager = roleManager;
            emailSettings = _emailSettings.Value;
            smsSettings = _smsSettings.Value;
            _httpContext = httpContextAccessor;
            _paymentGateway = paymentGateway.Value;
        }

        // Account Api Start //
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(ApplicationUser model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var isExist = await _userManager.FindByNameAsync(model.PhoneNumber);
                    if (isExist == null)
                    {
                        model.UserName = model.PhoneNumber;
                        model.IsActive = true;
                        var result = await _userManager.CreateAsync(model, model.PasswordHash);
                        if (result.Succeeded)
                        {
                            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
                            await _userManager.AddToRoleAsync(user, "User");
                            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                            IdentityAuth identityAuth = new IdentityAuth();
                            identityAuth.Token = token;
                            var random = new Random();
                            identityAuth.Otp = random.Next(000000, 999999);
                            //identityAuth.Otp = 123456;

                            identityAuth.UserId = user.Id;
                            identityAuth.TimeStamp = DateTime.Now;
                            identityAuth.IsPhone = true;
                            identityAuth.MaxAttempt = 0;
                            await _dbContext.IdentityAuth.AddAsync(identityAuth);
                            await _dbContext.SaveChangesAsync();
                            bool isSentOTP;
                            isSentOTP = SendOtp(user.PhoneNumber, identityAuth.Otp, user.Id);
                            transaction.Commit();
                            return new JsonResult(model);
                        }
                        else
                        {
                            Response.StatusCode = 441;
                            return new JsonResult("Registration Failed");
                        }
                    }
                    else
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("User already exist with this Phone Number");
                    }
                }
                catch (Exception e)
                {
                    Response.StatusCode = 441;
                    transaction.Rollback();
                    return new JsonResult(e.Message);
                }
            }
        }

        private bool SendOtp(string phoneNumber, int otp, string UserId)
        {

            using (var web = new System.Net.WebClient())
            {
                try
                {
                    string username = "amitmyform";
                    string message = "Hi, Your OTP for Apply My Form is " + otp + ".Thank You! Founder - Amit Kumar";
                    string sendername = "ATKMAR";
                    string apikey = "9a161016-c08a-4586-b7b0-50be49c3bc9f";
                    string peid = "1501434840000036961";
                    string templateid = "1507164569241079216";
                    string url = "http://sms.bulksmsind.in/v2/sendSMS?" +
                        "username=" + username + "&" +
                        "message=" + message + "&" +
                        "sendername=" + sendername + "&" +
                        "smstype=TRANS&" +
                        "numbers=" + phoneNumber + "&" +
                        "apikey=" + apikey + "&" +
                        "peid=" + peid + "&" +
                        "templateid=" + templateid + "";

                    string result = web.DownloadString(url);
                    if (result.Contains("success"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;

                }
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmAccount(ApiFetchParameters fetch)
        {
            var user = await _userManager.FindByNameAsync(fetch.UserName);
            if (user != null)
            {
                var identityAuth = await _dbContext.IdentityAuth.Where(g => g.UserId == user.Id).FirstOrDefaultAsync();
                if (Convert.ToInt32(DateTime.Now.Subtract(identityAuth.TimeStamp).TotalMinutes) > 10)
                {
                    Response.StatusCode = 441;
                    return new JsonResult("OTP Expired");
                }
                else if (identityAuth.Otp != fetch.Otp)
                {
                    Response.StatusCode = 441;
                    return new JsonResult("Invalid OTP");
                }
                else
                {
                    var result = await _userManager.VerifyChangePhoneNumberTokenAsync(user, identityAuth.Token, user.PhoneNumber);
                    if (result)
                    {
                        user.PhoneNumberConfirmed = true;
                        await _userManager.UpdateAsync(user);
                        return new JsonResult("Account Activated");
                    }
                }
                return new JsonResult(null);
            }
            else
            {
                Response.StatusCode = 441;
                return new JsonResult("Something went wrong, try again");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResendOTP(ApiFetchParameters fetch)
        {
            var user = await _userManager.FindByNameAsync(fetch.UserName);
            var identityAuth = await _dbContext.IdentityAuth.Where(g => g.UserId == user.Id).FirstOrDefaultAsync();
            Random random = new Random();
            var otp = random.Next(999999);
            identityAuth.Otp = otp;
            identityAuth.TimeStamp = DateTime.Now;
            _dbContext.IdentityAuth.Update(identityAuth);
            await _dbContext.SaveChangesAsync();
            SendOtp(user.PhoneNumber, otp, user.Id);
            return new JsonResult("OTP resent to your phone number");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Signin(Login model)
        {
            var FirstUser = new AdminAndRolesInitializer(_userManager, _roleManager);
            await FirstUser.Initialize();
            if (model != null)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        if (user.PhoneNumberConfirmed)
                        {
                            var result = await _userManager.CheckPasswordAsync(user, model.Password);
                            if (result)
                            {
                                var tokenHandler = new JwtSecurityTokenHandler();
                                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.Key));
                                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                                var userRoles = await _userManager.GetRolesAsync(user);
                                var claims = new List<Claim> {
                                new Claim(ClaimTypes.Name, user.Id)
                                };
                                foreach (var role in userRoles)
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, role));
                                }

                                var token = new JwtSecurityToken(_appsettings.Issuer,
                                  _appsettings.Issuer,
                                  claims,
                                  expires: DateTime.Now.AddMonths(12),
                                  signingCredentials: credentials);
                                var response = new UserResponse
                                {
                                    Id = user.Id,
                                    Token = tokenHandler.WriteToken(token),
                                    UserName = user.UserName,
                                    FullName = user.FullName,
                                    PhoneNumber = user.PhoneNumber,
                                    Roles = userRoles,
                                    IsSuccess = true,
                                };
                                return new JsonResult(response);
                            }
                            else
                            {
                                Response.StatusCode = 441;
                                return new JsonResult("Please enter a valid password");
                            }
                        }
                        else
                        {
                            var identityAuth = _dbContext.IdentityAuth.Where(g => g.UserId == user.Id).FirstOrDefault();
                            if (user.UserName == user.PhoneNumber && identityAuth.MaxAttempt >= 15 && identityAuth.TimeStamp.Date == DateTime.Today)
                            {
                                Response.StatusCode = 441;
                                return new JsonResult("You reached max attempt of the day");
                            }
                            if (user.UserName == user.PhoneNumber)
                            {
                                if (identityAuth.MaxAttempt == 0)
                                {
                                    identityAuth.MaxAttempt = 1;
                                }
                                else
                                {
                                    identityAuth.MaxAttempt++;
                                }
                            }
                            Random random = new Random();
                            identityAuth.UserId = user.Id;
                            identityAuth.TimeStamp = DateTime.Now;
                            identityAuth.Otp = random.Next(999999);
                            //identityAuth.Otp = 123456;
                            if (user.Email != null)
                            {
                                identityAuth.Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                string Subject = "Email Confirmation Otp from " + emailSettings.DisplayName;
                                string body = "<html><body><h3>Welcome to " + emailSettings.DisplayName + "</h3>" +
                                             "<p>OTP to confirm your " + emailSettings.DisplayName + " account is <b>" + identityAuth.Otp + "</b></p>" +
                                             "</body>" +
                                             "</html>";
                                //await SendMail(user.Email, Subject, body);
                            }
                            else if (user.PhoneNumber != null)
                            {
                                identityAuth.Token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                                identityAuth.IsPhone = true;
                                identityAuth.MaxAttempt++;
                                bool isSMSSent;
                                isSMSSent = SendOtp(user.PhoneNumber, identityAuth.Otp, user.Id);
                            }
                            _dbContext.IdentityAuth.Update(identityAuth);
                            await _dbContext.SaveChangesAsync();
                            Response.StatusCode = 406;
                            return new JsonResult(user.UserName);
                        }
                    }
                    else
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("Your account is locked contacat to administrator");
                    }
                }
                else
                {
                    Response.StatusCode = 441;
                    return new JsonResult("User not exist");
                }
            }
            else
            {
                return new JsonResult(null);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(ApplicationUser model)
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);
            if (user != null && model.OldPassword != null && model.NewPassword != null)
            {

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return new JsonResult(Response.StatusCode);
                }
                else
                {
                    Response.StatusCode = 441;
                    return new JsonResult("Please enter valid password detail");
                }
            }
            else
            {
                Response.StatusCode = 441;
                return new JsonResult("Something went wrong, please try again later");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ApiFetchParameters model)
        {
            var user = _userManager.Users.Where(g => g.UserName == model.UserName).FirstOrDefault();
            if (user != null)
            {
                if (user.PhoneNumberConfirmed == true)
                {
                    var identityAuth = _dbContext.IdentityAuth.Where(g => g.UserId == user.Id).FirstOrDefault();
                    if (model.UserName == user.PhoneNumber && identityAuth.MaxAttempt >= 15 && identityAuth.TimeStamp.Date == DateTime.Today)
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("You reached maximum attempt of the day");
                    }

                    var result = await _userManager.GeneratePasswordResetTokenAsync(user);
                    if (result.Length != 0)
                    {
                        Random random = new Random();
                        identityAuth.UserId = user.Id;
                        identityAuth.TimeStamp = DateTime.Now;
                        identityAuth.Otp = random.Next(999999);
                        if (user.PhoneNumber != null)
                        {
                            identityAuth.MaxAttempt++;
                            identityAuth.Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            identityAuth.IsPhone = true;
                            identityAuth.MaxAttempt++;
                            SendOtp(user.PhoneNumber, identityAuth.Otp, user.Id);
                        }
                        _dbContext.IdentityAuth.Update(identityAuth);
                        await _dbContext.SaveChangesAsync();
                        return new JsonResult("OTP sent to your phone number");
                    }
                    else
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("Something went wrong, please try again later");
                    }
                }
                else
                {
                    var identityAuth = _dbContext.IdentityAuth.Where(g => g.UserId == user.Id).FirstOrDefault();
                    if (user.UserName == user.PhoneNumber && identityAuth.MaxAttempt >= 15 && identityAuth.TimeStamp.Date == DateTime.Today)
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("You reached maximum attempt of the day");
                    }
                    Random random = new Random();
                    identityAuth.UserId = user.Id;
                    identityAuth.TimeStamp = DateTime.Now;
                    identityAuth.Otp = random.Next(999999);
                    if (user.PhoneNumber != null)
                    {
                        identityAuth.MaxAttempt++;
                        identityAuth.Token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                        identityAuth.IsPhone = true;
                        identityAuth.MaxAttempt++;
                        SendOtp(user.PhoneNumber, identityAuth.Otp, user.Id);
                    }
                    _dbContext.IdentityAuth.Update(identityAuth);
                    await _dbContext.SaveChangesAsync();
                    Response.StatusCode = 406;
                    return new JsonResult(Response.StatusCode);
                }

            }
            else
            {
                Response.StatusCode = 441;
                return new JsonResult("Username/Phone number not exist please enter valid username/phone number");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ApiFetchParameters model)
        {
            var user = _userManager.Users.Where(g => g.UserName == model.UserName).FirstOrDefault();
            if (user != null)
            {
                var identityAuth = _dbContext.IdentityAuth.Where(g => g.UserId == user.Id).FirstOrDefault();
                if (model.Otp == identityAuth.Otp)
                {
                    if (DateTime.Now.Subtract(identityAuth.TimeStamp) >= TimeSpan.FromMinutes(10))
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("OTP Expired");
                    }
                    var result = await _userManager.ResetPasswordAsync(user, identityAuth.Token, model.Password);
                    if (result.Succeeded)
                    {
                        identityAuth.IsPhone = false;
                        identityAuth.MaxAttempt = 0;
                        _dbContext.IdentityAuth.Update(identityAuth);
                        await _dbContext.SaveChangesAsync();
                        return new JsonResult("Password Reset Success");
                    }
                    else
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("Something went wrong, please try again later");
                    }
                }
                else
                {
                    Response.StatusCode = 441;
                    return new JsonResult("Invalid OTP, Please enter valid OTP");
                }
            }
            else
            {
                Response.StatusCode = 441;
                return new JsonResult("Something went wrong, please try again later");
            }
        }


        // Account Api End //

        // Qualification Api Start //

        public async Task<IActionResult> Qualifications(ApiFetchParameters fetch)
        {
            var model = await _dbContext.Qualifications.Where(g => g.AddedBy == User.Identity.Name || g.UpdatedBy == User.Identity.Name).Skip(fetch.skip).Take(fetch.take).ToListAsync();
            foreach (var item in model)
            {
                item.FileUrl = _appsettings.Host + "/Documents/" + item.FileUrl;
            }
            return new JsonResult(model);
        }

        public async Task<IActionResult> CreateQualification([FromForm] Qualification model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model.File != null)
                    {
                        string ext = model.File.FileName.Substring(model.File.FileName.LastIndexOf("."));
                        string name = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                        model.FileUrl = name;
                    }
                    model.AddedBy = User.Identity.Name;
                    model.TimeStamp = DateTime.Now;
                    await _dbContext.Qualifications.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Qualification Added Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }

        public async Task<IActionResult> GetQualificationById(Qualification fetch)
        {
            var model = await _dbContext.Qualifications.FindAsync(fetch.Id);
            return new JsonResult(model);
        }

        public async Task<IActionResult> UpdateQualification([FromForm] Qualification model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Qualifications.FindAsync(model.Id);
                    if (model.File != null)
                    {
                        if (get.FileUrl != null)
                        {
                            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.FileUrl);
                            if (System.IO.File.Exists(deletePath))
                            {
                                System.IO.File.Delete(deletePath);
                            }
                        }
                        string ext = model.File.FileName.Substring(model.File.FileName.LastIndexOf("."));
                        string name = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                        get.FileUrl = name;
                    }
                    get.Name = model.Name;
                    get.UpdatedBy = User.Identity.Name;
                    get.UpdatedOn = DateTime.Now;
                    _dbContext.Qualifications.Update(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Qualification Updated Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }
        public async Task<IActionResult> DeleteQualification(Qualification fetch)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Qualifications.FindAsync(fetch.Id);
                    if (get.FileUrl != null)
                    {
                        var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.FileUrl);
                        if (System.IO.File.Exists(file))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    _dbContext.Qualifications.Remove(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Qualification Deleted Successfully");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }
        // Qualification Api End //


        // Document Api Start //

        public async Task<IActionResult> Documents(ApiFetchParameters fetch)
        {
            var model = await _dbContext.Documents.Where(g => g.AddedBy == User.Identity.Name || g.UpdatedBy == User.Identity.Name).Skip(fetch.skip).Take(fetch.take).ToListAsync();
            foreach (var item in model)
            {
                item.FileUrl = _appsettings.Host + "/Documents/" + item.FileUrl;
            }
            return new JsonResult(model);
        }

        public async Task<IActionResult> CreateDocument([FromForm] Document model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model.File != null)
                    {
                        string ext = model.File.FileName.Substring(model.File.FileName.LastIndexOf("."));
                        string name = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                        model.FileUrl = name;
                    }
                    model.AddedBy = User.Identity.Name;
                    model.TimeStamp = DateTime.Now;
                    await _dbContext.Documents.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Document Added Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }

        public async Task<IActionResult> GetDocumentById(Document fetch)
        {
            var model = await _dbContext.Documents.FindAsync(fetch.Id);
            return new JsonResult(model);
        }

        public async Task<IActionResult> UpdateDocument([FromForm] Document model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Documents.FindAsync(model.Id);
                    if (model.File != null)
                    {
                        if (get.FileUrl != null)
                        {
                            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.FileUrl);
                            if (System.IO.File.Exists(deletePath))
                            {
                                System.IO.File.Delete(deletePath);
                            }
                        }
                        string ext = model.File.FileName.Substring(model.File.FileName.LastIndexOf("."));
                        string name = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                        get.FileUrl = name;
                    }
                    get.Name = model.Name;
                    get.UpdatedBy = User.Identity.Name;
                    get.UpdatedOn = DateTime.Now;
                    _dbContext.Documents.Update(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Document Updated Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }
        public async Task<IActionResult> DeleteDocument(Document fetch)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Documents.FindAsync(fetch.Id);
                    if (get.FileUrl != null)
                    {
                        var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.FileUrl);
                        if (System.IO.File.Exists(file))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    _dbContext.Documents.Remove(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Document Deleted Successfully");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new JsonResult(e.Message);
                }
            }
        }
        // Document Api End //


        // Experience Api Start //

        public async Task<IActionResult> Experiences(ApiFetchParameters fetch)
        {
            var model = await _dbContext.Experiences.Where(g => g.AddedBy == User.Identity.Name).Skip(fetch.skip).Take(fetch.take).ToListAsync();
            return new JsonResult(model);
        }
        public async Task<IActionResult> CreateExperience([FromForm] Experience model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model.File != null)
                    {
                        string ext = model.File.FileName.Substring(model.File.FileName.LastIndexOf("."));
                        string name = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                        model.FileUrl = name;
                    }
                    model.AddedBy = User.Identity.Name;
                    model.TimeStamp = DateTime.Now;
                    await _dbContext.Experiences.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Experience Added Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }

        public async Task<IActionResult> GetExperienceById(Experience fetch)
        {
            var model = await _dbContext.Experiences.FindAsync(fetch.Id);
            return new JsonResult(model);
        }
        public async Task<IActionResult> UpdateExperience([FromForm] Experience model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Experiences.FindAsync(model.Id);
                    if (model.File != null)
                    {
                        if (get.FileUrl != null)
                        {
                            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.FileUrl);
                            if (System.IO.File.Exists(deletePath))
                            {
                                System.IO.File.Delete(deletePath);
                            }
                        }
                        string ext = model.File.FileName.Substring(model.File.FileName.LastIndexOf("."));
                        string name = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                        get.FileUrl = name;
                    }
                    get.Name = model.Name;
                    get.JoinDate = model.JoinDate;
                    get.LeaveDate = model.LeaveDate;
                    get.Salary = model.Salary;
                    get.UpdatedBy = User.Identity.Name;
                    get.UpdatedOn = DateTime.Now;
                    _dbContext.Experiences.Update(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Experience Updated Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }
        public async Task<IActionResult> DeleteExperience(Experience fetch)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Experiences.FindAsync(fetch.Id);
                    if (get.FileUrl != null)
                    {
                        var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.FileUrl);
                        if (System.IO.File.Exists(file))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    _dbContext.Experiences.Remove(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Experience Deleted Successfully");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new JsonResult(e.Message);
                }
            }
        }
        // Experience Api End //


        // Personal Detail Api Start //

        public async Task<IActionResult> PersonalDetail()
        {
            var model = await _dbContext.PersonalDetails.Where(g => g.AddedBy == User.Identity.Name).FirstOrDefaultAsync();
            //if (model != null)
            //{
            //    if (model.SignUrl != null)
            //    {
            //        model.SignUrl = _appsettings.Host + "/Documents/" + model.SignUrl;
            //    }
            //    if (model.PhotoUrl != null)
            //    {
            //        model.PhotoUrl = _appsettings.Host + "/Documents/" + model.PhotoUrl;
            //    }
            //}
            return new JsonResult(model);
        }
        public async Task<IActionResult> CreatePersonalDetail([FromForm] PersonalDetail model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //if (model.Photo != null)
                    //{
                    //    string ext = model.Photo.FileName.Substring(model.Photo.FileName.LastIndexOf("."));
                    //    string name = Guid.NewGuid() + ext;
                    //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                    //    using (var stream = new FileStream(path, FileMode.Create))
                    //    {
                    //        await model.Photo.CopyToAsync(stream);
                    //    }
                    //    model.PhotoUrl = name;
                    //}
                    //if (model.Sign != null)
                    //{
                    //    string ext = model.Sign.FileName.Substring(model.Sign.FileName.LastIndexOf("."));
                    //    string name = Guid.NewGuid() + ext;
                    //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                    //    using (var stream = new FileStream(path, FileMode.Create))
                    //    {
                    //        await model.Sign.CopyToAsync(stream);
                    //    }
                    //    model.SignUrl = name;
                    //}
                    model.AddedBy = User.Identity.Name;
                    model.TimeStamp = DateTime.Now;
                    await _dbContext.PersonalDetails.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("PersonalDetail Added Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }

        public async Task<IActionResult> UpdatePersonalDetail([FromForm] PersonalDetail model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.PersonalDetails.FindAsync(model.Id);
                    //if (model.Photo != null)
                    //{
                    //    if (get.PhotoUrl != null)
                    //    {
                    //        var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.PhotoUrl);
                    //        if (System.IO.File.Exists(deletePath))
                    //        {
                    //            System.IO.File.Delete(deletePath);
                    //        }
                    //    }
                    //    string ext = model.Photo.FileName.Substring(model.Photo.FileName.LastIndexOf("."));
                    //    string name = Guid.NewGuid() + ext;
                    //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                    //    using (var stream = new FileStream(path, FileMode.Create))
                    //    {
                    //        await model.Photo.CopyToAsync(stream);
                    //    }
                    //    get.PhotoUrl = name;
                    //}
                    //if (model.Sign != null)
                    //{
                    //    if (get.SignUrl != null)
                    //    {
                    //        var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", get.SignUrl);
                    //        if (System.IO.File.Exists(deletePath))
                    //        {
                    //            System.IO.File.Delete(deletePath);
                    //        }
                    //    }
                    //    string ext = model.Sign.FileName.Substring(model.Sign.FileName.LastIndexOf("."));
                    //    string name = Guid.NewGuid() + ext;
                    //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents", name);
                    //    using (var stream = new FileStream(path, FileMode.Create))
                    //    {
                    //        await model.Sign.CopyToAsync(stream);
                    //    }
                    //    get.SignUrl = name;
                    //}
                    get.Name = model.Name;
                    get.FatherName = model.FatherName;
                    get.MotherName = model.MotherName;
                    get.PhoneNumber = model.PhoneNumber;
                    get.AlternateNumber = model.AlternateNumber;
                    get.AadhaarNumber = model.AadhaarNumber;
                    get.Gender = model.Gender;
                    get.Address1 = model.Address1;
                    get.Address2 = model.Address2;
                    get.Country = model.Country;
                    get.State = model.State;
                    get.City = model.City;
                    get.LandMark = model.LandMark;
                    get.PinCode = model.PinCode;
                    get.UpdatedBy = User.Identity.Name;
                    get.UpdatedOn = DateTime.Now;
                    _dbContext.PersonalDetails.Update(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new JsonResult("Personal Detail Updated Successfully");
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult("Something went wrong");
                }
            }
        }

        // Personal Detail Api End //

        // Jobs Api Start //

        public async Task<IActionResult> HomeItems(ApiFetchParameters fetch)
        {
            var categories = await _dbContext.Categories.Where(g => g.IsActive == true).Take(20).ToListAsync();
            if (fetch.id != 0)
            {
                var jobs = await _dbContext.Jobs.Where(g => g.IsActive == true && g.CategoryId == fetch.id).Skip(fetch.skip).Take(fetch.take).ToListAsync();
                foreach (var item in jobs)
                {
                    item.Category = await _dbContext.Categories.FindAsync(item.CategoryId);
                }
                return new JsonResult(new { categories, jobs });
            }
            else
            {
                var jobs = await _dbContext.Jobs.Where(g => g.IsActive == true).Skip(fetch.skip).Take(fetch.take).ToListAsync();
                foreach (var item in jobs)
                {
                    item.Category = await _dbContext.Categories.FindAsync(item.CategoryId);
                }
                return new JsonResult(new { categories, jobs });
            }
        }

        public async Task<IActionResult> MoreCategories(ApiFetchParameters fetch)
        {
            var categories = await _dbContext.Categories.Where(g => g.IsActive == true).Skip(fetch.skip).Take(fetch.take).ToListAsync();
            return new JsonResult(categories);
        }

        public async Task<IActionResult> JobDetail(ApiFetchParameters fetch)
        {
            var job = await _dbContext.Jobs.FindAsync(fetch.id);
            if (job != null)
            {
                job.JobPdfUrl = _appsettings.Host + "/JobPdf/" + job.JobPdfUrl;
            }
            var posts = await (from p in _dbContext.JobPosts
                               join f in _dbContext.FormFees on p.JobPostId equals f.JobPostId
                               where p.JobId == job.JobId
                               select p).OrderBy(o => o.JobPostId).Distinct().ToListAsync();
            foreach (var item in posts)
            {
                var applied = await _dbContext.ApplicationHistories.Where(g => g.JobPostId == item.JobPostId && g.AddedBy == User.Identity.Name).FirstOrDefaultAsync();
                if (applied != null)
                {
                    item.IsApplied = true;
                }
                item.Fees = await _dbContext.FormFees.Where(g => g.JobPostId == item.JobPostId).ToListAsync();
            }
            var data = new { job, posts };
            return new JsonResult(data);
        }

        // Jobs Api End //

        // Payment Api Actions Start //

        [HttpPost]
        public async Task<IActionResult> InitiatePayment(List<JobApply> model)
        {
            decimal amount = 0;
            try
            {
                var user = await _userManager.FindByIdAsync(User.Identity.Name);
                foreach (var item in model)
                {
                    var postFees = await (from p in _dbContext.JobPosts
                                          join f in _dbContext.FormFees on p.JobPostId equals f.JobPostId
                                          where p.JobPostId == item.PostId && f.Id == item.FormFeeId
                                          select f).FirstOrDefaultAsync();

                    var post = await _dbContext.JobPosts.FindAsync(item.PostId);
                    var applied = await _dbContext.ApplicationHistories.Where(g => g.JobPostId == item.PostId && g.AddedBy == User.Identity.Name).FirstOrDefaultAsync();
                    if (applied != null)
                    {
                        Response.StatusCode = 441;
                        return new JsonResult("You are already applied for " + post.JobPostName);
                    }
                    if (postFees != null)
                    {
                        amount = amount + postFees.FormFee + postFees.FormFillingFee + postFees.AdmitCardFee;
                    }
                }

                string RazorpayKey = _paymentGateway.PublicKey,

                RazorpaySecret = _paymentGateway.SecretKey;

                RazorpayClient client = new RazorpayClient(RazorpayKey, RazorpaySecret);
                Dictionary<string, object> options = new Dictionary<string, object>();

                //Required POST parameters
                options.Add("amount", amount * 100);
                options.Add("currency", "INR");
                options.Add("payment_capture", "0");

                Order orderResponse = client.Order.Create(options);
                string orderId = orderResponse["id"].ToString();
                OrderModel orderModel = new OrderModel();
                orderModel.orderId = orderResponse.Attributes["id"];
                orderModel.razorpayKey = RazorpayKey;
                orderModel.amount = amount * 100;
                orderModel.currency = "INR";
                if (user.FullName != null)
                {
                    orderModel.name = user.FullName;
                }
                else
                {
                    orderModel.name = user.UserName;
                }
                orderModel.email = user.Email;
                orderModel.contactNumber = user.PhoneNumber;
                return new JsonResult(orderModel);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 441;
                return new JsonResult(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CompletePayment(string paymentId, List<JobApply> model)
        {
            string RazorpayKey = _paymentGateway.PublicKey,

                RazorpaySecret = _paymentGateway.SecretKey;
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server

            RazorpayClient client = new RazorpayClient(RazorpayKey, RazorpaySecret);

            Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Payment paymentCaptured = payment.Capture(options);

            //  Payment Response to model //
            PaymentHistory paymentHistory = new PaymentHistory
            {
                TransactionId = paymentId,
                Amount = paymentCaptured.Attributes["amount"] / 100,
                Status = paymentCaptured.Attributes["status"],
                OrderId = paymentCaptured.Attributes["order_id"],
                PaymentMode = paymentCaptured.Attributes["method"],
                PaidBy = User.Identity.Name,
                TimeStamp = DateTime.Now,
            };


            //// Check payment made successfully
            if (paymentHistory.Status == "captured")
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (paymentHistory == null)
                        {
                            throw new NullReferenceException();
                        }
                        else
                        {
                            await _dbContext.PaymentHistories.AddAsync(paymentHistory);
                            await _dbContext.SaveChangesAsync();
                            foreach (var item in model)
                            {
                                var postFees = await (from p in _dbContext.JobPosts
                                                      join f in _dbContext.FormFees on p.JobPostId equals f.JobPostId
                                                      where p.JobPostId == item.PostId && f.Id == item.FormFeeId
                                                      select new { f, p }).FirstOrDefaultAsync();
                                var history = new ApplicationHistory
                                {
                                    JobId = postFees.p.JobId,
                                    JobPostId = postFees.p.JobPostId,
                                    PaymentId = paymentHistory.Id,
                                    Category = postFees.f.Category,
                                    FormFee = postFees.f.FormFee,
                                    FormFillingFee = postFees.f.FormFillingFee,
                                    AdmitCardFee = postFees.f.AdmitCardFee,
                                    Status = "Requested",
                                    Remark = "Request Sent",
                                    AddedBy = User.Identity.Name,
                                    TimeStamp = DateTime.Now,
                                };
                                await _dbContext.ApplicationHistories.AddAsync(history);
                                await _dbContext.SaveChangesAsync();
                            }
                            transaction.Commit();
                            return new JsonResult("Request Send Successfully");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        //initialize the SDK client
                        RazorpayClient client1 = new RazorpayClient(RazorpayKey, RazorpaySecret);

                        // payment to be refunded, payment must be a captured payment
                        Payment payment1 = client.Payment.Fetch(paymentId);

                        //Full Refund
                        Refund refund = payment.Refund();
                        Response.StatusCode = 441;
                        return new JsonResult("Failed to place your order, if payment dedected from your account, will refunded within 5-7 working days");
                    }
                }
            }
            else
            {
                await _dbContext.PaymentHistories.AddAsync(paymentHistory);
                await _dbContext.SaveChangesAsync();
                Response.StatusCode = 441;
                return new JsonResult("Payment Failed, if payment dedected from your account, will refunded within 5-7 working days");
            }
        }

        // Payment Api Actions End //


        // Application History Actions Start //

        public async Task<IActionResult> AppliedJobs(ApiFetchParameters fetch)
        {
            var get = await _dbContext.ApplicationHistories.Where(g => g.AddedBy == User.Identity.Name).Skip(fetch.skip).Take(fetch.take).ToListAsync();
            foreach (var item in get)
            {
                item.Job = await _dbContext.Jobs.FindAsync(item.JobId);
                item.Job.Category = await _dbContext.Categories.FindAsync(item.Job.CategoryId);
                item.JobPost = await _dbContext.JobPosts.FindAsync(item.JobPostId);
            }
            return new JsonResult(get);
        }

        public async Task<IActionResult> ApplicationHistory(ApiFetchParameters fetch)
        {
            var get = await _dbContext.ApplicationHistories.Where(g => g.AddedBy == User.Identity.Name).Skip(fetch.skip).Take(fetch.take).ToListAsync();
            foreach (var item in get)
            {
                item.Job = await _dbContext.Jobs.FindAsync(item.JobId);
                item.Job.Category = await _dbContext.Categories.FindAsync(item.Job.CategoryId);
                item.JobPost = await _dbContext.JobPosts.FindAsync(item.JobPostId);
            }
            return new JsonResult(get);
        }


        public async Task<IActionResult> AdmitCards(ApiFetchParameters fetch)
        {
            var get = await _dbContext.ApplicationHistories.Where(g => g.AddedBy == User.Identity.Name && g.AdmitCardUrl != null).Skip(fetch.skip).Take(fetch.take).ToListAsync();
            foreach (var item in get)
            {
                if (item.AdmitCardUrl != null)
                {
                    item.AdmitCardUrl = _appsettings.Host + "/AdmitCards/" + item.AdmitCardUrl;
                }
                item.Job = await _dbContext.Jobs.FindAsync(item.JobId);
                item.Job.Category = await _dbContext.Categories.FindAsync(item.Job.CategoryId);
                item.JobPost = await _dbContext.JobPosts.FindAsync(item.JobPostId);
            }
            return new JsonResult(get);
        }

        public async Task<IActionResult> ApplicationDetail(ApiFetchParameters fetch)
        {
            var get = await _dbContext.ApplicationHistories.FindAsync(fetch.id);
            get.Job = await _dbContext.Jobs.FindAsync(get.JobId);
            get.JobPost = await _dbContext.JobPosts.FindAsync(get.JobPostId);
            get.PaymentHistory = await _dbContext.PaymentHistories.FindAsync(get.PaymentId);
            return new JsonResult(get);
        }

        // Application History Actions Start //

        public async Task<IActionResult> ServiceNotification(ServiceNotification model)
        {
            var exictservice = await _dbContext.ServiceNotifications.Where(w => w.Services == model.Services && w.AddedBy == User.Identity.Name).FirstOrDefaultAsync();
            if(exictservice != null)
            {
                Response.StatusCode = 441;
                return new JsonResult("Already Applied");
            }
            try
            {
                model.AddedBy = User.Identity.Name;
                model.TimeStamp = DateTime.Now;
                await _dbContext.ServiceNotifications.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return new JsonResult(model);
            }
            catch (Exception e)
            {
                Response.StatusCode = 441;
                return new JsonResult(e.Message);
            }
        }
    }
}
