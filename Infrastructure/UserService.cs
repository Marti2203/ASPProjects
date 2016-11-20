using DataAccess;
using CommonFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFiles.DTO;

namespace Infrastructure
{
    public class UserService
    {
        private ATPEntities _dbContext;
        public UserService()
        {
            _dbContext = new ATPEntities();
        }

        public void InsertUser(UserDTO user)
        {
            _dbContext.USER_NEW_NEW.Add(
                new USER_NEW_NEW
                {
                    USERNAME = user.Username,
                    ABOUT = user.About,
                    EMAIL = user.Email,
                    GENDER = user.Gender,
                    PASSWORD = user.Password,
                    SECRET_A = user.SecretAnswer,
                    SECRET_Q = user.SecretQuestion, ID = user.ID
                });

            _dbContext.SaveChanges();
        }
    }
}
