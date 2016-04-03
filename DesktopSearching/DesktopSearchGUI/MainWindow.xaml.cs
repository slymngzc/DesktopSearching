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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopSearchingSpellCorrection;

namespace DesktopSearchGUI
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<FileSystemModel> tempFileList = new List<FileSystemModel>();
        public List<FileSystemModel> tempDirectoryList = new List<FileSystemModel>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var a = DesktopSearchingSpellCorrection.Constants.fileList.Take(1000).ToList();
            a.AddRange(DesktopSearchingSpellCorrection.Constants.directoryList.Take(1000).ToList());
            dataGrid.ItemsSource = a;
        }

        private void populating_Correction(object sender, PopulatingEventArgs e)
        {
            string text = autoCompleteBox.Text;

            tempDirectoryList.Clear();
            tempFileList.Clear();

            switch (text.Length)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                default:
                    tempDirectoryList = DesktopSearchingSpellCorrection.Constants.directoryList.Where(fsm => EditDistance.LevDistance(fsm.Name, text) > (text.Length+fsm.Name.Length)/2).ToList();
                    tempFileList = DesktopSearchingSpellCorrection.Constants.fileList.Where(fsm => EditDistance.LevDistance(fsm.Name, text) > (text.Length + fsm.Name.Length) / 2).ToList();
                    break;
            }

            tempDirectoryList.AddRange(tempFileList);
            autoCompleteBox.ItemsSource = tempDirectoryList.Select(s => s.Name).ToList();
            autoCompleteBox.PopulateComplete();
        }
    }
}
