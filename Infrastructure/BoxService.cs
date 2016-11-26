using CommonFiles.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BoxService
    {
        private ATPEntities _dbContext; //Database context(database connection)
        private int starterID;
        public BoxService()
        {
            _dbContext = new ATPEntities();//Connect to database
            starterID = new Random().Next();
        }
        public void InsertBox(BoxDTO box)
        {
            start:
            try
            {
                _dbContext.User.Add(GenerateUser(box));
            }
            catch (System.Data.DataException)
            {
                starterID = new Random().Next();
                goto start;
            }
                _dbContext.SaveChanges();
        }
        public IEnumerable<BoxDTO> GetBoxes()
        {
            foreach (User user in _dbContext.User)
            {
                yield return GenerateDTO(user);
            }
        }
        /*
         * Name : Colour
         * Password: Material
         * About: Weight,Length,Height,Width
         * Email: GUID
         */
        private User GenerateUser(BoxDTO box)
        {
            User boxObject = new User
            {
                USERNAME = box.Colour,
                PASSWORD = box.Material,
                EMAIL = box.ID,
            };
            #region Add Dimensions To User Entity
            StringBuilder builder = new StringBuilder();

            builder.Append(box.Weight);
            builder.Append(',');
            builder.Append(box.Length);
            builder.Append(',');
            builder.Append(box.Height);
            builder.Append(',');
            builder.Append(box.Width);

            boxObject.ABOUT = builder.ToString();
            #endregion

            boxObject.ID = starterID++;
            boxObject.GENDER = "b";
            boxObject.SECRET_A = string.Empty;
            boxObject.SECRET_Q = string.Empty;

            return boxObject;
        }
        private BoxDTO GenerateDTO(User user)
        {
            BoxDTO box = new BoxDTO()
            {
                Colour = user.USERNAME,
                Material = user.PASSWORD,
                ID = user.EMAIL
            };

            string[] elements = user.ABOUT.Split(',');

            #region Get Dimensions From User Entity
            box.Weight = int.Parse(elements[0]);
            box.Length = int.Parse(elements[1]);
            box.Height = int.Parse(elements[2]);
            box.Width = int.Parse(elements[3]);
            #endregion

            return box;
        }

        public void RemoveBox(string id)
        {
            User user= _dbContext.User.Where(entity => entity.EMAIL == id).FirstOrDefault();
            if (user != null)
                _dbContext.User.Remove(user);
            _dbContext.SaveChanges();
        }

        public BoxDTO GetBox(string id)
        {
            User user = _dbContext.User.Where(entity => entity.EMAIL == id).FirstOrDefault();
            if (user != null)
                return GenerateDTO(user);
            return null;
        }
    }
}
