using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManageProject;

namespace ТестированиеMP
{
    [TestClass]
    public class Test_Project
    {

        private Project projectt;

        [TestInitialize]
        public void Setup()
        {
            projectt = new Project("Тестовый проект", "Описание проекта", DateTime.Now, DateTime.Now.AddMonths(1));
        }

        [TestMethod]
        public void Constructor_Initializes()
        {
            Assert.AreEqual("Тестовый проект", projectt.Name);
            Assert.AreEqual("Описание проекта", projectt.Description);
            Assert.IsTrue(projectt.StartDate <= projectt.EndDate);
            Assert.AreEqual(0, projectt.Progress);
        }

        [TestMethod]
        public void UpdateProgressSuccessfully()
        {
            projectt.UpdateProgress(50);
            Assert.AreEqual(50, projectt.Progress);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Прогресс должен быть между 0 и 100")]
        public void UpdateProgress_NegativeValue()
        {
            projectt.UpdateProgress(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Прогресс должен быть между 0 и 100")]
        public void UpdateProgress_MoreThan10()
        {
            projectt.UpdateProgress(101);
        }
    }
}