using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class CorpusManager
    {
        private ICorpusConnect connector;

        public void ConnectCorpus(ICorpusConnect connector)
        {
            this.connector = connector;
            connector.Connect();
        }

        public void AddCorpus(string title)
        {
            connector.AddCorpus(title);
        }
    }
}
