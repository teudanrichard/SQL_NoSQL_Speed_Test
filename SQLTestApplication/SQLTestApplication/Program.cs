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
                   minMSSQL = stats[4].Time.getExecutionTime(),
                   maxMSSQL = stats[4].Time.getExecutionTime();

            SQLStatistic mindexNoSQL = stats[0],
                         maxdexNoSQL = stats[0],
                         mindexMSSQL = stats[4],
                         maxdexMSSQL = stats[4];

            double atlagMSSQL = 0, atlagNoSQL = 0;
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
                    atlagMSSQL += s.Time.getExecutionTime();
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
                    if (minMSSQL > s.Time.getExecutionTime())
                    {
                        minMSSQL = s.Time.getExecutionTime();
                        mindexMSSQL = s;
                    }

                    if (maxMSSQL < s.Time.getExecutionTime())
                    {
                        maxMSSQL = s.Time.getExecutionTime();
                        maxdexMSSQL = s;
                    }
                }
            }
            #endregion
            #region Eredmények kiírása képernyőre
            Console.WriteLine("\nLeggyorsabb NoSQL függvény : " + mindexNoSQL.ToString());
            Console.WriteLine("\nLeglassabb  NoSQL függvény: " + maxdexNoSQL.ToString());
            Console.WriteLine("\nLeggyorsabb MSSQL függvény : " + mindexMSSQL.ToString());
            Console.WriteLine("\nLeglassabb  MSSQL függvény: " + maxdexMSSQL.ToString());

            if(mindexNoSQL.Time.getExecutionTime() < mindexMSSQL.Time.getExecutionTime())
            {
                Console.WriteLine("\nLeggyorsabb        függvény : " + mindexNoSQL.ToString());
            }
            else
            {
                Console.WriteLine("\nLeggyorsabb        függvény : " + mindexMSSQL.ToString());
            }
            if (maxdexNoSQL.Time.getExecutionTime() > maxdexMSSQL.Time.getExecutionTime())
            {
                Console.WriteLine("\nLeglassabb        függvény : " + maxdexNoSQL.ToString());
            }
            else
            {
                Console.WriteLine("\nLeglassabb        függvény : " + maxdexMSSQL.ToString());
            }
            Console.WriteLine("\nÖsszes NoSQL függvény átlagos futásideje : {0:0.000000} sec", (atlagNoSQL / count));
            Console.WriteLine("\nÖsszes MSSQL függvény átlagos futásideje : {0:0.000000} sec", (atlagMSSQL / count));
            Console.ReadLine();
            #endregion
        }

    }
}
