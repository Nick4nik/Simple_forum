using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;

namespace Test_Task_for_GeeksForLess.Validators
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        public int RequiredLength { get; set; }

        public PasswordValidator(int length)
        {
            RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (string.IsNullOrEmpty(password) || password.Length < RequiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"The minimum password length is {RequiredLength}"
                });
            }

            string pattern = "^[A-Za-z0-9]*$";

            if (!Regex.IsMatch(password, pattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Password must consist of numbers and symbols `A-Z`"
                });
            }

            return Task.FromResult(errors.Count == 0 ?
                    IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
