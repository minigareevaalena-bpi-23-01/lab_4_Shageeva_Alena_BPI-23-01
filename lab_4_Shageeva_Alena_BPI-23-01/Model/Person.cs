using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4_Shageeva_Alena_BPI_23_01.Model

{
    public class Person
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; } // тут теперь string

        public Person() { }

        public Person(int id, int roleId, string firstName, string lastName, string birthday)
        {
            Id = id;
            RoleId = roleId;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        public Person CopyFromPersonDPO(PersonDPO dpo)
        {
            return new Person
            {
                Id = dpo.Id,
                RoleId = dpo.RoleId,
                FirstName = dpo.FirstName,
                LastName = dpo.LastName,
                Birthday = dpo.Birthday
            };
        }
    }
}