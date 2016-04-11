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
            dataGrid.ItemsSource = DesktopSearchingSpellCorrection.Constants.allFileSystems.Take(1000).ToList();
        }

        private void searchBox_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = searchBox.Text;
            linkPanel.Visibility = Visibility.Hidden;

            switch (text.Length)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    var sonuc = DistanceCalculation.CalculateDistance(text);
                    if (sonuc.Distance <= 2)
                    {
                        linkText.Text = sonuc.Name;
                        linkPanel.Visibility = Visibility.Visible;
                    }

                    break;
                default:
                    sonuc = DistanceCalculation.CalculateDistance(text);
                    if (sonuc.Distance <= 3)
                    {
                        linkText.Text = sonuc.Name;
                        linkPanel.Visibility = Visibility.Visible;
                    }
                    break;
            }

            dataGrid.ItemsSource = DesktopSearchingSpellCorrection.Constants.allFileSystems
                .Where(fsm => fsm.Name.ToLower()
                .Replace(" ","").Contains(text.ToLower().Replace(" ",""))).ToList();
        }
    }
}
