using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PatikaLMSCoreProject.Data.Entities
{
    public class EnrollmentEntity : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StudentCount { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        // Relational Properties
        public UserEntity User { get; set; }
        public ModuleEntity Module { get; set; }
    }

    public class EnrollmentConfiguration : BaseConfiguration<EnrollmentEntity>
    {
        public override void Configure(EntityTypeBuilder<EnrollmentEntity> builder)
        {
            base.Configure(builder);
        }
    }
}