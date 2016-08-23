using System;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GeneticNetworkTrainerForm
{
    partial class MainForm
    {
        private ListViewColumnSorter LVSColumnSorter;
        private int ListViewStructIslandsSelection = 0;
        private int ListViewStructuresSelection = 0;
        private bool NewStructStatsReady = false;

        private void ListViewStructIslands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListViewStructIslands.SelectedIndices.Count == 0) return;
            ListViewStructIslandsSelection = ListViewStructIslands.SelectedIndices[0];
            NewInternalStatsReady = true;
            NewStructStatsReady = true;
            RedrawStructIslandsPlot();
            PopulateStructs();
            RedrawStructPlot();
            PopulateInternalIslands();
            RedrawInternalIslandsPlot();
            PopulateInternalNets();
            RedrawInternalPlot();

        }
        private void ListViewStructures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListViewStructures.SelectedIndices.Count == 0) return;
            ListViewStructuresSelection = ListViewStructures.SelectedIndices[0];
            NewInternalStatsReady = true;
            RedrawStructPlot();
            PopulateInternalIslands();
            RedrawInternalIslandsPlot();
            PopulateInternalNets();
            RedrawInternalPlot();
        }
        private void ListViewStructures_ColumnClicked(object sender, ColumnClickEventArgs e)
        {
            //// Determine if clicked column is already the column that is being sorted.
            //if (e.Column == LVSColumnSorter.SortColumn)
            //{
            //    // Reverse the current sort direction for this column.
            //    if (LVSColumnSorter.Order == SortOrder.Ascending)
            //    {
            //        LVSColumnSorter.Order = SortOrder.Descending;
            //    }
            //    else
            //    {
            //        LVSColumnSorter.Order = SortOrder.Ascending;
            //    }
            //}
            //else
            //{
            //    // Set the column number that is to be sorted; default to ascending.
            //    LVSColumnSorter.SortColumn = e.Column;
            //    LVSColumnSorter.Order = SortOrder.Ascending;
            //}

            //// Perform the sort with these new sort options.
            //this.ListViewStructures.Sort();
        }
        private void RadioStructIslandScore_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructIslandsPlot();
        }
        private void RadioStructIslandTestScore_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructIslandsPlot();
        }
        private void RadioStructIslandLayers_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructIslandsPlot();
        }
        private void RadioStructIslandNeurons_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructIslandsPlot();
        }
        private void RadioStructIslandSeries_CheckedChanged(object sender, EventArgs e)
        {
            PlotStructureIslandsSeries.BringToFront();
            RedrawStructIslandsPlot();
        }
        private void RadioStructIslandHistogram_CheckedChanged(object sender, EventArgs e)
        {
            PlotStructureIslandsHist.BringToFront();
            RedrawStructIslandsPlot();
        }
        private void RadioStructScore_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructPlot();
        }
        private void RadioStructTestScore_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructPlot();
        }
        private void RadioStructLayer_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructPlot();
        }
        private void RadioStructNeurons_CheckedChanged(object sender, EventArgs e)
        {
            RedrawStructPlot();
        }

        private void RedrawStructIslandsPlot()
        {
            if (RadioStructIslandSeries.Checked)// Series
            {
                if (ListViewStructIslandsSelection >= MyGenTrainer.SettledStatsStructure.StructIslandsStats.Count)
                    ListViewStructIslandsSelection = 0;
                PlotStructureIslandsSeries.Model.Series.Clear();
                if (RadioStructIslandScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "Score", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].ScoreHistory.GetData();
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotStructureIslandsSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                    PlotStructureIslandsSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotStructureIslandsSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotStructureIslandsSeries.Model.Axes[1].Minimum = MinY;
                }
                else if (RadioStructIslandTestScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "TestScore", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].TestScoreHistory.GetData();
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotStructureIslandsSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                    PlotStructureIslandsSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotStructureIslandsSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotStructureIslandsSeries.Model.Axes[1].Minimum = MinY;
                }
                else if (RadioStructIslandLayers.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "Layers", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].LayersHistory.GetData();

                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotStructureIslandsSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                    PlotStructureIslandsSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotStructureIslandsSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotStructureIslandsSeries.Model.Axes[1].Minimum = MinY;
                }
                else
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "Neurons", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].NeuronsHistory.GetData();
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotStructureIslandsSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                    PlotStructureIslandsSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotStructureIslandsSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotStructureIslandsSeries.Model.Axes[1].Minimum = MinY;
                }
                PlotStructureIslandsSeries.Model.InvalidatePlot(true);
            }
            else//Histogram
            {
                PlotStructureIslandsHist.Model.Series.Clear();
                PlotStructureIslandsHist.Model.Axes.Clear();

                if (RadioStructIslandScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    ColumnSeries PlotStructureIslandsSeriesCS = new ColumnSeries { Title = "Score", StrokeThickness = 2 };
                    CategoryAxis NewCategoryAxis = new CategoryAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = HistogramsBins };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].ScoreHistogram;
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < HistogramsBins; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesCS.Items.Add(new ColumnItem(MyData[Cnt]));
                        if (Cnt == HistogramsBins - 1)
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0") + " " + MyData[HistogramsBins + 1 + Cnt].ToString("0.0"));
                        else
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0"));
                    }

                    PlotStructureIslandsHist.Model.Axes.Add(NewCategoryAxis);
                    PlotStructureIslandsHist.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = MinY, Maximum = MaxY + 1 });
                    PlotStructureIslandsHist.Model.Series.Add(PlotStructureIslandsSeriesCS);
                }
                else if (RadioStructIslandTestScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    ColumnSeries PlotStructureIslandsSeriesCS = new ColumnSeries { Title = "TestScore", StrokeThickness = 2 };
                    CategoryAxis NewCategoryAxis = new CategoryAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = HistogramsBins };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].TestScoreHistogram;
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < HistogramsBins; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesCS.Items.Add(new ColumnItem(MyData[Cnt]));
                        if (Cnt == HistogramsBins - 1)
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0") + " " + MyData[HistogramsBins + 1 + Cnt].ToString("0.0"));
                        else
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0"));
                    }

                    PlotStructureIslandsHist.Model.Axes.Add(NewCategoryAxis);
                    PlotStructureIslandsHist.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = MinY, Maximum = MaxY + 1 });
                    PlotStructureIslandsHist.Model.Series.Add(PlotStructureIslandsSeriesCS);
                }
                else if (RadioStructIslandLayers.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    ColumnSeries PlotStructureIslandsSeriesCS = new ColumnSeries { Title = "Layers", StrokeThickness = 2 };
                    CategoryAxis NewCategoryAxis = new CategoryAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = HistogramsBins };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].LayersHistogram;
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < HistogramsBins; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesCS.Items.Add(new ColumnItem(MyData[Cnt]));
                        if (Cnt == HistogramsBins - 1)
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString() + "   " + MyData[HistogramsBins + 1 + Cnt].ToString());
                        else
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString());
                    }

                    PlotStructureIslandsHist.Model.Axes.Add(NewCategoryAxis);
                    PlotStructureIslandsHist.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = MinY, Maximum = MaxY + 1 });
                    PlotStructureIslandsHist.Model.Series.Add(PlotStructureIslandsSeriesCS);
                }
                else if (RadioStructIslandNeurons.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    ColumnSeries PlotStructureIslandsSeriesCS = new ColumnSeries { Title = "Neurons", StrokeThickness = 2 };
                    CategoryAxis NewCategoryAxis = new CategoryAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = HistogramsBins };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].NeuronsHistogram;
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < HistogramsBins; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotStructureIslandsSeriesCS.Items.Add(new ColumnItem(MyData[Cnt]));
                        if (Cnt == HistogramsBins - 1)
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString() + "   " + MyData[HistogramsBins + 1 + Cnt].ToString());
                        else
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString());
                    }

                    PlotStructureIslandsHist.Model.Axes.Add(NewCategoryAxis);
                    PlotStructureIslandsHist.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = MinY, Maximum = MaxY + 1 });
                    PlotStructureIslandsHist.Model.Series.Add(PlotStructureIslandsSeriesCS);
                }
                PlotStructureIslandsHist.Model.InvalidatePlot(true);
            }
        }
        private void RedrawStructPlot()
        {
            if (ListViewStructIslandsSelection >= MyGenTrainer.SettledStatsStructure.StructIslandsStats.Count)
                ListViewStructIslandsSelection = 0;
            if (ListViewStructuresSelection >= MyGenTrainer.SettledStatsStructure.StructIslandsStats[0].StructStats.Count)
                ListViewStructuresSelection = 0;
            PlotStructureSeries.Model.Series.Clear();
            if (RadioStructScore.Checked)
            {
                float MaxY = float.MinValue;
                float MinY = float.MaxValue;
                LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "Score", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].ScoreHistory.GetData();
                if (MyData == null || MyData.Length == 0) return;
                for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                {
                    if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                    if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                    PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                }
                PlotStructureSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                PlotStructureSeries.Model.Axes[0].Maximum = MyData.Length;
                PlotStructureSeries.Model.Axes[1].Maximum = MaxY + 1;
                PlotStructureSeries.Model.Axes[1].Minimum = MinY;
            }
            else if (RadioStructTestScore.Checked)
            {
                float MaxY = float.MinValue;
                float MinY = float.MaxValue;
                LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "TestScore", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].TestScoreHistory.GetData();
                if (MyData == null || MyData.Length == 0) return;
                for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                {
                    if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                    if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                    PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                }
                PlotStructureSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                PlotStructureSeries.Model.Axes[0].Maximum = MyData.Length;
                PlotStructureSeries.Model.Axes[1].Maximum = MaxY + 1;
                PlotStructureSeries.Model.Axes[1].Minimum = MinY;
            }
            else if (RadioStructLayer.Checked)
            {
                float MaxY = float.MinValue;
                float MinY = float.MaxValue;
                LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "Layers", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].LayersHistory.GetData();
                if (MyData == null || MyData.Length == 0) return;
                for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                {
                    if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                    if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                    PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                }
                PlotStructureSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                PlotStructureSeries.Model.Axes[0].Maximum = MyData.Length;
                PlotStructureSeries.Model.Axes[1].Maximum = MaxY + 1;
                PlotStructureSeries.Model.Axes[1].Minimum = MinY;
            }
            else if (RadioStructNeurons.Checked)
            {
                float MaxY = float.MinValue;
                float MinY = float.MaxValue;
                LineSeries PlotStructureIslandsSeriesLS = new LineSeries { Title = "Neurons", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].NeuronsHistory.GetData();
                if (MyData == null || MyData.Length == 0) return;
                for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                {
                    if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                    if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                    PlotStructureIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                }
                PlotStructureSeries.Model.Series.Add(PlotStructureIslandsSeriesLS);
                PlotStructureSeries.Model.Axes[0].Maximum = MyData.Length;
                PlotStructureSeries.Model.Axes[1].Maximum = MaxY + 1;
                PlotStructureSeries.Model.Axes[1].Minimum = MinY;
            }
            PlotStructureSeries.Model.InvalidatePlot(true);
        }
        private void PopulateStructIslands()
        {
            LabelStatsStructIsland.Text = MyGenTrainer.SettledStatsStructure.BestIsland.ToString();
            LabelStructID.Text = MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].BestStructure.ToString();
            LabelIntIsland.Text = MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].StructStats[MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].BestStructure].BestIsland.ToString();
            LabelStatScore.Text = MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].StructStats[MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].BestStructure].InternalIslandsStats[MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].StructStats[MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].BestStructure].BestIsland].ScoreHistory.ReadLastValue().ToString("0.0000");
            LabelStatTestScore.Text = MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].StructStats[MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].BestStructure].InternalIslandsStats[MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].StructStats[MyGenTrainer.SettledStatsStructure.StructIslandsStats[MyGenTrainer.SettledStatsStructure.BestIsland].BestStructure].BestIsland].TestScoreHistory.ReadLastValue().ToString("0.0000");

            if (MainTabControl.SelectedIndex != 3 || !NewStructStatsReady) return;

            ListViewStructIslands.Items.Clear();
            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.CurrNumberStructureIslands; Cnt++)
            {
                ListViewStructIslands.Items.Add(Cnt.ToString());
                ListViewStructIslands.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[Cnt].ScoreHistory.ReadLastValue().ToString());
            }
            ListViewStructIslandsSelection = 0;
            ListViewStructIslands.Items[ListViewStructIslandsSelection].Selected = true;
            ListViewStructIslands.Items[ListViewStructIslandsSelection].Focused = true;
        }
        private void PopulateStructs()
        {
            LabelStructTypeLayers.Text = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].LayersHistory.ReadLastValue().ToString();
            LabelStructTypeNeurons.Text = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].NeuronsHistory.ReadLastValue().ToString();
            if (MyGenTrainer.SettledNetsStructure.Count != 0)
                LabelStructTypeWeights.Text = MyGenTrainer.SettledNetsStructure[ListViewStructIslandsSelection][ListViewStructuresSelection][0][0].GetWeightsNumber().ToString();

            if (MainTabControl.SelectedIndex != 3 || !NewStructStatsReady) return;
            NewStructStatsReady = false;
            NewInternalStatsReady = true;

            ListViewStructures.Items.Clear();
            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.StructurePopulationPerIsland; Cnt++)
            {
                ListViewStructures.Items.Add(Cnt.ToString());
                ListViewStructures.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[Cnt].ScoreHistory.ReadLastValue().ToString());
                ListViewStructures.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[Cnt].TestScoreHistory.ReadLastValue().ToString());
                ListViewStructures.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[Cnt].LayersHistory.ReadLastValue().ToString());
                ListViewStructures.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[Cnt].NeuronsHistory.ReadLastValue().ToString());
            }
            ListViewStructuresSelection = 0;
            ListViewStructures.Items[ListViewStructuresSelection].Selected = true;
            ListViewStructures.Items[ListViewStructuresSelection].Focused = true;
        }
    }
}
