using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public interface ICorpusConnector
    {
        public void Connect();
        public Corpus GetCorpora();
        public Corpus AddCorpus(object corpus);
        public void RemoveCorpus(object corpus);
        public Corpus EditCorpus(object corpus, string newName);
        public TextFile AddFile(object corpus, string fileName);
        public void RemoveFile(object file);
        public TextFile EditFile(object file, string newName);
        /*public void Connect();

        public List<ICorpora> GetCorpora();

        public void AddCorpus(object corpus);

        public void AddCorpus(string title);
        void AddFile(Corpus corpus, string fileName);
        void AddFile(ICorpora corpus, string fileName, string newName);
        void RemoveCorpus(ICorpora corpus);
        void EditCorpus(ICorpora corpus, string title);
        void RemoveFile(ICorpora corpus, ICorpora file);
        void EditFile(ICorpora corpus, ICorpora file, string textInfo);*/
    }
}
