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

        public DataSet GetDataSet()
        {
            var dataSet = new DataSet("OpenCorpora");

            using(connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd =
                    "select * from text;" +
                    "select * from paragraph;" +
                    "select * from sentence;" +
                    "select * from token;" +
                    "select * from tfr;" +
                    "select * from v;" +
                    "select * from l;" +
                    "select * from g;";

                var adapter = new SqlDataAdapter(cmd, connection);
                adapter.TableMappings.Add("Table", "text");
                adapter.TableMappings.Add("Table1", "paragraph");
                adapter.TableMappings.Add("Table2", "sentence");
                adapter.TableMappings.Add("Table3", "token");
                adapter.TableMappings.Add("Table4", "tfr");
                adapter.TableMappings.Add("Table5", "v");
                adapter.TableMappings.Add("Table6", "l");
                adapter.TableMappings.Add("Table7", "g");

                adapter.Fill(dataSet);

                /*var text = dataSet.Tables["text"];
                var paragraph = dataSet.Tables["paragraph"];
                var sentence = dataSet.Tables["sentence"];
                var token = dataSet.Tables["token"];
                var tfr = dataSet.Tables["tfr"];
                var v = dataSet.Tables["v"];
                var l = dataSet.Tables["l"];
                var g = dataSet.Tables["g"];

                var tp = new DataRelation("text_paragraph", text.Columns["text_Id"], paragraph.Columns["text_Id"], true);
                var ps = new DataRelation("paragraph_sentence", paragraph.Columns["paragraph_Id"], sentence.Columns["paragraph_Id"], true);
                var st = new DataRelation("sentence_token", sentence.Columns["sentence_Id"], token.Columns["sentence_Id"], true);
                var tt = new DataRelation("token_tfr", token.Columns["token_Id"], tfr.Columns["token_Id"], true);
                var tv = new DataRelation("tfr_v", tfr.Columns["tfr_Id"], v.Columns["tfr_Id"], true);
                var vl = new DataRelation("v_l", v.Columns["v_Id"], l.Columns["v_Id"], true);
                var lg = new DataRelation("l_g", l.Columns["l_Id"], g.Columns["l_Id"], true);*/

                /*dataSet.Relations.AddRange(new DataRelation[]{
                tp, ps, st, tt, tv, vl, lg }
                );*/
            }

            return dataSet;
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
