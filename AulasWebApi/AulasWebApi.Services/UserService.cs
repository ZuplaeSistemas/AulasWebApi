using AulasWebApi.Infra.Repositories;
using AulasWebApi.Models;
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

        public override void Update(User model)
        {
            User existingUser = ReadById(model.Id);
            if(existingUser != null)
            {
                model.Email = existingUser.Email; // Evita que o email seja alterado
                model.Person_Id = existingUser.Person_Id; // Evita que o Person_Id seja alterado
                model.Password = _passwordHasher.HashPassword(model.Password);
                base.Update(model);
            }
        }
    }
}
