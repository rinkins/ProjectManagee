using ManageProject;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Reflection.Emit;


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
            this.Width = 437;
            this.Height = 405;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.Lavender;

            nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150,
                Text = "Название",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            descriptionTextBox = new TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 200,
                Text = "Описание",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            startDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(10, 40),
                Width = 135,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            endDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(160, 40),
                Width = 135,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            progressTextBox = new TextBox
            {
                Location = new System.Drawing.Point(305, 40),
                Width = 65,
                Text = "Прогресс",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue,
                TextAlign = HorizontalAlignment.Center
            };

            addProjectButton = new Button
            {
                Location = new System.Drawing.Point(10, 70),
                Text = "Добавить",
                Width = 100,
                Height = 27,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };
            addProjectButton.Click += AddProjectButton_Click;

            removeProjectButton = new Button
            {
                Location = new System.Drawing.Point(115, 70),
                Text = "Удалить",
                Width = 100,
                Height = 27,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };
            removeProjectButton.Click += RemoveProjectButton_Click;

            updateProgressButton = new Button
            {
                Location = new System.Drawing.Point(220, 70),
                Text = "Обновить прогресс",
                Width = 120,
                Height = 27,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };
            updateProgressButton.Click += UpdateProgressButton_Click;

            projectsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 110),
                Width = 400,
                Height = 250,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
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