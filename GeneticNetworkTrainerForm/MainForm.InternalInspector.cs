using System;
using System.Drawing;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using GeneticNetworkTrainer;

namespace GeneticNetworkTrainerForm
{
    partial class MainForm
    {
        private int ListViewInternalIslandsSelection = 0;
        private bool NewInternalStatsReady = false;

        private void ListViewInternalIslands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListViewInternalIslands.SelectedIndices.Count == 0) return;
            ListViewInternalIslandsSelection = ListViewInternalIslands.SelectedIndices[0];
            NewInternalStatsReady = true;
            RedrawInternalIslandsPlot();
            PopulateInternalNets();
            RedrawInternalPlot();
        }
        private void ListViewNets_ItemChecked(object sender, EventArgs e)
        {
            RedrawInternalPlot();
            if (ListViewNets.CheckedIndices.Count > 0)
            {
                LabelMessage.Text = "Fill In the input By hand or select an input from the Slider";
                SliderHandData_Scroll(null, null);
                ButtonEvaluate.BackColor = Color.LawnGreen;
            }
            else
            {
                LabelMessage.Text = "Please Select a net from the inernal inspector.";
                ButtonEvaluate.BackColor = Color.Silver;
            }
        }
        private void RadioInternalScore_CheckedChanged(object sender, EventArgs e)
        {
            RedrawInternalIslandsPlot();
        }
        private void RadioInternalTestScore_CheckedChanged(object sender, EventArgs e)
        {
            RedrawInternalIslandsPlot();
        }
        private void RadioInternalSeries_CheckedChanged(object sender, EventArgs e)
        {
            PlotInternalIslandsSeries.BringToFront();
            RedrawInternalIslandsPlot();
        }
        private void RadioInternalHistogram_CheckedChanged(object sender, EventArgs e)
        {
            PlotInternalIslandsHist.BringToFront();
            RedrawInternalIslandsPlot();
        }
        private void ButtonLogSelected_Click(object sender, EventArgs e)
        {
            if (ListViewNets.CheckedIndices.Count == 0)
            {
                AppendFeedback("please select at least one net", 1);
                return;
            }
            for (int Cnt = 0; Cnt < ListViewNets.CheckedIndices.Count; Cnt++)
            {
                AppendFeedback(string.Format("Adjacency matrix of Net :({0}, {1}, {2}, {3}) ", ListViewStructIslandsSelection, ListViewStructuresSelection, ListViewInternalIslandsSelection, ListViewNets.CheckedIndices[Cnt]), 0);
                AppendFeedback(MyGenTrainer.SettledNetsStructure[ListViewStructIslandsSelection][ListViewStructuresSelection][ListViewInternalIslandsSelection][ListViewNets.CheckedIndices[Cnt]].LogMeDesciption(), 0);
                if (!CheckBoxLogStructOnly.Checked)
                {
                    AppendFeedback(string.Format("Params of Net :({0}, {1}, {2}, {3}) ", ListViewStructIslandsSelection, ListViewStructuresSelection, ListViewInternalIslandsSelection, ListViewNets.CheckedIndices[Cnt]), 0);
                    AppendFeedback(MyGenTrainer.SettledNetsStructure[ListViewStructIslandsSelection][ListViewStructuresSelection][ListViewInternalIslandsSelection][ListViewNets.CheckedIndices[Cnt]].LogMeParams(), 0);
                }
            }

        }
        private void ButtonSaveSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListViewNets.CheckedIndices.Count == 0)
                    AppendFeedback(" Please select a net to export.", 1);
                else
                {
                    for (int Cnt = 0; Cnt < ListViewNets.CheckedIndices.Count; Cnt++)
                    {
                        GenNetwork NetToSave = MyGenTrainer.SettledNetsStructure[ListViewStructIslandsSelection][ListViewStructuresSelection][ListViewInternalIslandsSelection][ListViewNets.CheckedIndices[Cnt]].CloneMe(false, false, false, null);
                        string NetFileName = "\\I" + MyGenTrainer.MyState.InData[0].Length + "O" + MyGenTrainer.MyState.LabelData[0].Length + "_L" + NetToSave.GetLayersCount() + "N" + NetToSave.GetNeuronsCount() + "_D" + DateTime.Now.ToString("dd.MM.yy") + "[" + NetToSave.GetScore() + "_" + NetToSave.GetTestScore() + " ]";
                        NetToSave.ExportNet(NetFileName.Replace(":", "."));
                        AppendFeedback(" Net " + "[" + ListViewStructIslandsSelection + "_" + ListViewStructuresSelection + "_" + ListViewInternalIslandsSelection + "_" + ListViewNets.CheckedIndices[Cnt] + " ] Exported Succesfully.", 0);
                    }
                }
            }
            catch (Exception Ex)
            {
                AppendFeedback(" Failed to export Net. Message: " + Ex.Message + "\n Trace: " + GetTrace(Ex), 2);
            }
        }
        private void RedrawInternalIslandsPlot()
        {
            if (RadioInternalSeries.Checked)// Series
            {
                PlotInternalIslandsSeries.Model.Series.Clear();
                if (RadioInternalScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    LineSeries PlotInternalIslandsSeriesLS = new LineSeries { Title = "Score", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].ScoreHistory.GetData();
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotInternalIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotInternalIslandsSeries.Model.Series.Add(PlotInternalIslandsSeriesLS);
                    PlotInternalIslandsSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotInternalIslandsSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotInternalIslandsSeries.Model.Axes[1].Minimum = MinY;
                }
                else if (RadioInternalTestScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    LineSeries PlotInternalIslandsSeriesLS = new LineSeries { Title = "Test Score", StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].TestScoreHistory.GetData();
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotInternalIslandsSeriesLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotInternalIslandsSeries.Model.Series.Add(PlotInternalIslandsSeriesLS);
                    PlotInternalIslandsSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotInternalIslandsSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotInternalIslandsSeries.Model.Axes[1].Minimum = MinY;
                }
                PlotInternalIslandsSeries.Model.InvalidatePlot(true);
            }
            else//Histogram
            {
                PlotInternalIslandsHist.Model.Series.Clear();
                PlotInternalIslandsHist.Model.Axes.Clear();

                if (RadioInternalScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    ColumnSeries PlotInternalIslandsSeriesCS = new ColumnSeries { Title = "Score", StrokeThickness = 2 };
                    CategoryAxis NewCategoryAxis = new CategoryAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = HistogramsBins };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].ScoreHistogram;
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < HistogramsBins; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotInternalIslandsSeriesCS.Items.Add(new ColumnItem(MyData[Cnt]));
                        if (Cnt == HistogramsBins - 1)
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0") + " " + MyData[HistogramsBins + 1 + Cnt].ToString("0.0"));
                        else
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0"));
                    }

                    PlotInternalIslandsHist.Model.Axes.Add(NewCategoryAxis);
                    PlotInternalIslandsHist.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = MinY, Maximum = MaxY + 1 });
                    PlotInternalIslandsHist.Model.Series.Add(PlotInternalIslandsSeriesCS);
                }
                else if (RadioInternalTestScore.Checked)
                {
                    float MaxY = float.MinValue;
                    float MinY = float.MaxValue;
                    ColumnSeries PlotInternalIslandsSeriesCS = new ColumnSeries { Title = "Test Score", StrokeThickness = 2 };
                    CategoryAxis NewCategoryAxis = new CategoryAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = HistogramsBins };

                    float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].TestScoreHistogram;
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < HistogramsBins; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotInternalIslandsSeriesCS.Items.Add(new ColumnItem(MyData[Cnt]));
                        if (Cnt == HistogramsBins - 1)
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0") + " " + MyData[HistogramsBins + 1 + Cnt].ToString("0.0"));
                        else
                            NewCategoryAxis.Labels.Add(MyData[HistogramsBins + Cnt].ToString("0.0"));
                    }

                    PlotInternalIslandsHist.Model.Axes.Add(NewCategoryAxis);
                    PlotInternalIslandsHist.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = MinY, Maximum = MaxY + 1 });
                    PlotInternalIslandsHist.Model.Series.Add(PlotInternalIslandsSeriesCS);
                }
                PlotInternalIslandsHist.Model.InvalidatePlot(true);
            }
        }
        private void RedrawInternalPlot()
        {
            PlotInternalSeries.Model.Series.Clear();
            if (ListViewNets.CheckedIndices.Count > 1)// Show only score from all selected networks
            {
                float MaxY = float.MinValue;
                float MinY = float.MaxValue;
                float[] MyData = null;
                for (int GraphCnt = 0; GraphCnt < ListViewNets.CheckedIndices.Count && GraphCnt < 5; GraphCnt++)
                {
                    LineSeries PlotInternalNetsLS = new LineSeries { Title = ListViewNets.CheckedIndices[GraphCnt] + "-Score", StrokeThickness = 2, Color = GetOxyColor(GraphCnt) };
                    MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[ListViewNets.CheckedIndices[GraphCnt]].ScoreHistory.GetData();
                    if (MyData == null || MyData.Length == 0) return;
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotInternalNetsLS.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotInternalSeries.Model.Series.Add(PlotInternalNetsLS);
                    PlotInternalSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotInternalSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotInternalSeries.Model.Axes[1].Minimum = MinY;
                }
            }
            else if (ListViewNets.CheckedIndices.Count == 1)//Show all graphs of one network
            {
                float MaxY = float.MinValue;
                float MinY = float.MaxValue;
                LineSeries PlotInternalNetScore = new LineSeries { Title = "Score", StrokeThickness = 2, Color = GetOxyColor(0) };
                float[] MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[ListViewNets.CheckedIndices[0]].ScoreHistory.GetData();
                if (MyData == null || MyData.Length == 0) return;
                for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                {
                    if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                    if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                    PlotInternalNetScore.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                }
                PlotInternalSeries.Model.Series.Add(PlotInternalNetScore);
                PlotInternalSeries.Model.Axes[0].Maximum = MyData.Length;
                PlotInternalSeries.Model.Axes[1].Maximum = MaxY + 1;
                PlotInternalSeries.Model.Axes[1].Minimum = MinY;
                if (CheckBoxHalfForTesting.Checked)
                {
                    LineSeries PlotInternalNetTstScore = new LineSeries { Title = "Test Score", StrokeThickness = 2, Color = GetOxyColor(1) };
                    MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[ListViewNets.CheckedIndices[0]].TestScoreHistory.GetData();
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotInternalNetTstScore.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotInternalSeries.Model.Series.Add(PlotInternalNetTstScore);
                    PlotInternalSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotInternalSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotInternalSeries.Model.Axes[1].Minimum = MinY;
                }
                LineSeries PlotInternalNetOuterr = new LineSeries { Title = "Out Error", StrokeThickness = 2, Color = GetOxyColor(2) };
                MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[ListViewNets.CheckedIndices[0]].OutErrorHistory.GetData();
                for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                {
                    if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                    if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                    PlotInternalNetOuterr.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                }
                PlotInternalSeries.Model.Series.Add(PlotInternalNetOuterr);
                PlotInternalSeries.Model.Axes[0].Maximum = MyData.Length;
                PlotInternalSeries.Model.Axes[1].Maximum = MaxY + 1;
                PlotInternalSeries.Model.Axes[1].Minimum = MinY;
                if (CheckBoxHalfForTesting.Checked)
                {
                    LineSeries PlotInternalNetTstOutErr = new LineSeries { Title = "Test Out Error", StrokeThickness = 2, Color = GetOxyColor(3) };
                    MyData = MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[ListViewNets.CheckedIndices[0]].TestOutErrorHistory.GetData();
                    for (int Cnt = 0; Cnt < MyData.Length; Cnt++)
                    {
                        if (MyData[Cnt] > MaxY) MaxY = MyData[Cnt];
                        if (MyData[Cnt] < MinY) MinY = MyData[Cnt];
                        PlotInternalNetTstOutErr.Points.Add(new DataPoint(Cnt, MyData[Cnt]));
                    }
                    PlotInternalSeries.Model.Series.Add(PlotInternalNetTstOutErr);
                    PlotInternalSeries.Model.Axes[0].Maximum = MyData.Length;
                    PlotInternalSeries.Model.Axes[1].Maximum = MaxY + 1;
                    PlotInternalSeries.Model.Axes[1].Minimum = MinY;
                }
            }
            PlotInternalSeries.Model.InvalidatePlot(true);
        }
        private OxyColor GetOxyColor(int Index)
        {
            if (Index == 0) return OxyColor.FromRgb(255, 0, 0);
            else if (Index == 1) return OxyColor.FromRgb(255, 255, 0);
            else if (Index == 2) return OxyColor.FromRgb(0, 255, 0);
            else if (Index == 3) return OxyColor.FromRgb(0, 255, 255);
            else return OxyColor.FromRgb(0, 0, 255);
        }
        private void PopulateInternalIslands()
        {
            if (MainTabControl.SelectedIndex != 5 || !NewInternalStatsReady) return;

            ListViewInternalIslands.Items.Clear();

            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.InitialNumberInternalIslands; Cnt++)
            {
                ListViewInternalIslands.Items.Add(Cnt.ToString());
                ListViewInternalIslands.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[Cnt].ScoreHistory.ReadLastValue().ToString());
            }
            ListViewInternalIslandsSelection = 0;
            ListViewInternalIslands.Items[ListViewInternalIslandsSelection].Selected = true;
            ListViewInternalIslands.Items[ListViewInternalIslandsSelection].Focused = true;
        }
        private void PopulateInternalNets()
        {
            if (MainTabControl.SelectedIndex != 5 || !NewInternalStatsReady) return;
            NewInternalStatsReady = false;

            ListViewNets.Items.Clear();
            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.InternalPopulationPerIsland; Cnt++)
            {
                ListViewNets.Items.Add(Cnt.ToString());
                ListViewNets.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[Cnt].ScoreHistory.ReadLastValue().ToString());
                ListViewNets.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[Cnt].TestScoreHistory.ReadLastValue().ToString());
                ListViewNets.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[Cnt].OutErrorHistory.ReadLastValue().ToString());
                ListViewNets.Items[Cnt].SubItems.Add(MyGenTrainer.SettledStatsStructure.StructIslandsStats[ListViewStructIslandsSelection].StructStats[ListViewStructuresSelection].InternalIslandsStats[ListViewInternalIslandsSelection].NetStats[Cnt].TestOutErrorHistory.ReadLastValue().ToString());
            }
        }
    }
}
