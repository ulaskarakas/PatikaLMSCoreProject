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
            builder.Ignore(x => x.Id); // Id property'sini görmezden geldik, tabloya aktarılmayacak.
            builder.HasKey("CourseId", "FeatureId"); // Composite Key oluşturup yeni Primary Key olarak atadık.

            base.Configure(builder);
        }
    }
}