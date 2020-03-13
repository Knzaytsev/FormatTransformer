using FormatTransformerLib;
using FormatTransformerLib.Connectors.CorpusConnector;
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
    /// Логика взаимодействия для MasterWizard1.xaml
    /// </summary>
    public partial class MasterWizard1 : Window
    {
        ICorpusConnector connector;
        Singleton singleton;

        public MasterWizard1(Singleton singleton)
        {
            InitializeComponent();
            this.singleton = singleton;
            connector = this.singleton.ConnectorFrom;
            connector.Connect();
            listCorpora.ItemsSource = new ObservableCollection<ICorpora>(connector.GetCorpora().GetCorpora());
        }

        private void nextStepButton_Click(object sender, RoutedEventArgs e)
        {
            singleton.Corpus = (ICorpora)listCorpora.SelectedItem;
            var form = new MasterWizard2(singleton);
            form.Show();
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new MasterWizard();
            form.Show();
            Close();
        }
    }
}
