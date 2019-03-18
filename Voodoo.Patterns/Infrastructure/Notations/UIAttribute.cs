using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;

namespace Voodoo.Infrastructure.Notations
{
    public class UIAttribute : Attribute
    {
        public bool IsHidden { get; set; }
        public bool IsReadOnly { get; set; }
        public DisplayFormat DisplayFormat { get; set; } = DisplayFormat.NotSet;
        public string Grouping { get; set; }
        public int GroupOrder { get; set; }
        public int? ListId { get; set; }
        public bool DoNotSort { get; set; }
        public bool DoNotFormat { get; set; }
        public int NumberOfDecimalPlaces { get; set; } = -1;
        public List<NameValuePair> Metadata { get; set; }
        public string SortExpression { get; set; }
        public string HelpText { get; set; }
    }

    public enum DisplayFormat
    {
        NotSet=0,
        Text = 1,
        Date = 2,
        Time = 3,
        DateTime = 4,
        Currency = 5,
        Decimal = 6,
        PhoneNumber = 7,
        Int = 8
    }
}