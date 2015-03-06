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
using System.ComponentModel;

namespace CoordMaker
{
    /// <summary>
    /// Логика взаимодействия для StackerRack.xaml
    /// </summary>
    public partial class StackerRack : UserControl
    {
        public StackerRack()
        {
            InitializeComponent();
        }

        private static void DepParamsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StackerRack ctrl = (StackerRack)d;
            if (e.Property.Name == "WorkParams")
            {

            }
            ctrl.SetParam(e.Property.Name, e.NewValue, e.OldValue);

        }

        public void SetParam(String propname, Object val, object oldval)
        {
            switch (propname)
            {
               /* case "Rows":
                    while (RackLeft.Columns.Count < Rows)
                    {
                        RackLeft.Columns.Add(new DataGridTextColumn() );
                    }

                    while (RackRight.Columns.Count < Rows)
                    {
                        RackRight.Columns.Add(new DataGridTextColumn());
                    }
                    // 
                    while (RackLeft.Columns.Count > Rows)
                    {
                        RackLeft.Columns.RemoveAt(0);
                    }

                    while (RackRight.Columns.Count > Rows)
                    {
                        RackRight.Columns.RemoveAt(0);
                    }

                    int i = 1;
                    foreach (DataGridTextColumn dgtc in RackLeft.Columns)
                    {
                        dgtc.Header = i;
                        DataGridLength dgl = dgtc.Width;
                        
                        dgtc.Width = dgl;
                        i++;
                    }

                    i = 1;
                    foreach (DataGridTextColumn dgtc in RackRight.Columns)
                    {
                        dgtc.Header = i;
                        i++;
                    }

                    break;
                case "Floors":
                    while (RackLeft.Items.Count < Floors)
                    {
                        RackLeft.Items.Add(new Object());
                    }

                    while (RackRight.Items.Count < Floors)
                    {
                        RackRight.Items.Add(new Object());
                    }
                    
                    break;*/
            }
        }
        /*
        // Dependency Property
        public static readonly DependencyProperty RowsDP = DependencyProperty.Register("Rows", typeof(Int32), typeof(StackerRack), new FrameworkPropertyMetadata(5, DepParamsChanged));
        // .NET Property wrapper
        [Description("Row count"), Category("Stacker")]
        public Int32 Rows
        {
            get
            {
                return (Int32)GetValue(RowsDP);
            }
            set
            {
                SetValue(RowsDP, value);
            }
        }

        // Dependency Property
        public static readonly DependencyProperty FloorsDP = DependencyProperty.Register("Floors", typeof(Int32), typeof(StackerRack), new FrameworkPropertyMetadata(5, DepParamsChanged));
        // .NET Property wrapper
        [Description("Floors count"), Category("Stacker")]
        public Int32 Floors
        {
            get
            {
                return (Int32)GetValue(FloorsDP);
            }
            set
            {
                SetValue(FloorsDP, value);
            }
        }*/

    }
}
