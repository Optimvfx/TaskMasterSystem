using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuthenticationSystemV2.Context;
using UserAuthenticationSystemV2.Generators.PasswordEncryptor;
using UserAuthenticationSystemV2.Models;

namespace UserAuthenticationSystemV2.Systems
{
    public class AuthenticationSystem
    {
         private readonly ApplicationDbContext _dbContext;

        private readonly IPasswordEncryptor _passwordEncryptor;
        
        private readonly UniqueGuidGenerationSystem _uniqueGuidGenerationSystem;
        
        public AuthenticationSystem(ApplicationDbContext dbContext, IPasswordEncryptor passwordEncryptor)
        {
            _dbContext = dbContext;
            _passwordEncryptor = passwordEncryptor;
            
            _uniqueGuidGenerationSystem = new UniqueGuidGenerationSystem();
        }

        public bool TryRegister(RegisterModel model, out User newUser)
        {
            newUser = default;
            
            User user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user != null)
                return false;

            var userPassword = _passwordEncryptor.Encrypt(model.Password);

            user = new User(_uniqueGuidGenerationSystem.GenerateUniqueId(_dbContext.Users.Select(user => user.Id)), model.Email, userPassword);

            _dbContext.Users.Add(user);

            _dbContext.SaveChangesAsync();

            newUser = user;
            
            return true;
        }

        public bool TryLogin(LoginViewModel viewModel, out User user)
        {
            user = default;
            
            var userPassword = _passwordEncryptor.Encrypt(viewModel.Password);

            if (_dbContext.Users.Any(u => u.Email == viewModel.Email && u.Password == userPassword) == false)
                return false;

            user = _dbContext.Users.First(u => u.Email == viewModel.Email && u.Password == userPassword);
            
            return true;
        }

        public bool TryDeleteAccount(LoginViewModel viewModel)
        {
            if (TryLogin(viewModel, out User user) == false)
                return false;
            
            _dbContext.Remove(user);

            return true;
        }
    } 
}