using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextToSQL
{
    class Program
    {
        const string TABLE_SOURCE_FILE = "SourceTables.txt";
        const string SQL_OUTPUT_FILE = "Schema.sql";
        const string TABLE_DESIGNATOR = "*";
        const string PAD4 = "    ";
        
        private static Dictionary<string, string> dictFieldTamplates =
            new Dictionary<string, string> 
            { 
                {"Id", "[{0}] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY"}, 
                {"int", "[{0}] [int] NULL"}, 
                {"Quantity", "[{0}] [int] NULL"},
            };

        private static List<TableSpec> listTableSpec = new List<TableSpec>();
        
        static void Main(string[] args)
        {
            ReadFile();

            using (StreamWriter writer = new StreamWriter(SQL_OUTPUT_FILE))
            {
                foreach (var tableSpec in listTableSpec)
                    writer.Write(BuildCreateTableSQL(tableSpec));

                foreach (var tableSpec in listTableSpec)
                    writer.Write(BuildAlterTableSQL(tableSpec));
            }
        }

        private static string BuildCreateTableSQL(TableSpec tableSpec)
        {
            const string FIELD_DEFAULT_TEMPLATE = "[{0}] [nvarchar](max) NULL";
            const string TABLE_CREATE_TEMPLATE = "CREATE TABLE [dbo].[{0}](";

            var listFields = new List<string>();
            var tableName = tableSpec.TableName;

            foreach (var field in tableSpec.ListFields) 
            {
                var fieldKey = (field.ToLower().EndsWith("id") && field.Length > 2) ? "int" : field;

                if (dictFieldTamplates.ContainsKey(fieldKey))
                    listFields.Add(string.Format(PAD4 + dictFieldTamplates[fieldKey], field));
                else 
                    listFields.Add(string.Format(PAD4 + FIELD_DEFAULT_TEMPLATE, field));
            }

            var sql = new StringBuilder(string.Format(TABLE_CREATE_TEMPLATE, tableName));
            
            sql.AppendLine();
            sql.AppendLine(String.Join("," + Environment.NewLine, listFields.ToArray()));
            sql.AppendLine(")");
            sql.AppendLine("GO");
            sql.AppendLine();

            return sql.ToString();
        }

        private static string BuildAlterTableSQL(TableSpec tableSpec)
        {
            const string TABLE_ALTER_FOREIGN = 
                "ALTER TABLE [dbo].[{0}] ADD FOREIGN KEY ({1}Id) REFERENCES [dbo].[{2}] (Id)";

            var sql = new StringBuilder();

            foreach (var foreignKey in tableSpec.ListForeignKeys)
            {
                var foreignTable = foreignKey + "s";

                sql.AppendFormat(TABLE_ALTER_FOREIGN, tableSpec.TableName, foreignKey, foreignTable);

                sql.AppendLine();
                sql.AppendLine("GO");
                sql.AppendLine();
            }

            return sql.ToString();
        }

        private static void ReadFile()
        {
            string lastTable = string.Empty;

            using (StreamReader reader = new StreamReader(TABLE_SOURCE_FILE))
            {
                string line = string.Empty;
                var tableName = string.Empty;
                TableSpec tableSpec = null;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(TABLE_DESIGNATOR))
                    {
                        tableName = line.Trim().Replace(TABLE_DESIGNATOR, "");
                        tableSpec = new TableSpec(tableName);
                        listTableSpec.Add(tableSpec);
                    }
                    else if (line.Length > 0)
                    {
                        var fieldName = line.Trim();
                        tableSpec.ListFields.Add(fieldName);

                        if (fieldName.ToLower().EndsWith("id") && fieldName.ToLower() != "id")
                            tableSpec.ListForeignKeys.Add(fieldName.Substring(0, fieldName.Length - 2));
                    }
                }
            }
        }
    }
}
