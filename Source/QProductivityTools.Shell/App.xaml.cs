using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace QProductivityTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {       
            // register other needed services here
        }

        protected override Window CreateShell()
        {
            var w = this.Container.Resolve<MainWindow>();
            return w;
        }
    }
}
