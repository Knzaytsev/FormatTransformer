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
        private Corpus corpus = new Corpus() { Title = "Corpora" };

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

        public void AddFile(Corpus corpus, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            connection = new SqlConnection(connectionString);
        }

        public void EditCorpus(ICorpora corpus, string title)
        {
            throw new NotImplementedException();
        }

        public List<ICorpora> GetCorpora()
        {
            return corpus.GetCorpora();
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
