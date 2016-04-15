using MongoDB.Bson;
using MongoDB.Driver;
using SQLTestApplication.Exceptions;
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
        static SQLDatabase.MSSQLTestClass msSQL = new SQLDatabase.MSSQLTestClass();
        static SQLDatabase.MySQLInnoDB mySQLInnoDB = new SQLDatabase.MySQLInnoDB();
        static SQLDatabase.MySQLMyISAM mySQLMyISAM = new SQLDatabase.MySQLMyISAM();

        static List<SQLStatistic> stats = new List<SQLStatistic>();

        static void Main(string[] args)
        {


            Console.WriteLine("Start test!");

            Console.WriteLine("Press any key!");
            Console.ReadKey();
            #region Adatbázisok takarítása
            //ha esetleg lenne adat az adatbázisokban azokat töröljük
            try { 
                noSQL.deleteAllRows();
                msSQL.deleteAllRows();
                mySQLInnoDB.deleteAllRows();
                mySQLMyISAM.deleteAllRows();
            }
            catch(NoSQLException ex)
            {
                Console.WriteLine(ex.Message);
            }catch (MSSQLException ex)
            {
                Console.WriteLine(ex.Message);
            }catch(MySQLInnoDBException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (MySQLMyISAMDBException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
            #region Teszt ciklus
            //teszt kezdete
            for (int i = 0; i < 100; i++) {
                try { 
                    stats.Add(noSQL.insertRows(1));
                    stats.Add(noSQL.selectAllRows().Result);
                    stats.Add(noSQL.updateRows(1).Result);
                    stats.Add(noSQL.deleteAllRows());
                    //----------------------------------------
                    stats.Add(msSQL.insertRows(1));
                    stats.Add(msSQL.selectAllRows());
                    stats.Add(msSQL.updateRows(1));
                    stats.Add(msSQL.deleteAllRows());
                    //----------------------------------------
                    stats.Add(mySQLInnoDB.insertRows(1));
                    stats.Add(mySQLInnoDB.selectAllRows());
                    stats.Add(mySQLInnoDB.updateRows(1));
                    stats.Add(mySQLInnoDB.deleteAllRows());
                    //----------------------------------------
                    stats.Add(mySQLMyISAM.insertRows(1));
                    stats.Add(mySQLMyISAM.selectAllRows());
                    stats.Add(mySQLMyISAM.updateRows(1));
                    stats.Add(mySQLMyISAM.deleteAllRows());
                }
                catch(MSSQLException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }catch(NoSQLException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (MySQLInnoDBException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (MySQLMyISAMDBException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (Exception ex) { 
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
            #endregion
            #region Adatok kiértékelése
            double mind = -1,
                   maxd = 1,
                   minNoSQL = -1,
                   maxNoSQL = 1,
                   minMSSQL = -1,
                   maxMSSQL = 1,
                   minMySQLInnoDB = -1,
                   maxMySQLInnoDB = 1,
                   minMySQLMyISAM = -1,
                   maxMySQLMyISAM = 1;

            try {
                mind = stats[0].Time.getExecutionTime();
                maxd           = stats[0].Time.getExecutionTime();
                minNoSQL       = stats[0].Time.getExecutionTime();
                maxNoSQL       = stats[0].Time.getExecutionTime();
                minMSSQL       = stats[4].Time.getExecutionTime();
                maxMSSQL       = stats[4].Time.getExecutionTime();
                minMySQLInnoDB = stats[8].Time.getExecutionTime();
                maxMySQLInnoDB = stats[8].Time.getExecutionTime();
                minMySQLMyISAM = stats[12].Time.getExecutionTime();
                maxMySQLMyISAM = stats[12].Time.getExecutionTime();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            SQLStatistic mins              = stats[0],
                         maxs              = stats[0],
                         mindexNoSQL       = mins,
                         maxdexNoSQL       = maxs,
                         mindexMSSQL       = stats[4],
                         maxdexMSSQL       = stats[4],
                         mindexMySQLInnoDB = stats[8],
                         maxdexMySQLInnoDB = stats[8],
                         mindexMySQLMyISAM = stats[12],
                         maxdexMySQLMyISAM = stats[12];

            double atlagMSSQL = 0, atlagNoSQL = 0, atlagMySQLInnoDB = 0, atlagMySQLMyISAM = 0;
            int count = 0;

            foreach(SQLStatistic s in stats)
            {

                count++;
                if (s.getSQLType.Equals(Types.SQLType.NoSQL))
                {
                    atlagNoSQL += s.Time.getExecutionTime();
                    if (minNoSQL > s.Time.getExecutionTime())
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
                else if (s.getSQLType.Equals(Types.SQLType.MSSQL))
                {

                    atlagMSSQL += s.Time.getExecutionTime();
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
                else if (s.getSQLType.Equals(Types.SQLType.MySQLInnoDB))
                {
                    atlagMySQLInnoDB += s.Time.getExecutionTime();
                    if (minMSSQL > s.Time.getExecutionTime())
                    {
                        minMySQLInnoDB = s.Time.getExecutionTime();
                        mindexMySQLInnoDB = s;
                    }
                    if (maxMSSQL < s.Time.getExecutionTime())
                    {
                        maxMySQLInnoDB = s.Time.getExecutionTime();
                        maxdexMySQLInnoDB = s;
                    }
                }
                else if (s.getSQLType.Equals(Types.SQLType.MySQLMyISAM))
                {
                    atlagMySQLMyISAM += s.Time.getExecutionTime();
                    if (minMSSQL > s.Time.getExecutionTime())
                    {
                        minMySQLMyISAM = s.Time.getExecutionTime();
                        mindexMySQLMyISAM = s;
                    }
                    if (maxMSSQL < s.Time.getExecutionTime())
                    {
                        maxMySQLMyISAM = s.Time.getExecutionTime();
                        maxdexMySQLMyISAM = s;
                    }
                }
            }
            #endregion
            #region Eredmények kiírása képernyőre
            Console.WriteLine("\nLeggyorsabb NoSQL         függvény: " + mindexNoSQL.ToString());
            Console.WriteLine("\nLeglassabb  NoSQL         függvény: " + maxdexNoSQL.ToString());
            Console.WriteLine("\nLeggyorsabb MSSQL         függvény: " + mindexMSSQL.ToString());
            Console.WriteLine("\nLeglassabb  MSSQL         függvény: " + maxdexMSSQL.ToString());
            Console.WriteLine("\nLeggyorsabb MySQL(InnoDB) függvény: " + mindexMySQLInnoDB.ToString());
            Console.WriteLine("\nLeglassabb  MySQL(InnoDB) függvény: " + maxdexMySQLInnoDB.ToString());
            Console.WriteLine("\nLeggyorsabb MySQL(MyISAM) függvény: " + mindexMySQLMyISAM.ToString());
            Console.WriteLine("\nLeglassabb  MySQL(MyISAM) függvény: " + maxdexMySQLMyISAM.ToString());

            Console.WriteLine("\nÖsszes NoSQL         függvény átlagos futásideje : {0:0.00000000} sec", (atlagNoSQL / count));
            Console.WriteLine("\nÖsszes MSSQL         függvény átlagos futásideje : {0:0.00000000} sec", (atlagMSSQL / count));
            Console.WriteLine("\nÖsszes MySQL(InnoDB) függvény átlagos futásideje : {0:0.00000000} sec", (atlagMySQLInnoDB / count));
            Console.WriteLine("\nÖsszes MySQL(MyISAM) függvény átlagos futásideje : {0:0.00000000} sec", (atlagMySQLMyISAM / count));
            Console.ReadLine();
            #endregion
        }

    }
}
