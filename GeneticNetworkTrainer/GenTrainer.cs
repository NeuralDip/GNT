using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Linq;

namespace GeneticNetworkTrainer
{
    [Serializable]
    public partial class GenTrainer
    {
        public enum ScoreRules
        {
            RuleOutError,
            Rule1X2
        }
        public enum TrainingState
        {
            StructGenEnded,
            StructIslandEnded,
            StructStarted,
            StructEnded,
            InternalGenEnded,
            InternalIslandEnded,
            NetEnded,
            TrainingStopped,
            TrainingEnded,
            StructIslandsHalve,
            InternalIsladsHalve
        }

        [Serializable]
        public class StateClass
        {
            public bool IncludeNetsInTheSave = false;
            // Main Control
            public string DataFileName;
            public bool DataLoadedCorrectly = false;
            public List<float[]> InData;
            public List<float[]> LabelData;
            public int DataToUse = 0;
            public int NetInputs = 1;
            public int NetOutputs = 1;
            public bool HalfDataForTesting = false;

            public int TotalInternalPopulation = 100;
            public int InternalPopulationPerIsland = 100;
            public int InternalGenerations = 20;
            public int TotalStructurePopulation = 20;
            public int StructurePopulationPerIsland = 20;
            public int StructureGenerations = 10;
            public int CurrStructureGeneration = 0;

            public bool StopOnTesting = false;
            public int TestingToStopOn = 5;
            public bool StopOnScore = false;
            public float ScoreToStopOn = 1.0f;
            public bool StopOnTime = false;
            public TimeSpan TimeToStopOn = new TimeSpan(0, 10, 0);

            public bool ThreadingActivated = false;
            public int MaxThreadsinParallel = 1;
            public bool DynamicSearchMaxThreads = false;

            //Structure Genetics
            public float StructureCrossover = 0.2f;
            public float StructureMutation = 0.4f;
            public float StructureMutationStrength = 0.5f;
            public float LayerCost = 0.4f;
            public float FunctionCost = 0.2f;
            public float NeuronCost = 0.1f;
            public float ConnectionCost = 0.05f;
            public bool WeightsRandomized = false;
            public bool BiasesRandomized = false;
            public float StructureCopy = 0.4f;

            public bool StructIslandRestructuringNeeded = false;
            public int InitialNumberStructureIslands = 1;
            public int CurrNumberStructureIslands = 1;
            public bool HalveStructureIslands = false;
            public int StructureIslandsHalvingSteps = 1;
            public int StructureIslandsHalveIn = 1;

            public bool ScorePenaltyNeurons = false;
            public int ScorePenaltyNumberOfNeurons = 3;
            public bool ScorePenaltyLayers = false;
            public int ScorePenaltyNumberOfLayers = 3;
            public bool ScorePenaltyConnections = false;
            public int ScorePenaltyNumberOfConnections = 3;

            //Internal Genetics
            public float InternalCrossover = 0.2f;
            public float InternalMutation = 0.4f;
            public float InternalMutationStrength = 0.5f;
            public float WeightsLimit = 4.0f;
            public float BiasesLimit = 1.0f;
            public bool MutateWeights = true;
            public bool MutateBiases = true;
            public float InternalCopy = 0.4f;

            public float InitialInternalAnnealing = 0.0f;
            public bool ReduceAnnealing = false;
            public int AnnealingReducingSteps = 1;
            public int InternalAnnealingReduceIn = 1;

            public bool InternalIslandRestructuringNeeded = false;
            public int InitialNumberInternalIslands = 1;
            public bool HalveInternalIslands = false;
            public int InternalIslandsHalvingSteps = 1;
            public int InternalIslandsHalveIn = 1;

            public ScoreRules ScoreRule = ScoreRules.RuleOutError;
            public float ThresholdOfWin = 0.5f;
            public float ThresholdOfValid = 0.0f;

            //Net Structure That will be saved
            public List<List<List<GenNetwork[]>>> NetsStructureToSave; //Islands[Structures[Islands[Nets]]]
        }
        [Serializable]
        public class CircularBuffer
        {
            private float[] Buffer;
            private int DataPointer = 0;
            private bool Full = false;
            public CircularBuffer(int Dimension) { Buffer = new float[Dimension]; }
            public int Size() { return Buffer.Length; }
            public void PutValue(float NewValue) { Buffer[DataPointer] = NewValue; AdvancePointer(); }
            public float ReadLastValue() { int ReadPointer = DataPointer - 1; if (ReadPointer == -1) { ReadPointer = Buffer.Length - 1; } return Buffer[ReadPointer]; }
            public float[] GetData()
            {
                float[] ToReturn = null;
                if (Full)
                {
                    ToReturn = new float[Buffer.Length];
                    Array.Copy(Buffer, DataPointer, ToReturn, 0, Buffer.Length - DataPointer);
                    Array.Copy(Buffer, 0, ToReturn, Buffer.Length - DataPointer, DataPointer);
                }
                else
                {
                    ToReturn = new float[DataPointer];
                    Array.Copy(Buffer, 0, ToReturn, 0, DataPointer);
                }
                return ToReturn;
            }
            public void Clear() { Buffer = new float[Buffer.Length]; DataPointer = 0; Full = false; }
            public CircularBuffer CloneMe()
            {
                CircularBuffer ToReturn = new CircularBuffer(Buffer.Length);
                Array.Copy(Buffer, 0, ToReturn.Buffer, 0, Buffer.Length);
                ToReturn.DataPointer = DataPointer;
                ToReturn.Full = Full;
                return ToReturn;
            }
            public bool Maximize(CircularBuffer Other)
            { //Take the max Values from both Buffers
                if (Buffer.Length != Other.Buffer.Length) return false;

                for (int Cnt = 0; Cnt < Buffer.Length; Cnt++)
                    if (Buffer[Cnt] < Other.Buffer[Cnt]) Buffer[Cnt] = Other.Buffer[Cnt];

                return true;
            }
            public bool Minimize(CircularBuffer Other)
            { //Take the min Values from both Buffers
                if (Buffer.Length != Other.Buffer.Length) return false;

                for (int Cnt = 0; Cnt > Buffer.Length; Cnt++)
                    if (Buffer[Cnt] < Other.Buffer[Cnt]) Buffer[Cnt] = Other.Buffer[Cnt];

                return true;
            }
            private void AdvancePointer() { DataPointer++; if (DataPointer == Buffer.Length) { Full = true; DataPointer = 0; } }
        }
        [Serializable]
        public class StatsStructureClass
        {
            public class StructIslandStatsClass
            {
                public class StructStatsClass
                {
                    public class InternalIslandStatsClass
                    {
                        public class NetStatsClass
                        {
                            public CircularBuffer ScoreHistory;
                            public CircularBuffer TestScoreHistory;
                            public CircularBuffer OutErrorHistory;
                            public CircularBuffer TestOutErrorHistory;
                            public NetStatsClass(int InternalGenerations)
                            {
                                ScoreHistory = new CircularBuffer(InternalGenerations);
                                TestScoreHistory = new CircularBuffer(InternalGenerations);
                                OutErrorHistory = new CircularBuffer(InternalGenerations);
                                TestOutErrorHistory = new CircularBuffer(InternalGenerations);
                            }
                        }
                        public CircularBuffer ScoreHistory;
                        public CircularBuffer TestScoreHistory;
                        public float[] ScoreHistogramData;
                        public float[] TestScoreHistogramData;
                        public float[] ScoreHistogram;
                        public float[] TestScoreHistogram;
                        public NetStatsClass[] NetStats;
                        public InternalIslandStatsClass(int NetPopulationPerIsland, int InternalGenerations)
                        {
                            ScoreHistory = new CircularBuffer(InternalGenerations);
                            TestScoreHistory = new CircularBuffer(InternalGenerations);
                            NetStats = new NetStatsClass[NetPopulationPerIsland];
                            ScoreHistogramData = new float[NetPopulationPerIsland];
                            TestScoreHistogramData = new float[NetPopulationPerIsland];
                            for (int Cnt = 0; Cnt < NetPopulationPerIsland; Cnt++) NetStats[Cnt] = new NetStatsClass(InternalGenerations);
                        }
                    }
                    public CircularBuffer ScoreHistory;
                    public CircularBuffer TestScoreHistory;
                    public CircularBuffer LayersHistory;
                    public CircularBuffer NeuronsHistory;
                    public int BestIsland;
                    public List<InternalIslandStatsClass> InternalIslandsStats = new List<InternalIslandStatsClass>();
                    public StructStatsClass(int StructPopulationPerIsland, int InternalIslandsNumber, int NetPopulationPerIsland, int InternalGenerations)
                    {
                        ScoreHistory = new CircularBuffer(StructPopulationPerIsland);
                        TestScoreHistory = new CircularBuffer(StructPopulationPerIsland);
                        LayersHistory = new CircularBuffer(StructPopulationPerIsland);
                        NeuronsHistory = new CircularBuffer(StructPopulationPerIsland);
                        for (int Cnt = 0; Cnt < InternalIslandsNumber; Cnt++) InternalIslandsStats.Add(new InternalIslandStatsClass(NetPopulationPerIsland, InternalGenerations));
                    }
                }
                public CircularBuffer ScoreHistory;
                public CircularBuffer TestScoreHistory;
                public CircularBuffer LayersHistory;
                public CircularBuffer NeuronsHistory;
                public float[] ScoreHistogramData;
                public float[] TestScoreHistogramData;
                public float[] LayersHistogramData;
                public float[] NeuronsHistogramData;
                public float[] ScoreHistogram;
                public float[] TestScoreHistogram;
                public float[] LayersHistogram;
                public float[] NeuronsHistogram;
                public int BestStructure;
                public List<StructStatsClass> StructStats = new List<StructStatsClass>();
                public StructIslandStatsClass(int StructPopulationPerIsland, int InternalIslandsNumber, int NetPopulationPerIsland, int StructGenerations, int InternalGenerations)
                {
                    ScoreHistory = new CircularBuffer(StructGenerations);
                    TestScoreHistory = new CircularBuffer(StructGenerations);
                    LayersHistory = new CircularBuffer(StructGenerations);
                    NeuronsHistory = new CircularBuffer(StructGenerations);
                    ScoreHistogramData = new float[StructPopulationPerIsland];
                    TestScoreHistogramData = new float[StructPopulationPerIsland];
                    LayersHistogramData = new float[StructPopulationPerIsland];
                    NeuronsHistogramData = new float[StructPopulationPerIsland];
                    for (int Cnt = 0; Cnt < StructPopulationPerIsland; Cnt++) StructStats.Add(new StructStatsClass(StructPopulationPerIsland, InternalIslandsNumber, NetPopulationPerIsland, InternalGenerations));
                }
            }
            public int BestIsland;
            public List<StructIslandStatsClass> StructIslandsStats = new List<StructIslandStatsClass>();
            public StatsStructureClass(int StructIslands, int StructPopulation, int InternalIslandsNumber, int NetPopulation, int StructGenerations, int InternalGenerations)
            { for (int Cnt = 0; Cnt < StructIslands; Cnt++) StructIslandsStats.Add(new StructIslandStatsClass(StructPopulation, InternalIslandsNumber, NetPopulation, StructGenerations, InternalGenerations)); }
        }


        // interaction with environment
        public Action<string, int> ParentFormLogging;// string: Message, int: 0=info, 1=Warning, 2=Error
        public Action<string, string, object> ParentFormControlSet;// string: ControlName, string: ProperyName, object: NewValue
        public Func<string, string, object> ParentFormControlGet;// string: ControlName, string: ProperyName, object: NewValue

        public delegate void SomethingHappenedDelegate(TrainingState WhatHappened);
        public event SomethingHappenedDelegate CallTheForm;

        public StateClass MyState;
        //Ststs and net Structures that are not saved
        public List<List<List<GenNetwork[]>>> DevelopingNetsStructure; //Islands[Structures[Islands[Nets]]]
        public List<List<List<GenNetwork[]>>> SettledNetsStructure; //Islands[Structures[Islands[Nets]]]

        public StatsStructureClass SettledStatsStructure;
        public StatsStructureClass DevelopingStatsStructure;

        public int HistogramsBins;
        public bool StateFileExists = false;
        private bool ForceStopTraining = false;
        private bool StopTraining = false;
        private bool BoolSaveState = false;
        [ThreadStatic]
        public static Random Rnd = new Random();
        private Stopwatch MyGlobalWatch = new Stopwatch();
        private int TestScoreDecreasedFor = 0;
        private float PrevTestScore = float.MinValue;

        // the first two keep the value on the form as "Initial". they are Internal 
        // These are arrays for the multithreaded case
        private float[] CurrAnnealing;
        private int[] CurrInternalIslands;
        private int[] CurrInternalPopulationPerIsland;
        // The struct Island will update the form with the new variable instead.Its variable is in the MyState and has to be saved
        //private int CurrStructIslands = 0;

        public GenTrainer(Action<string, int> iLoggingFunction, Action<string, string, object> iParentFormControlSet, Func<string, string, object> iParentFormControlGet)
        {
            MyState = new StateClass();
            ParentFormLogging = iLoggingFunction;
            ParentFormControlSet = iParentFormControlSet;
            ParentFormControlGet = iParentFormControlGet;
            SettledNetsStructure = new List<List<List<GenNetwork[]>>>(); //Islands[Structures[Islands[Nets]]]
            DevelopingNetsStructure = new List<List<List<GenNetwork[]>>>(); //Islands[Structures[Islands[Nets]]]
            SettledStatsStructure = new StatsStructureClass(MyState.InitialNumberStructureIslands, MyState.StructurePopulationPerIsland, MyState.InitialNumberInternalIslands, MyState.InternalPopulationPerIsland, MyState.StructureGenerations, MyState.InternalGenerations);
            DevelopingStatsStructure = new StatsStructureClass(MyState.InitialNumberStructureIslands, MyState.StructurePopulationPerIsland, MyState.InitialNumberInternalIslands, MyState.InternalPopulationPerIsland, MyState.StructureGenerations, MyState.InternalGenerations);
        }

        public void ResetStructures(bool Complete, bool StatsOnly)
        {
            if (!StatsOnly)
            {
                if (Complete) SettledNetsStructure = new List<List<List<GenNetwork[]>>>(); //Islands[Structures[Islands[Nets]]]
                DevelopingNetsStructure = new List<List<List<GenNetwork[]>>>(); //Islands[Structures[Islands[Nets]]]

                for (int SICnt = 0; SICnt < MyState.InitialNumberStructureIslands; SICnt++)
                {
                    if (Complete) SettledNetsStructure.Add(new List<List<GenNetwork[]>>());
                    DevelopingNetsStructure.Add(new List<List<GenNetwork[]>>());
                    for (int SPCnt = 0; SPCnt < MyState.StructurePopulationPerIsland; SPCnt++)
                    {
                        if (Complete) SettledNetsStructure[SICnt].Add(new List<GenNetwork[]>());
                        DevelopingNetsStructure[SICnt].Add(new List<GenNetwork[]>());
                        for (int IICnt = 0; IICnt < MyState.InitialNumberInternalIslands; IICnt++)
                        {
                            if (Complete)
                            {
                                SettledNetsStructure[SICnt][SPCnt].Add(new GenNetwork[MyState.InternalPopulationPerIsland]);
                                for (int IPCnt = 0; IPCnt < MyState.InternalPopulationPerIsland; IPCnt++) SettledNetsStructure[SICnt][SPCnt][IICnt][IPCnt] = new GenNetwork(MyState.NetInputs, 1, MyState.NetOutputs, new int[] { });
                            }
                            DevelopingNetsStructure[SICnt][SPCnt].Add(new GenNetwork[MyState.InternalPopulationPerIsland]);
                            for (int IPCnt = 0; IPCnt < MyState.InternalPopulationPerIsland; IPCnt++) DevelopingNetsStructure[SICnt][SPCnt][IICnt][IPCnt] = new GenNetwork(MyState.NetInputs, 1, MyState.NetOutputs, new int[] { });
                        }
                    }
                }
            }
            if (Complete) SettledStatsStructure = new StatsStructureClass(MyState.InitialNumberStructureIslands, MyState.StructurePopulationPerIsland, MyState.InitialNumberInternalIslands, MyState.InternalPopulationPerIsland, MyState.StructureGenerations, MyState.InternalGenerations);
            DevelopingStatsStructure = new StatsStructureClass(MyState.InitialNumberStructureIslands, MyState.StructurePopulationPerIsland, MyState.InitialNumberInternalIslands, MyState.InternalPopulationPerIsland, MyState.StructureGenerations, MyState.InternalGenerations);

        }

        public void TrainNetNotThreaded(object iInput)
        {
            try
            {
                PreProcess(true);
                for (int StrGenCnt = MyState.CurrStructureGeneration; StrGenCnt < MyState.StructureGenerations; StrGenCnt++)
                {
                    MyState.CurrStructureGeneration = StrGenCnt;
                    ParentFormControlSet("LabelCurrStructGen", "Text", StrGenCnt.ToString());
                    if (BoolSaveState) ForceSaveState();
                    if (StopTraining) { CallTheForm(TrainingState.TrainingStopped); StopTraining = false; return; }

                    CheckIslandsHalving(false, 0, 0, StrGenCnt, 0);

                    for (int SICnt = 0; SICnt < MyState.CurrNumberStructureIslands; SICnt++)
                    {
                        NextStructGeneration(SICnt, 0);// prepare generation for this Structure Island
                        for (int SPCnt = 0; SPCnt < MyState.StructurePopulationPerIsland; SPCnt++)
                        {
                            PopulateStatsStructure(TrainingState.StructStarted, SICnt, SPCnt, 0, 0, 0);
                            ParentFormControlSet("LabelCurrStructure", "Text", string.Format("({0}, {1})", SICnt, SPCnt));
                            for (int IntGenCnt = 0; IntGenCnt < MyState.InternalGenerations; IntGenCnt++)
                            {
                                CheckAnnealingUpdate(IntGenCnt, 0);
                                CheckIslandsHalving(true, SICnt, SPCnt, IntGenCnt, 0);
                                ParentFormControlSet("LabelCurrInternalGen", "Text", IntGenCnt.ToString());
                                for (int IICnt = 0; IICnt < CurrInternalIslands[0]; IICnt++)
                                {

                                    NextInternalGeneration(SICnt, SPCnt, IICnt, 0);// prepare generation for this Internal Island
                                    for (int IPCnt = 0; IPCnt < CurrInternalPopulationPerIsland[0]; IPCnt++)
                                    {
                                        DevelopingNetsStructure[SICnt][SPCnt][IICnt][IPCnt].ResetScores();
                                        if (!DevelopingNetsStructure[SICnt][SPCnt][IICnt][IPCnt].CalculateScores(MyState.InData, MyState.LabelData, MyState.DataToUse, MyState.HalfDataForTesting, MyState.ScoreRule, MyState.ThresholdOfWin, MyState.ThresholdOfValid))
                                        {
                                            ParentFormLogging(string.Format("Scoring Calculation failed for net ({0}{1}{2}{3}). Inputs or Outputs dont match the nets IOs. ", SICnt, SPCnt, IICnt, IPCnt), 2);
                                            return;
                                        }
                                        PopulateStatsStructure(TrainingState.NetEnded, SICnt, SPCnt, IICnt, IPCnt, 0);
                                        if (ForceStopTraining) { CallTheForm(TrainingState.TrainingStopped); StopTraining = false; ForceStopTraining = false; return; }
                                    }
                                    PopulateStatsStructure(TrainingState.InternalIslandEnded, SICnt, SPCnt, IICnt, 0, 0);
                                }
                                PopulateStatsStructure(TrainingState.InternalGenEnded, SICnt, SPCnt, 0, 0, 0);
                            }
                            CurrInternalIslands[0] = MyState.InitialNumberInternalIslands;
                            CurrAnnealing[0] = MyState.InitialInternalAnnealing;
                            PopulateStatsStructure(TrainingState.StructEnded, SICnt, SPCnt, 0, 0, 0);
                        }
                        PopulateStatsStructure(TrainingState.StructIslandEnded, SICnt, 0, 0, 0, 0);
                    }
                    PopulateStatsStructure(TrainingState.StructGenEnded, 0, 0, 0, 0, 0);
                    PostProcess(0);
                    SettledNetsStructure = CloneNetsStruct(DevelopingNetsStructure);
                    SettledStatsStructure = CloneStatsStruct(DevelopingStatsStructure);
                    CheckStopConditions();
                    CallTheForm(TrainingState.StructGenEnded);
                }
            }
            catch (Exception Ex)
            {
                ParentFormLogging(" Error While training. Message: " + Ex.Message + "\n Trace: " + new StackTrace(Ex, true).ToString(), 2);
            }
            CallTheForm(TrainingState.TrainingEnded);
        }

        private void NextStructGeneration(int SICnt, int ThreadID)
        {
            List<List<GenNetwork[]>> NewGeneration = new List<List<GenNetwork[]>>();
            int PopulationFromCrossover = (int)Math.Floor(MyState.StructureCrossover * MyState.StructurePopulationPerIsland);
            int PopulationFromMutation = (int)Math.Floor(MyState.StructureMutation * MyState.StructurePopulationPerIsland);
            int PopulationFromCopy = MyState.StructurePopulationPerIsland - PopulationFromCrossover - PopulationFromMutation;

            // Crossovering...
            // Solve this quadratic equation to Find the Number of Parents needed : (x^2-x)/2 = PopulationFromCrossover
            // Then Ceil it to have an integer
            int IndependentParents = (int)Math.Ceiling(0.5f + Math.Sqrt(0.25f + 2 * PopulationFromCrossover));

            int Parent1ID = 0;
            int Parent2ID = 1;
            int ChildIdx = 0;
            for (; ChildIdx < PopulationFromCrossover; ChildIdx++)
            {
                GenNetwork NewNetwork = DevelopingNetsStructure[SICnt][Parent1ID][0][0].CrossoverStruct(DevelopingNetsStructure[SICnt][Parent2ID][0][0], Rnd);
                NewGeneration.Add(new List<GenNetwork[]>());
                for (int IICnt = 0; IICnt < CurrInternalIslands[ThreadID]; IICnt++)// Repaet the new structure for all internal Islands and populations underneath
                {
                    NewGeneration[ChildIdx].Add(new GenNetwork[CurrInternalPopulationPerIsland[ThreadID]]);
                    for (int IPCnt = 0; IPCnt < CurrInternalPopulationPerIsland[ThreadID]; IPCnt++)
                    {
                        //DevelopingStatsStructure.StructIslandsStats[ChildIdx].StructStats[IICnt].InternalIslandsStats[IPCnt].NetStats.
                        NewGeneration[ChildIdx][IICnt][IPCnt] = NewNetwork.CloneMe(true, MyState.WeightsRandomized, MyState.BiasesRandomized, Rnd);
                    }
                }

                Parent2ID++;
                if (Parent2ID == IndependentParents)
                {
                    Parent1ID++;
                    Parent2ID = Parent1ID + 1;
                }
            }

            // Mutating
            for (int OriginalChildIdx = 0; ChildIdx < PopulationFromCrossover + PopulationFromMutation; ChildIdx++, OriginalChildIdx++)
            {
                GenNetwork NewNetwork = DevelopingNetsStructure[SICnt][OriginalChildIdx][0][0].MutateStruct(MyState.StructureMutationStrength, new float[] { MyState.LayerCost, MyState.FunctionCost, MyState.NeuronCost, MyState.ConnectionCost }, new bool[] { MyState.ScorePenaltyLayers, MyState.ScorePenaltyNeurons, MyState.ScorePenaltyConnections }, new int[] { MyState.ScorePenaltyNumberOfLayers, MyState.ScorePenaltyNumberOfNeurons, MyState.ScorePenaltyNumberOfConnections }, Rnd);
                NewGeneration.Add(new List<GenNetwork[]>());
                for (int IICnt = 0; IICnt < CurrInternalIslands[ThreadID]; IICnt++)// Repaet the new structure for all internal Islands and populations underneath
                {
                    NewGeneration[ChildIdx].Add(new GenNetwork[CurrInternalPopulationPerIsland[ThreadID]]);
                    for (int IPCnt = 0; IPCnt < CurrInternalPopulationPerIsland[ThreadID]; IPCnt++)
                        NewGeneration[ChildIdx][IICnt][IPCnt] = NewNetwork.CloneMe(true, MyState.WeightsRandomized, MyState.BiasesRandomized, Rnd);
                }
            }
            // Copying
            for (int OriginalChildIdx = 0; ChildIdx < MyState.StructurePopulationPerIsland; ChildIdx++, OriginalChildIdx++)
            {
                NewGeneration.Add(new List<GenNetwork[]>());
                for (int IICnt = 0; IICnt < CurrInternalIslands[ThreadID]; IICnt++)// Repaet the new structure for all internal Islands and populations underneath
                {
                    NewGeneration[ChildIdx].Add(new GenNetwork[CurrInternalPopulationPerIsland[ThreadID]]);
                    for (int IPCnt = 0; IPCnt < CurrInternalPopulationPerIsland[ThreadID]; IPCnt++)
                        NewGeneration[ChildIdx][IICnt][IPCnt] = DevelopingNetsStructure[SICnt][OriginalChildIdx][0][0].CloneMe(true, MyState.WeightsRandomized, MyState.BiasesRandomized, Rnd);
                }
            }

            DevelopingNetsStructure[SICnt] = NewGeneration;
        }
        private void NextInternalGeneration(int SICnt, int SPCnt, int IICnt, int ThreadID)
        {
            GenNetwork[] NewGeneration = new GenNetwork[CurrInternalPopulationPerIsland[ThreadID]];
            StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass.NetStatsClass[] NewStats = new StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass.NetStatsClass[CurrInternalPopulationPerIsland[ThreadID]];
            int PopulationFromCrossover = (int)Math.Floor(MyState.InternalCrossover * CurrInternalPopulationPerIsland[ThreadID]);
            int PopulationFromMutation = (int)Math.Floor(MyState.InternalMutation * CurrInternalPopulationPerIsland[ThreadID]);
            int PopulationFromCopy = CurrInternalPopulationPerIsland[ThreadID] - PopulationFromCrossover - PopulationFromMutation;

            // Crossovering...
            // Solve this quadratic equation to Find the Number of Parents needed : (x^2-x)/2 = PopulationFromCrossover
            // Then Ceil it to have an integer
            int IndependentParents = (int)Math.Ceiling(0.5f + Math.Sqrt(0.25f + 2 * PopulationFromCrossover));

            int Parent1ID = 0;
            int Parent2ID = 1;
            int ChildIdx = 0;
            for (; ChildIdx < PopulationFromCrossover; ChildIdx++)
            {
                NewStats[ChildIdx] = new StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass.NetStatsClass(MyState.InternalGenerations);
                NewStats[ChildIdx].OutErrorHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[Parent1ID].OutErrorHistory.CloneMe();
                NewStats[ChildIdx].ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[Parent1ID].ScoreHistory.CloneMe();
                NewStats[ChildIdx].TestOutErrorHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[Parent1ID].TestOutErrorHistory.CloneMe();
                NewStats[ChildIdx].TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[Parent1ID].TestScoreHistory.CloneMe();

                NewGeneration[ChildIdx] = DevelopingNetsStructure[SICnt][SPCnt][IICnt][Parent1ID].CrossoverInternal(DevelopingNetsStructure[SICnt][SPCnt][IICnt][Parent2ID], MyState.MutateWeights, MyState.MutateBiases, CurrAnnealing[ThreadID], Rnd);

                Parent2ID++;
                if (Parent2ID == IndependentParents)
                {
                    Parent1ID++;
                    Parent2ID = Parent1ID + 1;
                }
            }

            // Mutating
            for (int OriginalChildIdx = 0; ChildIdx < PopulationFromCrossover + PopulationFromMutation; ChildIdx++, OriginalChildIdx++)
            {
                NewStats[ChildIdx] = new StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass.NetStatsClass(MyState.InternalGenerations);
                NewStats[ChildIdx].OutErrorHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].OutErrorHistory.CloneMe();
                NewStats[ChildIdx].ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].ScoreHistory.CloneMe();
                NewStats[ChildIdx].TestOutErrorHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].TestOutErrorHistory.CloneMe();
                NewStats[ChildIdx].TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].TestScoreHistory.CloneMe();
                NewGeneration[ChildIdx] = DevelopingNetsStructure[SICnt][SPCnt][IICnt][OriginalChildIdx].MutateInternal(MyState.MutateWeights, MyState.MutateBiases, MyState.InternalMutationStrength, CurrAnnealing[ThreadID], Rnd);
            }
            // Copying
            for (int OriginalChildIdx = 0; ChildIdx < CurrInternalPopulationPerIsland[ThreadID]; ChildIdx++, OriginalChildIdx++)
            {
                NewStats[ChildIdx] = new StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass.NetStatsClass(MyState.InternalGenerations);
                NewStats[ChildIdx].OutErrorHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].OutErrorHistory.CloneMe();
                NewStats[ChildIdx].ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].ScoreHistory.CloneMe();
                NewStats[ChildIdx].TestOutErrorHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].TestOutErrorHistory.CloneMe();
                NewStats[ChildIdx].TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[OriginalChildIdx].TestScoreHistory.CloneMe();
                NewGeneration[ChildIdx] = DevelopingNetsStructure[SICnt][SPCnt][IICnt][OriginalChildIdx].CloneMe(false, false, false, null);
            }

            DevelopingNetsStructure[SICnt][SPCnt][IICnt] = NewGeneration;
            DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats = NewStats;
        }
        private void PopulateStatsStructure(TrainingState CurrLevel, int SICnt, int SPCnt, int IICnt, int IPCnt, int ThreadID)
        {
            try
            {

                switch (CurrLevel)
                {
                    case TrainingState.NetEnded:
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].ScoreHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][IICnt][IPCnt].GetScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].TestScoreHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][IICnt][IPCnt].GetTestScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].OutErrorHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][IICnt][IPCnt].GetOutError());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].TestOutErrorHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][IICnt][IPCnt].GetTestOutError());

                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogramData[IPCnt] = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].ScoreHistory.ReadLastValue();
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogramData[IPCnt] = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].TestScoreHistory.ReadLastValue();

                        break;
                    case TrainingState.InternalIslandEnded:
                        Array.Sort(DevelopingNetsStructure[SICnt][SPCnt][IICnt], DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats);

                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram = ListToHistogram(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogramData);
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogram = ListToHistogram(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogramData);

                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][IICnt][0].GetScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][IICnt][0].GetTestScore());

                        float IIslandCurrScore = int.MinValue;
                        for (int LocalCnt = 0; LocalCnt < CurrInternalIslands[ThreadID]; LocalCnt++)
                        {
                            if (IIslandCurrScore < DevelopingNetsStructure[SICnt][SPCnt][LocalCnt][0].GetScore())
                            {
                                DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].BestIsland = LocalCnt;
                                IIslandCurrScore = DevelopingNetsStructure[SICnt][SPCnt][LocalCnt][0].GetScore();
                            }
                        }
                        break;
                    case TrainingState.StructStarted:
                        for (int IICntInt = 0; IICntInt < DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats.Count; IICntInt++)// These loops are done on the unchanged limits, because islands have not yet been structured
                        {
                            for (int Cnt = 0; Cnt < DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICntInt].NetStats.Length; Cnt++)
                            {
                                DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICntInt].NetStats[Cnt].ScoreHistory.Clear();
                                DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICntInt].NetStats[Cnt].TestScoreHistory.Clear();
                                DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICntInt].NetStats[Cnt].OutErrorHistory.Clear();
                                DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICntInt].NetStats[Cnt].TestOutErrorHistory.Clear();

                            }
                        }
                        break;
                    case TrainingState.InternalGenEnded:
                        break;
                    case TrainingState.StructEnded:
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].ScoreHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].BestIsland][0].GetScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].TestScoreHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].BestIsland][0].GetTestScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].LayersHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][0][0].GetLayersNumber());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].NeuronsHistory.PutValue(DevelopingNetsStructure[SICnt][SPCnt][0][0].GetNeuronsNumber());

                        DevelopingStatsStructure.StructIslandsStats[SICnt].ScoreHistogramData[SPCnt] = (DevelopingNetsStructure[SICnt][SPCnt][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].BestIsland][0].GetScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].TestScoreHistogramData[SPCnt] = (DevelopingNetsStructure[SICnt][SPCnt][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].BestIsland][0].GetTestScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].LayersHistogramData[SPCnt] = (DevelopingNetsStructure[SICnt][SPCnt][0][0].GetLayersNumber());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].NeuronsHistogramData[SPCnt] = (DevelopingNetsStructure[SICnt][SPCnt][0][0].GetNeuronsNumber());
                        float StructCurrScore = int.MinValue;
                        for (int LocalCnt = 0; LocalCnt < MyState.StructurePopulationPerIsland; LocalCnt++)
                        {
                            if (StructCurrScore < DevelopingNetsStructure[SICnt][LocalCnt][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[LocalCnt].BestIsland][0].GetScore())
                            {
                                DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure = LocalCnt;
                                StructCurrScore = DevelopingNetsStructure[SICnt][LocalCnt][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[LocalCnt].BestIsland][0].GetScore();
                            }
                        }
                        break;
                    case TrainingState.StructIslandEnded:

                        DevelopingStatsStructure.StructIslandsStats[SICnt].ScoreHistory.PutValue(DevelopingNetsStructure[SICnt][DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure].BestIsland][0].GetScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].TestScoreHistory.PutValue(DevelopingNetsStructure[SICnt][DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure].BestIsland][0].GetTestScore());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].LayersHistory.PutValue(DevelopingNetsStructure[SICnt][DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure].BestIsland][0].GetLayersNumber());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].NeuronsHistory.PutValue(DevelopingNetsStructure[SICnt][DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure][DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[DevelopingStatsStructure.StructIslandsStats[SICnt].BestStructure].BestIsland][0].GetNeuronsNumber());
                        DevelopingStatsStructure.StructIslandsStats[SICnt].ScoreHistogram = ListToHistogram(DevelopingStatsStructure.StructIslandsStats[SICnt].ScoreHistogramData);
                        DevelopingStatsStructure.StructIslandsStats[SICnt].TestScoreHistogram = ListToHistogram(DevelopingStatsStructure.StructIslandsStats[SICnt].TestScoreHistogramData);
                        DevelopingStatsStructure.StructIslandsStats[SICnt].LayersHistogram = ListToHistogram(DevelopingStatsStructure.StructIslandsStats[SICnt].LayersHistogramData);
                        DevelopingStatsStructure.StructIslandsStats[SICnt].NeuronsHistogram = ListToHistogram(DevelopingStatsStructure.StructIslandsStats[SICnt].NeuronsHistogramData);
                        float SIslandCurrScore = int.MinValue;
                        for (int LocalCnt = 0; LocalCnt < MyState.CurrNumberStructureIslands; LocalCnt++)
                        {
                            if (SIslandCurrScore < DevelopingNetsStructure[LocalCnt][DevelopingStatsStructure.StructIslandsStats[LocalCnt].BestStructure][DevelopingStatsStructure.StructIslandsStats[LocalCnt].StructStats[DevelopingStatsStructure.StructIslandsStats[LocalCnt].BestStructure].BestIsland][0].GetScore())
                            {
                                DevelopingStatsStructure.BestIsland = LocalCnt;
                                SIslandCurrScore = DevelopingNetsStructure[LocalCnt][DevelopingStatsStructure.StructIslandsStats[LocalCnt].BestStructure][DevelopingStatsStructure.StructIslandsStats[LocalCnt].StructStats[DevelopingStatsStructure.StructIslandsStats[LocalCnt].BestStructure].BestIsland][0].GetScore();
                            }
                        }
                        break;
                    case TrainingState.StructGenEnded:
                        break;
                    default: ParentFormLogging(" Check Your Stats Populating... ", 2); break;

                }

            }
            catch (Exception Ex)
            {
                ParentFormLogging(" Error While training. Message: " + Ex.Message + "\n Trace: " + new StackTrace(Ex, true).ToString(), 2);
            }
        }
        private float[] ListToHistogram(float[] InList)
        {
            float[] ToReturn = new float[HistogramsBins * 2 + 1];
            float MaxValue = (float)Math.Ceiling(InList.Max());
            float MinValue = InList.Min();
            float BinWidth;

            if (MaxValue == (float)Math.Ceiling(MinValue)) BinWidth = 1;
            else BinWidth = (MaxValue - MinValue) / HistogramsBins;

            // Putting Bar Values
            for (int Cnt = 0; Cnt < InList.Length; Cnt++)
                ToReturn[(int)Math.Floor((InList[Cnt] - MinValue) / BinWidth)]++;

            // Putting Bar Labels
            for (int Cnt = 0; Cnt < HistogramsBins + 1; Cnt++) ToReturn[HistogramsBins + Cnt] = MinValue + Cnt * BinWidth;

            return ToReturn;
        }
        private void CheckStopConditions()
        {
            if (MyState.StopOnTesting)
            {
                if (SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].StructStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].BestStructure].InternalIslandsStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].StructStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].BestStructure].BestIsland].TestScoreHistory.ReadLastValue() < PrevTestScore)
                    TestScoreDecreasedFor++;
                else TestScoreDecreasedFor = 0;

                PrevTestScore = SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].StructStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].BestStructure].InternalIslandsStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].StructStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].BestStructure].BestIsland].TestScoreHistory.ReadLastValue();
                if (TestScoreDecreasedFor == MyState.TestingToStopOn)
                {
                    TestScoreDecreasedFor = 0;
                    ParentFormLogging(" Training Stopping because of Test Decrease Condition. ", 1);
                    Stop(false);
                    return;
                }
            }
            if (MyState.StopOnScore)
            {
                if (SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].StructStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].BestStructure].InternalIslandsStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].StructStats[SettledStatsStructure.StructIslandsStats[SettledStatsStructure.BestIsland].BestStructure].BestIsland].ScoreHistory.ReadLastValue() >= MyState.ScoreToStopOn)
                {
                    ParentFormLogging(" Training Stopping because of Score Condition. ", 1);
                    Stop(false);
                    return;
                }
            }
            if (MyState.StopOnTime)
            {
                if (MyGlobalWatch.Elapsed > MyState.TimeToStopOn)
                {
                    ParentFormLogging(" Training Stopping because of Time Condition. ", 1);
                    Stop(false);
                    return;
                }
            }
        }
        private List<List<List<GenNetwork[]>>> CloneNetsStruct(List<List<List<GenNetwork[]>>> Original)
        {
            List<List<List<GenNetwork[]>>> ToReturn = new List<List<List<GenNetwork[]>>>();

            for (int SICnt = 0; SICnt < MyState.CurrNumberStructureIslands; SICnt++)
            {
                ToReturn.Add(new List<List<GenNetwork[]>>());
                for (int SPCnt = 0; SPCnt < MyState.StructurePopulationPerIsland; SPCnt++)
                {
                    ToReturn[SICnt].Add(new List<GenNetwork[]>());
                    for (int IICnt = 0; IICnt < MyState.InitialNumberInternalIslands; IICnt++)
                    {
                        ToReturn[SICnt][SPCnt].Add(new GenNetwork[MyState.InternalPopulationPerIsland]);
                        for (int IPCnt = 0; IPCnt < MyState.InternalPopulationPerIsland; IPCnt++)
                            ToReturn[SICnt][SPCnt][IICnt][IPCnt] = Original[SICnt][SPCnt][IICnt][IPCnt].CloneMe(false, false, false, null);
                    }
                }
            }

            return ToReturn;
        }
        private StatsStructureClass CloneStatsStruct(StatsStructureClass Original)
        {
            StatsStructureClass ToReturn = new StatsStructureClass(MyState.CurrNumberStructureIslands, MyState.StructurePopulationPerIsland, MyState.InitialNumberInternalIslands, MyState.InternalPopulationPerIsland, MyState.StructureGenerations, MyState.InternalGenerations);
            ToReturn.BestIsland = Original.BestIsland;
            for (int SICnt = 0; SICnt < MyState.CurrNumberStructureIslands; SICnt++)
            {
                ToReturn.StructIslandsStats[SICnt].BestStructure = Original.StructIslandsStats[SICnt].BestStructure;
                ToReturn.StructIslandsStats[SICnt].ScoreHistory = Original.StructIslandsStats[SICnt].ScoreHistory.CloneMe();
                ToReturn.StructIslandsStats[SICnt].TestScoreHistory = Original.StructIslandsStats[SICnt].TestScoreHistory.CloneMe();
                ToReturn.StructIslandsStats[SICnt].LayersHistory = Original.StructIslandsStats[SICnt].LayersHistory.CloneMe();
                ToReturn.StructIslandsStats[SICnt].NeuronsHistory = Original.StructIslandsStats[SICnt].NeuronsHistory.CloneMe();
                Array.Copy(Original.StructIslandsStats[SICnt].ScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].ScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].ScoreHistogramData.Length);
                Array.Copy(Original.StructIslandsStats[SICnt].TestScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].TestScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].TestScoreHistogramData.Length);
                Array.Copy(Original.StructIslandsStats[SICnt].LayersHistogramData, 0, ToReturn.StructIslandsStats[SICnt].LayersHistogramData, 0, ToReturn.StructIslandsStats[SICnt].LayersHistogramData.Length);
                Array.Copy(Original.StructIslandsStats[SICnt].NeuronsHistogramData, 0, ToReturn.StructIslandsStats[SICnt].NeuronsHistogramData, 0, ToReturn.StructIslandsStats[SICnt].NeuronsHistogramData.Length);
                if (Original.StructIslandsStats[SICnt].ScoreHistogram != null)
                {
                    if (ToReturn.StructIslandsStats[SICnt].ScoreHistogram == null)
                    {
                        ToReturn.StructIslandsStats[SICnt].ScoreHistogram = new float[Original.StructIslandsStats[SICnt].ScoreHistogram.Length];
                        ToReturn.StructIslandsStats[SICnt].TestScoreHistogram = new float[Original.StructIslandsStats[SICnt].TestScoreHistogram.Length];
                        ToReturn.StructIslandsStats[SICnt].LayersHistogram = new float[Original.StructIslandsStats[SICnt].LayersHistogram.Length];
                        ToReturn.StructIslandsStats[SICnt].NeuronsHistogram = new float[Original.StructIslandsStats[SICnt].NeuronsHistogram.Length];
                    }
                    Array.Copy(Original.StructIslandsStats[SICnt].ScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].ScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].ScoreHistogram.Length);
                    Array.Copy(Original.StructIslandsStats[SICnt].TestScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].TestScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].TestScoreHistogram.Length);
                    Array.Copy(Original.StructIslandsStats[SICnt].LayersHistogram, 0, ToReturn.StructIslandsStats[SICnt].LayersHistogram, 0, ToReturn.StructIslandsStats[SICnt].LayersHistogram.Length);
                    Array.Copy(Original.StructIslandsStats[SICnt].NeuronsHistogram, 0, ToReturn.StructIslandsStats[SICnt].NeuronsHistogram, 0, ToReturn.StructIslandsStats[SICnt].NeuronsHistogram.Length);
                }
                for (int SPCnt = 0; SPCnt < MyState.StructurePopulationPerIsland; SPCnt++)
                {
                    ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].BestIsland = Original.StructIslandsStats[SICnt].StructStats[SPCnt].BestIsland;
                    ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].ScoreHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].ScoreHistory.CloneMe();
                    ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].TestScoreHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].TestScoreHistory.CloneMe();
                    ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].LayersHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].LayersHistory.CloneMe();
                    ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].NeuronsHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].NeuronsHistory.CloneMe();

                    for (int IICnt = 0; IICnt < MyState.InitialNumberInternalIslands; IICnt++)
                    {
                        ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistory.CloneMe();
                        ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistory.CloneMe();
                        Array.Copy(Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogramData.Length);
                        Array.Copy(Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogramData, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogramData.Length);
                        if (Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram != null)
                        {
                            if (ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram == null)
                            {
                                ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram = new float[Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram.Length];
                                ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogram = new float[Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogram.Length];
                            }
                            Array.Copy(Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].ScoreHistogram.Length);
                            Array.Copy(Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogram, 0, ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].TestScoreHistogram.Length);
                        }
                        for (int IPCnt = 0; IPCnt < MyState.InternalPopulationPerIsland; IPCnt++)
                        {
                            ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].ScoreHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].ScoreHistory.CloneMe();
                            ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].TestScoreHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].TestScoreHistory.CloneMe();
                            ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].OutErrorHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].OutErrorHistory.CloneMe();
                            ToReturn.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].TestOutErrorHistory = Original.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt].NetStats[IPCnt].TestOutErrorHistory.CloneMe();
                        }
                    }
                }
            }

            return ToReturn;
        }
        private void CheckAnnealingUpdate(int CurrGeneration, int ThreadID)
        {

            if (MyState.ReduceAnnealing)
            {
                if (CurrGeneration == 0) CurrAnnealing[ThreadID] = MyState.InitialInternalAnnealing;
                float SingleReduceAmount = CurrAnnealing[ThreadID] / (MyState.AnnealingReducingSteps);
                int GenerationsStride = MyState.InternalGenerations / (MyState.AnnealingReducingSteps + 1);
                if (CurrGeneration % GenerationsStride == 0)
                {
                    CurrAnnealing[ThreadID] = (float)(SingleReduceAmount * (Math.Floor((float)(MyState.InternalGenerations - CurrGeneration) / (float)GenerationsStride) - 1));
                    //ParentFormLogging("Curr Annealing : " + CurrAnnealing + " On Generation : " + CurrGeneration, 1);
                }
            }
        }
        private void CheckIslandsHalving(bool IsInternal, int CurrSI, int CurrSP, int CurrGeneration, int ThreadID)
        {
            if (IsInternal)
            {
                if (MyState.HalveInternalIslands && CurrInternalIslands[ThreadID] != 1)
                {
                    if (CurrGeneration == 0) CurrInternalIslands[ThreadID] = MyState.InitialNumberInternalIslands;
                    int GenerationsStride = (int)Math.Ceiling((float)MyState.InternalGenerations / ((float)MyState.InternalIslandsHalvingSteps + 1));

                    if (CurrGeneration % GenerationsStride == 0)
                    {
                        CurrInternalIslands[ThreadID] = CurrGeneration > 0 ? CurrInternalIslands[ThreadID] / 2 : CurrInternalIslands[ThreadID];

                        //ParentFormLogging("Internal IslandsModified : " + CurrInternalIslands + " On Generation : " + CurrGeneration, 1);
                        RestructureIslands(true, false, CurrSI, CurrSP, ThreadID);
                    }
                }
            }
            else
            {
                if (MyState.HalveStructureIslands && MyState.CurrNumberStructureIslands != 1)
                {
                    int GenerationsStride = (int)Math.Ceiling((float)MyState.StructureGenerations / ((float)MyState.StructureIslandsHalvingSteps + 1));
                    int GensRemaining = GenerationsStride - (int)Math.Ceiling(CurrGeneration % (float)GenerationsStride);

                    ParentFormControlSet("LabelStructureHalveIn", "Text", GensRemaining.ToString());
                    if (CurrGeneration > 0 && CurrGeneration % GenerationsStride == 0)
                    {
                        MyState.CurrNumberStructureIslands /= 2;
                        //ParentFormLogging("Struct IslandsModified : " + MyState.CurrNumberStructureIslands + " On Generation : " + CurrGeneration, 1);
                        RestructureIslands(false, false, 0, 0, ThreadID);
                    }
                }
            }
        }
        public void RestructureIslands(bool IsInternal, bool CompleteRestructure, int CurrSI, int CurrSP, int ThreadID)
        {
            if (IsInternal)
            {
                int OldIslands = DevelopingNetsStructure[CurrSI][CurrSP].Count;
                int NewIslands = CurrInternalIslands[ThreadID];
                int OldPopulation = DevelopingNetsStructure[CurrSI][CurrSP][0].Length;
                CurrInternalPopulationPerIsland[ThreadID] = MyState.TotalInternalPopulation / NewIslands;
                int NewPopulation = CurrInternalPopulationPerIsland[ThreadID];
                if (OldIslands == NewIslands) return;

                if (NewIslands < OldIslands)// Merge
                {
                    int Multiplier = OldIslands / NewIslands;
                    for (int SICnt = CompleteRestructure ? 0 : CurrSI; SICnt < (CompleteRestructure ? DevelopingNetsStructure.Count : CurrSI + 1); SICnt++)// in case Struct islands have changed too, we first restructure the Internal islands, so we loop on the old Struct Islands here 
                    {
                        for (int SPCnt = CompleteRestructure ? 0 : CurrSP; SPCnt < (CompleteRestructure ? DevelopingNetsStructure[SICnt].Count : CurrSP + 1); SPCnt++)// in case Struct islands have changed too, we first restructure the Internal islands, so we loop on the old Struct Population here 
                        {
                            List<GenNetwork[]> NewIslandsList = new List<GenNetwork[]>();
                            StatsStructureClass.StructIslandStatsClass.StructStatsClass NewStructStats = new StatsStructureClass.StructIslandStatsClass.StructStatsClass(DevelopingNetsStructure[SICnt].Count, NewIslands, NewPopulation, MyState.InternalGenerations);
                            float BestScore = int.MinValue;
                            {// Stats Restructuring
                                NewStructStats.LayersHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].LayersHistory;
                                NewStructStats.NeuronsHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].NeuronsHistory;
                                NewStructStats.ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].ScoreHistory;
                                NewStructStats.TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].TestScoreHistory;
                            }
                            for (int IICnt = 0; IICnt < NewIslands; IICnt++)
                            {
                                GenNetwork[] NewIsland = new GenNetwork[NewPopulation];
                                StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass NewIslandStats = new StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass(NewPopulation, MyState.InternalGenerations);
                                {// Stats Restructuring
                                    NewIslandStats.ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt * Multiplier].ScoreHistory;
                                    NewIslandStats.TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt * Multiplier].TestScoreHistory;
                                    for (int Cnt = 0; Cnt < Multiplier - 1; Cnt++)
                                    {
                                        NewIslandStats.ScoreHistory.Maximize(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt * Multiplier + Cnt].ScoreHistory);
                                        NewIslandStats.TestScoreHistory.Maximize(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt * Multiplier + Cnt].TestScoreHistory);
                                    }
                                }
                                for (int Cnt = 0; Cnt < Multiplier; Cnt++)
                                {
                                    Array.Copy(DevelopingNetsStructure[SICnt][SPCnt][IICnt * Multiplier + Cnt], 0, NewIsland, OldPopulation * Cnt, OldPopulation);
                                    {// Stats Restructuring
                                        Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt * Multiplier + Cnt].NetStats, 0, NewIslandStats.NetStats, OldPopulation * Cnt, OldPopulation);
                                        Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt * Multiplier + Cnt].ScoreHistogramData, 0, NewIslandStats.ScoreHistogramData, OldPopulation * Cnt, OldPopulation);
                                        Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt * Multiplier + Cnt].TestScoreHistogramData, 0, NewIslandStats.TestScoreHistogramData, OldPopulation * Cnt, OldPopulation);
                                    }
                                }
                                // in merging we sort, in splitting we don't need to
                                Array.Sort(NewIsland, NewIslandStats.NetStats);
                                // Find Best internal island
                                if (BestScore < NewIsland[0].GetScore())
                                {
                                    BestScore = NewIsland[0].GetScore();
                                    NewStructStats.BestIsland = IICnt;
                                }

                                NewIslandsList.Add(NewIsland);
                                NewStructStats.InternalIslandsStats[IICnt] = NewIslandStats;
                            }
                            DevelopingNetsStructure[SICnt][SPCnt] = NewIslandsList;
                            DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt] = NewStructStats;
                        }
                    }
                }
                else // Split
                {
                    int Multiplier = NewIslands / OldIslands;

                    for (int SICnt = CompleteRestructure ? 0 : CurrSI; SICnt < (CompleteRestructure ? DevelopingNetsStructure.Count : CurrSI + 1); SICnt++)// in case Struct islands have changed too, we first restructure the Internal islands, so we loop on the old Struct Islands here 
                    {
                        for (int SPCnt = CompleteRestructure ? 0 : CurrSP; SPCnt < (CompleteRestructure ? DevelopingNetsStructure[SICnt].Count : CurrSP + 1); SPCnt++)// in case Struct islands have changed too, we first restructure the Internal islands, so we loop on the old Struct Population here 
                        {
                            List<GenNetwork[]> NewIslandsList = new List<GenNetwork[]>();
                            StatsStructureClass.StructIslandStatsClass.StructStatsClass NewStructStats = new StatsStructureClass.StructIslandStatsClass.StructStatsClass(DevelopingNetsStructure[SICnt].Count, NewIslands, NewPopulation, MyState.InternalGenerations);
                            float BestScore = int.MinValue;
                            {// Stats Restructuring
                                NewStructStats.LayersHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].LayersHistory;
                                NewStructStats.NeuronsHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].NeuronsHistory;
                                NewStructStats.ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].ScoreHistory;
                                NewStructStats.TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].TestScoreHistory;
                            }
                            for (int IICnt = 0; IICnt < NewIslands; IICnt++)
                            {
                                GenNetwork[] NewIsland = new GenNetwork[NewPopulation];
                                Array.Copy(DevelopingNetsStructure[SICnt][SPCnt][IICnt / Multiplier], (IICnt & (Multiplier - 1)) * NewPopulation, NewIsland, 0, NewPopulation);
                                StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass NewIslandStats = new StatsStructureClass.StructIslandStatsClass.StructStatsClass.InternalIslandStatsClass(NewPopulation, MyState.InternalGenerations);
                                {// Stats Restructuring
                                    NewIslandStats.ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt / Multiplier].ScoreHistory.CloneMe();
                                    NewIslandStats.TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt / Multiplier].TestScoreHistory.CloneMe();
                                    Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt / Multiplier].NetStats, (IICnt & (Multiplier - 1)) * NewPopulation, NewIslandStats.NetStats, 0, NewPopulation);
                                    Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt / Multiplier].ScoreHistogramData, (IICnt & (Multiplier - 1)) * NewPopulation, NewIslandStats.ScoreHistogramData, 0, NewPopulation);
                                    Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt].InternalIslandsStats[IICnt / Multiplier].TestScoreHistogramData, (IICnt & (Multiplier - 1)) * NewPopulation, NewIslandStats.TestScoreHistogramData, 0, NewPopulation);
                                    NewIslandStats.ScoreHistogram = ListToHistogram(NewIslandStats.ScoreHistogramData);
                                    NewIslandStats.TestScoreHistogram = ListToHistogram(NewIslandStats.TestScoreHistogramData);

                                }
                                // Find Best internal island
                                if (BestScore < NewIsland[0].GetScore())
                                {
                                    BestScore = NewIsland[0].GetScore();
                                    NewStructStats.BestIsland = IICnt;
                                }
                                NewIslandsList.Add(NewIsland);
                                NewStructStats.InternalIslandsStats[IICnt] = NewIslandStats;
                            }
                            DevelopingNetsStructure[SICnt][SPCnt] = NewIslandsList;
                            DevelopingStatsStructure.StructIslandsStats[SICnt].StructStats[SPCnt] = NewStructStats;
                        }
                    }
                }
            }
            else
            {
                int SliderValue;
                if (MyState.CurrNumberStructureIslands == 1) SliderValue = 0;
                else if (MyState.CurrNumberStructureIslands == 2) SliderValue = 1;
                else if (MyState.CurrNumberStructureIslands == 4) SliderValue = 2;
                else if (MyState.CurrNumberStructureIslands == 8) SliderValue = 3;
                else /*if (MyState.NumberStructureIslands == 16)*/ SliderValue = 4;

                ParentFormControlSet("SliderStructureIslands", "Value", SliderValue);
                ParentFormControlSet("LabelStructureIslands", "Text", MyState.CurrNumberStructureIslands.ToString());

                int OldIslands = DevelopingNetsStructure.Count;
                int NewIslands = MyState.CurrNumberStructureIslands;
                int OldPopulation = DevelopingNetsStructure[0].Count;
                MyState.StructurePopulationPerIsland = MyState.TotalStructurePopulation / NewIslands;
                int NewPopulation = MyState.StructurePopulationPerIsland;
                if (OldIslands == NewIslands) return;

                if (NewIslands < OldIslands)// Merge
                {
                    int Multiplier = OldIslands / NewIslands;
                    List<List<List<GenNetwork[]>>> NewNetsStruct = new List<List<List<GenNetwork[]>>>();
                    StatsStructureClass NewStructStats = new StatsStructureClass(NewIslands, MyState.StructurePopulationPerIsland, 0, 0, 0, 0);// Dont need to create a full structure, because we will overwrite it 
                    float BestScore = int.MinValue;
                    for (int SICnt = 0; SICnt < NewIslands; SICnt++)
                    {
                        List<List<GenNetwork[]>> NewIsland = new List<List<GenNetwork[]>>();
                        StatsStructureClass.StructIslandStatsClass NewIslandStats = new StatsStructureClass.StructIslandStatsClass(MyState.StructurePopulationPerIsland, 0, 0, 0, 0);
                        {// Stats Restructuring
                            NewIslandStats.ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier].ScoreHistory;
                            NewIslandStats.TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier].TestScoreHistory;
                            NewIslandStats.LayersHistory = DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier].LayersHistory;
                            NewIslandStats.NeuronsHistory = DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier].NeuronsHistory;
                            NewIslandStats.StructStats.Clear();
                        }
                        for (int Cnt = 0; Cnt < Multiplier; Cnt++)
                        {
                            NewIsland.AddRange(DevelopingNetsStructure[SICnt * Multiplier + Cnt]);
                            {// Stats Restructuring
                                NewIslandStats.StructStats.AddRange(DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier + Cnt].StructStats);
                                Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier + Cnt].ScoreHistogramData, 0, NewIslandStats.ScoreHistogramData, OldPopulation * Cnt, OldPopulation);
                                Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier + Cnt].TestScoreHistogramData, 0, NewIslandStats.TestScoreHistogramData, OldPopulation * Cnt, OldPopulation);
                                Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier + Cnt].LayersHistogramData, 0, NewIslandStats.LayersHistogramData, OldPopulation * Cnt, OldPopulation);
                                Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt * Multiplier + Cnt].NeuronsHistogramData, 0, NewIslandStats.NeuronsHistogramData, OldPopulation * Cnt, OldPopulation);
                            }
                        }
                        // Find Best Struct
                        float BestSScore = int.MinValue;
                        for (int Cnt = 0; Cnt < NewPopulation; Cnt++)
                        {
                            if (BestSScore < NewIsland[Cnt][NewIslandStats.StructStats[Cnt].BestIsland][0].GetScore())
                            {
                                NewIslandStats.BestStructure = Cnt;
                                BestSScore = NewIsland[Cnt][NewIslandStats.StructStats[Cnt].BestIsland][0].GetScore();
                            }
                        }

                        //Find best Struct Island
                        if (BestScore < NewIsland[NewIslandStats.BestStructure][NewIslandStats.StructStats[NewIslandStats.BestStructure].BestIsland][0].GetScore())
                        {
                            BestScore = NewIsland[NewIslandStats.BestStructure][NewIslandStats.StructStats[NewIslandStats.BestStructure].BestIsland][0].GetScore();
                            NewStructStats.BestIsland = SICnt;
                        }
                        NewNetsStruct.Add(NewIsland);
                        NewStructStats.StructIslandsStats[SICnt] = NewIslandStats;
                    }
                    DevelopingNetsStructure = NewNetsStruct;
                    DevelopingStatsStructure = NewStructStats;
                }
                else // Split
                {
                    int Multiplier = NewIslands / OldIslands;
                    List<List<List<GenNetwork[]>>> NewNetsStruct = new List<List<List<GenNetwork[]>>>();
                    StatsStructureClass NewStructStats = new StatsStructureClass(NewIslands, MyState.StructurePopulationPerIsland, 0, 0, 0, 0);// Dont need to create a full structure, because we will overwrite it 
                    float BestScore = int.MinValue;
                    for (int SICnt = 0; SICnt < NewIslands; SICnt++)
                    {
                        List<List<GenNetwork[]>> NewIsland = new List<List<GenNetwork[]>>(DevelopingNetsStructure[SICnt / Multiplier].GetRange((SICnt & (Multiplier - 1)) * NewPopulation, NewPopulation));
                        StatsStructureClass.StructIslandStatsClass NewIslandStats = new StatsStructureClass.StructIslandStatsClass(MyState.StructurePopulationPerIsland, 0, 0, 0, 0);
                        NewIslandStats.StructStats.Clear();
                        {// Stats Restructuring
                            NewIslandStats.StructStats.AddRange(DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].StructStats.GetRange((SICnt & (Multiplier - 1)) * NewPopulation, NewPopulation));
                            Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].ScoreHistogramData, (SICnt & (Multiplier - 1)) * NewPopulation, NewIslandStats.ScoreHistogramData, 0, NewPopulation);
                            Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].TestScoreHistogramData, (SICnt & (Multiplier - 1)) * NewPopulation, NewIslandStats.TestScoreHistogramData, 0, NewPopulation);
                            Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].LayersHistogramData, (SICnt & (Multiplier - 1)) * NewPopulation, NewIslandStats.LayersHistogramData, 0, NewPopulation);
                            Array.Copy(DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].NeuronsHistogramData, (SICnt & (Multiplier - 1)) * NewPopulation, NewIslandStats.NeuronsHistogramData, 0, NewPopulation);

                            NewIslandStats.ScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].ScoreHistory.CloneMe();
                            NewIslandStats.TestScoreHistory = DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].TestScoreHistory.CloneMe();
                            NewIslandStats.LayersHistory = DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].LayersHistory.CloneMe();
                            NewIslandStats.NeuronsHistory = DevelopingStatsStructure.StructIslandsStats[SICnt / Multiplier].NeuronsHistory.CloneMe();
                        }
                        // Find Best Struct
                        float BestSScore = int.MinValue;
                        for (int Cnt = 0; Cnt < NewPopulation; Cnt++)
                        {
                            if (BestSScore < NewIsland[Cnt][NewIslandStats.StructStats[Cnt].BestIsland][0].GetScore())
                            {
                                NewIslandStats.BestStructure = Cnt;
                                BestSScore = NewIsland[Cnt][NewIslandStats.StructStats[Cnt].BestIsland][0].GetScore();
                            }
                        }

                        //Find best Struct Island
                        if (BestScore < NewIsland[NewIslandStats.BestStructure][NewIslandStats.StructStats[NewIslandStats.BestStructure].BestIsland][0].GetScore())
                        {
                            BestScore = NewIsland[NewIslandStats.BestStructure][NewIslandStats.StructStats[NewIslandStats.BestStructure].BestIsland][0].GetScore();
                            NewStructStats.BestIsland = SICnt;
                        }
                        NewNetsStruct.Add(NewIsland);
                        NewStructStats.StructIslandsStats[SICnt] = NewIslandStats;
                    }
                    DevelopingNetsStructure = NewNetsStruct;
                    DevelopingStatsStructure = NewStructStats;
                }
                SettledNetsStructure = CloneNetsStruct(DevelopingNetsStructure);
                SettledStatsStructure = CloneStatsStruct(DevelopingStatsStructure);
            }
        }

        // Control
        private void PreProcess(bool JustPressedButton)// Just a wrapper of initializations common to both single and multithreaded
        {
            Rnd = new Random();
            if (MyState.ThreadingActivated && MyThreads != null)
            {
                CurrAnnealing = new float[MyThreads.Waiting()];
                CurrInternalIslands = new int[MyThreads.Waiting()];
                CurrInternalPopulationPerIsland = new int[MyThreads.Waiting()];
                for (int Cnt = 0; Cnt < MyThreads.Waiting(); Cnt++)
                {
                    CurrAnnealing[Cnt] = MyState.InitialInternalAnnealing;
                    CurrInternalIslands[Cnt] = MyState.InitialNumberInternalIslands;
                    CurrInternalPopulationPerIsland[Cnt] = MyState.InternalPopulationPerIsland;
                }
            }
            else
            {
                CurrAnnealing = new float[1];
                CurrInternalIslands = new int[1];
                CurrInternalPopulationPerIsland = new int[1];
                CurrAnnealing[0] = MyState.InitialInternalAnnealing;
                CurrInternalIslands[0] = MyState.InitialNumberInternalIslands;
                CurrInternalPopulationPerIsland[0] = MyState.InternalPopulationPerIsland;
            }

            if (MyState.InternalIslandRestructuringNeeded && SettledNetsStructure[0][0].Count != MyState.InitialNumberInternalIslands)// Don't change the order. first we reorder the internals and then the structs
                RestructureIslands(true, true, 0, 0, 0);

            if (MyState.StructIslandRestructuringNeeded && SettledNetsStructure.Count != MyState.CurrNumberStructureIslands)
                RestructureIslands(false, false, 0, 0, 0);

            MyState.InternalIslandRestructuringNeeded = false;
            MyState.StructIslandRestructuringNeeded = false;

            if (JustPressedButton)
            {
                MyGlobalWatch.Reset();
                MyGlobalWatch.Start();

                if (MyState.CurrStructureGeneration >= MyState.StructureGenerations - 1)
                {
                    MyState.CurrStructureGeneration = 0;
                    ParentFormLogging("Resetting Structure generation To 0 ... ", 0);
                }
                else if (MyState.CurrStructureGeneration == 0)
                    ParentFormLogging("Training Started.", 0);
            }

        }
        private void PostProcess(int ThreadID)
        {
            if (DevelopingNetsStructure[0][0].Count != CurrInternalIslands[ThreadID])// Don't change the order. first we reorder the internals and then the structs
                RestructureIslands(true, true, 0, 0, ThreadID);

            if (MyState.CurrStructureGeneration == MyState.StructureGenerations - 1)// Means training has ended
                MyState.CurrNumberStructureIslands = MyState.InitialNumberStructureIslands;
            if (DevelopingNetsStructure.Count != MyState.CurrNumberStructureIslands)
                RestructureIslands(false, false, 0, 0, ThreadID);
        }
        public void LoadState()
        {
            FileStream MyFile;
            MyFile = File.OpenRead(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\GenTrainingSave");
            BinaryFormatter BF = new BinaryFormatter();
            try
            {
                MyState = BF.Deserialize(MyFile) as StateClass;
                if (MyState.IncludeNetsInTheSave)
                {
                    SettledNetsStructure = MyState.NetsStructureToSave;
                    DevelopingNetsStructure = CloneNetsStruct(SettledNetsStructure);
                    ResetStructures(true, true);
                }
                else ResetStructures(true, false);

                ParentFormLogging(" State Loaded Succesfully.", 0);
                MyFile.Close();
            }
            catch (Exception Ex)
            {
                ParentFormLogging(" Error While Loading... Probably file is obsolete... ", 1);
                ParentFormLogging(" Message: " + Ex.Message + "\n Trace: " + new StackTrace(Ex, true).ToString(), 2);
            }
        }
        public void ForceSaveState()
        {
            BoolSaveState = false;

            FileStream MyFile;
            if (StateFileExists)
                MyFile = File.OpenWrite(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\GenTrainingSave");
            else
                MyFile = File.Create(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\GenTrainingSave");

            BinaryFormatter BF = new BinaryFormatter();

            if (MyState.IncludeNetsInTheSave)
            {
                PreProcess(false);
                MyState.NetsStructureToSave = DevelopingNetsStructure;
            }

            else MyState.NetsStructureToSave = null;

            BF.Serialize(MyFile, MyState);
            MyFile.Close();
            ParentFormLogging(" State Saved Succesfully.", 0);
            StateFileExists = true;
        }
        public void SaveState()
        {
            BoolSaveState = true;

            ParentFormLogging(string.Format("State will be saved at Generation ({0},{1}). Press again to Cancel. Hold Down to Force Save now.", MyState.CurrStructureGeneration + 1, 0), 0);
        }
        public void ForceStop() { ForceStopTraining = true; StopTraining = false; }
        public void Stop(bool Undo)
        {
            StopTraining = !Undo;
            if (!Undo)
                ParentFormLogging(string.Format("Training Will be stopped at Generation ({0},{1}). Press again to Cancel. Hold Down to Force Stop now.", MyState.CurrStructureGeneration + 1, 0), 0);
        }
    }
}
