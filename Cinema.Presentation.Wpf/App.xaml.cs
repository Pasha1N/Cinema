using Cinema.Presentation.Wpf.View;
using Cinema.Presentation.Wpf.View.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cinema.Presentation.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new MainViewModel();
            var mainWindow = new MainWindow(viewModel);
            mainWindow.Show();
        }
    }
}