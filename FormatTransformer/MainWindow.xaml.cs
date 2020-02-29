using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;

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
            var corpora = corpusManager.GetCorpora();
            var collectionCorpora = new ObservableCollection<ICorpora>();
            foreach(var c in corpora)
            {
                collectionCorpora.Add(c);
            }
            listCorpora.ItemsSource = collectionCorpora;
        }

        private void loadRules_Click(object sender, RoutedEventArgs e)
        {
            ruleManager.ConnectRules(new LocalRuleConnector());
        }

        private void addCorpus_Click(object sender, RoutedEventArgs e)
        {
            corpusManager.AddCorpus("AnotherCorpus");
        }

        private void listCorpora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            var textFiles = new ObservableCollection<ICorpora>();
            foreach(var f in corpus.GetCorpora())
            {
                textFiles.Add(f);
            }
            listFiles.ItemsSource = textFiles;
        }

        private void listFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addFile_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            var opf = new OpenFileDialog();
            if (opf.ShowDialog() == true)
            {
                corpusManager.AddFile(corpus, opf.FileName);
            }
        }

        private void deleteCorpus_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            corpusManager.RemoveCorpus(corpus);
        }

        private void editCorpus_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            corpusManager.EditCorpus(corpus, "SomeCorpus");
        }
    }
}
