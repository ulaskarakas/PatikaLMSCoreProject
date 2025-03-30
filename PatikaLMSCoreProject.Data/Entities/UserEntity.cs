using PatikaLMSCoreProject.Data.Enums;

namespace PatikaLMSCoreProject.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public UserType UserType { get; set; }
        // Relational Properties
        public ICollection<EnrollmentEntity> Enrollments { get; set; }
    }
}