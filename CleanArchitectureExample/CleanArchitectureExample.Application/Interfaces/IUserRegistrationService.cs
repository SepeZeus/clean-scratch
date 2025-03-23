using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Interfaces
{
    public interface IUserRegistrationService
    {
        void RegisterUser(string name, string email);


        Task<bool> EmailExistsAsync(string email);
        Task<bool> RegisterUserAsync(string name, string email);
    }

}
