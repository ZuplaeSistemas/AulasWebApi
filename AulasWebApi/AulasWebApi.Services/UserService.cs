using AulasWebApi.Infra.Repositories;
using AulasWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AulasWebApi.Services
{
    public class UserService : Service<User>
    {
        private readonly PasswordHasher _passwordHasher;
        public UserService(UserRepository repository) : base(repository)
        {
            _passwordHasher = new PasswordHasher();
        }
        public override int Create(User model)
        {
            model.Password = _passwordHasher.HashPassword(model.Password);
            return base.Create(model);
        }       
    }
}
