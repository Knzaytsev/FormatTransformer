using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ConnectorForm.xaml
    /// </summary>
    public partial class ConnectorForm : Window
    {
        public ICorpusConnector Connector { get; set; }
        public ConnectorForm()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            var item = connectorList.SelectedItem as ListBoxItem;
            var connectionString = connectionTextBox.Text;
            switch (item.Name)
            {
                case "db":
                    Connector = new DBConnector(connectionString);
                    break;
                case "local":
                    Connector = new LocalCorpusXMLConnector();
                    break;
            }
            this.DialogResult = true;
        }

        private void connectorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
