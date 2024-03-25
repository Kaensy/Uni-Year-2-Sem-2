using System.ComponentModel;
using System.IO;
using JetBrains.Annotations;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPP_CSharp.Domain;
using MPP_CSharp.Repository;

namespace MPP_CSharp.Tests.Repository
{
    [TestClass]
    [TestSubject(typeof(RepoUser))]
    public class RepoUserTest
    {

        [TestMethod]
        [DisplayName("Test RepoUser")]
        public void Test()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            
            var repoUser = new RepoUser();
            var user1 = new User( 3,"test", "test");
            Assert.AreEqual(repoUser.FindByUsernamePassword("test", "test"),user1);
            
            DBUtil.Instance.CloseConnection();
        }
    }
}