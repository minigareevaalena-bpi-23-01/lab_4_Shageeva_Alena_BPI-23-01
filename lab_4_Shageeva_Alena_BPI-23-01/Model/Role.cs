using lab_4_Shageeva_Alena_BPI_23_01.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab_4_Shageeva_Alena_BPI_23_01.Model
{
    public class Role : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string nameRole;
        public string NameRole
        {
            get { return nameRole; }
            set
            {
                nameRole = value; OnPropertyChanged("NameRole");
            }
        }

        public Role() { }

        public Role(int id, string nameRole)
        {
            this.Id = id;
            this.NameRole = nameRole;
        }

        public Role ShallowCopy() => (Role)this.MemberwiseClone();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
