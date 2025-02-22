using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenerateVerificationTokenService
    {
        string GenerateVerificationToken(string email);

        string ValidateVerificationToken(string token);
    }
}
