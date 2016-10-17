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
        private enum ScrolType { New, Copy, Crossover, Mutation };
        private void SliderInternalCrossover_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderInternalCrossover.Value / 100;

            if (NewValue > MyGenTrainer.MyState.InternalCrossover)// increasing
                CalculateInternalScrolls(NewValue - MyGenTrainer.MyState.InternalCrossover, ScrolType.Crossover);
            else// decreasing
                MyGenTrainer.MyState.InternalNew += (MyGenTrainer.MyState.InternalCrossover - NewValue);

            MyGenTrainer.MyState.InternalNew = (float)Math.Round(MyGenTrainer.MyState.InternalNew, 2);
            MyGenTrainer.MyState.InternalCopy = (float)Math.Round(MyGenTrainer.MyState.InternalCopy, 2);
            MyGenTrainer.MyState.InternalCrossover = (float)Math.Round(MyGenTrainer.MyState.InternalCrossover, 2);
            MyGenTrainer.MyState.InternalMutation = (float)Math.Round(MyGenTrainer.MyState.InternalMutation, 2);

            LabelInternalNew.Text = MyGenTrainer.MyState.InternalNew.ToString();
            SliderInternalNew.Value = (int)(MyGenTrainer.MyState.InternalNew * 100);
            LabelInternalCopy.Text = MyGenTrainer.MyState.InternalCopy.ToString();
            SliderInternalCopy.Value = (int)(MyGenTrainer.MyState.InternalCopy * 100);
            LabelInternalMutation.Text = MyGenTrainer.MyState.InternalMutation.ToString();
            SliderInternalMutation.Value = (int)(MyGenTrainer.MyState.InternalMutation * 100);

            LabelInternalCrossover.Text = NewValue.ToString();
            MyGenTrainer.MyState.InternalCrossover = NewValue;
        }
        private void SliderInternalMutation_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderInternalMutation.Value / 100;

            if (NewValue > MyGenTrainer.MyState.InternalMutation)// increasing
                CalculateInternalScrolls(NewValue - MyGenTrainer.MyState.InternalMutation, ScrolType.Mutation);
            else// descrease
                MyGenTrainer.MyState.InternalNew += (MyGenTrainer.MyState.InternalMutation - NewValue);

            MyGenTrainer.MyState.InternalNew = (float)Math.Round(MyGenTrainer.MyState.InternalNew, 2);
            MyGenTrainer.MyState.InternalCopy = (float)Math.Round(MyGenTrainer.MyState.InternalCopy, 2);
            MyGenTrainer.MyState.InternalCrossover = (float)Math.Round(MyGenTrainer.MyState.InternalCrossover, 2);
            MyGenTrainer.MyState.InternalMutation = (float)Math.Round(MyGenTrainer.MyState.InternalMutation, 2);

            LabelInternalNew.Text = MyGenTrainer.MyState.InternalNew.ToString();
            SliderInternalNew.Value = (int)(MyGenTrainer.MyState.InternalNew * 100);
            LabelInternalCopy.Text = MyGenTrainer.MyState.InternalCopy.ToString();
            SliderInternalCopy.Value = (int)(MyGenTrainer.MyState.InternalCopy * 100);
            LabelInternalCrossover.Text = MyGenTrainer.MyState.InternalCrossover.ToString();
            SliderInternalCrossover.Value = (int)(MyGenTrainer.MyState.InternalCrossover * 100);

            LabelInternalMutation.Text = NewValue.ToString();
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

            if (NewValue > MyGenTrainer.MyState.InternalCopy)// increasing
                CalculateInternalScrolls(NewValue - MyGenTrainer.MyState.InternalCopy, ScrolType.Copy);
            else// descrease
                MyGenTrainer.MyState.InternalNew += (MyGenTrainer.MyState.InternalCopy - NewValue);

            MyGenTrainer.MyState.InternalNew = (float)Math.Round(MyGenTrainer.MyState.InternalNew, 2);
            MyGenTrainer.MyState.InternalCopy = (float)Math.Round(MyGenTrainer.MyState.InternalCopy, 2);
            MyGenTrainer.MyState.InternalCrossover = (float)Math.Round(MyGenTrainer.MyState.InternalCrossover, 2);
            MyGenTrainer.MyState.InternalMutation = (float)Math.Round(MyGenTrainer.MyState.InternalMutation, 2);

            LabelInternalNew.Text = MyGenTrainer.MyState.InternalNew.ToString();
            SliderInternalNew.Value = (int)(MyGenTrainer.MyState.InternalNew * 100);
            LabelInternalMutation.Text = MyGenTrainer.MyState.InternalMutation.ToString();
            SliderInternalMutation.Value = (int)(MyGenTrainer.MyState.InternalMutation * 100);
            LabelInternalCrossover.Text = MyGenTrainer.MyState.InternalCrossover.ToString();
            SliderInternalCrossover.Value = (int)(MyGenTrainer.MyState.InternalCrossover * 100);

            LabelInternalCopy.Text = NewValue.ToString();
            MyGenTrainer.MyState.InternalCopy = NewValue;
        }
        private void SliderInternalNew_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderInternalNew.Value / 100;

            if (NewValue > MyGenTrainer.MyState.InternalNew)// increasing
                CalculateInternalScrolls(NewValue - MyGenTrainer.MyState.InternalNew, ScrolType.New);
            else// descrease
                MyGenTrainer.MyState.InternalCopy += (MyGenTrainer.MyState.InternalNew - NewValue);

            MyGenTrainer.MyState.InternalNew = (float)Math.Round(MyGenTrainer.MyState.InternalNew, 2);
            MyGenTrainer.MyState.InternalCopy = (float)Math.Round(MyGenTrainer.MyState.InternalCopy, 2);
            MyGenTrainer.MyState.InternalCrossover = (float)Math.Round(MyGenTrainer.MyState.InternalCrossover, 2);
            MyGenTrainer.MyState.InternalMutation = (float)Math.Round(MyGenTrainer.MyState.InternalMutation, 2);

            LabelInternalCopy.Text = MyGenTrainer.MyState.InternalCopy.ToString();
            SliderInternalCopy.Value = (int)(MyGenTrainer.MyState.InternalCopy * 100);
            LabelInternalMutation.Text = MyGenTrainer.MyState.InternalMutation.ToString();
            SliderInternalMutation.Value = (int)(MyGenTrainer.MyState.InternalMutation * 100);
            LabelInternalCrossover.Text = MyGenTrainer.MyState.InternalCrossover.ToString();
            SliderInternalCrossover.Value = (int)(MyGenTrainer.MyState.InternalCrossover * 100);

            LabelInternalNew.Text = NewValue.ToString();
            MyGenTrainer.MyState.InternalNew = NewValue;
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
        }
        private void RadioRules1X2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRules1X2.Checked)
                MyGenTrainer.MyState.ScoreRule = GenTrainer.ScoreRules.Rule1X2;

            TextBoxRulesWinThreshold.Enabled = RadioRules1X2.Checked;
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

        private void CalculateInternalScrolls(float AmountToIncrease, ScrolType Type)
        {
            if (Type != ScrolType.New && AmountToIncrease>0)//Get it from New
            {
                if (MyGenTrainer.MyState.InternalNew >= AmountToIncrease)
                {
                    MyGenTrainer.MyState.InternalNew -= AmountToIncrease;
                    return;
                }
                else
                {
                    AmountToIncrease -= MyGenTrainer.MyState.InternalNew;
                    MyGenTrainer.MyState.InternalNew = 0;
                }
            }

            if (Type != ScrolType.Copy && AmountToIncrease > 0)//Get it from Copy
            {
                if (MyGenTrainer.MyState.InternalCopy >= AmountToIncrease)
                {
                    MyGenTrainer.MyState.InternalCopy -= AmountToIncrease;
                    return;
                }
                else
                {
                    AmountToIncrease -= MyGenTrainer.MyState.InternalCopy;
                    MyGenTrainer.MyState.InternalCopy = 0;
                }
            }

            if (Type != ScrolType.Crossover && AmountToIncrease > 0)//Get it from Crossover
            {
                if (MyGenTrainer.MyState.InternalCrossover >= AmountToIncrease)
                {
                    MyGenTrainer.MyState.InternalCrossover -= AmountToIncrease;
                    return;
                }
                else
                {
                    AmountToIncrease -= MyGenTrainer.MyState.InternalCrossover;
                    MyGenTrainer.MyState.InternalCrossover = 0;
                }
            }

            if (Type != ScrolType.Mutation && AmountToIncrease > 0)//Get it from Mutation
            {
                if (MyGenTrainer.MyState.InternalMutation >= AmountToIncrease)
                {
                    MyGenTrainer.MyState.InternalMutation -= AmountToIncrease;
                    return;
                }
                else
                {
                    AmountToIncrease -= MyGenTrainer.MyState.InternalMutation;
                    MyGenTrainer.MyState.InternalMutation = 0;
                }
            }
        }
    }
}
