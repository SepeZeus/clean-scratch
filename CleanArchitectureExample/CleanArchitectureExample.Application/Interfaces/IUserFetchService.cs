using CleanArchitectureExample.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Interfaces
{
    public interface IUserFetchService
    {
        Task<UserDto> FetchUserByEmailAsync(string email);
    }
}
