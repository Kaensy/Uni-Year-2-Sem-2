using System.ComponentModel;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPP_CSharp.Domain;
using MPP_CSharp.Repository;

namespace MPP_CSharp.Tests.Repository
{
    [TestClass]
    [TestSubject(typeof(RepoChild))]
    public class RepoChildTest
    {

        [TestMethod]
        [DisplayName("Test RepoChild")]
        public void Test()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            
            var id = 11;
            var repoChild = new RepoChild();
            var child1 = new Child(id,"name", 10);
            Assert.AreEqual(repoChild.Save(child1),child1);
            Assert.AreEqual(repoChild.Search(id),child1);
            Assert.AreEqual(repoChild.GetAll().Count(),1);
            
            DBUtil.Instance.CloseConnection();
        }
    }
}