using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FormatTransformerLib.Connectors.RuleConnector
{
    public class LocalRuleConnector : IRuleConnect
    {
        private string connectorString = @"..\..\..\";
        private List<Rule> rules = new List<Rule>();

        public LocalRuleConnector()
        {

        }

        public LocalRuleConnector(string connectorString)
        {
            this.connectorString = connectorString;
        }

        public void AddRule(string fileName)
        {
            var path = connectorString + @"RuleStore\" + Path.GetFileName(fileName);
            var fileInfo = new FileInfo(fileName);
            fileInfo.CopyTo(path, true);

            rules.Add(new Rule() { Title = Path.GetFileName(path), Info = path });
        }

        public void Connect()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + "RuleStore");
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            foreach (var f in directoryInfo.GetFiles())
            {
                var rule = new Rule()
                {
                    Title = f.Name,
                    Info = f.FullName
                };
                rules.Add(rule);
            }
        }

        public void DeleteRule(Rule rule)
        {
            var fileInfo = new FileInfo(rule.Info);
            fileInfo.Delete();

            rules.Remove(rule);
        }

        public List<Rule> GetRules()
        {
            return rules;
        }

        public void RenameRule(Rule rule, string newName)
        {
            var fileInfo = new FileInfo(rule.Info);
            fileInfo.MoveTo(connectorString + @"RuleStore\" + newName);

            rule.Title = newName;
        }
    }
}
