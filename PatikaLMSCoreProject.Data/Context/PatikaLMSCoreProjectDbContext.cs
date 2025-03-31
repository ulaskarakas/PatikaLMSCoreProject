using Microsoft.EntityFrameworkCore;
using PatikaLMSCoreProject.Data.Entities;

namespace PatikaLMSCoreProject.Data.Context
{
    public class PatikaLMSCoreProjectDbContext : DbContext
    {
        public PatikaLMSCoreProjectDbContext(DbContextOptions<PatikaLMSCoreProjectDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode = false
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<FeatureEntity> Features => Set<FeatureEntity>();
        public DbSet<CourseEntity> Courses => Set<CourseEntity>();
        public DbSet<CourseFeatureEntity> CourseFeatures => Set<CourseFeatureEntity>();
        public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();
        public DbSet<ModuleEntity> Modules => Set<ModuleEntity>();
        public DbSet<SettingEntity> Settings => Set<SettingEntity>();
    }
}