using System;

namespace ToDoList.Models {
    public class ToDoTask {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
