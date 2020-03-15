using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace FormatTransformerLib.Transformers
{
    public class FlatFileDB : ITransformer
    {
        public object Transform(object input, object rule, object output)
        {
            var inputFile = input as string;
            var outputDataTable = new DataTable("text");

            using (var reader = new StreamReader(inputFile))
            {
                var columns = reader.ReadLine().Split('\t');
                foreach(var c in columns)
                {
                    outputDataTable.Columns.Add(c);
                }
                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine().Split('\t');
                    outputDataTable.Rows.Add(row);
                }
            }

            return outputDataTable;
        }
    }
}
