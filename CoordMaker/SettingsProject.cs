using System;
using System.Runtime;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Markup;
using System.Globalization;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CoordMaker
{

    public delegate void OnSizeChange(int ysize, int xsize);
    [Serializable]
    public class ProjectContainer : DependencyObject
    {
        public ItemsChangeObservableCollection<CoordObject> CoordsXList { get; set; }
        public ItemsChangeObservableCollection<CoordObject> CoordsYList { get; set; }
        public ItemsChangeObservableCollection<CuttingPoint> CuttingPointList { get; set; }
        public ItemsChangeObservableCollection<PointAddr> FixPointList { get; set; }
        public Int32 Zmax { get; set; }
        public Int32 NullX { get; set; }
        public Int32 NullY { get; set; }
        public Int32 NullZ { get; set; }
        public Int32 UpDownDist { get; set; }
        public Int32 YTimeStab { get; set; }

        public Int32 XSpeed { get; set; }
        public Int32 XAcc { get; set; }
        public Int32 XDec { get; set; }

        public Int32 YSpeed { get; set; }
        public Int32 YAcc { get; set; }
        public Int32 YDec { get; set; }

        public Int32 ZSpeed { get; set; }
        public Int32 ZAcc { get; set; }
        public Int32 ZDec { get; set; }

        public Double ScaleK { get; set; }
        public Double ScaleB { get; set; }
        public Int32 ScaleOverloadLimit { get; set; }
        public Int32 ScaleUnderloadLimit { get; set; }
        public Int32 ScaleOverloadDZTime { get; set; }


        // Dependency Property
        public static readonly DependencyProperty IncMatrLeftXDP = DependencyProperty.Register("IncMatrLeftX", typeof(DynMatrix2D<Int32>), typeof(ProjectContainer), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> IncMatrLeftX
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(IncMatrLeftXDP);
            }
            set { SetValue(IncMatrLeftXDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty IncMatrRightXDP = DependencyProperty.Register("IncMatrRightX", typeof(DynMatrix2D<Int32>), typeof(ProjectContainer), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> IncMatrRightX
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(IncMatrRightXDP);
            }
            set { SetValue(IncMatrRightXDP, value); }
        }


        // Dependency Property
        public static readonly DependencyProperty IncMatrLeftYDP = DependencyProperty.Register("IncMatrLeftY", typeof(DynMatrix2D<Int32>), typeof(ProjectContainer), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> IncMatrLeftY
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(IncMatrLeftYDP);
            }
            set { SetValue(IncMatrLeftYDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty IncMatrRightYDP = DependencyProperty.Register("IncMatrRightY", typeof(DynMatrix2D<Int32>), typeof(ProjectContainer), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> IncMatrRightY
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(IncMatrRightYDP);
            }
            set { SetValue(IncMatrRightYDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty IncMatrLeftZDP = DependencyProperty.Register("IncMatrLeftZ", typeof(DynMatrix2D<Int32>), typeof(ProjectContainer), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> IncMatrLeftZ
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(IncMatrLeftZDP);
            }
            set { SetValue(IncMatrLeftZDP, value); }
        }

        // Dependency Property
        public static readonly DependencyProperty IncMatrRightZDP = DependencyProperty.Register("IncMatrRightZ", typeof(DynMatrix2D<Int32>), typeof(ProjectContainer), new FrameworkPropertyMetadata(null));
        // .NET Property wrapper
        public DynMatrix2D<Int32> IncMatrRightZ
        {
            get
            {
                return (DynMatrix2D<Int32>)GetValue(IncMatrRightZDP);
            }
            set { SetValue(IncMatrRightZDP, value); }
        }

        private OnSizeChange OnSizeChange_hndlr;
        public event OnSizeChange OnSizeChange
        {
            add { lock (this) 
                    { 
                    OnSizeChange_hndlr += value;
                    if (this.OnSizeChange_hndlr != null)
                        this.OnSizeChange_hndlr(CoordsYList.Count, CoordsXList.Count);
                    } 
                }
            remove { lock (this) { OnSizeChange_hndlr -= value; } }
        }


        public String XmlVersion { get; set; }

        public void SetDefaultCoords()
        {
            for (int i = 0; i < 10; i++)
            {
                CoordsXList.Add(new CoordObject(0));
                CoordsYList.Add(new CoordObject(0));
            }
            SetSizes();
        }

        public ProjectContainer()
        {
            CoordsXList = new ItemsChangeObservableCollection<CoordObject>();
            CoordsYList = new ItemsChangeObservableCollection<CoordObject>();          
            CuttingPointList = new ItemsChangeObservableCollection<CuttingPoint>();
            FixPointList = new ItemsChangeObservableCollection<PointAddr>();

            Zmax = 755;
            NullX = 10; NullY = 5; NullZ = 755;
            UpDownDist = 80;
            YTimeStab = 1000;
            XSpeed = 700;
            XAcc = 200;
            XDec = 200;
            YSpeed = 300;
            YAcc = 300;
            YDec = 300;
            ZSpeed = 200;
            ZAcc = 200;
            ZDec = 200;
            ScaleK = 0.000005;
            ScaleB = -102.36;
            ScaleOverloadLimit = 2000;
            ScaleUnderloadLimit = 80;
            ScaleOverloadDZTime = 2000;
            XmlVersion = "55543";

            IncMatrLeftX = new DynMatrix2D<int>();
            IncMatrRightX = new DynMatrix2D<int>();

            IncMatrLeftY = new DynMatrix2D<int>();
            IncMatrRightY = new DynMatrix2D<int>();

            IncMatrLeftZ = new DynMatrix2D<int>();
            IncMatrRightZ = new DynMatrix2D<int>();

            CoordsXList.CollectionChanged += new NotifyCollectionChangedEventHandler(CoordsXList_CollectionChanged);
            CoordsYList.CollectionChanged += new NotifyCollectionChangedEventHandler(CoordsYList_CollectionChanged);
        }

        void CoordsYList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetSizes();
        }

        void CoordsXList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            SetSizes();
        }

        public void IncX(Int32 pos, Int32 inc=1)
        {
            for (int i = pos; i < CoordsXList.Count; i++)
                CoordsXList[i].Value += inc;
        }

        public void DecX(Int32 pos, Int32 inc = 1)
        {
            for (int i = pos; i < CoordsXList.Count; i++)
                CoordsXList[i].Value -= inc;
        }

        public void IncY(Int32 pos, Int32 inc = 1)
        {
            for (int i = pos; i < CoordsYList.Count; i++)
                CoordsYList[i].Value += inc;
        }

        public void DecY(Int32 pos, Int32 inc = 1)
        {
            for (int i = pos; i < CoordsYList.Count; i++)
                CoordsYList[i].Value -= inc;
        }

        public void Update()
        {
            SetSizes();
        }

        protected void SetSizes()
        {
            IncMatrLeftX.setsizes(CoordsYList.Count, CoordsXList.Count);
            IncMatrRightX.setsizes(CoordsYList.Count, CoordsXList.Count);

            IncMatrLeftY.setsizes(CoordsYList.Count, CoordsXList.Count);
            IncMatrRightY.setsizes(CoordsYList.Count, CoordsXList.Count);

            IncMatrLeftZ.setsizes(CoordsYList.Count, CoordsXList.Count);
            IncMatrRightZ.setsizes(CoordsYList.Count, CoordsXList.Count);

            if (this.OnSizeChange_hndlr != null)
                this.OnSizeChange_hndlr(CoordsYList.Count, CoordsXList.Count);
        }

        public void Import(String filename)
        {
            var xmlbuf = GetXML();
            xmlbuf.Save(filename);
        }

        private List<XElement> MakeCells()
        {
            
     /*       try
            {*/
                // Build left and right cell matrix numbers
                Int32[,] cellmatr_left = new Int32[CoordsYList.Count, CoordsXList.Count];
                Int32[,] cellmatr_right = new Int32[CoordsYList.Count, CoordsXList.Count];
                int[,] Matr_Left = new int[CoordsYList.Count, CoordsXList.Count];
                int[,] Matr_Right = new int[CoordsYList.Count, CoordsXList.Count];

                int c = 0;
                int x = 0;
                int y = 0;

                for (x = 0; x < CoordsXList.Count; x++)
                {
                    for (y = 0; y < CoordsYList.Count; y++)
                    {
                        Matr_Left[y, x] = -2;
                        Matr_Right[y, x] = -2;
                    }
                }

                for (int i = 0; i < FixPointList.Count; i++) 
                {
                    if (FixPointList[i].Right)
                    {
                        Matr_Right[FixPointList[i].X, FixPointList[i].Y] = FixPointList[i].CellID;
                    }
                    else 
                    {
                        Matr_Left[FixPointList[i].X, FixPointList[i].Y] = FixPointList[i].CellID;
                    }
                }


                x = 0;
                y = 0;
            
                bool stop = false;
                // left walking
                y = 0;
                while (!stop)
                {
                    bool move = true;
                    if (Matr_Left[y, x] > -2)
                    { }
                    else
                    {
                        IEnumerable<PointAddr> InFixed = this.FixPointList.Where(p => (p.CellID == c));
                        if (InFixed.Count() == 0)
                        {

                            IEnumerable<CuttingPoint> Empty = CuttingPointList.Where(p => ((p.X == x) && (p.Y == y) && (p.Right == false)));
                            if (Empty.Count() > 0) // spend the cell
                            {
                                Matr_Left[y, x] = -1;
                            }
                            else
                            {

                                Matr_Left[y, x] = c;

                                c++;
                            }
                        }
                        else
                            c++;
                    }
                    if (move)
                    {
                        y++;
                        if (y >= CoordsYList.Count)
                        {
                            y = 0;
                            x++;
                            if (x >= CoordsXList.Count) 
                                stop = true;
                        }
                    }
                }                           

                stop = false;
                // right walking
                x = 0;
                y = 0;
                while (!stop)
                {
                    bool move = true;
                    if (Matr_Right[y, x] > -2)
                    { }
                    else
                    {
                        IEnumerable<PointAddr> InFixed = this.FixPointList.Where(p => (p.CellID == c));
                        if (InFixed.Count() == 0)
                        {
                            IEnumerable<CuttingPoint> Empty = CuttingPointList.Where(p => ((p.X == x) && (p.Y == y) && (p.Right = true)));
                            if (Empty.Count() > 0) // spend the cell
                            {
                                Matr_Right[y, x] = -1;
                            }
                            else
                            {
                                Matr_Right[y, x] = c;

                                c++;
                            }
                        }
                        else
                            c++;
                    }
                    if (move)
                    {
                        y++;
                        if (y >= CoordsYList.Count)
                        {
                            y = 0;
                            x++;
                            if (x >= CoordsXList.Count) stop = true;
                        }
                    }
                 }

                List<XElement> cells = new List<XElement>();
                
                 for (Int32 _x = 0; _x < CoordsXList.Count; _x++)
                 {
                     for (Int32 _y = 0; _y < CoordsYList.Count; _y++)
                     {
                        if (Matr_Left[_y, _x] < 0) continue;
                        var cellcoords = new XElement("cell");
                        cellcoords.SetAttributeValue("ID", Matr_Left[_y, _x]);
                        cellcoords.SetAttributeValue("X", CoordsXList[_x].Value + IncMatrLeftX[_y,_x]);
                        cellcoords.SetAttributeValue("Y", CoordsXList[_y].Value + IncMatrLeftY[_y, _x]);
                        cellcoords.SetAttributeValue("Z", Zmax + IncMatrLeftZ[_y, _x]);
                        
                        cells.Add(cellcoords);
                    }
                }

                for (Int32 _x = 0; _x < CoordsXList.Count; _x++)
                {
                    for (Int32 _y = 0; _y < CoordsYList.Count; _y++)
                    {
                        if (Matr_Right[_y, _x] < 0) continue;
                        var cellcoords = new XElement("cell");
                        cellcoords.SetAttributeValue("ID", Matr_Right[_y, _x]);
                        cellcoords.SetAttributeValue("X", CoordsXList[_x].Value + IncMatrRightX[_y, _x]);
                        cellcoords.SetAttributeValue("Y", CoordsXList[_y].Value + IncMatrRightY[_y, _x]);
                        cellcoords.SetAttributeValue("Z", -Zmax + IncMatrRightZ[_y, _x]);
                        cells.Add(cellcoords);
                    }
                }
    /*        }
            catch (System.Exception exc)
            {

            }

*/

            return cells;
        }


        private XElement GetXML()
        {
            // Создать XML 
            var xmlbuf = new XElement("trans");
            xmlbuf.SetAttributeValue("ID", "GENERAL");
            xmlbuf.SetAttributeValue("version", XmlVersion);

            var settings = new XElement("settings");

            var ZeroCell = new XElement("ZeroCell");
            ZeroCell.SetAttributeValue("X", NullX);
            ZeroCell.SetAttributeValue("Y", NullY);
            ZeroCell.SetAttributeValue("Z", NullZ);
            settings.Add(ZeroCell);

            var MovX = new XElement("MovingX");
            MovX.SetAttributeValue("Speed", XSpeed);
            MovX.SetAttributeValue("Acc", XAcc);
            MovX.SetAttributeValue("Dec", XDec);

            var MovY = new XElement("MovingY");
            MovY.SetAttributeValue("Speed", YSpeed);
            MovY.SetAttributeValue("Acc", YAcc);
            MovY.SetAttributeValue("Dec", YDec);

            var MovZ = new XElement("MovingZ");
            MovZ.SetAttributeValue("Speed", ZSpeed);
            MovZ.SetAttributeValue("Acc", ZAcc);
            MovZ.SetAttributeValue("Dec", ZDec);

            var Scale = new XElement("Scales");
            Scale.SetAttributeValue("K", ScaleK);
            Scale.SetAttributeValue("B", ScaleB);
            Scale.SetAttributeValue("OverloadLimit", ScaleOverloadLimit);
            Scale.SetAttributeValue("UnderloadLimit", ScaleUnderloadLimit);
            Scale.SetAttributeValue("OverloadDZTime", ScaleOverloadDZTime);

            var Up = new XElement("Up");
            Up.SetAttributeValue("UpDownDist", UpDownDist);
            Up.SetAttributeValue("YTimeStab", YTimeStab);

            settings.Add(MovX);

            settings.Add(MovY);

            settings.Add(MovZ);

            settings.Add(Scale);

            settings.Add(Up);

            xmlbuf.Add(new XComment("Settings"));
            xmlbuf.Add(settings);

            xmlbuf.Add(new XComment("Cells coordinates"));
            // Cells
            List<XElement> cells = MakeCells();
            foreach(XElement xe in cells)
            {
                xmlbuf.Add(xe);
            }
            // movestepX
            xmlbuf.Add(new XComment("MoveStep X"));
            for (int i = 0; i < CoordsXList.Count; i++)
            {
                var MovStepX = new XElement("movestepX");
                MovStepX.SetAttributeValue("ID", i);
                MovStepX.SetAttributeValue("X", CoordsXList[i].Value);
                xmlbuf.Add(MovStepX);
            }

            // movestepY
            xmlbuf.Add(new XComment("MoveStep Y"));
            for (int i = 0; i < CoordsYList.Count; i++)
            {
                var MovStepX = new XElement("movestepY");
                MovStepX.SetAttributeValue("ID", i);
                MovStepX.SetAttributeValue("Y", CoordsYList[i].Value);
                xmlbuf.Add(MovStepX);
            }

            // movestepZ
            xmlbuf.Add(new XComment("MoveStep Z"));
            var MovStepZ1 = new XElement("movestepZ");
            MovStepZ1.SetAttributeValue("ID", 0);
            MovStepZ1.SetAttributeValue("Z", -Zmax);
            xmlbuf.Add(MovStepZ1);

            MovStepZ1 = new XElement("movestepZ");
            MovStepZ1.SetAttributeValue("ID", 1);
            MovStepZ1.SetAttributeValue("Z", 0);
            xmlbuf.Add(MovStepZ1);

            MovStepZ1 = new XElement("movestepZ");
            MovStepZ1.SetAttributeValue("ID", 2);
            MovStepZ1.SetAttributeValue("Z", Zmax);
            xmlbuf.Add(MovStepZ1);

            return xmlbuf;
        }

    }

}
