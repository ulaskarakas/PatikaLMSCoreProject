using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatikaLMSCoreProject.Data.Enums;

namespace PatikaLMSCoreProject.Data.Entities
{
    public class CourseEntity : BaseEntity
    {
        public string Name { get; set; }
        public int? Stars { get; set; }
        public EducationType EducationType { get; set; }
        // Relational Properties
        public ICollection<CourseFeatureEntity> CourseFeatures { get; set; }
        public ICollection<ModuleEntity> Modules { get; set; }
    }

    public class CourseConfiguration : BaseConfiguration<CourseEntity>
    {
        public override void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.Property(x => x.Stars).IsRequired(false);
            builder.Property(x => x.Name).IsRequired()
                                         .HasMaxLength(80);

            base.Configure(builder);
        }
    }
}