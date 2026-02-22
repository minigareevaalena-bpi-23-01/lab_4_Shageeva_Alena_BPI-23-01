using lab_4_Shageeva_Alena_BPI_23_01.ViewModel;
//using lab_4_Shageeva_Alena_BPI_23_01.Model;
//using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab_4_Shageeva_Alena_BPI_23_01.Model
{
    public class PersonDPO : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int roleId;
        public int RoleId
        {
            get => roleId;
            set { roleId = value; OnPropertyChanged(); }
        }

        private string roleName;
        public string RoleName
        {
            get => roleName;
            set { roleName = value; OnPropertyChanged(); }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(); }
        }

        private string birthday; // тут тоже теперь  string
        public string Birthday
        {
            get => birthday;
            set { birthday = value; OnPropertyChanged(); }
        }

        public PersonDPO() { }

        public PersonDPO(int id, int roleId, string roleName, string firstName, string lastName, string birthday)
        {
            Id = id;
            RoleId = roleId;
            RoleName = roleName;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        public PersonDPO ShallowCopy() => (PersonDPO)this.MemberwiseClone();

        public PersonDPO CopyFromPerson(Person person)
        {
            PersonDPO perDpo = new PersonDPO();
            RoleViewModel vmRole = new RoleViewModel();
            string role = string.Empty;

            foreach (var r in vmRole.ListRole)
            {
                if (r.Id == person.RoleId)
                {
                    role = r.NameRole;
                    break;
                }
            }

            if (role != string.Empty)
            {
                perDpo.Id = person.Id;
                perDpo.RoleId = person.RoleId;
                perDpo.RoleName = role;
                perDpo.FirstName = person.FirstName;
                perDpo.LastName = person.LastName;
                perDpo.Birthday = person.Birthday;
            }

            return perDpo;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}