using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLTestApplication.Statistic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SQLTestApplication.NoSQLDatabase
{
    class NoSQLTestClass
    {


        public SQLStatistic deleteAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MongoDB Delete", Types.SQLActions.Törlés, Types.SQLType.NoSQL);
            stat.Time.Start();
            DataObject obj = new DataObject();
            //------------------------------------------------------------------------------------------------------------
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("Data");
            var collection = db.GetCollection<DataObject>("TestDatabase");

            collection.DeleteMany("{}");
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            return stat;
        }

        public SQLStatistic insertRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MongoDB Insert", Types.SQLActions.Beszúrás, Types.SQLType.NoSQL);
            stat.Time.Start();
            //------------------------------------------------------------------------------------------------------------
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("Data");
            var collection = db.GetCollection<DataObject>("TestDatabase");

            for(int i=0; i<rows; i++) {
                DataObject test = new DataObject();
                test.ID = i;
                test.Name = "testUser"+i;
                test.NeptunCode = "B"+i;
                collection.InsertOneAsync(test);
                //stat.addDataObject(test);
            }
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            return stat;
        }

        public async Task<SQLStatistic> selectAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MongoDB Read", Types.SQLActions.Olvasás, Types.SQLType.NoSQL);
            stat.Time.Start();
            DataObject obj = new DataObject();
            //------------------------------------------------------------------------------------------------------------
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("Data");
            var collection = db.GetCollection<BsonDocument>("TestDatabase");
            var filter = new BsonDocument();
            var count = 0;
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        //Console.Write(document.ToString() + "\n");
                        //count++;
                    }
                }
            }
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            
            return stat;
        }

        public async Task<SQLStatistic> updateRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MongoDB Update", Types.SQLActions.Frissítés, Types.SQLType.NoSQL);
            stat.Time.Start();
            DataObject obj = new DataObject();
            //------------------------------------------------------------------------------------------------------------
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("Data");
            var collection = db.GetCollection<BsonDocument>("TestDatabase");
            for(int i=0;i < rows; i++) { 
                var filter = Builders<BsonDocument>.Filter.Eq("Name", "testUser"+i);
                var update = Builders<BsonDocument>.Update
                    .Set("NeptunCode", "HS8GZ9");
                var result = await collection.UpdateOneAsync(filter, update);
            }
            //------------------------------------------------------------------------------------------------------------
            stat.Time.End();
            return stat;
        }
    }
}
