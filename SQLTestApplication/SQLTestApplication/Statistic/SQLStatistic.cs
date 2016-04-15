using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Statistic
{
    public class SQLStatistic
    {

        private string testName;
        private List<DataObject> datas = new List<DataObject>();
        private ExecutionTime time = new ExecutionTime();
        private Types.SQLType type;
        private Types.SQLActions actions;

        internal ExecutionTime Time
        {
            get
            { return time; }
        }

        public Types.SQLType getSQLType
        {
            get { return type; }
        }

        public SQLStatistic(string testName, Types.SQLActions actions, Types.SQLType type)
        {
            this.testName = testName;
            this.actions = actions;
            this.type = type;
        }

        public void addDataObject(DataObject data)
        {
            datas.Add(data);
        }
        public override string ToString()
        {
            string str = "";
            if(datas != null)
                for(int i = 0; i < datas.Count; i++)
                    {
                        str += (i+1)+". Adat\nID: "+datas[i].ID+"\nNeve: "+datas[i].Name+"\nKód: "+datas[i].NeptunCode+"\n";
                    }
            str += "\n\tTeszt neve: " + testName + "\n\tMűvelet(ek): " + actions + " \n\tAdatbázis típusa: " + type + "\n\tFutásidő: " + Time.getExecutionTime() + " sec";
            return str;
        }
    }
}
