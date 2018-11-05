using Cinema.Presentation.Wpf.View;
using System.Windows;
using Ninject;
using Ninject.Extensions.Conventions;
using Films.Domain;

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
                    .From("Films.Data", "Films.Data.EntityFramework", "Films.Domain")
                    .SelectAllClasses()
                    .BindAllInterfaces()
                    .ConfigureFor<FilmManager>(config => config.InSingletonScope())
            );

            container.Bind(
               configurator => configurator
                   .From("Cinema.Presentation.Wpf")
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