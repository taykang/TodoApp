using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.FilterModel;

public class FilterModel
{
    public int _page { get; set; } = 1;
    public int _page_size { get; set; } = 10;
    public string? _search_text { get; set; }


    // Sorting
    public string? _sort_field { get; set; }
    public string? _sort_direction { get; set; } = "asc";


    // Filter by year and month
    public int _year { get; set; } = 0;
    public int _month { get; set; } = 0;
}
