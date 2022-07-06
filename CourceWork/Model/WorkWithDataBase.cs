using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace CourceWork.Model
{
    class WorkWithDataBase
    {
        private string ConnectString { get; set; }

        public WorkWithDataBase(string login, string password) : this(login, password, "localhost", "5432") { }
        public WorkWithDataBase(string login, string password, string host, string port)
        {
            ConnectString = $"Host={host};" +
                            $"Port={port};" +
                            "Database=courcework;" +
                            $"Username={login};" +
                            $"Password={password}";
        }

        public List<List<object>> Select(string columns, string tables)
        {
            List<List<object>> list = new List<List<object>>();

            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectString))
            {
                conn.Open();
                string textCommand = $"SELECT {columns} FROM {tables};";
                NpgsqlCommand command = new NpgsqlCommand(textCommand, conn);

                NpgsqlDataReader reader = command.ExecuteReader();
                NpgsqlDataAdapter adapter;
                while (reader.Read())
                {
                    List<object> listTemp = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        listTemp.Add(reader[i]);
                    }
                    list.Add(listTemp);
                }

                conn.Close();
            }

            return list;
        }

        public DataSet SelectAdapter(string columns, string tables)
        {
            DataSet ds = new DataSet();
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectString))
            {
                conn.Open();
                string textCommand = $"SELECT {columns} FROM {tables};";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(textCommand, conn);
                adapter.Fill(ds);
            }

            return ds;
        }

        public DataSet SelectAdapter(string columns, string tables, string where)
        {
            DataSet ds = new DataSet();
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectString))
            {
                conn.Open();
                string textCommand = $"SELECT {columns} FROM {tables} WHERE {where};";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(textCommand, conn);
                adapter.Fill(ds);
            }

            return ds;
        }

        public List<List<object>> Select(string columns, string tables, string where)
        {
            List<List<object>> list = new List<List<object>>();

            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectString))
            {
                conn.Open();
                string textCommand = $"SELECT {columns} FROM {tables} WHERE {where};";
                NpgsqlCommand command = new NpgsqlCommand(textCommand, conn);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    List<object> listTemp = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        listTemp.Add(reader[i]);
                    }
                    list.Add(listTemp);
                }

                conn.Close();
            }

            return list;
        }

        public void Insert(string table, string fields, string values)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectString))
            {
                conn.Open();

                string textCommand = $"INSERT INTO {table} ({fields}) VALUES ({values});";
                NpgsqlCommand command = new NpgsqlCommand(textCommand, conn);
                command.ExecuteNonQuery();

                conn.Close();
            }
        }

        public object InsertRet(string table, string fields, string values, string retField)
        {
            object ret;
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectString))
            {
                conn.Open();

                string textCommand = $"INSERT INTO {table} ({fields}) VALUES ({values}) RETURNING {retField};";
                NpgsqlCommand command = new NpgsqlCommand(textCommand, conn);
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                ret = reader[0];

                conn.Close();
            }

            return ret;
        }

        public void Update(string table, string field, string value, string where)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectString))
            {
                conn.Open();

                string textCommand = $"UPDATE {table} SET {field} = {value} WHERE {where};";
                NpgsqlCommand command = new NpgsqlCommand(textCommand, conn);
                command.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}
