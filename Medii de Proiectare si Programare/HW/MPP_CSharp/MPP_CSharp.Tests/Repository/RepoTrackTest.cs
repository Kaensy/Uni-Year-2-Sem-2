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
    [TestSubject(typeof(RepoTrack))]
    public class RepoTrackTest
    {

        [TestMethod]
        [DisplayName("Test RepoTrack")]
        public void Test()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            var id = 14;
            var repoTrack = new RepoTrack();
            var track1 = new Track(id,"name", 10,12, 100);
            Assert.AreEqual(repoTrack.Save(track1),track1);
            Assert.AreEqual(repoTrack.Search(id),track1);
            Assert.AreEqual(repoTrack.GetAll().Count(),7);
            
            DBUtil.Instance.CloseConnection();
        }
    }
}