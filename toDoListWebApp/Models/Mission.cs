using System.ComponentModel.DataAnnotations;


namespace toDoListWebApp.Models
{
    public class Mission
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public byte TaskStatus { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
