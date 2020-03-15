using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace FormatTransformerLib.Transformers
{
    public class DBFlatFile : ITransformer
    {
        public object Transform(object input, object rule, object output)
        {
            //var dataTable = (input as DataSet).Tables["text"];
            var dataTable = input as DataTable;
            using(var file = File.CreateText(output as string))
            {
                var columnNames = dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName);
                file.Write(string.Join('\t', columnNames));
                foreach (DataRow r in dataTable.Rows)
                {
                    file.WriteLine();
                    file.Write(string.Join('\t', r.ItemArray));
                }
            }
            return output;
        }
    }
}
