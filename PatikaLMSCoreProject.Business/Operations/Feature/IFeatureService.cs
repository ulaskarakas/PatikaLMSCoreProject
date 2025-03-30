using PatikaLMSCoreProject.Business.Operations.Feature.Dtos;
using PatikaLMSCoreProject.Business.Types;

namespace PatikaLMSCoreProject.Business.Operations.Feature
{
    public interface IFeatureService
    {
        Task<ServiceMessage> AddFeature(AddFeatureDto feature);
    }
}