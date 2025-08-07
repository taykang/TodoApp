using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.FilterModel;

public class TodoFilterModel: FilterModel
{
    public string? _tag { get; set; }
    public bool _isCompleted { get; set; } = false;
    public string? _priority { get; set; }
}
