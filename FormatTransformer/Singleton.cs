using FormatTransformerLib;
using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformer
{
    public class Singleton
    {
        private static Singleton instance;

        public ICorpora Corpus { get; set; }
        public Rule Rule { get; set; }
        public ICorpusConnector ConnectorFrom { get; set; }
        public ICorpusConnector ConnectorTo { get; set; }

        public static Singleton GetInstance()
        {
            if (instance == null)
                return new Singleton();
            return instance;
        }
    }
}
