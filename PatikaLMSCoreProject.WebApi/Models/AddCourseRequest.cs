using PatikaLMSCoreProject.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace PatikaLMSCoreProject.WebApi.Models
{
    public class AddCourseRequest
    {
        [Required]
        public string Name { get; set; }
        public int? Stars { get; set; }
        [Required]
        public EducationType EducationType { get; set; }
        public List<int> FeatureIds { get; set; }
    }
}