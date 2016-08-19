using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Threading;

namespace GeneticNetworkTrainer
{
    public partial class GenTrainer
    {
        // Dynamic Threading Variables
        private Stopwatch DynamicThreadingWatch = new Stopwatch();
        private CircularBuffer Last5StructGenTimings = new CircularBuffer(5);
        private CircularBuffer Last5ThreadsinParallel = new CircularBuffer(5);

        private class ThreadsStruct
        {
            public class SingleThread
            {
                public bool Running;
                public bool Finished;
                public int StructIsland;
                public int StructIdx;
                public SingleThread(int iStructIsland, int iStructIdx) { StructIsland = iStructIsland; StructIdx = iStructIdx; }
            }

            private List<SingleThread> NotStartedThreads = new List<SingleThread>();
            private List<SingleThread> RunningThreads = new List<SingleThread>();
            public void Clean()
            {
                for (int Cnt = NotStartedThreads.Count - 1; Cnt >= 0; Cnt--)
                    if (NotStartedThreads[Cnt].Running)
                    {
                        RunningThreads.Add(NotStartedThreads[Cnt]);
                        NotStartedThreads.RemoveAt(Cnt);
                    }
                for (int Cnt = RunningThreads.Count - 1; Cnt >= 0; Cnt--)
                    if (!RunningThreads[Cnt].Running)
                        RunningThreads.RemoveAt(Cnt);
            }
            public SingleThread GetNextThreadObj()
            {
                for (int Cnt = 0; Cnt < NotStartedThreads.Count; Cnt++)
                    if (!NotStartedThreads[Cnt].Running && !NotStartedThreads[Cnt].Finished) return NotStartedThreads[Cnt];

                return null;
            }
            public void Add(int iStructIsland, int iStructIdx) { NotStartedThreads.Add(new SingleThread(iStructIsland, iStructIdx)); }
            public int Running() { return RunningThreads.Count; }
            public int Waiting() { return NotStartedThreads.Count; }
        }
        private ThreadsStruct MyThreads;

        // Locking Objects
        private object SctructLocker = new object();
        private object StructGenLocker = new object();

        private static ManualResetEvent StructWaitHandle = new ManualResetEvent(false);

        public void LaunchNextStructGeneration(object Dummy)
        {
            Rnd = new Random();
            ParentFormControlSet("LabelCurrInternalGen", "Text", "-");
            ParentFormControlSet("LabelCurrStructGen", "Text", MyState.CurrStructureGeneration.ToString());
            CheckIslandsUpadate(false, 0, 0, MyState.CurrStructureGeneration);
            //preparing threads
            if (MyState.ThreadingActivated && MyState.DynamicSearchMaxThreads) CalculateDynamicThreading(true);
            MyThreads = new ThreadsStruct();
            for (int SICnt = 0; SICnt < MyState.CurrNumberStructureIslands; SICnt++)
                for (int SPCnt = 0; SPCnt < MyState.StructurePopulationPerIsland; SPCnt++)
                    MyThreads.Add(SICnt, SPCnt);

            for (int SICnt = 0; SICnt < MyState.CurrNumberStructureIslands; SICnt++)
                NextStructGeneration(SICnt);// prepare generation for this Structure Island
            LaunchAnyWaitingThreads();
        }
        private void LaunchAnyWaitingThreads()
        {
            while (true)
            {
                while (MyThreads.Waiting() != 0 && MyThreads.Running() < MyState.MaxThreadsinParallel)
                {
                    ThreadsStruct.SingleThread CurrThreadData = MyThreads.GetNextThreadObj();
                    if (CurrThreadData != null)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(SingleStructThread), CurrThreadData);
                        CurrThreadData.Running = true;
                    }
                    MyThreads.Clean();
                }
                StructWaitHandle.WaitOne();
                StructWaitHandle.Reset();
                MyThreads.Clean();
                if (MyThreads.Waiting() == 0 && MyThreads.Running() == 0)
                { MultiTStructGenerationCompeted(); return; }
            }
        }

        private void SingleStructThread(object Input)
        {
            PreProcess();
            ParentFormLogging("Training Started.", 0);
            ParentFormControlSet("LabelCurrStructure", "Text", (MyState.TotalStructurePopulation - MyThreads.Waiting()).ToString());
            ThreadsStruct.SingleThread CurrThreadData = (ThreadsStruct.SingleThread)Input;
            //try
            //{
            PopulateStatsStructure(TrainingState.StructStarted, CurrThreadData.StructIsland, CurrThreadData.StructIdx, 0, 0);
            for (int IntGenCnt = 0; IntGenCnt < MyState.InternalGenerations; IntGenCnt++)
            {
                CheckAnnealingUpdate(IntGenCnt);
                CheckIslandsUpadate(true, CurrThreadData.StructIsland, CurrThreadData.StructIdx, IntGenCnt);
                for (int IICnt = 0; IICnt < CurrInternalIslands; IICnt++)
                {
                    NextInternalGeneration(CurrThreadData.StructIsland, CurrThreadData.StructIdx, IICnt);// prepare generation for this Internal Island

                    for (int IPCnt = 0; IPCnt < MyState.InternalPopulationPerIsland; IPCnt++)
                    {
                        if (ForceStopTraining) { CurrThreadData.Running = false; CurrThreadData.Finished = true; StructWaitHandle.Set(); return; }
                        DevelopingNetsStructure[CurrThreadData.StructIsland][CurrThreadData.StructIdx][IICnt][IPCnt].ResetScores();
                        if (!DevelopingNetsStructure[CurrThreadData.StructIsland][CurrThreadData.StructIdx][IICnt][IPCnt].CalculateScores(MyState.InData, MyState.LabelData, MyState.DataToUse, MyState.HalfDataForTesting, MyState.ScoreRule, MyState.ThresholdOfValidOut))
                        {
                            ParentFormLogging(string.Format("Scoring Calculation failed for net ({0}{1}{2}{3}). Inputs or Outputs dont match the nets IOs. ", CurrThreadData.StructIsland, CurrThreadData.StructIdx, IICnt, IPCnt), 2);
                            return;
                        }
                        PopulateStatsStructure(TrainingState.NetEnded, CurrThreadData.StructIsland, CurrThreadData.StructIdx, IICnt, IPCnt);
                    }
                    PopulateStatsStructure(TrainingState.InternalIslandEnded, CurrThreadData.StructIsland, CurrThreadData.StructIdx, IICnt, 0);
                }
                PopulateStatsStructure(TrainingState.InternalGenEnded, CurrThreadData.StructIsland, CurrThreadData.StructIdx, 0, 0);
            }
            PopulateStatsStructure(TrainingState.StructEnded, CurrThreadData.StructIsland, CurrThreadData.StructIdx, 0, 0);
            CurrThreadData.Running = false;
            CurrThreadData.Finished = true;

            StructWaitHandle.Set();
            //}
            //catch (Exception Ex)
            //{
            //    ParentFormLogging(" Error While training. Message: " + Ex.Message + "\n Trace: " + new StackTrace(Ex, true).ToString(), 2);
            //}
        }

        private void MultiTStructGenerationCompeted()
        {
            if (ForceStopTraining) { CallTheForm(TrainingState.TrainingStopped); StopTraining = false; ForceStopTraining = false; return; }//stop before populating STats

            for (int Cnt = 0; Cnt < MyState.CurrNumberStructureIslands; Cnt++)
                PopulateStatsStructure(TrainingState.StructIslandEnded, Cnt, 0, 0, 0);

            PopulateStatsStructure(TrainingState.StructGenEnded, 0, 0, 0, 0);
            SettledNetsStructure = CloneNetsStruct(DevelopingNetsStructure);
            SettledStatsStructure = CloneStatsStruct(DevelopingStatsStructure);
            CheckStopConditions();
            if (MyState.ThreadingActivated && MyState.DynamicSearchMaxThreads) CalculateDynamicThreading(false);
            CallTheForm(TrainingState.StructGenEnded);

            MyState.CurrStructureGeneration++;
            if (StopTraining) { CallTheForm(TrainingState.TrainingStopped); StopTraining = false; return; }
            if (MyState.CurrStructureGeneration < MyState.StructureGenerations) ThreadPool.QueueUserWorkItem(new WaitCallback(LaunchNextStructGeneration));
            else CallTheForm(TrainingState.TrainingEnded);
        }

        private void CalculateDynamicThreading(bool Start)
        {
            if (Start)
            {
                DynamicThreadingWatch.Reset();
                DynamicThreadingWatch.Start();
                float[] Timings = Last5StructGenTimings.GetData();
                float[] ThrInP = Last5ThreadsinParallel.GetData();
                if (Timings.Length == 0) return;
                else if (Timings.Length == 1)
                {
                    MyState.MaxThreadsinParallel++;
                    ParentFormControlSet("TextBoxThreadsInParallel", "Text", MyState.MaxThreadsinParallel.ToString());
                }
                else
                {
                    Array.Sort(Timings, ThrInP);
                    if (ThrInP[0] - ThrInP[1] < 0)
                    {
                        if (MyState.MaxThreadsinParallel > 1)
                            MyState.MaxThreadsinParallel--;
                    }
                    else MyState.MaxThreadsinParallel++;
                    ParentFormControlSet("TextBoxThreadsInParallel", "Text", MyState.MaxThreadsinParallel.ToString());
                }
            }
            else
            {
                DynamicThreadingWatch.Stop();
                ParentFormControlSet("LabelDynB", "Text", ((float)DynamicThreadingWatch.ElapsedMilliseconds / 60000).ToString("0.00"));

                Last5StructGenTimings.PutValue(DynamicThreadingWatch.ElapsedMilliseconds);
                Last5ThreadsinParallel.PutValue(MyState.MaxThreadsinParallel);
            }
        }
    }
}
