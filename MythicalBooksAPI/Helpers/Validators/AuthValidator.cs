using Microsoft.AspNetCore.Mvc;
using MythicalBooksAPI.Dtos.Auth;

namespace MythicalBooksAPI.Helpers.Validators
{
    public static class AuthValidator
    {
        public static ValidationResult ValidateRegister(RegisterUserDto registerUserDto)
        {
            // Check Empty Fields
            List<string> missingFields = new List<string>();

            if (!ValidationHelper.IsNotEmpty(registerUserDto.Name)) missingFields.Add("Name");
            if (!ValidationHelper.IsNotEmpty(registerUserDto.Surname)) missingFields.Add("Surname");
            if (!ValidationHelper.IsNotEmpty(registerUserDto.Email)) missingFields.Add("Email");
            if (!ValidationHelper.IsNotEmpty(registerUserDto.Password)) missingFields.Add("Password");
            if (!ValidationHelper.IsNotEmpty(registerUserDto.Country)) missingFields.Add("Country");

            if (missingFields.Count > 0)
            {
                return ValidationResult.Fail(
                    new
                    {
                        message = "Missing required fields",
                        fields = missingFields
                    });
            }

            // Check Field Length Too Long
            List<string> fieldsTooLong = new List<string>();

            if (!ValidationHelper.HasMaxLength(registerUserDto.Email, 254))
                fieldsTooLong.Add("Email");

            if (!ValidationHelper.HasMaxLength(registerUserDto.Name, 60))
                fieldsTooLong.Add("Name");

            if (!ValidationHelper.HasMaxLength(registerUserDto.Surname, 60))
                fieldsTooLong.Add("Surname");

            if (!ValidationHelper.HasMaxLength(registerUserDto.Country, 60))
                fieldsTooLong.Add("Country");

            if (!ValidationHelper.HasMaxLength(registerUserDto.Password, 128))
                fieldsTooLong.Add("Password");


            if (fieldsTooLong.Count > 0)
            {
                return ValidationResult.Fail(new
                {
                    message = "Some fields have too long lengths",
                    fields = fieldsTooLong
                });
            }

            // Check Field Length Too Short
            List<string> fieldsTooShort = new List<string>();

            if (!ValidationHelper.HasMinLength(registerUserDto.Email, 5))
                fieldsTooShort.Add("Email");

            if (!ValidationHelper.HasMinLength(registerUserDto.Name, 1))
                fieldsTooShort.Add("Name");

            if (!ValidationHelper.HasMinLength(registerUserDto.Surname, 1))
                fieldsTooShort.Add("Surname");

            if (!ValidationHelper.HasMinLength(registerUserDto.Country, 2))
                fieldsTooShort.Add("Country");

            if (!ValidationHelper.HasMinLength(registerUserDto.Password, 8))
                fieldsTooShort.Add("Password");

            if (fieldsTooShort.Count > 0)
            {
                return ValidationResult.Fail(new
                {
                    message = "Some fields have too short lengths",
                    fields = fieldsTooShort
                });
            }

            // Check Format
            if (!ValidationHelper.IsValidEmail(registerUserDto.Email))
            {
                return ValidationResult.Fail("Invalid email format");
            }

            return ValidationResult.Success();
        }

    }
}
