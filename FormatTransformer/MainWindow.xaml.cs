using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FormatTransformerLib;
using FormatTransformerLib.Connectors.CorpusConnector;
using FormatTransformerLib.Connectors.RuleConnector;

namespace FormatTransformer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CorpusManager corpusManager = new CorpusManager();
        private RuleManager ruleManager = new RuleManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void transformButton_Click(object sender, RoutedEventArgs e)
        {
            var transformer = new Transformer();
            transformer.Transform(new XMLTransformer());
        }

        private void loadCorpora_Click(object sender, RoutedEventArgs e)
        {
            corpusManager.ConnectCorpus(new LocalCorpusConnector());
        }

        private void loadRules_Click(object sender, RoutedEventArgs e)
        {
            ruleManager.ConnectRules(new LocalRuleConnector());
        }

        private void addCorpus_Click(object sender, RoutedEventArgs e)
        {
            corpusManager.AddCorpus("OtherCorpus");
        }
    }
}
