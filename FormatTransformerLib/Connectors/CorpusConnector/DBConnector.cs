using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public class DBConnector : ICorpusConnector
    {
        private string connectionString = "";
        private SqlConnection connection;
        //private Corpus corpus = new Corpus() { Title = "Corpora" };

        public DBConnector()
        {

        }

        public DBConnector(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Corpus AddCorpus(object corpus)
        {
            using(connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var dataSet = corpus as DataSet;
                var done = false;
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataTable t in dataSet.Tables)
                    {
                        try
                        {
                            new SqlCommand(string.Format("SET IDENTITY_INSERT {0} ON;", t.TableName), connection).ExecuteNonQuery();
                        }
                        catch { }

                        var valuesCorpus = "";
                        var columnsCorpus = "";
                        if (!done)
                        {
                            columnsCorpus += "corpus_Id,";
                            valuesCorpus += "1,";
                            done = true;
                        }
                        foreach (DataRow r in t.Rows)
                        {
                            var values = valuesCorpus;
                            var columns = columnsCorpus;
                            foreach (DataColumn c in t.Columns)
                            {
                                columns += c.ColumnName + ",";
                                /*if (r[c].ToString().Contains("\"") || r[c].ToString().Contains("'"))
                                {
                                    r[c] = r[c].ToString().Replace("'", "");
                                }*/
                                values += "'" + r[c] + "',";
                            }
                            columns = columns.Remove(columns.Length - 1);
                            values = values.Remove(values.Length - 1);
                            var cmd = string.Format("insert into {0} ({1}) values ({2})", t.TableName, columns, values);
                            var command = new SqlCommand(cmd, connection);
                            command.ExecuteNonQuery();
                        }
                        try
                        {
                            new SqlCommand(string.Format("SET IDENTITY_INSERT {0} OFF;", t.TableName), connection).ExecuteNonQuery();
                        }
                        catch { }
                    }
                }
            }
            return GetCorpora();
        }

        public TextFile AddFile(object corpus, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
            }
        }

        public Corpus EditCorpus(object corpus, string newName)
        {
            var corpusId = corpus as string;

            using(connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = string.Format("update corpora set name = {0} where corpus_Id = {1}", "'" + newName + "'", corpusId);
                var command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
            }

            return new Corpus()
            {
                Title = newName,
                Info = corpusId
            };
        }

        public TextFile EditFile(object file, string newName)
        {
            var fileInfo = file as string;
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(string.Format("update text set name = {0} where text_Id = {1}",
                    "'" + newName + "'", fileInfo), connection);
                command.ExecuteNonQuery();
            }

            return new TextFile()
            {
                Title = newName,
                Info = fileInfo
            };
        }

        public Corpus GetCorpora()
        {
            var corpora = new Corpus()
            {
                Title = "CorporaStore",
                Info = "0"
            };

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("select * from corpora", connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader["corpus_Id"].ToString();
                        var title = reader["name"].ToString();

                        var corpus = new Corpus()
                        {
                            Title = title.ToString(),
                            Info = id.ToString()
                        };
                        var commandFiles = new SqlCommand(string.Format("select * from text where corpus_Id = {0}", id),
                            connection);
                        var readerFiles = commandFiles.ExecuteReader();

                        if (readerFiles.HasRows)
                        {
                            while (readerFiles.Read())
                            {
                                id = readerFiles["text_Id"].ToString();
                                title = readerFiles["name"].ToString();

                                var file = new TextFile()
                                {
                                    Title = title,
                                    Info = id
                                };
                                corpus.Add(file);
                            }
                        }
                        corpora.Add(corpus);
                    }
                }
            }
            return corpora;
        }

        public void RemoveCorpus(object corpus)
        {
            var corpusId = corpus as string;

            using(connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = string.Format("delete from corpora where corpus_Id = {0}", corpusId);
                var command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveFile(object file)
        {
            var fileId = file as string;

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = string.Format("delete from text where text_Id = {0}", fileId);
                var command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
            }
        }

        /*public void AddCorpus(object corpus)
        {
            var dataSet = corpus as DataSet;
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataTable t in dataSet.Tables)
                {
                    try
                    {
                        new SqlCommand(string.Format("SET IDENTITY_INSERT {0} ON;", t.TableName), connection).ExecuteNonQuery();
                    }
                    catch { }
                    foreach (DataRow r in t.Rows)
                    {
                        var values = "";
                        var columns = "";
                        foreach (DataColumn c in t.Columns)
                        {
                            columns += c.ColumnName + ",";
                            if (r[c].ToString().Contains("\"") || r[c].ToString().Contains("'"))
                            {
                                r[c] = r[c].ToString().Replace("'", "");
                            }
                            values += "'" + r[c] + "',";
                        }
                        columns = columns.Remove(columns.Length - 1);
                        values = values.Remove(values.Length - 1);
                        var cmd = string.Format("insert into {0} ({1}) values ({2})", t.TableName, columns, values);
                        var command = new SqlCommand(cmd, connection);
                        command.ExecuteNonQuery();
                    }
                    try
                    {
                        new SqlCommand(string.Format("SET IDENTITY_INSERT {0} OFF;", t.TableName), connection).ExecuteNonQuery();
                    }
                    catch { }
                }

                foreach (DataRow r in dataSet.Tables["text"].Rows)
                {
                    this.corpus.Add(new TextFile()
                    {
                        Title = r[dataSet.Tables["text"].Columns["name"]].ToString(),
                        Info = r[dataSet.Tables["text"].Columns["text_Id"]].ToString()
                    });
                }
            }
        }

        public void AddCorpus(string title)
        {
            throw new NotImplementedException();
        }

        public void AddFile(Corpus corpus, string fileName)
        {
            throw new NotImplementedException();
        }

        public void AddFile(ICorpora corpus, string fileName, string newName)
        {
            throw new NotImplementedException();
        }

        // TODO: изменить на подключение к корпусу, а не к текстам.
        public void Connect()
        {
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("select * from text", connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader["text_Id"].ToString();
                        var title = reader["name"].ToString();

                        corpus.Add(new TextFile() { Title = title, Info = id });
                    }
                }
            }
        }

        public void EditCorpus(ICorpora corpus, string title)
        {
            corpus.Title = title;
        }

        public void EditFile(ICorpora corpus, ICorpora file, string textInfo)
        {
            using(connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(string.Format("update text set name = {0} where text_Id = {1}",
                    "'" + textInfo + "'", file.Info), connection);
                command.ExecuteNonQuery();
            }

            file.Title = textInfo;
        }

        public List<ICorpora> GetCorpora()
        {
            //return corpus.GetCorpora();
            return new List<ICorpora>() { corpus };
        }


        //TODO: Изменить на удаление корпуса, а не удаление текста.
        public void RemoveCorpus(ICorpora corpus)
        {
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = string.Format("delete from text where text_Id = {0}", "'" + corpus.Info + "'");
                var command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
            }

            this.corpus.Delete(corpus);
        }

        public void RemoveFile(ICorpora corpus, ICorpora file)
        {
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = string.Format("delete from text where text_Id = {0}", "'" + corpus.Info + "'");
                var command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
            }

            corpus.Delete(file);
        }*/
    }
}
