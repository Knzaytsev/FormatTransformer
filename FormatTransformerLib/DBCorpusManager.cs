using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FormatTransformerLib
{
    public class DBCorpusManager : CorpusManager
    {
        public object GetDataSetFromDB()
        {
            return (connector as DBConnector).GetDataSet();
        }

        public void AddFile(Corpus corpus, object file)
        {
            var dataTable = file as DataTable;
            
            foreach(DataRow r in dataTable.Rows)
            {
                corpus.Add(connector.AddFile(r, r["name"].ToString()));
            }
        }
    }
}
