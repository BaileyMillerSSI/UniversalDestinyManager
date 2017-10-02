using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyManifestViewer
{
    class Program
    {
        static SQLiteConnection _Connection;

        static void Main(string[] args)
        {
            //To-Do display all the tables names inside the database
            StartConnection();

            var tableNames = ListTableNames(false);

            var EnemyRacesRaw = DumpTableData(tableNames.First(), false);

            var EnemyRaces = ConvertTableData<EnemyRaceDefinition>(EnemyRacesRaw);

            PrintEndingStatement();
        }




        private static List<T> ConvertTableData<T>(List<string> tableDump)
        {
            var newData = new List<T>();

            foreach (var item in tableDump)
            {
                newData.Add(JsonConvert.DeserializeObject<T>(item));
            }

            return newData;
        }

        private static List<String> DumpTableData(string tableName, bool printData = true)
        {
            var keyData = new List<String>();
            if (_Connection != null && _Connection.State == System.Data.ConnectionState.Open)
            {
                
                var query = new SQLiteCommand($"select * from {tableName};", _Connection);
                try
                {
                    var reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        if (printData)
                        {
                            Console.WriteLine($"Column: {reader.GetName(0)} -- Data: {reader.GetString(1)}");
                        }
                        keyData.Add(reader.GetString(1));
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return keyData;
        }

        private static void ListTableColumns(string tableName)
        {
            if (_Connection != null && _Connection.State == System.Data.ConnectionState.Open)
            {
                var query = new SQLiteCommand($"select * from {tableName};", _Connection);
                try
                {
                    var reader = query.ExecuteReader();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"Column: {reader.GetName(i)}");
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private static List<String> ListTableNames(bool printData = true)
        {
            if (_Connection != null && _Connection.State == System.Data.ConnectionState.Open)
            {
                var names = new List<String>();
                //Print all the table names
                const string GET_TABLES_QUERY = "SELECT NAME from sqlite_master";
                var getTablesQuery = new SQLiteCommand(GET_TABLES_QUERY, _Connection);
                var dataReader = getTablesQuery.ExecuteReader();
                while (dataReader.Read())
                {
                    if (printData)
                    {
                        Console.WriteLine($"Table Name: {dataReader["name"]}");
                        ListTableColumns(dataReader["name"].ToString());
                    }
                    names.Add(dataReader["name"].ToString());
                }
                return names;
            }
            return null;
        }

        private static bool StartConnection()
        {
            _Connection = new SQLiteConnection("Data Source=Manifest.sqlite;Version=3;");
            _Connection.Open();
            return _Connection.State == System.Data.ConnectionState.Open;
        }

        private static void PrintEndingStatement()
        {
            Console.WriteLine("Press any key to exit.");
            _Connection.Close();
            Console.ReadLine();
        }
    }
}
