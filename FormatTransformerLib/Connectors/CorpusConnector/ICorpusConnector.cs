using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public interface ICorpusConnector
    {
        public void Connect();

        public List<ICorpora> GetCorpora();

        public void AddCorpus(object corpus);

        public void AddCorpus(string title);
        void AddFile(Icorpora corpus, string fileName);
        void AddFile(ICorpora corpus, string fileName, string newName);
        void RemoveCorpus(ICorpora corpus);
        void EditCorpus(ICorpora corpus, string title);
    }
}
