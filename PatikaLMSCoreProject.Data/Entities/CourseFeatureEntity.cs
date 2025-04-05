using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PatikaLMSCoreProject.Data.Entities
{
    public class CourseFeatureEntity : BaseEntity
    {
        public int CourseId { get; set; }
        public int FeatureId { get; set; }
        // Relational Properties
        public CourseEntity Course { get; set; }
        public FeatureEntity Feature { get; set; }
    }

    public class CourseFeatureConfiguration : BaseConfiguration<CourseFeatureEntity>
    {
        public override void Configure(EntityTypeBuilder<CourseFeatureEntity> builder)
        {
            builder.Ignore(x => x.Id); // Id property is ignored, it will not be passed to the table
            builder.HasKey("CourseId", "FeatureId"); // Composite Key was created and assigned as the new Primary Key

            base.Configure(builder);
        }
    }
}