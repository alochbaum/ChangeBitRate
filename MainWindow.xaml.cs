using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace ChangeBitRate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Andrew Lochbaum's program to change all Bitrates";
        }

        private void btnInFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.FileName = tbInFile.Text;
            if (fileDialog.ShowDialog() == true)
            {
                tbInFile.Text = fileDialog.FileName;
                tbOutFile.Text = tbInFile.Text.Substring(0, tbInFile.Text.Length - 4) + "_fix.xml";
                myAppend("Changed Input File");
            }
        }

        private void btnOutFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.FileName = tbOutFile.Text;
            if (fileDialog.ShowDialog() == true)
                tbOutFile.Text = fileDialog.FileName;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            // Load inFile
            XDocument inFile = XDocument.Load(tbInFile.Text);
            myAppend("Loading In File");

            //change counter
            Int64 iCount = 0;

            // For each maxBitrate in file
            foreach (XElement elementMax in inFile.Descendants("maxBitrate"))
            {
                elementMax.Value = "0";
                iCount++;
            }
            myAppend($"Changed maxBitrate {iCount} times");
            iCount = 0;
            // Fore each minBitrate in File
            foreach (XElement elementMin in inFile.Descendants("minBitrate"))
            {
                elementMin.Value = "0";
                iCount++;
            }
            myAppend($"Changed minBitrate {iCount} times");

            if (File.Exists(tbOutFile.Text))
            {
                File.Delete(tbOutFile.Text);
                myAppend("Deleted old outfile");
            }
            // Save the changes to the full file
            inFile.Save(tbOutFile.Text);
            myAppend("Saved File to tbOutFile");
        }

        private void myAppend(string inStr)
        {
            rtbStatus.AppendText($"{inStr}{Environment.NewLine}");
        }
    }
}
