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
    //Service for communication between the Database and the Controller which wants to
    public class UserService
    {
        private ATPEntities _dbContext; //Database context(database connection)
        public UserService()
        {
            _dbContext = new ATPEntities();//Connect to database
        }
        //Insert a user to the database
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
                });//Map DTO to Database Object

            _dbContext.SaveChanges();//ALWAYS SAVE AFTER BEING DONE. Make only one transfer to the database for safety reasons
        }
    }
}
