using lab_4_Shageeva_Alena_BPI_23_01.Model;
using lab_4_Shageeva_Alena_BPI_23_01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab_4_Shageeva_Alena_BPI_23_01.View
{
    public partial class WindowNewRole : Window
    {
        public WindowNewRole()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Role role)
            {
                if (string.IsNullOrWhiteSpace(role.NameRole))
                {
                    MessageBox.Show("Введите наименование должности.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DialogResult = true;
            }
        }
    }
}
