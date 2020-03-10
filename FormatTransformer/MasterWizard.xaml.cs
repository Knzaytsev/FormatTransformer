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
    /// Логика взаимодействия для MasterWizard.xaml
    /// </summary>
    public partial class MasterWizard : Window
    {
        private Singleton singleton;
        private bool dir = true;
        public MasterWizard()
        {
            InitializeComponent();
            singleton = Singleton.GetInstance();
        }

        public MasterWizard(Singleton singleton, bool dir)
        {
            InitializeComponent();
            this.singleton = singleton;
            this.dir = dir;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void nextStepButton_Click(object sender, RoutedEventArgs e)
        {
            ICorpusConnector connector = null;
            switch (((ComboBoxItem)typesConnection.SelectedItem).Name)
            {
                case "local":
                    connector = new LocalCorpusConnector();
                    break;
                case "db":
                    connector = new DBConnector(connectionTextBox.Text);
                    break;
            }
            if (dir)
            {
                singleton.ConnectorFrom = connector;
                var form = new MasterWizard1(singleton);
                form.Show();
            }
            else
            {
                singleton.ConnectorTo = connector;
                var form = new MasterWizard3(singleton);
                form.Show();
            }
            Close();
        }
    }
}
