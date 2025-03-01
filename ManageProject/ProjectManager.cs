using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ManageProject
{

        public class ProjectManager
        {
            public List<Project> Projects { get; private set; }
            public ProjectManager()
            {
                Projects = new List<Project>();
                LoadProjects();
            }
            public void AddProject(Project project)
            {
                if (project == null)
                {
                    throw new ArgumentNullException(nameof(project));
                }
                Projects.Add(project);
                SaveProjects();
            }
            public void RemoveProject(Project project)
            {
                if (project == null)
                {
                    throw new ArgumentNullException(nameof(project));
                }
                Projects.Remove(project);
                SaveProjects();
            }
            public void UpdateProjectProgress(Project project, int newProgress)
            {
                if (project == null)
                {
                    throw new ArgumentNullException(nameof(project));
                }
                project.UpdateProgress(newProgress);
                SaveProjects();
            }
            private void SaveProjects()
            {
                File.WriteAllLines("projects.txt", Projects.Select
                (p => $"{p.Name}|{p.Description}|{p.StartDate.ToString("yyyy-MM-dd")}|{p.EndDate.ToString("yyyyMM-dd")}|{p.Progress}"));
            }
            private void LoadProjects()
            {
                if (File.Exists("projects.txt"))
                {
                    var lines = File.ReadAllLines("projects.txt");
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 5)
                        {
                            DateTime startDate;
                            DateTime endDate;
                            int progress;
                            if (DateTime.TryParse(parts[2], out startDate) && DateTime.TryParse(parts[3],out endDate) && int.TryParse(parts[4], out progress))
                            {
                                Project project = new Project(parts[0], parts[1], startDate, endDate);
                                project.Progress = progress;
                                Projects.Add(project);
                            }
                        }
                    }
                }
            }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProjectForm());
        }

    }

}

