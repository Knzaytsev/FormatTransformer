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
            //var dataTable = input as DataTable;
            var dataSet = input as DataSet;
            var result = new FlatFile();

            result.CorpusTitle = "SomeCorpora";
            foreach (DataTable t in dataSet.Tables)
            {
                using (var file = File.CreateText(t.TableName as string))
                {
                    var columnNames = t.Columns.Cast<DataColumn>().Select(x => x.ColumnName);
                    file.Write(string.Join('\t', columnNames));
                    foreach (DataRow r in t.Rows)
                    {
                        file.WriteLine();
                        file.Write(string.Join('\t', r.ItemArray));
                    }
                }
            }
            result.FlatFiles.Add(dataSet.DataSetName, dataSet.Tables.Cast<DataTable>().Select(x => x.TableName).ToList());
            return result;
        }
    }
}
