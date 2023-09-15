using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Helpers
{
    public static class DateTimeExtension
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int difference = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * difference).Date;
        }
    }
}
