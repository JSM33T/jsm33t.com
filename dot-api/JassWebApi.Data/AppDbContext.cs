using JassWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace JassWebApi.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor for Dependency Injection
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<VerificationToken> VerificationTokens { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordRecovery> PasswordRecoveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint on Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Unique constraint on GoogleUserId (if not null)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.GoogleUserId)
                .IsUnique();

            // One-to-many: User -> UserSessions
            modelBuilder.Entity<UserSession>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSessions)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one: UserSession -> RefreshToken
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.UserSession)
                .WithMany(us => us.RefreshTokens)
                .HasForeignKey(rt => rt.UserSessionId)
                .OnDelete(DeleteBehavior.Cascade);


            // One-to-many: User -> PasswordRecoveries
            modelBuilder.Entity<PasswordRecovery>()
                .HasOne(pr => pr.User)
                .WithMany()
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VerificationToken>()
                .HasOne(vt => vt.User)
                .WithMany()
                .HasForeignKey(vt => vt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                    }
    }
}
