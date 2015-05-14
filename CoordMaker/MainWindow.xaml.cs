using System;
using System.Runtime;
using System.Configuration;
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CoordMaker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    [Serializable]
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            try
            {
                this.OpenProject(args[1]);
                
            }
            catch (System.Exception ex)
            { 
            
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        // Save project
        public void SaveProject(bool SaveAs=false)
        {
            // и в файл ее
            // Create OpenFileDialog
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".bsp";
            dlg.Filter = "BnR stacker project (.bsp)|*.bsp";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result;
            if (SaveAs)
                result = dlg.ShowDialog();
            else
            {
                if (filename == "")
                {
                    result = dlg.ShowDialog();
                    SaveAs = true;
                }
                else
                    result = true;
            }
            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                if (SaveAs)
                    filename = dlg.FileName;

                XmlSerializer xmlFormat = new XmlSerializer(typeof(ProjectContainer));
                using (Stream fStream = new FileStream(filename,
                    FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    xmlFormat.Serialize(fStream, ProjectData);
                }
            }


        }
        // Open project
        private string filename = "";

       // public ProjectContainer ProjectData { get; set; }

        public static readonly DependencyProperty ProjectDataDP = DependencyProperty.Register("ProjectData", typeof(ProjectContainer), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ProjectContainer ProjectData
        {
            get
            {
                return (ProjectContainer)GetValue(ProjectDataDP);
            }
            set { SetValue(ProjectDataDP, value); }
        }

        public void OpenProject(String fname = "")
        {
            if (fname != "")
            {
                filename = fname;
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ProjectContainer));
                using (Stream fStream = File.OpenRead(filename))
                {
                    ProjectContainer p = (ProjectContainer)xmlFormat.Deserialize(fStream);
                    ProjectData = p;
                    ProjectData.Update();

                    //  ProjectData.XCoordsObservableCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(XCoordsObservableCollection_CollectionChanged);

                    //  ProjectData.YCoordsObservableCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(YCoordsObservableCollection_CollectionChanged);
                }

            }
            else
            {
                // и в файл ее
                // Create OpenFileDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".bsp";
                dlg.Filter = "BnR stacker project (.bsp)|*.bsp";

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    filename = dlg.FileName;
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(ProjectContainer));
                    using (Stream fStream = File.OpenRead(filename))
                    {
                        ProjectContainer p = (ProjectContainer)xmlFormat.Deserialize(fStream);
                        ProjectData = p;
                        ProjectData.Update();

                        //  ProjectData.XCoordsObservableCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(XCoordsObservableCollection_CollectionChanged);

                        //  ProjectData.YCoordsObservableCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(YCoordsObservableCollection_CollectionChanged);
                    }
                }
            }
        }    
        
        // Dependency Property
        public static readonly DependencyProperty XLeftMatrDP = DependencyProperty.Register("XLeftMatr", typeof(DynMatrix2D<Int32>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> XLeftMatr
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(XLeftMatrDP);
            }
            set { SetValue(XLeftMatrDP, value); }
        }


        // Dependency Property
        public static readonly DependencyProperty XRightMatrDP = DependencyProperty.Register("XRightMatr", typeof(DynMatrix2D<Int32>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> XRightMatr
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(XRightMatrDP);
            }
            set { SetValue(XRightMatrDP, value); }
        }


        // Dependency Property
        public static readonly DependencyProperty YLeftMatrDP = DependencyProperty.Register("YLeftMatr", typeof(DynMatrix2D<Int32>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> YLeftMatr
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(YLeftMatrDP);
            }
            set { SetValue(YLeftMatrDP, value); }
        }


        // Dependency Property
        public static readonly DependencyProperty YRightMatrDP = DependencyProperty.Register("YRightMatr", typeof(DynMatrix2D<Int32>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> YRightMatr
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(YRightMatrDP);
            }
            set { SetValue(YRightMatrDP, value); }
        }


        // Dependency Property
        public static readonly DependencyProperty ZLeftMatrDP = DependencyProperty.Register("ZLeftMatr", typeof(DynMatrix2D<Int32>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> ZLeftMatr
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(ZLeftMatrDP);
            }
            set { SetValue(ZLeftMatrDP, value); }
        }


        // Dependency Property
        public static readonly DependencyProperty ZRightMatrDP = DependencyProperty.Register("ZRightMatr", typeof(DynMatrix2D<Int32>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> ZRightMatr
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(ZRightMatrDP);
            }
            set { SetValue(ZRightMatrDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty ZMaxDP = DependencyProperty.Register("ZMax", typeof(Int32), typeof(MainWindow), new FrameworkPropertyMetadata(755));
        // .NET Property wrapper
        public Int32 ZMax
        {
            get
            {
                return (Int32)GetValue(ZMaxDP);
            }
            set { SetValue(ZMaxDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty UpY2DP = DependencyProperty.Register("UpY2", typeof(ItemsChangeObservableCollection<CoordObject>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ItemsChangeObservableCollection<CoordObject> UpY2
        {
            get
            {
                return (ItemsChangeObservableCollection<CoordObject>)GetValue(UpY2DP);
            }
            set { SetValue(UpY2DP, value); }
        }

        // Movestep X,Y,Z

        // Dependency Property
        public static readonly DependencyProperty MoveStepXDP = DependencyProperty.Register("MoveStepX", typeof(ItemsChangeObservableCollection<CoordObject>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ItemsChangeObservableCollection<CoordObject> MoveStepX
        {
            get
            {
                return (ItemsChangeObservableCollection<CoordObject>)GetValue(MoveStepXDP);
            }
            set { SetValue(MoveStepXDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty MoveStepYDP = DependencyProperty.Register("MoveStepY", typeof(ItemsChangeObservableCollection<CoordObject>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ItemsChangeObservableCollection<CoordObject> MoveStepY
        {
            get
            {
                return (ItemsChangeObservableCollection<CoordObject>)GetValue(MoveStepYDP);
            }
            set { SetValue(MoveStepYDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty MoveStepZDP = DependencyProperty.Register("MoveStepZ", typeof(ItemsChangeObservableCollection<CoordObject>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ItemsChangeObservableCollection<CoordObject> MoveStepZ
        {
            get
            {
                return (ItemsChangeObservableCollection<CoordObject>)GetValue(MoveStepZDP);
            }
            set { SetValue(MoveStepZDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty ScalesDP = DependencyProperty.Register("Scales", typeof(ObservableCollection<ScaleObject>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ObservableCollection<ScaleObject> Scales
        {
            get
            {
                return (ObservableCollection<ScaleObject>)GetValue(ScalesDP);
            }
            set { SetValue(ScalesDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty CuttingPointsDP = DependencyProperty.Register("CuttingPoints", typeof(ItemsChangeObservableCollection<CuttingPoint>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ItemsChangeObservableCollection<CuttingPoint> CuttingPoints
        {
            get
            {
                return (ItemsChangeObservableCollection<CuttingPoint>)GetValue(CuttingPointsDP);
            }
            set { SetValue(CuttingPointsDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty FixedPointsDP = DependencyProperty.Register("FixedPoints", typeof(ItemsChangeObservableCollection<PointAddr>), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public ItemsChangeObservableCollection<PointAddr> FixedPoints
        {
            get
            {
                return (ItemsChangeObservableCollection<PointAddr>)GetValue(FixedPointsDP);
            }
            set { SetValue(FixedPointsDP, value); }
        }

        public RoutedCommand Import
        {
            get { return (RoutedCommand)GetValue(ExportCommandProperty); }
            set { SetValue(ExportCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OKCommand.  This enables animation, styling, binding, etc...  
        public static readonly DependencyProperty ExportCommandProperty =
            DependencyProperty.Register("OKCommand", typeof(RoutedCommand), typeof(MainWindow), new UIPropertyMetadata(new RoutedCommand("Export", typeof(MainWindow))));  
  

        private void Window_Initialized(object sender, EventArgs e)
        {
            
            ProjectData = new ProjectContainer();
            ProjectData.SetDefaultCoords();
            ProjectData.OnSizeChange += new OnSizeChange(ProjectData_OnSizeChange);
            ProjectData.Update();

            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute, canExecute);
            CommandBindings.Add(saveCommand);

            CommandBinding saveAsCommand = new CommandBinding(ApplicationCommands.SaveAs, execute, canExecute);
            CommandBindings.Add(saveAsCommand);

            CommandBinding exitCommand = new CommandBinding(ApplicationCommands.Close, execute, canExecute);
            CommandBindings.Add(exitCommand);

            CommandBinding openCommand = new CommandBinding(ApplicationCommands.Open, execute, canExecute);
            CommandBindings.Add(openCommand);

            CommandBinding newCommand = new CommandBinding(ApplicationCommands.New, execute, canExecute);
            CommandBindings.Add(newCommand);

            /*
            CommandBinding exportCommand = new CommandBinding(Export, execute, canExecute);
            CommandBindings.Add(exportCommand);*/
            CommandBindings.Add(
                    new CommandBinding(MyCommands.CmdImport,   // this is the command object
                                execute,      // execute
                                canExecute));// can execute?
        }

        void ProjectData_OnSizeChange(int ysize, int xsize)
        {
            //throw new NotImplementedException();
            XLeftMatr = new DynMatrix2D<int>(ProjectData.IncMatrLeftX.Rows, ProjectData.IncMatrLeftX.Cols);
            XRightMatr = new DynMatrix2D<int>(ProjectData.IncMatrRightX.Rows, ProjectData.IncMatrRightX.Cols);

            YLeftMatr = new DynMatrix2D<int>(ProjectData.IncMatrLeftY.Rows, ProjectData.IncMatrLeftY.Cols);
            YRightMatr = new DynMatrix2D<int>(ProjectData.IncMatrRightY.Rows, ProjectData.IncMatrRightY.Cols);

            ZLeftMatr = new DynMatrix2D<int>(ProjectData.IncMatrLeftZ.Rows, ProjectData.IncMatrLeftZ.Cols);
            ZRightMatr = new DynMatrix2D<int>(ProjectData.IncMatrRightZ.Rows, ProjectData.IncMatrRightZ.Cols);
        }

        void canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            /*
            if (e.Command == ApplicationCommands.Save)
            {
                if (filename != "") e.CanExecute = true;
                else e.CanExecute = false;
            }
            else
            */
            e.CanExecute = true;
        }

        void execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Close)
                Close();
            else if (e.Command == ApplicationCommands.New)
            {
                ProjectData = new ProjectContainer();
                ProjectData.SetDefaultCoords();
                filename = "";
            }
            else if (e.Command == ApplicationCommands.Open)
                OpenProject();
            else if (e.Command == ApplicationCommands.Save)
            {
                SaveProject();
            }
            else if (e.Command == ApplicationCommands.SaveAs)
            {
                SaveProject(true);
            }
            else if (e.Command == MyCommands.CmdImport)
            {
                ImportCfg();
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridCell cell = DataGridHelper.GetCell(CoordsX.SelectedCells[0]);
                Int32 pos = DataGridHelper.GetRowIndex(cell);
                ProjectData.IncX(pos);
            }
            catch (System.Exception exc)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridCell cell = DataGridHelper.GetCell(CoordsX.CurrentCell);
                Int32 pos = DataGridHelper.GetRowIndex(cell);
                ProjectData.DecX(pos);
            }
            catch (System.Exception exc)
            {

            }
        }

        private void Rows_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                while (ProjectData.CoordsXList.Count < Convert.ToInt32(Rows.Text))
                {
                    ProjectData.CoordsXList.Add(new CoordObject(ProjectData.CoordsXList[ProjectData.CoordsXList.Count - 1].Value));
                }

                while (ProjectData.CoordsXList.Count > Convert.ToInt32(Rows.Text))
                {
                    ProjectData.CoordsXList.RemoveAt(ProjectData.CoordsXList.Count - 1);
                }

                ProjectData.Update();
            }
            catch (System.Exception exc)
            {
            }
        }

        private void Floors_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                while (ProjectData.CoordsYList.Count < Convert.ToInt32(Floors.Text))
                {
                    ProjectData.CoordsYList.Add(new CoordObject(ProjectData.CoordsYList[ProjectData.CoordsYList.Count - 1].Value));
                }

                while (ProjectData.CoordsYList.Count > Convert.ToInt32(Floors.Text))
                {
                    ProjectData.CoordsYList.RemoveAt(ProjectData.CoordsYList.Count - 1);
                }

                ProjectData.Update();
            }
            catch (System.Exception exc)
            {
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridCell cell = DataGridHelper.GetCell(CoordsY.SelectedCells[0]);
                Int32 pos = DataGridHelper.GetRowIndex(cell);
                ProjectData.IncY(pos);
            }
            catch (System.Exception exc)
            {

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridCell cell = DataGridHelper.GetCell(CoordsY.CurrentCell);
                Int32 pos = DataGridHelper.GetRowIndex(cell);
                ProjectData.DecY(pos);
            }
            catch (System.Exception exc)
            {

            }
        }

        private void AprX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridCell cell = DataGridHelper.GetCell(CoordsX.SelectedCells[0]);
                Int32 pos = DataGridHelper.GetRowIndex(cell);
                List<Int32> diffs = new List<int>();
                Int32 xlast = ProjectData.CoordsXList[0].Value;
                for (int i = 1; i <= pos; i++)
                {
                    diffs.Add(ProjectData.CoordsXList[i].Value - xlast);
                    xlast = ProjectData.CoordsXList[i].Value;
                }

                xlast = ProjectData.CoordsXList[pos].Value;
                for (int i = pos + 1; i < ProjectData.CoordsXList.Count; i++)
                {
                    ProjectData.CoordsXList[i].Value = xlast + diffs[(i - pos - 1) % diffs.Count];
                    xlast = ProjectData.CoordsXList[i].Value;
                }
            }
            catch (System.Exception exc)
            { }
        }

        private void AprY_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridCell cell = DataGridHelper.GetCell(CoordsY.SelectedCells[0]);
                Int32 pos = DataGridHelper.GetRowIndex(cell);
                List<Int32> diffs = new List<int>();
                Int32 ylast = ProjectData.CoordsYList[0].Value;
                for (int i = 1; i <= pos; i++)
                {
                    diffs.Add(ProjectData.CoordsYList[i].Value - ylast);
                    ylast = ProjectData.CoordsYList[i].Value;
                }

                ylast = ProjectData.CoordsYList[pos].Value;
                for (int i = pos + 1; i < ProjectData.CoordsYList.Count; i++)
                {
                    ProjectData.CoordsYList[i].Value = ylast + diffs[(i - pos - 1) % diffs.Count];
                    ylast = ProjectData.CoordsYList[i].Value;
                }
            }
            catch (System.Exception exc)
            { }
        }

        private XElement xmlbuf = new XElement("trans");

        private XElement MovStepX = new XElement("movestepX");
        private XElement MovStepY = new XElement("movestepY");
        private XElement MovStepZ = new XElement("movestepZ");

        

       
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        

        private void ImportCfg()
        {
            String xmlfilename = "";
            System.Windows.Forms.FolderBrowserDialog folderdlg = new System.Windows.Forms.FolderBrowserDialog();
            if (filename != "")
            {
                FileInfo FI = new FileInfo(this.filename);
                folderdlg.SelectedPath = FI.DirectoryName;
            }
            System.Windows.Forms.DialogResult result = folderdlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // choose the file name
                xmlfilename = folderdlg.SelectedPath;
                Dlgfilename dlf = new Dlgfilename();
                Nullable<bool> hresult = dlf.ShowDialog();
                if (hresult == true)
                {
                    // Open document
                    filename = dlf.FileName;
                    ProjectData.Import(xmlfilename + "/" + filename);
                }
            }

            

            /*

            // и в файл ее
            // Create OpenFileDialog
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";
            System.Windows.Forms.FolderBrowserDialog folderdlg = new System.Windows.Forms.FolderBrowserDialog(); 



            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                ProjectData.Export(filename);
            }
             * 
             * 
             */
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SaveProject();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            OpenProject();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ProjectData.IncMatrLeftX = ProjectData.IncMatrLeftX + this.XLeftMatr;
            XLeftMatr.Reset();
            XLeft.ItemsSource2D = XLeft.ItemsSource2D;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ProjectData.IncMatrRightX = ProjectData.IncMatrRightX + this.XRightMatr;
            XRightMatr.Reset();
            XRight.ItemsSource2D = XRight.ItemsSource2D;
        }
        
    }

    public static class MyCommands
    {
        public static RoutedUICommand CmdImport = new RoutedUICommand("CmdImport",
                                                                   "CmdImport",
                                                                   typeof(MyCommands));
    }

    // Целое значение
    [Serializable]
    public class CoordObject : DependencyObject, INotifyPropertyChanged
    {

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

            }
        }

        private Int32 fValue;
        public Int32 Value
        {
            get
            {
                return fValue;
            }

            set
            {
                fValue = value;
                OnPropertyChanged("Value");
            }
        }

        public CoordObject(int val = 0)
        {
            Value = val;
        }

        public CoordObject()
        {
            Value = 0;
        }
    }
    // Плавающее значение
    [Serializable]
    public class ScaleObject : DependencyObject, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

            }
        }

        private Double fValue;
        public Double Value
        {
            get
            {
                return fValue;
            }

            set
            {
                fValue = value;
                OnPropertyChanged("Value");
            }
        }

        public ScaleObject(Double val = 0)
        {
            Value = val;
        }

        public ScaleObject()
        {
            Value = 0;
        }
    }
    // Точка, вырезаемая из массива ячеек
    [Serializable]
    public class CuttingPoint : DependencyObject, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

            }
        }

        private Int32 fX;
        public Int32 X
        {
            get
            {
                return fX;
            }

            set
            {
                fX = value;
                OnPropertyChanged("X");
            }
        }

        private Int32 fY;
        public Int32 Y
        {
            get
            {
                return fY;
            }

            set
            {
                fY = value;
                OnPropertyChanged("Y");
            }
        }

        private bool fRight = false;
        public bool Right
        {
            get 
            { 
                return fRight; 
            }
            set 
            { 
                fRight = value;
                OnPropertyChanged("Right");
            }
        }

        public CuttingPoint(Int32 x = 0, Int32 y = 0, Boolean right = false)
        {
            X = x;
            Y = y;
            fRight = right;
        }

        public CuttingPoint()
        {
            X = 0;
            Y = 0;
            fRight = false;
        }
    }

    [Serializable]
    public class PointAddr : DependencyObject, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

            }
        }

        private Int32 fX;
        public Int32 X
        {
            get
            {
                return fX;
            }

            set
            {
                fX = value;
                OnPropertyChanged("X");
            }
        }

        private Int32 fY;
        public Int32 Y
        {
            get
            {
                return fY;
            }

            set
            {
                fY = value;
                OnPropertyChanged("Y");
            }
        }

        private bool fRight = false;
        public bool Right
        {
            get
            {
                return fRight;
            }
            set
            {
                fRight = value;
                OnPropertyChanged("Right");
            }
        }

        private Int32 fCellID=0;
        public Int32 CellID
        {
            get
            {
                return fCellID;
            }

            set
            {
                fCellID = value;
                OnPropertyChanged("CellID");
            }
        }

        public PointAddr(Int32 cellid = 0, Int32 x = 0, Int32 y = 0, Boolean right = false)
        {
            CellID = cellid;
            X = x;
            Y = y;
            Right = right;
        }

        public PointAddr()
        {
            CellID = 0;
            X = 0;
            Y = 0;
            Right = false;
        }
    }
    
    //
    public static class DataGridHelper
    {
        public static DataGridCell GetCell(DataGridCellInfo dataGridCellInfo)
        {
            if (!dataGridCellInfo.IsValid)
            {
                return null;
            }

            var cellContent = dataGridCellInfo.Column.GetCellContent(dataGridCellInfo.Item);
            if (cellContent != null)
            {
                return (DataGridCell)cellContent.Parent;
            }
            else
            {
                return null;
            }
        }
        public static int GetRowIndex(DataGridCell dataGridCell)
        {
            // Use reflection to get DataGridCell.RowDataItem property value.
            PropertyInfo rowDataItemProperty = dataGridCell.GetType().GetProperty("RowDataItem", BindingFlags.Instance | BindingFlags.NonPublic);

            DataGrid dataGrid = GetDataGridFromChild(dataGridCell);

            return dataGrid.Items.IndexOf(rowDataItemProperty.GetValue(dataGridCell, null));
        }
        public static DataGrid GetDataGridFromChild(DependencyObject dataGridPart)
        {
            if (VisualTreeHelper.GetParent(dataGridPart) == null)
            {
                throw new NullReferenceException("Control is null.");
            }
            if (VisualTreeHelper.GetParent(dataGridPart) is DataGrid)
            {
                return (DataGrid)VisualTreeHelper.GetParent(dataGridPart);
            }
            else
            {
                return GetDataGridFromChild(VisualTreeHelper.GetParent(dataGridPart));
            }
        }
    }

}



