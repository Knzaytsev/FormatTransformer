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
        delegate void UpdateElementHandler();
        event UpdateElementHandler UpdateCorpus;
        event UpdateElementHandler UpdateFile;

        private CorpusManager corpusManager = new CorpusManager();
        private RuleManager ruleManager = new RuleManager();

        public MainWindow()
        {
            InitializeComponent();
            UpdateCorpus += updateListCorpora;
            UpdateFile += updateListFiles;
        }

        private void updateListFiles()
        {
            var corpus = (Icorpora)listCorpora.SelectedItem;
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
            foreach (var c in corpora)
            {
                collectionCorpora.Add(c);
            }
            listCorpora.ItemsSource = collectionCorpora;
        }

        private void transformButton_Click(object sender, RoutedEventArgs e)
        {
            var transformer = new Transformer();
            transformer.Transform(new XMLTransformer());
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

        private void loadRules_Click(object sender, RoutedEventArgs e)
        {
            ruleManager.ConnectRules(new LocalRuleConnector());
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
            var corpus = (Icorpora)listCorpora.SelectedItem;
            var opf = new OpenFileDialog();
            if (opf.ShowDialog() == true)
            {
                corpusManager.AddFile(corpus, opf.FileName);
                UpdateFile?.Invoke();
            }
        }

        private void deleteCorpus_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Icorpora)listCorpora.SelectedItem;
            corpusManager.RemoveCorpus(corpus);
            UpdateCorpus?.Invoke();
        }

        private void editCorpus_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Icorpora)listCorpora.SelectedItem;
            var form = new ChangeInfoForm("Название корпуса:", "Изменить");
            if(form.ShowDialog() == true)
            {
                corpusManager.EditCorpus(corpus, form.TextInfo);
                UpdateCorpus?.Invoke();
            }
        }

        private void deleteFile_Click(object sender, RoutedEventArgs e)
        {
            var corpus = (Icorpora)listCorpora.SelectedItem;
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
    }
}
