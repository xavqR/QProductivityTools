using Prism.Ioc;
using Prism.Unity;
using System;
using System.Threading;
using System.Windows;

namespace QProductivityTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication, IDisposable
    {
        #region Fields

        private Mutex applicationMutex = null;
        private const string ApplicationName = @"Global\QProductivityTools.Shell";

        #endregion

        #region Override Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNewApplicationMutex;

            this.applicationMutex = new Mutex(true, App.ApplicationName, out isNewApplicationMutex);
            if (isNewApplicationMutex)
            {
                this.Exit += this.ApplicationOnExit;
                base.OnStartup(e);
            }
            else
            {
                App.Current.Shutdown();
            }        
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // register other needed services here
        }

        protected override Window CreateShell()
        {       
            MainWindow mainWindow = this.Container.Resolve<MainWindow>();
            return mainWindow;
        }

        #endregion

        #region Private Methods

        private void ApplicationOnExit(object sender, ExitEventArgs e)
        {
            e.ApplicationExitCode = 0;
            this.Dispose();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Gets a value indicating whether this <see cref="App"/> is disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        protected bool Disposed { get; private set; }

        /// <summary>
        /// Internals the dispose.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    this.Exit -= this.ApplicationOnExit;
                    this.applicationMutex.ReleaseMutex();
                    this.applicationMutex.Dispose();
                }
                // Dispose here only unmanaged objects 
                // NEVER call here anything managed because maybe 
                // they have been finalized already and you will get an exception
            }

            this.Disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the App class
        /// </summary>
        ~App()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
