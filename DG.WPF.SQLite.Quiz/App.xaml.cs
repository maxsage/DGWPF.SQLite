using DG.WPF.SQLite.Quiz.HelperClasses;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DG.WPF
{
    /// <summary>
    /// Interaction logic for App
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            AutoMapperConfiguration.Configure();

            base.OnStartup(e);
        }
    }
}
