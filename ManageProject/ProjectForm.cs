using ManageProject;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ManageProject
{

    partial class ProjectForm : Form
    {
        private ProjectManager projectManager;
        private TextBox nameTextBox;
        private TextBox descriptionTextBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private TextBox progressTextBox;
        private Button addProjectButton;
        private Button removeProjectButton;
        private Button updateProgressButton;
        private ListBox projectsListBox;
        public ProjectForm()
        {
            this.Text = "Управление проектами";
            this.Width = 600;
            this.Height = 500;

            nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150,
                Text = "Название"
            };

            descriptionTextBox = new TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 200,
                Text = "Описание"
            };

            startDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(10, 40),
                Width = 125
            };

            endDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(150, 40),
                Width = 125
            };

            progressTextBox = new TextBox
            {
                Location = new System.Drawing.Point(285, 40),
                Width = 65,
                Text = "Прогресс"
            };

            addProjectButton = new Button
            {
                Location = new System.Drawing.Point(10, 70),
                Text = "Добавить",
                Width = 100
            };
            addProjectButton.Click += AddProjectButton_Click;

            removeProjectButton = new Button
            {
                Location = new System.Drawing.Point(115, 70),
                Text = "Удалить",
                Width = 100
            };
            removeProjectButton.Click += RemoveProjectButton_Click;

            updateProgressButton = new Button
            {
                Location = new System.Drawing.Point(220, 70),
                Text = "Обновить прогресс",
                Width = 120
            };
            updateProgressButton.Click += UpdateProgressButton_Click;

            projectsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 100),
                Width = 560,
                Height = 250
            };
            this.Controls.Add(nameTextBox);
            this.Controls.Add(descriptionTextBox);
            this.Controls.Add(startDatePicker);
            this.Controls.Add(endDatePicker);
            this.Controls.Add(progressTextBox);
            this.Controls.Add(addProjectButton);
            this.Controls.Add(removeProjectButton);
            this.Controls.Add(updateProgressButton);
            this.Controls.Add(projectsListBox);
            projectManager = new ProjectManager();
            UpdateProjectsList();
        }
        private void UpdateProjectsList()
        {
            projectsListBox.Items.Clear();
            foreach (var project in projectManager.Projects)
            {
                projectsListBox.Items.Add($"{project.Name} - Прогресс: {project.Progress}%");
            }
        }
        private void AddProjectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) ||
            string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DateTime startDate = startDatePicker.Value;
            DateTime endDate = endDatePicker.Value;
            if (startDate > endDate)
            {
                MessageBox.Show("Дата начала должна быть раньше даты окончания!");
                return;
            }
            Project newProject = new Project(nameTextBox.Text, descriptionTextBox.Text,
            startDate, endDate);
            try
            {
                projectManager.AddProject(newProject);
                nameTextBox.Clear();
                descriptionTextBox.Clear();
                UpdateProjectsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveProjectButton_Click(object sender, EventArgs e)
        {
            if (projectsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите проект для удаления!");
                return;
            }
            string selectedItem = projectsListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string name = parts[0].Trim();
                var projectToRemove = projectManager.Projects.Find(p => p.Name == name);
                if (projectToRemove != null)
                {
                    try
                    {
                        projectManager.RemoveProject(projectToRemove);
                        UpdateProjectsList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void UpdateProgressButton_Click(object sender, EventArgs e)
        {
            if (projectsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите проект для обновления прогресса!");
                return;
            }
            string selectedItem = projectsListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string name = parts[0].Trim();
                var projectToUpdate = projectManager.Projects.Find(p => p.Name == name);
                if (projectToUpdate != null)
                {
                    if (string.IsNullOrEmpty(progressTextBox.Text))
                    {
                        MessageBox.Show("Введите новый прогресс!");
                        return;
                    }
                    int newProgress;
                    if (!int.TryParse(progressTextBox.Text, out newProgress))
                    {
                        MessageBox.Show("Неверный формат прогресса!");
                        return;
                    }
                    try
                    {
                        projectManager.UpdateProjectProgress(projectToUpdate, newProgress);
                        UpdateProjectsList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}