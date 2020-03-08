using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.RuleConnector
{
    public interface IRuleConnect
    {
        public void Connect();

        public void AddRule(string fileName);

        public void DeleteRule(Rule rule);

        public void RenameRule(Rule rule, string newName);

        public List<Rule> GetRules();
    }
}
