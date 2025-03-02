using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManageProject;

namespace ТестированиеMP
{
    [TestClass]
    public class Test_ProjectManager
    {
        private ProjectManager projectmanager;
        private const string TestFilePath = "test_projects.txt";

        [TestInitialize]
        public void Setup()
        {
            projectmanager = new ProjectManager();

            // Очистить тестовый файл перед каждым тестом
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [TestMethod]
        public void AddProjectToList()
        {
            var project = new Project("Новый проект", "Описание нового проекта", DateTime.Now, DateTime.Now.AddMonths(1));
            projectmanager.AddProject(project);
            Assert.AreEqual(1, projectmanager.Projects.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProject_Null()
        {
            projectmanager.AddProject(null);
        }

        [TestMethod]
        public void RemoveProject_RemovesFromList()
        {
            var project = new Project("Проект для удаления", "Описание", DateTime.Now, DateTime.Now.AddMonths(1));
            projectmanager.AddProject(project);
            projectmanager.RemoveProject(project);
            Assert.AreEqual(0, projectmanager.Projects.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveProject_Null()
        {
            projectmanager.RemoveProject(null);
        }

        [TestMethod]
        public void UpdateProjectProgress_UpdatesProgress()
        {
            var project = new Project("Проект", "Описание", DateTime.Now, DateTime.Now.AddMonths(1));
            projectmanager.AddProject(project);
            projectmanager.UpdateProjectProgress(project, 75);
            Assert.AreEqual(75, project.Progress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateProjectProgress_Null()
        {
            projectmanager.UpdateProjectProgress(null, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateProjectProgress_UnrealValue()
        {
            var project = new Project("Проект", "Описание", DateTime.Now, DateTime.Now.AddMonths(1));
            projectmanager.AddProject(project);
            projectmanager.UpdateProjectProgress(project, 150); 
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }
    }
}