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
using FormatTransformerLib.Transformers;
using Microsoft.Win32;

namespace FormatTransformer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate void UpdateElementHandler();
        event UpdateElementHandler UpdateCorpus;
        event UpdateElementHandler UpdateFile;
        event UpdateElementHandler UpdateRule;

        private CorpusManager corpusManager = new CorpusManager();
        private RuleManager ruleManager = new RuleManager();

        public MainWindow()
        {
            InitializeComponent();
            UpdateCorpus += updateListCorpora;
            UpdateFile += updateListFiles;
            UpdateRule += updateRules;
        }

        private void updateListFiles()
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            if (corpus is null)
                return;
            var textFiles = new ObservableCollection<ICorpora>();
            foreach (var f in corpus.GetCorpora())
            {
                textFiles.Add(f);
            }
            listFiles.ItemsSource = textFiles;
        }

        private void updateListCorpora()
        {
            var corpora = corpusManager.GetCorpora();
            if (corpora is null)
                return;
            var collectionCorpora = new ObservableCollection<ICorpora>();
            foreach (var c in corpora.GetCorpora())
            {
                collectionCorpora.Add(c);
            }
            listCorpora.ItemsSource = collectionCorpora;
        }

        private void loadCorpora_Click(object sender, RoutedEventArgs e)
        {
            var form = new ConnectorForm();
            if (form.ShowDialog() == true)
            {
                corpusManager.ConnectCorpus(form.Connector);
                UpdateCorpus?.Invoke();
            }
        }

        private void addCorpus_Click(object sender, RoutedEventArgs e)
        {
            var form = new ChangeInfoForm("Название корпуса:", "Добавить");
            if(form.ShowDialog() == true)
            {
                corpusManager.AddCorpus(form.TextInfo);
                UpdateCorpus?.Invoke();
            }
        }

        private void listCorpora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFile?.Invoke();
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
                UpdateFile?.Invoke();
            }
        }

        private void deleteCorpus_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            corpusManager.RemoveCorpus(corpus);
            UpdateCorpus?.Invoke();
        }

        private void editCorpus_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            var form = new ChangeInfoForm("Название корпуса:", "Изменить");
            if(form.ShowDialog() == true)
            {
                corpusManager.EditCorpus(corpus, form.TextInfo);
                UpdateCorpus?.Invoke();
            }
        }

        private void deleteFile_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Corpus)listCorpora.SelectedItem;
            var file = (ICorpora)listFiles.SelectedItem;
            corpusManager.RemoveFile(corpus, file);
            UpdateFile?.Invoke();
        }

        private void editFile_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (ICorpora)listCorpora.SelectedItem;
            var file = (ICorpora)listFiles.SelectedItem;

            var form = new ChangeInfoForm("Название файла", "Изменить");
            if(form.ShowDialog() == true)
            {
                corpusManager.EditFile(corpus, file, form.TextInfo);
                UpdateFile?.Invoke();
            }
        }

        private void updateRules()
        {
            ruleManager = new RuleManager();
            ruleManager.ConnectRules(new LocalRuleConnector());

            var ruleFiles = new ObservableCollection<Rule>();
            foreach (var r in ruleManager.GetRule())
            {
                ruleFiles.Add(r);
            }
            listRules.ItemsSource = ruleFiles;
        }

        private void deleteRule_Click(object sender, RoutedEventArgs e)
        {
            var rule = (Rule)listRules.SelectedItem;

            ruleManager = new RuleManager();
            ruleManager.DeleteRule(rule);
            UpdateRule?.Invoke();
        }

        private void loadRules_Click(object sender, RoutedEventArgs e)
        {
            UpdateRule?.Invoke();
        }

        private void editRule_Click(object sender, RoutedEventArgs e)
        {
            var rule = (Rule)listRules.SelectedItem;

            var form = new ChangeInfoForm("Название правила:", "Изменить");
            if(form.ShowDialog() == true)
            {
                ruleManager.RenameRule(rule, form.TextInfo);
            }
            UpdateRule?.Invoke();
        }

        private void addRule_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                ruleManager.AddRule(ofd.FileName);
            }
        }

        private void transformingButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new MasterWizard();
            form.Show();
        }
    }
}
