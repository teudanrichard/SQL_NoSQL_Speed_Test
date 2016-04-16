using MySql.Data.MySqlClient;
using SQLTestApplication.Exceptions;
using SQLTestApplication.Statistic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.SQLDatabase
{
    class MySQLInnoDB
    {
        private static DataSet DataSetObject = new DataSet();
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
        private static MySqlConnection SqlConnectionObject = new MySqlConnection(ConnectionString);
        private static MySqlDataAdapter SqlDataAdapterObject = new MySqlDataAdapter();


        public SQLStatistic selectAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MySQL(InnoDB) Read", Types.SQLActions.Olvasás, Types.SQLType.MySQLInnoDB);
            try
            {
                stat.Start();
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                MySqlDataReader dataReader = null;
                string query = "SELECT * FROM test";
                SqlDataAdapterObject.SelectCommand = new MySqlCommand(query, SqlConnectionObject);
                dataReader = SqlDataAdapterObject.SelectCommand.ExecuteReader();
                string result = "";
                while (dataReader.Read())
                {
                    result += dataReader["ID"] + "," + dataReader["Name"] + "," + dataReader["NeptunCode"] + "\n";
                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MySQLInnoDBException("MySQL(InnoDB) Hiba történt az adat(ok) olvasása során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic insertRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MySQL(InnoDB) Insert", Types.SQLActions.Beszúrás, Types.SQLType.MySQLInnoDB);
            try
            {
                stat.Start();
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                for (int i = 0; i < rows; i++)
                {
                    string query = "INSERT INTO test(ID,Name,NeptunCode) VALUES (@ID,@Name,@NeptunCode)";
                    SqlDataAdapterObject.InsertCommand = new MySqlCommand(query, SqlConnectionObject);
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@ID", MySqlDbType.Int32).Value = i;
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@Name", MySqlDbType.VarChar, 30).Value = "testUser" + i;
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@NeptunCode", MySqlDbType.VarChar, 6).Value = "B" + i;
                    SqlDataAdapterObject.InsertCommand.ExecuteNonQuery();
                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MySQLInnoDBException("MySQL(InnoDB) Hiba történt az adat(ok) beszúrása során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic deleteAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MySQL(InnoDB) Delete", Types.SQLActions.Törlés, Types.SQLType.MySQLInnoDB);
            try
            {
                stat.Start();
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                string query = "DELETE FROM test";
                SqlDataAdapterObject.DeleteCommand = new MySqlCommand(query, SqlConnectionObject);
                SqlDataAdapterObject.DeleteCommand.ExecuteNonQuery();
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MySQLInnoDBException("MySQL(InnoDB) Hiba történt az adat(ok) törlése során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic updateRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MySQL(InnoDB) Update", Types.SQLActions.Frissítés, Types.SQLType.MySQLInnoDB);

            try
            {
                stat.Start();
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                for (int i = 0; i < rows; i++)
                {
                    string query = "UPDATE test SET NeptunCode=@code WHERE Name=@name";
                    SqlDataAdapterObject.UpdateCommand = new MySqlCommand(query, SqlConnectionObject);
                    SqlDataAdapterObject.UpdateCommand.Parameters.Add("@name", MySqlDbType.VarChar, 30).Value = "testUser" + i;
                    SqlDataAdapterObject.UpdateCommand.Parameters.Add("@code", MySqlDbType.VarChar, 30).Value = "HS8GZ9";

                    SqlDataAdapterObject.UpdateCommand.ExecuteNonQuery();


                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MySQLInnoDBException("MySQL(InnoDB) Hiba történt az adat(ok) frissítése során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }
    }
}
