using Cinema.Presentation.Wpf.View;
using Cinema.Presentation.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using Ninject.Extensions.Conventions;
using MovieDomain;

namespace Cinema.Presentation.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new StandardKernel();
            container.Bind(
                configurator => configurator
                    .From("Film.Data.XmlXDocument", "Movie.Domain")
                    .SelectAllClasses()
                    .BindAllInterfaces()
                    .ConfigureFor<FilmManager>(config => config.InSingletonScope())
            );

            container.Bind(
               configurator => configurator
                   .From("Cocktails.Presentation.Wpf")
                   .IncludingNonPublicTypes()
                   .SelectAllInterfaces()
                   .EndingWith("Factory")
                   .BindToFactory()
           );

            var mainWindow = container.Get<MainWindow>();
            mainWindow.Show();
        }
    }
}