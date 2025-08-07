﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Constant
{
    public static class TodoPriority
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";

        public static readonly string[] All = { Low, Medium, High };
    }

}
