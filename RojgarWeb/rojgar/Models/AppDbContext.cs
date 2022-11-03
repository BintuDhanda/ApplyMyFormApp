using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace rojgar.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<IdentityAuth> IdentityAuth { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<PersonalDetail> PersonalDetails { get; set; }     
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<FormFees> FormFees { get; set; }
        public DbSet<ApplicationHistory> ApplicationHistories { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<RefundHistory> RefundHistories { get; set; }
        public DbSet<PushNotification> PushNotifications { get; set; }
        public DbSet<ServiceNotification> ServiceNotifications { get; set; }


    }
}
