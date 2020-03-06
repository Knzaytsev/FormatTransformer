using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public class LocalCorpusConnector : ICorpusConnector
    {
        private string connectorString = @"..\..\..\";
        private Corpus corpora = new Corpus();

        public LocalCorpusConnector()
        {

        }

        public LocalCorpusConnector(string connectorString)
        {
            this.connectorString = connectorString;
        }

        /// <summary>
        /// Подключение к корпусу на локальном диске.
        /// Если корпуса не существует, то создать,
        /// иначе считать все файлы, находящиеся в нём.
        /// </summary>
        public void Connect()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + "CorporaStore");
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            foreach(var d in directoryInfo.GetDirectories())
            {
                Corpus corpus = new Corpus() { Title = d.Name };
                foreach (var f in d.GetFiles())
                {
                    var file = new TextFile()
                    {
                        Path = f.FullName,
                        Title = f.Name
                    };
                    corpus.Add(file);
                }
                corpora.Add(corpus);
            }
        }

        public void AddCorpus(string title)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + @"CorporaStore\" + title);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                corpora.Add(new Corpus() { Title = title });
            }
        }

        public void AddTextFile(ICorpora corpus, TextFile file)
        {
            corpus.Add(file);
        }

        public List<ICorpora> GetCorpora()
        {
            return corpora.GetCorpora();
        }

        public void AddFile(Corpus corpus, string fileName)
        {
            corpus.Add(new TextFile() { Title = Path.GetFileName(fileName), Path = fileName });
            FileInfo fileInfo = new FileInfo(fileName);
            fileInfo.CopyTo(connectorString + @"\CorporaStore\" + corpus.Title + @"\" + Path.GetFileName(fileName));
        }

        public void RemoveCorpus(Corpus corpus)
        {
            corpora.Delete(corpus);
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + @"\CorporaStore\" + corpus.Title);
            directoryInfo.Delete(true);
        }

        public void EditCorpus(ICorpora corpus, string title)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + @"\CorporaStore\" + corpus.Title);
            directoryInfo.MoveTo(connectorString + @"\CorporaStore\" + title);
            corpus.Title = title;
        }

        public void AddCorpus(object corpus)
        {
            throw new NotImplementedException();
        }
    }
}
