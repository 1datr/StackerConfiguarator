using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoordMaker
{
    /// <summary>
    /// Логика взаимодействия для Dlgfilename.xaml
    /// </summary>
    public partial class Dlgfilename : Window
    {
        public Dlgfilename()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private String pFileName = "configSH1.xml";
        public String FileName { get { return pFileName; } set { pFileName = value; } }

        private void xml_filename_TextChanged(object sender, TextChangedEventArgs e)
        {
            pFileName = xml_filename.Text;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            pFileName = "configSH1.xml";
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            pFileName = "configSH2.xml";
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            pFileName = "configSIM.xml";
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            pFileName = xml_filename.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            pFileName = "configSH1.xml";
        }
    }
}
