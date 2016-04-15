using SQLTestApplication.Exceptions;
using System;
using System.Diagnostics;

namespace SQLTestApplication.Statistic
{
    internal class ExecutionTime
    {

        Stopwatch stopwatch = new Stopwatch();

        public void Start()
        {
           stopwatch.Start();
        }

        public void End()
        {
            stopwatch.Stop();
        }


        public double getExecutionTime()
        {           
            return stopwatch.Elapsed.TotalMilliseconds / 1000;
        }

    }
}