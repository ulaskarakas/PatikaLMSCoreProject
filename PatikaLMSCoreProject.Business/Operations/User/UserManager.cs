using PatikaLMSCoreProject.Business.Operations.User.Dtos;
using PatikaLMSCoreProject.Business.Types;
using PatikaLMSCoreProject.Data.Entities;
using PatikaLMSCoreProject.Data.Enums;
using PatikaLMSCoreProject.Data.Repositories;
using PatikaLMSCoreProject.Data.UnitOfWork;

namespace PatikaLMSCoreProject.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower()).Any();

            if (hasMail)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Email address already exists"
                };
            }

            var UserEntity = new UserEntity
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                UserType = UserType.Student
            };

            _userRepository.Add(UserEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred during user registration");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }
    }
}