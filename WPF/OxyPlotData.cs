using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Legends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using OxyPlot.Axes;

namespace WPF
{
    public class OxyPlotData
    {
        public SplinesData data = null;
        public PlotModel plotModel { get; private set; }
        public OxyPlotData(SplinesData data)
        {
            this.data = data;
            this.plotModel = new PlotModel { Title = "Nodes and splines" };

            AddMeasured();
            if (data.Spl_Data.nodes_arr != null)
            {
                AddSpline();
            }
            // plotModel.InvalidatePlot(true);
        }
        public void AddMeasured()
        {
            this.plotModel.Series.Clear();
            OxyColor color = OxyColors.Red;
            LineSeries lineSeries = new LineSeries();
            for (int j = 0; j < data.M_Data.nx; j++) lineSeries.Points.Add(new DataPoint(data.M_Data.nodes_arr[j], data.M_Data.values[j]));
            lineSeries.Color = color;

            lineSeries.MarkerType = MarkerType.Circle;
            lineSeries.MarkerSize = 4;
            lineSeries.MarkerStroke = color;
            lineSeries.MarkerFill = color;
            lineSeries.LineStyle = LineStyle.None;
            lineSeries.Title = "Measured Data";

            
            Legend legend = new Legend();
            plotModel.Legends.Add(legend);
            this.plotModel.Series.Add(lineSeries);
            AddAxes(plotModel);

        }

        public void AddSpline()
        {
            //this.plotModel.Series.Clear();
            OxyColor color = OxyColors.Blue;
            LineSeries lineSeries = new LineSeries();
            for (int j = 0; j < data.Spl_Data.nx; j++) lineSeries.Points.Add(new DataPoint(data.Spl_Data.nodes_arr[j], data.Values[j]));
            lineSeries.Color = color;

            lineSeries.MarkerType = MarkerType.Circle;
            lineSeries.MarkerSize = 0;
            lineSeries.MarkerStroke = color;
            lineSeries.MarkerFill = color;
            lineSeries.LineStyle = LineStyle.Solid;
            lineSeries.Title = "Spline Data (Free Ends)";


            //Legend legend = new Legend();
            //plotModel.Legends.Add(legend);
            this.plotModel.Series.Add(lineSeries);
        }

        private static void AddAxes(PlotModel plotModel)
        {
            plotModel.Axes.Add(new LinearAxis
            {
                Title = "x",
                TitleFontSize = 14,
                TitleFontWeight = FontWeights.Bold,
                AxisTitleDistance = 15,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Solid,
                Position = AxisPosition.Bottom,
            });

            plotModel.Axes.Add(new LinearAxis
            {
                Title = "y",
                TitleFontSize = 14,
                TitleFontWeight = FontWeights.Bold,
                AxisTitleDistance = 15,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Solid,
                Position = AxisPosition.Left
            });
        }
    }
}
