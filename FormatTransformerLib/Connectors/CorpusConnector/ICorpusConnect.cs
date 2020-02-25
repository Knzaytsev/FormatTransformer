using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    interface ICorpusConnect
    {
        public void Connect(object connector);

        public ICorpora GetCorpora();
    }
}
