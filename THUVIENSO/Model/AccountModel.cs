using Model.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class AccountModel
    {
        private ThuVienSoDBContext context = new ThuVienSoDBContext();
       
        public int Login(string Accountname, string Password )
        {
            object[] sqlParms =
            {
                new SqlParameter("@Accountname", Accountname),
                new SqlParameter("@Password",Password),
            };
            int res = context.Database.SqlQuery<int>("Sp_Account_Login @Accountname,@Password").SingleOrDefault();
            return res;
        }

       
    }
}
