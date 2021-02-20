using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.SYSTEM
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 현재시간반환
        /// </summary>
        /// <returns>현재시간반환</returns>
        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 시간차이값반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the시간차이값반환</returns>
        public static TimeSpan Diff(DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2);
        }

        /// <summary>
        /// 밀리초(Sec)차이값반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in millisecond</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double AbsDiffInMilliSec(DateTime time1, DateTime time2)
        {
            return Math.Abs(time1.Subtract(time2).TotalMilliseconds);
        }

        /// <summary>
        /// 두 시간의 차이를 찾고 차이를 밀리 초 단위로 반환 
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in millisecond</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double DiffInMilliSec(DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2).TotalMilliseconds;
        }

        /// <summary>
        /// 두 시간의 절대 차이를 찾고 두 번째 시간의 차이를 반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in second</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double AbsDiffInSec(DateTime time1, DateTime time2)
        {
            return Math.Abs(time1.Subtract(time2).TotalSeconds);
        }

        /// <summary>
        /// 두 시간의 차이를 찾아서 두 번째 차이를 반환 
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in second</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double DiffInSec(DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2).TotalSeconds;
        }

        /// <summary>
        /// 두 시간 사이의 절대적인 차이를 찾아서 분 차이를 반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in minute</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double AbsDiffInMin(DateTime time1, DateTime time2)
        {
            return Math.Abs(time1.Subtract(time2).TotalMinutes);
        }

        /// <summary>
        /// 두 시간의 차이를 찾아서 분 차이를 반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in minute</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double DiffInMin(DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2).TotalMinutes;
        }

        /// <summary>
        /// 두 시간의 절대 차이를 찾아 시간 차이를 반환 
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in hour</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double AbsDiffInHour(DateTime time1, DateTime time2)
        {
            return Math.Abs(time1.Subtract(time2).TotalHours);
        }

        /// <summary>
        /// 두 시간의 차이를 찾아 시간 차이를 반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in hour</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double DiffInHour(DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2).TotalHours;
        }

        /// <summary>
        /// 두 시간의 절대적인 차이를 찾아서 그 차이를 반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in day</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double AbsDiffInDay(DateTime time1, DateTime time2)
        {
            return Math.Abs(time1.Subtract(time2).TotalDays);
        }

        /// <summary>
        /// 두 시간의 차이를 찾아서 그 차이를 반환
        /// </summary>
        /// <param name="time1">the first time to find the difference</param>
        /// <param name="time2">the second time to find the difference</param>
        /// <returns>the difference between two given time in day</returns>
        /// <remarks>result = time1 - time2</remarks>
        public static double DiffInDay(DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2).TotalDays;
        }

        // 현재 초
        public static int GetNowTime()
        {
            System.DateTime now = System.DateTime.Now.ToLocalTime();
            System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            int nowTime = (int)span.TotalSeconds;

            return nowTime;
        }

        public static string GetNowString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static DateTime ConvertDtFromExamdate(String dt)
        {
            return new DateTime(int.Parse(dt.Substring(0, 4)), int.Parse(dt.Substring(4, 2)), int.Parse(dt.Substring(6)));
        }
    }
}
