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
            _dbContext.USER_NEW_NEW.Add(Convert(user));            //Map DTO to Database Object


            _dbContext.SaveChanges();//ALWAYS SAVE AFTER BEING DONE. Make only one transfer to the database for safety reasons
        }

        public UserDTO GetUser(string username)
        {
            USER_NEW_NEW userEntity = _dbContext.USER_NEW_NEW.FirstOrDefault(user => user.USERNAME == username);
            if (userEntity == null)
                return null;
            return Convert(userEntity);
        }
        public bool HasUser(string username) => _dbContext
            .USER_NEW_NEW
            .FirstOrDefault(entity => entity.USERNAME == username) != null;

        public IEnumerable<UserDTO> GetUsers()
        {
            throw new NotImplementedException();
        }

        private UserDTO Convert(USER_NEW_NEW userEntity) => new UserDTO
        {
            Username = userEntity.USERNAME,
            Password = userEntity.PASSWORD,
            About = userEntity.ABOUT,
            Email = userEntity.EMAIL,
            Gender = userEntity.GENDER,
            SecretAnswer = userEntity.SECRET_A,
            SecretQuestion = userEntity.SECRET_Q
        };
        private USER_NEW_NEW Convert(UserDTO dto) => new USER_NEW_NEW
        {
            USERNAME = dto.Username,
            ABOUT = dto.About,
            EMAIL = dto.Email,
            GENDER = dto.Gender,
            PASSWORD = dto.Password,
            SECRET_A = dto.SecretAnswer,
            SECRET_Q = dto.SecretQuestion,
            ID = dto.ID
        };



    }
}
