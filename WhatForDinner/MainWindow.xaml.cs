using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace WhatForDinner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ItemSource = new ObservableCollection<string>();
        }

        public ObservableCollection<string> ItemSource
        {
            get { return (ObservableCollection<string>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(ObservableCollection<string>), typeof(MainWindow), new PropertyMetadata(null));

        private string SavePath = @"D://mygif.gif";
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Helper.CreateGif(ItemSource, SavePath);
            outputPath.Text = SavePath;
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            string newiRTEMS = newItemString.Text;
            if (!string.IsNullOrEmpty(newiRTEMS))
            {
                var items = newiRTEMS.Split(',', '，');
                foreach (var item in items)
                {
                    ItemSource.Add(item);
                }
            }
            var distList = ItemSource.Distinct().ToList();
            ItemSource = new ObservableCollection<string>(distList);
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var content = button.Content.ToString();

            var removeItems = ItemSource.Where(x => x == content).ToList();
            foreach (var item in removeItems)
            {
                if (ItemSource.Any(x => x == item))
                {
                    ItemSource.Remove(item);
                }
            }
        }

    }
}
