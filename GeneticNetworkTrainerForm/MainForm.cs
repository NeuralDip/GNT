using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using OxyPlot;
using OxyPlot.WindowsForms;
using System.Reflection;
using GeneticNetworkTrainer;



namespace GeneticNetworkTrainerForm
{
    public partial class MainForm : Form
    {
        //Net Structure
        public GenTrainer MyGenTrainer;

        private PlotView PlotStructureIslandsSeries;
        private PlotView PlotStructureIslandsHist;
        private PlotView PlotStructureSeries;
        private PlotView PlotInternalIslandsSeries;
        private PlotView PlotInternalIslandsHist;
        private PlotView PlotInternalSeries;
        private int HistogramsBins = 10;

        private string LogFileName;
        public MainForm()
        {
            InitializeComponent();
            MyInitialize();
            MyGenTrainer = new GenTrainer(AppendFeedback, SetControlPropertyThreadSafe, GetControlPropertyThreadSafe);

            LogFileName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Log.txt";
            OpenFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            MyGenTrainer.StateFileExists = File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\GenTrainingSave");
            MyGenTrainer.HistogramsBins = HistogramsBins;
            FromTrainerToForm();

            MyGenTrainer.CallTheForm += new GenTrainer.SomethingHappenedDelegate(GenTrainerJustCalled);

            MyGlobalTimer.Interval = (1000); 
            MyGlobalTimer.Tick += new EventHandler(UpdateGlobalStopWatch);
            MyGlobalTimer.Start();
        }

        public void AppendFeedback(string ToPrint, int Type)
        {
            if (Type == 0)//Info
                MyAppendColoredText(Console, DateTime.Now.ToString() + " INF.: " + ToPrint, Color.White);
            else if (Type == 1)//Warning
                MyAppendColoredText(Console, DateTime.Now.ToString() + " WRN.: " + ToPrint, Color.Yellow);
            else// Error
                MyAppendColoredText(Console, DateTime.Now.ToString() + " ERR.: " + ToPrint, Color.Red);
        }
        private delegate void AppendThreadSafeDelegate(RichTextBox MyBox, string MyText, Color MyColor);
        private void MyAppendColoredText(RichTextBox MyBox, string MyText, Color MyColor)
        {
            if (MyBox.InvokeRequired)
            {
                Invoke(new AppendThreadSafeDelegate(MyAppendColoredText), new object[] { MyBox, MyText, MyColor });
            }
            else
            {
                if (CheckBoxLog.Checked)
                {
                    using (StreamWriter writer = File.AppendText(LogFileName))
                    {
                        writer.Write(MyText);
                        writer.Flush();
                        writer.Close();
                    }
                }
                MyBox.SelectionStart = MyBox.TextLength;
                MyBox.SelectionLength = 0;
                MyBox.SelectionColor = MyColor;
                MyBox.AppendText(MyText + "\n");
                MyBox.SelectionColor = MyBox.ForeColor;
            }
        }
        private delegate void SetControlPropertyThreadSafeDelegate(string ControlName, string PropertyName, object PropertyValue);
        public void SetControlPropertyThreadSafe(string ControlName, string PropertyName, object PropertyValue)
        {
            Control CurrControl = this.Controls.Find(ControlName, true).FirstOrDefault();
            if (CurrControl == null)
            {
                AppendFeedback("Control name was not found in the form : '" + ControlName + "'.", 1);
                return;
            }
            if (CurrControl.InvokeRequired)
            {
                CurrControl.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { ControlName, PropertyName, PropertyValue });
            }
            else
            {
                try
                {
                    CurrControl.GetType().InvokeMember(PropertyName, BindingFlags.SetProperty, null, CurrControl, new object[] { PropertyValue });
                }
                catch (Exception Ex)
                {
                    AppendFeedback(" Error While Accessing Control. Message: " + Ex.Message + "\n Trace: " + GetTrace(Ex), 2);
                }
            }
        }
        private delegate object GetControlPropertyThreadSafeDelegate(string ControlName, string PropertyName);
        public object GetControlPropertyThreadSafe(string ControlName, string PropertyName)
        {
            Control CurrControl = this.Controls.Find(ControlName, true).FirstOrDefault();
            if (CurrControl == null)
            {
                AppendFeedback("Control name was not found in the form : '" + ControlName + "'.", 1);
                return null;
            }
            if (CurrControl.InvokeRequired)
            {
                return CurrControl.Invoke(new GetControlPropertyThreadSafeDelegate(GetControlPropertyThreadSafe), new object[] { ControlName, PropertyName });
            }
            else
            {
                try
                {
                    return CurrControl.GetType().InvokeMember(PropertyName, BindingFlags.GetProperty, null, CurrControl, null);
                    //return CurrControl.GetType().GetField(PropertyName).GetValue(null);
                }
                catch (Exception Ex)
                {
                    AppendFeedback(" Error While Accessing Control. Message: " + Ex.Message + "\n Trace: " + GetTrace(Ex), 2);
                }
            }
            return null;
        }
        private void Tab_Changed_Handler(object sender, EventArgs e)
        {
            if (MainTabControl.SelectedIndex == 0)
            {
                CheckBoxLog.Parent = MainTabControl.TabPages[0];
                Console.Parent = MainTabControl.TabPages[0];
            }
            else if (MainTabControl.SelectedIndex == 1)
            {
                CheckBoxLog.Parent = MainTabControl.TabPages[1];
                Console.Parent = MainTabControl.TabPages[1];
            }
            else if (MainTabControl.SelectedIndex == 2)
            {
                CheckBoxLog.Parent = MainTabControl.TabPages[2];
                Console.Parent = MainTabControl.TabPages[2];
            }
            else if (MainTabControl.SelectedIndex == 3)
            {
                PopulateAllListViews();
                GroupBoxGlobalStats.Parent = MainTabControl.TabPages[3];
                GroupBoxStructType.Parent = MainTabControl.TabPages[3];
                GroupBoxStructType.Location = new Point(301, 189);
                GroupBoxStructType.Size = new Size(152, 101);
            }
            else if (MainTabControl.SelectedIndex == 4)
            {
                PopulateAllListViews();
                GroupBoxGlobalStats.Parent = MainTabControl.TabPages[4];
                GroupBoxStructType.Parent = MainTabControl.TabPages[4];
                GroupBoxStructType.Location = new Point(242, 186);
                GroupBoxStructType.Size = new Size(211, 101);
            }
            else
            {
                CheckBoxLog.Parent = MainTabControl.TabPages[5];
                Console.Parent = MainTabControl.TabPages[5];
            }
        }
        private void FromTrainerToForm()
        {
            if (MyGenTrainer.MyState.DataLoadedCorrectly)
            {
                SliderDataToUse.Maximum = MyGenTrainer.MyState.InData.Count;
                SliderDataToUse.Value = MyGenTrainer.MyState.DataToUse;
                LabelDataToUse.Text = MyGenTrainer.MyState.DataToUse.ToString();
                LabelTotalData.Text = SliderDataToUse.Maximum.ToString();

                TextBoxNetInput.Text = MyGenTrainer.MyState.NetInputs.ToString();
                TextBoxNetOutput.Text = MyGenTrainer.MyState.NetOutputs.ToString();

                ButtonSelectData.Text = MyGenTrainer.MyState.DataFileName;
                TextBoxNetInput.Text = MyGenTrainer.MyState.NetInputs.ToString();
                TextBoxNetOutput.Text = MyGenTrainer.MyState.NetOutputs.ToString();

                StartStopState = StartSaveState.Ready;
                SaveStateState = StartSaveState.Ready;
                ButtonStartStop.BackColor = Color.LawnGreen;
                ButtonSaveState.BackColor = Color.LawnGreen;
            }
            else
            {
                SliderDataToUse.Maximum = 0;
                SliderDataToUse.Value = 0;
                LabelDataToUse.Text = "-";
                LabelTotalData.Text = "-";

                TextBoxNetInput.Text = "1";
                TextBoxNetOutput.Text = "1";

                ButtonSelectData.Text = "Select Data File";

                StartStopState = StartSaveState.NotReady;
                SaveStateState = StartSaveState.Ready;
                ButtonStartStop.BackColor = Color.DarkGray;
                ButtonSaveState.BackColor = Color.DarkGray;
            }
            FixStartButtStyle();
            FixLoadButtStyle();
            FixSaveButtStyle();
            FixResetButtStyle();
            SliderDataToUse.Enabled = MyGenTrainer.MyState.DataLoadedCorrectly;
            TextBoxNetInput.Enabled = MyGenTrainer.MyState.DataLoadedCorrectly && (OpenFileDialog.FileName != "");
            TextBoxNetOutput.Enabled = MyGenTrainer.MyState.DataLoadedCorrectly && (OpenFileDialog.FileName != "");

            CheckBoxHalfForTesting.Checked = MyGenTrainer.MyState.HalfDataForTesting;


            TextBoxInternalPopulation.Text = MyGenTrainer.MyState.TotalInternalPopulation.ToString();
            TextBoxInternalGenerations.Text = MyGenTrainer.MyState.InternalGenerations.ToString();

            LabelCurrInternalGen.Text = "0";
            LabelCurrStructure.Text = "(0, 0)";
            TextBoxStructurePopulation.Text = MyGenTrainer.MyState.TotalStructurePopulation.ToString();
            TextBoxStructureGenerations.Text = MyGenTrainer.MyState.StructureGenerations.ToString();
            LabelCurrStructGen.Text = MyGenTrainer.MyState.CurrStructureGeneration.ToString();

            CheckBoxStopCondTesting.Checked = MyGenTrainer.MyState.StopOnTesting;
            CheckBoxStopCondTesting.Enabled = CheckBoxHalfForTesting.Checked;
            TextBoxStopCondTesting.Text = MyGenTrainer.MyState.TestingToStopOn.ToString();
            TextBoxStopCondTesting.Enabled = CheckBoxStopCondTesting.Checked && CheckBoxHalfForTesting.Checked;
            CheckBoxStopCondScore.Checked = MyGenTrainer.MyState.StopOnScore;
            TextBoxStopCondScore.Text = MyGenTrainer.MyState.ScoreToStopOn.ToString();
            TextBoxStopCondScore.Enabled = CheckBoxStopCondScore.Checked;
            CheckBoxStopCondTime.Checked = MyGenTrainer.MyState.StopOnTime;
            TextBoxStopCondTime.Text = Math.Round(MyGenTrainer.MyState.TimeToStopOn.TotalMinutes).ToString();
            TextBoxStopCondTime.Enabled = CheckBoxStopCondTime.Checked;
            CheckBoxIncludeNetsInSave.Checked = MyGenTrainer.MyState.IncludeNetsInTheSave;

            CheckBoxActivateThreading.Checked = MyGenTrainer.MyState.ThreadingActivated;

            CheckBoxParallelThreadsDS.Enabled = CheckBoxActivateThreading.Checked;
            TextBoxThreadsInParallel.Enabled = CheckBoxActivateThreading.Checked && !MyGenTrainer.MyState.DynamicSearchMaxThreads;
            LabelDynA.Enabled = CheckBoxActivateThreading.Checked && MyGenTrainer.MyState.DynamicSearchMaxThreads;
            LabelDynB.Enabled = CheckBoxActivateThreading.Checked && MyGenTrainer.MyState.DynamicSearchMaxThreads;
            LabelDynC.Enabled = CheckBoxActivateThreading.Checked && MyGenTrainer.MyState.DynamicSearchMaxThreads;

            CheckBoxParallelThreadsDS.Checked = MyGenTrainer.MyState.DynamicSearchMaxThreads;
            TextBoxThreadsInParallel.Text = MyGenTrainer.MyState.MaxThreadsinParallel.ToString();

            //Structure Genetics
            SliderStructureCrossover.Value = (int)(MyGenTrainer.MyState.StructureCrossover * 100);
            LabelStructureCrossover.Text = MyGenTrainer.MyState.StructureCrossover.ToString();
            SliderStructureMutation.Value = (int)(MyGenTrainer.MyState.StructureMutation * 100);
            LabelStructureMutation.Text = MyGenTrainer.MyState.StructureMutation.ToString();
            SliderStructureMutationStrength.Value = (int)(MyGenTrainer.MyState.StructureMutationStrength * 100);
            LabelStructureMutationStrength.Text = MyGenTrainer.MyState.StructureMutationStrength.ToString();
            TextBoxLayerCost.Text = MyGenTrainer.MyState.LayerCost.ToString();
            TextBoxFunctionCost.Text = MyGenTrainer.MyState.FunctionCost.ToString();
            TextBoxNeuronCost.Text = MyGenTrainer.MyState.NeuronCost.ToString();
            TextBoxConnectionCost.Text = MyGenTrainer.MyState.ConnectionCost.ToString();
            CheckBoxWeightsRandom.Checked = MyGenTrainer.MyState.WeightsRandomized;
            CheckBoxBiasesRandom.Checked = MyGenTrainer.MyState.BiasesRandomized;
            SliderStructureCopy.Value = (int)(MyGenTrainer.MyState.StructureCopy * 100);
            LabelStructureCopy.Text = MyGenTrainer.MyState.StructureCopy.ToString();

            if (MyGenTrainer.MyState.CurrNumberStructureIslands == 1) SliderStructureIslands.Value = 0;
            else if (MyGenTrainer.MyState.CurrNumberStructureIslands == 2) SliderStructureIslands.Value = 1;
            else if (MyGenTrainer.MyState.CurrNumberStructureIslands == 4) SliderStructureIslands.Value = 2;
            else if (MyGenTrainer.MyState.CurrNumberStructureIslands == 8) SliderStructureIslands.Value = 3;
            else /*if (MyState.NumberStructureIslands == 16)*/ SliderStructureIslands.Value = 4;
            LabelStructureIslands.Text = MyGenTrainer.MyState.CurrNumberStructureIslands.ToString();
            CheckBoxStructureHalve.Checked = MyGenTrainer.MyState.HalveStructureIslands;
            TextBoxStructureIslandsSteps.Text = MyGenTrainer.MyState.StructureIslandsHalvingSteps.ToString();
            LabelStructureHalveIn.Text = MyGenTrainer.MyState.StructureIslandsHalveIn.ToString();

            CheckBoxScoreNeurons.Checked = MyGenTrainer.MyState.ScorePenaltyNeurons;
            TextBoxScoreNeurons.Enabled = MyGenTrainer.MyState.ScorePenaltyNeurons;
            TextBoxScoreNeurons.Text = MyGenTrainer.MyState.ScorePenaltyNumberOfNeurons.ToString();
            CheckBoxScoreLayers.Checked = MyGenTrainer.MyState.ScorePenaltyLayers;
            TextBoxScoreLayer.Enabled = MyGenTrainer.MyState.ScorePenaltyLayers;
            TextBoxScoreLayer.Text = MyGenTrainer.MyState.ScorePenaltyNumberOfLayers.ToString();
            CheckBoxScoreConnections.Checked = MyGenTrainer.MyState.ScorePenaltyConnections;
            TextBoxScoreConnections.Enabled = MyGenTrainer.MyState.ScorePenaltyConnections;
            TextBoxScoreConnections.Text = MyGenTrainer.MyState.ScorePenaltyNumberOfConnections.ToString();

            //Internal Genetics
            SliderInternalCrossover.Value = (int)(MyGenTrainer.MyState.InternalCrossover * 100);
            LabelInternalCrossover.Text = MyGenTrainer.MyState.InternalCrossover.ToString();
            SliderInternalMutation.Value = (int)(MyGenTrainer.MyState.InternalMutation * 100);
            LabelInternalMutation.Text = MyGenTrainer.MyState.InternalMutation.ToString();
            SliderInternalMutationStrength.Value = (int)(MyGenTrainer.MyState.InternalMutationStrength * 100);
            LabelInternalMutationStrength.Text = MyGenTrainer.MyState.InternalMutationStrength.ToString();
            TextBoxLimitWeights.Text = MyGenTrainer.MyState.WeightsLimit.ToString();
            TextBoxLimitBiases.Text = MyGenTrainer.MyState.BiasesLimit.ToString();
            CheckBoxMutateWeights.Checked = MyGenTrainer.MyState.MutateWeights;
            CheckBoxMutateBiases.Checked = MyGenTrainer.MyState.MutateBiases;
            SliderInternalCopy.Value = (int)(MyGenTrainer.MyState.InternalCopy * 100);
            LabelInternalCopy.Text = MyGenTrainer.MyState.InternalCopy.ToString();

            SliderAnnealing.Value = (int)(MyGenTrainer.MyState.InitialInternalAnnealing * 100);
            LabelAnnealing.Text = MyGenTrainer.MyState.InitialInternalAnnealing.ToString();
            CheckBoxAnealReduce.Checked = MyGenTrainer.MyState.ReduceAnnealing;
            TextBoxInternalAnnealStep.Text = MyGenTrainer.MyState.AnnealingReducingSteps.ToString();

            if (MyGenTrainer.MyState.InitialNumberInternalIslands == 1) SliderInternalIslands.Value = 0;
            else if (MyGenTrainer.MyState.InitialNumberInternalIslands == 2) SliderInternalIslands.Value = 1;
            else if (MyGenTrainer.MyState.InitialNumberInternalIslands == 4) SliderInternalIslands.Value = 2;
            else if (MyGenTrainer.MyState.InitialNumberInternalIslands == 8) SliderInternalIslands.Value = 3;
            else /*if (MyState.NumberInternalIslands == 16)*/ SliderInternalIslands.Value = 4;
            LabelInternalIslands.Text = MyGenTrainer.MyState.InitialNumberInternalIslands.ToString();
            CheckBoxInternalHalve.Checked = MyGenTrainer.MyState.HalveInternalIslands;
            TextBoxInternalIslandsStep.Text = MyGenTrainer.MyState.InternalIslandsHalvingSteps.ToString();

            RadioRulesOutError.Checked = MyGenTrainer.MyState.ScoreRule == GenTrainer.ScoreRules.RuleOutError;
            RadioRules1X2.Checked = MyGenTrainer.MyState.ScoreRule == GenTrainer.ScoreRules.Rule1X2;
            TextBoxRulesOutThreshold.Text = MyGenTrainer.MyState.ThresholdOfValidOut.ToString();

            // HandTester
            PopulateOutLabels();
            PopulateInTextBoxes();
            SliderHandData.Maximum = MyGenTrainer.MyState.InData == null ? 0 : MyGenTrainer.MyState.InData.Count - 1;
            SliderHandData.Value = 0;
            if (MyGenTrainer.MyState.InData != null)
                for (int Cnt = 0; Cnt < MyGenTrainer.MyState.NetInputs; Cnt++) LayoutIn.Controls[2 * Cnt + 1].Text = MyGenTrainer.MyState.InData[SliderHandData.Value][Cnt].ToString();

        }

        private delegate void GenTrainerJustCalledDelegate(GenTrainer.TrainingState WhatHappened);
        private void GenTrainerJustCalled(GenTrainer.TrainingState WhatHappened)
        {
            if (InvokeRequired) Invoke(new GenTrainerJustCalledDelegate(GenTrainerJustCalled), new object[] { WhatHappened });
            else
            {
                switch (WhatHappened)
                {
                    case GenTrainer.TrainingState.TrainingEnded:
                        AppendFeedback(" Training Ended. ", 0);
                        LabelCurrStructGen.Text = MyGenTrainer.MyState.CurrStructureGeneration.ToString();
                        LabelCurrStructure.Text = MyGenTrainer.MyState.ThreadingActivated ? "0" : "(0, 0)";
                        LabelCurrInternalGen.Text = "0";
                        EnableRunningControls(true);
                        StartStopState = StartSaveState.Ready;
                        FixLoadButtStyle();
                        FixSaveButtStyle();
                        FixResetButtStyle();
                        FixStartButtStyle();
                        MyGlobalStopWatch.Stop();
                        break;
                    case GenTrainer.TrainingState.TrainingStopped:
                        AppendFeedback(string.Format("Training stopped at Generation ({0},{1}).", MyGenTrainer.MyState.CurrStructureGeneration, 0), 0);
                        LabelCurrStructGen.Text = MyGenTrainer.MyState.CurrStructureGeneration.ToString();
                        LabelCurrStructure.Text = MyGenTrainer.MyState.ThreadingActivated ? "0" : "(0, 0)";
                        EnableRunningControls(true);
                        StartStopState = StartSaveState.Ready;
                        FixLoadButtStyle();
                        FixSaveButtStyle();
                        FixResetButtStyle();
                        FixStartButtStyle();
                        MyGlobalStopWatch.Stop();
                        break;
                    case GenTrainer.TrainingState.StructGenEnded:
                        NewInternalStatsReady = true;
                        NewStructStatsReady = true;
                        RedrawAllPlots();
                        PopulateAllListViews();
                        break;
                    default:
                        AppendFeedback("Happening not implemented... : " + WhatHappened.ToString(), 1);
                        break;
                }
            }
        }

        private void EnableRunningControls(bool Enabling)
        {
            ButtonSelectData.Enabled = Enabling;
            SliderDataToUse.Enabled = Enabling;
            CheckBoxHalfForTesting.Enabled = Enabling;
            TextBoxInternalPopulation.Enabled = Enabling;
            TextBoxStructurePopulation.Enabled = Enabling;
            CheckBoxActivateThreading.Enabled = Enabling;
            TextBoxThreadsInParallel.Enabled = Enabling;
            CheckBoxParallelThreadsDS.Enabled = Enabling;
            SliderStructureIslands.Enabled = Enabling;
            CheckBoxStructureHalve.Enabled = Enabling;
            TextBoxStructureIslandsSteps.Enabled = Enabling;
            SliderAnnealing.Enabled = Enabling;
            CheckBoxAnealReduce.Enabled = Enabling;
            TextBoxInternalAnnealStep.Enabled = Enabling;
            SliderInternalIslands.Enabled = Enabling;
            CheckBoxInternalHalve.Enabled = Enabling;
            TextBoxInternalIslandsStep.Enabled = Enabling;
        }
        private void RedrawAllPlots()
        {
            RedrawStructIslandsPlot();
            RedrawStructPlot();
            RedrawInternalIslandsPlot();
            RedrawInternalPlot();
        }
        private void PopulateAllListViews()
        {
            PopulateStructIslands();
            PopulateStructs();
            PopulateInternalIslands();
            PopulateInternalNets();
        }

        public class ListViewColumnSorter : IComparer
        {
            /// <summary>
            /// Specifies the column to be sorted
            /// </summary>
            private int ColumnToSort;
            /// <summary>
            /// Specifies the order in which to sort (i.e. 'Ascending').
            /// </summary>
            private SortOrder OrderOfSort;
            /// <summary>
            /// Case insensitive comparer object
            /// </summary>
            private CaseInsensitiveComparer ObjectCompare;

            /// <summary>
            /// Class constructor.  Initializes various elements
            /// </summary>
            public ListViewColumnSorter()
            {
                // Initialize the column to '0'
                ColumnToSort = 0;

                // Initialize the sort order to 'none'
                OrderOfSort = SortOrder.None;

                // Initialize the CaseInsensitiveComparer object
                ObjectCompare = new CaseInsensitiveComparer();
            }

            /// <summary>
            /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX, listviewY;

                // Cast the objects to be compared to ListViewItem objects
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                // Compare the two items
                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

                // Calculate correct return value based on object comparison
                if (OrderOfSort == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return normal result of compare operation
                    return compareResult;
                }
                else if (OrderOfSort == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of compare operation
                    return (-compareResult);
                }
                else
                {
                    // Return '0' to indicate they are equal
                    return 0;
                }
            }

            /// <summary>
            /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
            /// </summary>
            public int SortColumn
            {
                set
                {
                    ColumnToSort = value;
                }
                get
                {
                    return ColumnToSort;
                }
            }

            /// <summary>
            /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
            /// </summary>
            public SortOrder Order
            {
                set
                {
                    OrderOfSort = value;
                }
                get
                {
                    return OrderOfSort;
                }
            }
        }
    }
}
