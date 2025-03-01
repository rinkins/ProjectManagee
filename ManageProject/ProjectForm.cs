using ManageProject;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ManageProject
{

    partial class ProjectForm : Form
    {
        private ProjectManager projectManager;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private System.Windows.Forms.TextBox progressTextBox;
        private System.Windows.Forms.Button addProjectButton;
        private System.Windows.Forms.Button removeProjectButton;
        private System.Windows.Forms.Button updateProgressButton;
        private ListBox projectsListBox;

        public ProjectForm()
        {
            // Настройки формы
            this.Text = "Управление проектами";
            this.Width = 437;
            this.Height = 405;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.Lavender;


            // Настройки поля ввода названия
            nameTextBox = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150,
                Text = "Название",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            nameTextBox.KeyPress += new KeyPressEventHandler(nameTextBox_KeyPress);

            // Настройки поля ввода описания
            descriptionTextBox = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 200,
                Text = "Описание",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            descriptionTextBox.KeyPress += new KeyPressEventHandler(descriptionTextBox_KeyPress);

            // Настройки поля ввода даты начала
            startDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(10, 40),
                Width = 135,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            // Настройки поля ввода даты конца
            endDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(160, 40),
                Width = 135,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            // Настройки поля ввода процесса
            progressTextBox = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(305, 40),
                Width = 65,
                Text = "Прогресс",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue,
                TextAlign = HorizontalAlignment.Center
            };

            // Кнопка добавить
            addProjectButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(10, 70),
                Text = "Добавить",
                Width = 100,
                Height = 27,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };
            addProjectButton.Click += AddProjectButton_Click;

            // Кнопка удалить
            removeProjectButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(115, 70),
                Text = "Удалить",
                Width = 100,
                Height = 27,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };
            removeProjectButton.Click += RemoveProjectButton_Click;

            // Кнопка обновить процесс
            updateProgressButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(220, 70),
                Text = "Обновить прогресс",
                Width = 120,
                Height = 27,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };
            updateProgressButton.Click += UpdateProgressButton_Click;

            // Настройки поля вывода проектов
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
        // Ограничение для названия - ввод только кириллицы
        private void nameTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (!IsCyrillic(e.KeyChar))
            {
                e.Handled = true; 
            }

        }
        // Ограничение для описания - ввод только кириллицы
        private void descriptionTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (!IsCyrillic(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        // Кириллица
        private bool IsCyrillic(char c)
        {
            return (c >= 'а' && c <= 'я') || 
                   (c >= 'А' && c <= 'Я') || 
                   c == 'ё' || c == 'Ё'; 
        }
        // Обновление проекта
        private void UpdateProjectsList()
        {
            projectsListBox.Items.Clear();
            foreach (var project in projectManager.Projects)
            {
                projectsListBox.Items.Add($"{project.Name} - Прогресс: {project.Progress}%");
            }
        }
        // Добавление проекта
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
        //Удаление проекта
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

        // Обновление процесса
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