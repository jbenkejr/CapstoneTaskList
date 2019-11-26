using System;
using System.Collections.Generic;

namespace TaskListCapstone.Models
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }

        public Tasks() {}

        public Tasks(int id, string taskName, string description, DateTime dueDate, bool complete, string userID)
        {
            Id = id;
            TaskName = taskName;
            Description = description;
            DueDate = dueDate;
            Complete = complete;
            UserId = userID;
        }

        public Tasks(string taskName, string description, DateTime dueDate, bool complete, string userID)
        {
            
            TaskName = taskName;
            Description = description;
            DueDate = dueDate;
            Complete = complete;
            UserId = userID;
        }

    }
}
