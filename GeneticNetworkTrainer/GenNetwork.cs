using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace GeneticNetworkTrainer
{
    [Serializable]
    public class GenNetwork : IComparable<GenNetwork>
    {
        private List<string> ActionHistory = new List<string>(); // For Debug purposes
        [Serializable]
        private class Adjacency
        {
            public int[,] AdjacencyMatrix;
            public List<int> IDsOrder;// Input ID will always be in 0 position and Output ID will always be in the last position
            public List<int> EvaluationOrder;
            public Adjacency(int Dimention) { AdjacencyMatrix = new int[Dimention, Dimention]; IDsOrder = new List<int>(); }
            public Adjacency CloneMe()
            {

                Adjacency ToReturn = new Adjacency(IDsOrder.Count);
                Array.Copy(AdjacencyMatrix, 0, ToReturn.AdjacencyMatrix, 0, AdjacencyMatrix.Length);
                ToReturn.IDsOrder = new List<int>(IDsOrder);
                ToReturn.EvaluationOrder = new List<int>(EvaluationOrder);
                return ToReturn;
            }

            public void ConnectFromTo(int FromID, int ToID)
            {
                AdjacencyMatrix[IDsOrder.IndexOf(FromID), IDsOrder.IndexOf(ToID)] = 1;
                EvalOrder();
            }
            public bool RemoveFromTo(int FromID, int ToID)
            {
                int FromIndx = IDsOrder.IndexOf(FromID);
                int ToIndx = IDsOrder.IndexOf(ToID);

                if (AdjacencyMatrix[FromIndx, ToIndx] == 1)
                {
                    AdjacencyMatrix[FromIndx, ToIndx] = 0;
                    EvalOrder();
                    return true;
                }
                else return false;
            }
            public bool RemoveNode(int ID)
            {
                int LocalIndx = 0;
                if (!IDsOrder.Contains(ID)) return false;
                else LocalIndx = IDsOrder.IndexOf(ID);

                bool OverI = false;
                bool OverJ = false;
                int[,] NewMat = new int[IDsOrder.Count - 1, IDsOrder.Count - 1];
                for (int i = 0; i < IDsOrder.Count; i++)
                {
                    if (i == LocalIndx) { OverI = true; continue; }
                    OverJ = false;
                    for (int j = 0; j < IDsOrder.Count; j++)
                    {
                        if (j == LocalIndx) { OverJ = true; continue; }
                        NewMat[i - (OverI ? 1 : 0), j - (OverJ ? 1 : 0)] = AdjacencyMatrix[i, j];
                    }
                }

                IDsOrder.Remove(ID);
                AdjacencyMatrix = NewMat;
                EvalOrder();
                return true;
            }
            public void AddNode(int ID, bool InLayer, bool OutLayer)
            {
                int[,] NewMat = new int[IDsOrder.Count + 1, IDsOrder.Count + 1];
                if (InLayer)//Adjmatrix is empty. Just add the in node
                    IDsOrder.Insert(IDsOrder.Count, ID);
                else if (OutLayer)// adding the OutNode
                {
                    for (int i = 0; i < IDsOrder.Count; i++)
                        for (int j = 0; j < IDsOrder.Count; j++)
                            NewMat[i, j] = AdjacencyMatrix[i, j];
                    IDsOrder.Insert(IDsOrder.Count, ID);
                }
                else// Adding a hidden node. In and Out layers are already in
                {
                    for (int i = 0; i < IDsOrder.Count - 1; i++)
                        for (int j = 0; j < IDsOrder.Count - 1; j++)
                            NewMat[i, j] = AdjacencyMatrix[i, j];
                    for (int i = 0; i < IDsOrder.Count - 1; i++)
                        NewMat[i, IDsOrder.Count] = AdjacencyMatrix[i, IDsOrder.Count - 1];// add the output layer allways to the last position
                    IDsOrder.Insert(IDsOrder.Count - 1, ID);
                }
                AdjacencyMatrix = NewMat;
                EvalOrder();
            }
            public int[] RemoveCycles()// returns Indexes(Not IDs) of edge to remove
            {
                List<List<int>> Cycles = new List<List<int>>();
                FindAllCycles(new List<int>(), Cycles, 0);
                if (Cycles.Count == 0) return new int[2] { -1, -1 };//No cycles in the net

                List<int[]> Edges = new List<int[]>();//[StartLayerID,EndLayerID] 
                List<int> Encounters = new List<int>();
                foreach (List<int> Cycle in Cycles)// Count all the times an edge is encountered in the various cycles
                {
                    int[] CurrEdge;
                    for (int Cnt = 0; Cnt < Cycle.Count; Cnt++)
                    {
                        if (Cnt == Cycle.Count - 1) CurrEdge = new int[2] { Cycle[Cnt], Cycle[0] };
                        else CurrEdge = new int[2] { Cycle[Cnt], Cycle[Cnt + 1] };

                        if (Edges.Contains(CurrEdge)) Encounters[Edges.IndexOf(CurrEdge)]++;
                        else
                        {
                            Edges.Add(CurrEdge);
                            Encounters.Add(1);
                        }
                    }
                }
                int[] EdgeSelected = Edges[Encounters.IndexOf(Encounters.Max())];
                AdjacencyMatrix[EdgeSelected[0], EdgeSelected[1]] = 0;
                EvalOrder();
                return new int[2] { EdgeSelected[0], EdgeSelected[1] };
            }
            public bool IsInMainPath(int FromID, int ToID, bool RemoveLayer)
            {// returns answer to the question : should i not remove the layer/connection ?
                List<int> IOPath = new List<int>();
                if (!InOutPath(new List<int>(), IOPath, 1)) return true;// Network is already disconnected!!

                if (!IOPath.Contains(FromID)) return false; // FromId is not in the path so we can remove 

                // from now on we know the FromID is in the path

                if (RemoveLayer) return true;// Layer is in the IO path

                // We are removing a connection, we have to check if the connection is part of the IOPath(FromID and ToID have to be in consecutive positions)
                int IndexOfFrom = IOPath.IndexOf(FromID);
                if (IndexOfFrom == IOPath.Count - 1) return true;// this is the out layer, We should never really arrive here
                else if (IOPath[IndexOfFrom + 1] == ToID) return true; // FromID and ToID are in consecutive indexes, so we cannot remove connection
                else return false;

            }
            public void Crossover(Adjacency OtherAdjacency, int COConnections1, int COConnections2, int NumberOfLayers)
            {
                int RawOfCoConn2 = (int)Math.Ceiling((float)COConnections2 / (float)NumberOfLayers);
                Array.Copy(OtherAdjacency.AdjacencyMatrix, COConnections1, AdjacencyMatrix, COConnections1, COConnections2 - COConnections1);
                while (RawOfCoConn2 < IDsOrder.Count - 1 && IDsOrder.Count != OtherAdjacency.IDsOrder.Count) RemoveNode(-1);
                EvalOrder();
            }
            public void EvalOrder()
            {//Evaluate the order with which layers will be called
                List<int> ToReturn = new List<int>();
                int[,] LocalAdj = new int[IDsOrder.Count, IDsOrder.Count];
                Buffer.BlockCopy(AdjacencyMatrix, 0, LocalAdj, 0, sizeof(int) * IDsOrder.Count * IDsOrder.Count);

                while (ToReturn.Count < IDsOrder.Count)
                {
                    for (int CurrIndx = 0; CurrIndx < IDsOrder.Count; CurrIndx++)
                    {
                        bool NoIns = true;
                        for (int Cnt = 0; Cnt < IDsOrder.Count; Cnt++) if (LocalAdj[Cnt, CurrIndx] == 1) { NoIns = false; break; } // Check if there are any inputs pending
                        if (NoIns)
                        {
                            ToReturn.Add(IDsOrder[CurrIndx]);
                            for (int Cnt = 0; Cnt < IDsOrder.Count; Cnt++) LocalAdj[CurrIndx, Cnt] = 0;// remove dependence to outs
                        }
                    }
                }

                EvaluationOrder = ToReturn;
            }

            private void FindAllCycles(List<int> CurrentCycleVisited, List<List<int>> Cycles, int CurrNode)
            {
                CurrentCycleVisited.Add(CurrNode);
                for (int OutEdgeCnt = 0; OutEdgeCnt < AdjacencyMatrix.GetLength(0); OutEdgeCnt++)
                {
                    if (AdjacencyMatrix[IDsOrder.IndexOf(CurrNode), OutEdgeCnt] == 1)//CurrNode Is connected with OutEdgeCnt
                    {
                        if (CurrentCycleVisited.Contains(IDsOrder[OutEdgeCnt]))
                        {
                            int StartIndex = CurrentCycleVisited.IndexOf(IDsOrder[OutEdgeCnt]);
                            int EndIndex = CurrentCycleVisited.IndexOf(CurrNode);
                            Cycles.Add(CurrentCycleVisited.GetRange(StartIndex, EndIndex - StartIndex + 1));
                        }
                        else
                        {
                            FindAllCycles(new List<int>(CurrentCycleVisited), Cycles, IDsOrder[OutEdgeCnt]);
                        }
                    }
                }
            }
            private bool InOutPath(List<int> AllNodesVisited, List<int> IOPath, int CurrNode)
            {// this is one of the paths from the input to the output. At least one of these must survive
             // We start this algo from the Out and make our way to the In
                AllNodesVisited.Add(CurrNode);
                if (CurrNode == 0) { IOPath.Add(CurrNode); return true; }// Arrived at the Out Node

                for (int OutEdgeCnt = AdjacencyMatrix.GetLength(0) - 1; OutEdgeCnt >= 0; OutEdgeCnt--)
                {
                    if (AdjacencyMatrix[OutEdgeCnt, IDsOrder.IndexOf(CurrNode)] == 1)//CurrNode Is connected with OutEdgeCnt
                        if (!AllNodesVisited.Contains(IDsOrder[OutEdgeCnt]))
                            if (InOutPath(AllNodesVisited, IOPath, IDsOrder[OutEdgeCnt])) { IOPath.Add(CurrNode); return true; }
                }
                return false;
            }
        }
        private Dictionary<int, GenLayer> AllLayers;
        private Adjacency AdjacencyObject;
        private int IncrementalID;

        private float Score = float.MinValue;
        private float TestScore = float.MinValue;
        private float OutError = 0;
        private float TestOutError = 0;

        private GenNetwork() { AdjacencyObject = new Adjacency(0); AllLayers = new Dictionary<int, GenLayer>(); }
        public GenNetwork(int InDimention, int InNeurons, int OutNeurons, bool[] FixedActivations, int[] FixedActivationFunctions, params int[] HiddenDimentions)// each integer in the array represents the number of neurons in each hidden layer. Layers are initially connected in series.
        {
            AllLayers = new Dictionary<int, GenLayer>();
            AdjacencyObject = new Adjacency(0);
            AdjacencyObject.AddNode(IncrementalID, true, false);
            AllLayers.Add(IncrementalID, new GenLayer(InDimention, InNeurons, true, false, IncrementalID++, FixedActivations[1] ? (GenLayer.ActivationFunction)FixedActivationFunctions[1] : GenLayer.ActivationFunction.Linear));
            for (int Cnt = 1; Cnt < HiddenDimentions.Length + 1; Cnt++)
            {
                AdjacencyObject.AddNode(IncrementalID, false, false);
                AllLayers.Add(IncrementalID, new GenLayer(0, HiddenDimentions[Cnt - 1], false, false, IncrementalID++, FixedActivations[1] ? (GenLayer.ActivationFunction)FixedActivationFunctions[1] : GenLayer.ActivationFunction.Linear));
                AddConnection(IncrementalID - 2, IncrementalID - 1);
            }
            AdjacencyObject.AddNode(IncrementalID, false, true);
            AllLayers.Add(IncrementalID, new GenLayer(0, OutNeurons, false, true, IncrementalID++, FixedActivations[0] ? (GenLayer.ActivationFunction)FixedActivationFunctions[0] : GenLayer.ActivationFunction.Linear));
            AddConnection(IncrementalID - 2, IncrementalID - 1);
        }

        public GenNetwork CloneMe(bool Reset, bool RandomizeWeights, bool RandomizeBiases, Random Rnd)
        {
            GenNetwork TempNetwork = new GenNetwork();

            TempNetwork.IncrementalID = 0;
            foreach (GenLayer CurrLayer in AllLayers.Values)
            {
                TempNetwork.AllLayers.Add(CurrLayer.GetID(), CurrLayer.CloneMe());
                if (CurrLayer.GetID() > TempNetwork.IncrementalID) TempNetwork.IncrementalID = CurrLayer.GetID();
            }
            TempNetwork.IncrementalID++;
            TempNetwork.AdjacencyObject = AdjacencyObject.CloneMe();
            TempNetwork.Score = Score;
            if (Reset)
            {
                foreach (GenLayer CurrLayer in AllLayers.Values)
                {
                    CurrLayer.ResetWeights(RandomizeWeights, Rnd);
                    CurrLayer.ResetBiases(RandomizeBiases, Rnd);
                }
                Score = 0;
            }
            TempNetwork.ActionHistory = new List<string>(ActionHistory);
            if (TempNetwork.ActionHistory[TempNetwork.ActionHistory.Count - 1] != "Cloning...")
                TempNetwork.ActionHistory.Add("Cloning...");
            return TempNetwork;
        }

        public float GetScore() { return Score; }
        public float GetTestScore() { return TestScore; }
        public float GetOutError() { return OutError; }
        public float GetTestOutError() { return TestOutError; }
        public int GetLayersNumber() { return AllLayers.Count; }
        public int GetWeightsNumber() { int TotatWeights = 0; for (int Cnt = 0; Cnt < AllLayers.Count; Cnt++) TotatWeights += AllLayers[AdjacencyObject.IDsOrder[Cnt]].GetInDim() * AllLayers[AdjacencyObject.IDsOrder[Cnt]].GetOutDim(); return TotatWeights; }
        public int GetNeuronsNumber() { int TotatNeurons = 0; for (int Cnt = 0; Cnt < AllLayers.Count; Cnt++) TotatNeurons += AllLayers[AdjacencyObject.IDsOrder[Cnt]].GetOutDim(); return TotatNeurons; }
        public int GetConnectionsNumber()
        {
            int TotatConnections = 0;
            for (int CntX = 0; CntX < AdjacencyObject.IDsOrder.Count; CntX++)
                for (int CntY = 0; CntY < AdjacencyObject.IDsOrder.Count; CntY++)
                    TotatConnections += AdjacencyObject.AdjacencyMatrix[CntY, CntX];
            return TotatConnections;
        }
        public int GetNetInDimention() { return AllLayers[0].GetInDim(); }
        public int GetNetOutDimention() { return AllLayers[1].GetOutDim(); }
        public float[] GetNetOutput() { return AllLayers[1].GetOutput(); }
        private float[] GetOutputByID(int ID) { return AllLayers[ID].GetOutput(); }
        public string LogMeDesciption()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.AppendFormat("IDs : \n");
            for (int CntV = 0; CntV < AdjacencyObject.AdjacencyMatrix.GetLength(0); CntV++)
                sb.Append(" - " + AdjacencyObject.IDsOrder[CntV]);
            sb.Append("\n");
            sb.AppendFormat("Adjacency : \n");
            for (int CntV = 0; CntV < AdjacencyObject.AdjacencyMatrix.GetLength(0); CntV++)
            {
                for (int CntH = 0; CntH < AdjacencyObject.AdjacencyMatrix.GetLength(1); CntH++)
                    sb.Append(" - " + AdjacencyObject.AdjacencyMatrix[CntV, CntH]);
                sb.Append("\n");
            }
            sb.Append("\nScore : " + Score + "\n");
            sb.Append("Test Score : " + TestScore + "\n");
            sb.Append("Out Error : " + OutError + "\n");
            sb.Append("Test Out Error : " + TestOutError + "\n");
            sb.Append("Layers : " + GetLayersNumber() + "\n");
            sb.Append("Neurons : " + GetNeuronsNumber() + "\n");
            sb.Append("Connections : " + GetConnectionsNumber() + "\n");
            sb.Append("\n");
            return sb.ToString();
        }
        public string LogMeParams()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            foreach (GenLayer CurrLayer in AllLayers.Values)
            {
                sb.AppendFormat("Params of Layer with ID :" + CurrLayer.GetID() + " \n");
                sb.Append(CurrLayer.LogMe());
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public float[] EvaluateNet(float[] GlobalInputs)
        {
            for (int Cnt = 0; Cnt < AdjacencyObject.EvaluationOrder.Count; Cnt++)
            {
                GenLayer CurrLayer = AllLayers[AdjacencyObject.EvaluationOrder[Cnt]];
                float[] Inputs;
                if (CurrLayer.GetIsInLayer()) Inputs = GlobalInputs;
                else Inputs = ConstructInputs(CurrLayer.GetInList());

                CurrLayer.Evaluate(Inputs);
            }
            return GetNetOutput();
        }
        public void ResetScores() { Score = 0; TestScore = 0; OutError = 0; TestOutError = 0; }
        public bool CalculateScores(List<float[]> InData, List<float[]> Labels, int DataToUse, bool Test, GenTrainer.ScoreRules ScoreRule, float WinThresh)
        {
            // CalculateScores Calculates the scores and the Outerrors for all Data 
            float[] Output = GetNetOutput();
            if (InData[0].Length != AllLayers[0].GetInDim() ||
                Labels[0].Length != AllLayers[1].GetOutDim() ||
                InData.Count != Labels.Count)
                return false;
            for (int Cnt = 0; Cnt < DataToUse; Cnt++)
            {
                EvaluateNet(InData[Cnt]);

                for (int OutCnt = 0; OutCnt < Labels[Cnt].Length; OutCnt++)
                    if ((Cnt & 1) == 1 && Test)
                        TestOutError += Math.Abs(Labels[Cnt][OutCnt] - Output[OutCnt]);
                    else
                        OutError += Math.Abs(Labels[Cnt][OutCnt] - Output[OutCnt]);

                if (ScoreRule == GenTrainer.ScoreRules.RuleOutError)
                {
                    TestScore = -TestOutError;
                    Score = -OutError;
                }
                else
                {
                    for (int OutCnt = 0; OutCnt < Labels[Cnt].Length; OutCnt++)
                    {
                        if ((Cnt & 1) == 1 && Test)
                            TestScore += Get1X2Score(InData[Cnt][(InData[Cnt].Length / 3) * (OutCnt + 1) - 1], Output[OutCnt], Labels[Cnt][OutCnt], WinThresh);
                        else
                            Score += Get1X2Score(InData[Cnt][(InData[Cnt].Length / 3) * (OutCnt + 1) - 1], Output[OutCnt], Labels[Cnt][OutCnt], WinThresh);
                    }
                }
            }
            return true;
        }

        // internal GA
        public GenNetwork MutateInternal(bool MutateWeights, bool MutateBiases, float MutationStength, float Annealing, Random Rnd)
        {
            GenNetwork NetToReturn = CloneMe(false, false, false, null);
            foreach (KeyValuePair<int, GenLayer> CurrLayer in NetToReturn.AllLayers)
            {
                if (MutateWeights)
                    NetToReturn.AllLayers[CurrLayer.Key].MutateLayerWeights(MutationStength, Annealing, Rnd);
                if (MutateBiases)
                    NetToReturn.AllLayers[CurrLayer.Key].MutateLayerBiases(MutationStength, Annealing, Rnd);
            }

            return NetToReturn;
        }
        public GenNetwork CrossoverInternal(GenNetwork OtherParent, bool MutateWeights, bool MutateBiases, float Annealing, Random Rnd)
        {
            GenNetwork NetToReturn = CloneMe(false, false, false, null);
            foreach (KeyValuePair<int, GenLayer> CurrLayer in NetToReturn.AllLayers)
            {
                NetToReturn.AllLayers[CurrLayer.Key].CrossoverLayer(OtherParent.AllLayers[CurrLayer.Key], Rnd);
                // minor Mutation is Applied in order to avoid duplicates
                if (MutateWeights)
                    NetToReturn.AllLayers[CurrLayer.Key].MutateLayerWeights(0.001f + Annealing, 0, Rnd);
                if (MutateBiases)
                    NetToReturn.AllLayers[CurrLayer.Key].MutateLayerBiases(0.001f + Annealing, 0, Rnd);
            }

            return NetToReturn;
        }

        // Structure GA
        public GenNetwork MutateStruct(float MutationStength, float[] Costs, bool[] FixedActivations, int[] FixedActivationFunctions, bool[] PenaltyBools, int[] PenaltyValues, Random Rnd)
        {                                     //Costs:[LayerCost, FunctionCost, NeuronsCost, ConnectionsCost]     //Penalties:[Layers,Neurons,Connections]
            GenNetwork Child = CloneMe(false, false, false, null);
            int[] Mutations = GetMutations(MutationStength, Costs, Rnd);
            // Create or destroy Layers
            float PenaltyThreashold;
            if (PenaltyBools[0] && PenaltyValues[0] < Child.GetLayersNumber())
                PenaltyThreashold = 0.5f - (PenaltyValues[0] - Child.GetLayersNumber()) / PenaltyValues[0];
            else PenaltyThreashold = 0.5f;

            for (int Cnt = 0; Cnt < Mutations[0]; Cnt++)
            {
                if (Rnd.NextDouble() > PenaltyThreashold)//create
                    Child.AddLayer(new GenLayer(0, 1, false, false, Child.IncrementalID, FixedActivations[1] ? (GenLayer.ActivationFunction)FixedActivationFunctions[1] : GenLayer.ActivationFunction.Linear));
                else//destroy
                {
                    int LayerID = Child.AdjacencyObject.IDsOrder[Rnd.Next(1, Child.AllLayers.Count - 1)];
                    if (!Child.AdjacencyObject.IsInMainPath(LayerID, 0, true))
                        Child.RemoveLayer(LayerID);
                }
            }

            // Change activation function
            for (int Cnt = 0; Cnt < Mutations[1]; Cnt++)
            {
                GenLayer CurrLayer = Child.AllLayers[Child.AdjacencyObject.IDsOrder[Rnd.Next(Child.AllLayers.Count)]];
                if ((CurrLayer.GetIsOutLayer() && !FixedActivations[0]) || !CurrLayer.GetIsOutLayer() && !FixedActivations[1])
                    CurrLayer.SetActivationFunc((GenLayer.ActivationFunction)(Rnd.Next(Enum.GetValues(typeof(GenLayer.ActivationFunction)).Length)));
            }
            // Change the number of neurons to one of the layers
            if (PenaltyBools[1] && PenaltyValues[1] < Child.GetLayersNumber())
                PenaltyThreashold = 0.5f - (PenaltyValues[1] - Child.GetNeuronsNumber()) / PenaltyValues[1];
            else PenaltyThreashold = 0.5f;

            for (int Cnt = 0; Cnt < Mutations[2]; Cnt++)
            {
                int ChildIDToChange = Child.AdjacencyObject.IDsOrder[Rnd.Next(Child.AllLayers.Count)];
                int Delta = Rnd.NextDouble() > PenaltyThreashold ? 1 : -1;

                if (Child.AllLayers[ChildIDToChange].GetOutDim() + Delta <= 0) continue;

                Child.AllLayers[ChildIDToChange].SetNumberOfNeurons(Delta, Rnd);
                Child.ActionHistory.Add("Change Neurons (ID, Delta) (" + ChildIDToChange + ", " + Delta + ")");
                foreach (int CurrOutID in Child.AllLayers[ChildIDToChange].GetOutList())
                {
                    int Start = 0;
                    foreach (int CurrInID in Child.AllLayers[CurrOutID].GetInList())
                    {
                        if (CurrInID == ChildIDToChange) break;
                        Start += Child.AllLayers[CurrInID].GetOutput().Length;
                    }
                    Child.AllLayers[CurrOutID].ModifyInput(Start, Delta);
                }
            }
            // Add or remove a connection
            if (PenaltyBools[2] && PenaltyValues[2] < Child.GetLayersNumber())
                PenaltyThreashold = 0.5f - (PenaltyValues[2] - Child.GetConnectionsNumber()) / PenaltyValues[2];
            else PenaltyThreashold = 0.5f;

            for (int Cnt = 0; Cnt < Mutations[3]; Cnt++)
            {
                GenLayer LayerSelected = Child.AllLayers[Child.AdjacencyObject.IDsOrder[Rnd.Next(Child.AllLayers.Count)]];
                if (Rnd.NextDouble() > PenaltyThreashold)//Add Connections
                    Child.AddConnection(LayerSelected.GetID(), Child.AdjacencyObject.IDsOrder[Rnd.Next(Child.AllLayers.Count)]);
                else //Remove Connection
                {
                    List<int> OutIds = LayerSelected.GetOutList();

                    if (OutIds.Count != 0)
                    {
                        int FromID = LayerSelected.GetID();
                        int ToID = OutIds[Rnd.Next(OutIds.Count)];
                        if (!Child.AdjacencyObject.IsInMainPath(FromID, ToID, false))
                            Child.RemoveConnection(FromID, ToID);
                    }
                }
            }

            // Remove Cycles
            int[] EdgeToRemove = Child.AdjacencyObject.RemoveCycles();
            while (EdgeToRemove[0] != -1)
            {
                Child.RemoveConnection(EdgeToRemove[0], EdgeToRemove[1]);
                EdgeToRemove = Child.AdjacencyObject.RemoveCycles();
            }

            return Child;
        }
        public GenNetwork CrossoverStruct(GenNetwork OtherParent, Random Rnd)
        {
            GenNetwork Child;
            Adjacency Parent2Adjacency;// For parent1 adjacency the child will be used
            Dictionary<int, GenLayer> Parent1Layers, Parent2Layers;
            //GraphX crossover implemented
            int NumberOfLayers = Math.Max(OtherParent.AllLayers.Count, AllLayers.Count);
            int MinNumberOfLayers = Math.Min(OtherParent.AllLayers.Count, AllLayers.Count);
            int[] LayerSelector = new int[NumberOfLayers];//Used for the types of the Layers(Neuron number, activation function etc)
            int COLayer1 = Rnd.Next(0, NumberOfLayers);
            int COLayer2 = Rnd.Next(COLayer1, NumberOfLayers);
            for (int Cnt = COLayer1; Cnt < COLayer2 - 1; Cnt++) LayerSelector[Cnt] = 1;
            int COConnections1 = Rnd.Next(NumberOfLayers * NumberOfLayers - 1); // Used for the connections
            int COConnections2 = Rnd.Next(COConnections1, NumberOfLayers * NumberOfLayers);

            //Parent1 and Child are always the small ones
            if (NumberOfLayers == MinNumberOfLayers) // two parents have same dimension
            {
                Parent2Adjacency = OtherParent.AdjacencyObject.CloneMe();
                Child = CloneMe(false, false, false, null);
                Parent1Layers = new Dictionary<int, GenLayer>(AllLayers);
                Parent2Layers = new Dictionary<int, GenLayer>(OtherParent.AllLayers);
            }
            else if (NumberOfLayers == OtherParent.AllLayers.Count) // OtherPerent is bigger
            {
                Parent2Adjacency = OtherParent.AdjacencyObject.CloneMe();
                Child = CloneMe(false, false, false, null);
                for (int Cnt = 0; Cnt < NumberOfLayers - MinNumberOfLayers; Cnt++) Child.AdjacencyObject.AddNode(-1, false, false);//make Adjaceny matrices of same dim
                Parent1Layers = new Dictionary<int, GenLayer>(AllLayers);
                Parent2Layers = new Dictionary<int, GenLayer>(OtherParent.AllLayers);
            }
            else //if (NumberOfLayers == OtherParent.AllLayers.Count) // this is bigger
            {
                Parent2Adjacency = AdjacencyObject.CloneMe();
                Child = OtherParent.CloneMe(false, false, false, null);
                for (int Cnt = 0; Cnt < NumberOfLayers - MinNumberOfLayers; Cnt++) Child.AdjacencyObject.AddNode(-1, false, false);//make Adjaceny matrices of same dim
                Parent1Layers = new Dictionary<int, GenLayer>(OtherParent.AllLayers);
                Parent2Layers = new Dictionary<int, GenLayer>(AllLayers);
            }

            Child.AdjacencyObject.Crossover(Parent2Adjacency, COConnections1, COConnections2, NumberOfLayers);

            // Create Net

            Child.AllLayers = new Dictionary<int, GenLayer>();
            Child.IncrementalID = Math.Max(Child.AdjacencyObject.IDsOrder.Max(), Parent2Adjacency.IDsOrder.Max()) + 1;
            for (int Cnt = 0; Cnt < Child.AdjacencyObject.IDsOrder.Count; Cnt++)//put the layers
            {
                if (LayerSelector[Cnt] == 0 && Child.AdjacencyObject.IDsOrder[Cnt] != -1)//take the Layer from Parent1
                    Child.AllLayers.Add(Child.AdjacencyObject.IDsOrder[Cnt], Parent1Layers[Child.AdjacencyObject.IDsOrder[Cnt]].CloneMe());
                else//take the Layer from Parent2
                {
                    if (Child.AllLayers.Keys.Contains(Parent2Adjacency.IDsOrder[Cnt]))
                    {
                        while ((Child.AdjacencyObject.IDsOrder.Contains(Child.IncrementalID))) Child.IncrementalID++;// Make sure we found an unused ID
                        Child.AllLayers.Add(Child.IncrementalID, Parent2Layers[Parent2Adjacency.IDsOrder[Cnt]].CloneMe(Child.IncrementalID));
                        Child.AdjacencyObject.IDsOrder[Cnt] = Child.IncrementalID++;//in case Child.AdjacencyObject.IDsOrder[Cnt]=-1
                    }
                    else
                    {
                        Child.AllLayers.Add(Parent2Adjacency.IDsOrder[Cnt], Parent2Layers[Parent2Adjacency.IDsOrder[Cnt]].CloneMe());
                        Child.AdjacencyObject.IDsOrder[Cnt] = Parent2Adjacency.IDsOrder[Cnt];//in case Child.AdjacencyObject.IDsOrder[Cnt]=-1
                        if (Child.IncrementalID <= Child.AdjacencyObject.IDsOrder[Cnt]) Child.IncrementalID = Child.AdjacencyObject.IDsOrder[Cnt] + 1;
                    }
                }
                Child.AllLayers[Child.AdjacencyObject.IDsOrder[Cnt]].Clear();//Will be connected afterwards

            }

            for (int CntVert = 0; CntVert < Child.AdjacencyObject.IDsOrder.Count; CntVert++)//Connect the layers
                for (int CntHor = 0; CntHor < Child.AdjacencyObject.IDsOrder.Count; CntHor++)
                    if (Child.AdjacencyObject.AdjacencyMatrix[CntVert, CntHor] == 1)
                        Child.AddConnection(Child.AdjacencyObject.IDsOrder[CntVert], Child.AdjacencyObject.IDsOrder[CntHor]);

            Child.AdjacencyObject.EvalOrder();

            int[] EdgeToRemove = Child.AdjacencyObject.RemoveCycles();
            while (EdgeToRemove[0] != -1)
            {
                Child.RemoveConnection(EdgeToRemove[0], EdgeToRemove[1]);
                EdgeToRemove = Child.AdjacencyObject.RemoveCycles();
            }

            return Child;
        }

        //Privates
        private void AddLayer(GenLayer LayerToAdd)
        {
            ActionHistory.Add("Add Layer With ID : " + IncrementalID);
            AdjacencyObject.AddNode(IncrementalID, false, false);
            AllLayers.Add(IncrementalID, LayerToAdd);
            IncrementalID++;
            ActionHistory.Add("Layer Added ");
        }
        private bool RemoveLayer(int iID)
        {
            ActionHistory.Add("Remove Layer With ID : " + iID);
            if (AllLayers[iID].GetIsOutLayer() || AllLayers[iID].GetIsInLayer()) return false;
            List<int> ToRemoveList = AllLayers[iID].GetInList();
            for (int Cnt = ToRemoveList.Count - 1; Cnt >= 0; Cnt--) RemoveConnection(ToRemoveList[Cnt], iID);
            ToRemoveList = AllLayers[iID].GetOutList();
            for (int Cnt = ToRemoveList.Count - 1; Cnt >= 0; Cnt--) RemoveConnection(iID, ToRemoveList[Cnt]);
            AdjacencyObject.RemoveNode(iID);
            AllLayers.Remove(iID);
            ActionHistory.Add("Layer Removed");
            return true;
        }
        private bool AddConnection(int FromID, int ToID)
        {
            ActionHistory.Add("Add Connection ( " + FromID + ", " + ToID + ")");
            if (FromID == 1 || ToID == 0) return false;// These generate cycles
            if (AllLayers[FromID].GetIsOutLayer() || AllLayers[ToID].GetIsInLayer()) return false;
            if (FromID == ToID || AllLayers[FromID].GetIsOutLayer() || AllLayers[FromID].GetOutList().Contains(ToID)) return false;
            AdjacencyObject.ConnectFromTo(FromID, ToID);
            AllLayers[FromID].ConnectToOutput(ToID);
            AllLayers[ToID].ConnectToInput(FromID, AllLayers[FromID].GetOutput().Length);
            ActionHistory.Add("Connection Added");
            return true;
        }
        private bool RemoveConnection(int FromID, int ToID)
        {
            ActionHistory.Add("Remove Connection ( " + FromID + ", " + ToID + ")");
            if (!AdjacencyObject.RemoveFromTo(FromID, ToID)) return false;
            int Start = 0;
            foreach (int CurrLayerID in AllLayers[ToID].GetInList())
            {
                if (CurrLayerID == AllLayers[FromID].GetID()) break;
                Start += AllLayers[CurrLayerID].GetOutput().Length;
            }
            AllLayers[ToID].DisconnectFromInput(FromID, Start, AllLayers[FromID].GetOutput().Length);
            AllLayers[FromID].DisconnectFromOutput(ToID);
            ActionHistory.Add("Connection Removed");
            return true;
        }
        private int[] GetMutations(float MutationStrength, float[] Costs, Random Rnd)
        {
            // Modify these to change the mixture of mutations

            int[] ToReturn = new int[4];// [LayersToModify, FunctionsToModify, NeuronsToModify, ConnectionsToModify]

            ToReturn[0] = (int)Math.Floor(Rnd.NextDouble() * MutationStrength / Costs[0]);
            MutationStrength -= ToReturn[0] * Costs[0];

            ToReturn[1] = (int)Math.Floor(Rnd.NextDouble() * MutationStrength / Costs[1]);
            MutationStrength -= ToReturn[1] * Costs[1];

            ToReturn[2] = (int)Math.Floor(Rnd.NextDouble() * MutationStrength / Costs[2]);
            MutationStrength -= ToReturn[2] * Costs[2];

            ToReturn[3] = (int)Math.Floor(Rnd.NextDouble() * MutationStrength / Costs[3]);
            MutationStrength -= ToReturn[3] * Costs[3];

            return ToReturn;
        }
        private float[] ConstructInputs(List<int> InIDs)
        {
            int TotalLength = 0;
            for (int Cnt = 0; Cnt < InIDs.Count; Cnt++)
                TotalLength += AllLayers[InIDs[Cnt]].GetOutput().Length;

            float[] ToReturn = new float[TotalLength];
            TotalLength = 0;
            for (int Cnt = 0; Cnt < InIDs.Count; Cnt++)
            {
                Array.Copy(AllLayers[InIDs[Cnt]].GetOutput(), 0, ToReturn, TotalLength, AllLayers[InIDs[Cnt]].GetOutput().Length);
                TotalLength += AllLayers[InIDs[Cnt]].GetOutput().Length;
            }

            return ToReturn;
        }
        private int CountNeurons()
        {
            int TotalNeurons = 0;
            foreach (GenLayer CurrLayer in AllLayers.Values)
                TotalNeurons += CurrLayer.GetOutput().Length;
            return TotalNeurons;
        }
        private float Get1X2Score(float In, float Prediction, float Expected, float WinThresh)
        {
            if (Prediction < WinThresh) return 0;// Prediction is not taken into consideration
            else if (Expected == 1 && Prediction > WinThresh)
                return (1 / In) - 1;//We Won the bet. we get the odd value
            else
                return -1;//We lost the bet
        }

        // Comparers
        public int CompareTo(GenNetwork Other)
        {
            return -Score.CompareTo(Other.Score) * 100;
        }
        //Import export
        public void ExportNet(string NetFileName)
        {
            string ToTheFile = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + NetFileName + ".gnet";
            using (FileStream MyFile = File.Create(ToTheFile))
            {
                BinaryFormatter BF = new BinaryFormatter();

                BF.Serialize(MyFile, this);
                MyFile.Close();
            }
        }
        public static GenNetwork ImportNet(string NetFileName)
        {
            using (FileStream MyFile = File.OpenRead(NetFileName))
            {
                BinaryFormatter BF = new BinaryFormatter();
                GenNetwork NetworkRead = BF.Deserialize(MyFile) as GenNetwork;

                MyFile.Close();
                return NetworkRead;
            }

        }
    }
}
