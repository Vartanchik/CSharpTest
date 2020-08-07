using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            // because of start date is counted we subtract 1
            var requiredCountOfDays = dayCount - 1;

            // checking are there weekends
            if (weekEnds == null || weekEnds.Length == 0)
            {
                return startDate.AddDays(requiredCountOfDays);
            }

            // daysBetweenWeekends - summ of days between weekends and first weekend and start date
            // counting days between first weekend and start date
            var daysBetweenWeekends = (weekEnds[0].StartDate - startDate).Days - 1;

            // checking if days between first weekend and start date are enough
            if ((daysBetweenWeekends) >= requiredCountOfDays)
            {
                return startDate.AddDays(requiredCountOfDays);
            }

            // checking if there is more then one weekend
            if (weekEnds.Length >= 2)
            {
                // checking between what pair of weekends we will have enough days for dayCount
                for (int i = 1; i < weekEnds.Length; i++)
                {
                    var daysBetweenCurrentWeekend = (weekEnds[i].StartDate - weekEnds[i-1].EndDate).Days - 1;

                    if ((daysBetweenWeekends + daysBetweenCurrentWeekend) >= requiredCountOfDays)
                    {
                        return weekEnds[i-1].EndDate.AddDays(requiredCountOfDays - daysBetweenWeekends);
                    }

                    daysBetweenWeekends += daysBetweenCurrentWeekend;
                }
            }

            return weekEnds[weekEnds.Length-1].EndDate.AddDays(requiredCountOfDays - daysBetweenWeekends);
        }
    }
}
