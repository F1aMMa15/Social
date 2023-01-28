using FluentValidation;
using Social.Domain.Validators;

namespace Social.Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {
        private BasicInfo() { }

        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Phone { get; private set; } = null!;
        public DateTime DateOfBirtg { get; private set; }
        public string CurrentCity { get; private set; } = null!;

        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string email,
            string phone, DateTime dateOfBirth, string currentCity)
        {
            var basicInfo = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                DateOfBirtg = dateOfBirth,
                CurrentCity = currentCity,
            };

            var validator = new BasicInfoValidator();
            validator.ValidateAndThrow(basicInfo);

            return basicInfo;
        }
    }
}
