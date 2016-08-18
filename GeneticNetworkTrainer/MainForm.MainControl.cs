using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;


namespace GeneticNetworkTrainer
{
    partial class MainForm
    {
        private bool MouseDownStarted1 = false;
        Stopwatch MyStopwatch1 = new Stopwatch();
        private bool MouseDownStarted2 = false;
        Stopwatch MyStopwatch2 = new Stopwatch();

        private enum StartSaveState
        {
            NotReady,
            Ready,
            Running,
            Waiting
        }
        private StartSaveState StartStopState = StartSaveState.NotReady;
        private StartSaveState SaveStateState = StartSaveState.NotReady;

        private void ReadFromFileToData(string[] AllLines, int InputDimention, int OutDimention)
        {
            MyGenTrainer.MyState.InData = new List<float[]>();
            MyGenTrainer.MyState.LabelData = new List<float[]>();

            for (int CntLines = 0; CntLines < AllLines.Length; CntLines++)
            {
                MyGenTrainer.MyState.InData.Add(new float[InputDimention]);
                MyGenTrainer.MyState.LabelData.Add(new float[OutDimention]);

                float[] CurrData = Array.ConvertAll(AllLines[CntLines].Split(','), Single.Parse);
                Buffer.BlockCopy(CurrData, 0, MyGenTrainer.MyState.InData[CntLines], 0, sizeof(float) * InputDimention);
                Buffer.BlockCopy(CurrData, sizeof(float) * (CurrData.Length - InputDimention), MyGenTrainer.MyState.LabelData[CntLines], 0, sizeof(float) * OutDimention);
            }
        }
        private void ButtonSelectData_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                //try
                //{
                    string[] AllLines = File.ReadAllLines(OpenFileDialog.FileName);
                    int InputDimention = AllLines[0].Split(',').Length - 1;

                    ReadFromFileToData(AllLines, InputDimention, 1);

                    AppendFeedback(" Data Succesfully Loaded. ", 0);

                    SliderDataToUse.Maximum = AllLines.Length;
                    SliderDataToUse.Value = AllLines.Length;
                    LabelDataToUse.Text = AllLines.Length.ToString();
                    LabelTotalData.Text = AllLines.Length.ToString();

                    TextBoxNetInput.Text = InputDimention.ToString();
                    TextBoxNetOutput.Text = "1";
                    MyGenTrainer.MyState.NetInputs = InputDimention;
                    MyGenTrainer.MyState.NetOutputs = 1;

                    ButtonSelectData.Text = OpenFileDialog.FileName;
                    MyGenTrainer.MyState.DataLoadedCorrectly = true;
                    MyGenTrainer.MyState.DataFileName = OpenFileDialog.FileName;

                    SliderDataToUse.Enabled = true;
                    TextBoxNetInput.Enabled = true;
                    TextBoxNetOutput.Enabled = true;

                    StartStopState = StartSaveState.Ready;
                    SaveStateState = StartSaveState.Ready;
                    ButtonStartStop.BackColor = Color.LawnGreen;
                    ButtonSaveState.BackColor = Color.LawnGreen;

                    OpenFileDialog.InitialDirectory = OpenFileDialog.FileName.Substring(0, OpenFileDialog.FileName.LastIndexOf("\\"));
                    MyGenTrainer.MyState.DataToUse = MyGenTrainer.MyState.InData.Count;
                    MyGenTrainer.ResetStructures(true);

                    PopulateInTextBoxes();
                    PopulateOutLabels();
                //}
                //catch (Exception Ex)
                //{
                //    AppendFeedback(" Did not Load Data. Message: " + Ex.Message + "\n Trace: " + GetTrace(Ex), 2);
                //}
            }
        }
        private void SliderDataToUse_Scroll(object sender, EventArgs e)
        {
            LabelDataToUse.Text = SliderDataToUse.Value.ToString();
            MyGenTrainer.MyState.DataToUse = SliderDataToUse.Value;
        }
        private void CheckBoxHalfForTesting_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.HalfDataForTesting = CheckBoxHalfForTesting.Checked;
            CheckBoxStopCondTesting.Enabled = CheckBoxHalfForTesting.Checked;
            TextBoxStopCondTesting.Enabled = CheckBoxStopCondTesting.Checked && CheckBoxHalfForTesting.Checked;
            RadioInternalTestScore.Enabled = CheckBoxHalfForTesting.Checked;
            RadioStructIslandTestScore.Enabled = CheckBoxHalfForTesting.Checked;
            RadioStructTestScore.Enabled = CheckBoxHalfForTesting.Checked;
            if (!CheckBoxHalfForTesting.Checked)
            {
                RadioInternalTestScore.Checked = false;
                RadioInternalScore.Checked = true;
                RadioStructIslandTestScore.Checked = false;
                RadioStructIslandScore.Checked = true;
                RadioStructTestScore.Checked = false;
                RadioStructScore.Checked = true;
            }
        }
        private void TextBoxNetInput_TextChanged(object sender, EventArgs e)
        {
            if (!MyGenTrainer.MyState.DataLoadedCorrectly) return;
            int NewIn;
            if (OpenFileDialog.FileName == "") return; //data not loaded by hand
            string[] AllLines = File.ReadAllLines(OpenFileDialog.FileName);
            int AllIOs = AllLines[0].Split(',').Length;
            if (!int.TryParse(TextBoxNetInput.Text, out NewIn) || NewIn < 1 || (NewIn > AllIOs - 1))
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer < {1}", 0, AllIOs), 1);
                TextBoxNetInput.Text = MyGenTrainer.MyState.NetInputs.ToString();
            }
            else
            {
                PopulateOutLabels();
                int OutDimention = AllIOs - NewIn;

                try
                {
                    ReadFromFileToData(AllLines, NewIn, OutDimention);
                    TextBoxNetInput.Text = NewIn.ToString();
                    TextBoxNetOutput.Text = OutDimention.ToString();
                    MyGenTrainer.MyState.NetInputs = NewIn;
                    MyGenTrainer.MyState.NetOutputs = OutDimention;
                }
                catch (Exception Ex)
                {
                    TextBoxNetInput.Text = MyGenTrainer.MyState.NetInputs.ToString();
                    AppendFeedback(" Did not Split Data. Message: " + Ex.Message + "\n Trace: " + GetTrace(Ex), 3);
                }
            }
        }
        private void TextBoxNetOutput_TextChanged(object sender, EventArgs e)
        {
            if (!MyGenTrainer.MyState.DataLoadedCorrectly) return;
            int NewOut;
            if (OpenFileDialog.FileName == "") return; //data not loaded by hand
            string[] AllLines = File.ReadAllLines(OpenFileDialog.FileName);
            int AllIOs = AllLines[0].Split(',').Length;
            if (!int.TryParse(TextBoxNetOutput.Text, out NewOut) || NewOut < 1 || (NewOut > AllIOs - 1))
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer < {1}", 0, AllIOs), 1);
                TextBoxNetOutput.Text = MyGenTrainer.MyState.NetOutputs.ToString();
            }
            else
            {
                PopulateInTextBoxes();
                int InDimention = AllIOs - NewOut;

                try
                {
                    ReadFromFileToData(AllLines, InDimention, NewOut);
                    TextBoxNetInput.Text = InDimention.ToString();
                    TextBoxNetOutput.Text = NewOut.ToString();
                    MyGenTrainer.MyState.NetInputs = InDimention;
                    MyGenTrainer.MyState.NetOutputs = NewOut;

                    if (NewOut == 3)
                        RadioRules1X2.Enabled = true;
                    else
                    {
                        RadioRules1X2.Enabled = false;
                        RadioRulesOutError.Checked = true;
                        MyGenTrainer.MyState.ScoreRule = GenTrainer.ScoreRules.RuleOutError;
                    }
                }
                catch (Exception Ex)
                {
                    TextBoxNetOutput.Text = MyGenTrainer.MyState.NetOutputs.ToString();
                    AppendFeedback(" Did not Split Data. Message: " + Ex.Message + "\n Trace: " + GetTrace(Ex), 3);
                }
            }
        }
        private void TextBoxInternalPopulation_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxInternalPopulation.Text, out NewValue) || NewValue < 2 * MyGenTrainer.MyState.InitialNumberInternalIslands)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0}(2*NumIslands-1)  < Integer. ", 2 * MyGenTrainer.MyState.InitialNumberInternalIslands - 1), 1);
                TextBoxInternalPopulation.Text = MyGenTrainer.MyState.TotalInternalPopulation.ToString();
            }
            else//Number has to be divisible by the number of islands
            {
                NewValue = (NewValue - (NewValue % MyGenTrainer.MyState.InitialNumberInternalIslands));
                MyGenTrainer.MyState.TotalInternalPopulation = NewValue;
                MyGenTrainer.MyState.InternalPopulationPerIsland = NewValue / MyGenTrainer.MyState.InitialNumberInternalIslands;
                MyGenTrainer.ResetStructures(false);
            }
        }
        private void TextBoxInternalGenerations_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxInternalGenerations.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxInternalGenerations.Text = MyGenTrainer.MyState.InternalGenerations.ToString();
            }
            else MyGenTrainer.MyState.InternalGenerations = NewValue;
        }
        private void TextBoxStructurePopulation_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxStructurePopulation.Text, out NewValue) || NewValue < 2 * MyGenTrainer.MyState.CurrNumberStructureIslands)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0}(2*NumIslands-1) < Integer. ", 2 * MyGenTrainer.MyState.CurrNumberStructureIslands - 1), 1);
                TextBoxStructurePopulation.Text = MyGenTrainer.MyState.TotalStructurePopulation.ToString();
            }
            else//Number has to be divisible by the number of islands
            {
                NewValue = (NewValue - (NewValue % MyGenTrainer.MyState.CurrNumberStructureIslands));
                MyGenTrainer.MyState.TotalStructurePopulation = NewValue;
                MyGenTrainer.MyState.StructurePopulationPerIsland = NewValue / MyGenTrainer.MyState.CurrNumberStructureIslands;
                MyGenTrainer.ResetStructures(false);
            }
        }
        private void TextBoxStructureGenerations_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxStructureGenerations.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxStructureGenerations.Text = MyGenTrainer.MyState.StructureGenerations.ToString();
            }
            else MyGenTrainer.MyState.StructureGenerations = NewValue;
        }
        private void CheckBoxStopCondTesting_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxStopCondTesting.Enabled = CheckBoxStopCondTesting.Checked;
            MyGenTrainer.MyState.StopOnTesting = CheckBoxStopCondTesting.Checked;
        }
        private void TextBoxStopCondTesting_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxStopCondTesting.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxStopCondTesting.Text = MyGenTrainer.MyState.TestingToStopOn.ToString();
            }
            else MyGenTrainer.MyState.TestingToStopOn = NewValue;
        }
        private void CheckBoxStopCondScore_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxStopCondScore.Enabled = CheckBoxStopCondScore.Checked;
            MyGenTrainer.MyState.StopOnScore = CheckBoxStopCondScore.Checked;
        }
        private void TextBoxStopCondScore_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxStopCondScore.Text, out NewValue))
            {
                AppendFeedback("Invalid input. Expected Float. ", 1);
                TextBoxStopCondScore.Text = MyGenTrainer.MyState.ScoreToStopOn.ToString();
            }
            else MyGenTrainer.MyState.ScoreToStopOn = NewValue;
        }
        private void CheckBoxStopCondTime_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxStopCondTime.Enabled = CheckBoxStopCondTime.Checked;
            MyGenTrainer.MyState.StopOnTime = CheckBoxStopCondTime.Checked;
        }
        private void TextBoxStopCondTime_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxStopCondTime.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxStopCondTime.Text = MyGenTrainer.MyState.TimeToStopOn.TotalMinutes.ToString();
            }
            else MyGenTrainer.MyState.TimeToStopOn = new TimeSpan(0, NewValue, 0);
        }
        private void CheckBoxActivateThreading_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.ThreadingActivated = CheckBoxActivateThreading.Checked;

            TextBoxThreadsInParallel.Enabled = CheckBoxActivateThreading.Checked && !MyGenTrainer.MyState.DynamicSearchMaxThreads;
            CheckBoxParallelThreadsDS.Enabled = CheckBoxActivateThreading.Checked;
        }
        private void TextBoxThreadsInParallel_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxThreadsInParallel.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxThreadsInParallel.Text = MyGenTrainer.MyState.MaxThreadsinParallel.ToString();
            }
            else MyGenTrainer.MyState.MaxThreadsinParallel = NewValue;
        }
        private void CheckBoxParallelThreadsDS_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.DynamicSearchMaxThreads = CheckBoxParallelThreadsDS.Checked;
            TextBoxThreadsInParallel.Enabled = CheckBoxActivateThreading.Checked && !MyGenTrainer.MyState.DynamicSearchMaxThreads;
            LabelDynA.Enabled = CheckBoxActivateThreading.Checked && MyGenTrainer.MyState.DynamicSearchMaxThreads;
            LabelDynB.Enabled = CheckBoxActivateThreading.Checked && MyGenTrainer.MyState.DynamicSearchMaxThreads;
            LabelDynC.Enabled = CheckBoxActivateThreading.Checked && MyGenTrainer.MyState.DynamicSearchMaxThreads;
            if (!LabelDynB.Enabled) LabelDynB.Text = "-";
        }

        private void ButtonStartStop_Click(object sender, EventArgs e)
        {
            switch (StartStopState)
            {
                case StartSaveState.NotReady:
                    AppendFeedback("Cannot Start training. No Data Yet Loaded.", 1);
                    break;
                case StartSaveState.Ready:
                    StartStopState = StartSaveState.Running;
                    if (MyGenTrainer.MyState.ThreadingActivated) ThreadPool.QueueUserWorkItem(new WaitCallback(MyGenTrainer.LaunchNextStructGeneration));
                    else ThreadPool.QueueUserWorkItem(new WaitCallback(MyGenTrainer.TrainNetNotThreaded));
                    EnableRunningControls(false);
                    FixStartButtStyle();
                    FixLoadButtStyle();
                    FixSaveButtStyle();
                    FixResetButtStyle();
                    break;
                case StartSaveState.Running:
                    StartStopState = StartSaveState.Waiting;
                    MyGenTrainer.Stop(false);
                    FixStartButtStyle();
                    FixLoadButtStyle();
                    FixSaveButtStyle();
                    FixResetButtStyle();
                    break;
                case StartSaveState.Waiting:
                    StartStopState = StartSaveState.Running;
                    MyGenTrainer.Stop(true);
                    FixStartButtStyle();
                    FixLoadButtStyle();
                    FixSaveButtStyle();
                    FixResetButtStyle();
                    break;
                default: break;
            }
        }
        private void ButtonStartStop_MouseDown(object sender, EventArgs e)
        {
            if (!MouseDownStarted1 && StartStopState == StartSaveState.Waiting)
            {
                MouseDownStarted1 = true;
                MyStopwatch1.Start();
            }

        }
        private void ButtonStartStop_MouseUp(object sender, EventArgs e)
        {
            if (MouseDownStarted1)
            {
                if (MyStopwatch1.ElapsedMilliseconds > 500)
                    MyGenTrainer.ForceStop();
                MouseDownStarted1 = false;
                MyStopwatch1.Stop();
                MyStopwatch1.Reset();
            }

        }

        private void ButtonSaveState_Click(object sender, EventArgs e)
        {
            switch (SaveStateState)
            {
                case StartSaveState.Ready:
                    if (StartStopState == StartSaveState.Running || StartStopState == StartSaveState.Waiting)
                    {
                        MyGenTrainer.SaveState();
                        SaveStateState = StartSaveState.Waiting;
                        FixSaveButtStyle();
                    }
                    else MyGenTrainer.ForceSaveState();
                    FixLoadButtStyle();
                    break;
                case StartSaveState.Waiting:
                    SaveStateState = StartSaveState.Ready;
                    FixSaveButtStyle();
                    FixLoadButtStyle();
                    break;
                default: AppendFeedback("State not predicted", 2); break;
            }
        }
        private void ButtonSaveState_MouseDown(object sender, EventArgs e)
        {
            if (!MouseDownStarted2 && SaveStateState == StartSaveState.Waiting)
            {
                MouseDownStarted2 = true;
                MyStopwatch2.Start();
            }
        }
        private void ButtonSaveState_MouseUp(object sender, EventArgs e)
        {
            if (MouseDownStarted2)
            {
                if (MyStopwatch2.ElapsedMilliseconds > 500)
                {
                    SaveStateState = StartSaveState.Ready;
                    MyGenTrainer.ForceSaveState();
                    FixLoadButtStyle();
                    FixSaveButtStyle();
                    FixResetButtStyle();
                    FixStartButtStyle();
                }
                MouseDownStarted2 = false;
                MyStopwatch2.Stop();
                MyStopwatch2.Reset();
            }
        }
        private void CheckBoxIncludeNetsInSave_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.IncludeNetsInTheSave = CheckBoxIncludeNetsInSave.Checked;
        }

        private void ButtonLoadState_Click(object sender, EventArgs e)
        {
            if (MyGenTrainer.StateFileExists)
            {
                switch (StartStopState)
                {
                    case StartSaveState.Running:
                    case StartSaveState.Waiting:
                        AppendFeedback(" Stop the Training before you load the state from file.", 1);
                        break;
                    case StartSaveState.NotReady:
                    case StartSaveState.Ready:
                        MyGenTrainer.LoadState();
                        FromTrainerToForm();
                        StartStopState = StartSaveState.Ready;
                        SaveStateState = StartSaveState.Ready;
                        FixSaveButtStyle();
                        FixStartButtStyle();
                        break;
                    default: break;
                }
            }
            else AppendFeedback(" No SaveState File Found.", 1);
        }
        private void ButtonResetState_Click(object sender, EventArgs e)
        {
            if (StartStopState == StartSaveState.NotReady || StartStopState == StartSaveState.Ready)
            {
                AppendFeedback("Net Structures have been reset.", 0);
                MyGenTrainer.MyState.CurrStructureGeneration = 0;
                LabelCurrInternalGen.Text = "0";
                LabelCurrStructure.Text = "(0, 0)";
                LabelCurrStructGen.Text = "0";
                MyGenTrainer.ResetStructures(true);
            }
            else
            {
                AppendFeedback(" Stop the Training before you reset the state.", 1);
            }
        }

        private void FixLoadButtStyle()
        {
            if (MyGenTrainer.StateFileExists)
            {
                switch (StartStopState)
                {
                    case StartSaveState.NotReady:
                    case StartSaveState.Ready:
                        ButtonLoadState.BackColor = Color.LawnGreen;
                        break;
                    case StartSaveState.Running:
                    case StartSaveState.Waiting:
                        ButtonLoadState.BackColor = Color.OrangeRed;
                        break;
                    default: break;
                }
            }
            else ButtonResetState.BackColor = Color.Silver;
        }
        private void FixSaveButtStyle()
        {
            switch (SaveStateState)
            {
                case StartSaveState.Ready:
                    ButtonSaveState.BackColor = Color.LawnGreen;
                    ButtonSaveState.Text = "SAVE";
                    break;
                case StartSaveState.Waiting:
                    ButtonSaveState.BackColor = Color.Yellow;
                    ButtonSaveState.Text = "SAVING";
                    break;
                default: break;
            }
        }
        private void FixStartButtStyle()
        {
            switch (StartStopState)
            {
                case StartSaveState.NotReady:
                    ButtonStartStop.BackColor = Color.Silver;
                    ButtonStartStop.Text = "START";
                    break;
                case StartSaveState.Ready:
                    ButtonStartStop.BackColor = Color.LawnGreen;
                    ButtonStartStop.Text = "START";
                    break;
                case StartSaveState.Running:
                    ButtonStartStop.BackColor = Color.OrangeRed;
                    ButtonStartStop.Text = "STOP";
                    break;
                case StartSaveState.Waiting:
                    ButtonStartStop.BackColor = Color.Yellow;
                    ButtonStartStop.Text = "STOPPING";
                    break;
                default: break;
            }
        }
        private void FixResetButtStyle()
        {
            switch (StartStopState)
            {
                case StartSaveState.NotReady:
                case StartSaveState.Ready:
                    ButtonResetState.BackColor = Color.Yellow;
                    break;
                case StartSaveState.Running:
                case StartSaveState.Waiting:
                    ButtonResetState.BackColor = Color.OrangeRed;
                    break;
                default: break;
            }
        }
        private int GetLine(Exception E)
        {
            var st = new System.Diagnostics.StackTrace(E, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            return frame.GetFileLineNumber();
        }
        private string GetFile(Exception E)
        {
            var st = new System.Diagnostics.StackTrace(E, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            return frame.GetFileName();
        }
        private string GetTrace(Exception E)
        {
            return new System.Diagnostics.StackTrace(E, true).ToString();
        }
    }
}
