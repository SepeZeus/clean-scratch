using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Mappers
{
    public class UserMapper
    {
        public UserDto EntityToDto(User user)
        {
            return new UserDto {Id = user.Id, Name = user.Name, Email = user.Email };
        }
    }
}
