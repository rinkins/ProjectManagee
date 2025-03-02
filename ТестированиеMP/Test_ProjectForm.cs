//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ManageProject;
//using System.Reflection;

//namespace ТестированиеMP
//{
//    [TestClass]
//    public class Test_ProjectForm
//    {

//        public ProjectForm projectForm;

//            [TestInitialize]
//            public void TestInitialize()
//            {
//                // Инициализация формы перед каждым тестом
//                projectForm = new ProjectForm();
//                projectForm.Show(); // Показываем форму для тестирования
//            }

//            [TestCleanup]
//            public void TestCleanup()
//            {
//                // Закрываем форму после каждого теста
//                projectForm.Close();
//                projectForm.Dispose();
//            }

//            [TestMethod]
//            public void AddProjectButton_VisibleAndEnabled_OnFormLoad()
//            {
//                // Проверка видимости и доступности кнопки "Добавить"
//                Assert.IsTrue(projectForm.Controls.Find("addProjectButton", true)[0].Visible, "Кнопка 'Добавить' должна быть видимой.");
//                Assert.IsTrue(projectForm.Controls.Find("addProjectButton", true)[0].Enabled, "Кнопка 'Добавить' должна быть доступной.");
//            }

//            [TestMethod]
//            public void RemoveProjectButton_VisibleAndEnabled_OnFormLoad()
//            {
//                // Проверка видимости и доступности кнопки "Удалить"
//                Assert.IsTrue(projectForm.Controls.Find("removeProjectButton", true)[0].Visible, "Кнопка 'Удалить' должна быть видимой.");
//                Assert.IsTrue(projectForm.Controls.Find("removeProjectButton", true)[0].Enabled, "Кнопка 'Удалить' должна быть доступной.");
//            }

//            [TestMethod]
//            public void UpdateProgressButton_VisibleAndEnabled_OnFormLoad()
//            {
//                // Проверка видимости и доступности кнопки "Обновить прогресс"
//                Assert.IsTrue(projectForm.Controls.Find("updateProgressButton", true)[0].Visible, "Кнопка 'Обновить прогресс' должна быть видимой.");
//                Assert.IsTrue(projectForm.Controls.Find("updateProgressButton", true)[0].Enabled, "Кнопка 'Обновить прогресс' должна быть доступной.");
//            }

//            [TestMethod]
//            public void RemoveProjectButton_Disabled_WhenNoProjectSelected()
//            {
//                // Проверка, что кнопка "Удалить" недоступна, если проект не выбран
//                var removeButton = projectForm.Controls.Find("removeProjectButton", true)[0] as Button;
//                var listBox = projectForm.Controls.Find("projectsListBox", true)[0] as ListBox;

//                // Очищаем выбор в ListBox
//                listBox.SelectedIndex = -1;

//                // Проверяем, что кнопка "Удалить" недоступна
//                Assert.IsFalse(removeButton.Enabled, "Кнопка 'Удалить' должна быть недоступной, если проект не выбран.");
//            }

//            [TestMethod]
//            public void UpdateProgressButton_Disabled_WhenNoProjectSelected()
//            {
//                // Проверка, что кнопка "Обновить прогресс" недоступна, если проект не выбран
//                var updateButton = projectForm.Controls.Find("updateProgressButton", true)[0] as Button;
//                var listBox = projectForm.Controls.Find("projectsListBox", true)[0] as ListBox;

//                // Очищаем выбор в ListBox
//                listBox.SelectedIndex = -1;

//                // Проверяем, что кнопка "Обновить прогресс" недоступна
//                Assert.IsFalse(updateButton.Enabled, "Кнопка 'Обновить прогресс' должна быть недоступной, если проект не выбран.");
//            }
//        }
//    }

