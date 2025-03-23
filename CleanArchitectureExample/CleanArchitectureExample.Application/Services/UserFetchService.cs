using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Application.Mappers;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Services
{
    public class UserFetchService : IUserFetchService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserMapper _userMapper;

        public UserFetchService(IUserRepository userRepository, UserMapper userMapper)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public async Task<UserDto> FetchUserByEmailAsync(string Email)
        {
            User user = await _userRepository.FetchUserByEmailAsync(Email);
            if (user == null)
            {
                return null;
            }
            UserDto userDto = _userMapper.EntityToDto(user);
            return userDto;
        }
    }
}
