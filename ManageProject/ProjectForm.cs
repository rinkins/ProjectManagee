using System.Windows.Forms;
using System;
using System.Drawing;


namespace ManageProject
{

    partial class ProjectForm : Form
    {
        private ProjectManager projectManager;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private NumericUpDown reminderDatePicker; 
        private System.Windows.Forms.Label reminderLabel;
        private System.Windows.Forms.Label reminder2Label;
        private System.Windows.Forms.TextBox progressTextBox;
        private System.Windows.Forms.Button addProjectButton;
        private System.Windows.Forms.Button removeProjectButton;
        private System.Windows.Forms.Button updateProgressButton;
        private System.Windows.Forms.Button updateRememberButton; 
        private ListBox projectsListBox;

        public ProjectForm()
        {
            this.Text = "Управление проектами";
            this.Width = 437;
            this.Height = 465;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.Lavender;



            nameTextBox = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150,
                Text = "Название",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            nameTextBox.KeyPress += new KeyPressEventHandler(nameTextBox_KeyPress);

            descriptionTextBox = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 200,
                Text = "Описание",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            descriptionTextBox.KeyPress += new KeyPressEventHandler(descriptionTextBox_KeyPress);

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

            progressTextBox = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(305, 40),
                Width = 65,
                Text = "Прогресс",
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue,
                TextAlign = HorizontalAlignment.Center
            };

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

            updateRememberButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(10, 388),
                Text = "Обновить напоминалку",
                Width = 170,
                Height = 27,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };
            updateRememberButton.Click += UpdateRememberButton_Click; 

            projectsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 110),
                Width = 400,
                Height = 250,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue
            };

            reminderDatePicker = new NumericUpDown
            {
                Location = new System.Drawing.Point(290, 360),
                Width = 40,
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue,
                Value = 7

            };

            reminderLabel = new Label
            {
                Location = new System.Drawing.Point(10, 363),
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue,
                Width = 330,
                Text = "Предупреждать о сроках сдачи проекта за "
            };

            reminder2Label = new Label
            {
                Location = new System.Drawing.Point(340, 363),
                Font = new System.Drawing.Font("Candara", 10),
                ForeColor = System.Drawing.Color.MidnightBlue,
                Text = "дней"
            };




            this.Controls.Add(nameTextBox);
            this.Controls.Add(descriptionTextBox);
            this.Controls.Add(startDatePicker);
            this.Controls.Add(endDatePicker);
            this.Controls.Add(progressTextBox); 
            this.Controls.Add(addProjectButton);
            this.Controls.Add(removeProjectButton);
            this.Controls.Add(updateProgressButton);
            this.Controls.Add(updateRememberButton);
            this.Controls.Add(reminder2Label);
            this.Controls.Add(projectsListBox);
            this.Controls.Add(reminderDatePicker);
            this.Controls.Add(reminderLabel);
            this.Controls.Add(reminder2Label); 
            projectManager = new ProjectManager();
            this.MessageAlarm(this);
            this.FriendlyReminder(this); 
            UpdateProjectsList();
        }

        private void nameTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (!IsCyrillic(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true; 
            }

        }
        private void descriptionTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (!IsCyrillic(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;
            }

        }
        private bool IsCyrillic(char c)
        {
            return (c >= 'а' && c <= 'я') || 
                   (c >= 'А' && c <= 'Я') || 
                   c == 'ё' || c == 'Ё'; 
        }
        private void UpdateProjectsList()
        {
            projectsListBox.Items.Clear();
            foreach (var project in projectManager.Projects)
            {
                projectsListBox.Items.Add($"{project.Name} - Прогресс: {project.Progress}% - Напоминить за {project.Remember} дней");
            }

        }


        private void AddProjectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(descriptionTextBox.Text))
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
            Project newProject = new Project(nameTextBox.Text, descriptionTextBox.Text, startDate, endDate);
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

             private void UpdateRememberButton_Click(object sender, EventArgs e)
        {
            if (projectsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите проект для обновления напоминалки!");
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
                    if (string.IsNullOrEmpty(reminderDatePicker.Text))
                    {
                        MessageBox.Show("Введите новый срок напоминалки!");
                        return;
                    }
                    int newRemember;
                    if (!int.TryParse(reminderDatePicker.Text, out newRemember))
                    {
                        MessageBox.Show("Неверный формат напоминалки!");
                        return;
                    }
                    try
                    {
                        projectManager.UpdateProjectRemember(projectToUpdate, newRemember);
                        UpdateProjectsList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }

        private void MessageAlarm(ProjectForm projectForm)
        {

            foreach (var project in projectManager.Projects)
            {
                DateTime endDate = project.EndDate;

                if (DateTime.Now > endDate)
                {
                    if (project.Progress != 100)
                    {
                        MessageBox.Show($"Обратите внимание! У проекта {project.Name} просрочен срок сдачи"); 
                    }
                }
            }
        }
        private void FriendlyReminder(ProjectForm projectForm)
        {

            foreach (var project in projectManager.Projects)
            {
                DateTime endDate = project.EndDate;

                if (Math.Round((endDate - DateTime.Now).TotalDays)+1 == project.Remember)
                {
                    if (project.Progress != 100)
                    {
                        MessageBox.Show($"Обратите внимание! До срока завершения проекта {project.Name} осталось {project.Remember} дней");
                    }
                }
            }
        }



    }
}