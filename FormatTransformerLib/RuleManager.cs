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

        public void AddRule()
        {

        }

        public void DeleteRule()
        {

        }

        public void RenameRule()
        {

        }

        public void GetRule()
        {

        }
    }
}
