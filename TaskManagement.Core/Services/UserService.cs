﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Entities.Requests;
using TaskManagement.Core.Helpers;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(Register register);
        Task<UserDTO> AuthenticateUserAsync(Login login);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IJWTAuthenticationManager _jWTAuthenticationManager;
        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, IJWTAuthenticationManager jWTAuthenticationManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _jWTAuthenticationManager = jWTAuthenticationManager;
        }
        public async Task<UserDTO> CreateUserAsync(Register register)
        {
            _logger.LogInformation("Attempting user creation user {email}", register.Email);

            if (string.IsNullOrWhiteSpace(register.Password)) throw new Exception("Password is required");

            var user = _mapper.Map<User>(register);
            var userExist = await _userRepository.GetUserByEmail(register.Email);

            if (userExist != null) throw new Exception("User already exists");

            var salt = PasswordSalt.Create();
            var hash = PasswordHash.Create(register.Password, salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.CreatedDate = DateTime.Now;

            var result = await _userRepository.AddUser(user);
            _logger.LogInformation($"user created {user.Email}");

            var mappedUser = _mapper.Map<UserDTO>(result);


            return mappedUser;
        }
        public async Task<UserDTO> AuthenticateUserAsync(Login login)
        {
            _logger.LogInformation("Attempting login for user {email}", login.Email);
            var user = await _userRepository.GetUserByEmail(login.Email);

            _logger.LogWarning("No user found with email {email}", login.Email);
            if (user == null) throw new Exception("User not found");

            var mappedUser = _mapper.Map<UserDTO>(user);
            mappedUser.Token = await _jWTAuthenticationManager.GetTokenAsync(login.Email);

            return mappedUser;
        }
    }
}