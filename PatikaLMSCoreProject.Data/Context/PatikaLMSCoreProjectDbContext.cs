using Microsoft.EntityFrameworkCore;
using PatikaLMSCoreProject.Data.Entities;

namespace PatikaLMSCoreProject.Data.Context
{
    public class PatikaLMSCoreProjectDbContext : DbContext
    {
        public PatikaLMSCoreProjectDbContext(DbContextOptions<PatikaLMSCoreProjectDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<FeatureEntity> Features => Set<FeatureEntity>();
        public DbSet<CourseEntity> Courses => Set<CourseEntity>();
        public DbSet<CourseFeatureEntity> CourseFeatures => Set<CourseFeatureEntity>();
        public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();
        public DbSet<ModuleEntity> Modules => Set<ModuleEntity>();
    }
}