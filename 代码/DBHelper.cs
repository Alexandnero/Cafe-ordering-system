using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace index
{
    class DBHelper
    {
        public static string constr = "Integrated Security=SSPI;Persist Security Info = false;Data Source =.;Initial Catalog =CoffeeHouse";
        public static SqlConnection con = new SqlConnection(constr);

  
    }
}
