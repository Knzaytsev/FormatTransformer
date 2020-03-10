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
        void AddFile(Corpus corpus, string fileName);
        void AddFile(ICorpora corpus, string fileName, string newName);
        void RemoveCorpus(ICorpora corpus);
        void EditCorpus(ICorpora corpus, string title);
        void RemoveFile(ICorpora corpus, ICorpora file);
        void EditFile(ICorpora corpus, ICorpora file, string textInfo);
    }
}
