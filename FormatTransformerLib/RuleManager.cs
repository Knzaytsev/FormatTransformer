using FormatTransformerLib.Connectors.RuleConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class RuleManager
    {
        private IRuleConnect connect;
        private List<Rule> rules;

        public void ConnectRules(IRuleConnect connect)
        {
            this.connect = connect;
            connect.Connect();
            rules = connect.GetRules();
        }

        public void AddRule(string fileName)
        {
            connect.AddRule(fileName);
        }

        public void DeleteRule(Rule rule)
        {
            connect.DeleteRule(rule);
        }

        public void RenameRule(Rule rule, string newName)
        {
            connect.RenameRule(rule, newName);
        }

        public List<Rule> GetRule()
        {
            return rules;
        }
    }
}
