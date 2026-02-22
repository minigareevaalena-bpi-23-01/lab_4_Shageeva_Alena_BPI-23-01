using lab_4_Shageeva_Alena_BPI_23_01.Model;
using lab_4_Shageeva_Alena_BPI_23_01.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace lab_4_Shageeva_Alena_BPI_23_01.View
{
    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee(ObservableCollection<Role> roles)
        {
            InitializeComponent();
            CbRole.ItemsSource = roles;

            if (DataContext is PersonDPO person)
            {
                CbRole.SelectedValue = person.RoleId;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PersonDPO person)
            {
                if (CbRole.SelectedItem is Role selectedRole)
                {
                    person.RoleId = selectedRole.Id;
                    person.RoleName = selectedRole.NameRole;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите должность.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DialogResult = true;
            }
        }

        private void tbBirthday_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tbBirthday.Visibility == Visibility.Hidden)
            {
                ClBirthday.Visibility = Visibility.Visible;
            }
            else
            {
                ClBirthday.Visibility = Visibility.Hidden;
            }
        }
    }
}