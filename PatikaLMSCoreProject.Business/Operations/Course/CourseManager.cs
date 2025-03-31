using Microsoft.EntityFrameworkCore;
using PatikaLMSCoreProject.Business.Operations.Course.Dtos;
using PatikaLMSCoreProject.Business.Types;
using PatikaLMSCoreProject.Data.Entities;
using PatikaLMSCoreProject.Data.Repositories;
using PatikaLMSCoreProject.Data.UnitOfWork;

namespace PatikaLMSCoreProject.Business.Operations.Course
{
    public class CourseManager : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CourseEntity> _courseRepository;
        private readonly IRepository<CourseFeatureEntity> _courseFeatureRepository;

        public CourseManager(IUnitOfWork unitOfWork, IRepository<CourseEntity> courseRepository, IRepository<CourseFeatureEntity> courseFeatureRepository)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _courseFeatureRepository = courseFeatureRepository;
        }

        public async Task<ServiceMessage> AddCourse(AddCourseDto course)
        {
            var hasCourse = _courseRepository.GetAll(x => x.Name.ToLower() == course.Name.ToLower()).Any();

            if (hasCourse)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "This course already exists in the system"
                };
            }

            await _unitOfWork.BeginTransaction();

            var courseEntity = new CourseEntity
            {
                Name = course.Name,
                Stars = course.Stars,
                EducationType = course.EducationType
            };

            _courseRepository.Add(courseEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("There was a problem registering for the course");
            }

            foreach (var featureId in course.FeatureIds)
            {
                var coursFeature = new CourseFeatureEntity
                {
                    CourseId = courseEntity.Id,
                    FeatureId = featureId
                };

                _courseFeatureRepository.Add(coursFeature);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw new Exception("An error occurred while adding course features, so all actions were rolled back.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        public async Task<CourseDto> GetCourse(int id)
        {
            var course = await _courseRepository.GetAll(x => x.Id == id)
                                        .Select(x => new CourseDto
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Stars = x.Stars,
                                            EducationType = x.EducationType,
                                            Features = x.CourseFeatures.Select(f => new CourseFeatureDto
                                            {
                                                Id = f.Id,
                                                Title = f.Feature.Title
                                            }).ToList()
                                        }).FirstOrDefaultAsync();

            return course;
        }

        public async Task<List<CourseDto>> GetCourses()
        {
            var courses = await _courseRepository.GetAll()
                                        .Select(x => new CourseDto
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Stars = x.Stars,
                                            EducationType = x.EducationType,
                                            Features = x.CourseFeatures.Select(f => new CourseFeatureDto
                                            {
                                                Id = f.Id,
                                                Title = f.Feature.Title
                                            }).ToList()
                                        }).ToListAsync();
            return courses;
        }
    }
}