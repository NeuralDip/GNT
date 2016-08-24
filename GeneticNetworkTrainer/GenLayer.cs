using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNetworkTrainer
{
    [Serializable]
    internal class GenLayer
    {
        private int ID;// IDs Are unique integers defined at the moment of instantiation and cannot be changed afterwards.
        private bool IsOutLayer;
        private bool IsInLayer;
        private ActivationFunction MyActivation;
        private float MaxWeight = 4;// Weights WIll be forced to stay btween -MaxWeight,+MaxWeight
        private float MaxBias = 10;// Biases WIll be forced to stay btween -MaxBias,+MaxBias
        private List<int> ListOfOutLayers;
        private List<int> ListOfInLayers;
        private float[,] Weights;
        private float[] Biases;
        private float[] Output; // The output after evaluation.

        public enum ActivationFunction
        {
            Linear = 0,
            ReLU = 1,
            SoftSign = 2,//f(x) = x / (1+|x|)
            Sigmoid = 3,// f(x) = 1/(1+exp(-x))
            Tanh = 4,
            SoftMax = 5 // f(x) = e^x/sum(e^xi)
        }

        // Constructor
        public GenLayer(int InConnections, int NumberNeurons, bool iIsInLayer, bool iIsOutLayer, ActivationFunction iActivation, int iID)
        {
            ID = iID;
            IsOutLayer = iIsOutLayer;
            IsInLayer = iIsInLayer;
            MyActivation = iActivation;

            ListOfOutLayers = new List<int>();
            ListOfInLayers = new List<int>();
            Weights = new float[InConnections, NumberNeurons];
            Biases = new float[NumberNeurons];
            Output = new float[NumberNeurons];
        }
        public GenLayer CloneMe()
        {
            GenLayer ToReturn = new GenLayer(Weights.GetLength(0), Weights.GetLength(1), IsInLayer, IsOutLayer, MyActivation, ID);

            ToReturn.ListOfOutLayers = new List<int>(ListOfOutLayers);
            ToReturn.ListOfInLayers = new List<int>(ListOfInLayers);
            Array.Copy(Weights, 0, ToReturn.Weights, 0, Weights.Length);
            Array.Copy(Biases, 0, ToReturn.Biases, 0, Biases.Length);
            return ToReturn;
        }
        public GenLayer CloneMe(int NewID)
        {
            GenLayer ToReturn = new GenLayer(Weights.GetLength(0), Weights.GetLength(1), IsInLayer, IsOutLayer, MyActivation, NewID);

            ToReturn.ListOfOutLayers = new List<int>(ListOfOutLayers);
            ToReturn.ListOfInLayers = new List<int>(ListOfInLayers);
            Array.Copy(Weights, 0, ToReturn.Weights, 0, Weights.Length);
            Array.Copy(Biases, 0, ToReturn.Biases, 0, Biases.Length);
            return ToReturn;
        }

        // Getters Setters
        public int GetID() { return ID; }
        public bool GetIsInLayer() { return IsInLayer; }
        public bool GetIsOutLayer() { return IsOutLayer; }
        public int GetInDim() { return Weights.GetLength(0); }
        public int GetOutDim() { return Weights.GetLength(1); }
        public ActivationFunction GetActivationFunc() { return MyActivation; }
        public void SetActivationFunc(ActivationFunction iActivation) { MyActivation = iActivation; }
        public float GetMaxWeight() { return MaxWeight; }
        public void SetMaxWeight(float iMaxWeight) { MaxWeight = iMaxWeight; }
        public float GetMaxBias() { return MaxBias; }
        public void SetMaxBias(float iMaxBias) { MaxBias = iMaxBias; }
        public float[] GetOutput() { return Output; }// This method is not safe. You are not supposed to modify the contents of Output... A copy is not returned for performance.
        public List<int> GetInList() { return ListOfInLayers; }// Unsafe
        public List<int> GetOutList() { return ListOfOutLayers; }//Unsafe
        public string LogMe()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Activation : " + MyActivation.ToString() + "\n");
            sb.AppendFormat("Weights :\n");
            for (int B = 0; B < Weights.GetLength(1); B++)
            {
                for (int A = 0; A < Weights.GetLength(0); A++)
                    sb.AppendFormat("{0,5:0.000}", Weights[A, B]).Append(", ");
                sb.Append("\n");
            }
            sb.AppendFormat("Biases :\n");
            for (int B = 0; B < Weights.GetLength(1); B++)
                sb.AppendFormat("{0,5:0.000}", Biases[B]).Append(", ");
            sb.Append("\n");
            return sb.ToString();
        }

        // Layer Modifiers
        public void ConnectToInput(int ID, int Length)
        {
            ListOfInLayers.Add(ID);
            float[,] NewWeights = new float[Weights.GetLength(0) + Length, Weights.GetLength(1)];
            for (int A = 0; A < Weights.GetLength(0); A++)
                for (int B = 0; B < Weights.GetLength(1); B++)
                    NewWeights[A, B] = Weights[A, B];
            Weights = NewWeights;
        }
        public bool DisconnectFromInput(int ID, int Start, int Length)
        {
            if (IsInLayer) return false;
            ListOfInLayers.Remove(ID);
            float[,] NewWeights = new float[Weights.GetLength(0) - Length, Weights.GetLength(1)];
            for (int A = 0; A < Start; A++)// Copy inputs before the one to remove
                for (int B = 0; B < Weights.GetLength(1); B++)
                    NewWeights[A, B] = Weights[A, B];
            for (int A = Start + Length; A < Weights.GetLength(0); A++)// Copy inputs after the one to remove
                for (int B = 0; B < Weights.GetLength(1); B++)
                    NewWeights[A - Length, B] = Weights[A, B];
            Weights = NewWeights;
            return true;
        }
        public bool ModifyInput(int Start, int Delta)
        {
            if (IsInLayer) return false;
            float[,] NewWeights = new float[Weights.GetLength(0) + Delta, Weights.GetLength(1)];
            if (Delta < 0)
            {
                for (int A = 0; A < Start; A++)
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        NewWeights[A, B] = Weights[A, B];
                for (int A = Start - Delta; A < Weights.GetLength(0); A++)
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        NewWeights[A + Delta, B] = Weights[A, B];
            }
            else
            {
                for (int A = 0; A < Start; A++)
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        NewWeights[A, B] = Weights[A, B];
                for (int A = Start; A < Weights.GetLength(0); A++)
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        NewWeights[A + Delta, B] = Weights[A, B];
            }
            Weights = NewWeights;
            return true;
        }
        public void ConnectToOutput(int ID) { ListOfOutLayers.Add(ID); }
        public void DisconnectFromOutput(int ID) { ListOfOutLayers.Remove(ID); }
        public void Clear()
        {
            if (!IsOutLayer)
                ListOfOutLayers.Clear();
            if (!IsInLayer)
            {
                ListOfInLayers.Clear();
                Weights = new float[0, Biases.Length];//inputs are zero and so is dim0
            }
        }
        public void SetNumberOfNeurons(int Delta, Random Rnd)
        {
            if (IsOutLayer || Biases.Length + Delta <= 0) return;
            float[,] NewWeights = new float[Weights.GetLength(0), Biases.Length + Delta];
            float[] NewBiases = new float[Biases.Length + Delta];

            if (Delta > 0)// Will add new neurons
            {

                for (int B = 0; B < Biases.Length; B++)
                {
                    NewBiases[B] = Biases[B];
                    for (int A = 0; A < Weights.GetLength(0); A++)
                        NewWeights[A, B] = Weights[A, B];
                }

                for (int B = Biases.Length; B < Biases.Length + Delta; B++)
                {
                    NewBiases[B] = (float)(Rnd.NextDouble() * 2 * MaxBias - MaxBias);
                    for (int A = 0; A < Weights.GetLength(0); A++)
                        NewWeights[A, B] = (float)(Rnd.NextDouble() * 2 * MaxWeight - MaxWeight);

                }
            }
            else // Will remove neurons from the end
            {
                for (int B = 0; B < Biases.Length + Delta; B++)
                {
                    NewBiases[B] = Biases[B];
                    for (int A = 0; A < Weights.GetLength(0); A++)
                        NewWeights[A, B] = Weights[A, B];
                }
            }
            Weights = NewWeights;
            Biases = NewBiases;
            Output = new float[Biases.Length];
        }
        public void ResetWeights(bool Randomize, Random Rnd)
        {
            if (Randomize)
            {
                for (int A = 0; A < Weights.GetLength(0); A++)
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        Weights[A, B] = (float)(Rnd.NextDouble() * 2 * MaxWeight - MaxWeight);
            }
            else
            {
                for (int A = 0; A < Weights.GetLength(0); A++)
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        Weights[A, B] = 0;
            }
        }
        public void ResetBiases(bool Randomize, Random Rnd)
        {
            if (Randomize)
            {
                for (int B = 0; B < Weights.GetLength(1); B++)
                    Biases[B] = (float)(Rnd.NextDouble() * 2 * MaxBias - MaxBias);
            }
            else
            {
                for (int B = 0; B < Weights.GetLength(1); B++)
                    Biases[B] = 0;
            }
        }
        public void MutateLayerWeights(float Strength, float Annealing, Random Rnd)
        {
            for (int B = 0; B < Weights.GetLength(1); B++)
            {
                for (int A = 0; A < Weights.GetLength(0); A++)
                    Weights[A, B] = KeepInBounds(MaxWeight, (float)(Weights[A, B] + (Rnd.NextDouble() * 2 * Strength - Strength) + Annealing * (Rnd.NextDouble() * 2 - 1)));
            }
        }
        public void MutateLayerBiases(float Strength, float Annealing, Random Rnd)
        {
            for (int B = 0; B < Weights.GetLength(1); B++)
                Biases[B] = KeepInBounds(MaxBias, (float)(Biases[B] + (Rnd.NextDouble() * 2 * Strength - Strength) + Annealing * (Rnd.NextDouble() * 2 - 1)));
        }
        public void CrossoverLayer(GenLayer OtherParent, Random Rnd)
        {
            //Crossover Weights
            int StartIndex = Rnd.Next((int)Math.Ceiling((float)Weights.Length / 2));
            Array.Copy(OtherParent.Weights, StartIndex, Weights, StartIndex, Weights.Length / 2);// Copy gen code from other parent into self

            //Crossover Biases
            StartIndex = Rnd.Next((int)Math.Ceiling((float)Biases.Length / 2));
            Array.Copy(OtherParent.Biases, StartIndex, Biases, StartIndex, Biases.Length / 2);
        }

        // Evaluate
        public float[] Evaluate(float[] Input)
        {
            if (Input.Length == 0)// Layer is not In Connected
                for (int B = 0; B < Weights.GetLength(1); B++)
                    Output[B] = 0;
            else
                for (int B = 0; B < Weights.GetLength(1); B++)
                {
                    Output[B] = 0;
                    for (int A = 0; A < Weights.GetLength(0); A++)
                        Output[B] += Weights[A, B] * Input[A];
                    Output[B] += Biases[B];
                }
            Activate();
            return Output;
        }

        // Privates
        private void Activate()
        {
            switch (MyActivation)
            {
                case ActivationFunction.Linear:
                    // Nothing to Do...
                    break;
                case ActivationFunction.ReLU:
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        if (Output[B] < 0) Output[B] = 0;
                    break;
                case ActivationFunction.Sigmoid:
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        Output[B] = (float)(1 / (1 + Math.Exp(-Output[B])));
                    break;
                case ActivationFunction.SoftMax:
                    double ExpSum = 0;
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        ExpSum += Math.Exp(Output[B]);
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        Output[B] = (float)(Math.Exp(Output[B]) / ExpSum);
                    break;
                case ActivationFunction.SoftSign:
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        Output[B] = (float)(Output[B] / (1 + Math.Abs(Output[B])));
                    break;
                case ActivationFunction.Tanh:
                    for (int B = 0; B < Weights.GetLength(1); B++)
                        Output[B] = (float)Math.Tanh(Output[B]);
                    break;
                default:
                    break;
            }
        }
        private float KeepInBounds(float Limit, float Value)
        {
            if (Value > Limit)
                return Limit - (Value - Limit);
            else if (Value < -Limit)
                return -Limit - (Value + Limit);
            else return Value;
        }
    }
}
