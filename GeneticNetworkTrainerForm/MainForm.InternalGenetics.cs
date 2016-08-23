using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticNetworkTrainer;

namespace GeneticNetworkTrainerForm
{
    partial class MainForm
    {
        private void SliderInternalCrossover_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderInternalCrossover.Value / 100;

            LabelInternalCrossover.Text = NewValue.ToString();

            if (NewValue > MyGenTrainer.MyState.InternalCrossover)
            {
                if (MyGenTrainer.MyState.InternalCopy >= NewValue - MyGenTrainer.MyState.InternalCrossover)
                    SliderInternalCopy.Value = 100 - SliderInternalMutation.Value - SliderInternalCrossover.Value;
                else if (MyGenTrainer.MyState.InternalCopy > 0)// Get Value from Copy
                {
                    SliderInternalMutation.Value = 100 - SliderInternalCrossover.Value;
                    SliderInternalCopy.Value = 0;
                }
                else // get it from Mutation
                    SliderInternalMutation.Value = 100 - SliderInternalCrossover.Value;
            }
            else
                SliderInternalCopy.Value = 100 - SliderInternalMutation.Value - SliderInternalCrossover.Value;

            LabelInternalCopy.Text = ((float)SliderInternalCopy.Value / 100).ToString();
            MyGenTrainer.MyState.InternalCopy = (float)SliderInternalCopy.Value / 100;
            LabelInternalMutation.Text = ((float)SliderInternalMutation.Value / 100).ToString();
            MyGenTrainer.MyState.InternalMutation = (float)SliderInternalMutation.Value / 100;

            MyGenTrainer.MyState.InternalCrossover = NewValue;
        }
        private void SliderInternalMutation_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderInternalMutation.Value / 100;

            LabelInternalMutation.Text = NewValue.ToString();

            if (NewValue > MyGenTrainer.MyState.InternalMutation)
            {
                if (MyGenTrainer.MyState.InternalCopy >= NewValue - MyGenTrainer.MyState.InternalMutation)
                    SliderInternalCopy.Value = 100 - SliderInternalMutation.Value - SliderInternalCrossover.Value;
                else if (MyGenTrainer.MyState.InternalCopy > 0)// Get Value from Copy
                {
                    SliderInternalCrossover.Value = 100 - SliderInternalMutation.Value;
                    SliderInternalCopy.Value = 0;
                }
                else // get it from Mutation
                    SliderInternalCrossover.Value = 100 - SliderInternalMutation.Value;
            }
            else
                SliderInternalCopy.Value = 100 - SliderInternalMutation.Value - SliderInternalCrossover.Value;

            LabelInternalCopy.Text = ((float)SliderInternalCopy.Value / 100).ToString();
            MyGenTrainer.MyState.InternalCopy = (float)SliderInternalCopy.Value / 100;
            LabelInternalCrossover.Text = ((float)SliderInternalCrossover.Value / 100).ToString();
            MyGenTrainer.MyState.InternalCrossover = (float)SliderInternalCrossover.Value / 100;

            MyGenTrainer.MyState.InternalMutation = NewValue;
        }
        private void CheckBoxMutateWeights_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.MutateWeights = CheckBoxMutateWeights.Checked;
        }
        private void CheckBoxMutateBiases_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.MutateBiases = CheckBoxMutateBiases.Checked;
        }
        private void TextBoxLimitWeights_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxLimitWeights.Text, out NewValue) || NewValue < 0.01)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} < Float . ", 0.01f, 1), 1);
                TextBoxLimitWeights.Text = MyGenTrainer.MyState.WeightsLimit.ToString();
            }
            else MyGenTrainer.MyState.WeightsLimit = NewValue;
        }
        private void TextBoxLimitBiases_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxLimitBiases.Text, out NewValue) || NewValue < 0.01)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} < Float . ", 0.01f, 1), 1);
                TextBoxLimitBiases.Text = MyGenTrainer.MyState.BiasesLimit.ToString();
            }
            else MyGenTrainer.MyState.BiasesLimit = NewValue;
        }
        private void SliderInternalMutationStrength_Scroll(object sender, EventArgs e)
        {
            LabelInternalMutationStrength.Text = ((float)SliderInternalMutationStrength.Value / 100).ToString();
            MyGenTrainer.MyState.InternalMutationStrength = (float)SliderInternalMutationStrength.Value / 100;
        }
        private void SliderInternalCopy_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderInternalCopy.Value / 100;

            LabelInternalCopy.Text = NewValue.ToString();

            if (NewValue > MyGenTrainer.MyState.InternalCopy)
            {
                if (MyGenTrainer.MyState.InternalCrossover >= NewValue - MyGenTrainer.MyState.InternalCopy)
                    SliderInternalCrossover.Value = 100 - SliderInternalMutation.Value - SliderInternalCopy.Value;
                else if (MyGenTrainer.MyState.InternalCrossover > 0)// Get Value from Crossover
                {
                    SliderInternalMutation.Value = 100 - SliderInternalCopy.Value;
                    SliderInternalCrossover.Value = 0;
                }
                else // get it from Mutation
                    SliderInternalMutation.Value = 100 - SliderInternalCopy.Value;
            }
            else
                SliderInternalCrossover.Value = 100 - SliderInternalMutation.Value - SliderInternalCopy.Value;

            LabelInternalMutation.Text = ((float)SliderInternalMutation.Value / 100).ToString();
            MyGenTrainer.MyState.InternalMutation = (float)SliderInternalMutation.Value / 100;
            LabelInternalCrossover.Text = ((float)SliderInternalCrossover.Value / 100).ToString();
            MyGenTrainer.MyState.InternalCrossover = (float)SliderInternalCrossover.Value / 100;

            MyGenTrainer.MyState.InternalCopy = NewValue;
        }
        private void SliderAnnealing_Scroll(object sender, EventArgs e)
        {
            LabelAnnealing.Text = ((float)SliderAnnealing.Value / 100).ToString();
            MyGenTrainer.MyState.InitialInternalAnnealing = (float)SliderAnnealing.Value / 100;
        }
        private void CheckBoxAnealReduce_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxInternalAnnealStep.Enabled = CheckBoxAnealReduce.Checked;
            MyGenTrainer.MyState.ReduceAnnealing = CheckBoxAnealReduce.Checked;
        }
        private void TextBoxInternalAnnealStep_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxInternalAnnealStep.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxInternalAnnealStep.Text = MyGenTrainer.MyState.AnnealingReducingSteps.ToString();
            }
            else MyGenTrainer.MyState.AnnealingReducingSteps = NewValue;
        }
        private void SliderInternalIslands_Scroll(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.InternalIslandRestructuringNeeded = true;
            int NewValue = 1 << SliderInternalIslands.Value;
            if (MyGenTrainer.MyState.TotalInternalPopulation / NewValue < 2)
            {
                AppendFeedback("Not Enough population to share", 1);
                SliderInternalIslands.Value--;
                return;
            }
            LabelInternalIslands.Text = NewValue.ToString();
            MyGenTrainer.MyState.InitialNumberInternalIslands = NewValue;

            MyGenTrainer.MyState.TotalInternalPopulation = (MyGenTrainer.MyState.TotalInternalPopulation - (MyGenTrainer.MyState.TotalInternalPopulation % MyGenTrainer.MyState.InitialNumberInternalIslands));
            MyGenTrainer.MyState.InternalPopulationPerIsland = MyGenTrainer.MyState.TotalInternalPopulation / MyGenTrainer.MyState.InitialNumberInternalIslands;
            TextBoxInternalPopulation.Text = MyGenTrainer.MyState.TotalInternalPopulation.ToString();
        }
        private void CheckBoxinternalHalve_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxInternalIslandsStep.Enabled = CheckBoxInternalHalve.Checked;
            MyGenTrainer.MyState.HalveInternalIslands = CheckBoxInternalHalve.Checked;
        }
        private void TextBoxInternalIslandsStep_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxInternalIslandsStep.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxInternalIslandsStep.Text = MyGenTrainer.MyState.InternalIslandsHalvingSteps.ToString();
            }
            else MyGenTrainer.MyState.InternalIslandsHalvingSteps = NewValue;
        }
        private void RadioRulesOutError_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRulesOutError.Checked)
                MyGenTrainer.MyState.ScoreRule = GenTrainer.ScoreRules.RuleOutError;

            TextBoxRulesWinThreshold.Enabled = !RadioRulesOutError.Checked;
            TextBoxRulesValidThreshold.Enabled = !RadioRulesOutError.Checked;
        }
        private void RadioRules1X2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRules1X2.Checked)
                MyGenTrainer.MyState.ScoreRule = GenTrainer.ScoreRules.Rule1X2;

            TextBoxRulesWinThreshold.Enabled = RadioRules1X2.Checked;
            TextBoxRulesValidThreshold.Enabled = RadioRules1X2.Checked;
        }
        private void TextBoxRulesValidThreshold_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxRulesValidThreshold.Text, out NewValue) || NewValue < 0)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} <= Float . ", 0f, 1), 1);
                TextBoxRulesValidThreshold.Text = MyGenTrainer.MyState.ThresholdOfValid.ToString();
            }
            else MyGenTrainer.MyState.ThresholdOfValid = NewValue;
        }
        private void TextBoxRulesWinThreshold_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxRulesWinThreshold.Text, out NewValue) || NewValue < 0)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} <= Float . ", 0f, 1), 1);
                TextBoxRulesWinThreshold.Text = MyGenTrainer.MyState.ThresholdOfWin.ToString();
            }
            else MyGenTrainer.MyState.ThresholdOfWin = NewValue;
        }
    }
}
