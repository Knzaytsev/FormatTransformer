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

        public DBConnector(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddCorpus(object corpus)
        {
            var dataSet = corpus as DataSet;
            using (connection)
            {
                connection.Open();

                foreach (DataTable t in dataSet.Tables)
                {
                    new SqlCommand(string.Format("SET IDENTITY_INSERT {0} ON;", t.TableName), connection).ExecuteNonQuery();
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
                    new SqlCommand(string.Format("SET IDENTITY_INSERT {0} OFF;", t.TableName), connection).ExecuteNonQuery();
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
            throw new NotImplementedException();
        }

        public void RemoveCorpus(Corpus corpus)
        {
            throw new NotImplementedException();
        }
    }
}
