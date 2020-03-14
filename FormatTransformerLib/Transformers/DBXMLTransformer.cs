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
            var inputDataSet = input as DataSet;
            inputDataSet.Tables["text"].Columns.Remove("corpus_Id");
            var dataSet = new DataSet("OpenCorpora");
            dataSet.ReadXmlSchema(rule as string);
            foreach(DataTable t in inputDataSet.Tables)
            {
                foreach(DataRow r in t.Rows)
                {
                    dataSet.Tables[t.TableName].Rows.Add(r.ItemArray);
                }
            }
            
            //dataSet.ReadXmlSchema(rule as string);
            //dataSet.WriteXmlSchema("schema");
            //dataSet.WriteXmlSchema("schema");
            //dataSet.ReadXmlSchema("rule1.xsd");
            dataSet.WriteXml(output as string);
            return output;
        }
    }
}
