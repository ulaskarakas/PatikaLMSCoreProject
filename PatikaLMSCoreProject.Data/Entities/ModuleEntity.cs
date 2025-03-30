using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PatikaLMSCoreProject.Data.Entities
{
    public class ModuleEntity : BaseEntity
    {
        public string ModuleName { get; set; }
        public int CourseId { get; set; }
        // Relational Properties
        public ICollection<EnrollmentEntity> Enrollments { get; set; }
        public CourseEntity Course { get; set; }
    }

    public class ModuleConfiguration : BaseConfiguration<ModuleEntity>
    {
        public override void Configure(EntityTypeBuilder<ModuleEntity> builder)
        {
            base.Configure(builder);
        }
    }
}