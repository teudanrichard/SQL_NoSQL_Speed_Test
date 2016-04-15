using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLTestApplication.Statistic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SQLTestApplication.SQLDatabase
{
    public class SQLTestClass
    {
        private static DataSet DataSetObject = new DataSet();
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
        private static SqlConnection SqlConnectionObject = new SqlConnection(ConnectionString);
        private static SqlDataAdapter SqlDataAdapterObject = new SqlDataAdapter();


        public SQLStatistic selectAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Read", Types.SQLActions.Olvasás, Types.SQLType.SQL);
            stat.Time.Start();
            DataObject obj = new DataObject();
            //------------------------------------------------------------------------------------------------------------
            SqlDataReader dataReader = null;
            string query = "SELECT * FROM test";
            SqlConnectionObject.Open();
            SqlDataAdapterObject.SelectCommand = new SqlCommand(query, SqlConnectionObject);
            dataReader = SqlDataAdapterObject.SelectCommand.ExecuteReader();
            string result = "";
            while (dataReader.Read())
            {
                result += dataReader["ID"]+","+dataReader["Name"]+","+ dataReader["NeptunCode"]+"\n";
            }
            SqlConnectionObject.Close();
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            return stat;
        }

        public SQLStatistic insertRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Insert", Types.SQLActions.Beszúrás, Types.SQLType.SQL);
            stat.Time.Start();
            DataObject obj = new DataObject();
            //------------------------------------------------------------------------------------------------------------
            for (int i=0;i< rows; i++) { 
                string query = "INSERT INTO test(Name,NeptunCode) VALUES (@Name,@NeptunCode)";
                SqlDataAdapterObject.InsertCommand = new SqlCommand(query, SqlConnectionObject);
                SqlDataAdapterObject.InsertCommand.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = "testUser"+i;
                SqlDataAdapterObject.InsertCommand.Parameters.Add("@NeptunCode", SqlDbType.VarChar, 30).Value = "B"+i;
                SqlConnectionObject.Open();
                SqlDataAdapterObject.InsertCommand.ExecuteNonQuery();
                SqlConnectionObject.Close();
            }
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            return stat;
        }

        public SQLStatistic deleteAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Delete", Types.SQLActions.Törlés, Types.SQLType.SQL);
            stat.Time.Start();
            DataObject obj = new DataObject();
            //------------------------------------------------------------------------------------------------------------
            string query = "DELETE FROM test";
            SqlDataAdapterObject.DeleteCommand = new SqlCommand(query, SqlConnectionObject);
            SqlConnectionObject.Open();
            SqlDataAdapterObject.DeleteCommand.ExecuteNonQuery();
            SqlConnectionObject.Close();
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            return stat;
        }

        public SQLStatistic updateRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Update", Types.SQLActions.Frissítés, Types.SQLType.SQL);
            stat.Time.Start();
            DataObject obj = new DataObject();
            //------------------------------------------------------------------------------------------------------------
            for (int i = 0; i < rows; i++)
            {
                string query = "UPDATE test SET NeptunCode=@code WHERE Name=@name";
                SqlDataAdapterObject.UpdateCommand = new SqlCommand(query, SqlConnectionObject);
                SqlDataAdapterObject.UpdateCommand.Parameters.Add("@name", SqlDbType.VarChar, 30).Value = "testUser"+i;
                SqlDataAdapterObject.UpdateCommand.Parameters.Add("@code", SqlDbType.VarChar, 30).Value = "HS8GZ9";
                SqlConnectionObject.Open();
                SqlDataAdapterObject.UpdateCommand.ExecuteNonQuery();

                SqlConnectionObject.Close();
            }
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            return stat;
        }

    }
}
