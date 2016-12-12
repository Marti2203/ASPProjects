using CommonFiles.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureInterfaces
{
    public static class Services
    {
        public static IService<UserDTO> UserService { get { return ServiceFactory.CreateService<UserDTO, USER_NEW_NEW>(); } }
        public static IService<BoxDTO> BoxService { get { return ServiceFactory.CreateService<BoxDTO, BOX>(); } }
        public static IService<CVDTO> CVService { get { return ServiceFactory.CreateService<CVDTO, CV>(); } }
    }
}
