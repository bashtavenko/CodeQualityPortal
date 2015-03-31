using System;
using System.Collections.Generic;
using System.Globalization;

namespace CodeQualityPortal.Data
{
    public class DimDate
    {
        public int DateId { get; set; }
        public DateTime DateTime { get; set; }
        public int Year { get; set; }
        public string YearString { get; set; }
        public int Month { get; set; }
        public string MonthString { get; set; }        
        public int WeekOfYear { get; set; }
        public DateTime Date { get; set; }
        public int DayOfYear { get; set; }
        public int DayOfMonth { get; set; }
        public int DayOfWeek { get; set; }
        public virtual List<FactCodeChurn> Churn { get; set;}
        public virtual List<FactMetrics> Metrics { get; set; }

        public DimDate() : this(DateTime.Now)
        {
        }
        
        public DimDate (DateTime dateTime)
        {
            DateTime = dateTime;
            Year = this.DateTime.Year;
            YearString = Year.ToString();
            Month = this.DateTime.Month;
            MonthString = Month.ToString();
            
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;
            WeekOfYear = calendar.GetWeekOfYear(this.DateTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            Date = this.DateTime.Date;
            DayOfYear = this.DateTime.DayOfYear;
            DayOfMonth = this.DateTime.Day;
            DayOfWeek = (int)this.DateTime.DayOfWeek;

            Churn = new List<FactCodeChurn>();
        }
    }
}
