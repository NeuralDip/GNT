using System;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;


namespace GeneticNetworkTrainerForm
{
    partial class MainForm
    {
        private TabControl MainTabControl;
        private TabPage TabMainControl;
        private TabPage TabInternalGenetics;
        private TabPage TabStructureGenetics;
        private Button ButtonStartStop;
        private RichTextBox Console;
        private GroupBox groupBox3;
        private Label label4;
        private Label label3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private TrackBar SliderDataToUse;
        private Label LabelDataToUse;
        private Label LabelTotalData;
        private Label label6;
        private Label label5;
        private Label label1;
        private Button ButtonSelectData;
        private OpenFileDialog OpenFileDialog;
        private TextBox TextBoxNetOutput;
        private TextBox TextBoxNetInput;
        private TextBox TextBoxThreadsInParallel;
        private Label label2;
        private CheckBox CheckBoxParallelThreadsDS;
        private CheckBox CheckBoxActivateThreading;
        private GroupBox groupBox4;
        private Label label11;
        private TabPage TabStructureInspector;
        private GroupBox groupBox5;
        private TextBox TextBoxStructureGenerations;
        private Label label17;
        private TextBox TextBoxInternalPopulation;
        private Label label18;
        private CheckBox CheckBoxLog;
        private TrackBar SliderInternalMutationStrength;
        private Label label14;
        private TrackBar SliderInternalCrossover;
        private Label label26;
        private Label label27;
        private TrackBar SliderInternalMutation;
        private Label label15;
        private Label label25;
        private TrackBar SliderInternalCopy;
        private Label label12;
        private Label label13;
        private GroupBox groupBox7;
        private Label label7;
        private GroupBox groupBox8;
        private Label LabelAnnealing;
        private TextBox TextBoxLimitWeights;
        private Label label33;
        private CheckBox CheckBoxAnealReduce;
        private TrackBar SliderAnnealing;
        private Label label34;
        private Label label35;
        private Label LabelInternalCopy;
        private Label LabelInternalMutationStrength;
        private Label LabelInternalMutation;
        private Label LabelInternalCrossover;
        private TextBox TextBoxInternalGenerations;
        private Label label30;
        private TextBox TextBoxStructurePopulation;
        private Label label29;
        private TextBox TextBoxInternalIslandsStep;
        private Label label28;
        private CheckBox CheckBoxInternalHalve;
        private Label LabelInternalIslands;
        private TrackBar SliderInternalIslands;
        private Label label21;
        private CheckBox CheckBoxMutateWeights;
        private CheckBox CheckBoxMutateBiases;
        private GroupBox groupBox10;
        private Label LabelStructureHalveIn;
        private Label label50;
        private Label label51;
        private TextBox TextBoxStructureIslandsSteps;
        private Label label52;
        private CheckBox CheckBoxStructureHalve;
        private Label LabelStructureIslands;
        private TrackBar SliderStructureIslands;
        private Label label54;
        private GroupBox groupBox11;
        private TextBox TextBoxScoreLayer;
        private CheckBox CheckBoxScoreLayers;
        private GroupBox groupBox12;
        private Label LabelStructureCopy;
        private Label LabelStructureMutationStrength;
        private Label LabelStructureMutation;
        private Label LabelStructureCrossover;
        private TrackBar SliderStructureCrossover;
        private TrackBar SliderStructureMutationStrength;
        private Label label61;
        private Label label62;
        private Label label63;
        private TrackBar SliderStructureMutation;
        private Label label64;
        private Label label65;
        private Label label66;
        private TrackBar SliderStructureCopy;
        private Label label67;
        private Label label68;
        private TextBox TextBoxConnectionCost;
        private Label label72;
        private TextBox TextBoxNeuronCost;
        private Label label71;
        private TextBox TextBoxLayerCost;
        private Label label70;
        private TextBox TextBoxFunctionCost;
        private Label label69;
        private TextBox TextBoxScoreNeurons;
        private CheckBox CheckBoxScoreNeurons;
        private GroupBox groupBox6;
        private Label label19;
        private TextBox TextBoxStopCondTime;
        private CheckBox CheckBoxStopCondTime;
        private TextBox TextBoxStopCondScore;
        private Label label20;
        private CheckBox CheckBoxStopCondScore;
        private CheckBox CheckBoxWeightsRandom;
        private CheckBox CheckBoxBiasesRandom;
        private ListView ListViewStructures;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ListView ListViewStructIslands;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private TabPage TabInternalInspector;
        private GroupBox groupBox16;
        private GroupBox groupBox17;
        private GroupBox GroupBoxGlobalStats;
        private Label label56;
        private Label label55;
        private Label label73;
        private Label label74;
        private ListView ListViewNets;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ListView ListViewInternalIslands;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private GroupBox groupBox19;
        private Label label78;
        private Label label75;
        private Label label77;
        private GroupBox groupBox22;
        private RadioButton RadioStructLayer;
        private RadioButton RadioStructNeurons;
        private RadioButton RadioStructScore;
        private Panel StructPlotPlaceHolder;
        private GroupBox groupBox21;
        private RadioButton RadioStructIslandHistogram;
        private RadioButton RadioStructIslandSeries;
        private GroupBox groupBox20;
        private RadioButton RadioStructIslandLayers;
        private RadioButton RadioStructIslandNeurons;
        private RadioButton RadioStructIslandScore;
        private Panel StructIslandPlotPlaceHolder;
        private Label LabelStatScore;
        private Label label84;
        private Label LabelStatsStructIsland;
        private Label LabelStructID;
        private Label LabelIntIsland;
        private GroupBox groupBox23;
        private GroupBox groupBox13;
        private Panel InternalPlotPlaceHolder;
        private GroupBox groupBox15;
        private GroupBox groupBox24;
        private RadioButton RadioInternalHistogram;
        private RadioButton RadioInternalSeries;
        private Panel InternalIslandPlotPlaceHolder;
        private TextBox TextBoxLimitBiases;
        private Label label8;
        private Button ButtonLogSelected;
        private CheckBox CheckBoxLogStructOnly;
        private Button ButtonSaveSelected;
        private Button ButtonSaveState;
        private TextBox TextBoxInternalAnnealStep;
        private RadioButton RadioRules1X2;
        private RadioButton RadioRulesOutError;
        private Label LabelCurrStructGen;
        private Label label32;
        private Label LabelCurrInternalGen;
        private Label label9;
        private CheckBox CheckBoxHalfForTesting;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader13;
        private GroupBox GroupBoxStructType;
        private Label LabelStructTypeWeights;
        private Label label31;
        private Label LabelStructTypeLayers;
        private Label LabelStructTypeNeurons;
        private Label label93;
        private Label label95;
        private Label label22;
        private TextBox TextBoxStopCondTesting;
        private CheckBox CheckBoxStopCondTesting;
        private Button ButtonResetState;
        private GroupBox groupBox9;
        private Button ButtonLoadState;
        private ColumnHeader columnHeader14;
        private RadioButton RadioStructTestScore;
        private RadioButton RadioStructIslandTestScore;
        private GroupBox groupBox14;
        private RadioButton RadioInternalTestScore;
        private RadioButton RadioInternalScore;
        private Label LabelCurrStructure;
        private TabPage TabHandTester;
        private TableLayoutPanel LayoutIn;
        private TableLayoutPanel LayoutOut;
        private Label LabelStatTestScore;
        private Label label16;
        private Label label36;
        private Label label10;
        private GroupBox Message;
        private TextBox LabelMessage;
        private Button ButtonEvaluate;
        private TrackBar SliderHandData;
        private Label LabelHandData;
        private TextBox TextBoxScoreConnections;
        private CheckBox CheckBoxScoreConnections;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.TabMainControl = new System.Windows.Forms.TabPage();
            this.LabelGlobalStopwatchElapsed = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.CheckBoxIncludeNetsInSave = new System.Windows.Forms.CheckBox();
            this.ButtonLoadState = new System.Windows.Forms.Button();
            this.ButtonSaveState = new System.Windows.Forms.Button();
            this.ButtonResetState = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.TextBoxStopCondTesting = new System.Windows.Forms.TextBox();
            this.CheckBoxStopCondTesting = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.TextBoxStopCondTime = new System.Windows.Forms.TextBox();
            this.CheckBoxStopCondTime = new System.Windows.Forms.CheckBox();
            this.TextBoxStopCondScore = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.CheckBoxStopCondScore = new System.Windows.Forms.CheckBox();
            this.CheckBoxLog = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.LabelCurrStructure = new System.Windows.Forms.Label();
            this.LabelCurrStructGen = new System.Windows.Forms.Label();
            this.TextBoxInternalPopulation = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.TextBoxInternalGenerations = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.LabelCurrInternalGen = new System.Windows.Forms.Label();
            this.TextBoxStructurePopulation = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.TextBoxStructureGenerations = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TextBoxNetOutput = new System.Windows.Forms.TextBox();
            this.TextBoxNetInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LabelDynC = new System.Windows.Forms.Label();
            this.LabelDynB = new System.Windows.Forms.Label();
            this.LabelDynA = new System.Windows.Forms.Label();
            this.TextBoxThreadsInParallel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CheckBoxParallelThreadsDS = new System.Windows.Forms.CheckBox();
            this.CheckBoxActivateThreading = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckBoxHalfForTesting = new System.Windows.Forms.CheckBox();
            this.SliderDataToUse = new System.Windows.Forms.TrackBar();
            this.LabelDataToUse = new System.Windows.Forms.Label();
            this.LabelTotalData = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonSelectData = new System.Windows.Forms.Button();
            this.ButtonStartStop = new System.Windows.Forms.Button();
            this.Console = new System.Windows.Forms.RichTextBox();
            this.TabStructureGenetics = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.LabelStructureHalveIn = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.TextBoxStructureIslandsSteps = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.CheckBoxStructureHalve = new System.Windows.Forms.CheckBox();
            this.LabelStructureIslands = new System.Windows.Forms.Label();
            this.SliderStructureIslands = new System.Windows.Forms.TrackBar();
            this.label54 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.TextBoxScoreConnections = new System.Windows.Forms.TextBox();
            this.CheckBoxScoreConnections = new System.Windows.Forms.CheckBox();
            this.TextBoxScoreLayer = new System.Windows.Forms.TextBox();
            this.TextBoxScoreNeurons = new System.Windows.Forms.TextBox();
            this.CheckBoxScoreNeurons = new System.Windows.Forms.CheckBox();
            this.CheckBoxScoreLayers = new System.Windows.Forms.CheckBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.CheckBoxWeightsRandom = new System.Windows.Forms.CheckBox();
            this.CheckBoxBiasesRandom = new System.Windows.Forms.CheckBox();
            this.TextBoxConnectionCost = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.TextBoxNeuronCost = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.TextBoxLayerCost = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.TextBoxFunctionCost = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.LabelStructureCopy = new System.Windows.Forms.Label();
            this.LabelStructureMutationStrength = new System.Windows.Forms.Label();
            this.LabelStructureMutation = new System.Windows.Forms.Label();
            this.LabelStructureCrossover = new System.Windows.Forms.Label();
            this.SliderStructureCrossover = new System.Windows.Forms.TrackBar();
            this.SliderStructureMutationStrength = new System.Windows.Forms.TrackBar();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.SliderStructureMutation = new System.Windows.Forms.TrackBar();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.SliderStructureCopy = new System.Windows.Forms.TrackBar();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.TabInternalGenetics = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.TextBoxRulesValidThreshold = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.TextBoxRulesWinThreshold = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.RadioRules1X2 = new System.Windows.Forms.RadioButton();
            this.RadioRulesOutError = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TextBoxInternalAnnealStep = new System.Windows.Forms.TextBox();
            this.LabelAnnealing = new System.Windows.Forms.Label();
            this.CheckBoxAnealReduce = new System.Windows.Forms.CheckBox();
            this.SliderAnnealing = new System.Windows.Forms.TrackBar();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.TextBoxInternalIslandsStep = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.CheckBoxInternalHalve = new System.Windows.Forms.CheckBox();
            this.LabelInternalIslands = new System.Windows.Forms.Label();
            this.SliderInternalIslands = new System.Windows.Forms.TrackBar();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TextBoxLimitBiases = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CheckBoxMutateWeights = new System.Windows.Forms.CheckBox();
            this.CheckBoxMutateBiases = new System.Windows.Forms.CheckBox();
            this.LabelInternalCopy = new System.Windows.Forms.Label();
            this.LabelInternalMutationStrength = new System.Windows.Forms.Label();
            this.TextBoxLimitWeights = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.LabelInternalMutation = new System.Windows.Forms.Label();
            this.LabelInternalCrossover = new System.Windows.Forms.Label();
            this.SliderInternalCrossover = new System.Windows.Forms.TrackBar();
            this.SliderInternalMutationStrength = new System.Windows.Forms.TrackBar();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.SliderInternalMutation = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.SliderInternalCopy = new System.Windows.Forms.TrackBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.TabStructureInspector = new System.Windows.Forms.TabPage();
            this.GroupBoxStructType = new System.Windows.Forms.GroupBox();
            this.LabelStructTypeWeights = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.LabelStructTypeLayers = new System.Windows.Forms.Label();
            this.LabelStructTypeNeurons = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.RadioStructTestScore = new System.Windows.Forms.RadioButton();
            this.RadioStructLayer = new System.Windows.Forms.RadioButton();
            this.RadioStructNeurons = new System.Windows.Forms.RadioButton();
            this.RadioStructScore = new System.Windows.Forms.RadioButton();
            this.StructPlotPlaceHolder = new System.Windows.Forms.Panel();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.RadioStructIslandHistogram = new System.Windows.Forms.RadioButton();
            this.RadioStructIslandSeries = new System.Windows.Forms.RadioButton();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.RadioStructIslandTestScore = new System.Windows.Forms.RadioButton();
            this.RadioStructIslandLayers = new System.Windows.Forms.RadioButton();
            this.RadioStructIslandNeurons = new System.Windows.Forms.RadioButton();
            this.RadioStructIslandScore = new System.Windows.Forms.RadioButton();
            this.StructIslandPlotPlaceHolder = new System.Windows.Forms.Panel();
            this.GroupBoxGlobalStats = new System.Windows.Forms.GroupBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.LabelStatTestScore = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.LabelStatScore = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.LabelStatsStructIsland = new System.Windows.Forms.Label();
            this.LabelStructID = new System.Windows.Forms.Label();
            this.LabelIntIsland = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.ListViewStructures = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewStructIslands = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabInternalInspector = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.ButtonSaveSelected = new System.Windows.Forms.Button();
            this.CheckBoxLogStructOnly = new System.Windows.Forms.CheckBox();
            this.InternalPlotPlaceHolder = new System.Windows.Forms.Panel();
            this.ButtonLogSelected = new System.Windows.Forms.Button();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.RadioInternalTestScore = new System.Windows.Forms.RadioButton();
            this.RadioInternalScore = new System.Windows.Forms.RadioButton();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.RadioInternalHistogram = new System.Windows.Forms.RadioButton();
            this.RadioInternalSeries = new System.Windows.Forms.RadioButton();
            this.InternalIslandPlotPlaceHolder = new System.Windows.Forms.Panel();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.ListViewNets = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewInternalIslands = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabHandTester = new System.Windows.Forms.TabPage();
            this.LabelHandData = new System.Windows.Forms.Label();
            this.SliderHandData = new System.Windows.Forms.TrackBar();
            this.ButtonEvaluate = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.GroupBox();
            this.LabelMessage = new System.Windows.Forms.TextBox();
            this.LayoutOut = new System.Windows.Forms.TableLayoutPanel();
            this.LayoutIn = new System.Windows.Forms.TableLayoutPanel();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.MainTabControl.SuspendLayout();
            this.TabMainControl.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderDataToUse)).BeginInit();
            this.TabStructureGenetics.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureIslands)).BeginInit();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureCrossover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureMutationStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureMutation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureCopy)).BeginInit();
            this.TabInternalGenetics.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderAnnealing)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalIslands)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalCrossover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalMutationStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalMutation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalCopy)).BeginInit();
            this.TabStructureInspector.SuspendLayout();
            this.GroupBoxStructType.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.GroupBoxGlobalStats.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.TabInternalInspector.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.TabHandTester.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderHandData)).BeginInit();
            this.Message.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.TabMainControl);
            this.MainTabControl.Controls.Add(this.TabStructureGenetics);
            this.MainTabControl.Controls.Add(this.TabInternalGenetics);
            this.MainTabControl.Controls.Add(this.TabStructureInspector);
            this.MainTabControl.Controls.Add(this.TabInternalInspector);
            this.MainTabControl.Controls.Add(this.TabHandTester);
            this.MainTabControl.Location = new System.Drawing.Point(12, 12);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(1062, 694);
            this.MainTabControl.TabIndex = 0;
            this.MainTabControl.SelectedIndexChanged += new System.EventHandler(this.Tab_Changed_Handler);
            // 
            // TabMainControl
            // 
            this.TabMainControl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TabMainControl.Controls.Add(this.LabelGlobalStopwatchElapsed);
            this.TabMainControl.Controls.Add(this.groupBox9);
            this.TabMainControl.Controls.Add(this.groupBox6);
            this.TabMainControl.Controls.Add(this.CheckBoxLog);
            this.TabMainControl.Controls.Add(this.groupBox5);
            this.TabMainControl.Controls.Add(this.groupBox3);
            this.TabMainControl.Controls.Add(this.groupBox2);
            this.TabMainControl.Controls.Add(this.groupBox1);
            this.TabMainControl.Controls.Add(this.ButtonStartStop);
            this.TabMainControl.Controls.Add(this.Console);
            this.TabMainControl.Location = new System.Drawing.Point(4, 22);
            this.TabMainControl.Name = "TabMainControl";
            this.TabMainControl.Padding = new System.Windows.Forms.Padding(3);
            this.TabMainControl.Size = new System.Drawing.Size(1054, 668);
            this.TabMainControl.TabIndex = 0;
            this.TabMainControl.Text = "Main Control";
            // 
            // LabelGlobalStopwatchElapsed
            // 
            this.LabelGlobalStopwatchElapsed.AutoSize = true;
            this.LabelGlobalStopwatchElapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelGlobalStopwatchElapsed.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelGlobalStopwatchElapsed.Location = new System.Drawing.Point(242, 215);
            this.LabelGlobalStopwatchElapsed.Name = "LabelGlobalStopwatchElapsed";
            this.LabelGlobalStopwatchElapsed.Size = new System.Drawing.Size(0, 15);
            this.LabelGlobalStopwatchElapsed.TabIndex = 37;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.CheckBoxIncludeNetsInSave);
            this.groupBox9.Controls.Add(this.ButtonLoadState);
            this.groupBox9.Controls.Add(this.ButtonSaveState);
            this.groupBox9.Controls.Add(this.ButtonResetState);
            this.groupBox9.Location = new System.Drawing.Point(124, 139);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(191, 63);
            this.groupBox9.TabIndex = 31;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "State";
            // 
            // CheckBoxIncludeNetsInSave
            // 
            this.CheckBoxIncludeNetsInSave.AutoSize = true;
            this.CheckBoxIncludeNetsInSave.Location = new System.Drawing.Point(59, 42);
            this.CheckBoxIncludeNetsInSave.Name = "CheckBoxIncludeNetsInSave";
            this.CheckBoxIncludeNetsInSave.Size = new System.Drawing.Size(126, 17);
            this.CheckBoxIncludeNetsInSave.TabIndex = 37;
            this.CheckBoxIncludeNetsInSave.Text = "Include Nets In Save";
            this.CheckBoxIncludeNetsInSave.UseVisualStyleBackColor = true;
            this.CheckBoxIncludeNetsInSave.CheckedChanged += new System.EventHandler(this.CheckBoxIncludeNetsInSave_CheckedChanged);
            // 
            // ButtonLoadState
            // 
            this.ButtonLoadState.BackColor = System.Drawing.Color.Silver;
            this.ButtonLoadState.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ButtonLoadState.Location = new System.Drawing.Point(6, 15);
            this.ButtonLoadState.Name = "ButtonLoadState";
            this.ButtonLoadState.Size = new System.Drawing.Size(75, 21);
            this.ButtonLoadState.TabIndex = 39;
            this.ButtonLoadState.Text = "LOAD";
            this.ButtonLoadState.UseVisualStyleBackColor = false;
            this.ButtonLoadState.Click += new System.EventHandler(this.ButtonLoadState_Click);
            // 
            // ButtonSaveState
            // 
            this.ButtonSaveState.BackColor = System.Drawing.Color.Silver;
            this.ButtonSaveState.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ButtonSaveState.Location = new System.Drawing.Point(107, 15);
            this.ButtonSaveState.Name = "ButtonSaveState";
            this.ButtonSaveState.Size = new System.Drawing.Size(75, 21);
            this.ButtonSaveState.TabIndex = 37;
            this.ButtonSaveState.Text = "SAVE";
            this.ButtonSaveState.UseVisualStyleBackColor = false;
            this.ButtonSaveState.Click += new System.EventHandler(this.ButtonSaveState_Click);
            this.ButtonSaveState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonSaveState_MouseDown);
            this.ButtonSaveState.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonSaveState_MouseUp);
            // 
            // ButtonResetState
            // 
            this.ButtonResetState.BackColor = System.Drawing.Color.Yellow;
            this.ButtonResetState.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ButtonResetState.Location = new System.Drawing.Point(6, 38);
            this.ButtonResetState.Name = "ButtonResetState";
            this.ButtonResetState.Size = new System.Drawing.Size(53, 21);
            this.ButtonResetState.TabIndex = 38;
            this.ButtonResetState.Text = "RST";
            this.ButtonResetState.UseVisualStyleBackColor = false;
            this.ButtonResetState.Click += new System.EventHandler(this.ButtonResetState_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.Controls.Add(this.TextBoxStopCondTesting);
            this.groupBox6.Controls.Add(this.CheckBoxStopCondTesting);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.TextBoxStopCondTime);
            this.groupBox6.Controls.Add(this.CheckBoxStopCondTime);
            this.groupBox6.Controls.Add(this.TextBoxStopCondScore);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.CheckBoxStopCondScore);
            this.groupBox6.Location = new System.Drawing.Point(6, 377);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(309, 124);
            this.groupBox6.TabIndex = 36;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Stop Conditions";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(236, 50);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 13);
            this.label22.TabIndex = 35;
            this.label22.Text = "Generations";
            // 
            // TextBoxStopCondTesting
            // 
            this.TextBoxStopCondTesting.Location = new System.Drawing.Point(167, 46);
            this.TextBoxStopCondTesting.Name = "TextBoxStopCondTesting";
            this.TextBoxStopCondTesting.Size = new System.Drawing.Size(59, 20);
            this.TextBoxStopCondTesting.TabIndex = 34;
            this.TextBoxStopCondTesting.Text = "1";
            this.TextBoxStopCondTesting.Leave += new System.EventHandler(this.TextBoxStopCondTesting_TextChanged);
            // 
            // CheckBoxStopCondTesting
            // 
            this.CheckBoxStopCondTesting.AutoSize = true;
            this.CheckBoxStopCondTesting.Location = new System.Drawing.Point(9, 49);
            this.CheckBoxStopCondTesting.Name = "CheckBoxStopCondTesting";
            this.CheckBoxStopCondTesting.Size = new System.Drawing.Size(164, 17);
            this.CheckBoxStopCondTesting.TabIndex = 33;
            this.CheckBoxStopCondTesting.Text = "Testing Score Decreasing for";
            this.CheckBoxStopCondTesting.UseVisualStyleBackColor = true;
            this.CheckBoxStopCondTesting.CheckedChanged += new System.EventHandler(this.CheckBoxStopCondTesting_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(198, 99);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(32, 13);
            this.label19.TabIndex = 32;
            this.label19.Text = "Mins.";
            // 
            // TextBoxStopCondTime
            // 
            this.TextBoxStopCondTime.Location = new System.Drawing.Point(133, 95);
            this.TextBoxStopCondTime.Name = "TextBoxStopCondTime";
            this.TextBoxStopCondTime.Size = new System.Drawing.Size(59, 20);
            this.TextBoxStopCondTime.TabIndex = 31;
            this.TextBoxStopCondTime.Text = "1";
            this.TextBoxStopCondTime.Leave += new System.EventHandler(this.TextBoxStopCondTime_TextChanged);
            // 
            // CheckBoxStopCondTime
            // 
            this.CheckBoxStopCondTime.AutoSize = true;
            this.CheckBoxStopCondTime.Location = new System.Drawing.Point(9, 95);
            this.CheckBoxStopCondTime.Name = "CheckBoxStopCondTime";
            this.CheckBoxStopCondTime.Size = new System.Drawing.Size(49, 17);
            this.CheckBoxStopCondTime.TabIndex = 30;
            this.CheckBoxStopCondTime.Text = "Time";
            this.CheckBoxStopCondTime.UseVisualStyleBackColor = true;
            this.CheckBoxStopCondTime.CheckedChanged += new System.EventHandler(this.CheckBoxStopCondTime_CheckedChanged);
            // 
            // TextBoxStopCondScore
            // 
            this.TextBoxStopCondScore.Location = new System.Drawing.Point(133, 69);
            this.TextBoxStopCondScore.Name = "TextBoxStopCondScore";
            this.TextBoxStopCondScore.Size = new System.Drawing.Size(59, 20);
            this.TextBoxStopCondScore.TabIndex = 29;
            this.TextBoxStopCondScore.Text = "1";
            this.TextBoxStopCondScore.Leave += new System.EventHandler(this.TextBoxStopCondScore_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(22, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(267, 13);
            this.label20.TabIndex = 28;
            this.label20.Text = "- If none selected then Number of Generations applies- ";
            // 
            // CheckBoxStopCondScore
            // 
            this.CheckBoxStopCondScore.AutoSize = true;
            this.CheckBoxStopCondScore.Location = new System.Drawing.Point(9, 72);
            this.CheckBoxStopCondScore.Name = "CheckBoxStopCondScore";
            this.CheckBoxStopCondScore.Size = new System.Drawing.Size(54, 17);
            this.CheckBoxStopCondScore.TabIndex = 22;
            this.CheckBoxStopCondScore.Text = "Score";
            this.CheckBoxStopCondScore.UseVisualStyleBackColor = true;
            this.CheckBoxStopCondScore.CheckedChanged += new System.EventHandler(this.CheckBoxStopCondScore_CheckedChanged);
            // 
            // CheckBoxLog
            // 
            this.CheckBoxLog.AutoSize = true;
            this.CheckBoxLog.Location = new System.Drawing.Point(963, 6);
            this.CheckBoxLog.Name = "CheckBoxLog";
            this.CheckBoxLog.Size = new System.Drawing.Size(85, 17);
            this.CheckBoxLog.TabIndex = 32;
            this.CheckBoxLog.Text = "Log Console";
            this.CheckBoxLog.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.LabelCurrStructure);
            this.groupBox5.Controls.Add(this.LabelCurrStructGen);
            this.groupBox5.Controls.Add(this.TextBoxInternalPopulation);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.TextBoxInternalGenerations);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Controls.Add(this.LabelCurrInternalGen);
            this.groupBox5.Controls.Add(this.TextBoxStructurePopulation);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.TextBoxStructureGenerations);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Location = new System.Drawing.Point(6, 239);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(309, 132);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Evolutionary Params";
            // 
            // LabelCurrStructure
            // 
            this.LabelCurrStructure.AutoSize = true;
            this.LabelCurrStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelCurrStructure.ForeColor = System.Drawing.Color.Blue;
            this.LabelCurrStructure.Location = new System.Drawing.Point(219, 59);
            this.LabelCurrStructure.Name = "LabelCurrStructure";
            this.LabelCurrStructure.Size = new System.Drawing.Size(11, 13);
            this.LabelCurrStructure.TabIndex = 20;
            this.LabelCurrStructure.Text = "-";
            // 
            // LabelCurrStructGen
            // 
            this.LabelCurrStructGen.AutoSize = true;
            this.LabelCurrStructGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelCurrStructGen.ForeColor = System.Drawing.Color.Blue;
            this.LabelCurrStructGen.Location = new System.Drawing.Point(240, 37);
            this.LabelCurrStructGen.Name = "LabelCurrStructGen";
            this.LabelCurrStructGen.Size = new System.Drawing.Size(11, 13);
            this.LabelCurrStructGen.TabIndex = 18;
            this.LabelCurrStructGen.Text = "-";
            // 
            // TextBoxInternalPopulation
            // 
            this.TextBoxInternalPopulation.Location = new System.Drawing.Point(135, 106);
            this.TextBoxInternalPopulation.Name = "TextBoxInternalPopulation";
            this.TextBoxInternalPopulation.Size = new System.Drawing.Size(59, 20);
            this.TextBoxInternalPopulation.TabIndex = 6;
            this.TextBoxInternalPopulation.Text = "100";
            this.TextBoxInternalPopulation.Leave += new System.EventHandler(this.TextBoxInternalPopulation_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 113);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(95, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Internal Population";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(223, 16);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(41, 13);
            this.label32.TabIndex = 17;
            this.label32.Text = "Current";
            // 
            // TextBoxInternalGenerations
            // 
            this.TextBoxInternalGenerations.Location = new System.Drawing.Point(135, 80);
            this.TextBoxInternalGenerations.Name = "TextBoxInternalGenerations";
            this.TextBoxInternalGenerations.Size = new System.Drawing.Size(59, 20);
            this.TextBoxInternalGenerations.TabIndex = 14;
            this.TextBoxInternalGenerations.Text = "100";
            this.TextBoxInternalGenerations.Leave += new System.EventHandler(this.TextBoxInternalGenerations_TextChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(14, 89);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(105, 13);
            this.label30.TabIndex = 13;
            this.label30.Text = "Internal Generations:";
            // 
            // LabelCurrInternalGen
            // 
            this.LabelCurrInternalGen.AutoSize = true;
            this.LabelCurrInternalGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelCurrInternalGen.ForeColor = System.Drawing.Color.Blue;
            this.LabelCurrInternalGen.Location = new System.Drawing.Point(239, 85);
            this.LabelCurrInternalGen.Name = "LabelCurrInternalGen";
            this.LabelCurrInternalGen.Size = new System.Drawing.Size(11, 13);
            this.LabelCurrInternalGen.TabIndex = 16;
            this.LabelCurrInternalGen.Text = "-";
            // 
            // TextBoxStructurePopulation
            // 
            this.TextBoxStructurePopulation.Location = new System.Drawing.Point(135, 56);
            this.TextBoxStructurePopulation.Name = "TextBoxStructurePopulation";
            this.TextBoxStructurePopulation.Size = new System.Drawing.Size(59, 20);
            this.TextBoxStructurePopulation.TabIndex = 12;
            this.TextBoxStructurePopulation.Text = "100";
            this.TextBoxStructurePopulation.Leave += new System.EventHandler(this.TextBoxStructurePopulation_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(16, 59);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(103, 13);
            this.label29.TabIndex = 11;
            this.label29.Text = "Structure Population";
            // 
            // TextBoxStructureGenerations
            // 
            this.TextBoxStructureGenerations.Location = new System.Drawing.Point(135, 30);
            this.TextBoxStructureGenerations.Name = "TextBoxStructureGenerations";
            this.TextBoxStructureGenerations.Size = new System.Drawing.Size(59, 20);
            this.TextBoxStructureGenerations.TabIndex = 10;
            this.TextBoxStructureGenerations.Text = "1";
            this.TextBoxStructureGenerations.Leave += new System.EventHandler(this.TextBoxStructureGenerations_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(113, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Structure Generations:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TextBoxNetOutput);
            this.groupBox3.Controls.Add(this.TextBoxNetInput);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(6, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(112, 94);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Network";
            // 
            // TextBoxNetOutput
            // 
            this.TextBoxNetOutput.Location = new System.Drawing.Point(47, 57);
            this.TextBoxNetOutput.Name = "TextBoxNetOutput";
            this.TextBoxNetOutput.Size = new System.Drawing.Size(59, 20);
            this.TextBoxNetOutput.TabIndex = 4;
            this.TextBoxNetOutput.Leave += new System.EventHandler(this.TextBoxNetOutput_TextChanged);
            // 
            // TextBoxNetInput
            // 
            this.TextBoxNetInput.Location = new System.Drawing.Point(47, 21);
            this.TextBoxNetInput.Name = "TextBoxNetInput";
            this.TextBoxNetInput.Size = new System.Drawing.Size(59, 20);
            this.TextBoxNetInput.TabIndex = 3;
            this.TextBoxNetInput.Leave += new System.EventHandler(this.TextBoxNetInput_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Output:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Input:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LabelDynC);
            this.groupBox2.Controls.Add(this.LabelDynB);
            this.groupBox2.Controls.Add(this.LabelDynA);
            this.groupBox2.Controls.Add(this.TextBoxThreadsInParallel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CheckBoxParallelThreadsDS);
            this.groupBox2.Controls.Add(this.CheckBoxActivateThreading);
            this.groupBox2.Location = new System.Drawing.Point(6, 507);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 114);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Threading";
            // 
            // LabelDynC
            // 
            this.LabelDynC.AutoSize = true;
            this.LabelDynC.Enabled = false;
            this.LabelDynC.Location = new System.Drawing.Point(219, 85);
            this.LabelDynC.Name = "LabelDynC";
            this.LabelDynC.Size = new System.Drawing.Size(29, 13);
            this.LabelDynC.TabIndex = 9;
            this.LabelDynC.Text = "Mins";
            // 
            // LabelDynB
            // 
            this.LabelDynB.AutoSize = true;
            this.LabelDynB.Enabled = false;
            this.LabelDynB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelDynB.ForeColor = System.Drawing.Color.Maroon;
            this.LabelDynB.Location = new System.Drawing.Point(151, 85);
            this.LabelDynB.Name = "LabelDynB";
            this.LabelDynB.Size = new System.Drawing.Size(11, 13);
            this.LabelDynB.TabIndex = 8;
            this.LabelDynB.Text = "-";
            // 
            // LabelDynA
            // 
            this.LabelDynA.AutoSize = true;
            this.LabelDynA.Enabled = false;
            this.LabelDynA.Location = new System.Drawing.Point(9, 85);
            this.LabelDynA.Name = "LabelDynA";
            this.LabelDynA.Size = new System.Drawing.Size(142, 13);
            this.LabelDynA.TabIndex = 7;
            this.LabelDynA.Text = "One Struct generation Took ";
            // 
            // TextBoxThreadsInParallel
            // 
            this.TextBoxThreadsInParallel.Location = new System.Drawing.Point(133, 53);
            this.TextBoxThreadsInParallel.Name = "TextBoxThreadsInParallel";
            this.TextBoxThreadsInParallel.Size = new System.Drawing.Size(59, 20);
            this.TextBoxThreadsInParallel.TabIndex = 6;
            this.TextBoxThreadsInParallel.Leave += new System.EventHandler(this.TextBoxThreadsInParallel_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Max Threads in Parallel:";
            // 
            // CheckBoxParallelThreadsDS
            // 
            this.CheckBoxParallelThreadsDS.AutoSize = true;
            this.CheckBoxParallelThreadsDS.Location = new System.Drawing.Point(198, 55);
            this.CheckBoxParallelThreadsDS.Name = "CheckBoxParallelThreadsDS";
            this.CheckBoxParallelThreadsDS.Size = new System.Drawing.Size(104, 17);
            this.CheckBoxParallelThreadsDS.TabIndex = 3;
            this.CheckBoxParallelThreadsDS.Text = "Dynamic Search";
            this.CheckBoxParallelThreadsDS.UseVisualStyleBackColor = true;
            this.CheckBoxParallelThreadsDS.CheckedChanged += new System.EventHandler(this.CheckBoxParallelThreadsDS_CheckedChanged);
            // 
            // CheckBoxActivateThreading
            // 
            this.CheckBoxActivateThreading.AutoSize = true;
            this.CheckBoxActivateThreading.Location = new System.Drawing.Point(6, 23);
            this.CheckBoxActivateThreading.Name = "CheckBoxActivateThreading";
            this.CheckBoxActivateThreading.Size = new System.Drawing.Size(65, 17);
            this.CheckBoxActivateThreading.TabIndex = 2;
            this.CheckBoxActivateThreading.Text = "Activate";
            this.CheckBoxActivateThreading.UseVisualStyleBackColor = true;
            this.CheckBoxActivateThreading.CheckedChanged += new System.EventHandler(this.CheckBoxActivateThreading_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CheckBoxHalfForTesting);
            this.groupBox1.Controls.Add(this.SliderDataToUse);
            this.groupBox1.Controls.Add(this.LabelDataToUse);
            this.groupBox1.Controls.Add(this.LabelTotalData);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ButtonSelectData);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 125);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // CheckBoxHalfForTesting
            // 
            this.CheckBoxHalfForTesting.AutoSize = true;
            this.CheckBoxHalfForTesting.Location = new System.Drawing.Point(167, 93);
            this.CheckBoxHalfForTesting.Name = "CheckBoxHalfForTesting";
            this.CheckBoxHalfForTesting.Size = new System.Drawing.Size(118, 17);
            this.CheckBoxHalfForTesting.TabIndex = 23;
            this.CheckBoxHalfForTesting.Text = "Use half for Testing";
            this.CheckBoxHalfForTesting.UseVisualStyleBackColor = true;
            this.CheckBoxHalfForTesting.CheckedChanged += new System.EventHandler(this.CheckBoxHalfForTesting_CheckedChanged);
            // 
            // SliderDataToUse
            // 
            this.SliderDataToUse.Location = new System.Drawing.Point(41, 45);
            this.SliderDataToUse.Name = "SliderDataToUse";
            this.SliderDataToUse.Size = new System.Drawing.Size(193, 45);
            this.SliderDataToUse.TabIndex = 7;
            this.SliderDataToUse.Value = 10;
            this.SliderDataToUse.Scroll += new System.EventHandler(this.SliderDataToUse_Scroll);
            // 
            // LabelDataToUse
            // 
            this.LabelDataToUse.AutoSize = true;
            this.LabelDataToUse.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelDataToUse.Location = new System.Drawing.Point(240, 57);
            this.LabelDataToUse.Name = "LabelDataToUse";
            this.LabelDataToUse.Size = new System.Drawing.Size(10, 13);
            this.LabelDataToUse.TabIndex = 6;
            this.LabelDataToUse.Text = "-";
            // 
            // LabelTotalData
            // 
            this.LabelTotalData.AutoSize = true;
            this.LabelTotalData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelTotalData.ForeColor = System.Drawing.Color.Maroon;
            this.LabelTotalData.Location = new System.Drawing.Point(51, 93);
            this.LabelTotalData.Name = "LabelTotalData";
            this.LabelTotalData.Size = new System.Drawing.Size(11, 13);
            this.LabelTotalData.TabIndex = 5;
            this.LabelTotalData.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Data Inputs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Use ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Out of ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File:";
            // 
            // ButtonSelectData
            // 
            this.ButtonSelectData.Location = new System.Drawing.Point(38, 19);
            this.ButtonSelectData.Name = "ButtonSelectData";
            this.ButtonSelectData.Size = new System.Drawing.Size(265, 23);
            this.ButtonSelectData.TabIndex = 0;
            this.ButtonSelectData.Text = "Select Data File";
            this.ButtonSelectData.UseVisualStyleBackColor = true;
            this.ButtonSelectData.Click += new System.EventHandler(this.ButtonSelectData_Click);
            // 
            // ButtonStartStop
            // 
            this.ButtonStartStop.BackColor = System.Drawing.Color.Silver;
            this.ButtonStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ButtonStartStop.Location = new System.Drawing.Point(124, 210);
            this.ButtonStartStop.Name = "ButtonStartStop";
            this.ButtonStartStop.Size = new System.Drawing.Size(95, 23);
            this.ButtonStartStop.TabIndex = 28;
            this.ButtonStartStop.Text = "START";
            this.ButtonStartStop.UseVisualStyleBackColor = false;
            this.ButtonStartStop.Click += new System.EventHandler(this.ButtonStartStop_Click);
            this.ButtonStartStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonStartStop_MouseDown);
            this.ButtonStartStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonStartStop_MouseUp);
            // 
            // Console
            // 
            this.Console.BackColor = System.Drawing.SystemColors.InfoText;
            this.Console.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Console.ForeColor = System.Drawing.Color.Yellow;
            this.Console.Location = new System.Drawing.Point(321, 6);
            this.Console.Name = "Console";
            this.Console.Size = new System.Drawing.Size(727, 656);
            this.Console.TabIndex = 27;
            this.Console.Text = "";
            // 
            // TabStructureGenetics
            // 
            this.TabStructureGenetics.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TabStructureGenetics.Controls.Add(this.groupBox10);
            this.TabStructureGenetics.Controls.Add(this.groupBox11);
            this.TabStructureGenetics.Controls.Add(this.groupBox12);
            this.TabStructureGenetics.Location = new System.Drawing.Point(4, 22);
            this.TabStructureGenetics.Name = "TabStructureGenetics";
            this.TabStructureGenetics.Padding = new System.Windows.Forms.Padding(3);
            this.TabStructureGenetics.Size = new System.Drawing.Size(1054, 668);
            this.TabStructureGenetics.TabIndex = 2;
            this.TabStructureGenetics.Text = "Structure Genetics";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.LabelStructureHalveIn);
            this.groupBox10.Controls.Add(this.label50);
            this.groupBox10.Controls.Add(this.label51);
            this.groupBox10.Controls.Add(this.TextBoxStructureIslandsSteps);
            this.groupBox10.Controls.Add(this.label52);
            this.groupBox10.Controls.Add(this.CheckBoxStructureHalve);
            this.groupBox10.Controls.Add(this.LabelStructureIslands);
            this.groupBox10.Controls.Add(this.SliderStructureIslands);
            this.groupBox10.Controls.Add(this.label54);
            this.groupBox10.Location = new System.Drawing.Point(6, 305);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(309, 141);
            this.groupBox10.TabIndex = 36;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Islands";
            // 
            // LabelStructureHalveIn
            // 
            this.LabelStructureHalveIn.AutoSize = true;
            this.LabelStructureHalveIn.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructureHalveIn.Location = new System.Drawing.Point(72, 113);
            this.LabelStructureHalveIn.Name = "LabelStructureHalveIn";
            this.LabelStructureHalveIn.Size = new System.Drawing.Size(10, 13);
            this.LabelStructureHalveIn.TabIndex = 35;
            this.LabelStructureHalveIn.Text = "-";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(135, 113);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(64, 13);
            this.label50.TabIndex = 34;
            this.label50.Text = "Generations";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(17, 113);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(49, 13);
            this.label51.TabIndex = 33;
            this.label51.Text = "Halve in ";
            // 
            // TextBoxStructureIslandsSteps
            // 
            this.TextBoxStructureIslandsSteps.Location = new System.Drawing.Point(138, 79);
            this.TextBoxStructureIslandsSteps.Name = "TextBoxStructureIslandsSteps";
            this.TextBoxStructureIslandsSteps.Size = new System.Drawing.Size(59, 20);
            this.TextBoxStructureIslandsSteps.TabIndex = 32;
            this.TextBoxStructureIslandsSteps.Text = "1";
            this.TextBoxStructureIslandsSteps.Leave += new System.EventHandler(this.TextBoxStructureIslandsSteps_TextChanged);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(98, 82);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(34, 13);
            this.label52.TabIndex = 31;
            this.label52.Text = "Steps";
            // 
            // CheckBoxStructureHalve
            // 
            this.CheckBoxStructureHalve.AutoSize = true;
            this.CheckBoxStructureHalve.Location = new System.Drawing.Point(20, 82);
            this.CheckBoxStructureHalve.Name = "CheckBoxStructureHalve";
            this.CheckBoxStructureHalve.Size = new System.Drawing.Size(54, 17);
            this.CheckBoxStructureHalve.TabIndex = 30;
            this.CheckBoxStructureHalve.Text = "Halve";
            this.CheckBoxStructureHalve.UseVisualStyleBackColor = true;
            this.CheckBoxStructureHalve.CheckedChanged += new System.EventHandler(this.CheckBoxStructureHalve_CheckedChanged);
            // 
            // LabelStructureIslands
            // 
            this.LabelStructureIslands.AutoSize = true;
            this.LabelStructureIslands.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructureIslands.Location = new System.Drawing.Point(252, 32);
            this.LabelStructureIslands.Name = "LabelStructureIslands";
            this.LabelStructureIslands.Size = new System.Drawing.Size(10, 13);
            this.LabelStructureIslands.TabIndex = 29;
            this.LabelStructureIslands.Text = "-";
            // 
            // SliderStructureIslands
            // 
            this.SliderStructureIslands.LargeChange = 1;
            this.SliderStructureIslands.Location = new System.Drawing.Point(71, 28);
            this.SliderStructureIslands.Maximum = 5;
            this.SliderStructureIslands.Name = "SliderStructureIslands";
            this.SliderStructureIslands.Size = new System.Drawing.Size(169, 45);
            this.SliderStructureIslands.TabIndex = 28;
            this.SliderStructureIslands.Value = 1;
            this.SliderStructureIslands.Scroll += new System.EventHandler(this.SliderStructureIslands_Scroll);
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(11, 40);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(44, 13);
            this.label54.TabIndex = 27;
            this.label54.Text = "Number";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.TextBoxScoreConnections);
            this.groupBox11.Controls.Add(this.CheckBoxScoreConnections);
            this.groupBox11.Controls.Add(this.TextBoxScoreLayer);
            this.groupBox11.Controls.Add(this.TextBoxScoreNeurons);
            this.groupBox11.Controls.Add(this.CheckBoxScoreNeurons);
            this.groupBox11.Controls.Add(this.CheckBoxScoreLayers);
            this.groupBox11.Location = new System.Drawing.Point(6, 452);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(309, 103);
            this.groupBox11.TabIndex = 35;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Score Penalty";
            // 
            // TextBoxScoreConnections
            // 
            this.TextBoxScoreConnections.Location = new System.Drawing.Point(165, 71);
            this.TextBoxScoreConnections.Name = "TextBoxScoreConnections";
            this.TextBoxScoreConnections.Size = new System.Drawing.Size(59, 20);
            this.TextBoxScoreConnections.TabIndex = 33;
            this.TextBoxScoreConnections.Text = "1";
            this.TextBoxScoreConnections.Leave += new System.EventHandler(this.TextBoxScoreConnections_TextChanged);
            // 
            // CheckBoxScoreConnections
            // 
            this.CheckBoxScoreConnections.AutoSize = true;
            this.CheckBoxScoreConnections.Location = new System.Drawing.Point(14, 71);
            this.CheckBoxScoreConnections.Name = "CheckBoxScoreConnections";
            this.CheckBoxScoreConnections.Size = new System.Drawing.Size(137, 17);
            this.CheckBoxScoreConnections.TabIndex = 32;
            this.CheckBoxScoreConnections.Text = "Number of Connections";
            this.CheckBoxScoreConnections.UseVisualStyleBackColor = true;
            this.CheckBoxScoreConnections.CheckedChanged += new System.EventHandler(this.CheckBoxScoreConnections_CheckedChanged);
            // 
            // TextBoxScoreLayer
            // 
            this.TextBoxScoreLayer.Location = new System.Drawing.Point(165, 19);
            this.TextBoxScoreLayer.Name = "TextBoxScoreLayer";
            this.TextBoxScoreLayer.Size = new System.Drawing.Size(59, 20);
            this.TextBoxScoreLayer.TabIndex = 31;
            this.TextBoxScoreLayer.Text = "1";
            this.TextBoxScoreLayer.Leave += new System.EventHandler(this.TextBoxScoreLayer_TextChanged);
            // 
            // TextBoxScoreNeurons
            // 
            this.TextBoxScoreNeurons.Location = new System.Drawing.Point(165, 45);
            this.TextBoxScoreNeurons.Name = "TextBoxScoreNeurons";
            this.TextBoxScoreNeurons.Size = new System.Drawing.Size(59, 20);
            this.TextBoxScoreNeurons.TabIndex = 29;
            this.TextBoxScoreNeurons.Text = "1";
            this.TextBoxScoreNeurons.Leave += new System.EventHandler(this.TextBoxScoreNeurons_TextChanged);
            // 
            // CheckBoxScoreNeurons
            // 
            this.CheckBoxScoreNeurons.AutoSize = true;
            this.CheckBoxScoreNeurons.Location = new System.Drawing.Point(14, 48);
            this.CheckBoxScoreNeurons.Name = "CheckBoxScoreNeurons";
            this.CheckBoxScoreNeurons.Size = new System.Drawing.Size(118, 17);
            this.CheckBoxScoreNeurons.TabIndex = 22;
            this.CheckBoxScoreNeurons.Text = "Number of Neurons";
            this.CheckBoxScoreNeurons.UseVisualStyleBackColor = true;
            this.CheckBoxScoreNeurons.CheckedChanged += new System.EventHandler(this.CheckBoxScoreNeurons_CheckedChanged);
            // 
            // CheckBoxScoreLayers
            // 
            this.CheckBoxScoreLayers.AutoSize = true;
            this.CheckBoxScoreLayers.Location = new System.Drawing.Point(14, 22);
            this.CheckBoxScoreLayers.Name = "CheckBoxScoreLayers";
            this.CheckBoxScoreLayers.Size = new System.Drawing.Size(109, 17);
            this.CheckBoxScoreLayers.TabIndex = 30;
            this.CheckBoxScoreLayers.Text = "Number of Layers";
            this.CheckBoxScoreLayers.UseVisualStyleBackColor = true;
            this.CheckBoxScoreLayers.CheckedChanged += new System.EventHandler(this.CheckBoxScoreLayers_CheckedChanged);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.CheckBoxWeightsRandom);
            this.groupBox12.Controls.Add(this.CheckBoxBiasesRandom);
            this.groupBox12.Controls.Add(this.TextBoxConnectionCost);
            this.groupBox12.Controls.Add(this.label72);
            this.groupBox12.Controls.Add(this.TextBoxNeuronCost);
            this.groupBox12.Controls.Add(this.label71);
            this.groupBox12.Controls.Add(this.TextBoxLayerCost);
            this.groupBox12.Controls.Add(this.label70);
            this.groupBox12.Controls.Add(this.TextBoxFunctionCost);
            this.groupBox12.Controls.Add(this.label69);
            this.groupBox12.Controls.Add(this.LabelStructureCopy);
            this.groupBox12.Controls.Add(this.LabelStructureMutationStrength);
            this.groupBox12.Controls.Add(this.LabelStructureMutation);
            this.groupBox12.Controls.Add(this.LabelStructureCrossover);
            this.groupBox12.Controls.Add(this.SliderStructureCrossover);
            this.groupBox12.Controls.Add(this.SliderStructureMutationStrength);
            this.groupBox12.Controls.Add(this.label61);
            this.groupBox12.Controls.Add(this.label62);
            this.groupBox12.Controls.Add(this.label63);
            this.groupBox12.Controls.Add(this.SliderStructureMutation);
            this.groupBox12.Controls.Add(this.label64);
            this.groupBox12.Controls.Add(this.label65);
            this.groupBox12.Controls.Add(this.label66);
            this.groupBox12.Controls.Add(this.SliderStructureCopy);
            this.groupBox12.Controls.Add(this.label67);
            this.groupBox12.Controls.Add(this.label68);
            this.groupBox12.Location = new System.Drawing.Point(6, 6);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(309, 293);
            this.groupBox12.TabIndex = 34;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Main";
            // 
            // CheckBoxWeightsRandom
            // 
            this.CheckBoxWeightsRandom.AutoSize = true;
            this.CheckBoxWeightsRandom.Checked = true;
            this.CheckBoxWeightsRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxWeightsRandom.Location = new System.Drawing.Point(3, 219);
            this.CheckBoxWeightsRandom.Name = "CheckBoxWeightsRandom";
            this.CheckBoxWeightsRandom.Size = new System.Drawing.Size(152, 17);
            this.CheckBoxWeightsRandom.TabIndex = 37;
            this.CheckBoxWeightsRandom.Text = "Weights Start Randomized";
            this.CheckBoxWeightsRandom.UseVisualStyleBackColor = true;
            this.CheckBoxWeightsRandom.CheckedChanged += new System.EventHandler(this.CheckBoxWeightsRandom_CheckedChanged);
            // 
            // CheckBoxBiasesRandom
            // 
            this.CheckBoxBiasesRandom.AutoSize = true;
            this.CheckBoxBiasesRandom.Location = new System.Drawing.Point(156, 219);
            this.CheckBoxBiasesRandom.Name = "CheckBoxBiasesRandom";
            this.CheckBoxBiasesRandom.Size = new System.Drawing.Size(144, 17);
            this.CheckBoxBiasesRandom.TabIndex = 36;
            this.CheckBoxBiasesRandom.Text = "Biases Start Randomized";
            this.CheckBoxBiasesRandom.UseVisualStyleBackColor = true;
            this.CheckBoxBiasesRandom.CheckedChanged += new System.EventHandler(this.CheckBoxBiasesRandom_CheckedChanged);
            // 
            // TextBoxConnectionCost
            // 
            this.TextBoxConnectionCost.Location = new System.Drawing.Point(241, 176);
            this.TextBoxConnectionCost.Name = "TextBoxConnectionCost";
            this.TextBoxConnectionCost.Size = new System.Drawing.Size(59, 20);
            this.TextBoxConnectionCost.TabIndex = 35;
            this.TextBoxConnectionCost.Text = "1";
            this.TextBoxConnectionCost.Leave += new System.EventHandler(this.TextBoxConnectionCost_TextChanged);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(153, 180);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(85, 13);
            this.label72.TabIndex = 34;
            this.label72.Text = "Connection Cost";
            // 
            // TextBoxNeuronCost
            // 
            this.TextBoxNeuronCost.Location = new System.Drawing.Point(82, 177);
            this.TextBoxNeuronCost.Name = "TextBoxNeuronCost";
            this.TextBoxNeuronCost.Size = new System.Drawing.Size(59, 20);
            this.TextBoxNeuronCost.TabIndex = 33;
            this.TextBoxNeuronCost.Text = "0.1";
            this.TextBoxNeuronCost.Leave += new System.EventHandler(this.TextBoxNeuronCost_TextChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(3, 180);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(71, 13);
            this.label71.TabIndex = 32;
            this.label71.Text = "Neurons Cost";
            // 
            // TextBoxLayerCost
            // 
            this.TextBoxLayerCost.Location = new System.Drawing.Point(82, 150);
            this.TextBoxLayerCost.Name = "TextBoxLayerCost";
            this.TextBoxLayerCost.Size = new System.Drawing.Size(59, 20);
            this.TextBoxLayerCost.TabIndex = 31;
            this.TextBoxLayerCost.Text = "0.4";
            this.TextBoxLayerCost.Leave += new System.EventHandler(this.TextBoxLayerCost_TextChanged);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(6, 153);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(57, 13);
            this.label70.TabIndex = 30;
            this.label70.Text = "Layer Cost";
            // 
            // TextBoxFunctionCost
            // 
            this.TextBoxFunctionCost.Location = new System.Drawing.Point(241, 150);
            this.TextBoxFunctionCost.Name = "TextBoxFunctionCost";
            this.TextBoxFunctionCost.Size = new System.Drawing.Size(59, 20);
            this.TextBoxFunctionCost.TabIndex = 29;
            this.TextBoxFunctionCost.Text = "0.2";
            this.TextBoxFunctionCost.Leave += new System.EventHandler(this.TextBoxFunctionCost_TextChanged);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(162, 150);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(72, 13);
            this.label69.TabIndex = 28;
            this.label69.Text = "Function Cost";
            // 
            // LabelStructureCopy
            // 
            this.LabelStructureCopy.AutoSize = true;
            this.LabelStructureCopy.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructureCopy.Location = new System.Drawing.Point(246, 254);
            this.LabelStructureCopy.Name = "LabelStructureCopy";
            this.LabelStructureCopy.Size = new System.Drawing.Size(10, 13);
            this.LabelStructureCopy.TabIndex = 27;
            this.LabelStructureCopy.Text = "-";
            // 
            // LabelStructureMutationStrength
            // 
            this.LabelStructureMutationStrength.AutoSize = true;
            this.LabelStructureMutationStrength.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructureMutationStrength.Location = new System.Drawing.Point(246, 121);
            this.LabelStructureMutationStrength.Name = "LabelStructureMutationStrength";
            this.LabelStructureMutationStrength.Size = new System.Drawing.Size(10, 13);
            this.LabelStructureMutationStrength.TabIndex = 25;
            this.LabelStructureMutationStrength.Text = "-";
            // 
            // LabelStructureMutation
            // 
            this.LabelStructureMutation.AutoSize = true;
            this.LabelStructureMutation.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructureMutation.Location = new System.Drawing.Point(246, 70);
            this.LabelStructureMutation.Name = "LabelStructureMutation";
            this.LabelStructureMutation.Size = new System.Drawing.Size(10, 13);
            this.LabelStructureMutation.TabIndex = 24;
            this.LabelStructureMutation.Text = "-";
            // 
            // LabelStructureCrossover
            // 
            this.LabelStructureCrossover.AutoSize = true;
            this.LabelStructureCrossover.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructureCrossover.Location = new System.Drawing.Point(246, 29);
            this.LabelStructureCrossover.Name = "LabelStructureCrossover";
            this.LabelStructureCrossover.Size = new System.Drawing.Size(10, 13);
            this.LabelStructureCrossover.TabIndex = 23;
            this.LabelStructureCrossover.Text = "-";
            // 
            // SliderStructureCrossover
            // 
            this.SliderStructureCrossover.LargeChange = 20;
            this.SliderStructureCrossover.Location = new System.Drawing.Point(65, 19);
            this.SliderStructureCrossover.Maximum = 100;
            this.SliderStructureCrossover.Name = "SliderStructureCrossover";
            this.SliderStructureCrossover.Size = new System.Drawing.Size(169, 45);
            this.SliderStructureCrossover.SmallChange = 5;
            this.SliderStructureCrossover.TabIndex = 19;
            this.SliderStructureCrossover.Scroll += new System.EventHandler(this.SliderStructureCrossover_Scroll);
            // 
            // SliderStructureMutationStrength
            // 
            this.SliderStructureMutationStrength.LargeChange = 20;
            this.SliderStructureMutationStrength.Location = new System.Drawing.Point(82, 116);
            this.SliderStructureMutationStrength.Maximum = 100;
            this.SliderStructureMutationStrength.Name = "SliderStructureMutationStrength";
            this.SliderStructureMutationStrength.Size = new System.Drawing.Size(152, 45);
            this.SliderStructureMutationStrength.SmallChange = 10;
            this.SliderStructureMutationStrength.TabIndex = 22;
            this.SliderStructureMutationStrength.Value = 50;
            this.SliderStructureMutationStrength.Scroll += new System.EventHandler(this.SliderStructureMutationStrength_Scroll);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(234, 54);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(0, 13);
            this.label61.TabIndex = 18;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(5, 31);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(54, 13);
            this.label62.TabIndex = 17;
            this.label62.Text = "Crossover";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(5, 128);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(71, 13);
            this.label63.TabIndex = 21;
            this.label63.Text = "Mut. Strength";
            // 
            // SliderStructureMutation
            // 
            this.SliderStructureMutation.LargeChange = 20;
            this.SliderStructureMutation.Location = new System.Drawing.Point(65, 65);
            this.SliderStructureMutation.Maximum = 100;
            this.SliderStructureMutation.Name = "SliderStructureMutation";
            this.SliderStructureMutation.Size = new System.Drawing.Size(169, 45);
            this.SliderStructureMutation.SmallChange = 5;
            this.SliderStructureMutation.TabIndex = 15;
            this.SliderStructureMutation.Scroll += new System.EventHandler(this.SliderStructureMutation_Scroll);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(234, 184);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(0, 13);
            this.label64.TabIndex = 6;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(246, 179);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(0, 13);
            this.label65.TabIndex = 14;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(5, 77);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(48, 13);
            this.label66.TabIndex = 13;
            this.label66.Text = "Mutation";
            // 
            // SliderStructureCopy
            // 
            this.SliderStructureCopy.LargeChange = 20;
            this.SliderStructureCopy.Location = new System.Drawing.Point(65, 242);
            this.SliderStructureCopy.Maximum = 100;
            this.SliderStructureCopy.Name = "SliderStructureCopy";
            this.SliderStructureCopy.Size = new System.Drawing.Size(169, 45);
            this.SliderStructureCopy.SmallChange = 5;
            this.SliderStructureCopy.TabIndex = 11;
            this.SliderStructureCopy.Value = 100;
            this.SliderStructureCopy.Scroll += new System.EventHandler(this.SliderStructureCopy_Scroll);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(238, 100);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(0, 13);
            this.label67.TabIndex = 10;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(5, 254);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(31, 13);
            this.label68.TabIndex = 9;
            this.label68.Text = "Copy";
            // 
            // TabInternalGenetics
            // 
            this.TabInternalGenetics.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TabInternalGenetics.Controls.Add(this.groupBox23);
            this.TabInternalGenetics.Controls.Add(this.groupBox8);
            this.TabInternalGenetics.Controls.Add(this.groupBox7);
            this.TabInternalGenetics.Controls.Add(this.groupBox4);
            this.TabInternalGenetics.Location = new System.Drawing.Point(4, 22);
            this.TabInternalGenetics.Name = "TabInternalGenetics";
            this.TabInternalGenetics.Padding = new System.Windows.Forms.Padding(3);
            this.TabInternalGenetics.Size = new System.Drawing.Size(1054, 668);
            this.TabInternalGenetics.TabIndex = 1;
            this.TabInternalGenetics.Text = "Internal Genetics";
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.TextBoxRulesValidThreshold);
            this.groupBox23.Controls.Add(this.label24);
            this.groupBox23.Controls.Add(this.TextBoxRulesWinThreshold);
            this.groupBox23.Controls.Add(this.label23);
            this.groupBox23.Controls.Add(this.RadioRules1X2);
            this.groupBox23.Controls.Add(this.RadioRulesOutError);
            this.groupBox23.Location = new System.Drawing.Point(6, 513);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(309, 90);
            this.groupBox23.TabIndex = 36;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Score Rules";
            // 
            // TextBoxRulesValidThreshold
            // 
            this.TextBoxRulesValidThreshold.Location = new System.Drawing.Point(237, 64);
            this.TextBoxRulesValidThreshold.Name = "TextBoxRulesValidThreshold";
            this.TextBoxRulesValidThreshold.Size = new System.Drawing.Size(59, 20);
            this.TextBoxRulesValidThreshold.TabIndex = 38;
            this.TextBoxRulesValidThreshold.Text = "0.5";
            this.TextBoxRulesValidThreshold.Leave += new System.EventHandler(this.TextBoxRulesValidThreshold_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(105, 67);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 13);
            this.label24.TabIndex = 37;
            this.label24.Text = "Valid Threshold";
            // 
            // TextBoxRulesWinThreshold
            // 
            this.TextBoxRulesWinThreshold.Location = new System.Drawing.Point(237, 42);
            this.TextBoxRulesWinThreshold.Name = "TextBoxRulesWinThreshold";
            this.TextBoxRulesWinThreshold.Size = new System.Drawing.Size(59, 20);
            this.TextBoxRulesWinThreshold.TabIndex = 36;
            this.TextBoxRulesWinThreshold.Text = "0.5";
            this.TextBoxRulesWinThreshold.Leave += new System.EventHandler(this.TextBoxRulesWinThreshold_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(105, 45);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(123, 13);
            this.label23.TabIndex = 35;
            this.label23.Text = "Win predicted Threshold";
            // 
            // RadioRules1X2
            // 
            this.RadioRules1X2.AutoSize = true;
            this.RadioRules1X2.Enabled = false;
            this.RadioRules1X2.Location = new System.Drawing.Point(20, 42);
            this.RadioRules1X2.Name = "RadioRules1X2";
            this.RadioRules1X2.Size = new System.Drawing.Size(44, 17);
            this.RadioRules1X2.TabIndex = 34;
            this.RadioRules1X2.Text = "1X2";
            this.RadioRules1X2.UseVisualStyleBackColor = true;
            this.RadioRules1X2.CheckedChanged += new System.EventHandler(this.RadioRules1X2_CheckedChanged);
            // 
            // RadioRulesOutError
            // 
            this.RadioRulesOutError.AutoSize = true;
            this.RadioRulesOutError.Checked = true;
            this.RadioRulesOutError.Location = new System.Drawing.Point(20, 19);
            this.RadioRulesOutError.Name = "RadioRulesOutError";
            this.RadioRulesOutError.Size = new System.Drawing.Size(64, 17);
            this.RadioRulesOutError.TabIndex = 33;
            this.RadioRulesOutError.TabStop = true;
            this.RadioRulesOutError.Text = "OutError";
            this.RadioRulesOutError.UseVisualStyleBackColor = true;
            this.RadioRulesOutError.CheckedChanged += new System.EventHandler(this.RadioRulesOutError_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.TextBoxInternalAnnealStep);
            this.groupBox8.Controls.Add(this.LabelAnnealing);
            this.groupBox8.Controls.Add(this.CheckBoxAnealReduce);
            this.groupBox8.Controls.Add(this.SliderAnnealing);
            this.groupBox8.Controls.Add(this.label34);
            this.groupBox8.Controls.Add(this.label35);
            this.groupBox8.Location = new System.Drawing.Point(6, 287);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(309, 102);
            this.groupBox8.TabIndex = 33;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Annealing";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(89, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Steps";
            // 
            // TextBoxInternalAnnealStep
            // 
            this.TextBoxInternalAnnealStep.Location = new System.Drawing.Point(129, 67);
            this.TextBoxInternalAnnealStep.Name = "TextBoxInternalAnnealStep";
            this.TextBoxInternalAnnealStep.Size = new System.Drawing.Size(59, 20);
            this.TextBoxInternalAnnealStep.TabIndex = 33;
            this.TextBoxInternalAnnealStep.Text = "1";
            this.TextBoxInternalAnnealStep.Leave += new System.EventHandler(this.TextBoxInternalAnnealStep_TextChanged);
            // 
            // LabelAnnealing
            // 
            this.LabelAnnealing.AutoSize = true;
            this.LabelAnnealing.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelAnnealing.Location = new System.Drawing.Point(246, 23);
            this.LabelAnnealing.Name = "LabelAnnealing";
            this.LabelAnnealing.Size = new System.Drawing.Size(10, 13);
            this.LabelAnnealing.TabIndex = 26;
            this.LabelAnnealing.Text = "-";
            // 
            // CheckBoxAnealReduce
            // 
            this.CheckBoxAnealReduce.AutoSize = true;
            this.CheckBoxAnealReduce.Location = new System.Drawing.Point(11, 70);
            this.CheckBoxAnealReduce.Name = "CheckBoxAnealReduce";
            this.CheckBoxAnealReduce.Size = new System.Drawing.Size(64, 17);
            this.CheckBoxAnealReduce.TabIndex = 21;
            this.CheckBoxAnealReduce.Text = "Reduce";
            this.CheckBoxAnealReduce.UseVisualStyleBackColor = true;
            this.CheckBoxAnealReduce.CheckedChanged += new System.EventHandler(this.CheckBoxAnealReduce_CheckedChanged);
            // 
            // SliderAnnealing
            // 
            this.SliderAnnealing.LargeChange = 20;
            this.SliderAnnealing.Location = new System.Drawing.Point(65, 19);
            this.SliderAnnealing.Maximum = 100;
            this.SliderAnnealing.Name = "SliderAnnealing";
            this.SliderAnnealing.Size = new System.Drawing.Size(169, 45);
            this.SliderAnnealing.SmallChange = 5;
            this.SliderAnnealing.TabIndex = 19;
            this.SliderAnnealing.Scroll += new System.EventHandler(this.SliderAnnealing_Scroll);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(234, 54);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(0, 13);
            this.label34.TabIndex = 18;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(5, 31);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(43, 13);
            this.label35.TabIndex = 17;
            this.label35.Text = "Amount";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.TextBoxInternalIslandsStep);
            this.groupBox7.Controls.Add(this.label28);
            this.groupBox7.Controls.Add(this.CheckBoxInternalHalve);
            this.groupBox7.Controls.Add(this.LabelInternalIslands);
            this.groupBox7.Controls.Add(this.SliderInternalIslands);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Location = new System.Drawing.Point(6, 395);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(309, 112);
            this.groupBox7.TabIndex = 32;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Islands";
            // 
            // TextBoxInternalIslandsStep
            // 
            this.TextBoxInternalIslandsStep.Location = new System.Drawing.Point(138, 79);
            this.TextBoxInternalIslandsStep.Name = "TextBoxInternalIslandsStep";
            this.TextBoxInternalIslandsStep.Size = new System.Drawing.Size(59, 20);
            this.TextBoxInternalIslandsStep.TabIndex = 32;
            this.TextBoxInternalIslandsStep.Text = "1";
            this.TextBoxInternalIslandsStep.Leave += new System.EventHandler(this.TextBoxInternalIslandsStep_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(98, 82);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(34, 13);
            this.label28.TabIndex = 31;
            this.label28.Text = "Steps";
            // 
            // CheckBoxInternalHalve
            // 
            this.CheckBoxInternalHalve.AutoSize = true;
            this.CheckBoxInternalHalve.Location = new System.Drawing.Point(20, 82);
            this.CheckBoxInternalHalve.Name = "CheckBoxInternalHalve";
            this.CheckBoxInternalHalve.Size = new System.Drawing.Size(54, 17);
            this.CheckBoxInternalHalve.TabIndex = 30;
            this.CheckBoxInternalHalve.Text = "Halve";
            this.CheckBoxInternalHalve.UseVisualStyleBackColor = true;
            this.CheckBoxInternalHalve.CheckedChanged += new System.EventHandler(this.CheckBoxinternalHalve_CheckedChanged);
            // 
            // LabelInternalIslands
            // 
            this.LabelInternalIslands.AutoSize = true;
            this.LabelInternalIslands.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelInternalIslands.Location = new System.Drawing.Point(252, 32);
            this.LabelInternalIslands.Name = "LabelInternalIslands";
            this.LabelInternalIslands.Size = new System.Drawing.Size(10, 13);
            this.LabelInternalIslands.TabIndex = 29;
            this.LabelInternalIslands.Text = "-";
            // 
            // SliderInternalIslands
            // 
            this.SliderInternalIslands.LargeChange = 1;
            this.SliderInternalIslands.Location = new System.Drawing.Point(71, 28);
            this.SliderInternalIslands.Maximum = 5;
            this.SliderInternalIslands.Name = "SliderInternalIslands";
            this.SliderInternalIslands.Size = new System.Drawing.Size(169, 45);
            this.SliderInternalIslands.TabIndex = 28;
            this.SliderInternalIslands.Value = 1;
            this.SliderInternalIslands.Scroll += new System.EventHandler(this.SliderInternalIslands_Scroll);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(11, 40);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 13);
            this.label21.TabIndex = 27;
            this.label21.Text = "Number";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TextBoxLimitBiases);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.CheckBoxMutateWeights);
            this.groupBox4.Controls.Add(this.CheckBoxMutateBiases);
            this.groupBox4.Controls.Add(this.LabelInternalCopy);
            this.groupBox4.Controls.Add(this.LabelInternalMutationStrength);
            this.groupBox4.Controls.Add(this.TextBoxLimitWeights);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.LabelInternalMutation);
            this.groupBox4.Controls.Add(this.LabelInternalCrossover);
            this.groupBox4.Controls.Add(this.SliderInternalCrossover);
            this.groupBox4.Controls.Add(this.SliderInternalMutationStrength);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.SliderInternalMutation);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.SliderInternalCopy);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(309, 275);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Main";
            // 
            // TextBoxLimitBiases
            // 
            this.TextBoxLimitBiases.Location = new System.Drawing.Point(241, 139);
            this.TextBoxLimitBiases.Name = "TextBoxLimitBiases";
            this.TextBoxLimitBiases.Size = new System.Drawing.Size(33, 20);
            this.TextBoxLimitBiases.TabIndex = 31;
            this.TextBoxLimitBiases.Text = "10";
            this.TextBoxLimitBiases.Leave += new System.EventHandler(this.TextBoxLimitBiases_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(172, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Limit Biases";
            // 
            // CheckBoxMutateWeights
            // 
            this.CheckBoxMutateWeights.AutoSize = true;
            this.CheckBoxMutateWeights.Checked = true;
            this.CheckBoxMutateWeights.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxMutateWeights.Location = new System.Drawing.Point(6, 116);
            this.CheckBoxMutateWeights.Name = "CheckBoxMutateWeights";
            this.CheckBoxMutateWeights.Size = new System.Drawing.Size(101, 17);
            this.CheckBoxMutateWeights.TabIndex = 29;
            this.CheckBoxMutateWeights.Text = "Mutate Weights";
            this.CheckBoxMutateWeights.UseVisualStyleBackColor = true;
            this.CheckBoxMutateWeights.CheckedChanged += new System.EventHandler(this.CheckBoxMutateWeights_CheckedChanged);
            // 
            // CheckBoxMutateBiases
            // 
            this.CheckBoxMutateBiases.AutoSize = true;
            this.CheckBoxMutateBiases.Checked = true;
            this.CheckBoxMutateBiases.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxMutateBiases.Location = new System.Drawing.Point(159, 116);
            this.CheckBoxMutateBiases.Name = "CheckBoxMutateBiases";
            this.CheckBoxMutateBiases.Size = new System.Drawing.Size(93, 17);
            this.CheckBoxMutateBiases.TabIndex = 28;
            this.CheckBoxMutateBiases.Text = "Mutate Biases";
            this.CheckBoxMutateBiases.UseVisualStyleBackColor = true;
            this.CheckBoxMutateBiases.CheckedChanged += new System.EventHandler(this.CheckBoxMutateBiases_CheckedChanged);
            // 
            // LabelInternalCopy
            // 
            this.LabelInternalCopy.AutoSize = true;
            this.LabelInternalCopy.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelInternalCopy.Location = new System.Drawing.Point(246, 236);
            this.LabelInternalCopy.Name = "LabelInternalCopy";
            this.LabelInternalCopy.Size = new System.Drawing.Size(10, 13);
            this.LabelInternalCopy.TabIndex = 27;
            this.LabelInternalCopy.Text = "-";
            // 
            // LabelInternalMutationStrength
            // 
            this.LabelInternalMutationStrength.AutoSize = true;
            this.LabelInternalMutationStrength.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelInternalMutationStrength.Location = new System.Drawing.Point(246, 181);
            this.LabelInternalMutationStrength.Name = "LabelInternalMutationStrength";
            this.LabelInternalMutationStrength.Size = new System.Drawing.Size(10, 13);
            this.LabelInternalMutationStrength.TabIndex = 25;
            this.LabelInternalMutationStrength.Text = "-";
            // 
            // TextBoxLimitWeights
            // 
            this.TextBoxLimitWeights.Location = new System.Drawing.Point(82, 139);
            this.TextBoxLimitWeights.Name = "TextBoxLimitWeights";
            this.TextBoxLimitWeights.Size = new System.Drawing.Size(32, 20);
            this.TextBoxLimitWeights.TabIndex = 23;
            this.TextBoxLimitWeights.Text = "4";
            this.TextBoxLimitWeights.Leave += new System.EventHandler(this.TextBoxLimitWeights_TextChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 142);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(70, 13);
            this.label33.TabIndex = 22;
            this.label33.Text = "Limit Weights";
            // 
            // LabelInternalMutation
            // 
            this.LabelInternalMutation.AutoSize = true;
            this.LabelInternalMutation.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelInternalMutation.Location = new System.Drawing.Point(246, 70);
            this.LabelInternalMutation.Name = "LabelInternalMutation";
            this.LabelInternalMutation.Size = new System.Drawing.Size(10, 13);
            this.LabelInternalMutation.TabIndex = 24;
            this.LabelInternalMutation.Text = "-";
            // 
            // LabelInternalCrossover
            // 
            this.LabelInternalCrossover.AutoSize = true;
            this.LabelInternalCrossover.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelInternalCrossover.Location = new System.Drawing.Point(246, 29);
            this.LabelInternalCrossover.Name = "LabelInternalCrossover";
            this.LabelInternalCrossover.Size = new System.Drawing.Size(10, 13);
            this.LabelInternalCrossover.TabIndex = 23;
            this.LabelInternalCrossover.Text = "-";
            // 
            // SliderInternalCrossover
            // 
            this.SliderInternalCrossover.LargeChange = 20;
            this.SliderInternalCrossover.Location = new System.Drawing.Point(65, 19);
            this.SliderInternalCrossover.Maximum = 100;
            this.SliderInternalCrossover.Name = "SliderInternalCrossover";
            this.SliderInternalCrossover.Size = new System.Drawing.Size(169, 45);
            this.SliderInternalCrossover.SmallChange = 5;
            this.SliderInternalCrossover.TabIndex = 19;
            this.SliderInternalCrossover.Scroll += new System.EventHandler(this.SliderInternalCrossover_Scroll);
            // 
            // SliderInternalMutationStrength
            // 
            this.SliderInternalMutationStrength.LargeChange = 20;
            this.SliderInternalMutationStrength.Location = new System.Drawing.Point(82, 176);
            this.SliderInternalMutationStrength.Maximum = 100;
            this.SliderInternalMutationStrength.Name = "SliderInternalMutationStrength";
            this.SliderInternalMutationStrength.Size = new System.Drawing.Size(152, 45);
            this.SliderInternalMutationStrength.SmallChange = 10;
            this.SliderInternalMutationStrength.TabIndex = 22;
            this.SliderInternalMutationStrength.Value = 50;
            this.SliderInternalMutationStrength.Scroll += new System.EventHandler(this.SliderInternalMutationStrength_Scroll);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(234, 54);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(0, 13);
            this.label26.TabIndex = 18;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(5, 31);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(54, 13);
            this.label27.TabIndex = 17;
            this.label27.Text = "Crossover";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 188);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Mut. Strength";
            // 
            // SliderInternalMutation
            // 
            this.SliderInternalMutation.LargeChange = 20;
            this.SliderInternalMutation.Location = new System.Drawing.Point(65, 65);
            this.SliderInternalMutation.Maximum = 100;
            this.SliderInternalMutation.Name = "SliderInternalMutation";
            this.SliderInternalMutation.Size = new System.Drawing.Size(169, 45);
            this.SliderInternalMutation.SmallChange = 5;
            this.SliderInternalMutation.TabIndex = 15;
            this.SliderInternalMutation.Scroll += new System.EventHandler(this.SliderInternalMutation_Scroll);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(234, 224);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(246, 219);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 14;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(5, 77);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(48, 13);
            this.label25.TabIndex = 13;
            this.label25.Text = "Mutation";
            // 
            // SliderInternalCopy
            // 
            this.SliderInternalCopy.LargeChange = 20;
            this.SliderInternalCopy.Location = new System.Drawing.Point(65, 224);
            this.SliderInternalCopy.Maximum = 100;
            this.SliderInternalCopy.Name = "SliderInternalCopy";
            this.SliderInternalCopy.Size = new System.Drawing.Size(169, 45);
            this.SliderInternalCopy.SmallChange = 5;
            this.SliderInternalCopy.TabIndex = 11;
            this.SliderInternalCopy.Value = 100;
            this.SliderInternalCopy.Scroll += new System.EventHandler(this.SliderInternalCopy_Scroll);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(238, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 236);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Copy";
            // 
            // TabStructureInspector
            // 
            this.TabStructureInspector.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TabStructureInspector.Controls.Add(this.GroupBoxStructType);
            this.TabStructureInspector.Controls.Add(this.groupBox16);
            this.TabStructureInspector.Controls.Add(this.groupBox17);
            this.TabStructureInspector.Controls.Add(this.GroupBoxGlobalStats);
            this.TabStructureInspector.Controls.Add(this.label56);
            this.TabStructureInspector.Controls.Add(this.label55);
            this.TabStructureInspector.Controls.Add(this.ListViewStructures);
            this.TabStructureInspector.Controls.Add(this.ListViewStructIslands);
            this.TabStructureInspector.Location = new System.Drawing.Point(4, 22);
            this.TabStructureInspector.Name = "TabStructureInspector";
            this.TabStructureInspector.Padding = new System.Windows.Forms.Padding(3);
            this.TabStructureInspector.Size = new System.Drawing.Size(1054, 668);
            this.TabStructureInspector.TabIndex = 3;
            this.TabStructureInspector.Text = "Structure Inspector";
            // 
            // GroupBoxStructType
            // 
            this.GroupBoxStructType.Controls.Add(this.LabelStructTypeWeights);
            this.GroupBoxStructType.Controls.Add(this.label31);
            this.GroupBoxStructType.Controls.Add(this.LabelStructTypeLayers);
            this.GroupBoxStructType.Controls.Add(this.LabelStructTypeNeurons);
            this.GroupBoxStructType.Controls.Add(this.label93);
            this.GroupBoxStructType.Controls.Add(this.label95);
            this.GroupBoxStructType.Location = new System.Drawing.Point(301, 189);
            this.GroupBoxStructType.Name = "GroupBoxStructType";
            this.GroupBoxStructType.Size = new System.Drawing.Size(152, 98);
            this.GroupBoxStructType.TabIndex = 44;
            this.GroupBoxStructType.TabStop = false;
            this.GroupBoxStructType.Text = "Structure Type";
            // 
            // LabelStructTypeWeights
            // 
            this.LabelStructTypeWeights.AutoSize = true;
            this.LabelStructTypeWeights.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructTypeWeights.Location = new System.Drawing.Point(79, 73);
            this.LabelStructTypeWeights.Name = "LabelStructTypeWeights";
            this.LabelStructTypeWeights.Size = new System.Drawing.Size(10, 13);
            this.LabelStructTypeWeights.TabIndex = 36;
            this.LabelStructTypeWeights.Text = "-";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(24, 73);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(46, 13);
            this.label31.TabIndex = 35;
            this.label31.Text = "Weights";
            // 
            // LabelStructTypeLayers
            // 
            this.LabelStructTypeLayers.AutoSize = true;
            this.LabelStructTypeLayers.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructTypeLayers.Location = new System.Drawing.Point(79, 22);
            this.LabelStructTypeLayers.Name = "LabelStructTypeLayers";
            this.LabelStructTypeLayers.Size = new System.Drawing.Size(10, 13);
            this.LabelStructTypeLayers.TabIndex = 34;
            this.LabelStructTypeLayers.Text = "-";
            // 
            // LabelStructTypeNeurons
            // 
            this.LabelStructTypeNeurons.AutoSize = true;
            this.LabelStructTypeNeurons.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructTypeNeurons.Location = new System.Drawing.Point(79, 48);
            this.LabelStructTypeNeurons.Name = "LabelStructTypeNeurons";
            this.LabelStructTypeNeurons.Size = new System.Drawing.Size(10, 13);
            this.LabelStructTypeNeurons.TabIndex = 33;
            this.LabelStructTypeNeurons.Text = "-";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(24, 48);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(47, 13);
            this.label93.TabIndex = 30;
            this.label93.Text = "Neurons";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(33, 22);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(38, 13);
            this.label95.TabIndex = 28;
            this.label95.Text = "Layers";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.groupBox22);
            this.groupBox16.Controls.Add(this.StructPlotPlaceHolder);
            this.groupBox16.Location = new System.Drawing.Point(301, 293);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(747, 369);
            this.groupBox16.TabIndex = 40;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Structure Stats";
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.RadioStructTestScore);
            this.groupBox22.Controls.Add(this.RadioStructLayer);
            this.groupBox22.Controls.Add(this.RadioStructNeurons);
            this.groupBox22.Controls.Add(this.RadioStructScore);
            this.groupBox22.Location = new System.Drawing.Point(6, 19);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(129, 119);
            this.groupBox22.TabIndex = 2;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Plot Data";
            // 
            // RadioStructTestScore
            // 
            this.RadioStructTestScore.AutoSize = true;
            this.RadioStructTestScore.Location = new System.Drawing.Point(7, 44);
            this.RadioStructTestScore.Name = "RadioStructTestScore";
            this.RadioStructTestScore.Size = new System.Drawing.Size(77, 17);
            this.RadioStructTestScore.TabIndex = 4;
            this.RadioStructTestScore.Text = "Test Score";
            this.RadioStructTestScore.UseVisualStyleBackColor = true;
            this.RadioStructTestScore.CheckedChanged += new System.EventHandler(this.RadioStructTestScore_CheckedChanged);
            // 
            // RadioStructLayer
            // 
            this.RadioStructLayer.AutoSize = true;
            this.RadioStructLayer.Location = new System.Drawing.Point(7, 67);
            this.RadioStructLayer.Name = "RadioStructLayer";
            this.RadioStructLayer.Size = new System.Drawing.Size(56, 17);
            this.RadioStructLayer.TabIndex = 3;
            this.RadioStructLayer.Text = "Layers";
            this.RadioStructLayer.UseVisualStyleBackColor = true;
            this.RadioStructLayer.CheckedChanged += new System.EventHandler(this.RadioStructLayer_CheckedChanged);
            // 
            // RadioStructNeurons
            // 
            this.RadioStructNeurons.AutoSize = true;
            this.RadioStructNeurons.Location = new System.Drawing.Point(7, 89);
            this.RadioStructNeurons.Name = "RadioStructNeurons";
            this.RadioStructNeurons.Size = new System.Drawing.Size(65, 17);
            this.RadioStructNeurons.TabIndex = 1;
            this.RadioStructNeurons.Text = "Neurons";
            this.RadioStructNeurons.UseVisualStyleBackColor = true;
            this.RadioStructNeurons.CheckedChanged += new System.EventHandler(this.RadioStructNeurons_CheckedChanged);
            // 
            // RadioStructScore
            // 
            this.RadioStructScore.AutoSize = true;
            this.RadioStructScore.Checked = true;
            this.RadioStructScore.Location = new System.Drawing.Point(7, 20);
            this.RadioStructScore.Name = "RadioStructScore";
            this.RadioStructScore.Size = new System.Drawing.Size(53, 17);
            this.RadioStructScore.TabIndex = 0;
            this.RadioStructScore.TabStop = true;
            this.RadioStructScore.Text = "Score";
            this.RadioStructScore.UseVisualStyleBackColor = true;
            this.RadioStructScore.CheckedChanged += new System.EventHandler(this.RadioStructScore_CheckedChanged);
            // 
            // StructPlotPlaceHolder
            // 
            this.StructPlotPlaceHolder.Location = new System.Drawing.Point(152, 19);
            this.StructPlotPlaceHolder.Name = "StructPlotPlaceHolder";
            this.StructPlotPlaceHolder.Size = new System.Drawing.Size(589, 344);
            this.StructPlotPlaceHolder.TabIndex = 1;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.groupBox21);
            this.groupBox17.Controls.Add(this.groupBox20);
            this.groupBox17.Controls.Add(this.StructIslandPlotPlaceHolder);
            this.groupBox17.Location = new System.Drawing.Point(459, 6);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(589, 281);
            this.groupBox17.TabIndex = 39;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Island Stats";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.RadioStructIslandHistogram);
            this.groupBox21.Controls.Add(this.RadioStructIslandSeries);
            this.groupBox21.Location = new System.Drawing.Point(11, 135);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(129, 71);
            this.groupBox21.TabIndex = 4;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "PlotType";
            // 
            // RadioStructIslandHistogram
            // 
            this.RadioStructIslandHistogram.AutoSize = true;
            this.RadioStructIslandHistogram.Location = new System.Drawing.Point(7, 39);
            this.RadioStructIslandHistogram.Name = "RadioStructIslandHistogram";
            this.RadioStructIslandHistogram.Size = new System.Drawing.Size(72, 17);
            this.RadioStructIslandHistogram.TabIndex = 3;
            this.RadioStructIslandHistogram.TabStop = true;
            this.RadioStructIslandHistogram.Text = "Histogram";
            this.RadioStructIslandHistogram.UseVisualStyleBackColor = true;
            this.RadioStructIslandHistogram.CheckedChanged += new System.EventHandler(this.RadioStructIslandHistogram_CheckedChanged);
            // 
            // RadioStructIslandSeries
            // 
            this.RadioStructIslandSeries.AutoSize = true;
            this.RadioStructIslandSeries.Checked = true;
            this.RadioStructIslandSeries.Location = new System.Drawing.Point(7, 20);
            this.RadioStructIslandSeries.Name = "RadioStructIslandSeries";
            this.RadioStructIslandSeries.Size = new System.Drawing.Size(54, 17);
            this.RadioStructIslandSeries.TabIndex = 0;
            this.RadioStructIslandSeries.TabStop = true;
            this.RadioStructIslandSeries.Text = "Series";
            this.RadioStructIslandSeries.UseVisualStyleBackColor = true;
            this.RadioStructIslandSeries.CheckedChanged += new System.EventHandler(this.RadioStructIslandSeries_CheckedChanged);
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.RadioStructIslandTestScore);
            this.groupBox20.Controls.Add(this.RadioStructIslandLayers);
            this.groupBox20.Controls.Add(this.RadioStructIslandNeurons);
            this.groupBox20.Controls.Add(this.RadioStructIslandScore);
            this.groupBox20.Location = new System.Drawing.Point(11, 19);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(129, 110);
            this.groupBox20.TabIndex = 1;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Plot Data";
            // 
            // RadioStructIslandTestScore
            // 
            this.RadioStructIslandTestScore.AutoSize = true;
            this.RadioStructIslandTestScore.Location = new System.Drawing.Point(6, 41);
            this.RadioStructIslandTestScore.Name = "RadioStructIslandTestScore";
            this.RadioStructIslandTestScore.Size = new System.Drawing.Size(77, 17);
            this.RadioStructIslandTestScore.TabIndex = 4;
            this.RadioStructIslandTestScore.TabStop = true;
            this.RadioStructIslandTestScore.Text = "Test Score";
            this.RadioStructIslandTestScore.UseVisualStyleBackColor = true;
            this.RadioStructIslandTestScore.CheckedChanged += new System.EventHandler(this.RadioStructIslandTestScore_CheckedChanged);
            // 
            // RadioStructIslandLayers
            // 
            this.RadioStructIslandLayers.AutoSize = true;
            this.RadioStructIslandLayers.Location = new System.Drawing.Point(6, 61);
            this.RadioStructIslandLayers.Name = "RadioStructIslandLayers";
            this.RadioStructIslandLayers.Size = new System.Drawing.Size(56, 17);
            this.RadioStructIslandLayers.TabIndex = 3;
            this.RadioStructIslandLayers.TabStop = true;
            this.RadioStructIslandLayers.Text = "Layers";
            this.RadioStructIslandLayers.UseVisualStyleBackColor = true;
            this.RadioStructIslandLayers.CheckedChanged += new System.EventHandler(this.RadioStructIslandLayers_CheckedChanged);
            // 
            // RadioStructIslandNeurons
            // 
            this.RadioStructIslandNeurons.AutoSize = true;
            this.RadioStructIslandNeurons.Location = new System.Drawing.Point(6, 83);
            this.RadioStructIslandNeurons.Name = "RadioStructIslandNeurons";
            this.RadioStructIslandNeurons.Size = new System.Drawing.Size(65, 17);
            this.RadioStructIslandNeurons.TabIndex = 1;
            this.RadioStructIslandNeurons.TabStop = true;
            this.RadioStructIslandNeurons.Text = "Neurons";
            this.RadioStructIslandNeurons.UseVisualStyleBackColor = true;
            this.RadioStructIslandNeurons.CheckedChanged += new System.EventHandler(this.RadioStructIslandNeurons_CheckedChanged);
            // 
            // RadioStructIslandScore
            // 
            this.RadioStructIslandScore.AutoSize = true;
            this.RadioStructIslandScore.Checked = true;
            this.RadioStructIslandScore.Location = new System.Drawing.Point(7, 20);
            this.RadioStructIslandScore.Name = "RadioStructIslandScore";
            this.RadioStructIslandScore.Size = new System.Drawing.Size(53, 17);
            this.RadioStructIslandScore.TabIndex = 0;
            this.RadioStructIslandScore.TabStop = true;
            this.RadioStructIslandScore.Text = "Score";
            this.RadioStructIslandScore.UseVisualStyleBackColor = true;
            this.RadioStructIslandScore.CheckedChanged += new System.EventHandler(this.RadioStructIslandScore_CheckedChanged);
            // 
            // StructIslandPlotPlaceHolder
            // 
            this.StructIslandPlotPlaceHolder.BackColor = System.Drawing.Color.White;
            this.StructIslandPlotPlaceHolder.Location = new System.Drawing.Point(150, 13);
            this.StructIslandPlotPlaceHolder.Name = "StructIslandPlotPlaceHolder";
            this.StructIslandPlotPlaceHolder.Size = new System.Drawing.Size(433, 262);
            this.StructIslandPlotPlaceHolder.TabIndex = 0;
            // 
            // GroupBoxGlobalStats
            // 
            this.GroupBoxGlobalStats.Controls.Add(this.groupBox19);
            this.GroupBoxGlobalStats.Location = new System.Drawing.Point(133, 6);
            this.GroupBoxGlobalStats.Name = "GroupBoxGlobalStats";
            this.GroupBoxGlobalStats.Size = new System.Drawing.Size(320, 177);
            this.GroupBoxGlobalStats.TabIndex = 38;
            this.GroupBoxGlobalStats.TabStop = false;
            this.GroupBoxGlobalStats.Text = "Global Stats";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.LabelStatTestScore);
            this.groupBox19.Controls.Add(this.label16);
            this.groupBox19.Controls.Add(this.LabelStatScore);
            this.groupBox19.Controls.Add(this.label84);
            this.groupBox19.Controls.Add(this.LabelStatsStructIsland);
            this.groupBox19.Controls.Add(this.LabelStructID);
            this.groupBox19.Controls.Add(this.LabelIntIsland);
            this.groupBox19.Controls.Add(this.label78);
            this.groupBox19.Controls.Add(this.label75);
            this.groupBox19.Controls.Add(this.label77);
            this.groupBox19.Location = new System.Drawing.Point(6, 16);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(156, 155);
            this.groupBox19.TabIndex = 0;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Best Net";
            // 
            // LabelStatTestScore
            // 
            this.LabelStatTestScore.AutoSize = true;
            this.LabelStatTestScore.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStatTestScore.Location = new System.Drawing.Point(79, 119);
            this.LabelStatTestScore.Name = "LabelStatTestScore";
            this.LabelStatTestScore.Size = new System.Drawing.Size(10, 13);
            this.LabelStatTestScore.TabIndex = 38;
            this.LabelStatTestScore.Text = "-";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 119);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 37;
            this.label16.Text = "Test Score";
            // 
            // LabelStatScore
            // 
            this.LabelStatScore.AutoSize = true;
            this.LabelStatScore.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStatScore.Location = new System.Drawing.Point(79, 100);
            this.LabelStatScore.Name = "LabelStatScore";
            this.LabelStatScore.Size = new System.Drawing.Size(10, 13);
            this.LabelStatScore.TabIndex = 36;
            this.LabelStatScore.Text = "-";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(38, 100);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(35, 13);
            this.label84.TabIndex = 35;
            this.label84.Text = "Score";
            // 
            // LabelStatsStructIsland
            // 
            this.LabelStatsStructIsland.AutoSize = true;
            this.LabelStatsStructIsland.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStatsStructIsland.Location = new System.Drawing.Point(79, 22);
            this.LabelStatsStructIsland.Name = "LabelStatsStructIsland";
            this.LabelStatsStructIsland.Size = new System.Drawing.Size(10, 13);
            this.LabelStatsStructIsland.TabIndex = 34;
            this.LabelStatsStructIsland.Text = "-";
            // 
            // LabelStructID
            // 
            this.LabelStructID.AutoSize = true;
            this.LabelStructID.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelStructID.Location = new System.Drawing.Point(79, 48);
            this.LabelStructID.Name = "LabelStructID";
            this.LabelStructID.Size = new System.Drawing.Size(10, 13);
            this.LabelStructID.TabIndex = 33;
            this.LabelStructID.Text = "-";
            // 
            // LabelIntIsland
            // 
            this.LabelIntIsland.AutoSize = true;
            this.LabelIntIsland.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabelIntIsland.Location = new System.Drawing.Point(79, 74);
            this.LabelIntIsland.Name = "LabelIntIsland";
            this.LabelIntIsland.Size = new System.Drawing.Size(10, 13);
            this.LabelIntIsland.TabIndex = 31;
            this.LabelIntIsland.Text = "-";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(20, 48);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(58, 13);
            this.label78.TabIndex = 30;
            this.label78.Text = "Struct Indx";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(23, 74);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(50, 13);
            this.label75.TabIndex = 24;
            this.label75.Text = "Int Island";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(12, 22);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(66, 13);
            this.label77.TabIndex = 28;
            this.label77.Text = "Struct Island";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label56.Location = new System.Drawing.Point(35, 170);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(81, 13);
            this.label56.TabIndex = 30;
            this.label56.Text = "STRUCTURES";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label55.Location = new System.Drawing.Point(35, 3);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(53, 13);
            this.label55.TabIndex = 29;
            this.label55.Text = "ISLANDS";
            // 
            // ListViewStructures
            // 
            this.ListViewStructures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ListViewStructures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader5,
            this.columnHeader9,
            this.columnHeader10});
            this.ListViewStructures.FullRowSelect = true;
            this.ListViewStructures.HideSelection = false;
            this.ListViewStructures.Location = new System.Drawing.Point(3, 186);
            this.ListViewStructures.MultiSelect = false;
            this.ListViewStructures.Name = "ListViewStructures";
            this.ListViewStructures.Size = new System.Drawing.Size(292, 476);
            this.ListViewStructures.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListViewStructures.TabIndex = 3;
            this.ListViewStructures.UseCompatibleStateImageBehavior = false;
            this.ListViewStructures.View = System.Windows.Forms.View.Details;
            this.ListViewStructures.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewStructures_ColumnClicked);
            this.ListViewStructures.SelectedIndexChanged += new System.EventHandler(this.ListViewStructures_SelectedIndexChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Indx";
            this.columnHeader7.Width = 47;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Score";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "TestScore";
            this.columnHeader5.Width = 66;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Layers";
            this.columnHeader9.Width = 52;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Neurons";
            // 
            // ListViewStructIslands
            // 
            this.ListViewStructIslands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ListViewStructIslands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ListViewStructIslands.FullRowSelect = true;
            this.ListViewStructIslands.HideSelection = false;
            this.ListViewStructIslands.Location = new System.Drawing.Point(6, 19);
            this.ListViewStructIslands.MultiSelect = false;
            this.ListViewStructIslands.Name = "ListViewStructIslands";
            this.ListViewStructIslands.Size = new System.Drawing.Size(121, 148);
            this.ListViewStructIslands.TabIndex = 0;
            this.ListViewStructIslands.UseCompatibleStateImageBehavior = false;
            this.ListViewStructIslands.View = System.Windows.Forms.View.Details;
            this.ListViewStructIslands.SelectedIndexChanged += new System.EventHandler(this.ListViewStructIslands_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Indx";
            this.columnHeader1.Width = 42;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Score";
            // 
            // TabInternalInspector
            // 
            this.TabInternalInspector.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TabInternalInspector.Controls.Add(this.groupBox13);
            this.TabInternalInspector.Controls.Add(this.groupBox15);
            this.TabInternalInspector.Controls.Add(this.label73);
            this.TabInternalInspector.Controls.Add(this.label74);
            this.TabInternalInspector.Controls.Add(this.ListViewNets);
            this.TabInternalInspector.Controls.Add(this.ListViewInternalIslands);
            this.TabInternalInspector.Location = new System.Drawing.Point(4, 22);
            this.TabInternalInspector.Name = "TabInternalInspector";
            this.TabInternalInspector.Padding = new System.Windows.Forms.Padding(3);
            this.TabInternalInspector.Size = new System.Drawing.Size(1054, 668);
            this.TabInternalInspector.TabIndex = 4;
            this.TabInternalInspector.Text = "Internal Inspector";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.ButtonSaveSelected);
            this.groupBox13.Controls.Add(this.CheckBoxLogStructOnly);
            this.groupBox13.Controls.Add(this.InternalPlotPlaceHolder);
            this.groupBox13.Controls.Add(this.ButtonLogSelected);
            this.groupBox13.Location = new System.Drawing.Point(242, 293);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(806, 369);
            this.groupBox13.TabIndex = 42;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Net Stats";
            // 
            // ButtonSaveSelected
            // 
            this.ButtonSaveSelected.Location = new System.Drawing.Point(6, 81);
            this.ButtonSaveSelected.Name = "ButtonSaveSelected";
            this.ButtonSaveSelected.Size = new System.Drawing.Size(92, 23);
            this.ButtonSaveSelected.TabIndex = 2;
            this.ButtonSaveSelected.Text = "Save Selected";
            this.ButtonSaveSelected.UseVisualStyleBackColor = true;
            this.ButtonSaveSelected.Click += new System.EventHandler(this.ButtonSaveSelected_Click);
            // 
            // CheckBoxLogStructOnly
            // 
            this.CheckBoxLogStructOnly.AutoSize = true;
            this.CheckBoxLogStructOnly.Checked = true;
            this.CheckBoxLogStructOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxLogStructOnly.Location = new System.Drawing.Point(6, 48);
            this.CheckBoxLogStructOnly.Name = "CheckBoxLogStructOnly";
            this.CheckBoxLogStructOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CheckBoxLogStructOnly.Size = new System.Drawing.Size(108, 17);
            this.CheckBoxLogStructOnly.TabIndex = 1;
            this.CheckBoxLogStructOnly.Text = "LogStructureOnly";
            this.CheckBoxLogStructOnly.UseVisualStyleBackColor = true;
            // 
            // InternalPlotPlaceHolder
            // 
            this.InternalPlotPlaceHolder.Location = new System.Drawing.Point(120, 19);
            this.InternalPlotPlaceHolder.Name = "InternalPlotPlaceHolder";
            this.InternalPlotPlaceHolder.Size = new System.Drawing.Size(680, 344);
            this.InternalPlotPlaceHolder.TabIndex = 1;
            // 
            // ButtonLogSelected
            // 
            this.ButtonLogSelected.Location = new System.Drawing.Point(6, 19);
            this.ButtonLogSelected.Name = "ButtonLogSelected";
            this.ButtonLogSelected.Size = new System.Drawing.Size(92, 23);
            this.ButtonLogSelected.TabIndex = 0;
            this.ButtonLogSelected.Text = "Log Selected";
            this.ButtonLogSelected.UseVisualStyleBackColor = true;
            this.ButtonLogSelected.Click += new System.EventHandler(this.ButtonLogSelected_Click);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.groupBox14);
            this.groupBox15.Controls.Add(this.groupBox24);
            this.groupBox15.Controls.Add(this.InternalIslandPlotPlaceHolder);
            this.groupBox15.Location = new System.Drawing.Point(459, 6);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(589, 281);
            this.groupBox15.TabIndex = 41;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Island Stats";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.RadioInternalTestScore);
            this.groupBox14.Controls.Add(this.RadioInternalScore);
            this.groupBox14.Location = new System.Drawing.Point(6, 19);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(129, 64);
            this.groupBox14.TabIndex = 43;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Plot Data";
            // 
            // RadioInternalTestScore
            // 
            this.RadioInternalTestScore.AutoSize = true;
            this.RadioInternalTestScore.Location = new System.Drawing.Point(6, 41);
            this.RadioInternalTestScore.Name = "RadioInternalTestScore";
            this.RadioInternalTestScore.Size = new System.Drawing.Size(77, 17);
            this.RadioInternalTestScore.TabIndex = 4;
            this.RadioInternalTestScore.TabStop = true;
            this.RadioInternalTestScore.Text = "Test Score";
            this.RadioInternalTestScore.UseVisualStyleBackColor = true;
            this.RadioInternalTestScore.CheckedChanged += new System.EventHandler(this.RadioInternalTestScore_CheckedChanged);
            // 
            // RadioInternalScore
            // 
            this.RadioInternalScore.AutoSize = true;
            this.RadioInternalScore.Checked = true;
            this.RadioInternalScore.Location = new System.Drawing.Point(7, 20);
            this.RadioInternalScore.Name = "RadioInternalScore";
            this.RadioInternalScore.Size = new System.Drawing.Size(53, 17);
            this.RadioInternalScore.TabIndex = 0;
            this.RadioInternalScore.TabStop = true;
            this.RadioInternalScore.Text = "Score";
            this.RadioInternalScore.UseVisualStyleBackColor = true;
            this.RadioInternalScore.CheckedChanged += new System.EventHandler(this.RadioInternalScore_CheckedChanged);
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.RadioInternalHistogram);
            this.groupBox24.Controls.Add(this.RadioInternalSeries);
            this.groupBox24.Location = new System.Drawing.Point(6, 90);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(129, 71);
            this.groupBox24.TabIndex = 4;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "PlotType";
            // 
            // RadioInternalHistogram
            // 
            this.RadioInternalHistogram.AutoSize = true;
            this.RadioInternalHistogram.Location = new System.Drawing.Point(7, 39);
            this.RadioInternalHistogram.Name = "RadioInternalHistogram";
            this.RadioInternalHistogram.Size = new System.Drawing.Size(72, 17);
            this.RadioInternalHistogram.TabIndex = 3;
            this.RadioInternalHistogram.TabStop = true;
            this.RadioInternalHistogram.Text = "Histogram";
            this.RadioInternalHistogram.UseVisualStyleBackColor = true;
            this.RadioInternalHistogram.CheckedChanged += new System.EventHandler(this.RadioInternalHistogram_CheckedChanged);
            // 
            // RadioInternalSeries
            // 
            this.RadioInternalSeries.AutoSize = true;
            this.RadioInternalSeries.Checked = true;
            this.RadioInternalSeries.Location = new System.Drawing.Point(7, 20);
            this.RadioInternalSeries.Name = "RadioInternalSeries";
            this.RadioInternalSeries.Size = new System.Drawing.Size(54, 17);
            this.RadioInternalSeries.TabIndex = 0;
            this.RadioInternalSeries.TabStop = true;
            this.RadioInternalSeries.Text = "Series";
            this.RadioInternalSeries.UseVisualStyleBackColor = true;
            this.RadioInternalSeries.CheckedChanged += new System.EventHandler(this.RadioInternalSeries_CheckedChanged);
            // 
            // InternalIslandPlotPlaceHolder
            // 
            this.InternalIslandPlotPlaceHolder.Location = new System.Drawing.Point(141, 13);
            this.InternalIslandPlotPlaceHolder.Name = "InternalIslandPlotPlaceHolder";
            this.InternalIslandPlotPlaceHolder.Size = new System.Drawing.Size(442, 262);
            this.InternalIslandPlotPlaceHolder.TabIndex = 0;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label73.Location = new System.Drawing.Point(35, 170);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(36, 13);
            this.label73.TabIndex = 34;
            this.label73.Text = "NETS";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label74.Location = new System.Drawing.Point(35, 3);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(53, 13);
            this.label74.TabIndex = 33;
            this.label74.Text = "ISLANDS";
            // 
            // ListViewNets
            // 
            this.ListViewNets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ListViewNets.CheckBoxes = true;
            this.ListViewNets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader14,
            this.columnHeader6,
            this.columnHeader13});
            this.ListViewNets.FullRowSelect = true;
            this.ListViewNets.HideSelection = false;
            this.ListViewNets.Location = new System.Drawing.Point(6, 186);
            this.ListViewNets.Name = "ListViewNets";
            this.ListViewNets.Size = new System.Drawing.Size(230, 476);
            this.ListViewNets.TabIndex = 32;
            this.ListViewNets.UseCompatibleStateImageBehavior = false;
            this.ListViewNets.View = System.Windows.Forms.View.Details;
            this.ListViewNets.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewNets_ItemChecked);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Indx";
            this.columnHeader3.Width = 44;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Score";
            this.columnHeader4.Width = 57;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "TestScore";
            this.columnHeader14.Width = 63;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "OutErr";
            this.columnHeader6.Width = 50;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "TestOutErr";
            this.columnHeader13.Width = 67;
            // 
            // ListViewInternalIslands
            // 
            this.ListViewInternalIslands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ListViewInternalIslands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.ListViewInternalIslands.FullRowSelect = true;
            this.ListViewInternalIslands.HideSelection = false;
            this.ListViewInternalIslands.Location = new System.Drawing.Point(6, 19);
            this.ListViewInternalIslands.MultiSelect = false;
            this.ListViewInternalIslands.Name = "ListViewInternalIslands";
            this.ListViewInternalIslands.Size = new System.Drawing.Size(121, 148);
            this.ListViewInternalIslands.TabIndex = 31;
            this.ListViewInternalIslands.UseCompatibleStateImageBehavior = false;
            this.ListViewInternalIslands.View = System.Windows.Forms.View.Details;
            this.ListViewInternalIslands.SelectedIndexChanged += new System.EventHandler(this.ListViewInternalIslands_SelectedIndexChanged);
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Indx";
            this.columnHeader11.Width = 45;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Score";
            // 
            // TabHandTester
            // 
            this.TabHandTester.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TabHandTester.Controls.Add(this.label38);
            this.TabHandTester.Controls.Add(this.label37);
            this.TabHandTester.Controls.Add(this.LabelHandData);
            this.TabHandTester.Controls.Add(this.SliderHandData);
            this.TabHandTester.Controls.Add(this.ButtonEvaluate);
            this.TabHandTester.Controls.Add(this.label36);
            this.TabHandTester.Controls.Add(this.label10);
            this.TabHandTester.Controls.Add(this.Message);
            this.TabHandTester.Controls.Add(this.LayoutOut);
            this.TabHandTester.Controls.Add(this.LayoutIn);
            this.TabHandTester.Location = new System.Drawing.Point(4, 22);
            this.TabHandTester.Name = "TabHandTester";
            this.TabHandTester.Padding = new System.Windows.Forms.Padding(3);
            this.TabHandTester.Size = new System.Drawing.Size(1054, 668);
            this.TabHandTester.TabIndex = 5;
            this.TabHandTester.Text = "Hand Tester";
            // 
            // LabelHandData
            // 
            this.LabelHandData.AutoSize = true;
            this.LabelHandData.Location = new System.Drawing.Point(215, 557);
            this.LabelHandData.Name = "LabelHandData";
            this.LabelHandData.Size = new System.Drawing.Size(13, 13);
            this.LabelHandData.TabIndex = 31;
            this.LabelHandData.Text = "0";
            // 
            // SliderHandData
            // 
            this.SliderHandData.Location = new System.Drawing.Point(143, 525);
            this.SliderHandData.Name = "SliderHandData";
            this.SliderHandData.Size = new System.Drawing.Size(172, 45);
            this.SliderHandData.TabIndex = 30;
            this.SliderHandData.Value = 10;
            this.SliderHandData.Scroll += new System.EventHandler(this.SliderHandData_Scroll);
            // 
            // ButtonEvaluate
            // 
            this.ButtonEvaluate.BackColor = System.Drawing.Color.Silver;
            this.ButtonEvaluate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ButtonEvaluate.Location = new System.Drawing.Point(137, 126);
            this.ButtonEvaluate.Name = "ButtonEvaluate";
            this.ButtonEvaluate.Size = new System.Drawing.Size(75, 23);
            this.ButtonEvaluate.TabIndex = 29;
            this.ButtonEvaluate.Text = "Evaluate";
            this.ButtonEvaluate.UseVisualStyleBackColor = false;
            this.ButtonEvaluate.Click += new System.EventHandler(this.ButtonEvaluate_Click);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label36.Location = new System.Drawing.Point(238, 131);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(58, 13);
            this.label36.TabIndex = 5;
            this.label36.Text = "OUTPUT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label10.Location = new System.Drawing.Point(46, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "INPUT";
            // 
            // Message
            // 
            this.Message.Controls.Add(this.LabelMessage);
            this.Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Message.Location = new System.Drawing.Point(137, 6);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(178, 105);
            this.Message.TabIndex = 2;
            this.Message.TabStop = false;
            this.Message.Text = "Message";
            // 
            // LabelMessage
            // 
            this.LabelMessage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.LabelMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LabelMessage.Location = new System.Drawing.Point(6, 19);
            this.LabelMessage.Multiline = true;
            this.LabelMessage.Name = "LabelMessage";
            this.LabelMessage.ReadOnly = true;
            this.LabelMessage.Size = new System.Drawing.Size(166, 63);
            this.LabelMessage.TabIndex = 1;
            this.LabelMessage.Text = "Please select a net from the inernal inspector";
            this.LabelMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LayoutOut
            // 
            this.LayoutOut.ColumnCount = 3;
            this.LayoutOut.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.LayoutOut.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.LayoutOut.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.LayoutOut.Location = new System.Drawing.Point(143, 174);
            this.LayoutOut.Name = "LayoutOut";
            this.LayoutOut.RowCount = 1;
            this.LayoutOut.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.LayoutOut.Size = new System.Drawing.Size(172, 331);
            this.LayoutOut.TabIndex = 1;
            // 
            // LayoutIn
            // 
            this.LayoutIn.AutoScroll = true;
            this.LayoutIn.ColumnCount = 2;
            this.LayoutIn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LayoutIn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LayoutIn.Location = new System.Drawing.Point(6, 36);
            this.LayoutIn.Name = "LayoutIn";
            this.LayoutIn.RowCount = 1;
            this.LayoutIn.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.LayoutIn.Size = new System.Drawing.Size(125, 570);
            this.LayoutIn.TabIndex = 0;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "CSV files|*.csv";
            this.OpenFileDialog.Title = "Select .csv Containing Data";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label37.Location = new System.Drawing.Point(251, 152);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(61, 13);
            this.label37.TabIndex = 32;
            this.label37.Text = "Predicted";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label38.Location = new System.Drawing.Point(191, 152);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(60, 13);
            this.label38.TabIndex = 33;
            this.label38.Text = "Expected";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1086, 718);
            this.Controls.Add(this.MainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "GeneticNetworkTrainer";
            this.MainTabControl.ResumeLayout(false);
            this.TabMainControl.ResumeLayout(false);
            this.TabMainControl.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderDataToUse)).EndInit();
            this.TabStructureGenetics.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureIslands)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureCrossover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureMutationStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureMutation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderStructureCopy)).EndInit();
            this.TabInternalGenetics.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderAnnealing)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalIslands)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalCrossover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalMutationStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalMutation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderInternalCopy)).EndInit();
            this.TabStructureInspector.ResumeLayout(false);
            this.TabStructureInspector.PerformLayout();
            this.GroupBoxStructType.ResumeLayout(false);
            this.GroupBoxStructType.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.GroupBoxGlobalStats.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.TabInternalInspector.ResumeLayout(false);
            this.TabInternalInspector.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.TabHandTester.ResumeLayout(false);
            this.TabHandTester.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderHandData)).EndInit();
            this.Message.ResumeLayout(false);
            this.Message.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private void MyInitialize()
        {

            LVSColumnSorter = new ListViewColumnSorter();
            this.ListViewStructures.ListViewItemSorter = LVSColumnSorter;

            PlotStructureIslandsSeries = new PlotView();
            PlotStructureIslandsHist = new PlotView();
            PlotStructureSeries = new PlotView();
            PlotInternalIslandsSeries = new PlotView();
            PlotInternalIslandsHist = new PlotView();
            PlotInternalSeries = new PlotView();

            PlotStructureIslandsSeries.Model = new PlotModel();
            StructIslandPlotPlaceHolder.Controls.Add(PlotStructureIslandsSeries);
            PlotStructureIslandsSeries.Location = new System.Drawing.Point(0, 0);
            PlotStructureIslandsSeries.Size = new System.Drawing.Size(433, 262);
            PlotStructureIslandsSeries.Model.PlotType = PlotType.XY;
            PlotStructureIslandsSeries.Model.Background = OxyColor.FromRgb(89, 15, 88);
            PlotStructureIslandsSeries.Model.TextColor = OxyColor.FromRgb(212, 240, 197);
            PlotStructureIslandsSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 100, AbsoluteMaximum=500 });
            PlotStructureIslandsSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = 0, Maximum = 100 });

            PlotStructureIslandsHist.Model = new PlotModel();
            StructIslandPlotPlaceHolder.Controls.Add(PlotStructureIslandsHist);
            PlotStructureIslandsHist.Location = new System.Drawing.Point(0, 0);
            PlotStructureIslandsHist.Size = new System.Drawing.Size(433, 262);
            PlotStructureIslandsHist.Model.PlotType = PlotType.XY;
            PlotStructureIslandsHist.Model.Background = OxyColor.FromRgb(89, 15, 88);
            PlotStructureIslandsHist.Model.TextColor = OxyColor.FromRgb(212, 240, 197);
            ColumnSeries PlotStructureIslandsSeriesCS = new ColumnSeries() { StrokeThickness = 2 };
            PlotStructureIslandsHist.Model.Series.Add(PlotStructureIslandsSeriesCS);

            PlotStructureSeries.Model = new PlotModel();
            StructPlotPlaceHolder.Controls.Add(PlotStructureSeries);
            PlotStructureSeries.Location = new System.Drawing.Point(0, 0);
            PlotStructureSeries.Size = new System.Drawing.Size(589, 344);
            PlotStructureSeries.Model.PlotType = PlotType.XY;
            PlotStructureSeries.Model.Background = OxyColor.FromRgb(89, 15, 88);
            PlotStructureSeries.Model.TextColor = OxyColor.FromRgb(212, 240, 197);
            PlotStructureSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 100, AbsoluteMaximum = 500 });
            PlotStructureSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = 0, Maximum = 100 });
            LineSeries PlotStructureSeriesLS = new LineSeries { StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };
            PlotStructureSeries.Model.Series.Add(PlotStructureSeriesLS);

            PlotInternalIslandsSeries.Model = new PlotModel();
            InternalIslandPlotPlaceHolder.Controls.Add(PlotInternalIslandsSeries);
            PlotInternalIslandsSeries.Location = new System.Drawing.Point(0, 0);
            PlotInternalIslandsSeries.Size = new System.Drawing.Size(442, 262);
            PlotInternalIslandsSeries.Model.PlotType = PlotType.XY;
            PlotInternalIslandsSeries.Model.Background = OxyColor.FromRgb(89, 15, 88);
            PlotInternalIslandsSeries.Model.TextColor = OxyColor.FromRgb(212, 240, 197);
            PlotInternalIslandsSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 100, AbsoluteMaximum = 500 });
            PlotInternalIslandsSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = 0, Maximum = 100 });
            LineSeries PlotInternalIslandsSeriesLS = new LineSeries { StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };
            PlotInternalIslandsSeries.Model.Series.Add(PlotInternalIslandsSeriesLS);

            PlotInternalIslandsHist.Model = new PlotModel();
            InternalIslandPlotPlaceHolder.Controls.Add(PlotInternalIslandsHist);
            PlotInternalIslandsHist.Location = new System.Drawing.Point(0, 0);
            PlotInternalIslandsHist.Size = new System.Drawing.Size(442, 262);
            PlotInternalIslandsHist.Model.PlotType = PlotType.XY;
            PlotInternalIslandsHist.Model.Background = OxyColor.FromRgb(89, 15, 88);
            PlotInternalIslandsHist.Model.TextColor = OxyColor.FromRgb(212, 240, 197);
            PlotInternalIslandsHist.Model.Axes.Add(new CategoryAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 100 });
            PlotInternalIslandsHist.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = 0, Maximum = 20 });
            ColumnSeries PlotInternalIslandsHistCS = new ColumnSeries() { StrokeThickness = 2 };
            PlotInternalIslandsHist.Model.Series.Add(PlotInternalIslandsHistCS);

            PlotInternalSeries.Model = new PlotModel();
            InternalPlotPlaceHolder.Controls.Add(PlotInternalSeries);
            PlotInternalSeries.Location = new System.Drawing.Point(0, 0);
            PlotInternalSeries.Size = new System.Drawing.Size(680, 344);
            PlotInternalSeries.Model.PlotType = PlotType.XY;
            PlotInternalSeries.Model.Background = OxyColor.FromRgb(89, 15, 88);
            PlotInternalSeries.Model.TextColor = OxyColor.FromRgb(212, 240, 197);
            PlotInternalSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 100, AbsoluteMaximum = 500 });
            PlotInternalSeries.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = 0, Maximum = 100 });
            LineSeries PlotInternalSeriesLS = new LineSeries { StrokeThickness = 2, Color = OxyColor.FromRgb(255, 255, 255) };
            PlotInternalSeries.Model.Series.Add(PlotInternalSeriesLS);
        }

        private Label LabelDynC;
        private Label LabelDynB;
        private Label LabelDynA;
        private CheckBox CheckBoxIncludeNetsInSave;
        private Label LabelGlobalStopwatchElapsed;
        private TextBox TextBoxRulesWinThreshold;
        private Label label23;
        private TextBox TextBoxRulesValidThreshold;
        private Label label24;
        private Label label38;
        private Label label37;
    }
}

