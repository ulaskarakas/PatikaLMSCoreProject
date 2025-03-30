using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaLMSCoreProject.Business.Operations.Feature;
using PatikaLMSCoreProject.Business.Operations.Feature.Dtos;
using PatikaLMSCoreProject.WebApi.Models;

namespace PatikaLMSCoreProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFeature(AddFeatureRequest request)
        {
            var addFeatureDto = new AddFeatureDto
            {
                Title = request.Title
            };

            var result = await _featureService.AddFeature(addFeatureDto);

            if (result.IsSucceed)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }
}
