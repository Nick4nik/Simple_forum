using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;

namespace Test_Task_for_GeeksForLess.Validators
{
    public class UserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (!user.Email.Contains("@"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Your email must contain `@`"
                });
            }

            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
