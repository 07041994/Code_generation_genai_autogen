using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccount.Utils
{
    public class DataBaseHelper
    {
        //public bool fetchValuesFromDB(string sql)
        //{
        //    SqlCommand comm = new SqlCommand();
        //    bool recordFound = false;
        //    Dictionary<string, object> objResults = new Dictionary<string, object>();

        //    comm.Connection = new SqlConnection("Data Source=" + TestContext.Parameters.Get("webDBServer") + ";  Trusted_Connection=Yes"); //Windows Authentication
        //    comm.CommandText = sql;
        //    comm.Connection.Open();
        //    Console.WriteLine("State: {0}", comm.Connection.State);
        //    Console.WriteLine("ConnectionTimeout: {0}", comm.Connection.ConnectionTimeout);
        //    SqlDataReader cursor = comm.ExecuteReader();
        //    var columns = new List<string>();

        //    for (int i = 0; i < cursor.FieldCount; i++)
        //    {
        //        columns.Add(cursor.GetName(i));
        //    }

        //    if (cursor.HasRows)
        //    {
        //        recordFound = true;
        //        while (cursor.Read())
        //        {
        //            for (int i = 0; i < columns.Count; i++)
        //            {
        //                if (!objResults.ContainsKey(cursor.GetName(i)))
        //                {
        //                    objResults.Add(cursor.GetName(i), cursor[columns[i]].ToString());
        //                }
        //            }
        //        }
        //    }
        //    comm.Connection.Close();
        //    return recordFound;
        //}

        public Dictionary<string, object> fetchValuesFromDB(string sql)
        {
            bool recordFound = false;
            Dictionary<string, object> objResults = new Dictionary<string, object>();

            using (SqlConnection connection = new SqlConnection("Data Source=" + TestContext.Parameters.Get("ServicingDBServer") + "; Trusted_Connection=Yes"))
            {
                using (SqlCommand comm = new SqlCommand(sql, connection))
                {
                    Console.WriteLine("Connection is going to be established");
                    connection.Open();
                    Console.WriteLine("State: {0}", connection.State);
                    Console.WriteLine("ConnectionTimeout: {0}", connection.ConnectionTimeout);

                    using (SqlDataReader cursor = comm.ExecuteReader())
                    {
                        var columns = new List<string>();
                        for (int i = 0; i < cursor.FieldCount; i++)
                        {
                            columns.Add(cursor.GetName(i));
                        }

                        if (cursor.HasRows)
                        {
                            recordFound = true;
                            while (cursor.Read())
                            {
                                foreach (var column in columns)
                                {
                                    if (!objResults.ContainsKey(column))
                                    {
                                        
                                        objResults.Add(column, cursor[column].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return objResults;
        }

    }
}
