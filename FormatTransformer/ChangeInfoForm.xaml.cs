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
    /// Логика взаимодействия для ChangeInfoForm.xaml
    /// </summary>
    public partial class ChangeInfoForm : Window
    {
        public string TextInfo { get; set; }

        public ChangeInfoForm(string label, string button)
        {
            InitializeComponent();
            labelInfo.Content = label;
            infoButton.Content = button;
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            TextInfo = infoField.Text;
            DialogResult = true;
        }
    }
}
