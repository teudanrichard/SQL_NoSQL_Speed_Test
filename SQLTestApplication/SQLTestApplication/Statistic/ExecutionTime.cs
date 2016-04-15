using SQLTestApplication.Exceptions;
using System;

namespace SQLTestApplication.Statistic
{
    internal class ExecutionTime
    {
        private long start = -1;
        private long end = -1;

        public void Start()
        {
           start = DateTime.Now.Ticks;
        }
        public void End()
        {
            if(start == -1)
            {
                throw new MissingParameterException("Start idő nincs beállítva!");
            }
            end = DateTime.Now.Ticks;
        }
        public double getExecutionTime()
        {
            if(end == -1)
            {
                throw new MissingParameterException("End idő nincs beállítva!"); 
            }
            return TimeSpan.FromTicks(end - start).TotalMilliseconds / 1000.0;
        }

    }
}