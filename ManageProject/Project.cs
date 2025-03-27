using System;

namespace ManageProject
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Progress { get; set; }
        public int Remember { get; set; }
        public Project(string name, string description, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Progress = 0;
            Remember = 7;
        }
        public void UpdateProgress(int newProgress)
        {
            if (newProgress < 0 || newProgress > 100)
            {
                throw new Exception ("Прогресс должен быть между 0 и 100");
            }

            Progress = newProgress;

        }
        public void UpdateRemember(int newRemember)
        {
            if (newRemember < 0)
            {
                throw new Exception("Напоминание не может быть отрицательным числом!");
            }

            Remember = newRemember;

        }

    }
}