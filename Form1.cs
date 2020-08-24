using DemoCommon.Grid;
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using Syncfusion.WinForms.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SfDataGridDemo
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.sfDataGrid.AutoGenerateColumns = false;
            this.sfDataGrid.DataSource = new ViewModel().Orders;
            this.sfDataGrid.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;           

            GridNumericColumn gridTextColumn1 = new GridNumericColumn() { MappingName = "OrderID", HeaderText = "Order ID" };
            GridTextColumn gridTextColumn2 = new GridTextColumn() { MappingName = "CustomerID", HeaderText = "Customer ID" };
            GridTextColumn gridTextColumn3 = new GridTextColumn() { MappingName = "CustomerName", HeaderText = "Customer Name" };
            GridTextColumn gridTextColumn4 = new GridTextColumn() { MappingName = "Country", HeaderText = "Country" };         
            GridDateTimeColumn gridDateTimeColumn = new GridDateTimeColumn() { MappingName = "Date", Width = 130, HeaderText = "Date", Pattern = Syncfusion.WinForms.Input.Enums.DateTimePattern.Custom, Format = "MM/dd/yyyy hh:mm" };

            this.sfDataGrid.Columns.Add(gridDateTimeColumn);
            this.sfDataGrid.Columns.Add(gridTextColumn1);
            this.sfDataGrid.Columns.Add(gridTextColumn2);
            this.sfDataGrid.Columns.Add(gridTextColumn3);
            this.sfDataGrid.Columns.Add(gridTextColumn4);

            this.sfDataGrid.CellRenderers.Remove("DateTime");
            this.sfDataGrid.CellRenderers.Add("DateTime", new GridDateTimeCellRendererExt());           
        }     

        public class GridDateTimeCellRendererExt : GridDateTimeCellRenderer
        {
            protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
            {
                string[] date = cellValue.Split();
                SizeF size = paint.MeasureString(date[0], style.Font.GetFont());
                float height = (cellRect.Height - size.Height) / 2;
                paint.DrawString(date[0], style.Font.GetFont(), new SolidBrush(style.TextColor), cellRect.X, cellRect.Y + height);
                paint.DrawString(date[1], new Font(style.Font.Facename, style.Font.Size, FontStyle.Bold), new SolidBrush(style.TextColor), cellRect.X + size.Width, cellRect.Y + height);
            }
        }
    }

    public class OrderInfo : INotifyPropertyChanged
    {
        decimal? orderID;
        string customerId;
        string country;
        string customerName;       
        DateTime date;

        public OrderInfo()
        {

        }

        public decimal? OrderID
        {
            get { return orderID; }
            set { orderID = value; this.OnPropertyChanged("OrderID"); }
        }

        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; this.OnPropertyChanged("CustomerID"); }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; this.OnPropertyChanged("CustomerName"); }
        }

        public string Country
        {
            get { return country; }
            set { country = value; this.OnPropertyChanged("Country"); }
        }       

        public DateTime Date
        {
            get { return date; }
            set { date = value; this.OnPropertyChanged("Date"); }
        }

        public OrderInfo(decimal? orderId, string customerName, string country, string customerId, DateTime date)
        {
            this.OrderID = orderId;
            this.CustomerName = customerName;
            this.Country = country;
            this.CustomerID = customerId;           
            this.Date = date;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModel
    {
        private ObservableCollection<OrderInfo> orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public ViewModel()
        {
            orders = new ObservableCollection<OrderInfo>();
            orders.Add(new OrderInfo(1001, "Thomas Hardy", "Germany", "ALFKI", new DateTime(2020, 02, 15, 01, 22, 23)));
            orders.Add(new OrderInfo(1002, "Laurence Lebihan", "Mexico", "ANATR", new DateTime(2020, 12, 21, 07, 12, 10)));
            orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", new DateTime(2020, 05, 05, 09, 45, 40)));
            orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", new DateTime(2020, 10, 15, 04, 28, 32)));
            orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", new DateTime(2020, 07, 20, 10, 48, 08)));
            orders.Add(new OrderInfo(1006, "Hanna Moos", "Sweden", "BLAUS", new DateTime(2015, 5, 2, 08, 26, 35)));
            orders.Add(new OrderInfo(1007, "Frederique Citeaux", "Sweden", "BLONP", new DateTime(2015, 5, 20, 06, 05,34)));
            orders.Add(new OrderInfo(1008, "Martin Sommer", "Sweden", "BOLID", new DateTime(2015, 5, 3, 05, 47, 45)));
            orders.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", new DateTime(2015, 5, 13, 03, 37, 38)));
            orders.Add(new OrderInfo(1010, "Elizabeth Lincoln", "France", "BOTTM", new DateTime(2015, 5, 23, 02, 15, 36)));
            orders.Add(new OrderInfo(1011, "Christina Berglund", "France", "BERGS", new DateTime(2015, 5, 13, 03, 18, 26)));
            orders.Add(new OrderInfo(1012, "Hanna Moos", "France", "BLAUS", new DateTime(2015, 5, 13, 04, 39, 28)));
            orders.Add(new OrderInfo(1013, "Frederique Citeaux", "France", "BLONP", new DateTime(2015, 1, 23, 12, 38, 49)));
            orders.Add(new OrderInfo(1014, "Martin Sommer", "France", "BOLID", new DateTime(2015, 4, 28, 11, 20, 23)));
            orders.Add(new OrderInfo(1015, "Laurence Lebihan", "Italy", "BONAP", new DateTime(2015, 5, 8, 07, 35, 29)));
        }
    }
}
