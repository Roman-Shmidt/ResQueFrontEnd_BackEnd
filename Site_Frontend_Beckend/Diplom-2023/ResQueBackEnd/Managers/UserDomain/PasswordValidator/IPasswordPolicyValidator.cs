using Infrastructure.FunctionalStyleResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.UserDomain.PasswordValidator
{
    public interface IPasswordPolicyValidator
    {
        Result Validate(string password);
    }
}
