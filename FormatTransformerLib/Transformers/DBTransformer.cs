using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FormatTransformerLib.Transformers
{
    public class DBTransformer : ITransform
    {
        public void Transform(object input, object rule, object output)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            dataSet.ReadXmlSchema((string)rule);
            dataSet.ReadXml((string)input);
            //var o = dataSet.Tables[0];
            foreach(DataTable o in dataSet.Tables)
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
            }
        }
    }
}
