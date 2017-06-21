﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voodoo.Infrastructure.Notations
{
    public class UIAttribute : Attribute
    {
        public bool IsHidden { get; set; }
        public bool IsReadOnly { get; set; }
        public DisplayFormat DisplayFormat { get; set; } = DisplayFormat.Text;
        public string Grouping { get; set; }
        public int GroupOrder { get; set; }
    }
    public enum DisplayFormat
    {
        Text = 0,
        Date = 1,
        Time = 2,
        DateTime = 3,
        Currency = 4,
        Decimal = 5,
        PhoneNumber = 6
    }
}