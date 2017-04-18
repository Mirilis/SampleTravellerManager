using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleManagerLibrary
{
    public partial class User
    {
        public static User GetCurrentUser()
        {
            var user = new User();
            using (var sql = new SampleTravellersEntities())
            {
                user = sql.Users.Where(x => x.Email == "ahoover@grede.com").First();
                
            }
            return user;
        }

    }
}
