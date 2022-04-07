using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace WPF
{
    class ViewData: IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void PropertiesChanged()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("M_Data_0"));
            PropertyChanged(this, new PropertyChangedEventArgs("M_Data_1"));
            PropertyChanged(this, new PropertyChangedEventArgs("M_Data_2"));

            PropertyChanged(this, new PropertyChangedEventArgs("Sp_I_0"));
            PropertyChanged(this, new PropertyChangedEventArgs("Sp_I_1"));
            PropertyChanged(this, new PropertyChangedEventArgs("Sp_I_2"));
        }
        public string Error { get { return "Error"; } }

        public string this[string property] //I know, that here much better was to use switch, not a lot of if's, whatever
        {
            get
            {
                string msg = null;
                if (property == "M_Data_1")
                {
                    if (M_Data_1 >= M_Data_2)
                    {
                        msg = "a must be less than b";
                    }
                }
                else if (property == "M_Data_2")
                {
                    if (M_Data_2 <= M_Data_1)
                    {
                        msg = "b must be greater than a";
                    }
                }
                else if (property == "M_Data_0")
                {
                    if (M_Data_0 <= 2)
                    {
                        msg = "nx must be greater than 2";
                    }
                }

                else if (property == "nx_spl")
                {
                    if (nx_spl <= 2)
                    {
                        msg = "number of spline nodes must be greater than 2";
                    }
                }
                else if (property == "Sp_I_0")
                {
                    if (Sp_I_0 < M_Data_1)
                    {
                        msg = "x1 must be not less than a";
                    }

                    else if (Sp_I_0 >= Sp_I_1)
                    {
                        msg = "x1 must be less than x2";
                    }
                }
                else if (property == "Sp_I_1")
                {
                    if (Sp_I_1 <= Sp_I_0)
                    {
                        msg = "x2 must be not less than x1";
                    }

                    else if (Sp_I_1 >= Sp_I_2)
                    {
                        msg = "x2 must be less than x3";
                    }
                }
                else if (property == "Sp_I_2")
                {
                    if (Sp_I_2 <= Sp_I_1)
                    {
                        msg = "x3 must be greater than x2";
                    }

                    else if (Sp_I_2 > M_Data_2)
                    {
                        msg = "x3 must be not greater than b";
                    }
                }
                return msg;
            }
        }

        //ViewData()
        //{
        //    M_Data = new MeasuredData();
        //    Sp_Par = new SplineParametres();
        //    Sp_Data = new SplinesData();
        //}
        public SplineParametres Sp_Par { get; set; } = new SplineParametres();
        public SplinesData Sp_Data { get; set; }
        public ObservableCollection<string> Measured_Items { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Spline_Der { get; set; } = new ObservableCollection<string>();
        public double[] Deriv { get; set; } = new double[6];
        public MeasuredData Measured { get; set; }
        public double[] M_Data { get; set; } = new double[3] {10, 0, 2};
        public double M_Data_0
        {
            get => M_Data[0];
            set
            {
                M_Data[0] = value;
            }
        }
        public double M_Data_1
        {
            get => M_Data[1];
            set
            {
                M_Data[1] = value;
                PropertiesChanged();
            }
        }
        public double M_Data_2
        {
            get => M_Data[2];
            set
            {
                M_Data[2] = value;
                PropertiesChanged();
            }
        }

        public int nx_spl
        {
            get => Sp_Par.nx;
            set
            {
                Sp_Par.nx = value;
            }
        }
        public double Sp_I_0
        {
            get => Sp_Par.integral_limits[0];
            set
            {
                Sp_Par.integral_limits[0] = value;
                PropertiesChanged();
            }
        }

        public double Sp_I_1
        {
            get => Sp_Par.integral_limits[1];
            set
            {
                Sp_Par.integral_limits[1] = value;
                PropertiesChanged();
            }
        }

        public double Sp_I_2
        {
            get => Sp_Par.integral_limits[2];
            set
            {
                Sp_Par.integral_limits[2] = value;
                PropertiesChanged();
            }
        }

        public Spf F_Fun { get; set; }
        public bool Uniform { get => !not_Uniform; }
        public double Test { get; set; }
        public bool not_Uniform { get; set; }

    }
}
