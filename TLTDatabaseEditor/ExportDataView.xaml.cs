// Decompiled with JetBrains decompiler
// Type: TLTDatabaseEditor.ExportDataView
// Assembly: TLTDatabaseEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3AA8152-A60D-4A42-87BE-242C5FFCE9A0
// Assembly location: C:\Program Files (x86)\GizmoTron v1.5\TLTDatabaseEditor.exe

using MahApps.Metro.Controls.Dialogs;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace TLTDatabaseEditor
{
    public partial class ExportDataView : UserControl
    {
        private ExportDataViewModel _viewModel = new ExportDataViewModel(DialogCoordinator.Instance);

        public ExportDataView()
        {
            this.InitializeComponent();
            this.DataContext = (object)this._viewModel;
        }

        private void ExportAllHandler(object sender, RoutedEventArgs e) => this._viewModel.ExportToExcel();

        private void BuildingSelectionChangedHandler(object sender, SelectionChangedEventArgs e) => this.BuildingsDataGrid.Items.Refresh();
    }
}
