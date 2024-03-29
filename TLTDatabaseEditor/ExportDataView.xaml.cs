﻿using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
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

namespace TLTDatabaseEditor
{
    /// <summary>
    /// Interaction logic for ExportDataView.xaml
    /// </summary>
    public partial class ExportDataView : UserControl
    {
        ExportDataViewModel _viewModel = new ExportDataViewModel(DialogCoordinator.Instance); 

        public ExportDataView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }   

        private void ExportAllHandler(object sender, RoutedEventArgs e)
        {
            _viewModel.ExportToExcel();
        }

        private void BuildingSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
