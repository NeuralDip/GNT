using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using GeneticNetworkTrainer;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Annotations;
using OxyPlot.Axes;

namespace GeneticNetworkTrainerForm
{
    partial class MainForm
    {
        private Dictionary<int, float[]> LayersDrawPositions;
        private List<int> ListToLayers;
        private int LayerInListSelected = -1;
        private float[] DrawingConectionCoords;

        private bool MouseLeftClicked = false;
        private bool MouseRightClicked = false;

        private void ButtonResetLayersPositions_Click(object sender, EventArgs e)
        {
            DrawGraphPlot(true);
        }
        private void ButtonInitNetAddLayer_Click(object sender, EventArgs e)
        {
            int NewID = MyGenTrainer.MyState.BaseGenNetwork.HCAddLayer();
            int OldID;
            ListToLayers = MyGenTrainer.MyState.BaseGenNetwork.GetAllLayersIDs();
            if (LayerInListSelected != -1)
            {
                OldID = ListToLayers[LayerInListSelected];
                LayerInListSelected = ListToLayers.IndexOf(OldID);
            }
            LayersDrawPositions.Add(NewID, new float[] { 0.1f, 0.1f });
            DrawGraphPlot(false);
            PopulateLayerData();
            MyAppendColoredText(InitNetConsole, "Layer with id: " + NewID + " added.", Color.White);
        }
        private void ButtonInitNetRemoveLayer_Click(object sender, EventArgs e)
        {
            int IDRemoving = ListToLayers[LayerInListSelected];
            int OldID = ListToLayers[LayerInListSelected];
            if (!MyGenTrainer.MyState.BaseGenNetwork.HCRemoveLayer(IDRemoving))
            {
                MyAppendColoredText(InitNetConsole, "Cannot Remove In or Out Layer.", Color.Yellow);
                return;
            }
            ListToLayers = MyGenTrainer.MyState.BaseGenNetwork.GetAllLayersIDs();
            LayerInListSelected = ListToLayers.IndexOf(OldID);
            LayersDrawPositions.Remove(IDRemoving);
            DrawGraphPlot(false);
            PopulateLayerData();
            MyAppendColoredText(InitNetConsole, "Layer with id: " + IDRemoving + " removed.", Color.White);
        }
        private void TextBoxInitNetNeuronNumber_TextChanged(object sender, EventArgs e)
        {
            if (LayerInListSelected == -1) return;
            int NewValue;
            if (!int.TryParse(TextBoxInitNetNeuronNumber.Text, out NewValue) || NewValue < 1)
                TextBoxInitNetNeuronNumber.Text = MyGenTrainer.MyState.BaseGenNetwork.GetLayerNeuronsCount(ListToLayers[LayerInListSelected]).ToString();
            else
            {
                MyGenTrainer.MyState.BaseGenNetwork.SetLayerNeuronsCount(ListToLayers[LayerInListSelected], NewValue);
                PopulateLayerData();
                DrawGraphPlot(false);
            }
        }

        private void Model_MouseUp(object sender, OxyMouseEventArgs e)
        {
            MouseLeftClicked = false;
            if (MouseRightClicked)
            {
                MouseRightClicked = false;
                DataPoint MyDataPoint = GetDataPointFromMouse(e);
                int FromID = ListToLayers[LayerInListSelected];
                int HitResult = MyHitTest(e, MyDataPoint);
                if (HitResult != -1)
                {
                    int ToID = ListToLayers[HitResult];

                    int AddConnectionResult = MyGenTrainer.MyState.BaseGenNetwork.HCAddConnection(FromID, ToID);
                    if (AddConnectionResult == -1)
                        MyAppendColoredText(InitNetConsole, "Cannot connect to In layer or from Out layer.", Color.Yellow);
                    else if (AddConnectionResult == -2)
                        MyAppendColoredText(InitNetConsole, "Cannot connect a layer with itself.", Color.Yellow);
                    else if (AddConnectionResult == -3)// connection exists... remove it
                    {
                        MyGenTrainer.MyState.BaseGenNetwork.HCRemoveConnection(FromID, ToID);
                        MyAppendColoredText(InitNetConsole, "Connection (" + FromID + ", " + ToID + ") removed.", Color.White);
                    }
                    else if (AddConnectionResult == -4)
                    {
                        MyGenTrainer.MyState.BaseGenNetwork.HCRemoveConnection(FromID, ToID);
                        MyAppendColoredText(InitNetConsole, "Cannot add this connection because it generates a cycle.", Color.Yellow);
                    }
                    else //(AddConnectionResult == 0)
                        MyAppendColoredText(InitNetConsole, "Connection (" + FromID + ", " + ToID + ") added.", Color.White);
                }
                DrawGraphPlot(false);
                PopulateLayerData();
            }
        }
        private void Model_MouseMove(object sender, OxyMouseEventArgs e)
        {
            if (MouseLeftClicked)
            {
                if (LayerInListSelected == -1) return;
                DataPoint MyDataPoint = GetDataPointFromMouse(e);
                LayersDrawPositions[ListToLayers[LayerInListSelected]] = new float[] { (float)MyDataPoint.X, (float)MyDataPoint.Y };
                DrawGraphPlot(false);
            }
            else if (MouseRightClicked)
            {
                DataPoint MyDataPoint = GetDataPointFromMouse(e);
                DrawingConectionCoords = new float[] { LayersDrawPositions[ListToLayers[LayerInListSelected]][0], LayersDrawPositions[ListToLayers[LayerInListSelected]][1], (float)MyDataPoint.X, (float)MyDataPoint.Y };
                DrawGraphPlot(false);
            }
        }
        private void Model_MouseDown(object sender, OxyMouseDownEventArgs e)
        {
            if (e.ChangedButton == OxyMouseButton.Left)
            {
                MouseLeftClicked = true;
                DeselectVisuallyAllLayers();
                if (e.HitTestResult != null)
                {
                    LayerInListSelected = (int)e.HitTestResult.Index;
                    ScatterSeries MyScatterSeries = (ScatterSeries)NetGraphPlot.Model.Series[0];
                    MyScatterSeries.Points[ListToLayers[LayerInListSelected]].Value = 0.8;
                    ButtonInitNetRemoveLayer.Enabled = true;
                    ButtonInitNetRemoveLayer.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    LayerInListSelected = -1;
                    ButtonInitNetRemoveLayer.Enabled = false;
                    ButtonInitNetRemoveLayer.BackColor = Color.FromArgb(224, 224, 224);
                }
                PopulateLayerData();
                NetGraphPlot.Model.InvalidatePlot(false);
            }
            else if (e.ChangedButton == OxyMouseButton.Right)
            {
                if (e.HitTestResult != null)
                {
                    MouseRightClicked = true;
                    LayerInListSelected = (int)e.HitTestResult.Index;
                    ScatterSeries MyScatterSeries = (ScatterSeries)NetGraphPlot.Model.Series[0];
                    MyScatterSeries.Points[ListToLayers[LayerInListSelected]].Value = 0.8;
                    ButtonInitNetRemoveLayer.Enabled = true;
                    ButtonInitNetRemoveLayer.BackColor = Color.FromArgb(255, 192, 192);

                    DataPoint MyDataPoint = GetDataPointFromMouse(e);
                    DrawingConectionCoords = new float[] { LayersDrawPositions[ListToLayers[LayerInListSelected]][0], LayersDrawPositions[ListToLayers[LayerInListSelected]][1], (float)MyDataPoint.X, (float)MyDataPoint.Y };
                }
                else
                {
                    LayerInListSelected = -1;
                    ButtonInitNetRemoveLayer.Enabled = false;
                    ButtonInitNetRemoveLayer.BackColor = Color.FromArgb(224, 224, 224);
                }
                PopulateLayerData();
                NetGraphPlot.Model.InvalidatePlot(false);
            }
        }

        private void BaseNetworkToForm()
        {
            if (MyGenTrainer.MyState.BaseGenNetwork == null)
                MyGenTrainer.MyState.BaseGenNetwork = new GenNetwork(1, 1, 1, new bool[] { false, false }, new int[] { 0, 0 });

            LayersDrawPositions = new Dictionary<int, float[]>();
            ListToLayers = MyGenTrainer.MyState.BaseGenNetwork.GetAllLayersIDs();

            for (int Cnt = 0; Cnt < ListToLayers.Count; Cnt++)
                LayersDrawPositions.Add(ListToLayers[Cnt], new float[] { 0, 0 });

            TextBoxNetInput.Text = MyGenTrainer.MyState.BaseGenNetwork.GetNetInDimention().ToString();
            TextBoxNetOutput.Text = MyGenTrainer.MyState.BaseGenNetwork.GetNetOutDimention().ToString();
            bool[] FixedBools = MyGenTrainer.MyState.BaseGenNetwork.GetFixedActivationsBools();
            int[] FixedTypes = MyGenTrainer.MyState.BaseGenNetwork.GetFixedActivationsTypes();
            CheckBoxFixOutAct.Checked = FixedBools[1];
            DropDownFixOutAct.Enabled = FixedBools[1];
            DropDownFixOutAct.SelectedIndex = FixedTypes[1];
            CheckBoxFixHidAct.Checked = FixedBools[0];
            DropDownFixHidAct.Enabled = FixedBools[0];
            DropDownFixHidAct.SelectedIndex = FixedTypes[0];
            PopulateLayerData();
            DrawGraphPlot(true, true);
        }
        private void PopulateLayerData()
        {
            if (LayerInListSelected == -1)
            {
                LabelInitNetID.Text = "-";
                LabelInitNetInOut.Text = "";
                TextBoxInitNetNeuronNumber.Text = "0";
                TextBoxInitNetNeuronNumber.Enabled = false;
                LabelInitNetNumberLayers.Text = "-";
                LabelInitNetActivationFixed.Text = "-";
                LabelInitNetActivationFixed.ForeColor = Color.Green;
                LabelInitNetActivation.Text = "-";
                TextBoxInitNetInLayers.Text = "";
                TextBoxInitNetOutLayers.Text = "";
                LabelInitNetWeightsMatrix.Text = "-";
            }
            else
            {
                LabelInitNetID.Text = ListToLayers[LayerInListSelected].ToString();
                if (MyGenTrainer.MyState.BaseGenNetwork.GetIsInLayer(ListToLayers[LayerInListSelected]))
                    LabelInitNetInOut.Text = "(In Layer)";
                else if (MyGenTrainer.MyState.BaseGenNetwork.GetIsOutLayer(ListToLayers[LayerInListSelected]))
                    LabelInitNetInOut.Text = "(Out Layer)";
                else LabelInitNetInOut.Text = "(Hidden Layer)";
                TextBoxInitNetNeuronNumber.Text = MyGenTrainer.MyState.BaseGenNetwork.GetLayerNeuronsCount(ListToLayers[LayerInListSelected]).ToString();
                TextBoxInitNetNeuronNumber.Enabled = true;
                LabelInitNetNumberLayers.Text = MyGenTrainer.MyState.BaseGenNetwork.GetLayersCount().ToString();

                if ((MyGenTrainer.MyState.BaseGenNetwork.GetIsOutLayer(ListToLayers[LayerInListSelected]) && CheckBoxFixOutAct.Checked) ||
                    (!MyGenTrainer.MyState.BaseGenNetwork.GetIsOutLayer(ListToLayers[LayerInListSelected]) && CheckBoxFixHidAct.Checked))
                {
                    LabelInitNetActivationFixed.ForeColor = Color.Red;
                    LabelInitNetActivationFixed.Text = "Fixed";
                }
                else
                {
                    LabelInitNetActivationFixed.ForeColor = Color.Green;
                    LabelInitNetActivationFixed.Text = "Free";
                }

                if (MyGenTrainer.MyState.BaseGenNetwork.GetIsOutLayer(ListToLayers[LayerInListSelected]))
                    LabelInitNetActivation.Text = DropDownFixOutAct.Items[DropDownFixOutAct.SelectedIndex].ToString();
                else
                    LabelInitNetActivation.Text = DropDownFixHidAct.Items[DropDownFixHidAct.SelectedIndex].ToString(); ;

                List<int> InLayers = MyGenTrainer.MyState.BaseGenNetwork.GetLayerInLayers(ListToLayers[LayerInListSelected]);
                TextBoxInitNetInLayers.Text = "";
                int InLayerDimention = 0;
                for (int Cnt = 0; Cnt < InLayers.Count; Cnt++)
                {
                    TextBoxInitNetInLayers.AppendText(InLayers[Cnt].ToString());
                    TextBoxInitNetInLayers.AppendText(Environment.NewLine);
                    InLayerDimention += MyGenTrainer.MyState.BaseGenNetwork.GetLayerNeuronsCount(InLayers[Cnt]);
                }
                if (MyGenTrainer.MyState.BaseGenNetwork.GetIsInLayer(ListToLayers[LayerInListSelected])) InLayerDimention = MyGenTrainer.MyState.BaseGenNetwork.GetNetInDimention();

                List<int> OutLayers = MyGenTrainer.MyState.BaseGenNetwork.GetLayerOutLayers(ListToLayers[LayerInListSelected]);
                TextBoxInitNetOutLayers.Text = "";
                for (int Cnt = 0; Cnt < OutLayers.Count; Cnt++)
                {
                    TextBoxInitNetOutLayers.AppendText(OutLayers[Cnt].ToString());
                    TextBoxInitNetOutLayers.AppendText(Environment.NewLine);
                }

                LabelInitNetWeightsMatrix.Text = InLayerDimention + "X" + MyGenTrainer.MyState.BaseGenNetwork.GetLayerNeuronsCount(ListToLayers[LayerInListSelected]);
            }
        }
        private void DrawGraphPlot(bool ResetPositions, bool Deselect = false)
        {
            ScatterSeries MyScatterSeries = new ScatterSeries { MarkerStrokeThickness = 5, ColorAxisKey = "MyScatterColors" };

            int AllLayersCnt = LayersDrawPositions.Count;
            if (ResetPositions)
            {
                List<int> CurrEvalOrder = MyGenTrainer.MyState.BaseGenNetwork.GetEvalOrder();
                float XStep = 1 / (float)(AllLayersCnt - 1);
                int Cnt = 0;
                foreach (KeyValuePair<int, float[]> CurrPos in LayersDrawPositions)
                {
                    if (MyGenTrainer.MyState.BaseGenNetwork.GetIsInLayer(CurrPos.Key))
                        CurrPos.Value[0] = 0;
                    else if (MyGenTrainer.MyState.BaseGenNetwork.GetIsOutLayer(CurrPos.Key))
                        CurrPos.Value[0] = 1;
                    else
                        CurrPos.Value[0] = (Cnt - 1) * XStep;
                    if (MyGenTrainer.MyState.BaseGenNetwork.GetIsInLayer(CurrPos.Key) || MyGenTrainer.MyState.BaseGenNetwork.GetIsOutLayer(CurrPos.Key))
                        CurrPos.Value[1] = 0.5f;
                    else
                        CurrPos.Value[1] = (float)GlobalRandom.NextDouble();
                    Cnt++;
                }
            }

            NetGraphPlot.Model.Annotations.Clear();
            // Draw Layers
            for (int Cnt = 0; Cnt < AllLayersCnt; Cnt++)
            {
                if (LayerInListSelected == Cnt)
                    MyScatterSeries.Points.Add(new ScatterPoint(LayersDrawPositions[ListToLayers[Cnt]][0], LayersDrawPositions[ListToLayers[Cnt]][1], 20, 0.8));
                else
                    MyScatterSeries.Points.Add(new ScatterPoint(LayersDrawPositions[ListToLayers[Cnt]][0], LayersDrawPositions[ListToLayers[Cnt]][1], 20, 0.7));
                OxyColor TextColor = OxyColor.Parse("#000000");
                NetGraphPlot.Model.Annotations.Add(new TextAnnotation() { Text = "ID:" + ListToLayers[Cnt], TextPosition = new DataPoint(LayersDrawPositions[ListToLayers[Cnt]][0], LayersDrawPositions[ListToLayers[Cnt]][1] + 0.01), StrokeThickness = 0, TextColor = TextColor });
                NetGraphPlot.Model.Annotations.Add(new TextAnnotation() { Text = "N.:" + MyGenTrainer.MyState.BaseGenNetwork.GetLayerNeuronsCount(ListToLayers[Cnt]).ToString(), TextPosition = new DataPoint(LayersDrawPositions[ListToLayers[Cnt]][0], LayersDrawPositions[ListToLayers[Cnt]][1] - 0.05), StrokeThickness = 0, TextColor = TextColor });
            }
            //Center gray box
            for (int Cnt = 0; Cnt < AllLayersCnt; Cnt++)
                MyScatterSeries.Points.Add(new ScatterPoint(LayersDrawPositions[ListToLayers[Cnt]][0], LayersDrawPositions[ListToLayers[Cnt]][1], 5, 0));

            // Draw arrows

            for (int Cnt = 0; Cnt < AllLayersCnt; Cnt++)
            {
                double StartScatterPointX = LayersDrawPositions[ListToLayers[Cnt]][0];
                double StartScatterPointY = LayersDrawPositions[ListToLayers[Cnt]][1];
                List<int> OutList = MyGenTrainer.MyState.BaseGenNetwork.GetLayerOutLayers(ListToLayers[Cnt]);
                for (int CntOut = 0; CntOut < OutList.Count; CntOut++)
                {
                    double EndScatterPointX = LayersDrawPositions[OutList[CntOut]][0];
                    double EndScatterPointY = LayersDrawPositions[OutList[CntOut]][1];
                    OxyColor CurrColor = OxyColor.Parse("#000000");
                    NetGraphPlot.Model.Annotations.Add(new ArrowAnnotation() { StartPoint = new DataPoint(StartScatterPointX, StartScatterPointY), EndPoint = new DataPoint(EndScatterPointX, EndScatterPointY), Color = CurrColor });
                }
            }
            // Draw creating connection
            if (MouseRightClicked)
            {
                double StartScatterPointX = DrawingConectionCoords[0];
                double StartScatterPointY = DrawingConectionCoords[1];
                double EndScatterPointX = DrawingConectionCoords[2];
                double EndScatterPointY = DrawingConectionCoords[3];
                OxyColor CurrColor = OxyColor.Parse("#000000");
                NetGraphPlot.Model.Annotations.Add(new ArrowAnnotation() { StartPoint = new DataPoint(StartScatterPointX, StartScatterPointY), EndPoint = new DataPoint(EndScatterPointX, EndScatterPointY), Color = CurrColor });
            }
            NetGraphPlot.Model.Series.Clear();
            NetGraphPlot.Model.Series.Add(MyScatterSeries);
            NetGraphPlot.Model.InvalidatePlot(true);
            if (Deselect) DeselectVisuallyAllLayers();
        }
        private void DeselectVisuallyAllLayers()
        {
            if (NetGraphPlot.Model.Series.Count == 0) return;
            ScatterSeries MyScatterSeries = (ScatterSeries)NetGraphPlot.Model.Series[0];
            for (int Cnt = 0; Cnt < LayersDrawPositions.Count; Cnt++)
                MyScatterSeries.Points[Cnt].Value = 0.7;
            LayerInListSelected = -1;
            ButtonInitNetRemoveLayer.Enabled = false;
            ButtonInitNetRemoveLayer.BackColor = Color.FromArgb(224, 224, 224);
        }
        private DataPoint GetDataPointFromMouse(OxyMouseEventArgs e)
        {
            PlotModel MyPlot = NetGraphPlot.Model as PlotModel;
            ElementCollection<Axis> AxisList = MyPlot.Axes;
            Axis X_Axis = AxisList[0];
            Axis Y_Axis = AxisList[0];
            foreach (Axis CurrAxis in AxisList)
            {
                if (CurrAxis.Position == AxisPosition.Bottom)
                    X_Axis = CurrAxis;
                else if (CurrAxis.Position == AxisPosition.Left)
                    Y_Axis = CurrAxis;
            }

            return Axis.InverseTransform(e.Position, X_Axis, Y_Axis);
        }
        private int MyHitTest(OxyMouseEventArgs e, DataPoint NewPoint)
        {
            HitTestArguments Args = new HitTestArguments(e.Position, 20);
            int CountItems = 0;
            IEnumerable<HitTestResult> HitResults = NetGraphPlot.Model.HitTest(Args);
            foreach (HitTestResult Result in HitResults)
            {
                if (Result.Item != null)
                    CountItems++;
            }
            if (CountItems == 1)
                foreach (HitTestResult Result in HitResults)
                {
                    if (Result.Item != null)
                        return (int)Result.Index;
                }
            return -1;
        }
    }
}
