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
                    stats.Add(noSQL.insertRows(10));
                    stats.Add(noSQL.selectAllRows().Result);
                    stats.Add(noSQL.updateRows(10).Result);
                    stats.Add(noSQL.deleteAllRows());
                    //----------------------------------------
                    stats.Add(msSQL.insertRows(10));
                    stats.Add(msSQL.selectAllRows());
                    stats.Add(msSQL.updateRows(10));
                    stats.Add(msSQL.deleteAllRows());
                    //----------------------------------------
                    stats.Add(mySQLInnoDB.insertRows(10));
                    stats.Add(mySQLInnoDB.selectAllRows());
                    stats.Add(mySQLInnoDB.updateRows(10));
                    stats.Add(mySQLInnoDB.deleteAllRows());
                    //----------------------------------------
                    stats.Add(mySQLMyISAM.insertRows(10));
                    stats.Add(mySQLMyISAM.selectAllRows());
                    stats.Add(mySQLMyISAM.updateRows(10));
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
            DatabaseStatisticObject SQL = new DatabaseStatisticObject(stats[0].getExecutionTime(), stats[0].getExecutionTime(), stats[0], stats[0]);
            DatabaseStatisticObject NoSQL = new DatabaseStatisticObject(stats[0].getExecutionTime(), stats[0].getExecutionTime(),stats[0],stats[0]);
            DatabaseStatisticObject MSSQL = new DatabaseStatisticObject(stats[4].getExecutionTime(), stats[4].getExecutionTime(), stats[4], stats[4]);
            DatabaseStatisticObject MySQLInnoDB = new DatabaseStatisticObject(stats[8].getExecutionTime(), stats[8].getExecutionTime(), stats[8], stats[8]);
            DatabaseStatisticObject MySQLMyISAM = new DatabaseStatisticObject(stats[12].getExecutionTime(), stats[12].getExecutionTime(), stats[12], stats[12]);

            int count = 0;

            foreach(SQLStatistic s in stats)
            {
                if (s.getSQLType.Equals(Types.SQLType.NoSQL))
                {
                    count++;
                    NoSQL.AtlagMethod += s.getExecutionTime();
                    if (NoSQL.MinMethod > s.getExecutionTime())
                    {
                        NoSQL.MinMethod = s.getExecutionTime();
                        NoSQL.MinStatisticMethod = s;
                    }
                    if (NoSQL.MaxMethod < s.getExecutionTime())
                    {
                        NoSQL.MaxMethod = s.getExecutionTime();
                        NoSQL.MaxStatisticMethod = s;
                    }
                }
                else if (s.getSQLType.Equals(Types.SQLType.MSSQL))
                {

                    MSSQL.AtlagMethod += s.getExecutionTime();
                    if (MSSQL.MinMethod > s.getExecutionTime())
                    {
                        MSSQL.MinMethod = s.getExecutionTime();
                        MSSQL.MinStatisticMethod = s;
                    }
                    if (MSSQL.MaxMethod < s.getExecutionTime())
                    {
                        MSSQL.MaxMethod = s.getExecutionTime();
                        MSSQL.MaxStatisticMethod = s;
                    }
                }
                else if (s.getSQLType.Equals(Types.SQLType.MySQLInnoDB))
                {
                    MySQLInnoDB.AtlagMethod += s.getExecutionTime();
                    if (MySQLInnoDB.MinMethod > s.getExecutionTime())
                    {
                        MySQLInnoDB.MinMethod = s.getExecutionTime();
                        MySQLInnoDB.MinStatisticMethod = s;
                    }
                    if (MySQLInnoDB.MaxMethod < s.getExecutionTime())
                    {
                        MySQLInnoDB.MaxMethod = s.getExecutionTime();
                        MySQLInnoDB.MaxStatisticMethod = s;
                    }
                }
                else if (s.getSQLType.Equals(Types.SQLType.MySQLMyISAM))
                {
                    MySQLMyISAM.AtlagMethod += s.getExecutionTime();
                    if (MySQLMyISAM.MinMethod > s.getExecutionTime())
                    {
                        MySQLMyISAM.MinMethod = s.getExecutionTime();
                        MySQLMyISAM.MinStatisticMethod = s;
                    }
                    if (MySQLMyISAM.MaxMethod < s.getExecutionTime())
                    {
                        MySQLMyISAM.MaxMethod = s.getExecutionTime();
                        MySQLMyISAM.MaxStatisticMethod = s;
                    }
                }
                if(SQL.MinMethod > s.getExecutionTime())
                {
                    SQL.MinMethod = s.getExecutionTime();
                    SQL.MinStatisticMethod = s;
                }
                if(SQL.MaxMethod < s.getExecutionTime())
                {
                    SQL.MaxMethod = s.getExecutionTime();
                    SQL.MaxStatisticMethod = s;
                }
            }
            #endregion
            #region Eredmények kiírása képernyőre
            Console.WriteLine("\nLeggyorsabb NoSQL         függvény: " + NoSQL.MinStatisticMethod.ToString());
            Console.WriteLine("\nLeglassabb  NoSQL         függvény: " + NoSQL.MaxStatisticMethod.ToString());
            Console.WriteLine("\nLeggyorsabb MSSQL         függvény: " + MSSQL.MinStatisticMethod.ToString());
            Console.WriteLine("\nLeglassabb  MSSQL         függvény: " + MSSQL.MaxStatisticMethod.ToString());
            Console.WriteLine("\nLeggyorsabb MySQL(InnoDB) függvény: " + MySQLInnoDB.MinStatisticMethod.ToString());
            Console.WriteLine("\nLeglassabb  MySQL(InnoDB) függvény: " + MySQLInnoDB.MaxStatisticMethod.ToString());
            Console.WriteLine("\nLeggyorsabb MySQL(MyISAM) függvény: " + MySQLMyISAM.MinStatisticMethod.ToString());
            Console.WriteLine("\nLeglassabb  MySQL(MyISAM) függvény: " + MySQLMyISAM.MaxStatisticMethod.ToString());

            Console.WriteLine("\nÖsszes NoSQL         függvény átlagos futásideje : {0:0.0000000} sec", (NoSQL.AtlagMethod / count));
            Console.WriteLine("\nÖsszes MSSQL         függvény átlagos futásideje : {0:0.0000000} sec", (MSSQL.AtlagMethod / count));
            Console.WriteLine("\nÖsszes MySQL(InnoDB) függvény átlagos futásideje : {0:0.0000000} sec", (MySQLInnoDB.AtlagMethod / count));
            Console.WriteLine("\nÖsszes MySQL(MyISAM) függvény átlagos futásideje : {0:0.0000000} sec", (MySQLMyISAM.AtlagMethod / count));

            Console.WriteLine("\n\n\nA legyorsabb függvény : {0:0.0000000} ", SQL.MinStatisticMethod.ToString());
            Console.WriteLine("\nA leglassabb függvény : {0:0.0000000} ", SQL.MaxStatisticMethod.ToString());
            Console.ReadLine();
            #endregion
        }

    }
}
