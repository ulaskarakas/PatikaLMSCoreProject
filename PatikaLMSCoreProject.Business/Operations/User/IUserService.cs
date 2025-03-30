using PatikaLMSCoreProject.Business.Operations.User.Dtos;
using PatikaLMSCoreProject.Business.Types;

namespace PatikaLMSCoreProject.Business.Operations.User
{
    public interface IUserService
    {
        // Should be asynchronous because UnitOfWork will be used
        Task<ServiceMessage> AddUser(AddUserDto user);
    }
}