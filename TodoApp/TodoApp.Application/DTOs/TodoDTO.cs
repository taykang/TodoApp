
using TodoApp.Core.Constant;

namespace TodoApp.Application.DTOs
{
    public class TodoDto
    {
        public int _id { get; set; }
        public string _title { get; set; } = string.Empty;
        public string _desc { get; set; } = string.Empty;
        public string _tag { get; set; } = string.Empty;
        public string _priority { get; set; } = string.Empty;
        public bool _isCompleted { get; set; }
        public DateTime _submitted_date { get; set; }
        public DateTime? _completed_date { get; set; }
    }

    public class TodoCreateDto
    {
        public string _title { get; set; } = string.Empty;
        public string _desc { get; set; } = string.Empty;
        public string _tag { get; set; } = string.Empty;
        public string _priority { get; set; } = TodoPriority.Low;
    }

    public class TodoUpdateDto
    {
        public int _id { get; set; }
        public string _title { get; set; } = string.Empty;
        public string _desc { get; set; } = string.Empty;
        public string _tag { get; set; } = string.Empty;
        public string _priority { get; set; } = TodoPriority.Low;
        public bool _isCompleted { get; set; }
    }
}
