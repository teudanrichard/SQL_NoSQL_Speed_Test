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
    class MySQLMyISAM
    {
        private static DataSet DataSetObject = new DataSet();
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
        private static MySqlConnection SqlConnectionObject = new MySqlConnection(ConnectionString);
        private static MySqlDataAdapter SqlDataAdapterObject = new MySqlDataAdapter();


        public SQLStatistic selectAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MySQL(MyISAM) Read", Types.SQLActions.Olvasás, Types.SQLType.MySQLMyISAM);
            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                MySqlDataReader dataReader = null;
                string query = "SELECT * FROM testisam";
                SqlDataAdapterObject.SelectCommand = new MySqlCommand(query, SqlConnectionObject);
                stat.Start();
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
                throw new MySQLMyISAMDBException("MySQL(MyISAM) Hiba történt az adat(ok) olvasása során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic insertRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MySQL(MyISAM) Insert", Types.SQLActions.Beszúrás, Types.SQLType.MySQLMyISAM);
            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                stat.Start();
                //------------------------------------------------------------------------------------------------------------
                for (int i = 0; i < rows; i++)
                {
                    string query = "INSERT INTO testisam(ID,Name,NeptunCode) VALUES (@ID,@Name,@NeptunCode)";
                    SqlDataAdapterObject.InsertCommand = new MySqlCommand(query, SqlConnectionObject);
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@ID", MySqlDbType.Int32).Value = i;
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@Name", MySqlDbType.VarChar, 30).Value = "testisamUser" + i;
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@NeptunCode", MySqlDbType.VarChar, 6).Value = "B" + i;
                    SqlDataAdapterObject.InsertCommand.ExecuteNonQuery();
                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MySQLMyISAMDBException("MySQL(MyISAM) Hiba történt az adat(ok) beszúrása során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic deleteAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MySQL(MyISAM) Delete", Types.SQLActions.Törlés, Types.SQLType.MySQLMyISAM);
            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                string query = "DELETE FROM testisam";
                SqlDataAdapterObject.DeleteCommand = new MySqlCommand(query, SqlConnectionObject);
                stat.Start();
                SqlDataAdapterObject.DeleteCommand.ExecuteNonQuery();
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MySQLMyISAMDBException("MySQL(MyISAM) Hiba történt az adat(ok) törlése során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic updateRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MySQL(MyISAM) Update", Types.SQLActions.Frissítés, Types.SQLType.MySQLMyISAM);

            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                string query = "UPDATE testisam SET NeptunCode=@code WHERE Name=@name";
                stat.Start();
                //------------------------------------------------------------------------------------------------------------
                for (int i = 0; i < rows; i++)
                {
                    SqlDataAdapterObject.UpdateCommand = new MySqlCommand(query, SqlConnectionObject);
                    SqlDataAdapterObject.UpdateCommand.Parameters.Add("@name", MySqlDbType.VarChar, 30).Value = "testisamUser" + i;
                    SqlDataAdapterObject.UpdateCommand.Parameters.Add("@code", MySqlDbType.VarChar, 6).Value = "HS8GZ9";

                    SqlDataAdapterObject.UpdateCommand.ExecuteNonQuery();


                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MySQLMyISAMDBException("MySQL(MyISAM) Hiba történt az adat(ok) frissítése során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }
    }
}
