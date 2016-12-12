using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace CommonFiles.DTO
{
    //DataTransferObject- Intermediary object between Model and Database Object
    public class CVDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Experience { get; set; }
        public string Qualities { get; set; }
        public string Education { get; set; }
        public string Address { get; set; }

        //The picture is two parts
        public byte[] PictureBytes { get; set; } //The picutre
        public string PictureName { get; set; } //The name of the picture
    }
}
