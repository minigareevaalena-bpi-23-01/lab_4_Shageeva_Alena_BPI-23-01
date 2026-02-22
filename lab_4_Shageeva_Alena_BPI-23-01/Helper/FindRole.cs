using lab_4_Shageeva_Alena_BPI_23_01.Model;
//using lab_4_Shageeva_Alena_BPI_23_01.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace lab_4_Shageeva_Alena_BPI_23_01.Helper
{
    public class FindRole
    {
        int id;
        public FindRole(int id)
        {
            this.id = id;
        }
        public bool RolePredicate(Role role)
        {
            return role.Id == id;
        }
    }


}
