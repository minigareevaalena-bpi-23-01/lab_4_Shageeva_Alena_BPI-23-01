using lab_4_Shageeva_Alena_BPI_23_01.Helper;
using lab_4_Shageeva_Alena_BPI_23_01.View;
//using lab_4_Shageeva_Alena_BPI_23_01.Helper;
//using lab_4_Shageeva_Alena_BPI_23_01.View;
using System.Windows.Input;

namespace lab_4_Shageeva_Alena_BPI_23_01.ViewModel
{
    public class MainViewModel
    {
        private RoleViewModel roleViewModel;

        public ICommand OpenEmployeesCommand { get; }
        public ICommand OpenRolesCommand { get; }

        public MainViewModel()
        {

            roleViewModel = new RoleViewModel();

            OpenEmployeesCommand = new RelayCommand(_ => OpenEmployees());
            OpenRolesCommand = new RelayCommand(_ => OpenRoles());
        }

        private void OpenEmployees()
        {
            var window = new WindowEmployee();

            var personViewModel = new PersonViewModel(roleViewModel);
            window.DataContext = personViewModel;
            window.ShowDialog();
        }

        private void OpenRoles()
        {
            var window = new WindowRole();
            window.DataContext = roleViewModel;
            window.ShowDialog();
        }
    }
}