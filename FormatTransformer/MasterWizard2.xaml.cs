using FormatTransformerLib;
using FormatTransformerLib.Connectors.RuleConnector;
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
    /// Логика взаимодействия для MasterWizard2.xaml
    /// </summary>
    public partial class MasterWizard2 : Window
    {
        Singleton singleton;
        public MasterWizard2()
        {
            InitializeComponent();
        }

        public MasterWizard2(Singleton singleton)
        {
            InitializeComponent();
            var ruleConnector = new LocalRuleConnector();
            ruleConnector.Connect();
            listRules.ItemsSource = new ObservableCollection<Rule>(ruleConnector.GetRules());
            this.singleton = singleton;
        }

        private void nextStepButton_Click(object sender, RoutedEventArgs e)
        {
            singleton.Rule = (Rule)listRules.SelectedItem;
            var form = new MasterWizard(singleton, false);
            form.Show();
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
