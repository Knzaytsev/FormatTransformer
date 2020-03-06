using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace FormatTransformerLib.Transformers
{
    public class DBTransformer : ITransformer
    {
        public void Transform(object input, object rule, object output)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            dataSet.ReadXmlSchema((string)rule);
            dataSet.ReadXml((string)input, XmlReadMode.ReadSchema);
            string connectionStr = @"Data Source=LAPTOP-6UGN0SO3\SQLEXPRESS01;Initial Catalog=OpenCorpora;Integrated Security=True";

            using (var connect = new SqlConnection(connectionStr))
            {
                connect.Open();

                foreach(DataTable t in dataSet.Tables)
                {
                    new SqlCommand(string.Format("SET IDENTITY_INSERT {0} ON;", t.TableName), connect).ExecuteNonQuery();
                    foreach(DataRow r in t.Rows)
                    {
                        var values = "";
                        var columns = "";
                        foreach(DataColumn c in t.Columns)
                        {
                            columns += c.ColumnName + ",";
                            if (r[c].ToString().Contains("\"") || r[c].ToString().Contains("'"))
                            {
                                r[c] = r[c].ToString().Replace("'", "");
                            }
                            values += "'" + r[c] + "',";
                        }
                        columns = columns.Remove(columns.Length - 1);
                        values = values.Remove(values.Length - 1);
                        var cmd = string.Format("insert into {0} ({1}) values ({2})", t.TableName, columns, values);
                        var command = new SqlCommand(cmd, connect);
                        command.ExecuteNonQuery();
                    }
                    new SqlCommand(string.Format("SET IDENTITY_INSERT {0} OFF;", t.TableName), connect).ExecuteNonQuery();
                }
            }


            //var o = dataSet.Tables[0];
            /*foreach(DataTable o in dataSet.Tables)
            {
                int cnt = 0;
                Console.WriteLine(o.TableName + "\n");
                string row = "";
                foreach (DataColumn t in o.Columns)
                {
                    row += t.ColumnName + "\t";
                    //Console.WriteLine(t.ColumnName + "\t");
                }
                row += "\n";
                Console.WriteLine(row);
                //Console.WriteLine("\n");
                row = "";
                foreach (DataRow r in o.Rows)
                {
                    foreach (var d in r.ItemArray)
                    {
                        row += d + "\t";
                        //Console.WriteLine(d + "\t");
                    }
                    row += "\n";
                    Console.WriteLine(row);
                    row = "";
                    cnt++;
                    if (cnt == 10)
                        break;
                    //Console.WriteLine("\n");
                }
                Console.WriteLine("\n\n");
            }*/
        }
    }
}
