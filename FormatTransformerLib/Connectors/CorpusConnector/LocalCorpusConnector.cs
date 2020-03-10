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
                        Info = f.FullName,
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

        public List<ICorpora> GetCorpora()
        {
            return corpora.GetCorpora();
        }

        public void AddFile(Corpus corpus, string fileName)
        {
            var path = connectorString + @"CorporaStore\" + corpus.Title + @"\" + Path.GetFileName(fileName);
            corpus.Add(new TextFile() { Title = Path.GetFileName(fileName), Info = path });
            FileInfo fileInfo = new FileInfo(fileName);
            fileInfo.CopyTo(path, true);
            //fileInfo.Delete();
        }

        public void AddFile(ICorpora corpus, string fileName, string newName)
        {
            var path = connectorString + @"CorporaStore\" + corpus.Title + @"\" + newName;

            FileInfo fileInfo = new FileInfo(fileName);
            fileInfo.CopyTo(path, true);
            fileInfo.Delete();
        }

        public void RemoveCorpus(ICorpora corpus)
        {
            corpora.Delete(corpus);
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + @"CorporaStore\" + corpus.Title);
            directoryInfo.Delete(true);
        }

        public void EditCorpus(ICorpora corpus, string title)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + @"CorporaStore\" + corpus.Title);
            directoryInfo.MoveTo(connectorString + @"CorporaStore\" + title);
            corpus.Title = title;
        }

        public void AddCorpus(object corpus)
        {
            throw new NotImplementedException();
        }

        public void RemoveFile(ICorpora corpus, ICorpora file)
        {
            var fileInfo = new FileInfo(file.Info);
            fileInfo.Delete();
            corpus.Delete(file);
        }

        public void EditFile(ICorpora corpus, ICorpora file, string textInfo)
        {
            var path = connectorString + @"CorporaStore\" + corpus.Title + @"\" + file.Title;
            var newPath = connectorString + @"CorporaStore\" + corpus.Title + @"\" + textInfo;

            var fileInfo = new FileInfo(path);
            fileInfo.MoveTo(newPath);

            file.Title = textInfo;
            file.Info = newPath;
        }
    }
}
