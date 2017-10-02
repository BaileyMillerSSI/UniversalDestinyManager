using DestinyManifestViewer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DestinyManifestViewer
{
    class Program
    {
        static SQLiteConnection _Connection;

        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            //Starts the connection to the database
            StartConnection();

            //Gets all the names of the tables
            var tableNames = ListTableNames(false);

            //Converts that table list to a que, so that they can be processed
            var tablesToDump = ToQue<String>(tableNames);
            
            //All the available tables and their data dump
            var EnemyRaces = ConvertTableData<EnemyRaceDefinition>(DumpTableData(tablesToDump.Dequeue(), false)).Map(x=>x.ProcessDisplayData()).ToList();
            var Places = ConvertTableData<PlaceDefinition>(DumpTableData(tablesToDump.Dequeue(), false));
            var Activities = ConvertTableData<DestinyActivityDefinition>(DumpTableData(tablesToDump.Dequeue(), false));
            var ActivityTypes = ConvertTableData<DestinyActivityTypeDefinition>(DumpTableData(tablesToDump.Dequeue(), false));
            var Classes = ConvertTableData<ClassDefinitions>(DumpTableData(tablesToDump.Dequeue(), false));
            var Genders = ConvertTableData<GenderDefinition>(DumpTableData(tablesToDump.Dequeue(), false));
            var InventoryBucketDefinitions = ConvertTableData<InventoryBucketDefinition>(DumpTableData(tablesToDump.Dequeue(), false));

            timer.Stop();

            Console.WriteLine($"Processing took: {timer.Elapsed.TotalSeconds} seconds");

            PrintEndingStatement();
        }

        private static Queue<T> ToQue<T>(List<T> tableNames)
        {
            var q = new Queue<T>();
            foreach (var item in tableNames)
            {
                q.Enqueue(item);
            }
            return q;
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

    public static class MapFunction
    {
        public static IEnumerable<T> Map<T>(this IEnumerable<T> me, Action<T> worker)
        {
            foreach (var item in me)
            {
                worker(item);
            }

            return me;
        }
    }
}
