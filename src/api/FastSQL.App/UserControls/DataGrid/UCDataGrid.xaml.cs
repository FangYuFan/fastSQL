﻿using FastSQL.Sync.Core.Filters;
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

namespace FastSQL.App.UserControls.DataGrid
{
    /// <summary>
    /// Interaction logic for UCDataGrid.xaml
    /// </summary>
    public partial class UCDataGrid : UserControl
    {
        private DataGridViewModel viewModel;
        public delegate void FilterDelegate(object sender, FilterArguments args);
        public event FilterDelegate OnFilter;

        public static readonly DependencyProperty UseFilterProperty =
           DependencyProperty.Register("UseFilter", typeof(bool),
           typeof(UCDataGrid), new FrameworkPropertyMetadata(false,
               FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnUseFilterChanged)));

        public static readonly DependencyProperty UsePagingProperty =
           DependencyProperty.Register("UsePaging", typeof(bool),
           typeof(UCDataGrid), new FrameworkPropertyMetadata(false,
               FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnUsePagingChanged)));

        private static void OnUsePagingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vm = (DataGridViewModel)d.GetValue(DataContextProperty);
            if (vm == null)
            {
                return;
            }
            vm.UsePaging = (bool)e.NewValue;
        }

        private static void OnUseFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vm = (DataGridViewModel)d.GetValue(DataContextProperty);
            if (vm == null)
            {
                return;
            }
            vm.UseFilter = (bool)e.NewValue;
        }

        public bool UseFilter
        {
            get
            {
                return (bool)GetValue(UseFilterProperty);
            }
            set
            {
                SetValue(UseFilterProperty, value);
            }
        }

        public bool UsePaging
        {
            get
            {
                return (bool)GetValue(UsePagingProperty);
            }
            set
            {
                SetValue(UsePagingProperty, value);
            }
        }

        public UCDataGrid()
        {
            InitializeComponent();
            this.Loaded += UCDataGrid_Loaded;
            
            DataContextChanged += UCDataGrid_DataContextChanged;
            dgrData.AutoGeneratingColumn += DgrData_AutoGeneratingColumn;
            dgrData.AutoGeneratedColumns += DgrData_AutoGeneratedColumns;
            dgrData.SourceUpdated += DgrData_SourceUpdated;
        }

        private void DgrData_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            var lastColumn = dgrData.Columns.LastOrDefault();
            if (lastColumn != null)
            {
                lastColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void UCDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
            {
                return;
            }
            viewModel = (DataGridViewModel)DataContext;
            viewModel.UseFilter = UseFilter;
            viewModel.UsePaging = UsePaging;
            viewModel.OnFilter += ViewModel_OnFilter;
        }

        private void ViewModel_OnFilter(object sender, FilterArguments args)
        {
            OnFilter?.Invoke(sender, args);
        }

        private void DgrData_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var lastColumn = dgrData.Columns.LastOrDefault();
            if (lastColumn != null)
            {
                lastColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void DgrData_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void UCDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void SetData(IEnumerable<object> data)
        {
            viewModel?.SetData(data);
        }

        public void SetOffset(int limit, int offset)
        {
            viewModel?.SetOffset(limit, offset);
        }

        public void Filter(FilterArguments args)
        {
            OnFilter?.Invoke(this, args);
        }
    }
}
