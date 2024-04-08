using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Config;
using MPP_CSharp.Repository;
using MPP_CSharp.Service;

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
            
            IRepositoryUser repoUser = new RepoUser();
            IRepositoryTrack repoTrack = new RepoTrack();
            IRepositoryChild repoChild = new RepoChild();
            Service.Service service = new Service.Service(new ServiceUser(repoUser), new ServiceTrack(repoTrack),
                new ServiceChild(repoChild));
            
            Application.Run(new Form1(service));
        }
    }
}