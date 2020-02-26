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
                    Path = f.FullName
                };
                rules.Add(rule);
            }
        }

        public List<Rule> GetRules()
        {
            return rules;
        }
    }
}
