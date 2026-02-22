using lab_4_Shageeva_Alena_BPI_23_01.ViewModel;
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
    public partial class WindowRole : Window
    {
        public WindowRole()
        {
            InitializeComponent();
            DataContext = new RoleViewModel();
        }
    }
}
