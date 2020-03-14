using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FormatTransformerLib.Transformers
{
    public class DBXMLTransformer : ITransformer
    {
        public object Transform(object input, object rule, object output)
        {
            var dataSet = input as DataSet;
            dataSet.WriteXml(output as string);
            return output;
        }
    }
}
