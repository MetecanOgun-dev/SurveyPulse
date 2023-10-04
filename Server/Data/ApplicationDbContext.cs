using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyPulse.Shared;
using System.Reflection.Emit;

namespace SurveyPulse.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);

            modelBuilder.Entity<Survey>()
                .HasKey(s => s.SurveyId);

            modelBuilder.Entity<SurveyDetail>()
                .HasKey(sd => sd.SurveyDetailId);

            modelBuilder.Entity<Question>()
                .HasKey(sd => sd.QuestionId);

            modelBuilder.Entity<QuestionOption>()
                .HasKey(qo => qo.QuestionOptionId);

            modelBuilder.Entity<Survey>()
                .HasOne(s => s.SurveyDetail)
                .WithOne(sd => sd.Survey)
                .HasForeignKey<SurveyDetail>(sd => sd.SurveyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Survey)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Respondent>()
                .HasOne(r => r.Survey)
                .WithMany(s => s.Respondents)
                .HasForeignKey(r => r.SurveyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuestionOption>()
                .HasOne(qo => qo.Question)
                .WithMany(q => q.QuestionOptions)
                .HasForeignKey(qo => qo.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyDetail> SurveyDetails { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Respondent> Respondeds { get; set; }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasData(
                new AppRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new AppRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" },
                new AppRole() { Name = "NonUser", ConcurrencyStamp = "3", NormalizedName = "NONUSER" }
                );
        }

    }
}
