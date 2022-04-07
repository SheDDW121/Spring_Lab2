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
using ClassLibrary;

namespace WPF
{
    public partial class MainWindow : Window
    {
        public static RoutedCommand MakeMeasured = new RoutedCommand("Measured", typeof(WPF.MainWindow));
        public static RoutedCommand MakeSpline = new RoutedCommand("Measured", typeof(WPF.MainWindow));
        ViewData Item = new ViewData();
        OxyPlotData OxyPlot_D;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Item;
            MData_Input.DataContext = Item;
            var enumDataSource = System.Enum.GetValues(typeof(System.Windows.HorizontalAlignment));
        }

        private void Button_Make_Measured(object sender, RoutedEventArgs e)
        {
            try
            {
                Item.Measured = new MeasuredData((int)Item.M_Data[0], Item.M_Data[1], Item.M_Data[2], Item.Uniform, Item.F_Fun);
                Item.Measured_Items.Clear();
                Item.Spline_Der.Clear();
                for (int i = 0; i < Item.Measured.nx; i++)
                {
                    Item.Measured_Items.Add($"x = {Item.Measured.nodes_arr[i].ToString("F3")}\ty = {Item.Measured.values[i].ToString("F3")}");
                }
                Item.Sp_Data = new SplinesData(Item.Measured, new SplineParametres());
                OxyPlot_D = new OxyPlotData(Item.Sp_Data);
                OxyPlot.DataContext = OxyPlot_D;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Make_Spline(object sender, RoutedEventArgs e)
        {
            try
            {
                Item.Sp_Data.Spl_Data = new SplineParametres(Item.Sp_Data.M_Data.start, Item.Sp_Data.M_Data.end, Item.Sp_Par.nx);
                Item.Sp_Data.Spl_Data.integral_limits = Item.Sp_Par.integral_limits;

                Item.Deriv = Item.Sp_Data.Start_MKL();
                Item.Spline_Der.Clear();

                Item.Spline_Der.Add("Левая Точка (а):");
                Item.Spline_Der.Add($"   Значение = {Item.Deriv[0].ToString("F3")}");
                Item.Spline_Der.Add($"   1-я производная = {Item.Deriv[1].ToString("F3")}");
                Item.Spline_Der.Add($"   2-я производная = {Item.Deriv[2].ToString("F3")}\n");

                Item.Spline_Der.Add("Правая Точка (b):");
                Item.Spline_Der.Add($"   Значение = {Item.Deriv[3].ToString("F3")}");
                Item.Spline_Der.Add($"   1-я производная = {Item.Deriv[4].ToString("F3")}");
                Item.Spline_Der.Add($"   2-я производная = {Item.Deriv[5].ToString("F3")}\n");

                Item.Spline_Der.Add("Интегралы:");
                Item.Spline_Der.Add($"   [x1, x2] = {Item.Sp_Data.Integrals_Values[0].ToString("F3")}");
                Item.Spline_Der.Add($"   [x2, x3] = {Item.Sp_Data.Integrals_Values[1].ToString("F3")}");
                Item.Spline_Der.Add($"   [x1, x3] = {Item.Sp_Data.Integrals_Values[2].ToString("F3")}");

                //for (int i = 0; i < 6; i++)
                //{
                //    Item.Spline_Der.Add($"{Item.Deriv[i].ToString("F3")}");
                //}

                OxyPlot_D = new OxyPlotData(Item.Sp_Data);
                OxyPlot.DataContext = OxyPlot_D;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CanMakeMeasured(object sender, CanExecuteRoutedEventArgs e)
        {

            if (Validation.GetHasError(M_1) || Validation.GetHasError(M_2) || Validation.GetHasError(M_3))
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
        }

        private void DoMakeMeasured(object sender, ExecutedRoutedEventArgs e)
        {
            Button_Make_Measured(sender, e);
        }

        private void CanMakeSpline(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Validation.GetHasError(Spline_N) || Validation.GetHasError(X_1) || Validation.GetHasError(X_2) || Validation.GetHasError(X_3))
            {
                e.CanExecute = false;
                return;
            }
            if (Item.Measured_Items.Count == 0)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }

        private void DoMakeSpline(object sender, ExecutedRoutedEventArgs e)
        {
            Button_Make_Spline(sender, e);
        }
    }
}
