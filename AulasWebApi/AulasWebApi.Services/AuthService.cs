using AulasWebApi.Infra.Repositories;
using AulasWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Services
{
    public class AuthService
    {
        private readonly AuthRepository _repository;
        private readonly JwtTokenService _jwtTokenService;
        private readonly PasswordHasher _passwordHasher;
        private readonly PersonService _personService;
        public AuthService(AuthRepository repository, JwtTokenService jwtTokenService, PersonService personService)
        {
            this._repository = repository;
            this._jwtTokenService = jwtTokenService;
            this._passwordHasher = new PasswordHasher();
            this._personService = personService;
        }

        public string Login(string email, string password)
        {            
            User model = this._repository.GetUserByEmail(email);
            if(model != null)
            {
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(model.Password, password);
                if(result == PasswordVerificationResult.Success)
                {       
                    Person person = this._personService.ReadById(model.Person_Id);
                    return _jwtTokenService.GenerateToken(model, person);
                }
            }
            
            throw new Exception("Usuario ou senha invalido");
        }
        public string Logout(int userId)
        {
            return "Logged out";
        }
    }
}
