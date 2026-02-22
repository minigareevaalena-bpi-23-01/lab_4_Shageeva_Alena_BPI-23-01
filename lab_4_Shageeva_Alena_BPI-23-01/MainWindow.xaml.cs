using System.Windows;

namespace lab_4_Shageeva_Alena_BPI_23_01
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.MainViewModel();
        }
    }
}