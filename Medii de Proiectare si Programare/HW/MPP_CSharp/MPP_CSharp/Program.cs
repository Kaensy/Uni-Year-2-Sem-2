using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Config;
using MPP_CSharp.Repository;

namespace MPP_CSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.ApplicationExit += (sender, args) =>
            {
               DBUtil.Instance.CloseConnection();
            };
            
            Application.Run(new Form1());
        }
    }
}