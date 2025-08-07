using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.DataModel
{
    public class TodoItemModel
    {
        [Key]
        public int _id { get; set; }
        public string _title { get; set; } = string.Empty;
        public string _desc { get; set; } = string.Empty;
        public string _tag { get; set; } = string.Empty;
        public string _priority { get; set; } = string.Empty;
        public bool _isCompleted { get; set; } = false;
        public DateTime _submitted_date { get; set; } = DateTime.Now;
        public DateTime? _completed_date { get; set; }
    }
}
