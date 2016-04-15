using MongoDB.Bson;
using MongoDB.Driver;
using SQLTestApplication.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQLTestApplication
{
    class Program
    {
        static NoSQLDatabase.NoSQLTestClass noSQL = new NoSQLDatabase.NoSQLTestClass();
        static SQLDatabase.SQLTestClass msSQL = new SQLDatabase.SQLTestClass();
        static List<SQLStatistic> stats = new List<SQLStatistic>();

        static void Main(string[] args)
        {


            Console.WriteLine("Start test!");

            Console.WriteLine("Press any key!");
            Console.ReadKey();
            #region Adatbázisok takarítása
            //ha esetleg lenne adat az adatbázisokban azokat töröljük
            noSQL.deleteAllRows();
            msSQL.deleteAllRows();
            #endregion
            #region Teszt ciklus
            //teszt kezdete
            for (int i = 0; i < 100; i++) {
                stats.Add(noSQL.insertRows(50));
                stats.Add(noSQL.selectAllRows().Result);
                stats.Add(noSQL.updateRows(50).Result);
                stats.Add(noSQL.deleteAllRows());
                //----------------------------------------
                stats.Add(msSQL.insertRows(50));
                stats.Add(msSQL.selectAllRows());
                stats.Add(msSQL.updateRows(50));
                stats.Add(msSQL.deleteAllRows());
            }
            #endregion
            #region Adatok kiértékelése
            double minNoSQL = stats[0].Time.getExecutionTime(),
                   maxNoSQL = stats[0].Time.getExecutionTime(),
                   minSQL = stats[4].Time.getExecutionTime(),
                   maxSQL = stats[4].Time.getExecutionTime();

            SQLStatistic mindexNoSQL = stats[0],
                         maxdexNoSQL = stats[0],
                         mindexSQL = stats[4],
                         maxdexSQL = stats[4];

            double atlagSQL = 0, atlagNoSQL = 0;
            int count = 0;

            foreach(SQLStatistic s in stats)
            {
                if (s.getSQLType.Equals(Types.SQLType.NoSQL))
                {
                    atlagNoSQL += s.Time.getExecutionTime();
                    count++;
                }
                else
                {
                    atlagSQL += s.Time.getExecutionTime();
                }

                if (s.getSQLType.Equals(Types.SQLType.NoSQL)) { 
                    if(minNoSQL > s.Time.getExecutionTime())
                    {
                        minNoSQL = s.Time.getExecutionTime();
                        mindexNoSQL = s;
                    }

                    if (maxNoSQL < s.Time.getExecutionTime())
                    {
                        maxNoSQL = s.Time.getExecutionTime();
                        maxdexNoSQL = s;
                    }
                }
                else
                {
                    if (minSQL > s.Time.getExecutionTime())
                    {
                        minSQL = s.Time.getExecutionTime();
                        mindexSQL = s;
                    }

                    if (maxSQL < s.Time.getExecutionTime())
                    {
                        maxSQL = s.Time.getExecutionTime();
                        maxdexSQL = s;
                    }
                }
            }
            #endregion
            #region Eredmények kiírása képernyőre
            Console.WriteLine("\nLeggyorsabb NoSQL függvény : " + mindexNoSQL.ToString());
            Console.WriteLine("\nLeglassabb  NoSQL függvény: " + maxdexNoSQL.ToString());
            Console.WriteLine("\nLeggyorsabb MSSQL függvény : " + mindexSQL.ToString());
            Console.WriteLine("\nLeglassabb  MSSQL függvény: " + maxdexSQL.ToString());

            if(mindexNoSQL.Time.getExecutionTime() < mindexSQL.Time.getExecutionTime())
            {
                Console.WriteLine("\nLeggyorsabb        függvény : " + mindexNoSQL.ToString());
            }
            else
            {
                Console.WriteLine("\nLeggyorsabb        függvény : " + mindexSQL.ToString());
            }
            if (maxdexNoSQL.Time.getExecutionTime() > maxdexSQL.Time.getExecutionTime())
            {
                Console.WriteLine("\nLeglassabb        függvény : " + maxdexNoSQL.ToString());
            }
            else
            {
                Console.WriteLine("\nLeglassabb        függvény : " + maxdexSQL.ToString());
            }
            Console.WriteLine("\nÖsszes NoSQL függvény átlagos futásideje : {0:0.000000} sec", (atlagNoSQL / count));
            Console.WriteLine("\nÖsszes MSSQL függvény átlagos futásideje : {0:0.000000} sec", (atlagSQL / count));
            Console.ReadLine();
            #endregion
        }

    }
}
