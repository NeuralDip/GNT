using System;
using System.Windows.Forms;
using GeneticNetworkTrainer;

namespace GeneticNetworkTrainerForm
{
    partial class MainForm
    {
        private void PopulateInTextBoxes()
        {
            LayoutIn.Controls.Clear();
            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.BaseGenNetwork.GetNetInDimention(); Cnt++)
            {
                LayoutIn.Controls.Add(new Label() { Text = "IN_" + Cnt + ":", Margin = new Padding(0, 5, 0, 0), Size = new System.Drawing.Size(40, 20) }, 0, Cnt);
                LayoutIn.Controls.Add(new TextBox() { Text = "0.0", Size = new System.Drawing.Size(50, 20) }, 1, Cnt);
                LayoutIn.Controls[2 * Cnt + 1].TextChanged += new EventHandler(TextBox_TextChangedEvent);
            }
        }
        private void PopulateOutLabels()
        {
            LayoutOut.Controls.Clear();
            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.BaseGenNetwork.GetNetOutDimention(); Cnt++)
            {
                LayoutOut.Controls.Add(new Label() { Text = "Out_" + Cnt + ":" }, 0, Cnt);
                LayoutOut.Controls.Add(new Label() { Text = "-" }, 1, Cnt);
                LayoutOut.Controls.Add(new Label() { Text = "-" }, 2, Cnt);
            }
        }
        private void TextBox_TextChangedEvent(object sender, EventArgs e)
        {
            TextBox CurrControl = (TextBox)sender;
            float NewValue;
            if (!float.TryParse(CurrControl.Text, out NewValue))
            {
                AppendFeedback(string.Format("Invalid input. Expected a float. "), 1);
                CurrControl.Text = "0.00";
            }
            else
            {// input changed expected Values are unknown, so clean them
                for (int Cnt = 0; Cnt < MyGenTrainer.MyState.BaseGenNetwork.GetNetOutDimention(); Cnt++)
                    LayoutOut.Controls[3 * Cnt + 1].Text = "-";
            }
        }
        private void ButtonEvaluate_Click(object sender, EventArgs e)
        {
            if (ListViewNets.CheckedIndices.Count > 0)
            {
                GenNetwork NetworkSelected = MyGenTrainer.SettledNetsStructure[ListViewStructIslandsSelection][ListViewStructuresSelection][ListViewInternalIslandsSelection][ListViewNets.CheckedIndices[0]].CloneMe(false, false, false, null);
                float[] Input = new float[MyGenTrainer.MyState.BaseGenNetwork.GetNetInDimention()];

                for (int Cnt = 0; Cnt < MyGenTrainer.MyState.BaseGenNetwork.GetNetInDimention(); Cnt++) Input[Cnt] = float.Parse(LayoutIn.Controls[2 * Cnt + 1].Text);
                NetworkSelected.EvaluateNet(Input);
                float[] Output = NetworkSelected.GetNetOutput();
                for (int Cnt = 0; Cnt < MyGenTrainer.MyState.BaseGenNetwork.GetNetOutDimention(); Cnt++) LayoutOut.Controls[3 * Cnt + 2].Text = Output[Cnt].ToString("E3");
                AppendFeedback(string.Format("Net Evaluated Succesfully."), 0);
            }
            else AppendFeedback(string.Format("Please select a Net from the 'Internal Inspector' tab."), 1);
        }
        private void SliderHandData_Scroll(object sender, EventArgs e)
        {
            LabelHandData.Text = SliderHandData.Value.ToString();
            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.BaseGenNetwork.GetNetInDimention(); Cnt++)
                LayoutIn.Controls[2 * Cnt + 1].Text = MyGenTrainer.MyState.InData[SliderHandData.Value][Cnt].ToString();
            for (int Cnt = 0; Cnt < MyGenTrainer.MyState.BaseGenNetwork.GetNetOutDimention(); Cnt++)
            {
                LayoutOut.Controls[3 * Cnt + 1].Text = MyGenTrainer.MyState.LabelData[SliderHandData.Value][Cnt].ToString();
                LayoutOut.Controls[3 * Cnt + 2].Text = "-";
            }
        }
        private void ResetHandTester()
        {
            PopulateOutLabels();
            PopulateInTextBoxes();
            SliderHandData.Maximum = MyGenTrainer.MyState.InData == null ? 0 : MyGenTrainer.MyState.InData.Count - 1;
            SliderHandData.Value = 0;
            if (MyGenTrainer.MyState.InData != null)
                SliderHandData_Scroll(null, null);

        }
    }
}