using FormatTransformerLib;
using FormatTransformerLib.Transformers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FormatTransformer
{
    /// <summary>
    /// Логика взаимодействия для MasterWizard3.xaml
    /// </summary>
    public partial class MasterWizard3 : Window
    {
        private Singleton singleton;
        public MasterWizard3(Singleton singleton)
        {
            InitializeComponent();
            this.singleton = singleton;
            corpusName.Content = singleton.Corpus.Title;
            listFiles.ItemsSource = new ObservableCollection<ICorpora>((this.singleton.Corpus as Corpus).GetCorpora());
            ruleName.Content = singleton.Rule.Title;
        }

        private void transformingButton_Click(object sender, RoutedEventArgs e)
        {
            Transformer transformer = new Transformer();
            transformer.AddFile((singleton.Corpus as Corpus).GetCorpora()[0].Info);
            transformer.AddRule(singleton.Rule.Info);
            transformer.Transform(new DBTransformer());
            var result = transformer.GetResult();
            var manager = new CorpusManager();
            manager.ConnectCorpus(singleton.ConnectorTo);
            manager.AddCorpus(result);
            MessageBox.Show("Успешно!", "Выполнение операции", MessageBoxButton.OK);
            Close();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
