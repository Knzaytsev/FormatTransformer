using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class CorpusManager
    {
        protected ICorpusConnector connector;
        protected Corpus corpora;

        public void ConnectCorpus(ICorpusConnector connector)
        {
            this.connector = connector;
            connector.Connect();
            corpora = connector.GetCorpora();
        }

        public void AddCorpus(object corpus)
        {
            corpora.Add(connector.AddCorpus(corpus));
        }

        public void AddCorpus(string title)
        {
            corpora.Add(connector.AddCorpus(title));
        }

        public Corpus GetCorpora()
        {
            return corpora;
        }

        // TODO: возможно стоит добавить возвращаемый объект методу AddFile.
        public void AddFile(Corpus corpus, string fileName)
        {
            corpus.Add(connector.AddFile(corpus.Title, fileName));
        }

        public void RemoveCorpus(ICorpora corpus)
        {
            connector.RemoveCorpus(corpus.Info);
            corpora.Delete(corpus);
        }

        // TODO: добавить файлы в новый корпус
        public void EditCorpus(ICorpora corpus, string title)
        {
            var editedCorpus = connector.EditCorpus(corpus.Info, title);
            corpora.Delete(corpus);
            corpora.Add(editedCorpus);
        }

        public void RemoveFile(Corpus corpus, ICorpora file)
        {
            connector.RemoveFile(file.Info);
            corpus.Delete(file);
        }

        public void EditFile(ICorpora corpus, ICorpora file, string textInfo)
        {
            var editedFile = connector.EditFile(file.Info, textInfo);
            corpus.Delete(file);
            corpus.Add(editedFile);
        }

        public void AddFile(object file)
        {
            connector.AddFile(file, "aaa");
        }
    }
}
