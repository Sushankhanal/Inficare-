using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InficareSushan.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                username="abc",password="abc123",role="admin"
            },

             new UserModel()
            {
                username="xyz",password="xyz123",role="employee"
            },
        };
    }
}
