using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class DBCorpusManager : CorpusManager
    {
        public object GetDataSetFromDB()
        {
            return (connector as DBConnector).GetDataSet();
        }
    }
}
