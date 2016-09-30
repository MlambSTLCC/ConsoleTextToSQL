using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextToSQL
{
    public class TableSpec
    {
        public string TableName { get; set; }
        public List<string> ListFields { get; set; }
        public List<string> ListForeignKeys { get; set; }

        public TableSpec() { }

        public TableSpec(string tableName)
        {
            this.TableName = tableName;
            this.ListFields = new List<string>();
            this.ListForeignKeys = new List<string>();
        }
    }
}
