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
        private Icorpora corpus = new Icorpora() { Title = "Corpora" };

        public DBConnector()
        {

        }

        public DBConnector(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddCorpus(object corpus)
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

        public void AddFile(Icorpora corpus, string fileName)
        {
            throw new NotImplementedException();
        }

        public void AddFile(ICorpora corpus, string fileName, string newName)
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

        public List<ICorpora> GetCorpora()
        {
            //return corpus.GetCorpora();
            return new List<ICorpora>() { corpus };
        }

        public void RemoveCorpus(ICorpora corpus)
        {
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = string.Format("delete from text where text_Id = {0}", corpus.Info);
                var command = new SqlCommand(cmd, connection);
                command.ExecuteNonQuery();
            }

            this.corpus.Delete(corpus);
        }
    }
}
