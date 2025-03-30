using System.ComponentModel.DataAnnotations;

namespace PatikaLMSCoreProject.WebApi.Models
{
    public class AddFeatureRequest
    {
        [Required]
        [Length(5, 30)]
        public string Title { get; set; }
    }
}