using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNetworkTrainer
{
    partial class MainForm
    {
        private void SliderStructureCrossover_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderStructureCrossover.Value / 100;

            LabelStructureCrossover.Text = NewValue.ToString();

            if (NewValue > MyGenTrainer.MyState.StructureCrossover)
            {
                if (MyGenTrainer.MyState.StructureCopy >= NewValue - MyGenTrainer.MyState.StructureCrossover)
                    SliderStructureCopy.Value = 100 - SliderStructureMutation.Value - SliderStructureCrossover.Value;
                else if (MyGenTrainer.MyState.StructureCopy > 0)// Get Value from Copy
                {
                    SliderStructureMutation.Value = 100 - SliderStructureCrossover.Value;
                    SliderStructureCopy.Value = 0;
                }
                else // get it from Mutation
                    SliderStructureMutation.Value = 100 - SliderStructureCrossover.Value;
            }
            else
                SliderStructureCopy.Value = 100 - SliderStructureMutation.Value - SliderStructureCrossover.Value;

            LabelStructureCopy.Text = ((float)SliderStructureCopy.Value / 100).ToString();
            MyGenTrainer.MyState.StructureCopy = (float)SliderStructureCopy.Value / 100;
            LabelStructureMutation.Text = ((float)SliderStructureMutation.Value / 100).ToString();
            MyGenTrainer.MyState.StructureMutation = (float)SliderStructureMutation.Value / 100;

            MyGenTrainer.MyState.StructureCrossover = NewValue;
        }
        private void SliderStructureMutation_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderStructureMutation.Value / 100;

            LabelStructureMutation.Text = NewValue.ToString();

            if (NewValue > MyGenTrainer.MyState.StructureMutation)
            {
                if (MyGenTrainer.MyState.StructureCopy >= NewValue - MyGenTrainer.MyState.StructureMutation)
                    SliderStructureCopy.Value = 100 - SliderStructureMutation.Value - SliderStructureCrossover.Value;
                else if (MyGenTrainer.MyState.StructureCopy > 0)// Get Value from Copy
                {
                    SliderStructureCrossover.Value = 100 - SliderStructureMutation.Value;
                    SliderStructureCopy.Value = 0;
                }
                else // get it from Mutation
                    SliderStructureCrossover.Value = 100 - SliderStructureMutation.Value;
            }
            else
                SliderStructureCopy.Value = 100 - SliderStructureMutation.Value - SliderStructureCrossover.Value;

            LabelStructureCopy.Text = ((float)SliderStructureCopy.Value / 100).ToString();
            MyGenTrainer.MyState.StructureCopy = (float)SliderStructureCopy.Value / 100;
            LabelStructureCrossover.Text = ((float)SliderStructureCrossover.Value / 100).ToString();
            MyGenTrainer.MyState.StructureCrossover = (float)SliderStructureCrossover.Value / 100;

            MyGenTrainer.MyState.StructureMutation = NewValue;
        }
        private void SliderStructureMutationStrength_Scroll(object sender, EventArgs e)
        {
            LabelStructureMutationStrength.Text = ((float)SliderStructureMutationStrength.Value / 100).ToString();
            MyGenTrainer.MyState.StructureMutationStrength = (float)SliderStructureMutationStrength.Value / 100;
        }
        private void TextBoxLayerCost_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxLayerCost.Text, out NewValue) || NewValue < 0 || NewValue > 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} < Float < {1:0.0}. ", 0, 1), 1);
                TextBoxLayerCost.Text = MyGenTrainer.MyState.LayerCost.ToString();
            }
            else MyGenTrainer.MyState.LayerCost = NewValue;
        }
        private void TextBoxNeuronCost_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxNeuronCost.Text, out NewValue) || NewValue < 0 || NewValue > 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} < Float < {1:0.0}. ", 0, 1), 1);
                TextBoxNeuronCost.Text = MyGenTrainer.MyState.NeuronCost.ToString();
            }
            else MyGenTrainer.MyState.NeuronCost = NewValue;
        }
        private void TextBoxFunctionCost_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxFunctionCost.Text, out NewValue) || NewValue < 0 || NewValue > 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} < Float < {1:0.0}. ", 0, 1), 1);
                TextBoxFunctionCost.Text = MyGenTrainer.MyState.FunctionCost.ToString();
            }
            else MyGenTrainer.MyState.FunctionCost = NewValue;
        }
        private void TextBoxConnectionCost_TextChanged(object sender, EventArgs e)
        {
            float NewValue;
            if (!float.TryParse(TextBoxConnectionCost.Text, out NewValue) || NewValue < 0 || NewValue > 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0:0.0} < Float < {1:0.0}. ", 0, 1), 1);
                TextBoxConnectionCost.Text = MyGenTrainer.MyState.ConnectionCost.ToString();
            }
            else MyGenTrainer.MyState.ConnectionCost = NewValue;
        }
        private void CheckBoxWeightsRandom_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.WeightsRandomized = CheckBoxWeightsRandom.Checked;
        }
        private void CheckBoxBiasesRandom_CheckedChanged(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.BiasesRandomized = CheckBoxBiasesRandom.Checked;
        }
        private void SliderStructureCopy_Scroll(object sender, EventArgs e)
        {
            float NewValue = (float)SliderStructureCopy.Value / 100;

            LabelStructureCopy.Text = NewValue.ToString();

            if (NewValue > MyGenTrainer.MyState.StructureCopy)
            {
                if (MyGenTrainer.MyState.StructureCrossover >= NewValue - MyGenTrainer.MyState.StructureCopy)
                    SliderStructureCrossover.Value = 100 - SliderStructureMutation.Value - SliderStructureCopy.Value;
                else if (MyGenTrainer.MyState.StructureCrossover > 0)// Get Value from Crossover
                {
                    SliderStructureMutation.Value = 100 - SliderStructureCopy.Value;
                    SliderStructureCrossover.Value = 0;
                }
                else // get it from Mutation
                    SliderStructureMutation.Value = 100 - SliderStructureCopy.Value;
            }
            else
                SliderStructureCrossover.Value = 100 - SliderStructureMutation.Value - SliderStructureCopy.Value;

            LabelStructureMutation.Text = ((float)SliderStructureMutation.Value / 100).ToString();
            MyGenTrainer.MyState.StructureMutation = (float)SliderStructureMutation.Value / 100;
            LabelStructureCrossover.Text = ((float)SliderStructureCrossover.Value / 100).ToString();
            MyGenTrainer.MyState.StructureCrossover = (float)SliderStructureCrossover.Value / 100;

            MyGenTrainer.MyState.StructureCopy = NewValue;
        }
        private void SliderStructureIslands_Scroll(object sender, EventArgs e)
        {
            MyGenTrainer.MyState.StructIslandRestructuringNeeded = true;
            int NewValue = 1 << SliderStructureIslands.Value;
            if (MyGenTrainer.MyState.TotalStructurePopulation / NewValue < 2)
            {
                AppendFeedback("Not Enough population to share", 1);
                SliderStructureIslands.Value--;
                return;
            }
            LabelStructureIslands.Text = NewValue.ToString();
            MyGenTrainer.MyState.InitialNumberStructureIslands = NewValue;
            MyGenTrainer.MyState.CurrNumberStructureIslands = NewValue;

            MyGenTrainer.MyState.TotalStructurePopulation = (MyGenTrainer.MyState.TotalStructurePopulation - (MyGenTrainer.MyState.TotalStructurePopulation % MyGenTrainer.MyState.CurrNumberStructureIslands));
            MyGenTrainer.MyState.StructurePopulationPerIsland = MyGenTrainer.MyState.TotalStructurePopulation / MyGenTrainer.MyState.CurrNumberStructureIslands;
            TextBoxStructurePopulation.Text = MyGenTrainer.MyState.TotalStructurePopulation.ToString();
        }
        private void CheckBoxStructureHalve_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxStructureIslandsSteps.Enabled = CheckBoxStructureHalve.Checked;
            MyGenTrainer.MyState.HalveStructureIslands = CheckBoxStructureHalve.Checked;
        }
        private void TextBoxStructureIslandsSteps_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxStructureIslandsSteps.Text, out NewValue) || NewValue < 1)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 1);
                TextBoxStructureIslandsSteps.Text = MyGenTrainer.MyState.StructureIslandsHalvingSteps.ToString();
            }
            else MyGenTrainer.MyState.StructureIslandsHalvingSteps = NewValue;
        }
        private void CheckBoxScoreNeurons_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxScoreNeurons.Enabled = CheckBoxScoreNeurons.Checked;
            MyGenTrainer.MyState.ScorePenaltyNeurons = CheckBoxScoreNeurons.Checked;
        }
        private void TextBoxScoreNeurons_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxScoreNeurons.Text, out NewValue) || NewValue < 3)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 2), 1);
                TextBoxScoreNeurons.Text = MyGenTrainer.MyState.ScorePenaltyNumberOfNeurons.ToString();
            }
            else MyGenTrainer.MyState.ScorePenaltyNumberOfNeurons = NewValue;
        }
        private void CheckBoxScoreLayers_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxScoreLayer.Enabled = CheckBoxScoreLayers.Checked;
            MyGenTrainer.MyState.ScorePenaltyLayers = CheckBoxScoreLayers.Checked;
        }
        private void TextBoxScoreLayer_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxScoreLayer.Text, out NewValue) || NewValue < 3)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 2), 1);
                TextBoxScoreLayer.Text = MyGenTrainer.MyState.ScorePenaltyNumberOfLayers.ToString();
            }
            else MyGenTrainer.MyState.ScorePenaltyNumberOfLayers = NewValue;
        }
        private void CheckBoxScoreConnections_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxScoreConnections.Enabled = CheckBoxScoreConnections.Checked;
            MyGenTrainer.MyState.ScorePenaltyConnections = CheckBoxScoreConnections.Checked;
        }
        private void TextBoxScoreConnections_TextChanged(object sender, EventArgs e)
        {
            int NewValue;
            if (!int.TryParse(TextBoxScoreConnections.Text, out NewValue) || NewValue < 3)
            {
                AppendFeedback(string.Format("Invalid input. Expected {0} < Integer. ", 0), 2);
                TextBoxScoreConnections.Text = MyGenTrainer.MyState.ScorePenaltyNumberOfConnections.ToString();
            }
            else MyGenTrainer.MyState.ScorePenaltyNumberOfConnections = NewValue;
        }
    }
}
