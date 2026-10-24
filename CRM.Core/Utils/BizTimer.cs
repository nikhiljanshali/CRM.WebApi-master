using System.Diagnostics;

namespace CRM.Core.Utils {
    /// <summary>
    /// purpose of this class is to get timing of executions of certain code. Need to have this for recording purpose in the future.
    /// </summary>
    public class BizTimer {
        Stopwatch sw;
        BizTimeUnit _timeUnit;

        /// <summary>
        /// Initialize  timer
        /// </summary>
        public BizTimer() {
            _timeUnit = BizTimeUnit.Millisecond;

            sw = new Stopwatch();
            sw.Start();
        }

        /// <summary>
        /// Initialize timer
        /// </summary>
        /// <param name="timeUnit">Provide time unit</param>
        public BizTimer(BizTimeUnit timeUnit) {
            _timeUnit = timeUnit;
            sw = new Stopwatch();
            sw.Start();
        }

        /// <summary>
        /// Stop stopwatch.
        /// </summary>
        /// <returns>Elapsed time</returns>
        public string Finish() {
            sw.Stop();
            switch (_timeUnit) {
                case BizTimeUnit.Millisecond:
                    return sw.ElapsedMilliseconds.ToString("0.##") + " ms";
                case BizTimeUnit.Second:
                    return sw.Elapsed.TotalSeconds.ToString("0.##") + " s";
                case BizTimeUnit.Minute:
                    return sw.Elapsed.TotalMinutes.ToString("0.##") + " mins";
                case BizTimeUnit.Hour:
                    return sw.Elapsed.TotalHours.ToString("0.##") + " hrs";
                case BizTimeUnit.Day:
                    return sw.Elapsed.TotalDays.ToString("0.##") + " days";
                default:
                    return "NOT SUPPORTED";
            }
        }

    }

    public enum BizTimeUnit {
        Millisecond,
        Second,
        Minute,
        Hour,
        Day
    }
}
