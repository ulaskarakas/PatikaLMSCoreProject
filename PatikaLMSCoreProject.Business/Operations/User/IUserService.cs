using PatikaLMSCoreProject.Business.Operations.User.Dtos;
using PatikaLMSCoreProject.Business.Types;

namespace PatikaLMSCoreProject.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user); // Should be asynchronous because UnitOfWork will be used
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}