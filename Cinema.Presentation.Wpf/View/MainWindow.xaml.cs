using Cinema.Presentation.Wpf.ViewModel;
using System.Windows;

namespace Cinema.Presentation.Wpf.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}