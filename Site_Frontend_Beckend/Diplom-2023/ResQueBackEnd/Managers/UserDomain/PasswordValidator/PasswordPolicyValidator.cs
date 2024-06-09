using Infrastructure.FunctionalStyleResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.UserDomain.PasswordValidator
{
    public sealed class PasswordPolicyValidator : IPasswordPolicyValidator
    {
        private const byte MinPasswordLength = 8;
        private const byte MinCountOfFulfilledRequirements = 3;

        public Result Validate(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Fail("Password cannot be empty", "PASSWORD_CANNOT_BE_EMPTY");
            }

            if (password.Length < MinPasswordLength)
            {
                return Result.Fail($"Password must be at least {MinPasswordLength} characters long", "PASSWORD_IS_TOO_SHORT");
            }

            byte countOfFulfilledRequirements = 0;

            if (ContainsUpperCaseLetter(password))
                countOfFulfilledRequirements++;

            if (ContainsLowerCaseLetter(password))
                countOfFulfilledRequirements++;

            if (ContainsDigit(password))
                countOfFulfilledRequirements++;

            if (ContainsSpecialCharacter(password))
                countOfFulfilledRequirements++;

            return countOfFulfilledRequirements < MinCountOfFulfilledRequirements
                ? Result.Fail("Password must fulfill at least 3 of the requirements: uppercase letters, lowercase letters, digits or special characters",
                    "PASSWORDS_MUST_FULFILL_AT_LEAST_3_OF_REQUIREMENTS")
                : Result.Ok();
        }

        private bool ContainsUpperCaseLetter(string password) => password.Any(char.IsUpper);

        private bool ContainsLowerCaseLetter(string password) => password.Any(char.IsLower);

        private bool ContainsDigit(string password) => password.Any(char.IsDigit);

        private bool ContainsSpecialCharacter(string password)
        {
            var regex = new Regex("[^A-Za-z0-9]");
            return regex.IsMatch(password);
        }
    }
}
