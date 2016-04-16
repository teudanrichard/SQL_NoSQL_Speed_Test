using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Statistic
{
    class DatabaseStatisticObject
    {
        private SQLStatistic MinStatistic,MaxStatistic;
        private double Min, Max, Atlag;

        public DatabaseStatisticObject(double min, double max,SQLStatistic mins, SQLStatistic maxs)
        {
            this.Min = min;
            this.Max = max;
            this.MinStatistic = mins;
            this.MaxStatistic = maxs;
        }

        public double AtlagMethod
        {
            get { return Atlag; }
            set { this.Atlag = value; }
        }
        public double MinMethod
        {
            get { return Min; }
            set { this.Min = value; }
        }
        public double MaxMethod
        {
            get { return Max; }
            set { this.Max = value; }
        }
        public SQLStatistic MinStatisticMethod
        {
            get { return MinStatistic; }
            set { this.MinStatistic = value; }
        }
        public SQLStatistic MaxStatisticMethod
        {
            get { return MaxStatistic; }
            set { this.MaxStatistic = value; }
        }
    }
}
