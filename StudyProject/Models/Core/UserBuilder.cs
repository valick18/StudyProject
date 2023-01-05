using StudyProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models.Core
{
    public class UserBuilder
    {
        RegistrationModel newUser;
        public UserBuilder(RegistrationModel newUser)
        {
            this.newUser = newUser;

        }

        public tbUser Build()
        {
            tbUser user = new tbUser()
            {
                idUser = Guid.NewGuid(),
                Login = newUser.Login,
                FirstName = newUser.FirstName,
                MiddleName = newUser.MiddleName,
                LastName = newUser.LastName,
                Age = newUser.Age,
                DateCreate = DateTime.Now,
                Role = 1 // Додати ролі
            };

            PassCoder coder = new PassCoder(user);
            user.Password = coder.EncryptPassword(newUser.Password);
            return user;
        }

    }
}