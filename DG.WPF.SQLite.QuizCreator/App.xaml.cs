using DG.WPF.SQLite.QuizCreator.HelperClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DG.WPF.SQLite.QuizCreator
{
    /// <summary>
    /// Interaction logic for App
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AutoMapperConfiguration.Configure();

            base.OnStartup(e);
        }
    }
}
