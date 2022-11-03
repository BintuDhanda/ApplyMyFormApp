using rojgar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Razorpay.Api;
using Microsoft.Extensions.Options;

namespace rojgar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;
        private readonly PaymentGateway _paymentGateway;

        public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, IOptions<PaymentGateway> paymentGateway)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = appDbContext;
            _paymentGateway = paymentGateway.Value;
        }
        public async Task<IActionResult> Index()
        {
            int countuser = await _dbContext.Users.CountAsync();
            ViewBag.totaluser = countuser;

            int countcategory = await _dbContext.Categories.CountAsync();
            ViewBag.totalcategory = countcategory;

            int countjobs = await _dbContext.Jobs.CountAsync();
            ViewBag.totaljobs = countjobs;

            int countrequest = await _dbContext.ApplicationHistories.CountAsync();
            ViewBag.totalrequest = countrequest;

            int countprocessed = await _dbContext.ApplicationHistories.Where(g => g.Status == "Processed").CountAsync();
            ViewBag.totalprocessed = countprocessed;

            int countcomplete = await _dbContext.ApplicationHistories.Where(g => g.Status == "Completed").CountAsync();
            ViewBag.totalcomplete = countcomplete;

            int countcanceled = await _dbContext.ApplicationHistories.Where(g => g.Status == "Cancelled").CountAsync();
            ViewBag.totalcanceled = countcanceled;

            int countpayment = await _dbContext.PaymentHistories.CountAsync();
            ViewBag.totalpayment = countpayment;

            int countrefunds = await _dbContext.RefundHistories.CountAsync();
            ViewBag.totalrefunds = countrefunds;



            return View();
        }






        // User Actions Start //

        public IActionResult admissions()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            var users = await (from u in _dbContext.Users
                               join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                               join r in _dbContext.Roles on ur.RoleId equals r.Id
                               where r.Name == "User" && u.UserName != "Admin@123"
                               select u).ToListAsync();
            ViewData.Model = users;
            return View();
        }

        public async Task<IActionResult> EnableDisableUser(string id)
        {
            var model = await _dbContext.Users.FindAsync(id);
            if (model.IsActive)
            {
                model.IsActive = false;
                TempData["Status"] = "User Locked Successfully";
            }
            else
            {
                model.IsActive = true;
                TempData["Status"] = "User Unlocked Successfully";
            }
            _dbContext.Users.Update(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Users");
        }

        // User Actions End //


        // Category Actions Start //

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            ViewData.Model = await _dbContext.Categories.ToListAsync();
            return View();
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category model)
        {
            model.TimeStamp = System.DateTime.Now;
            model.AddedBy = _userManager.GetUserId(User);
            model.IsActive = true;
            _dbContext.Categories.Add(model);
            await _dbContext.SaveChangesAsync();
            TempData["Status"] = "Category Added Successfully";
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Int64 id = 0)
        {
            var model = await _dbContext.Categories.Where(g => g.Id == id).FirstOrDefaultAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category model)
        {
            var get = await _dbContext.Categories.Where(g => g.Id == model.Id).FirstOrDefaultAsync();
            get.Name = model.Name;
            _dbContext.Categories.Update(get);
            await _dbContext.SaveChangesAsync();
            TempData["Status"] = "Category Updated Successfully";
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(Int64 id = 0)
        {
            var model = await _dbContext.Categories.FindAsync(id);
            _dbContext.Categories.Remove(model);
            await _dbContext.SaveChangesAsync();
            TempData["Status"] = "Category Deleted Successfully";
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public async Task<IActionResult> EnableDisableCategory(Int64 id = 0)
        {
            var model = await _dbContext.Categories.FindAsync(id);
            if (model.IsActive == true)
            {
                model.IsActive = false;
                TempData["Status"] = "Category Disabled Successfully";
            }
            else
            {
                model.IsActive = true;
                TempData["Status"] = "Category Enabled Successfully";
            }
            _dbContext.Categories.Update(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Categories");
        }

        // Category Actions End //

        // Job Actions Start //
        public async Task<IActionResult> Jobs()
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            ViewData.Model = await _dbContext.Jobs.ToListAsync();
            return View();
        }
        public async Task<IActionResult> JobDetail(Int64 id = 0)
        {
            ViewData.Model = await _dbContext.Jobs.FindAsync(id);
            return View();
        }
        public async Task<IActionResult> CreateJob()
        {
            ViewBag.Categories = await _dbContext.Categories.Where(g => g.IsActive).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateJob(Job model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    model.AddedBy = _userManager.GetUserId(User);
                    model.TimeStamp = DateTime.Now;
                    model.IsActive = true;
                    if (model.JobPdf != null)
                    {
                        var ext = model.JobPdf.FileName.Substring(model.JobPdf.FileName.LastIndexOf("."));
                        model.JobPdfUrl = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\JobPdf", model.JobPdfUrl);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.JobPdf.CopyToAsync(stream);
                        }
                    }
                    await _dbContext.Jobs.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    TempData["Status"] = "Job Added Successfully";
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("Jobs");
            }
        }
        public async Task<IActionResult> EditJob(Int64 id = 0)
        {
            ViewBag.Categories = await _dbContext.Categories.Where(g => g.IsActive).ToListAsync();
            var model = await _dbContext.Jobs.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditJob(Job model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Jobs.FindAsync(model.JobId);
                    if (model.JobPdf != null)
                    {
                        if (get.JobPdfUrl != null)
                        {
                            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\JobPdf", get.JobPdfUrl);
                            if (System.IO.File.Exists(deletePath))
                            {
                                System.IO.File.Delete(deletePath);
                            }
                        }
                        var ext = model.JobPdf.FileName.Substring(model.JobPdf.FileName.LastIndexOf("."));
                        get.JobPdfUrl = Guid.NewGuid() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\JobPdf", get.JobPdfUrl);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.JobPdf.CopyToAsync(stream);
                        }
                    }
                    get.JobName = model.JobName;
                    get.CategoryId = model.CategoryId;
                    get.JobDepartment = model.JobDepartment;
                    get.AdvtNumber = model.AdvtNumber;
                    get.JobDescription = model.JobDescription;
                    get.JobPublishDate = model.JobPublishDate;
                    get.JobStartDate = model.JobStartDate;
                    get.JobEndDate = model.JobEndDate;
                    get.QualificationLevel = model.QualificationLevel;
                    _dbContext.Jobs.Update(get);
                    await _dbContext.SaveChangesAsync();
                    TempData["Status"] = "Job Updated Successfully";
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("Jobs");
            }
        }
        public async Task<IActionResult> DeleteJob(Int64 id = 0)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.Jobs.FindAsync(id);
                    if (get.JobPdfUrl != null)
                    {
                        var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\JobPdf", get.JobPdfUrl);
                        if (System.IO.File.Exists(deletePath))
                        {
                            System.IO.File.Delete(deletePath);
                        }
                    }
                    _dbContext.Jobs.Remove(get);
                    await _dbContext.SaveChangesAsync();
                    TempData["Status"] = "Job Deleted Successfully";
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("Jobs");
            }
        }
        public async Task<IActionResult> EnableDisableJob(Int64 id = 0)
        {
            var model = await _dbContext.Jobs.FindAsync(id);
            if (model.IsActive)
            {
                model.IsActive = false;
                TempData["Status"] = "Job Disabled Successfully";
            }
            else
            {
                model.IsActive = true;
                TempData["Status"] = "Job Enabled Successfully";
            }
            _dbContext.Jobs.Update(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Jobs");
        }
        // Job Actions End //

        // Job Posts Actions Start //
        public async Task<IActionResult> JobPosts(Int64 id = 0)
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            ViewData.Model = await _dbContext.JobPosts.Where(g => g.JobId == id).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateJobPost(JobPost model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    model.AddedBy = _userManager.GetUserId(User);
                    model.TimeStamp = DateTime.Now;
                    await _dbContext.JobPosts.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    TempData["Status"] = "Post Added Successfully";
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("JobPosts", new { id = model.JobId });
            }
        }
        public async Task<IActionResult> EditJobPost(Int64 id = 0)
        {
            var model = await _dbContext.JobPosts.FindAsync(id);
            return new JsonResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditJobPost(JobPost model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.JobPosts.FindAsync(model.JobPostId);
                    get.JobPostName = model.JobPostName;
                    get.NumberOfPost = model.NumberOfPost;
                    get.JobPostGender = model.JobPostGender;
                    _dbContext.JobPosts.Update(get);
                    await _dbContext.SaveChangesAsync();
                    TempData["Status"] = "Post Updated Successfully";
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("JobPosts", new { id = model.JobId });
            }
        }
        public async Task<IActionResult> DeleteJobPost(Int64 id = 0)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var get = await _dbContext.JobPosts.FindAsync(id);
                try
                {
                    _dbContext.JobPosts.Remove(get);
                    await _dbContext.SaveChangesAsync();
                    TempData["Status"] = "Post Deleted Successfully";
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("JobPosts", new { id = get.JobId });
            }
        }

        // Job Posts Actions End //

        // Form Fees Actions Start //
        public async Task<IActionResult> FormFees(Int64 id = 0)
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            ViewData.Model = await _dbContext.FormFees.Where(g => g.JobPostId == id).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFormFee(FormFees model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var check = await _dbContext.FormFees.Where(g => g.JobPostId == model.JobPostId && g.Category.ToLower() == model.Category.ToLower()).FirstOrDefaultAsync();
                    if (check == null)
                    {
                        model.AddedBy = _userManager.GetUserId(User);
                        model.TimeStamp = DateTime.Now;
                        await _dbContext.FormFees.AddAsync(model);
                        await _dbContext.SaveChangesAsync();
                        TempData["Status"] = "Form Fees Added Successfully";
                        transaction.Commit();
                    }
                    else
                    {
                        TempData["Status"] = "Form Fees already exist for this category";
                    }
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("FormFees", new { id = model.JobPostId });
            }
        }
        public async Task<IActionResult> EditFormFee(Int64 id = 0)
        {
            var model = await _dbContext.FormFees.FindAsync(id);
            return new JsonResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditFormFee(FormFees model)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var check = await _dbContext.FormFees.Where(g => g.JobPostId == model.JobPostId && g.Category.ToLower() == model.Category.ToLower()
                    && g.Id != model.Id).FirstOrDefaultAsync();
                    if (check == null)
                    {
                        var get = await _dbContext.FormFees.FindAsync(model.Id);
                        get.FormFee = model.FormFee;
                        get.FormFillingFee = model.FormFillingFee;
                        get.AdmitCardFee = model.AdmitCardFee;
                        get.Category = model.Category;
                        _dbContext.FormFees.Update(get);
                        await _dbContext.SaveChangesAsync();
                        TempData["Status"] = "Form Fees Updated Successfully";
                        transaction.Commit();
                    }
                    else
                    {
                        TempData["Status"] = "Form Fees already exist for this category";
                    }
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("FormFees", new { id = model.JobPostId });
            }
        }
        public async Task<IActionResult> DeleteFormFee(Int64 id = 0)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var get = await _dbContext.FormFees.FindAsync(id);
                try
                {
                    _dbContext.FormFees.Remove(get);
                    await _dbContext.SaveChangesAsync();
                    TempData["Status"] = "Form Fees Deleted Successfully";
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    TempData["Status"] = e.Message;
                    transaction.Rollback();
                }
                return RedirectToAction("FormFees", new { id = get.JobPostId });
            }
        }

        // Form Fees Actions End //


        // Applicatin History Actions Start //

        public async Task<IActionResult> ApplicationRequests()
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            var list = await _dbContext.ApplicationHistories.Where(g => g.Status == "Requested").ToListAsync();
            foreach (var item in list)
            {
                item.JobPost = await _dbContext.JobPosts.FindAsync(item.JobPostId);
                item.User = await _userManager.FindByIdAsync(item.AddedBy);
            }
            ViewData.Model = list;
            return View();
        }

        public async Task<IActionResult> ApplicationProcessed()
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            var list = await _dbContext.ApplicationHistories.Where(g => g.Status == "Processed").ToListAsync();
            foreach (var item in list)
            {
                item.JobPost = await _dbContext.JobPosts.FindAsync(item.JobPostId);
                item.User = await _userManager.FindByIdAsync(item.AddedBy);
            }
            ViewData.Model = list;
            return View();
        }


        public async Task<IActionResult> ApplicationCompleted()
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            var list = await _dbContext.ApplicationHistories.Where(g => g.Status == "Completed").ToListAsync();
            foreach (var item in list)
            {
                item.JobPost = await _dbContext.JobPosts.FindAsync(item.JobPostId);
                item.User = await _userManager.FindByIdAsync(item.AddedBy);
            }
            ViewData.Model = list;
            return View();
        }


        public async Task<IActionResult> ApplicationCancelled()
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            var list = await _dbContext.ApplicationHistories.Where(g => g.Status == "Cancelled").ToListAsync();
            foreach (var item in list)
            {
                item.JobPost = await _dbContext.JobPosts.FindAsync(item.JobPostId);
                item.User = await _userManager.FindByIdAsync(item.AddedBy);
            }
            ViewData.Model = list;
            return View();
        }

        public async Task<IActionResult> ApplicationDetail(Int64 id = 0)
        {
            if (TempData["Status"] != null)
            {
                ModelState.AddModelError("", TempData["Status"].ToString());
            }
            var model = await _dbContext.ApplicationHistories.FindAsync(id);
            model.PaymentHistory = await _dbContext.PaymentHistories.FindAsync(model.PaymentId);
            model.RefundHistory = await _dbContext.RefundHistories.Where(g => g.ApplicationHistoryId == model.Id).FirstOrDefaultAsync();
            ViewData.Model = model;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadAdmitCard(ApplicationHistory model)
        {

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.ApplicationHistories.FindAsync(model.Id);
                    if (get.AdmitCardUrl != null)
                    {
                        var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\AdmitCards", get.AdmitCardUrl);
                        if (System.IO.File.Exists(deletePath))
                        {
                            System.IO.File.Delete(deletePath);
                        }
                    }
                    var ext = model.AdmitCard.FileName.Substring(model.AdmitCard.FileName.LastIndexOf("."));
                    get.AdmitCardUrl = Guid.NewGuid() + ext;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\AdmitCards", get.AdmitCardUrl);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.AdmitCard.CopyToAsync(stream);
                    }
                    get.UpdatedBy = _userManager.GetUserId(User);
                    get.UpdatedOn = DateTime.Now;
                    _dbContext.ApplicationHistories.Update(get);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    TempData["Status"] = "Admit Card Uploaded Successfully";
                }
                catch
                {
                    transaction.Rollback();
                    TempData["Status"] = "Admit Card not uploaded try again";

                }
                return RedirectToAction("ApplicationCompleted");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateApplicationStatus(ApplicationHistory model, string ReturnUrl)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var get = await _dbContext.ApplicationHistories.FindAsync(model.Id);
                    if (get.Status == "Cancelled")
                    {
                        TempData["Status"] = "This application Request is already cancelled";
                    }
                    else if (get.Status == "Completed")
                    {
                        TempData["Status"] = "This application Request is completed";
                    }
                    else
                    {
                        get.Status = model.Status;
                        get.Remark = model.Remark;
                        if (model.Status == "Cancelled")
                        {
                            string RazorpayKey = _paymentGateway.PublicKey,

                                RazorpaySecret = _paymentGateway.SecretKey;
                            var paymentHistory = await _dbContext.PaymentHistories.FindAsync(get.PaymentId);

                            //initialize the SDK client
                            RazorpayClient client = new RazorpayClient(RazorpayKey, RazorpaySecret);

                            // payment to be refunded, payment must be a captured payment
                            Payment payment = client.Payment.Fetch(paymentHistory.TransactionId);
                            // payment to be refunded, payment must be a captured payment

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("amount", Convert.ToInt32(get.FormFee + get.FormFillingFee + get.AdmitCardFee) * 100);

                            // Partial Refund
                            Refund refund = payment.Refund(data);
                            var refundHistory = new RefundHistory();
                            refundHistory.PaymentHistoryId = paymentHistory.Id;
                            refundHistory.ApplicationHistoryId = get.Id;
                            refundHistory.TransactionId = refund.Attributes["id"];
                            refundHistory.OrderId = refund.Attributes["receipt"];
                            refundHistory.Amount = refund.Attributes["amount"] / 100;
                            refundHistory.Status = refund.Attributes["status"];

                            await _dbContext.RefundHistories.AddAsync(refundHistory);
                            await _dbContext.SaveChangesAsync();
                        }
                        _dbContext.ApplicationHistories.Update(get);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    TempData["Status"] = e.Message;
                }
                return RedirectToAction(ReturnUrl);
            }
        }

        // Applicatin History Actions End //


        // Payment History Actions Start //

        public async Task<IActionResult> PaymentHistories()
        {
            var model = await _dbContext.PaymentHistories.ToListAsync();
            foreach (var item in model)
            {
                item.User = await _userManager.FindByIdAsync(item.PaidBy);
            }
            ViewData.Model = model;
            return View();
        }

        // Payment History Actions End //


        // Document Actions Start //
        public IActionResult Documents()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Documents(string PhoneNumber)
        {
            var user = await _userManager.FindByNameAsync(PhoneNumber);
            if (user != null)
            {
                var model = await _dbContext.Documents.Where(g => g.AddedBy == user.Id).ToListAsync();
                ViewData.Model = model;
            }
            else
            {
                ModelState.AddModelError("", "No any user exist with this phone number");
            }
            return View();
        }

        public IActionResult QualificationDocuments()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> QualificationDocuments(string PhoneNumber)
        {
            var user = await _userManager.FindByNameAsync(PhoneNumber);
            if (user != null)
            {
                var model = await _dbContext.Qualifications.Where(g => g.AddedBy == user.Id).ToListAsync();
                ViewData.Model = model;
            }
            else
            {
                ModelState.AddModelError("", "No any user exist with this phone number");
            }
            return View();
        }

        public IActionResult PersonalDetails()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PersonalDetails(string PhoneNumber)
        {
            var user = await _userManager.FindByNameAsync(PhoneNumber);
            if (user != null)
            {
                var model = await _dbContext.PersonalDetails.Where(g => g.AddedBy == user.Id).FirstOrDefaultAsync();
                ViewData.Model = model;
            }
            else
            {
                ModelState.AddModelError("", "No any user exist with this phone number");
            }
            return View();
        }
        public IActionResult Experiences()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Experiences(string PhoneNumber)
        {
            var user = await _userManager.FindByNameAsync(PhoneNumber);
            if (user != null)
            {
                var model = await _dbContext.Experiences.Where(g => g.AddedBy == user.Id).ToListAsync();
                ViewData.Model = model;
            }
            else
            {
                ModelState.AddModelError("", "No any user exist with this phone number");
            }
            return View();
        }
        // Document Actions End //

        public async Task<IActionResult> ServiceNotification()
        {
            var model = await _dbContext.ServiceNotifications.Include(i => i.User).ToListAsync();
            ViewData.Model = model;
            return View();
        }
    }
}
