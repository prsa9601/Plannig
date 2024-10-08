using System.Globalization;

namespace Common.Domain.Utils
{
    public static class DateConvertToPersian
    {
        public static string ToPersianDate(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            try
            {
                return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime).ToString().PadLeft(4),
                    pc.GetMonth(dateTime).ToString().PadLeft(2),
                    pc.GetDayOfMonth(dateTime).ToString().PadLeft(2));

                //PadLeft(4, "0"),
            }
            catch
            {
                return "";
            }

        }

        public static string ToPersianDate(this DateTime dateTime, string format)
        {
            //@DateTime.Now.ToPersianDate("ds dd ms y")
            PersianCalendar pc = new PersianCalendar();
            try
            {
                string date = format.Replace("Y", pc.GetYear(dateTime).ToString().PadLeft(4, '0'))
                    .Replace("mm", pc.GetMonth(dateTime).ToString())
                    .Replace("MM", pc.GetMonth(dateTime).ToString().PadLeft(2, '0'))
                     .Replace("dd", pc.GetDayOfMonth(dateTime).ToString())
                    .Replace("DD", pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0'))
                     .Replace("ds", GetDayOfWeekString((int)pc.GetDayOfWeek(dateTime)).ToString())
                    .Replace("ms", GetMonthString(pc.GetMonth(dateTime)).ToString());

                return date;
                //PadLeft(4, "0"),
            }
            catch 
            {
                return "";
            }
        }
        public static string GetDayOfWeekString(int day)
        {
          
                string[] days = new string[] { "شنبه", "جمعه", "پنجشنبه", "چهارشنبه", "سه شنبه", "دوشنبه", "یکشنبه" };
                if (day <= days.Length)
                {
                    return days[day];
                }
            
                return "";
            
        }
        public static string GetMonthString(int month)
        {
          
                string[] months = new string[] { "اسفند", "بهمن", "دی", "آذر", "آبان", "مهر", "شهریور", "مرداد", "تیر", "خرداد", "اردیبهشت", "فروردین" };
                if (month <= months.Length)
                {
                    return months[month-1];
                }
            
                return "";
            
        }
        public static string ToPersianDateTime(this DateTime dateTime)
        {
            try
            {
                return string.Format("{0}:{1} {2}", dateTime.Hour.ToString().PadLeft(2),
                    dateTime.Minute.ToString().PadLeft(2, '0'),
                    dateTime.ToPersianDate());
            }
            catch
            {
                return "";
            }
        }


    }
}
