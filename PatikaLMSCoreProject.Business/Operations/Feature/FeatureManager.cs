using PatikaLMSCoreProject.Business.Operations.Feature.Dtos;
using PatikaLMSCoreProject.Business.Types;
using PatikaLMSCoreProject.Data.Entities;
using PatikaLMSCoreProject.Data.Repositories;
using PatikaLMSCoreProject.Data.UnitOfWork;

namespace PatikaLMSCoreProject.Business.Operations.Feature
{
    public class FeatureManager : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FeatureEntity> _featureRepository;
        public FeatureManager(IUnitOfWork unitOfWork, IRepository<FeatureEntity> featureRepository)
        {
            _unitOfWork = unitOfWork;
            _featureRepository = featureRepository;
        }

        public async Task<ServiceMessage> AddFeature(AddFeatureDto feature)
        {
            var hasFeature = _featureRepository.GetAll(x => x.Title.ToLower() == feature.Title.ToLower()).Any();

            if(hasFeature)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The feature already exists"
                };
            }

            var featureEntity = new FeatureEntity
            {
                Title = feature.Title,
            };

            _featureRepository.Add(featureEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while registering the feature");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }
    }
}