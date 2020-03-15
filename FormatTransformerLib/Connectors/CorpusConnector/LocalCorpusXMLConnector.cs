using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public class LocalCorpusXMLConnector : ICorpusConnector
    {
        private string connectorString = @"..\..\..\CorporaStore\";
        //private Corpus corpora = new Corpus();

        public LocalCorpusXMLConnector()
        {

        }

        public LocalCorpusXMLConnector(string connectorString)
        {
            this.connectorString = connectorString;
        }

        public Corpus AddCorpus(object corpus)
        {
            var title = corpus as string;
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + title);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            return new Corpus()
            {
                Title = title,
                Info = connectorString + title
            };
        }

        public TextFile AddFile(object corpus, string fileName)
        {
            var titleCorpus = corpus as string;
            var path = connectorString + titleCorpus + @"\" + Path.GetFileName(fileName);

            FileInfo fileInfo = new FileInfo(fileName);
            fileInfo.CopyTo(path, true);

            return new TextFile()
            {
                Title = Path.GetFileName(fileName),
                Info = path
            };
        }

        public void Connect()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public Corpus EditCorpus(object corpus, string newName)
        {
            var corpusTitle = corpus as string;

            DirectoryInfo directoryInfo = new DirectoryInfo(corpusTitle);
            directoryInfo.MoveTo(connectorString + newName);

            return new Corpus()
            {
                Title = newName,
                Info = connectorString + newName
            };
        }

        public TextFile EditFile(object file, string newName)
        {
            var fileName = file as string;

            var fileInfo = new FileInfo(fileName);
            var corpus = Path.GetDirectoryName(fileName);
            var newPath = corpus + @"\" + newName;
            fileInfo.MoveTo(newPath);

            return new TextFile()
            {
                Title = newName,
                Info = newPath
            };
        }

        public Corpus GetCorpora()
        {
            var corpora = new Corpus()
            {
                Title = "CorporaStore",
                Info = connectorString
            };
            var directoryInfo = new DirectoryInfo(connectorString);
            foreach (var d in directoryInfo.GetDirectories())
            {
                var corpus = new Corpus()
                {
                    Title = d.Name,
                    Info = connectorString + d.Name
                };
                foreach (var f in d.GetFiles())
                {
                    var file = new TextFile()
                    {
                        Title = f.Name,
                        Info = connectorString + d.Name + @"\" +  f.Name
                    };
                    corpus.Add(file);
                }
                corpora.Add(corpus);
            }
            return corpora;
        }

        public void RemoveCorpus(object corpus)
        {
            var corpusName = corpus as string;
            //var directoryInfo = new DirectoryInfo(connectorString + Path.GetDirectoryName(corpusName));
            var directoryInfo = new DirectoryInfo(corpusName);
            directoryInfo.Delete(true);
        }

        public void RemoveFile(object file)
        {
            var fileName = file as string;
            var fileInfo = new FileInfo(fileName);
            fileInfo.Delete();
        }

        /// <summary>
        /// Подключение к корпусу на локальном диске.
        /// Если корпуса не существует, то создать,
        /// иначе считать все файлы, находящиеся в нём.
        /// </summary>
        /*public void Connect()
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
        }*/
    }
}
