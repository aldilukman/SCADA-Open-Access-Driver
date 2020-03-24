namespace NetspireSDKTester
{
    partial class Form1
    {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timerDeviceStates = new System.Windows.Forms.Timer(this.components);
            this.listViewDeviceStates = new System.Windows.Forms.ListView();
            this.columnHeaderDeviceId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderIPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDstNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSoftwareRevision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPortNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHealthTestStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDictionarySupport = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDictionaryStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDictionaryVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSupplementaryFields = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pollDevicesTabPage = new System.Windows.Forms.TabPage();
            this.textBoxDeviceStatesPollingSeconds = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxDeviceStatesHideFaultyCommsFaultDevices = new System.Windows.Forms.CheckBox();
            this.checkBoxDeviceStatesHideEDI_IDI = new System.Windows.Forms.CheckBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.checkBoxDeviceStatesPolling = new System.Windows.Forms.CheckBox();
            this.pollEventsTabPage = new System.Windows.Forms.TabPage();
            this.checkBoxEventsPolling = new System.Windows.Forms.CheckBox();
            this.listViewEventsPolling = new System.Windows.Forms.ListView();
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clear = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.comboBoxEventsPollingSeconds = new System.Windows.Forms.ComboBox();
            this.paControlTabPage = new System.Windows.Forms.TabPage();
            this.buttonGetPaSinkHealth = new System.Windows.Forms.Button();
            this.buttonResetPaSink = new System.Windows.Forms.Button();
            this.listBoxPaSinks = new System.Windows.Forms.ListBox();
            this.buttonListPaSinksIds_PA = new System.Windows.Forms.Button();
            this.checkBoxTmp = new System.Windows.Forms.CheckBox();
            this.label39 = new System.Windows.Forms.Label();
            this.textBoxSWPAPriority = new System.Windows.Forms.TextBox();
            this.buttonSetSourceGain = new System.Windows.Forms.Button();
            this.checkBoxPaSourcePersistenceFlag = new System.Windows.Forms.CheckBox();
            this.checkBoxPaSinkPersistenceFlag = new System.Windows.Forms.CheckBox();
            this.labelPaSinkGain = new System.Windows.Forms.Label();
            this.labelPaSourceGain = new System.Windows.Forms.Label();
            this.trackBarPaSinkGain = new System.Windows.Forms.TrackBar();
            this.trackBarPaSourceGain = new System.Windows.Forms.TrackBar();
            this.buttonGetSourceGain = new System.Windows.Forms.Button();
            this.buttonGetSinkGain = new System.Windows.Forms.Button();
            this.buttonSetSinkGain = new System.Windows.Forms.Button();
            this.buttonGetHWTriggerState = new System.Windows.Forms.Button();
            this.buttonGetSourceState = new System.Windows.Forms.Button();
            this.checkBoxPA_PTT = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonDisableSelector = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonEnableSelector = new System.Windows.Forms.Button();
            this.listBoxPaSelectorSinks = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonGetSelectorState = new System.Windows.Forms.Button();
            this.buttonListSelectorSinks = new System.Windows.Forms.Button();
            this.buttonListSources = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBoxPaSelectors = new System.Windows.Forms.ListBox();
            this.listBoxPaSources = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonListSelectors = new System.Windows.Forms.Button();
            this.buttonListPaZoneIds_PA = new System.Windows.Forms.Button();
            this.buttonListHWTriggers = new System.Windows.Forms.Button();
            this.buttonCreateSWTrigger = new System.Windows.Forms.Button();
            this.listBoxPaHWTriggers = new System.Windows.Forms.ListBox();
            this.buttonDeleteSWTrigger = new System.Windows.Forms.Button();
            this.listBoxPaAttachedZones = new System.Windows.Forms.ListBox();
            this.buttonPA_PTT = new System.Windows.Forms.Button();
            this.buttonDetachSource = new System.Windows.Forms.Button();
            this.buttonAttachPaZone = new System.Windows.Forms.Button();
            this.buttonAttachSource = new System.Windows.Forms.Button();
            this.buttonDetachPaZone = new System.Windows.Forms.Button();
            this.buttonDetachAllPaZones = new System.Windows.Forms.Button();
            this.buttonListAttachedPaZones = new System.Windows.Forms.Button();
            this.checkBoxReplaceExistingSet = new System.Windows.Forms.CheckBox();
            this.listBoxPaZones = new System.Windows.Forms.ListBox();
            this.announcementsControlTabPage = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelCustomPriority = new System.Windows.Forms.Label();
            this.radioButtonCustomPriority = new System.Windows.Forms.RadioButton();
            this.trackBarCustomPriority = new System.Windows.Forms.TrackBar();
            this.radioButtonSourcePriority = new System.Windows.Forms.RadioButton();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPageDVA = new System.Windows.Forms.TabPage();
            this.buttonGetDictionaryContentsURI = new System.Windows.Forms.Button();
            this.buttonUpdateDictionary = new System.Windows.Forms.Button();
            this.buttonGetDictionaryChangeItems = new System.Windows.Forms.Button();
            this.labelDVAGain = new System.Windows.Forms.Label();
            this.trackBarDVAGain = new System.Windows.Forms.TrackBar();
            this.buttonPlayDVA = new System.Windows.Forms.Button();
            this.dataGridViewDictionaryChangeset = new System.Windows.Forms.DataGridView();
            this.buttonGetDictionaryItems = new System.Windows.Forms.Button();
            this.dataGridViewDictionaryItems = new System.Windows.Forms.DataGridView();
            this.tabPageTTS = new System.Windows.Forms.TabPage();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.buttonGetVocalizerList = new System.Windows.Forms.Button();
            this.textBoxTTSText = new System.Windows.Forms.TextBox();
            this.comboBoxTTSVocalizerList = new System.Windows.Forms.ComboBox();
            this.buttonPreviewTTS = new System.Windows.Forms.Button();
            this.listBoxTTSAudioMessageList = new System.Windows.Forms.ListBox();
            this.buttonClearTTS = new System.Windows.Forms.Button();
            this.buttonPlayTTS = new System.Windows.Forms.Button();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.label35 = new System.Windows.Forms.Label();
            this.textBoxAnnouncementDisplayTemplateValue3 = new System.Windows.Forms.TextBox();
            this.textBoxAnnouncementDisplayTemplateValue2 = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.textBoxAnnouncementDisplayTemplateValue1 = new System.Windows.Forms.TextBox();
            this.textBoxAnnouncementDisplayTemplateKey = new System.Windows.Forms.TextBox();
            this.buttonAnnouncementDisplayChangeTemplate = new System.Windows.Forms.Button();
            this.textBoxAnnouncementDisplayTemplateName = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteDVASchedule = new System.Windows.Forms.Button();
            this.listBoxSchedules = new System.Windows.Forms.ListBox();
            this.buttonListDVASchedules = new System.Windows.Forms.Button();
            this.buttonCreateDVASchedule = new System.Windows.Forms.Button();
            this.comboBoxFrequency = new System.Windows.Forms.ComboBox();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxEndMins = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEndHours = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxStartMins = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxStartHours = new System.Windows.Forms.ComboBox();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxDayMask = new System.Windows.Forms.ComboBox();
            this.dataGridViewDVAPaZoneIds = new System.Windows.Forms.DataGridView();
            this.buttonListPaZoneIds_DVA = new System.Windows.Forms.Button();
            this.callControlTabPage = new System.Windows.Forms.TabPage();
            this.buttonGetISDNTrunks = new System.Windows.Forms.Button();
            this.buttonGetSIPTrunks = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.comboBoxCallStatesPollingSeconds = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxResumeCallOnTerminalBPartyID = new System.Windows.Forms.TextBox();
            this.textBoxResumeCallOnTerminalAPartyID = new System.Windows.Forms.TextBox();
            this.textBoxResumeCallOnTerminalCallID = new System.Windows.Forms.TextBox();
            this.buttonResumeCallOnTerminal = new System.Windows.Forms.Button();
            this.textBoxAnswerCallOnTerminalBPartyID = new System.Windows.Forms.TextBox();
            this.textBoxAnswerCallOnTerminalAPartyID = new System.Windows.Forms.TextBox();
            this.textBoxAnswerCallOnTerminalCallID = new System.Windows.Forms.TextBox();
            this.buttonAnswerCallOnTerminal = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxResumeCallCalleeID = new System.Windows.Forms.TextBox();
            this.textBoxHoldCallCalleelID = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxResumeCallID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxHoldCallID = new System.Windows.Forms.TextBox();
            this.buttonResumeCall = new System.Windows.Forms.Button();
            this.buttonHoldCall = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxCreateCallID = new System.Windows.Forms.TextBox();
            this.checkBoxCallsPolling = new System.Windows.Forms.CheckBox();
            this.listViewCalls = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxCreateDestinationDeviceID = new System.Windows.Forms.TextBox();
            this.textBoxCreateDestinationCLI = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxTerminateCallBPartyID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCreateCallBPartyID = new System.Windows.Forms.TextBox();
            this.textBoxCreateCallAPartyID = new System.Windows.Forms.TextBox();
            this.textBoxResetDestinationMapDeviceID = new System.Windows.Forms.TextBox();
            this.textBoxCreateDestinationBPartyExtList = new System.Windows.Forms.TextBox();
            this.buttonResetDestinationMap = new System.Windows.Forms.Button();
            this.buttonCreateDestination = new System.Windows.Forms.Button();
            this.textBoxTransferCallNewBPartyID = new System.Windows.Forms.TextBox();
            this.textBoxTransferCallReferenceID = new System.Windows.Forms.TextBox();
            this.textBoxTerminateCallID = new System.Windows.Forms.TextBox();
            this.buttonTerminateCall = new System.Windows.Forms.Button();
            this.buttonTransferCall = new System.Windows.Forms.Button();
            this.buttonCreateCall = new System.Windows.Forms.Button();
            this.signallingControlTabPage = new System.Windows.Forms.TabPage();
            this.textBoxAvailablePlatforms = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.textBoxAvailableStations = new System.Windows.Forms.TextBox();
            this.textBoxAvailableVehicles = new System.Windows.Forms.TextBox();
            this.textBoxAvailableLines = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.labelPISServerStatus = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.comboBoxSignallingInfoPollingSeconds = new System.Windows.Forms.ComboBox();
            this.checkBoxSignallingInfoPolling = new System.Windows.Forms.CheckBox();
            this.alarmManagementTabPage = new System.Windows.Forms.TabPage();
            this.alarmsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.alarmAcknowledgeButton = new System.Windows.Forms.Button();
            this.getAlarmsButton = new System.Windows.Forms.Button();
            this.alarmManagementCheckBox = new System.Windows.Forms.CheckBox();
            this.bucharestOA0784TabPage = new System.Windows.Forms.TabPage();
            this.buttonUploadFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCP1ActiveStatus = new System.Windows.Forms.Button();
            this.buttonCP2AutoInfoStatus = new System.Windows.Forms.Button();
            this.labelAutoInfoStatus = new System.Windows.Forms.Label();
            this.buttonCP1AutoInfoStatus = new System.Windows.Forms.Button();
            this.buttonCP2ActiveStatus = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxHMIInputSave = new System.Windows.Forms.TextBox();
            this.textBoxHMIInputStartStation = new System.Windows.Forms.TextBox();
            this.textBoxHMIInputDestination = new System.Windows.Forms.TextBox();
            this.textBoxHMIInputLine = new System.Windows.Forms.TextBox();
            this.listBoxHMIList = new System.Windows.Forms.ListBox();
            this.textBoxHMIInputCode = new System.Windows.Forms.TextBox();
            this.buttonDeleteHMIContent = new System.Windows.Forms.Button();
            this.buttonUpdateHMIData = new System.Windows.Forms.Button();
            this.waratahOA0979TabPage = new System.Windows.Forms.TabPage();
            this.textBoxWaratahCommercialRadioInterrupt = new System.Windows.Forms.TextBox();
            this.buttonWaratahSetCommercialRadioInterrupt = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonWaratahCSPKR8Monitoring = new System.Windows.Forms.Button();
            this.buttonWaratahCSPKR1Monitoring = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonTCXC8_DOUT6 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DOUT5 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DOUT4 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DOUT3 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DOUT2 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DOUT1 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN8 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN7 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN6 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN5 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN4 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN3 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN2 = new System.Windows.Forms.Button();
            this.buttonTCXC8_DIN1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonTCXC1_DOUT6 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DOUT5 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DOUT4 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DOUT3 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DOUT2 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DOUT1 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN8 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN7 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN6 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN5 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN4 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN3 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN2 = new System.Windows.Forms.Button();
            this.buttonTCXC1_DIN1 = new System.Windows.Forms.Button();
            this.checkBoxWaratahEXTPASide2 = new System.Windows.Forms.CheckBox();
            this.checkBoxWaratahEXTPASide1 = new System.Windows.Forms.CheckBox();
            this.buttonWaratahCancelInterruptionToCommercialRadio = new System.Windows.Forms.Button();
            this.buttonWaratahInterruptCommercialRadio = new System.Windows.Forms.Button();
            this.checkBoxWarathaEnableDisableCommercialRadio = new System.Windows.Forms.CheckBox();
            this.listBoxDeviceStateEvents = new System.Windows.Forms.ListBox();
            this.listBoxPAEvents = new System.Windows.Forms.ListBox();
            this.listBoxCallEvents = new System.Windows.Forms.ListBox();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToKTMBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToKLMonorailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToBucharestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToGCRTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToHKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToSLRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToWaratahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToMetrolinxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToOttawaLightRailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.connectToASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isCommsEstablishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isASConnectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.getSystemRevisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isSystemRevisionConsistentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDebugMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDeviceStatesEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPAEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCallEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCDREvetnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTerminalEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSignallingEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllMessagesEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdkVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxDebugMsg = new System.Windows.Forms.ListBox();
            this.timerCallStates = new System.Windows.Forms.Timer(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.listBoxCDREvents = new System.Windows.Forms.ListBox();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.listBoxTerminalEvents = new System.Windows.Forms.ListBox();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.listBoxSignallingEvents = new System.Windows.Forms.ListBox();
            this.contextMenuStripEvents = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearMessagsEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerEventsPolling = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripListViewDevices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.isolateDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deIsolateDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getDynamicConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDynamicConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initiateDeviceHealthTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readDigitalInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDigitalOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsetDigitalOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readDigitalOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSignallingEvents = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.pollDevicesTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pollEventsTabPage.SuspendLayout();
            this.paControlTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPaSinkGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPaSourceGain)).BeginInit();
            this.announcementsControlTabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCustomPriority)).BeginInit();
            this.tabControl3.SuspendLayout();
            this.tabPageDVA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDVAGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDictionaryChangeset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDictionaryItems)).BeginInit();
            this.tabPageTTS.SuspendLayout();
            this.tabPageDisplay.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDVAPaZoneIds)).BeginInit();
            this.callControlTabPage.SuspendLayout();
            this.signallingControlTabPage.SuspendLayout();
            this.alarmManagementTabPage.SuspendLayout();
            this.bucharestOA0784TabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.waratahOA0979TabPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.contextMenuStripEvents.SuspendLayout();
            this.contextMenuStripListViewDevices.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerDeviceStates
            // 
            this.timerDeviceStates.Interval = 5000;
            this.timerDeviceStates.Tick += new System.EventHandler(this.timerDeviceStates_Tick);
            // 
            // listViewDeviceStates
            // 
            resources.ApplyResources(this.listViewDeviceStates, "listViewDeviceStates");
            this.listViewDeviceStates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDeviceId,
            this.columnHeaderIPAddress,
            this.columnHeaderDstNo,
            this.columnHeaderSoftwareRevision,
            this.columnHeaderPortNo,
            this.columnHeaderState,
            this.columnHeaderHealthTestStatus,
            this.columnHeaderDictionarySupport,
            this.columnHeaderDictionaryStatus,
            this.columnHeaderDictionaryVersion,
            this.columnHeaderSupplementaryFields});
            this.listViewDeviceStates.FullRowSelect = true;
            this.listViewDeviceStates.GridLines = true;
            this.listViewDeviceStates.MultiSelect = false;
            this.listViewDeviceStates.Name = "listViewDeviceStates";
            this.listViewDeviceStates.UseCompatibleStateImageBehavior = false;
            this.listViewDeviceStates.View = System.Windows.Forms.View.Details;
            this.listViewDeviceStates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewDeviceStates_MouseDown);
            // 
            // columnHeaderDeviceId
            // 
            resources.ApplyResources(this.columnHeaderDeviceId, "columnHeaderDeviceId");
            // 
            // columnHeaderIPAddress
            // 
            resources.ApplyResources(this.columnHeaderIPAddress, "columnHeaderIPAddress");
            // 
            // columnHeaderDstNo
            // 
            resources.ApplyResources(this.columnHeaderDstNo, "columnHeaderDstNo");
            // 
            // columnHeaderSoftwareRevision
            // 
            resources.ApplyResources(this.columnHeaderSoftwareRevision, "columnHeaderSoftwareRevision");
            // 
            // columnHeaderPortNo
            // 
            resources.ApplyResources(this.columnHeaderPortNo, "columnHeaderPortNo");
            // 
            // columnHeaderState
            // 
            resources.ApplyResources(this.columnHeaderState, "columnHeaderState");
            // 
            // columnHeaderHealthTestStatus
            // 
            resources.ApplyResources(this.columnHeaderHealthTestStatus, "columnHeaderHealthTestStatus");
            // 
            // columnHeaderDictionarySupport
            // 
            resources.ApplyResources(this.columnHeaderDictionarySupport, "columnHeaderDictionarySupport");
            // 
            // columnHeaderDictionaryStatus
            // 
            resources.ApplyResources(this.columnHeaderDictionaryStatus, "columnHeaderDictionaryStatus");
            // 
            // columnHeaderDictionaryVersion
            // 
            resources.ApplyResources(this.columnHeaderDictionaryVersion, "columnHeaderDictionaryVersion");
            // 
            // columnHeaderSupplementaryFields
            // 
            resources.ApplyResources(this.columnHeaderSupplementaryFields, "columnHeaderSupplementaryFields");
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.pollDevicesTabPage);
            this.tabControl1.Controls.Add(this.pollEventsTabPage);
            this.tabControl1.Controls.Add(this.paControlTabPage);
            this.tabControl1.Controls.Add(this.announcementsControlTabPage);
            this.tabControl1.Controls.Add(this.callControlTabPage);
            this.tabControl1.Controls.Add(this.signallingControlTabPage);
            this.tabControl1.Controls.Add(this.alarmManagementTabPage);
            this.tabControl1.Controls.Add(this.bucharestOA0784TabPage);
            this.tabControl1.Controls.Add(this.waratahOA0979TabPage);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // pollDevicesTabPage
            // 
            this.pollDevicesTabPage.Controls.Add(this.textBoxDeviceStatesPollingSeconds);
            this.pollDevicesTabPage.Controls.Add(this.groupBox2);
            this.pollDevicesTabPage.Controls.Add(this.label38);
            this.pollDevicesTabPage.Controls.Add(this.label18);
            this.pollDevicesTabPage.Controls.Add(this.checkBoxDeviceStatesPolling);
            this.pollDevicesTabPage.Controls.Add(this.listViewDeviceStates);
            resources.ApplyResources(this.pollDevicesTabPage, "pollDevicesTabPage");
            this.pollDevicesTabPage.Name = "pollDevicesTabPage";
            this.pollDevicesTabPage.UseVisualStyleBackColor = true;
            // 
            // textBoxDeviceStatesPollingSeconds
            // 
            resources.ApplyResources(this.textBoxDeviceStatesPollingSeconds, "textBoxDeviceStatesPollingSeconds");
            this.textBoxDeviceStatesPollingSeconds.Name = "textBoxDeviceStatesPollingSeconds";
            this.textBoxDeviceStatesPollingSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDeviceStatesPollingSeconds_KeyPress);
            this.textBoxDeviceStatesPollingSeconds.Leave += new System.EventHandler(this.textBoxDeviceStatesPollingSeconds_Leave);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.checkBoxDeviceStatesHideFaultyCommsFaultDevices);
            this.groupBox2.Controls.Add(this.checkBoxDeviceStatesHideEDI_IDI);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkViolet;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBoxDeviceStatesHideFaultyCommsFaultDevices
            // 
            resources.ApplyResources(this.checkBoxDeviceStatesHideFaultyCommsFaultDevices, "checkBoxDeviceStatesHideFaultyCommsFaultDevices");
            this.checkBoxDeviceStatesHideFaultyCommsFaultDevices.Checked = true;
            this.checkBoxDeviceStatesHideFaultyCommsFaultDevices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDeviceStatesHideFaultyCommsFaultDevices.ForeColor = System.Drawing.Color.Black;
            this.checkBoxDeviceStatesHideFaultyCommsFaultDevices.Name = "checkBoxDeviceStatesHideFaultyCommsFaultDevices";
            this.checkBoxDeviceStatesHideFaultyCommsFaultDevices.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeviceStatesHideEDI_IDI
            // 
            resources.ApplyResources(this.checkBoxDeviceStatesHideEDI_IDI, "checkBoxDeviceStatesHideEDI_IDI");
            this.checkBoxDeviceStatesHideEDI_IDI.Checked = true;
            this.checkBoxDeviceStatesHideEDI_IDI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDeviceStatesHideEDI_IDI.ForeColor = System.Drawing.Color.Black;
            this.checkBoxDeviceStatesHideEDI_IDI.Name = "checkBoxDeviceStatesHideEDI_IDI";
            this.checkBoxDeviceStatesHideEDI_IDI.UseVisualStyleBackColor = true;
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.ForeColor = System.Drawing.Color.Red;
            this.label38.Name = "label38";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.ForeColor = System.Drawing.Color.DarkViolet;
            this.label18.Name = "label18";
            // 
            // checkBoxDeviceStatesPolling
            // 
            resources.ApplyResources(this.checkBoxDeviceStatesPolling, "checkBoxDeviceStatesPolling");
            this.checkBoxDeviceStatesPolling.BackColor = System.Drawing.Color.Khaki;
            this.checkBoxDeviceStatesPolling.Name = "checkBoxDeviceStatesPolling";
            this.checkBoxDeviceStatesPolling.UseVisualStyleBackColor = false;
            this.checkBoxDeviceStatesPolling.CheckedChanged += new System.EventHandler(this.checkBoxDeviceStatesPolling_CheckedChanged);
            // 
            // pollEventsTabPage
            // 
            this.pollEventsTabPage.Controls.Add(this.checkBoxEventsPolling);
            this.pollEventsTabPage.Controls.Add(this.listViewEventsPolling);
            this.pollEventsTabPage.Controls.Add(this.clear);
            this.pollEventsTabPage.Controls.Add(this.label36);
            this.pollEventsTabPage.Controls.Add(this.comboBoxEventsPollingSeconds);
            resources.ApplyResources(this.pollEventsTabPage, "pollEventsTabPage");
            this.pollEventsTabPage.Name = "pollEventsTabPage";
            this.pollEventsTabPage.UseVisualStyleBackColor = true;
            // 
            // checkBoxEventsPolling
            // 
            resources.ApplyResources(this.checkBoxEventsPolling, "checkBoxEventsPolling");
            this.checkBoxEventsPolling.BackColor = System.Drawing.Color.Khaki;
            this.checkBoxEventsPolling.Name = "checkBoxEventsPolling";
            this.checkBoxEventsPolling.UseVisualStyleBackColor = false;
            this.checkBoxEventsPolling.CheckedChanged += new System.EventHandler(this.checkBoxEventsPolling_CheckedChanged);
            // 
            // listViewEventsPolling
            // 
            resources.ApplyResources(this.listViewEventsPolling, "listViewEventsPolling");
            this.listViewEventsPolling.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader29});
            this.listViewEventsPolling.FullRowSelect = true;
            this.listViewEventsPolling.GridLines = true;
            this.listViewEventsPolling.MultiSelect = false;
            this.listViewEventsPolling.Name = "listViewEventsPolling";
            this.listViewEventsPolling.UseCompatibleStateImageBehavior = false;
            this.listViewEventsPolling.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader19
            // 
            resources.ApplyResources(this.columnHeader19, "columnHeader19");
            // 
            // columnHeader20
            // 
            resources.ApplyResources(this.columnHeader20, "columnHeader20");
            // 
            // columnHeader21
            // 
            resources.ApplyResources(this.columnHeader21, "columnHeader21");
            // 
            // columnHeader29
            // 
            resources.ApplyResources(this.columnHeader29, "columnHeader29");
            // 
            // clear
            // 
            resources.ApplyResources(this.clear, "clear");
            this.clear.Name = "clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.ForeColor = System.Drawing.Color.DarkViolet;
            this.label36.Name = "label36";
            // 
            // comboBoxEventsPollingSeconds
            // 
            resources.ApplyResources(this.comboBoxEventsPollingSeconds, "comboBoxEventsPollingSeconds");
            this.comboBoxEventsPollingSeconds.FormattingEnabled = true;
            this.comboBoxEventsPollingSeconds.Items.AddRange(new object[] {
            resources.GetString("comboBoxEventsPollingSeconds.Items"),
            resources.GetString("comboBoxEventsPollingSeconds.Items1"),
            resources.GetString("comboBoxEventsPollingSeconds.Items2"),
            resources.GetString("comboBoxEventsPollingSeconds.Items3"),
            resources.GetString("comboBoxEventsPollingSeconds.Items4"),
            resources.GetString("comboBoxEventsPollingSeconds.Items5"),
            resources.GetString("comboBoxEventsPollingSeconds.Items6"),
            resources.GetString("comboBoxEventsPollingSeconds.Items7"),
            resources.GetString("comboBoxEventsPollingSeconds.Items8"),
            resources.GetString("comboBoxEventsPollingSeconds.Items9"),
            resources.GetString("comboBoxEventsPollingSeconds.Items10"),
            resources.GetString("comboBoxEventsPollingSeconds.Items11"),
            resources.GetString("comboBoxEventsPollingSeconds.Items12"),
            resources.GetString("comboBoxEventsPollingSeconds.Items13")});
            this.comboBoxEventsPollingSeconds.Name = "comboBoxEventsPollingSeconds";
            // 
            // paControlTabPage
            // 
            this.paControlTabPage.Controls.Add(this.buttonGetPaSinkHealth);
            this.paControlTabPage.Controls.Add(this.buttonResetPaSink);
            this.paControlTabPage.Controls.Add(this.listBoxPaSinks);
            this.paControlTabPage.Controls.Add(this.buttonListPaSinksIds_PA);
            this.paControlTabPage.Controls.Add(this.checkBoxTmp);
            this.paControlTabPage.Controls.Add(this.label39);
            this.paControlTabPage.Controls.Add(this.textBoxSWPAPriority);
            this.paControlTabPage.Controls.Add(this.buttonSetSourceGain);
            this.paControlTabPage.Controls.Add(this.checkBoxPaSourcePersistenceFlag);
            this.paControlTabPage.Controls.Add(this.checkBoxPaSinkPersistenceFlag);
            this.paControlTabPage.Controls.Add(this.labelPaSinkGain);
            this.paControlTabPage.Controls.Add(this.labelPaSourceGain);
            this.paControlTabPage.Controls.Add(this.trackBarPaSinkGain);
            this.paControlTabPage.Controls.Add(this.trackBarPaSourceGain);
            this.paControlTabPage.Controls.Add(this.buttonGetSourceGain);
            this.paControlTabPage.Controls.Add(this.buttonGetSinkGain);
            this.paControlTabPage.Controls.Add(this.buttonSetSinkGain);
            this.paControlTabPage.Controls.Add(this.buttonGetHWTriggerState);
            this.paControlTabPage.Controls.Add(this.buttonGetSourceState);
            this.paControlTabPage.Controls.Add(this.checkBoxPA_PTT);
            this.paControlTabPage.Controls.Add(this.button4);
            this.paControlTabPage.Controls.Add(this.buttonDisableSelector);
            this.paControlTabPage.Controls.Add(this.button3);
            this.paControlTabPage.Controls.Add(this.buttonEnableSelector);
            this.paControlTabPage.Controls.Add(this.listBoxPaSelectorSinks);
            this.paControlTabPage.Controls.Add(this.button2);
            this.paControlTabPage.Controls.Add(this.buttonGetSelectorState);
            this.paControlTabPage.Controls.Add(this.buttonListSelectorSinks);
            this.paControlTabPage.Controls.Add(this.buttonListSources);
            this.paControlTabPage.Controls.Add(this.listBox1);
            this.paControlTabPage.Controls.Add(this.listBoxPaSelectors);
            this.paControlTabPage.Controls.Add(this.listBoxPaSources);
            this.paControlTabPage.Controls.Add(this.button1);
            this.paControlTabPage.Controls.Add(this.buttonListSelectors);
            this.paControlTabPage.Controls.Add(this.buttonListPaZoneIds_PA);
            this.paControlTabPage.Controls.Add(this.buttonListHWTriggers);
            this.paControlTabPage.Controls.Add(this.buttonCreateSWTrigger);
            this.paControlTabPage.Controls.Add(this.listBoxPaHWTriggers);
            this.paControlTabPage.Controls.Add(this.buttonDeleteSWTrigger);
            this.paControlTabPage.Controls.Add(this.listBoxPaAttachedZones);
            this.paControlTabPage.Controls.Add(this.buttonPA_PTT);
            this.paControlTabPage.Controls.Add(this.buttonDetachSource);
            this.paControlTabPage.Controls.Add(this.buttonAttachPaZone);
            this.paControlTabPage.Controls.Add(this.buttonAttachSource);
            this.paControlTabPage.Controls.Add(this.buttonDetachPaZone);
            this.paControlTabPage.Controls.Add(this.buttonDetachAllPaZones);
            this.paControlTabPage.Controls.Add(this.buttonListAttachedPaZones);
            this.paControlTabPage.Controls.Add(this.checkBoxReplaceExistingSet);
            this.paControlTabPage.Controls.Add(this.listBoxPaZones);
            resources.ApplyResources(this.paControlTabPage, "paControlTabPage");
            this.paControlTabPage.Name = "paControlTabPage";
            this.paControlTabPage.UseVisualStyleBackColor = true;
            // 
            // buttonGetPaSinkHealth
            // 
            resources.ApplyResources(this.buttonGetPaSinkHealth, "buttonGetPaSinkHealth");
            this.buttonGetPaSinkHealth.Name = "buttonGetPaSinkHealth";
            this.buttonGetPaSinkHealth.UseVisualStyleBackColor = true;
            this.buttonGetPaSinkHealth.Click += new System.EventHandler(this.buttonGetPaSinkHealth_Click);
            // 
            // buttonResetPaSink
            // 
            resources.ApplyResources(this.buttonResetPaSink, "buttonResetPaSink");
            this.buttonResetPaSink.Name = "buttonResetPaSink";
            this.buttonResetPaSink.UseVisualStyleBackColor = true;
            this.buttonResetPaSink.Click += new System.EventHandler(this.buttonResetPaSink_Click);
            // 
            // listBoxPaSinks
            // 
            this.listBoxPaSinks.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaSinks, "listBoxPaSinks");
            this.listBoxPaSinks.Name = "listBoxPaSinks";
            this.listBoxPaSinks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // buttonListPaSinksIds_PA
            // 
            resources.ApplyResources(this.buttonListPaSinksIds_PA, "buttonListPaSinksIds_PA");
            this.buttonListPaSinksIds_PA.Name = "buttonListPaSinksIds_PA";
            this.buttonListPaSinksIds_PA.UseVisualStyleBackColor = true;
            this.buttonListPaSinksIds_PA.Click += new System.EventHandler(this.buttonListPaSinksIds_PA_Click);
            // 
            // checkBoxTmp
            // 
            resources.ApplyResources(this.checkBoxTmp, "checkBoxTmp");
            this.checkBoxTmp.Name = "checkBoxTmp";
            this.checkBoxTmp.UseVisualStyleBackColor = true;
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.ForeColor = System.Drawing.Color.DarkViolet;
            this.label39.Name = "label39";
            // 
            // textBoxSWPAPriority
            // 
            resources.ApplyResources(this.textBoxSWPAPriority, "textBoxSWPAPriority");
            this.textBoxSWPAPriority.Name = "textBoxSWPAPriority";
            // 
            // buttonSetSourceGain
            // 
            resources.ApplyResources(this.buttonSetSourceGain, "buttonSetSourceGain");
            this.buttonSetSourceGain.Name = "buttonSetSourceGain";
            this.buttonSetSourceGain.UseVisualStyleBackColor = true;
            this.buttonSetSourceGain.Click += new System.EventHandler(this.buttonSetSourceGain_Click);
            // 
            // checkBoxPaSourcePersistenceFlag
            // 
            resources.ApplyResources(this.checkBoxPaSourcePersistenceFlag, "checkBoxPaSourcePersistenceFlag");
            this.checkBoxPaSourcePersistenceFlag.Name = "checkBoxPaSourcePersistenceFlag";
            this.checkBoxPaSourcePersistenceFlag.UseVisualStyleBackColor = true;
            // 
            // checkBoxPaSinkPersistenceFlag
            // 
            resources.ApplyResources(this.checkBoxPaSinkPersistenceFlag, "checkBoxPaSinkPersistenceFlag");
            this.checkBoxPaSinkPersistenceFlag.Name = "checkBoxPaSinkPersistenceFlag";
            this.checkBoxPaSinkPersistenceFlag.UseVisualStyleBackColor = true;
            // 
            // labelPaSinkGain
            // 
            resources.ApplyResources(this.labelPaSinkGain, "labelPaSinkGain");
            this.labelPaSinkGain.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelPaSinkGain.Name = "labelPaSinkGain";
            // 
            // labelPaSourceGain
            // 
            resources.ApplyResources(this.labelPaSourceGain, "labelPaSourceGain");
            this.labelPaSourceGain.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelPaSourceGain.Name = "labelPaSourceGain";
            // 
            // trackBarPaSinkGain
            // 
            this.trackBarPaSinkGain.LargeChange = 1;
            resources.ApplyResources(this.trackBarPaSinkGain, "trackBarPaSinkGain");
            this.trackBarPaSinkGain.Maximum = 256;
            this.trackBarPaSinkGain.Name = "trackBarPaSinkGain";
            this.trackBarPaSinkGain.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarPaSinkGain.Value = 256;
            this.trackBarPaSinkGain.Scroll += new System.EventHandler(this.trackBarPaSinkGain_Scroll);
            // 
            // trackBarPaSourceGain
            // 
            this.trackBarPaSourceGain.LargeChange = 1;
            resources.ApplyResources(this.trackBarPaSourceGain, "trackBarPaSourceGain");
            this.trackBarPaSourceGain.Maximum = 13800;
            this.trackBarPaSourceGain.Name = "trackBarPaSourceGain";
            this.trackBarPaSourceGain.Value = 9600;
            this.trackBarPaSourceGain.Scroll += new System.EventHandler(this.trackBarPaSourceGain_Scroll);
            // 
            // buttonGetSourceGain
            // 
            resources.ApplyResources(this.buttonGetSourceGain, "buttonGetSourceGain");
            this.buttonGetSourceGain.Name = "buttonGetSourceGain";
            this.buttonGetSourceGain.UseVisualStyleBackColor = true;
            this.buttonGetSourceGain.Click += new System.EventHandler(this.buttonGetSourceGain_Click);
            // 
            // buttonGetSinkGain
            // 
            resources.ApplyResources(this.buttonGetSinkGain, "buttonGetSinkGain");
            this.buttonGetSinkGain.Name = "buttonGetSinkGain";
            this.buttonGetSinkGain.UseVisualStyleBackColor = true;
            this.buttonGetSinkGain.Click += new System.EventHandler(this.buttonGetSinkGain_Click);
            // 
            // buttonSetSinkGain
            // 
            resources.ApplyResources(this.buttonSetSinkGain, "buttonSetSinkGain");
            this.buttonSetSinkGain.Name = "buttonSetSinkGain";
            this.buttonSetSinkGain.UseVisualStyleBackColor = true;
            this.buttonSetSinkGain.Click += new System.EventHandler(this.buttonSetSinkGain_Click);
            // 
            // buttonGetHWTriggerState
            // 
            resources.ApplyResources(this.buttonGetHWTriggerState, "buttonGetHWTriggerState");
            this.buttonGetHWTriggerState.Name = "buttonGetHWTriggerState";
            this.buttonGetHWTriggerState.UseVisualStyleBackColor = true;
            this.buttonGetHWTriggerState.Click += new System.EventHandler(this.buttonGetHWTriggerState_Click);
            // 
            // buttonGetSourceState
            // 
            resources.ApplyResources(this.buttonGetSourceState, "buttonGetSourceState");
            this.buttonGetSourceState.Name = "buttonGetSourceState";
            this.buttonGetSourceState.UseVisualStyleBackColor = true;
            this.buttonGetSourceState.Click += new System.EventHandler(this.buttonGetSourceState_Click);
            // 
            // checkBoxPA_PTT
            // 
            resources.ApplyResources(this.checkBoxPA_PTT, "checkBoxPA_PTT");
            this.checkBoxPA_PTT.Name = "checkBoxPA_PTT";
            this.checkBoxPA_PTT.UseVisualStyleBackColor = true;
            this.checkBoxPA_PTT.CheckedChanged += new System.EventHandler(this.checkBoxPA_PTT_CheckedChanged);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.buttonDisableSelector_Click);
            // 
            // buttonDisableSelector
            // 
            resources.ApplyResources(this.buttonDisableSelector, "buttonDisableSelector");
            this.buttonDisableSelector.Name = "buttonDisableSelector";
            this.buttonDisableSelector.UseVisualStyleBackColor = true;
            this.buttonDisableSelector.Click += new System.EventHandler(this.buttonDisableSelector_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonEnableSelector_Click);
            // 
            // buttonEnableSelector
            // 
            resources.ApplyResources(this.buttonEnableSelector, "buttonEnableSelector");
            this.buttonEnableSelector.Name = "buttonEnableSelector";
            this.buttonEnableSelector.UseVisualStyleBackColor = true;
            this.buttonEnableSelector.Click += new System.EventHandler(this.buttonEnableSelector_Click);
            // 
            // listBoxPaSelectorSinks
            // 
            this.listBoxPaSelectorSinks.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaSelectorSinks, "listBoxPaSelectorSinks");
            this.listBoxPaSelectorSinks.Name = "listBoxPaSelectorSinks";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonGetSelectorState_Click);
            // 
            // buttonGetSelectorState
            // 
            resources.ApplyResources(this.buttonGetSelectorState, "buttonGetSelectorState");
            this.buttonGetSelectorState.Name = "buttonGetSelectorState";
            this.buttonGetSelectorState.UseVisualStyleBackColor = true;
            this.buttonGetSelectorState.Click += new System.EventHandler(this.buttonGetSelectorState_Click);
            // 
            // buttonListSelectorSinks
            // 
            resources.ApplyResources(this.buttonListSelectorSinks, "buttonListSelectorSinks");
            this.buttonListSelectorSinks.Name = "buttonListSelectorSinks";
            this.buttonListSelectorSinks.UseVisualStyleBackColor = true;
            this.buttonListSelectorSinks.Click += new System.EventHandler(this.buttonListSelectorSinks_Click);
            // 
            // buttonListSources
            // 
            resources.ApplyResources(this.buttonListSources, "buttonListSources");
            this.buttonListSources.Name = "buttonListSources";
            this.buttonListSources.UseVisualStyleBackColor = true;
            this.buttonListSources.Click += new System.EventHandler(this.buttonListSources_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.Name = "listBox1";
            // 
            // listBoxPaSelectors
            // 
            this.listBoxPaSelectors.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaSelectors, "listBoxPaSelectors");
            this.listBoxPaSelectors.Name = "listBoxPaSelectors";
            // 
            // listBoxPaSources
            // 
            this.listBoxPaSources.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaSources, "listBoxPaSources");
            this.listBoxPaSources.Name = "listBoxPaSources";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonListSelectors_Click);
            // 
            // buttonListSelectors
            // 
            resources.ApplyResources(this.buttonListSelectors, "buttonListSelectors");
            this.buttonListSelectors.Name = "buttonListSelectors";
            this.buttonListSelectors.UseVisualStyleBackColor = true;
            this.buttonListSelectors.Click += new System.EventHandler(this.buttonListSelectors_Click);
            // 
            // buttonListPaZoneIds_PA
            // 
            resources.ApplyResources(this.buttonListPaZoneIds_PA, "buttonListPaZoneIds_PA");
            this.buttonListPaZoneIds_PA.Name = "buttonListPaZoneIds_PA";
            this.buttonListPaZoneIds_PA.UseVisualStyleBackColor = true;
            this.buttonListPaZoneIds_PA.Click += new System.EventHandler(this.buttonListPaZoneIds_PA_Click);
            // 
            // buttonListHWTriggers
            // 
            resources.ApplyResources(this.buttonListHWTriggers, "buttonListHWTriggers");
            this.buttonListHWTriggers.Name = "buttonListHWTriggers";
            this.buttonListHWTriggers.UseVisualStyleBackColor = true;
            this.buttonListHWTriggers.Click += new System.EventHandler(this.buttonListHWTriggers_Click);
            // 
            // buttonCreateSWTrigger
            // 
            resources.ApplyResources(this.buttonCreateSWTrigger, "buttonCreateSWTrigger");
            this.buttonCreateSWTrigger.Name = "buttonCreateSWTrigger";
            this.buttonCreateSWTrigger.UseVisualStyleBackColor = true;
            this.buttonCreateSWTrigger.Click += new System.EventHandler(this.buttonCreateSWTrigger_Click);
            // 
            // listBoxPaHWTriggers
            // 
            this.listBoxPaHWTriggers.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaHWTriggers, "listBoxPaHWTriggers");
            this.listBoxPaHWTriggers.Name = "listBoxPaHWTriggers";
            // 
            // buttonDeleteSWTrigger
            // 
            resources.ApplyResources(this.buttonDeleteSWTrigger, "buttonDeleteSWTrigger");
            this.buttonDeleteSWTrigger.Name = "buttonDeleteSWTrigger";
            this.buttonDeleteSWTrigger.UseVisualStyleBackColor = true;
            this.buttonDeleteSWTrigger.Click += new System.EventHandler(this.buttonDeleteSWTrigger_Click);
            // 
            // listBoxPaAttachedZones
            // 
            this.listBoxPaAttachedZones.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaAttachedZones, "listBoxPaAttachedZones");
            this.listBoxPaAttachedZones.Name = "listBoxPaAttachedZones";
            // 
            // buttonPA_PTT
            // 
            resources.ApplyResources(this.buttonPA_PTT, "buttonPA_PTT");
            this.buttonPA_PTT.Name = "buttonPA_PTT";
            this.buttonPA_PTT.UseVisualStyleBackColor = true;
            this.buttonPA_PTT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonPA_PTT_MouseDown);
            this.buttonPA_PTT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonPA_PTT_MouseUp);
            // 
            // buttonDetachSource
            // 
            resources.ApplyResources(this.buttonDetachSource, "buttonDetachSource");
            this.buttonDetachSource.Name = "buttonDetachSource";
            this.buttonDetachSource.UseVisualStyleBackColor = true;
            this.buttonDetachSource.Click += new System.EventHandler(this.buttonDetachSource_Click);
            // 
            // buttonAttachPaZone
            // 
            resources.ApplyResources(this.buttonAttachPaZone, "buttonAttachPaZone");
            this.buttonAttachPaZone.Name = "buttonAttachPaZone";
            this.buttonAttachPaZone.UseVisualStyleBackColor = true;
            this.buttonAttachPaZone.Click += new System.EventHandler(this.buttonAttachPaZone_Click);
            // 
            // buttonAttachSource
            // 
            resources.ApplyResources(this.buttonAttachSource, "buttonAttachSource");
            this.buttonAttachSource.Name = "buttonAttachSource";
            this.buttonAttachSource.UseVisualStyleBackColor = true;
            this.buttonAttachSource.Click += new System.EventHandler(this.buttonAttachSource_Click);
            // 
            // buttonDetachPaZone
            // 
            resources.ApplyResources(this.buttonDetachPaZone, "buttonDetachPaZone");
            this.buttonDetachPaZone.Name = "buttonDetachPaZone";
            this.buttonDetachPaZone.UseVisualStyleBackColor = true;
            this.buttonDetachPaZone.Click += new System.EventHandler(this.buttonDetachPaZone_Click);
            // 
            // buttonDetachAllPaZones
            // 
            resources.ApplyResources(this.buttonDetachAllPaZones, "buttonDetachAllPaZones");
            this.buttonDetachAllPaZones.Name = "buttonDetachAllPaZones";
            this.buttonDetachAllPaZones.UseVisualStyleBackColor = true;
            this.buttonDetachAllPaZones.Click += new System.EventHandler(this.buttonDetachAllPaZones_Click);
            // 
            // buttonListAttachedPaZones
            // 
            resources.ApplyResources(this.buttonListAttachedPaZones, "buttonListAttachedPaZones");
            this.buttonListAttachedPaZones.Name = "buttonListAttachedPaZones";
            this.buttonListAttachedPaZones.UseVisualStyleBackColor = true;
            this.buttonListAttachedPaZones.Click += new System.EventHandler(this.buttonListAttachedPaZones_Click);
            // 
            // checkBoxReplaceExistingSet
            // 
            resources.ApplyResources(this.checkBoxReplaceExistingSet, "checkBoxReplaceExistingSet");
            this.checkBoxReplaceExistingSet.Name = "checkBoxReplaceExistingSet";
            this.checkBoxReplaceExistingSet.UseVisualStyleBackColor = true;
            // 
            // listBoxPaZones
            // 
            this.listBoxPaZones.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPaZones, "listBoxPaZones");
            this.listBoxPaZones.Name = "listBoxPaZones";
            this.listBoxPaZones.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // announcementsControlTabPage
            // 
            resources.ApplyResources(this.announcementsControlTabPage, "announcementsControlTabPage");
            this.announcementsControlTabPage.Controls.Add(this.button5);
            this.announcementsControlTabPage.Controls.Add(this.groupBox3);
            this.announcementsControlTabPage.Controls.Add(this.tabControl3);
            this.announcementsControlTabPage.Controls.Add(this.groupBox4);
            this.announcementsControlTabPage.Controls.Add(this.dataGridViewDVAPaZoneIds);
            this.announcementsControlTabPage.Controls.Add(this.buttonListPaZoneIds_DVA);
            this.announcementsControlTabPage.Name = "announcementsControlTabPage";
            this.announcementsControlTabPage.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelCustomPriority);
            this.groupBox3.Controls.Add(this.radioButtonCustomPriority);
            this.groupBox3.Controls.Add(this.trackBarCustomPriority);
            this.groupBox3.Controls.Add(this.radioButtonSourcePriority);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // labelCustomPriority
            // 
            resources.ApplyResources(this.labelCustomPriority, "labelCustomPriority");
            this.labelCustomPriority.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelCustomPriority.Name = "labelCustomPriority";
            // 
            // radioButtonCustomPriority
            // 
            resources.ApplyResources(this.radioButtonCustomPriority, "radioButtonCustomPriority");
            this.radioButtonCustomPriority.Name = "radioButtonCustomPriority";
            this.radioButtonCustomPriority.UseVisualStyleBackColor = true;
            this.radioButtonCustomPriority.CheckedChanged += new System.EventHandler(this.radioButtonCustomPriority_CheckedChanged);
            // 
            // trackBarCustomPriority
            // 
            resources.ApplyResources(this.trackBarCustomPriority, "trackBarCustomPriority");
            this.trackBarCustomPriority.BackColor = System.Drawing.SystemColors.ControlLight;
            this.trackBarCustomPriority.LargeChange = 2;
            this.trackBarCustomPriority.Maximum = 1000;
            this.trackBarCustomPriority.Name = "trackBarCustomPriority";
            this.trackBarCustomPriority.SmallChange = 2;
            this.trackBarCustomPriority.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarCustomPriority.Value = 500;
            this.trackBarCustomPriority.Scroll += new System.EventHandler(this.trackBarCustomPriority_Scroll);
            // 
            // radioButtonSourcePriority
            // 
            resources.ApplyResources(this.radioButtonSourcePriority, "radioButtonSourcePriority");
            this.radioButtonSourcePriority.Checked = true;
            this.radioButtonSourcePriority.Name = "radioButtonSourcePriority";
            this.radioButtonSourcePriority.TabStop = true;
            this.radioButtonSourcePriority.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPageDVA);
            this.tabControl3.Controls.Add(this.tabPageTTS);
            this.tabControl3.Controls.Add(this.tabPageDisplay);
            resources.ApplyResources(this.tabControl3, "tabControl3");
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            // 
            // tabPageDVA
            // 
            this.tabPageDVA.Controls.Add(this.buttonGetDictionaryContentsURI);
            this.tabPageDVA.Controls.Add(this.buttonUpdateDictionary);
            this.tabPageDVA.Controls.Add(this.buttonGetDictionaryChangeItems);
            this.tabPageDVA.Controls.Add(this.labelDVAGain);
            this.tabPageDVA.Controls.Add(this.trackBarDVAGain);
            this.tabPageDVA.Controls.Add(this.buttonPlayDVA);
            this.tabPageDVA.Controls.Add(this.dataGridViewDictionaryChangeset);
            this.tabPageDVA.Controls.Add(this.buttonGetDictionaryItems);
            this.tabPageDVA.Controls.Add(this.dataGridViewDictionaryItems);
            resources.ApplyResources(this.tabPageDVA, "tabPageDVA");
            this.tabPageDVA.Name = "tabPageDVA";
            this.tabPageDVA.UseVisualStyleBackColor = true;
            // 
            // buttonGetDictionaryContentsURI
            // 
            resources.ApplyResources(this.buttonGetDictionaryContentsURI, "buttonGetDictionaryContentsURI");
            this.buttonGetDictionaryContentsURI.Name = "buttonGetDictionaryContentsURI";
            this.buttonGetDictionaryContentsURI.UseVisualStyleBackColor = true;
            this.buttonGetDictionaryContentsURI.Click += new System.EventHandler(this.buttonGetDictionaryContentsURI_Click);
            // 
            // buttonUpdateDictionary
            // 
            resources.ApplyResources(this.buttonUpdateDictionary, "buttonUpdateDictionary");
            this.buttonUpdateDictionary.Name = "buttonUpdateDictionary";
            this.buttonUpdateDictionary.UseVisualStyleBackColor = true;
            this.buttonUpdateDictionary.Click += new System.EventHandler(this.buttonUpdateDictionary_Click);
            // 
            // buttonGetDictionaryChangeItems
            // 
            resources.ApplyResources(this.buttonGetDictionaryChangeItems, "buttonGetDictionaryChangeItems");
            this.buttonGetDictionaryChangeItems.Name = "buttonGetDictionaryChangeItems";
            this.buttonGetDictionaryChangeItems.UseVisualStyleBackColor = true;
            this.buttonGetDictionaryChangeItems.Click += new System.EventHandler(this.buttonGetDictionaryChangeItems_Click);
            // 
            // labelDVAGain
            // 
            resources.ApplyResources(this.labelDVAGain, "labelDVAGain");
            this.labelDVAGain.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelDVAGain.Name = "labelDVAGain";
            // 
            // trackBarDVAGain
            // 
            resources.ApplyResources(this.trackBarDVAGain, "trackBarDVAGain");
            this.trackBarDVAGain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.trackBarDVAGain.LargeChange = 2;
            this.trackBarDVAGain.Maximum = 96;
            this.trackBarDVAGain.Name = "trackBarDVAGain";
            this.trackBarDVAGain.SmallChange = 2;
            this.trackBarDVAGain.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarDVAGain.Value = 96;
            this.trackBarDVAGain.Scroll += new System.EventHandler(this.trackBarDVAGain_Scroll);
            // 
            // buttonPlayDVA
            // 
            resources.ApplyResources(this.buttonPlayDVA, "buttonPlayDVA");
            this.buttonPlayDVA.Name = "buttonPlayDVA";
            this.buttonPlayDVA.UseVisualStyleBackColor = true;
            this.buttonPlayDVA.Click += new System.EventHandler(this.buttonPlayDVA_Click);
            // 
            // dataGridViewDictionaryChangeset
            // 
            this.dataGridViewDictionaryChangeset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewDictionaryChangeset, "dataGridViewDictionaryChangeset");
            this.dataGridViewDictionaryChangeset.Name = "dataGridViewDictionaryChangeset";
            // 
            // buttonGetDictionaryItems
            // 
            resources.ApplyResources(this.buttonGetDictionaryItems, "buttonGetDictionaryItems");
            this.buttonGetDictionaryItems.Name = "buttonGetDictionaryItems";
            this.buttonGetDictionaryItems.UseVisualStyleBackColor = true;
            this.buttonGetDictionaryItems.Click += new System.EventHandler(this.buttonGetDictionaryItems_Click);
            // 
            // dataGridViewDictionaryItems
            // 
            this.dataGridViewDictionaryItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewDictionaryItems, "dataGridViewDictionaryItems");
            this.dataGridViewDictionaryItems.Name = "dataGridViewDictionaryItems";
            this.dataGridViewDictionaryItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tabPageTTS
            // 
            this.tabPageTTS.Controls.Add(this.label49);
            this.tabPageTTS.Controls.Add(this.label48);
            this.tabPageTTS.Controls.Add(this.label47);
            this.tabPageTTS.Controls.Add(this.buttonGetVocalizerList);
            this.tabPageTTS.Controls.Add(this.textBoxTTSText);
            this.tabPageTTS.Controls.Add(this.comboBoxTTSVocalizerList);
            this.tabPageTTS.Controls.Add(this.buttonPreviewTTS);
            this.tabPageTTS.Controls.Add(this.listBoxTTSAudioMessageList);
            this.tabPageTTS.Controls.Add(this.buttonClearTTS);
            this.tabPageTTS.Controls.Add(this.buttonPlayTTS);
            resources.ApplyResources(this.tabPageTTS, "tabPageTTS");
            this.tabPageTTS.Name = "tabPageTTS";
            this.tabPageTTS.UseVisualStyleBackColor = true;
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.ForeColor = System.Drawing.Color.DarkViolet;
            this.label49.Name = "label49";
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.ForeColor = System.Drawing.Color.DarkViolet;
            this.label48.Name = "label48";
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.ForeColor = System.Drawing.Color.DarkViolet;
            this.label47.Name = "label47";
            // 
            // buttonGetVocalizerList
            // 
            resources.ApplyResources(this.buttonGetVocalizerList, "buttonGetVocalizerList");
            this.buttonGetVocalizerList.ForeColor = System.Drawing.Color.Black;
            this.buttonGetVocalizerList.Name = "buttonGetVocalizerList";
            this.buttonGetVocalizerList.UseVisualStyleBackColor = true;
            this.buttonGetVocalizerList.Click += new System.EventHandler(this.buttonGetVocalizerList_Click);
            // 
            // textBoxTTSText
            // 
            resources.ApplyResources(this.textBoxTTSText, "textBoxTTSText");
            this.textBoxTTSText.Name = "textBoxTTSText";
            // 
            // comboBoxTTSVocalizerList
            // 
            this.comboBoxTTSVocalizerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTTSVocalizerList.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxTTSVocalizerList, "comboBoxTTSVocalizerList");
            this.comboBoxTTSVocalizerList.Name = "comboBoxTTSVocalizerList";
            // 
            // buttonPreviewTTS
            // 
            resources.ApplyResources(this.buttonPreviewTTS, "buttonPreviewTTS");
            this.buttonPreviewTTS.Name = "buttonPreviewTTS";
            this.buttonPreviewTTS.UseVisualStyleBackColor = true;
            this.buttonPreviewTTS.Click += new System.EventHandler(this.buttonPreviewTTS_Click);
            // 
            // listBoxTTSAudioMessageList
            // 
            this.listBoxTTSAudioMessageList.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxTTSAudioMessageList, "listBoxTTSAudioMessageList");
            this.listBoxTTSAudioMessageList.Name = "listBoxTTSAudioMessageList";
            this.listBoxTTSAudioMessageList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBoxTTSAudioMessageList_KeyUp);
            // 
            // buttonClearTTS
            // 
            resources.ApplyResources(this.buttonClearTTS, "buttonClearTTS");
            this.buttonClearTTS.Name = "buttonClearTTS";
            this.buttonClearTTS.UseVisualStyleBackColor = true;
            this.buttonClearTTS.Click += new System.EventHandler(this.buttonClearTTS_Click);
            // 
            // buttonPlayTTS
            // 
            resources.ApplyResources(this.buttonPlayTTS, "buttonPlayTTS");
            this.buttonPlayTTS.Name = "buttonPlayTTS";
            this.buttonPlayTTS.UseVisualStyleBackColor = true;
            this.buttonPlayTTS.Click += new System.EventHandler(this.buttonPlayTTS_Click);
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.label35);
            this.tabPageDisplay.Controls.Add(this.textBoxAnnouncementDisplayTemplateValue3);
            this.tabPageDisplay.Controls.Add(this.textBoxAnnouncementDisplayTemplateValue2);
            this.tabPageDisplay.Controls.Add(this.label52);
            this.tabPageDisplay.Controls.Add(this.label50);
            this.tabPageDisplay.Controls.Add(this.textBoxAnnouncementDisplayTemplateValue1);
            this.tabPageDisplay.Controls.Add(this.textBoxAnnouncementDisplayTemplateKey);
            this.tabPageDisplay.Controls.Add(this.buttonAnnouncementDisplayChangeTemplate);
            this.tabPageDisplay.Controls.Add(this.textBoxAnnouncementDisplayTemplateName);
            this.tabPageDisplay.Controls.Add(this.label51);
            resources.ApplyResources(this.tabPageDisplay, "tabPageDisplay");
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.ForeColor = System.Drawing.Color.DarkViolet;
            this.label35.Name = "label35";
            // 
            // textBoxAnnouncementDisplayTemplateValue3
            // 
            resources.ApplyResources(this.textBoxAnnouncementDisplayTemplateValue3, "textBoxAnnouncementDisplayTemplateValue3");
            this.textBoxAnnouncementDisplayTemplateValue3.Name = "textBoxAnnouncementDisplayTemplateValue3";
            // 
            // textBoxAnnouncementDisplayTemplateValue2
            // 
            resources.ApplyResources(this.textBoxAnnouncementDisplayTemplateValue2, "textBoxAnnouncementDisplayTemplateValue2");
            this.textBoxAnnouncementDisplayTemplateValue2.Name = "textBoxAnnouncementDisplayTemplateValue2";
            // 
            // label52
            // 
            resources.ApplyResources(this.label52, "label52");
            this.label52.ForeColor = System.Drawing.Color.DarkViolet;
            this.label52.Name = "label52";
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.ForeColor = System.Drawing.Color.DarkViolet;
            this.label50.Name = "label50";
            // 
            // textBoxAnnouncementDisplayTemplateValue1
            // 
            resources.ApplyResources(this.textBoxAnnouncementDisplayTemplateValue1, "textBoxAnnouncementDisplayTemplateValue1");
            this.textBoxAnnouncementDisplayTemplateValue1.Name = "textBoxAnnouncementDisplayTemplateValue1";
            // 
            // textBoxAnnouncementDisplayTemplateKey
            // 
            resources.ApplyResources(this.textBoxAnnouncementDisplayTemplateKey, "textBoxAnnouncementDisplayTemplateKey");
            this.textBoxAnnouncementDisplayTemplateKey.Name = "textBoxAnnouncementDisplayTemplateKey";
            // 
            // buttonAnnouncementDisplayChangeTemplate
            // 
            resources.ApplyResources(this.buttonAnnouncementDisplayChangeTemplate, "buttonAnnouncementDisplayChangeTemplate");
            this.buttonAnnouncementDisplayChangeTemplate.Name = "buttonAnnouncementDisplayChangeTemplate";
            this.buttonAnnouncementDisplayChangeTemplate.UseVisualStyleBackColor = true;
            this.buttonAnnouncementDisplayChangeTemplate.Click += new System.EventHandler(this.buttonAnnouncementDisplayChangeTemplate_Click);
            // 
            // textBoxAnnouncementDisplayTemplateName
            // 
            resources.ApplyResources(this.textBoxAnnouncementDisplayTemplateName, "textBoxAnnouncementDisplayTemplateName");
            this.textBoxAnnouncementDisplayTemplateName.Name = "textBoxAnnouncementDisplayTemplateName";
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.ForeColor = System.Drawing.Color.DarkViolet;
            this.label51.Name = "label51";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonDeleteDVASchedule);
            this.groupBox4.Controls.Add(this.listBoxSchedules);
            this.groupBox4.Controls.Add(this.buttonListDVASchedules);
            this.groupBox4.Controls.Add(this.buttonCreateDVASchedule);
            this.groupBox4.Controls.Add(this.comboBoxFrequency);
            this.groupBox4.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.comboBoxEndMins);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.comboBoxEndHours);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.comboBoxStartMins);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.comboBoxStartHours);
            this.groupBox4.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox4.Controls.Add(this.comboBoxDayMask);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // buttonDeleteDVASchedule
            // 
            resources.ApplyResources(this.buttonDeleteDVASchedule, "buttonDeleteDVASchedule");
            this.buttonDeleteDVASchedule.Name = "buttonDeleteDVASchedule";
            this.buttonDeleteDVASchedule.UseVisualStyleBackColor = true;
            this.buttonDeleteDVASchedule.Click += new System.EventHandler(this.buttonDeleteDVASchedule_Click);
            // 
            // listBoxSchedules
            // 
            this.listBoxSchedules.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxSchedules, "listBoxSchedules");
            this.listBoxSchedules.Name = "listBoxSchedules";
            // 
            // buttonListDVASchedules
            // 
            resources.ApplyResources(this.buttonListDVASchedules, "buttonListDVASchedules");
            this.buttonListDVASchedules.Name = "buttonListDVASchedules";
            this.buttonListDVASchedules.UseVisualStyleBackColor = true;
            this.buttonListDVASchedules.Click += new System.EventHandler(this.buttonListDVASchedules_Click);
            // 
            // buttonCreateDVASchedule
            // 
            resources.ApplyResources(this.buttonCreateDVASchedule, "buttonCreateDVASchedule");
            this.buttonCreateDVASchedule.Name = "buttonCreateDVASchedule";
            this.buttonCreateDVASchedule.UseVisualStyleBackColor = true;
            this.buttonCreateDVASchedule.Click += new System.EventHandler(this.buttonCreateDVASchedule_Click);
            // 
            // comboBoxFrequency
            // 
            this.comboBoxFrequency.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxFrequency, "comboBoxFrequency");
            this.comboBoxFrequency.Name = "comboBoxFrequency";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePickerStartDate, "dateTimePickerStartDate");
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.DarkViolet;
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DarkViolet;
            this.label1.Name = "label1";
            // 
            // comboBoxEndMins
            // 
            this.comboBoxEndMins.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxEndMins, "comboBoxEndMins");
            this.comboBoxEndMins.Name = "comboBoxEndMins";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.DarkViolet;
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.DarkViolet;
            this.label2.Name = "label2";
            // 
            // comboBoxEndHours
            // 
            this.comboBoxEndHours.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxEndHours, "comboBoxEndHours");
            this.comboBoxEndHours.Name = "comboBoxEndHours";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkViolet;
            this.label5.Name = "label5";
            // 
            // comboBoxStartMins
            // 
            this.comboBoxStartMins.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxStartMins, "comboBoxStartMins");
            this.comboBoxStartMins.Name = "comboBoxStartMins";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.DarkViolet;
            this.label6.Name = "label6";
            // 
            // comboBoxStartHours
            // 
            this.comboBoxStartHours.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxStartHours, "comboBoxStartHours");
            this.comboBoxStartHours.Name = "comboBoxStartHours";
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePickerEndDate, "dateTimePickerEndDate");
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            // 
            // comboBoxDayMask
            // 
            this.comboBoxDayMask.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxDayMask, "comboBoxDayMask");
            this.comboBoxDayMask.Name = "comboBoxDayMask";
            // 
            // dataGridViewDVAPaZoneIds
            // 
            this.dataGridViewDVAPaZoneIds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewDVAPaZoneIds, "dataGridViewDVAPaZoneIds");
            this.dataGridViewDVAPaZoneIds.Name = "dataGridViewDVAPaZoneIds";
            this.dataGridViewDVAPaZoneIds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // buttonListPaZoneIds_DVA
            // 
            resources.ApplyResources(this.buttonListPaZoneIds_DVA, "buttonListPaZoneIds_DVA");
            this.buttonListPaZoneIds_DVA.Name = "buttonListPaZoneIds_DVA";
            this.buttonListPaZoneIds_DVA.UseVisualStyleBackColor = true;
            this.buttonListPaZoneIds_DVA.Click += new System.EventHandler(this.buttonListPaZoneIds_DVA_Click);
            // 
            // callControlTabPage
            // 
            this.callControlTabPage.Controls.Add(this.buttonGetISDNTrunks);
            this.callControlTabPage.Controls.Add(this.buttonGetSIPTrunks);
            this.callControlTabPage.Controls.Add(this.label34);
            this.callControlTabPage.Controls.Add(this.comboBoxCallStatesPollingSeconds);
            this.callControlTabPage.Controls.Add(this.label26);
            this.callControlTabPage.Controls.Add(this.label27);
            this.callControlTabPage.Controls.Add(this.label28);
            this.callControlTabPage.Controls.Add(this.label25);
            this.callControlTabPage.Controls.Add(this.label24);
            this.callControlTabPage.Controls.Add(this.label23);
            this.callControlTabPage.Controls.Add(this.textBoxResumeCallOnTerminalBPartyID);
            this.callControlTabPage.Controls.Add(this.textBoxResumeCallOnTerminalAPartyID);
            this.callControlTabPage.Controls.Add(this.textBoxResumeCallOnTerminalCallID);
            this.callControlTabPage.Controls.Add(this.buttonResumeCallOnTerminal);
            this.callControlTabPage.Controls.Add(this.textBoxAnswerCallOnTerminalBPartyID);
            this.callControlTabPage.Controls.Add(this.textBoxAnswerCallOnTerminalAPartyID);
            this.callControlTabPage.Controls.Add(this.textBoxAnswerCallOnTerminalCallID);
            this.callControlTabPage.Controls.Add(this.buttonAnswerCallOnTerminal);
            this.callControlTabPage.Controls.Add(this.label22);
            this.callControlTabPage.Controls.Add(this.label20);
            this.callControlTabPage.Controls.Add(this.textBoxResumeCallCalleeID);
            this.callControlTabPage.Controls.Add(this.textBoxHoldCallCalleelID);
            this.callControlTabPage.Controls.Add(this.label19);
            this.callControlTabPage.Controls.Add(this.textBoxResumeCallID);
            this.callControlTabPage.Controls.Add(this.label17);
            this.callControlTabPage.Controls.Add(this.textBoxHoldCallID);
            this.callControlTabPage.Controls.Add(this.buttonResumeCall);
            this.callControlTabPage.Controls.Add(this.buttonHoldCall);
            this.callControlTabPage.Controls.Add(this.label21);
            this.callControlTabPage.Controls.Add(this.textBoxCreateCallID);
            this.callControlTabPage.Controls.Add(this.checkBoxCallsPolling);
            this.callControlTabPage.Controls.Add(this.listViewCalls);
            this.callControlTabPage.Controls.Add(this.label16);
            this.callControlTabPage.Controls.Add(this.label15);
            this.callControlTabPage.Controls.Add(this.label14);
            this.callControlTabPage.Controls.Add(this.textBoxCreateDestinationDeviceID);
            this.callControlTabPage.Controls.Add(this.textBoxCreateDestinationCLI);
            this.callControlTabPage.Controls.Add(this.label13);
            this.callControlTabPage.Controls.Add(this.label12);
            this.callControlTabPage.Controls.Add(this.textBoxTerminateCallBPartyID);
            this.callControlTabPage.Controls.Add(this.label11);
            this.callControlTabPage.Controls.Add(this.label10);
            this.callControlTabPage.Controls.Add(this.label9);
            this.callControlTabPage.Controls.Add(this.label8);
            this.callControlTabPage.Controls.Add(this.label7);
            this.callControlTabPage.Controls.Add(this.textBoxCreateCallBPartyID);
            this.callControlTabPage.Controls.Add(this.textBoxCreateCallAPartyID);
            this.callControlTabPage.Controls.Add(this.textBoxResetDestinationMapDeviceID);
            this.callControlTabPage.Controls.Add(this.textBoxCreateDestinationBPartyExtList);
            this.callControlTabPage.Controls.Add(this.buttonResetDestinationMap);
            this.callControlTabPage.Controls.Add(this.buttonCreateDestination);
            this.callControlTabPage.Controls.Add(this.textBoxTransferCallNewBPartyID);
            this.callControlTabPage.Controls.Add(this.textBoxTransferCallReferenceID);
            this.callControlTabPage.Controls.Add(this.textBoxTerminateCallID);
            this.callControlTabPage.Controls.Add(this.buttonTerminateCall);
            this.callControlTabPage.Controls.Add(this.buttonTransferCall);
            this.callControlTabPage.Controls.Add(this.buttonCreateCall);
            resources.ApplyResources(this.callControlTabPage, "callControlTabPage");
            this.callControlTabPage.Name = "callControlTabPage";
            this.callControlTabPage.UseVisualStyleBackColor = true;
            // 
            // buttonGetISDNTrunks
            // 
            resources.ApplyResources(this.buttonGetISDNTrunks, "buttonGetISDNTrunks");
            this.buttonGetISDNTrunks.Name = "buttonGetISDNTrunks";
            this.buttonGetISDNTrunks.UseVisualStyleBackColor = true;
            this.buttonGetISDNTrunks.Click += new System.EventHandler(this.buttonGetISDNTrunks_Click);
            // 
            // buttonGetSIPTrunks
            // 
            resources.ApplyResources(this.buttonGetSIPTrunks, "buttonGetSIPTrunks");
            this.buttonGetSIPTrunks.Name = "buttonGetSIPTrunks";
            this.buttonGetSIPTrunks.UseVisualStyleBackColor = true;
            this.buttonGetSIPTrunks.Click += new System.EventHandler(this.buttonGetSIPTrunks_Click);
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.ForeColor = System.Drawing.Color.DarkViolet;
            this.label34.Name = "label34";
            // 
            // comboBoxCallStatesPollingSeconds
            // 
            this.comboBoxCallStatesPollingSeconds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCallStatesPollingSeconds.FormattingEnabled = true;
            this.comboBoxCallStatesPollingSeconds.Items.AddRange(new object[] {
            resources.GetString("comboBoxCallStatesPollingSeconds.Items"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items1"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items2"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items3"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items4"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items5"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items6"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items7"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items8"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items9"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items10"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items11"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items12"),
            resources.GetString("comboBoxCallStatesPollingSeconds.Items13")});
            resources.ApplyResources(this.comboBoxCallStatesPollingSeconds, "comboBoxCallStatesPollingSeconds");
            this.comboBoxCallStatesPollingSeconds.Name = "comboBoxCallStatesPollingSeconds";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.ForeColor = System.Drawing.Color.DarkViolet;
            this.label26.Name = "label26";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.ForeColor = System.Drawing.Color.DarkViolet;
            this.label27.Name = "label27";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.ForeColor = System.Drawing.Color.DarkViolet;
            this.label28.Name = "label28";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.ForeColor = System.Drawing.Color.DarkViolet;
            this.label25.Name = "label25";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.ForeColor = System.Drawing.Color.DarkViolet;
            this.label24.Name = "label24";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.ForeColor = System.Drawing.Color.DarkViolet;
            this.label23.Name = "label23";
            // 
            // textBoxResumeCallOnTerminalBPartyID
            // 
            resources.ApplyResources(this.textBoxResumeCallOnTerminalBPartyID, "textBoxResumeCallOnTerminalBPartyID");
            this.textBoxResumeCallOnTerminalBPartyID.Name = "textBoxResumeCallOnTerminalBPartyID";
            // 
            // textBoxResumeCallOnTerminalAPartyID
            // 
            resources.ApplyResources(this.textBoxResumeCallOnTerminalAPartyID, "textBoxResumeCallOnTerminalAPartyID");
            this.textBoxResumeCallOnTerminalAPartyID.Name = "textBoxResumeCallOnTerminalAPartyID";
            // 
            // textBoxResumeCallOnTerminalCallID
            // 
            resources.ApplyResources(this.textBoxResumeCallOnTerminalCallID, "textBoxResumeCallOnTerminalCallID");
            this.textBoxResumeCallOnTerminalCallID.Name = "textBoxResumeCallOnTerminalCallID";
            // 
            // buttonResumeCallOnTerminal
            // 
            resources.ApplyResources(this.buttonResumeCallOnTerminal, "buttonResumeCallOnTerminal");
            this.buttonResumeCallOnTerminal.Name = "buttonResumeCallOnTerminal";
            this.buttonResumeCallOnTerminal.UseVisualStyleBackColor = true;
            this.buttonResumeCallOnTerminal.Click += new System.EventHandler(this.buttonResumeCallOnTerminal_Click);
            // 
            // textBoxAnswerCallOnTerminalBPartyID
            // 
            resources.ApplyResources(this.textBoxAnswerCallOnTerminalBPartyID, "textBoxAnswerCallOnTerminalBPartyID");
            this.textBoxAnswerCallOnTerminalBPartyID.Name = "textBoxAnswerCallOnTerminalBPartyID";
            // 
            // textBoxAnswerCallOnTerminalAPartyID
            // 
            resources.ApplyResources(this.textBoxAnswerCallOnTerminalAPartyID, "textBoxAnswerCallOnTerminalAPartyID");
            this.textBoxAnswerCallOnTerminalAPartyID.Name = "textBoxAnswerCallOnTerminalAPartyID";
            // 
            // textBoxAnswerCallOnTerminalCallID
            // 
            resources.ApplyResources(this.textBoxAnswerCallOnTerminalCallID, "textBoxAnswerCallOnTerminalCallID");
            this.textBoxAnswerCallOnTerminalCallID.Name = "textBoxAnswerCallOnTerminalCallID";
            // 
            // buttonAnswerCallOnTerminal
            // 
            resources.ApplyResources(this.buttonAnswerCallOnTerminal, "buttonAnswerCallOnTerminal");
            this.buttonAnswerCallOnTerminal.Name = "buttonAnswerCallOnTerminal";
            this.buttonAnswerCallOnTerminal.UseVisualStyleBackColor = true;
            this.buttonAnswerCallOnTerminal.Click += new System.EventHandler(this.buttonAnswerCallOnTerminal_Click);
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.ForeColor = System.Drawing.Color.DarkViolet;
            this.label22.Name = "label22";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.DarkViolet;
            this.label20.Name = "label20";
            // 
            // textBoxResumeCallCalleeID
            // 
            resources.ApplyResources(this.textBoxResumeCallCalleeID, "textBoxResumeCallCalleeID");
            this.textBoxResumeCallCalleeID.Name = "textBoxResumeCallCalleeID";
            // 
            // textBoxHoldCallCalleelID
            // 
            resources.ApplyResources(this.textBoxHoldCallCalleelID, "textBoxHoldCallCalleelID");
            this.textBoxHoldCallCalleelID.Name = "textBoxHoldCallCalleelID";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.DarkViolet;
            this.label19.Name = "label19";
            // 
            // textBoxResumeCallID
            // 
            resources.ApplyResources(this.textBoxResumeCallID, "textBoxResumeCallID");
            this.textBoxResumeCallID.Name = "textBoxResumeCallID";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.Color.DarkViolet;
            this.label17.Name = "label17";
            // 
            // textBoxHoldCallID
            // 
            resources.ApplyResources(this.textBoxHoldCallID, "textBoxHoldCallID");
            this.textBoxHoldCallID.Name = "textBoxHoldCallID";
            // 
            // buttonResumeCall
            // 
            resources.ApplyResources(this.buttonResumeCall, "buttonResumeCall");
            this.buttonResumeCall.Name = "buttonResumeCall";
            this.buttonResumeCall.UseVisualStyleBackColor = true;
            this.buttonResumeCall.Click += new System.EventHandler(this.buttonResumeCall_Click);
            // 
            // buttonHoldCall
            // 
            resources.ApplyResources(this.buttonHoldCall, "buttonHoldCall");
            this.buttonHoldCall.Name = "buttonHoldCall";
            this.buttonHoldCall.UseVisualStyleBackColor = true;
            this.buttonHoldCall.Click += new System.EventHandler(this.buttonHoldCall_Click);
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.ForeColor = System.Drawing.Color.Crimson;
            this.label21.Name = "label21";
            // 
            // textBoxCreateCallID
            // 
            resources.ApplyResources(this.textBoxCreateCallID, "textBoxCreateCallID");
            this.textBoxCreateCallID.Name = "textBoxCreateCallID";
            // 
            // checkBoxCallsPolling
            // 
            resources.ApplyResources(this.checkBoxCallsPolling, "checkBoxCallsPolling");
            this.checkBoxCallsPolling.BackColor = System.Drawing.Color.Khaki;
            this.checkBoxCallsPolling.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxCallsPolling.Name = "checkBoxCallsPolling";
            this.checkBoxCallsPolling.UseVisualStyleBackColor = false;
            this.checkBoxCallsPolling.CheckedChanged += new System.EventHandler(this.checkBoxStartCallsPolling_CheckedChanged);
            // 
            // listViewCalls
            // 
            this.listViewCalls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.listViewCalls.FullRowSelect = true;
            this.listViewCalls.GridLines = true;
            resources.ApplyResources(this.listViewCalls, "listViewCalls");
            this.listViewCalls.MultiSelect = false;
            this.listViewCalls.Name = "listViewCalls";
            this.listViewCalls.UseCompatibleStateImageBehavior = false;
            this.listViewCalls.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader7
            // 
            resources.ApplyResources(this.columnHeader7, "columnHeader7");
            // 
            // columnHeader8
            // 
            resources.ApplyResources(this.columnHeader8, "columnHeader8");
            // 
            // columnHeader9
            // 
            resources.ApplyResources(this.columnHeader9, "columnHeader9");
            // 
            // columnHeader10
            // 
            resources.ApplyResources(this.columnHeader10, "columnHeader10");
            // 
            // columnHeader11
            // 
            resources.ApplyResources(this.columnHeader11, "columnHeader11");
            // 
            // columnHeader12
            // 
            resources.ApplyResources(this.columnHeader12, "columnHeader12");
            // 
            // columnHeader13
            // 
            resources.ApplyResources(this.columnHeader13, "columnHeader13");
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ForeColor = System.Drawing.Color.DarkViolet;
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.Color.DarkViolet;
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.DarkViolet;
            this.label14.Name = "label14";
            // 
            // textBoxCreateDestinationDeviceID
            // 
            resources.ApplyResources(this.textBoxCreateDestinationDeviceID, "textBoxCreateDestinationDeviceID");
            this.textBoxCreateDestinationDeviceID.Name = "textBoxCreateDestinationDeviceID";
            // 
            // textBoxCreateDestinationCLI
            // 
            resources.ApplyResources(this.textBoxCreateDestinationCLI, "textBoxCreateDestinationCLI");
            this.textBoxCreateDestinationCLI.Name = "textBoxCreateDestinationCLI";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.Color.DarkViolet;
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ForeColor = System.Drawing.Color.DarkViolet;
            this.label12.Name = "label12";
            // 
            // textBoxTerminateCallBPartyID
            // 
            resources.ApplyResources(this.textBoxTerminateCallBPartyID, "textBoxTerminateCallBPartyID");
            this.textBoxTerminateCallBPartyID.Name = "textBoxTerminateCallBPartyID";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.DarkViolet;
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.DarkViolet;
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.DarkViolet;
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.DarkViolet;
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.DarkViolet;
            this.label7.Name = "label7";
            // 
            // textBoxCreateCallBPartyID
            // 
            resources.ApplyResources(this.textBoxCreateCallBPartyID, "textBoxCreateCallBPartyID");
            this.textBoxCreateCallBPartyID.Name = "textBoxCreateCallBPartyID";
            // 
            // textBoxCreateCallAPartyID
            // 
            resources.ApplyResources(this.textBoxCreateCallAPartyID, "textBoxCreateCallAPartyID");
            this.textBoxCreateCallAPartyID.Name = "textBoxCreateCallAPartyID";
            // 
            // textBoxResetDestinationMapDeviceID
            // 
            resources.ApplyResources(this.textBoxResetDestinationMapDeviceID, "textBoxResetDestinationMapDeviceID");
            this.textBoxResetDestinationMapDeviceID.Name = "textBoxResetDestinationMapDeviceID";
            // 
            // textBoxCreateDestinationBPartyExtList
            // 
            resources.ApplyResources(this.textBoxCreateDestinationBPartyExtList, "textBoxCreateDestinationBPartyExtList");
            this.textBoxCreateDestinationBPartyExtList.Name = "textBoxCreateDestinationBPartyExtList";
            // 
            // buttonResetDestinationMap
            // 
            resources.ApplyResources(this.buttonResetDestinationMap, "buttonResetDestinationMap");
            this.buttonResetDestinationMap.Name = "buttonResetDestinationMap";
            this.buttonResetDestinationMap.UseVisualStyleBackColor = true;
            this.buttonResetDestinationMap.Click += new System.EventHandler(this.buttonResetDestinationMap_Click);
            // 
            // buttonCreateDestination
            // 
            resources.ApplyResources(this.buttonCreateDestination, "buttonCreateDestination");
            this.buttonCreateDestination.Name = "buttonCreateDestination";
            this.buttonCreateDestination.UseVisualStyleBackColor = true;
            this.buttonCreateDestination.Click += new System.EventHandler(this.buttonCreateDestination_Click);
            // 
            // textBoxTransferCallNewBPartyID
            // 
            resources.ApplyResources(this.textBoxTransferCallNewBPartyID, "textBoxTransferCallNewBPartyID");
            this.textBoxTransferCallNewBPartyID.Name = "textBoxTransferCallNewBPartyID";
            // 
            // textBoxTransferCallReferenceID
            // 
            resources.ApplyResources(this.textBoxTransferCallReferenceID, "textBoxTransferCallReferenceID");
            this.textBoxTransferCallReferenceID.Name = "textBoxTransferCallReferenceID";
            // 
            // textBoxTerminateCallID
            // 
            resources.ApplyResources(this.textBoxTerminateCallID, "textBoxTerminateCallID");
            this.textBoxTerminateCallID.Name = "textBoxTerminateCallID";
            // 
            // buttonTerminateCall
            // 
            resources.ApplyResources(this.buttonTerminateCall, "buttonTerminateCall");
            this.buttonTerminateCall.Name = "buttonTerminateCall";
            this.buttonTerminateCall.UseVisualStyleBackColor = true;
            this.buttonTerminateCall.Click += new System.EventHandler(this.buttonTerminateCall_Click);
            // 
            // buttonTransferCall
            // 
            resources.ApplyResources(this.buttonTransferCall, "buttonTransferCall");
            this.buttonTransferCall.Name = "buttonTransferCall";
            this.buttonTransferCall.UseVisualStyleBackColor = true;
            this.buttonTransferCall.Click += new System.EventHandler(this.buttonTransferCall_Click);
            // 
            // buttonCreateCall
            // 
            resources.ApplyResources(this.buttonCreateCall, "buttonCreateCall");
            this.buttonCreateCall.Name = "buttonCreateCall";
            this.buttonCreateCall.UseVisualStyleBackColor = true;
            this.buttonCreateCall.Click += new System.EventHandler(this.buttonCreateCall_Click);
            // 
            // signallingControlTabPage
            // 
            this.signallingControlTabPage.Controls.Add(this.textBoxAvailablePlatforms);
            this.signallingControlTabPage.Controls.Add(this.label44);
            this.signallingControlTabPage.Controls.Add(this.textBoxAvailableStations);
            this.signallingControlTabPage.Controls.Add(this.textBoxAvailableVehicles);
            this.signallingControlTabPage.Controls.Add(this.textBoxAvailableLines);
            this.signallingControlTabPage.Controls.Add(this.label43);
            this.signallingControlTabPage.Controls.Add(this.label42);
            this.signallingControlTabPage.Controls.Add(this.label41);
            this.signallingControlTabPage.Controls.Add(this.labelPISServerStatus);
            this.signallingControlTabPage.Controls.Add(this.label40);
            this.signallingControlTabPage.Controls.Add(this.comboBoxSignallingInfoPollingSeconds);
            this.signallingControlTabPage.Controls.Add(this.checkBoxSignallingInfoPolling);
            resources.ApplyResources(this.signallingControlTabPage, "signallingControlTabPage");
            this.signallingControlTabPage.Name = "signallingControlTabPage";
            this.signallingControlTabPage.UseVisualStyleBackColor = true;
            // 
            // textBoxAvailablePlatforms
            // 
            resources.ApplyResources(this.textBoxAvailablePlatforms, "textBoxAvailablePlatforms");
            this.textBoxAvailablePlatforms.Name = "textBoxAvailablePlatforms";
            this.textBoxAvailablePlatforms.ReadOnly = true;
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.ForeColor = System.Drawing.Color.DarkViolet;
            this.label44.Name = "label44";
            // 
            // textBoxAvailableStations
            // 
            resources.ApplyResources(this.textBoxAvailableStations, "textBoxAvailableStations");
            this.textBoxAvailableStations.Name = "textBoxAvailableStations";
            this.textBoxAvailableStations.ReadOnly = true;
            // 
            // textBoxAvailableVehicles
            // 
            resources.ApplyResources(this.textBoxAvailableVehicles, "textBoxAvailableVehicles");
            this.textBoxAvailableVehicles.Name = "textBoxAvailableVehicles";
            this.textBoxAvailableVehicles.ReadOnly = true;
            // 
            // textBoxAvailableLines
            // 
            resources.ApplyResources(this.textBoxAvailableLines, "textBoxAvailableLines");
            this.textBoxAvailableLines.Name = "textBoxAvailableLines";
            this.textBoxAvailableLines.ReadOnly = true;
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.ForeColor = System.Drawing.Color.DarkViolet;
            this.label43.Name = "label43";
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.ForeColor = System.Drawing.Color.DarkViolet;
            this.label42.Name = "label42";
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.ForeColor = System.Drawing.Color.DarkViolet;
            this.label41.Name = "label41";
            // 
            // labelPISServerStatus
            // 
            resources.ApplyResources(this.labelPISServerStatus, "labelPISServerStatus");
            this.labelPISServerStatus.ForeColor = System.Drawing.Color.Red;
            this.labelPISServerStatus.Name = "labelPISServerStatus";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.ForeColor = System.Drawing.Color.DarkViolet;
            this.label40.Name = "label40";
            // 
            // comboBoxSignallingInfoPollingSeconds
            // 
            this.comboBoxSignallingInfoPollingSeconds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSignallingInfoPollingSeconds.FormattingEnabled = true;
            this.comboBoxSignallingInfoPollingSeconds.Items.AddRange(new object[] {
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items1"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items2"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items3"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items4"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items5"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items6"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items7"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items8"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items9"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items10"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items11"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items12"),
            resources.GetString("comboBoxSignallingInfoPollingSeconds.Items13")});
            resources.ApplyResources(this.comboBoxSignallingInfoPollingSeconds, "comboBoxSignallingInfoPollingSeconds");
            this.comboBoxSignallingInfoPollingSeconds.Name = "comboBoxSignallingInfoPollingSeconds";
            // 
            // checkBoxSignallingInfoPolling
            // 
            resources.ApplyResources(this.checkBoxSignallingInfoPolling, "checkBoxSignallingInfoPolling");
            this.checkBoxSignallingInfoPolling.BackColor = System.Drawing.Color.Khaki;
            this.checkBoxSignallingInfoPolling.Name = "checkBoxSignallingInfoPolling";
            this.checkBoxSignallingInfoPolling.UseVisualStyleBackColor = false;
            this.checkBoxSignallingInfoPolling.CheckedChanged += new System.EventHandler(this.checkBoxSignallingInfoPolling_CheckedChanged);
            // 
            // alarmManagementTabPage
            // 
            this.alarmManagementTabPage.Controls.Add(this.alarmsListView);
            this.alarmManagementTabPage.Controls.Add(this.alarmAcknowledgeButton);
            this.alarmManagementTabPage.Controls.Add(this.getAlarmsButton);
            this.alarmManagementTabPage.Controls.Add(this.alarmManagementCheckBox);
            resources.ApplyResources(this.alarmManagementTabPage, "alarmManagementTabPage");
            this.alarmManagementTabPage.Name = "alarmManagementTabPage";
            this.alarmManagementTabPage.UseVisualStyleBackColor = true;
            // 
            // alarmsListView
            // 
            resources.ApplyResources(this.alarmsListView, "alarmsListView");
            this.alarmsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader25});
            this.alarmsListView.FullRowSelect = true;
            this.alarmsListView.GridLines = true;
            this.alarmsListView.HideSelection = false;
            this.alarmsListView.MultiSelect = false;
            this.alarmsListView.Name = "alarmsListView";
            this.alarmsListView.UseCompatibleStateImageBehavior = false;
            this.alarmsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // columnHeader14
            // 
            resources.ApplyResources(this.columnHeader14, "columnHeader14");
            // 
            // columnHeader15
            // 
            resources.ApplyResources(this.columnHeader15, "columnHeader15");
            // 
            // columnHeader16
            // 
            resources.ApplyResources(this.columnHeader16, "columnHeader16");
            // 
            // columnHeader17
            // 
            resources.ApplyResources(this.columnHeader17, "columnHeader17");
            // 
            // columnHeader18
            // 
            resources.ApplyResources(this.columnHeader18, "columnHeader18");
            // 
            // columnHeader22
            // 
            resources.ApplyResources(this.columnHeader22, "columnHeader22");
            // 
            // columnHeader23
            // 
            resources.ApplyResources(this.columnHeader23, "columnHeader23");
            // 
            // columnHeader24
            // 
            resources.ApplyResources(this.columnHeader24, "columnHeader24");
            // 
            // columnHeader25
            // 
            resources.ApplyResources(this.columnHeader25, "columnHeader25");
            // 
            // alarmAcknowledgeButton
            // 
            resources.ApplyResources(this.alarmAcknowledgeButton, "alarmAcknowledgeButton");
            this.alarmAcknowledgeButton.Name = "alarmAcknowledgeButton";
            this.alarmAcknowledgeButton.UseVisualStyleBackColor = true;
            this.alarmAcknowledgeButton.Click += new System.EventHandler(this.alarmAcknowledgeButton_Click);
            // 
            // getAlarmsButton
            // 
            resources.ApplyResources(this.getAlarmsButton, "getAlarmsButton");
            this.getAlarmsButton.Name = "getAlarmsButton";
            this.getAlarmsButton.UseVisualStyleBackColor = true;
            this.getAlarmsButton.Click += new System.EventHandler(this.getAlarmsButton_Click);
            // 
            // alarmManagementCheckBox
            // 
            resources.ApplyResources(this.alarmManagementCheckBox, "alarmManagementCheckBox");
            this.alarmManagementCheckBox.Name = "alarmManagementCheckBox";
            this.alarmManagementCheckBox.UseVisualStyleBackColor = true;
            this.alarmManagementCheckBox.CheckedChanged += new System.EventHandler(this.alarmManagementCheckBox_CheckedChanged);
            // 
            // bucharestOA0784TabPage
            // 
            this.bucharestOA0784TabPage.Controls.Add(this.buttonUploadFile);
            this.bucharestOA0784TabPage.Controls.Add(this.groupBox1);
            this.bucharestOA0784TabPage.Controls.Add(this.label33);
            this.bucharestOA0784TabPage.Controls.Add(this.label32);
            this.bucharestOA0784TabPage.Controls.Add(this.label31);
            this.bucharestOA0784TabPage.Controls.Add(this.label30);
            this.bucharestOA0784TabPage.Controls.Add(this.label29);
            this.bucharestOA0784TabPage.Controls.Add(this.textBoxHMIInputSave);
            this.bucharestOA0784TabPage.Controls.Add(this.textBoxHMIInputStartStation);
            this.bucharestOA0784TabPage.Controls.Add(this.textBoxHMIInputDestination);
            this.bucharestOA0784TabPage.Controls.Add(this.textBoxHMIInputLine);
            this.bucharestOA0784TabPage.Controls.Add(this.listBoxHMIList);
            this.bucharestOA0784TabPage.Controls.Add(this.textBoxHMIInputCode);
            this.bucharestOA0784TabPage.Controls.Add(this.buttonDeleteHMIContent);
            this.bucharestOA0784TabPage.Controls.Add(this.buttonUpdateHMIData);
            resources.ApplyResources(this.bucharestOA0784TabPage, "bucharestOA0784TabPage");
            this.bucharestOA0784TabPage.Name = "bucharestOA0784TabPage";
            this.bucharestOA0784TabPage.UseVisualStyleBackColor = true;
            // 
            // buttonUploadFile
            // 
            resources.ApplyResources(this.buttonUploadFile, "buttonUploadFile");
            this.buttonUploadFile.Name = "buttonUploadFile";
            this.buttonUploadFile.UseVisualStyleBackColor = true;
            this.buttonUploadFile.Click += new System.EventHandler(this.buttonUploadFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCP1ActiveStatus);
            this.groupBox1.Controls.Add(this.buttonCP2AutoInfoStatus);
            this.groupBox1.Controls.Add(this.labelAutoInfoStatus);
            this.groupBox1.Controls.Add(this.buttonCP1AutoInfoStatus);
            this.groupBox1.Controls.Add(this.buttonCP2ActiveStatus);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonCP1ActiveStatus
            // 
            this.buttonCP1ActiveStatus.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.buttonCP1ActiveStatus, "buttonCP1ActiveStatus");
            this.buttonCP1ActiveStatus.Name = "buttonCP1ActiveStatus";
            this.buttonCP1ActiveStatus.UseVisualStyleBackColor = false;
            // 
            // buttonCP2AutoInfoStatus
            // 
            this.buttonCP2AutoInfoStatus.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.buttonCP2AutoInfoStatus, "buttonCP2AutoInfoStatus");
            this.buttonCP2AutoInfoStatus.Name = "buttonCP2AutoInfoStatus";
            this.buttonCP2AutoInfoStatus.UseVisualStyleBackColor = false;
            // 
            // labelAutoInfoStatus
            // 
            resources.ApplyResources(this.labelAutoInfoStatus, "labelAutoInfoStatus");
            this.labelAutoInfoStatus.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelAutoInfoStatus.Name = "labelAutoInfoStatus";
            // 
            // buttonCP1AutoInfoStatus
            // 
            this.buttonCP1AutoInfoStatus.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.buttonCP1AutoInfoStatus, "buttonCP1AutoInfoStatus");
            this.buttonCP1AutoInfoStatus.Name = "buttonCP1AutoInfoStatus";
            this.buttonCP1AutoInfoStatus.UseVisualStyleBackColor = false;
            // 
            // buttonCP2ActiveStatus
            // 
            this.buttonCP2ActiveStatus.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.buttonCP2ActiveStatus, "buttonCP2ActiveStatus");
            this.buttonCP2ActiveStatus.Name = "buttonCP2ActiveStatus";
            this.buttonCP2ActiveStatus.UseVisualStyleBackColor = false;
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.ForeColor = System.Drawing.Color.DarkViolet;
            this.label33.Name = "label33";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.ForeColor = System.Drawing.Color.DarkViolet;
            this.label32.Name = "label32";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.ForeColor = System.Drawing.Color.DarkViolet;
            this.label31.Name = "label31";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.ForeColor = System.Drawing.Color.DarkViolet;
            this.label30.Name = "label30";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.ForeColor = System.Drawing.Color.DarkViolet;
            this.label29.Name = "label29";
            // 
            // textBoxHMIInputSave
            // 
            resources.ApplyResources(this.textBoxHMIInputSave, "textBoxHMIInputSave");
            this.textBoxHMIInputSave.Name = "textBoxHMIInputSave";
            // 
            // textBoxHMIInputStartStation
            // 
            resources.ApplyResources(this.textBoxHMIInputStartStation, "textBoxHMIInputStartStation");
            this.textBoxHMIInputStartStation.Name = "textBoxHMIInputStartStation";
            // 
            // textBoxHMIInputDestination
            // 
            resources.ApplyResources(this.textBoxHMIInputDestination, "textBoxHMIInputDestination");
            this.textBoxHMIInputDestination.Name = "textBoxHMIInputDestination";
            // 
            // textBoxHMIInputLine
            // 
            resources.ApplyResources(this.textBoxHMIInputLine, "textBoxHMIInputLine");
            this.textBoxHMIInputLine.Name = "textBoxHMIInputLine";
            // 
            // listBoxHMIList
            // 
            this.listBoxHMIList.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxHMIList, "listBoxHMIList");
            this.listBoxHMIList.Name = "listBoxHMIList";
            // 
            // textBoxHMIInputCode
            // 
            resources.ApplyResources(this.textBoxHMIInputCode, "textBoxHMIInputCode");
            this.textBoxHMIInputCode.Name = "textBoxHMIInputCode";
            // 
            // buttonDeleteHMIContent
            // 
            resources.ApplyResources(this.buttonDeleteHMIContent, "buttonDeleteHMIContent");
            this.buttonDeleteHMIContent.Name = "buttonDeleteHMIContent";
            this.buttonDeleteHMIContent.UseVisualStyleBackColor = true;
            this.buttonDeleteHMIContent.Click += new System.EventHandler(this.buttonDeleteHMIContent_Click);
            // 
            // buttonUpdateHMIData
            // 
            resources.ApplyResources(this.buttonUpdateHMIData, "buttonUpdateHMIData");
            this.buttonUpdateHMIData.Name = "buttonUpdateHMIData";
            this.buttonUpdateHMIData.UseVisualStyleBackColor = true;
            this.buttonUpdateHMIData.Click += new System.EventHandler(this.buttonUpdateHMIData_Click);
            // 
            // waratahOA0979TabPage
            // 
            this.waratahOA0979TabPage.Controls.Add(this.textBoxWaratahCommercialRadioInterrupt);
            this.waratahOA0979TabPage.Controls.Add(this.buttonWaratahSetCommercialRadioInterrupt);
            this.waratahOA0979TabPage.Controls.Add(this.groupBox7);
            this.waratahOA0979TabPage.Controls.Add(this.groupBox6);
            this.waratahOA0979TabPage.Controls.Add(this.groupBox5);
            this.waratahOA0979TabPage.Controls.Add(this.checkBoxWaratahEXTPASide2);
            this.waratahOA0979TabPage.Controls.Add(this.checkBoxWaratahEXTPASide1);
            this.waratahOA0979TabPage.Controls.Add(this.buttonWaratahCancelInterruptionToCommercialRadio);
            this.waratahOA0979TabPage.Controls.Add(this.buttonWaratahInterruptCommercialRadio);
            this.waratahOA0979TabPage.Controls.Add(this.checkBoxWarathaEnableDisableCommercialRadio);
            resources.ApplyResources(this.waratahOA0979TabPage, "waratahOA0979TabPage");
            this.waratahOA0979TabPage.Name = "waratahOA0979TabPage";
            this.waratahOA0979TabPage.UseVisualStyleBackColor = true;
            // 
            // textBoxWaratahCommercialRadioInterrupt
            // 
            resources.ApplyResources(this.textBoxWaratahCommercialRadioInterrupt, "textBoxWaratahCommercialRadioInterrupt");
            this.textBoxWaratahCommercialRadioInterrupt.Name = "textBoxWaratahCommercialRadioInterrupt";
            this.textBoxWaratahCommercialRadioInterrupt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaratahCommercialRadioInterrupt_KeyPress);
            // 
            // buttonWaratahSetCommercialRadioInterrupt
            // 
            resources.ApplyResources(this.buttonWaratahSetCommercialRadioInterrupt, "buttonWaratahSetCommercialRadioInterrupt");
            this.buttonWaratahSetCommercialRadioInterrupt.Name = "buttonWaratahSetCommercialRadioInterrupt";
            this.buttonWaratahSetCommercialRadioInterrupt.UseVisualStyleBackColor = true;
            this.buttonWaratahSetCommercialRadioInterrupt.Click += new System.EventHandler(this.buttonWaratahSetCommercialRadioInterrupt_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonWaratahCSPKR8Monitoring);
            this.groupBox7.Controls.Add(this.buttonWaratahCSPKR1Monitoring);
            this.groupBox7.ForeColor = System.Drawing.Color.DarkViolet;
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // buttonWaratahCSPKR8Monitoring
            // 
            resources.ApplyResources(this.buttonWaratahCSPKR8Monitoring, "buttonWaratahCSPKR8Monitoring");
            this.buttonWaratahCSPKR8Monitoring.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonWaratahCSPKR8Monitoring.Name = "buttonWaratahCSPKR8Monitoring";
            this.buttonWaratahCSPKR8Monitoring.UseVisualStyleBackColor = true;
            this.buttonWaratahCSPKR8Monitoring.Click += new System.EventHandler(this.buttonWaratahCSPKR8Monitoring_Click);
            // 
            // buttonWaratahCSPKR1Monitoring
            // 
            resources.ApplyResources(this.buttonWaratahCSPKR1Monitoring, "buttonWaratahCSPKR1Monitoring");
            this.buttonWaratahCSPKR1Monitoring.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonWaratahCSPKR1Monitoring.Name = "buttonWaratahCSPKR1Monitoring";
            this.buttonWaratahCSPKR1Monitoring.UseVisualStyleBackColor = true;
            this.buttonWaratahCSPKR1Monitoring.Click += new System.EventHandler(this.buttonWaratahCSPKR1Monitoring_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonTCXC8_DOUT6);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DOUT5);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DOUT4);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DOUT3);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DOUT2);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DOUT1);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN8);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN7);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN6);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN5);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN4);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN3);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN2);
            this.groupBox6.Controls.Add(this.buttonTCXC8_DIN1);
            this.groupBox6.ForeColor = System.Drawing.Color.DarkViolet;
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // buttonTCXC8_DOUT6
            // 
            this.buttonTCXC8_DOUT6.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DOUT6, "buttonTCXC8_DOUT6");
            this.buttonTCXC8_DOUT6.Name = "buttonTCXC8_DOUT6";
            this.buttonTCXC8_DOUT6.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DOUT5
            // 
            this.buttonTCXC8_DOUT5.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DOUT5, "buttonTCXC8_DOUT5");
            this.buttonTCXC8_DOUT5.Name = "buttonTCXC8_DOUT5";
            this.buttonTCXC8_DOUT5.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DOUT4
            // 
            this.buttonTCXC8_DOUT4.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DOUT4, "buttonTCXC8_DOUT4");
            this.buttonTCXC8_DOUT4.Name = "buttonTCXC8_DOUT4";
            this.buttonTCXC8_DOUT4.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DOUT3
            // 
            this.buttonTCXC8_DOUT3.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DOUT3, "buttonTCXC8_DOUT3");
            this.buttonTCXC8_DOUT3.Name = "buttonTCXC8_DOUT3";
            this.buttonTCXC8_DOUT3.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DOUT2
            // 
            this.buttonTCXC8_DOUT2.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DOUT2, "buttonTCXC8_DOUT2");
            this.buttonTCXC8_DOUT2.Name = "buttonTCXC8_DOUT2";
            this.buttonTCXC8_DOUT2.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DOUT1
            // 
            this.buttonTCXC8_DOUT1.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DOUT1, "buttonTCXC8_DOUT1");
            this.buttonTCXC8_DOUT1.Name = "buttonTCXC8_DOUT1";
            this.buttonTCXC8_DOUT1.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN8
            // 
            this.buttonTCXC8_DIN8.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN8, "buttonTCXC8_DIN8");
            this.buttonTCXC8_DIN8.Name = "buttonTCXC8_DIN8";
            this.buttonTCXC8_DIN8.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN7
            // 
            this.buttonTCXC8_DIN7.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN7, "buttonTCXC8_DIN7");
            this.buttonTCXC8_DIN7.Name = "buttonTCXC8_DIN7";
            this.buttonTCXC8_DIN7.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN6
            // 
            this.buttonTCXC8_DIN6.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN6, "buttonTCXC8_DIN6");
            this.buttonTCXC8_DIN6.Name = "buttonTCXC8_DIN6";
            this.buttonTCXC8_DIN6.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN5
            // 
            this.buttonTCXC8_DIN5.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN5, "buttonTCXC8_DIN5");
            this.buttonTCXC8_DIN5.Name = "buttonTCXC8_DIN5";
            this.buttonTCXC8_DIN5.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN4
            // 
            this.buttonTCXC8_DIN4.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN4, "buttonTCXC8_DIN4");
            this.buttonTCXC8_DIN4.Name = "buttonTCXC8_DIN4";
            this.buttonTCXC8_DIN4.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN3
            // 
            this.buttonTCXC8_DIN3.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN3, "buttonTCXC8_DIN3");
            this.buttonTCXC8_DIN3.Name = "buttonTCXC8_DIN3";
            this.buttonTCXC8_DIN3.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN2
            // 
            this.buttonTCXC8_DIN2.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN2, "buttonTCXC8_DIN2");
            this.buttonTCXC8_DIN2.Name = "buttonTCXC8_DIN2";
            this.buttonTCXC8_DIN2.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC8_DIN1
            // 
            this.buttonTCXC8_DIN1.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC8_DIN1, "buttonTCXC8_DIN1");
            this.buttonTCXC8_DIN1.Name = "buttonTCXC8_DIN1";
            this.buttonTCXC8_DIN1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonTCXC1_DOUT6);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DOUT5);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DOUT4);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DOUT3);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DOUT2);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DOUT1);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN8);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN7);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN6);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN5);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN4);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN3);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN2);
            this.groupBox5.Controls.Add(this.buttonTCXC1_DIN1);
            this.groupBox5.ForeColor = System.Drawing.Color.DarkViolet;
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // buttonTCXC1_DOUT6
            // 
            this.buttonTCXC1_DOUT6.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DOUT6, "buttonTCXC1_DOUT6");
            this.buttonTCXC1_DOUT6.Name = "buttonTCXC1_DOUT6";
            this.buttonTCXC1_DOUT6.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DOUT5
            // 
            this.buttonTCXC1_DOUT5.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DOUT5, "buttonTCXC1_DOUT5");
            this.buttonTCXC1_DOUT5.Name = "buttonTCXC1_DOUT5";
            this.buttonTCXC1_DOUT5.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DOUT4
            // 
            this.buttonTCXC1_DOUT4.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DOUT4, "buttonTCXC1_DOUT4");
            this.buttonTCXC1_DOUT4.Name = "buttonTCXC1_DOUT4";
            this.buttonTCXC1_DOUT4.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DOUT3
            // 
            this.buttonTCXC1_DOUT3.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DOUT3, "buttonTCXC1_DOUT3");
            this.buttonTCXC1_DOUT3.Name = "buttonTCXC1_DOUT3";
            this.buttonTCXC1_DOUT3.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DOUT2
            // 
            this.buttonTCXC1_DOUT2.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DOUT2, "buttonTCXC1_DOUT2");
            this.buttonTCXC1_DOUT2.Name = "buttonTCXC1_DOUT2";
            this.buttonTCXC1_DOUT2.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DOUT1
            // 
            this.buttonTCXC1_DOUT1.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DOUT1, "buttonTCXC1_DOUT1");
            this.buttonTCXC1_DOUT1.Name = "buttonTCXC1_DOUT1";
            this.buttonTCXC1_DOUT1.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN8
            // 
            this.buttonTCXC1_DIN8.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN8, "buttonTCXC1_DIN8");
            this.buttonTCXC1_DIN8.Name = "buttonTCXC1_DIN8";
            this.buttonTCXC1_DIN8.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN7
            // 
            this.buttonTCXC1_DIN7.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN7, "buttonTCXC1_DIN7");
            this.buttonTCXC1_DIN7.Name = "buttonTCXC1_DIN7";
            this.buttonTCXC1_DIN7.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN6
            // 
            this.buttonTCXC1_DIN6.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN6, "buttonTCXC1_DIN6");
            this.buttonTCXC1_DIN6.Name = "buttonTCXC1_DIN6";
            this.buttonTCXC1_DIN6.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN5
            // 
            this.buttonTCXC1_DIN5.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN5, "buttonTCXC1_DIN5");
            this.buttonTCXC1_DIN5.Name = "buttonTCXC1_DIN5";
            this.buttonTCXC1_DIN5.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN4
            // 
            this.buttonTCXC1_DIN4.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN4, "buttonTCXC1_DIN4");
            this.buttonTCXC1_DIN4.Name = "buttonTCXC1_DIN4";
            this.buttonTCXC1_DIN4.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN3
            // 
            this.buttonTCXC1_DIN3.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN3, "buttonTCXC1_DIN3");
            this.buttonTCXC1_DIN3.Name = "buttonTCXC1_DIN3";
            this.buttonTCXC1_DIN3.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN2
            // 
            this.buttonTCXC1_DIN2.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN2, "buttonTCXC1_DIN2");
            this.buttonTCXC1_DIN2.Name = "buttonTCXC1_DIN2";
            this.buttonTCXC1_DIN2.UseVisualStyleBackColor = true;
            // 
            // buttonTCXC1_DIN1
            // 
            this.buttonTCXC1_DIN1.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonTCXC1_DIN1, "buttonTCXC1_DIN1");
            this.buttonTCXC1_DIN1.Name = "buttonTCXC1_DIN1";
            this.buttonTCXC1_DIN1.UseVisualStyleBackColor = true;
            // 
            // checkBoxWaratahEXTPASide2
            // 
            this.checkBoxWaratahEXTPASide2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxWaratahEXTPASide2, "checkBoxWaratahEXTPASide2");
            this.checkBoxWaratahEXTPASide2.Name = "checkBoxWaratahEXTPASide2";
            this.checkBoxWaratahEXTPASide2.TabStop = false;
            this.checkBoxWaratahEXTPASide2.UseVisualStyleBackColor = false;
            this.checkBoxWaratahEXTPASide2.CheckedChanged += new System.EventHandler(this.checkBoxWaratahEXTPASide2_CheckedChanged);
            // 
            // checkBoxWaratahEXTPASide1
            // 
            this.checkBoxWaratahEXTPASide1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxWaratahEXTPASide1, "checkBoxWaratahEXTPASide1");
            this.checkBoxWaratahEXTPASide1.Name = "checkBoxWaratahEXTPASide1";
            this.checkBoxWaratahEXTPASide1.UseVisualStyleBackColor = false;
            this.checkBoxWaratahEXTPASide1.CheckedChanged += new System.EventHandler(this.checkBoxWaratahEXTPASide1_CheckedChanged);
            // 
            // buttonWaratahCancelInterruptionToCommercialRadio
            // 
            resources.ApplyResources(this.buttonWaratahCancelInterruptionToCommercialRadio, "buttonWaratahCancelInterruptionToCommercialRadio");
            this.buttonWaratahCancelInterruptionToCommercialRadio.Name = "buttonWaratahCancelInterruptionToCommercialRadio";
            this.buttonWaratahCancelInterruptionToCommercialRadio.UseVisualStyleBackColor = true;
            this.buttonWaratahCancelInterruptionToCommercialRadio.Click += new System.EventHandler(this.buttonWaratahCancelInterruptionToCommercialRadio_Click);
            // 
            // buttonWaratahInterruptCommercialRadio
            // 
            resources.ApplyResources(this.buttonWaratahInterruptCommercialRadio, "buttonWaratahInterruptCommercialRadio");
            this.buttonWaratahInterruptCommercialRadio.Name = "buttonWaratahInterruptCommercialRadio";
            this.buttonWaratahInterruptCommercialRadio.UseVisualStyleBackColor = true;
            this.buttonWaratahInterruptCommercialRadio.Click += new System.EventHandler(this.buttonWaratahInterruptCommercialRadio_Click);
            // 
            // checkBoxWarathaEnableDisableCommercialRadio
            // 
            resources.ApplyResources(this.checkBoxWarathaEnableDisableCommercialRadio, "checkBoxWarathaEnableDisableCommercialRadio");
            this.checkBoxWarathaEnableDisableCommercialRadio.BackColor = System.Drawing.Color.Red;
            this.checkBoxWarathaEnableDisableCommercialRadio.Name = "checkBoxWarathaEnableDisableCommercialRadio";
            this.checkBoxWarathaEnableDisableCommercialRadio.UseVisualStyleBackColor = false;
            this.checkBoxWarathaEnableDisableCommercialRadio.CheckedChanged += new System.EventHandler(this.checkBoxWarathaEnableDisableCommercialRadio_CheckedChanged);
            // 
            // listBoxDeviceStateEvents
            // 
            resources.ApplyResources(this.listBoxDeviceStateEvents, "listBoxDeviceStateEvents");
            this.listBoxDeviceStateEvents.FormattingEnabled = true;
            this.listBoxDeviceStateEvents.Name = "listBoxDeviceStateEvents";
            this.listBoxDeviceStateEvents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxDeviceStateEvents_MouseUp);
            // 
            // listBoxPAEvents
            // 
            resources.ApplyResources(this.listBoxPAEvents, "listBoxPAEvents");
            this.listBoxPAEvents.FormattingEnabled = true;
            this.listBoxPAEvents.Name = "listBoxPAEvents";
            this.listBoxPAEvents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxPAEvents_MouseUp);
            // 
            // listBoxCallEvents
            // 
            resources.ApplyResources(this.listBoxCallEvents, "listBoxCallEvents");
            this.listBoxCallEvents.FormattingEnabled = true;
            this.listBoxCallEvents.Name = "listBoxCallEvents";
            this.listBoxCallEvents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxCallEvents_MouseUp);
            // 
            // mainMenuStrip
            // 
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.statusToolStripMenuItem,
            this.controlToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            resources.ApplyResources(this.systemToolStripMenuItem, "systemToolStripMenuItem");
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToKTMBToolStripMenuItem,
            this.connectToKLMonorailToolStripMenuItem,
            this.connectToBucharestToolStripMenuItem,
            this.connectToGCRTToolStripMenuItem,
            this.connectToHKToolStripMenuItem,
            this.connectToSLRToolStripMenuItem,
            this.connectToWaratahToolStripMenuItem,
            this.connectToMetrolinxToolStripMenuItem,
            this.connectToOttawaLightRailToolStripMenuItem,
            this.toolStripSeparator2,
            this.connectToASToolStripMenuItem});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            resources.ApplyResources(this.connectToolStripMenuItem, "connectToolStripMenuItem");
            // 
            // connectToKTMBToolStripMenuItem
            // 
            this.connectToKTMBToolStripMenuItem.Name = "connectToKTMBToolStripMenuItem";
            resources.ApplyResources(this.connectToKTMBToolStripMenuItem, "connectToKTMBToolStripMenuItem");
            this.connectToKTMBToolStripMenuItem.Click += new System.EventHandler(this.connectToKTMBToolStripMenuItem_Click);
            // 
            // connectToKLMonorailToolStripMenuItem
            // 
            this.connectToKLMonorailToolStripMenuItem.Name = "connectToKLMonorailToolStripMenuItem";
            resources.ApplyResources(this.connectToKLMonorailToolStripMenuItem, "connectToKLMonorailToolStripMenuItem");
            this.connectToKLMonorailToolStripMenuItem.Click += new System.EventHandler(this.connectToKLMonorailToolStripMenuItem_Click);
            // 
            // connectToBucharestToolStripMenuItem
            // 
            this.connectToBucharestToolStripMenuItem.Name = "connectToBucharestToolStripMenuItem";
            resources.ApplyResources(this.connectToBucharestToolStripMenuItem, "connectToBucharestToolStripMenuItem");
            this.connectToBucharestToolStripMenuItem.Click += new System.EventHandler(this.connectToBucharestToolStripMenuItem_Click);
            // 
            // connectToGCRTToolStripMenuItem
            // 
            this.connectToGCRTToolStripMenuItem.Name = "connectToGCRTToolStripMenuItem";
            resources.ApplyResources(this.connectToGCRTToolStripMenuItem, "connectToGCRTToolStripMenuItem");
            this.connectToGCRTToolStripMenuItem.Click += new System.EventHandler(this.connectToGCRTToolStripMenuItem_Click);
            // 
            // connectToHKToolStripMenuItem
            // 
            this.connectToHKToolStripMenuItem.Name = "connectToHKToolStripMenuItem";
            resources.ApplyResources(this.connectToHKToolStripMenuItem, "connectToHKToolStripMenuItem");
            this.connectToHKToolStripMenuItem.Click += new System.EventHandler(this.connectToHKToolStripMenuItem_Click);
            // 
            // connectToSLRToolStripMenuItem
            // 
            this.connectToSLRToolStripMenuItem.Name = "connectToSLRToolStripMenuItem";
            resources.ApplyResources(this.connectToSLRToolStripMenuItem, "connectToSLRToolStripMenuItem");
            this.connectToSLRToolStripMenuItem.Click += new System.EventHandler(this.connectToSLRToolStripMenuItem_Click);
            // 
            // connectToWaratahToolStripMenuItem
            // 
            this.connectToWaratahToolStripMenuItem.Name = "connectToWaratahToolStripMenuItem";
            resources.ApplyResources(this.connectToWaratahToolStripMenuItem, "connectToWaratahToolStripMenuItem");
            this.connectToWaratahToolStripMenuItem.Click += new System.EventHandler(this.connectToWaratahToolStripMenuItem_Click);
            // 
            // connectToMetrolinxToolStripMenuItem
            // 
            this.connectToMetrolinxToolStripMenuItem.Name = "connectToMetrolinxToolStripMenuItem";
            resources.ApplyResources(this.connectToMetrolinxToolStripMenuItem, "connectToMetrolinxToolStripMenuItem");
            this.connectToMetrolinxToolStripMenuItem.Click += new System.EventHandler(this.connectToMetrolinxToolStripMenuItem_Click);
            // 
            // connectToOttawaLightRailToolStripMenuItem
            // 
            this.connectToOttawaLightRailToolStripMenuItem.Name = "connectToOttawaLightRailToolStripMenuItem";
            resources.ApplyResources(this.connectToOttawaLightRailToolStripMenuItem, "connectToOttawaLightRailToolStripMenuItem");
            this.connectToOttawaLightRailToolStripMenuItem.Click += new System.EventHandler(this.connectToOttawaLightRailToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // connectToASToolStripMenuItem
            // 
            this.connectToASToolStripMenuItem.Name = "connectToASToolStripMenuItem";
            resources.ApplyResources(this.connectToASToolStripMenuItem, "connectToASToolStripMenuItem");
            this.connectToASToolStripMenuItem.Click += new System.EventHandler(this.connectToASToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            resources.ApplyResources(this.disconnectToolStripMenuItem, "disconnectToolStripMenuItem");
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectFromASToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.isCommsEstablishedToolStripMenuItem,
            this.isASConnectedToolStripMenuItem,
            this.toolStripSeparator3,
            this.getSystemRevisionToolStripMenuItem,
            this.isSystemRevisionConsistentToolStripMenuItem});
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            resources.ApplyResources(this.statusToolStripMenuItem, "statusToolStripMenuItem");
            // 
            // isCommsEstablishedToolStripMenuItem
            // 
            this.isCommsEstablishedToolStripMenuItem.Name = "isCommsEstablishedToolStripMenuItem";
            resources.ApplyResources(this.isCommsEstablishedToolStripMenuItem, "isCommsEstablishedToolStripMenuItem");
            this.isCommsEstablishedToolStripMenuItem.Click += new System.EventHandler(this.isCommsEstablishedToolStripMenuItem_Click);
            // 
            // isASConnectedToolStripMenuItem
            // 
            this.isASConnectedToolStripMenuItem.Name = "isASConnectedToolStripMenuItem";
            resources.ApplyResources(this.isASConnectedToolStripMenuItem, "isASConnectedToolStripMenuItem");
            this.isASConnectedToolStripMenuItem.Click += new System.EventHandler(this.isASConnectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // getSystemRevisionToolStripMenuItem
            // 
            this.getSystemRevisionToolStripMenuItem.Name = "getSystemRevisionToolStripMenuItem";
            resources.ApplyResources(this.getSystemRevisionToolStripMenuItem, "getSystemRevisionToolStripMenuItem");
            this.getSystemRevisionToolStripMenuItem.Click += new System.EventHandler(this.getSystemRevisionToolStripMenuItem_Click);
            // 
            // isSystemRevisionConsistentToolStripMenuItem
            // 
            this.isSystemRevisionConsistentToolStripMenuItem.Name = "isSystemRevisionConsistentToolStripMenuItem";
            resources.ApplyResources(this.isSystemRevisionConsistentToolStripMenuItem, "isSystemRevisionConsistentToolStripMenuItem");
            this.isSystemRevisionConsistentToolStripMenuItem.Click += new System.EventHandler(this.isSystemRevisionConsistentToolStripMenuItem_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearDebugMessagesToolStripMenuItem,
            this.clearDeviceStatesEventsToolStripMenuItem,
            this.clearPAEventsToolStripMenuItem,
            this.clearCallEventsToolStripMenuItem,
            this.clearCDREvetnsToolStripMenuItem,
            this.clearTerminalEventsToolStripMenuItem,
            this.clearSignallingEventsToolStripMenuItem,
            this.clearAllMessagesEventsToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            resources.ApplyResources(this.controlToolStripMenuItem, "controlToolStripMenuItem");
            // 
            // clearDebugMessagesToolStripMenuItem
            // 
            this.clearDebugMessagesToolStripMenuItem.Name = "clearDebugMessagesToolStripMenuItem";
            resources.ApplyResources(this.clearDebugMessagesToolStripMenuItem, "clearDebugMessagesToolStripMenuItem");
            this.clearDebugMessagesToolStripMenuItem.Click += new System.EventHandler(this.clearDebugMessagesToolStripMenuItem_Click);
            // 
            // clearDeviceStatesEventsToolStripMenuItem
            // 
            this.clearDeviceStatesEventsToolStripMenuItem.Name = "clearDeviceStatesEventsToolStripMenuItem";
            resources.ApplyResources(this.clearDeviceStatesEventsToolStripMenuItem, "clearDeviceStatesEventsToolStripMenuItem");
            this.clearDeviceStatesEventsToolStripMenuItem.Click += new System.EventHandler(this.clearDeviceStatesEventsToolStripMenuItem_Click);
            // 
            // clearPAEventsToolStripMenuItem
            // 
            this.clearPAEventsToolStripMenuItem.Name = "clearPAEventsToolStripMenuItem";
            resources.ApplyResources(this.clearPAEventsToolStripMenuItem, "clearPAEventsToolStripMenuItem");
            this.clearPAEventsToolStripMenuItem.Click += new System.EventHandler(this.clearPAEventsToolStripMenuItem_Click);
            // 
            // clearCallEventsToolStripMenuItem
            // 
            this.clearCallEventsToolStripMenuItem.Name = "clearCallEventsToolStripMenuItem";
            resources.ApplyResources(this.clearCallEventsToolStripMenuItem, "clearCallEventsToolStripMenuItem");
            this.clearCallEventsToolStripMenuItem.Click += new System.EventHandler(this.clearCallEventsToolStripMenuItem_Click);
            // 
            // clearCDREvetnsToolStripMenuItem
            // 
            this.clearCDREvetnsToolStripMenuItem.Name = "clearCDREvetnsToolStripMenuItem";
            resources.ApplyResources(this.clearCDREvetnsToolStripMenuItem, "clearCDREvetnsToolStripMenuItem");
            this.clearCDREvetnsToolStripMenuItem.Click += new System.EventHandler(this.clearCDREvetnsToolStripMenuItem_Click);
            // 
            // clearTerminalEventsToolStripMenuItem
            // 
            this.clearTerminalEventsToolStripMenuItem.Name = "clearTerminalEventsToolStripMenuItem";
            resources.ApplyResources(this.clearTerminalEventsToolStripMenuItem, "clearTerminalEventsToolStripMenuItem");
            this.clearTerminalEventsToolStripMenuItem.Click += new System.EventHandler(this.clearTerminalEventsToolStripMenuItem_Click);
            // 
            // clearSignallingEventsToolStripMenuItem
            // 
            this.clearSignallingEventsToolStripMenuItem.Name = "clearSignallingEventsToolStripMenuItem";
            resources.ApplyResources(this.clearSignallingEventsToolStripMenuItem, "clearSignallingEventsToolStripMenuItem");
            this.clearSignallingEventsToolStripMenuItem.Click += new System.EventHandler(this.clearSignallingEventsToolStripMenuItem_Click);
            // 
            // clearAllMessagesEventsToolStripMenuItem
            // 
            this.clearAllMessagesEventsToolStripMenuItem.Name = "clearAllMessagesEventsToolStripMenuItem";
            resources.ApplyResources(this.clearAllMessagesEventsToolStripMenuItem, "clearAllMessagesEventsToolStripMenuItem");
            this.clearAllMessagesEventsToolStripMenuItem.Click += new System.EventHandler(this.clearAllMessagesEventsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdkVersionToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // sdkVersionToolStripMenuItem
            // 
            this.sdkVersionToolStripMenuItem.Name = "sdkVersionToolStripMenuItem";
            resources.ApplyResources(this.sdkVersionToolStripMenuItem, "sdkVersionToolStripMenuItem");
            this.sdkVersionToolStripMenuItem.Click += new System.EventHandler(this.sdkVersionToolStripMenuItem_Click);
            // 
            // listBoxDebugMsg
            // 
            resources.ApplyResources(this.listBoxDebugMsg, "listBoxDebugMsg");
            this.listBoxDebugMsg.FormattingEnabled = true;
            this.listBoxDebugMsg.Name = "listBoxDebugMsg";
            this.listBoxDebugMsg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxDebugMsg_MouseUp);
            // 
            // timerCallStates
            // 
            this.timerCallStates.Interval = 5000;
            this.timerCallStates.Tick += new System.EventHandler(this.timerCallStates_Tick);
            // 
            // tabControl2
            // 
            resources.ApplyResources(this.tabControl2, "tabControl2");
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage11);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            // 
            // tabPage5
            // 
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Controls.Add(this.listBoxDebugMsg);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Controls.Add(this.listBoxDeviceStateEvents);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            resources.ApplyResources(this.tabPage7, "tabPage7");
            this.tabPage7.Controls.Add(this.listBoxPAEvents);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            resources.ApplyResources(this.tabPage8, "tabPage8");
            this.tabPage8.Controls.Add(this.listBoxCallEvents);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.listBoxCDREvents);
            resources.ApplyResources(this.tabPage9, "tabPage9");
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // listBoxCDREvents
            // 
            resources.ApplyResources(this.listBoxCDREvents, "listBoxCDREvents");
            this.listBoxCDREvents.FormattingEnabled = true;
            this.listBoxCDREvents.Name = "listBoxCDREvents";
            this.listBoxCDREvents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxCDREvents_MouseUp);
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.listBoxTerminalEvents);
            resources.ApplyResources(this.tabPage10, "tabPage10");
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // listBoxTerminalEvents
            // 
            resources.ApplyResources(this.listBoxTerminalEvents, "listBoxTerminalEvents");
            this.listBoxTerminalEvents.FormattingEnabled = true;
            this.listBoxTerminalEvents.Name = "listBoxTerminalEvents";
            this.listBoxTerminalEvents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxTerminalEvents_MouseUp);
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.listBoxSignallingEvents);
            resources.ApplyResources(this.tabPage11, "tabPage11");
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // listBoxSignallingEvents
            // 
            resources.ApplyResources(this.listBoxSignallingEvents, "listBoxSignallingEvents");
            this.listBoxSignallingEvents.FormattingEnabled = true;
            this.listBoxSignallingEvents.Name = "listBoxSignallingEvents";
            this.listBoxSignallingEvents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxSignallingEvents_MouseUp);
            // 
            // contextMenuStripEvents
            // 
            this.contextMenuStripEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMessagsEventsToolStripMenuItem});
            this.contextMenuStripEvents.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStripEvents, "contextMenuStripEvents");
            // 
            // clearMessagsEventsToolStripMenuItem
            // 
            this.clearMessagsEventsToolStripMenuItem.Name = "clearMessagsEventsToolStripMenuItem";
            resources.ApplyResources(this.clearMessagsEventsToolStripMenuItem, "clearMessagsEventsToolStripMenuItem");
            this.clearMessagsEventsToolStripMenuItem.Click += new System.EventHandler(this.clearMessagsEventsToolStripMenuItem_Click);
            // 
            // timerEventsPolling
            // 
            this.timerEventsPolling.Interval = 5000;
            this.timerEventsPolling.Tick += new System.EventHandler(this.timerEventsPolling_Tick);
            // 
            // contextMenuStripListViewDevices
            // 
            this.contextMenuStripListViewDevices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.isolateDeviceToolStripMenuItem,
            this.deIsolateDeviceToolStripMenuItem,
            this.getDynamicConfigurationToolStripMenuItem,
            this.setDynamicConfigurationToolStripMenuItem,
            this.initiateDeviceHealthTestToolStripMenuItem,
            this.readDigitalInputToolStripMenuItem,
            this.setDigitalOutputToolStripMenuItem,
            this.unsetDigitalOutputToolStripMenuItem,
            this.readDigitalOutputToolStripMenuItem});
            this.contextMenuStripListViewDevices.Name = "contextMenuStripListViewDevices";
            resources.ApplyResources(this.contextMenuStripListViewDevices, "contextMenuStripListViewDevices");
            // 
            // isolateDeviceToolStripMenuItem
            // 
            this.isolateDeviceToolStripMenuItem.Name = "isolateDeviceToolStripMenuItem";
            resources.ApplyResources(this.isolateDeviceToolStripMenuItem, "isolateDeviceToolStripMenuItem");
            this.isolateDeviceToolStripMenuItem.Click += new System.EventHandler(this.setDeviceIsolationToolStripMenuItem_Click);
            // 
            // deIsolateDeviceToolStripMenuItem
            // 
            this.deIsolateDeviceToolStripMenuItem.Name = "deIsolateDeviceToolStripMenuItem";
            resources.ApplyResources(this.deIsolateDeviceToolStripMenuItem, "deIsolateDeviceToolStripMenuItem");
            this.deIsolateDeviceToolStripMenuItem.Click += new System.EventHandler(this.deIsolateDeviceToolStripMenuItem_Click);
            // 
            // getDynamicConfigurationToolStripMenuItem
            // 
            this.getDynamicConfigurationToolStripMenuItem.Name = "getDynamicConfigurationToolStripMenuItem";
            resources.ApplyResources(this.getDynamicConfigurationToolStripMenuItem, "getDynamicConfigurationToolStripMenuItem");
            this.getDynamicConfigurationToolStripMenuItem.Click += new System.EventHandler(this.getDynamicConfigurationToolStripMenuItem_Click);
            // 
            // setDynamicConfigurationToolStripMenuItem
            // 
            this.setDynamicConfigurationToolStripMenuItem.Name = "setDynamicConfigurationToolStripMenuItem";
            resources.ApplyResources(this.setDynamicConfigurationToolStripMenuItem, "setDynamicConfigurationToolStripMenuItem");
            this.setDynamicConfigurationToolStripMenuItem.Click += new System.EventHandler(this.setDynamicConfigurationToolStripMenuItem_Click);
            // 
            // initiateDeviceHealthTestToolStripMenuItem
            // 
            this.initiateDeviceHealthTestToolStripMenuItem.Name = "initiateDeviceHealthTestToolStripMenuItem";
            resources.ApplyResources(this.initiateDeviceHealthTestToolStripMenuItem, "initiateDeviceHealthTestToolStripMenuItem");
            this.initiateDeviceHealthTestToolStripMenuItem.Click += new System.EventHandler(this.initiateDeviceHealthTestToolStripMenuItem_Click);
            // 
            // readDigitalInputToolStripMenuItem
            // 
            this.readDigitalInputToolStripMenuItem.Name = "readDigitalInputToolStripMenuItem";
            resources.ApplyResources(this.readDigitalInputToolStripMenuItem, "readDigitalInputToolStripMenuItem");
            this.readDigitalInputToolStripMenuItem.Click += new System.EventHandler(this.readDigitalInputToolStripMenuItem_Click);
            // 
            // setDigitalOutputToolStripMenuItem
            // 
            this.setDigitalOutputToolStripMenuItem.Name = "setDigitalOutputToolStripMenuItem";
            resources.ApplyResources(this.setDigitalOutputToolStripMenuItem, "setDigitalOutputToolStripMenuItem");
            this.setDigitalOutputToolStripMenuItem.Click += new System.EventHandler(this.setDigitalOutputToolStripMenuItem_Click);
            // 
            // unsetDigitalOutputToolStripMenuItem
            // 
            this.unsetDigitalOutputToolStripMenuItem.Name = "unsetDigitalOutputToolStripMenuItem";
            resources.ApplyResources(this.unsetDigitalOutputToolStripMenuItem, "unsetDigitalOutputToolStripMenuItem");
            this.unsetDigitalOutputToolStripMenuItem.Click += new System.EventHandler(this.unsetDigitalOutputToolStripMenuItem_Click);
            // 
            // readDigitalOutputToolStripMenuItem
            // 
            this.readDigitalOutputToolStripMenuItem.Name = "readDigitalOutputToolStripMenuItem";
            resources.ApplyResources(this.readDigitalOutputToolStripMenuItem, "readDigitalOutputToolStripMenuItem");
            this.readDigitalOutputToolStripMenuItem.Click += new System.EventHandler(this.readDigitalOutputToolStripMenuItem_Click);
            // 
            // timerSignallingEvents
            // 
            this.timerSignallingEvents.Interval = 5000;
            this.timerSignallingEvents.Tick += new System.EventHandler(this.timerSignallingEvents_Tick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.pollDevicesTabPage.ResumeLayout(false);
            this.pollDevicesTabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pollEventsTabPage.ResumeLayout(false);
            this.pollEventsTabPage.PerformLayout();
            this.paControlTabPage.ResumeLayout(false);
            this.paControlTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPaSinkGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPaSourceGain)).EndInit();
            this.announcementsControlTabPage.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCustomPriority)).EndInit();
            this.tabControl3.ResumeLayout(false);
            this.tabPageDVA.ResumeLayout(false);
            this.tabPageDVA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDVAGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDictionaryChangeset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDictionaryItems)).EndInit();
            this.tabPageTTS.ResumeLayout(false);
            this.tabPageTTS.PerformLayout();
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDVAPaZoneIds)).EndInit();
            this.callControlTabPage.ResumeLayout(false);
            this.callControlTabPage.PerformLayout();
            this.signallingControlTabPage.ResumeLayout(false);
            this.signallingControlTabPage.PerformLayout();
            this.alarmManagementTabPage.ResumeLayout(false);
            this.alarmManagementTabPage.PerformLayout();
            this.bucharestOA0784TabPage.ResumeLayout(false);
            this.bucharestOA0784TabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.waratahOA0979TabPage.ResumeLayout(false);
            this.waratahOA0979TabPage.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.contextMenuStripEvents.ResumeLayout(false);
            this.contextMenuStripListViewDevices.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerDeviceStates;
        private System.Windows.Forms.ListView listViewDeviceStates;
        private System.Windows.Forms.ColumnHeader columnHeaderDeviceId;
        private System.Windows.Forms.ColumnHeader columnHeaderState;
        private System.Windows.Forms.ColumnHeader columnHeaderDictionaryVersion;
        private System.Windows.Forms.ColumnHeader columnHeaderHealthTestStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pollDevicesTabPage;
        private System.Windows.Forms.TabPage paControlTabPage;
        private System.Windows.Forms.Button buttonGetSelectorState;
        private System.Windows.Forms.Button buttonListSelectorSinks;
        private System.Windows.Forms.Button buttonListSources;
        private System.Windows.Forms.ListBox listBoxPaSelectors;
        private System.Windows.Forms.ListBox listBoxPaSources;
        private System.Windows.Forms.Button buttonListSelectors;
        private System.Windows.Forms.Button buttonListPaZoneIds_PA;
        private System.Windows.Forms.Button buttonListHWTriggers;
        private System.Windows.Forms.Button buttonCreateSWTrigger;
        private System.Windows.Forms.ListBox listBoxPaHWTriggers;
        private System.Windows.Forms.Button buttonDeleteSWTrigger;
        private System.Windows.Forms.ListBox listBoxPaAttachedZones;
        private System.Windows.Forms.Button buttonPA_PTT;
        private System.Windows.Forms.Button buttonDetachSource;
        private System.Windows.Forms.Button buttonAttachPaZone;
        private System.Windows.Forms.Button buttonAttachSource;
        private System.Windows.Forms.Button buttonDetachPaZone;
        private System.Windows.Forms.Button buttonDetachAllPaZones;
        private System.Windows.Forms.Button buttonListAttachedPaZones;
        private System.Windows.Forms.CheckBox checkBoxReplaceExistingSet;
        private System.Windows.Forms.ListBox listBoxPaZones;
        private System.Windows.Forms.ListBox listBoxPaSelectorSinks;
        private System.Windows.Forms.Button buttonEnableSelector;
        private System.Windows.Forms.Button buttonDisableSelector;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.CheckBox checkBoxPA_PTT;
        private System.Windows.Forms.Button buttonGetSourceState;
        private System.Windows.Forms.Button buttonGetHWTriggerState;
        private System.Windows.Forms.TabPage announcementsControlTabPage;
        private System.Windows.Forms.Button buttonCreateDVASchedule;
        private System.Windows.Forms.Button buttonPlayDVA;
        private System.Windows.Forms.Button buttonListPaZoneIds_DVA;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.ComboBox comboBoxEndMins;
        private System.Windows.Forms.ComboBox comboBoxEndHours;
        private System.Windows.Forms.ComboBox comboBoxStartMins;
        private System.Windows.Forms.ComboBox comboBoxStartHours;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxFrequency;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxDayMask;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonUpdateDictionary;
        private System.Windows.Forms.TabPage callControlTabPage;
        private System.Windows.Forms.Button buttonCreateCall;
        private System.Windows.Forms.Button buttonTerminateCall;
        private System.Windows.Forms.Button buttonTransferCall;
        private System.Windows.Forms.TextBox textBoxTerminateCallID;
        private System.Windows.Forms.TextBox textBoxTransferCallNewBPartyID;
        private System.Windows.Forms.TextBox textBoxTransferCallReferenceID;
        private System.Windows.Forms.Button buttonResetDestinationMap;
        private System.Windows.Forms.Button buttonCreateDestination;
        private System.Windows.Forms.TextBox textBoxCreateDestinationBPartyExtList;
        private System.Windows.Forms.TextBox textBoxResetDestinationMapDeviceID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCreateCallBPartyID;
        private System.Windows.Forms.TextBox textBoxCreateCallAPartyID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxTerminateCallBPartyID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxCreateDestinationDeviceID;
        private System.Windows.Forms.TextBox textBoxCreateDestinationCLI;
        private System.Windows.Forms.ListBox listBoxDebugMsg;
        private System.Windows.Forms.ListBox listBoxDeviceStateEvents;
        private System.Windows.Forms.CheckBox checkBoxDeviceStatesPolling;
        private System.Windows.Forms.ListBox listBoxPAEvents;
        private System.Windows.Forms.ListView listViewCalls;
        private System.Windows.Forms.ListBox listBoxCallEvents;
        private System.Windows.Forms.CheckBox checkBoxCallsPolling;
        private System.Windows.Forms.Timer timerCallStates;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxCreateCallID;
        private System.Windows.Forms.ColumnHeader columnHeaderDictionaryStatus;
        private System.Windows.Forms.Button buttonGetDictionaryItems;
        private System.Windows.Forms.Button buttonGetDictionaryChangeItems;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.DataGridView dataGridViewDictionaryChangeset;
        private System.Windows.Forms.DataGridView dataGridViewDictionaryItems;
        private System.Windows.Forms.DataGridView dataGridViewDVAPaZoneIds;
        private System.Windows.Forms.ColumnHeader columnHeaderSupplementaryFields;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.ListBox listBoxCDREvents;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.ListBox listBoxTerminalEvents;
        private System.Windows.Forms.ColumnHeader columnHeaderDictionarySupport;
        private System.Windows.Forms.Button buttonResumeCall;
        private System.Windows.Forms.Button buttonHoldCall;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxResumeCallID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxHoldCallID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxResumeCallCalleeID;
        private System.Windows.Forms.TextBox textBoxHoldCallCalleelID;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxResumeCallOnTerminalBPartyID;
        private System.Windows.Forms.TextBox textBoxResumeCallOnTerminalAPartyID;
        private System.Windows.Forms.TextBox textBoxResumeCallOnTerminalCallID;
        private System.Windows.Forms.Button buttonResumeCallOnTerminal;
        private System.Windows.Forms.TextBox textBoxAnswerCallOnTerminalBPartyID;
        private System.Windows.Forms.TextBox textBoxAnswerCallOnTerminalAPartyID;
        private System.Windows.Forms.TextBox textBoxAnswerCallOnTerminalCallID;
        private System.Windows.Forms.Button buttonAnswerCallOnTerminal;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ColumnHeader columnHeaderDstNo;
        private System.Windows.Forms.ColumnHeader columnHeaderPortNo;
        private System.Windows.Forms.ColumnHeader columnHeaderIPAddress;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabPage bucharestOA0784TabPage;
        private System.Windows.Forms.ListBox listBoxHMIList;
        private System.Windows.Forms.TextBox textBoxHMIInputCode;
        private System.Windows.Forms.Button buttonDeleteHMIContent;
        private System.Windows.Forms.Button buttonUpdateHMIData;
        private System.Windows.Forms.TextBox textBoxHMIInputSave;
        private System.Windows.Forms.TextBox textBoxHMIInputStartStation;
        private System.Windows.Forms.TextBox textBoxHMIInputDestination;
        private System.Windows.Forms.TextBox textBoxHMIInputLine;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox comboBoxCallStatesPollingSeconds;
        private System.Windows.Forms.Label labelDVAGain;
        private System.Windows.Forms.TrackBar trackBarDVAGain;
        private System.Windows.Forms.Label labelAutoInfoStatus;
        private System.Windows.Forms.Button buttonCP2AutoInfoStatus;
        private System.Windows.Forms.Button buttonCP1AutoInfoStatus;
        private System.Windows.Forms.Button buttonCP2ActiveStatus;
        private System.Windows.Forms.Button buttonCP1ActiveStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonUploadFile;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPAEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDeviceStatesEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCallEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCDREvetnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTerminalEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllMessagesEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDebugMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEvents;
        private System.Windows.Forms.ToolStripMenuItem clearMessagsEventsToolStripMenuItem;
        private System.Windows.Forms.TabPage signallingControlTabPage;
        private System.Windows.Forms.TabPage pollEventsTabPage;
        private System.Windows.Forms.ListView listViewEventsPolling;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox comboBoxEventsPollingSeconds;
        private System.Windows.Forms.CheckBox checkBoxEventsPolling;
        private System.Windows.Forms.Timer timerEventsPolling;
        private System.Windows.Forms.Button buttonGetSinkGain;
        private System.Windows.Forms.Button buttonSetSinkGain;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getSystemRevisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isSystemRevisionConsistentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isCommsEstablishedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem isASConnectedToolStripMenuItem;
        private System.Windows.Forms.Button buttonGetSourceGain;
        private System.Windows.Forms.Button buttonSetSourceGain;
        private System.Windows.Forms.Label labelPaSinkGain;
        private System.Windows.Forms.Label labelPaSourceGain;
        private System.Windows.Forms.TrackBar trackBarPaSinkGain;
        private System.Windows.Forms.TrackBar trackBarPaSourceGain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sdkVersionToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderSoftwareRevision;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListViewDevices;
        private System.Windows.Forms.ToolStripMenuItem isolateDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deIsolateDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getDynamicConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDynamicConfigurationToolStripMenuItem;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ToolStripMenuItem connectToKLMonorailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToBucharestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToGCRTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToSLRToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem connectToASToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem initiateDeviceHealthTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToKTMBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readDigitalInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDigitalOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readDigitalOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsetDigitalOutputToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxPaSourcePersistenceFlag;
        private System.Windows.Forms.CheckBox checkBoxPaSinkPersistenceFlag;
        private System.Windows.Forms.ToolStripMenuItem connectToHKToolStripMenuItem;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox textBoxSWPAPriority;
        private System.Windows.Forms.Button buttonGetISDNTrunks;
        private System.Windows.Forms.Button buttonGetSIPTrunks;
        private System.Windows.Forms.CheckBox checkBoxTmp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxDeviceStatesHideEDI_IDI;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox comboBoxSignallingInfoPollingSeconds;
        private System.Windows.Forms.CheckBox checkBoxSignallingInfoPolling;
        private System.Windows.Forms.ListBox listBoxSignallingEvents;
        private System.Windows.Forms.ToolStripMenuItem clearSignallingEventsToolStripMenuItem;
        private System.Windows.Forms.Timer timerSignallingEvents;
        private System.Windows.Forms.Label labelPISServerStatus;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox textBoxAvailableStations;
        private System.Windows.Forms.TextBox textBoxAvailableVehicles;
        private System.Windows.Forms.TextBox textBoxAvailableLines;
        private System.Windows.Forms.TextBox textBoxAvailablePlatforms;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TabPage waratahOA0979TabPage;
        private System.Windows.Forms.CheckBox checkBoxWarathaEnableDisableCommercialRadio;
        private System.Windows.Forms.Button buttonWaratahCancelInterruptionToCommercialRadio;
        private System.Windows.Forms.Button buttonWaratahInterruptCommercialRadio;
        private System.Windows.Forms.CheckBox checkBoxWaratahEXTPASide2;
        private System.Windows.Forms.CheckBox checkBoxWaratahEXTPASide1;
        private System.Windows.Forms.ToolStripMenuItem connectToWaratahToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxDeviceStatesHideFaultyCommsFaultDevices;
        private System.Windows.Forms.Button buttonPlayTTS;
        private System.Windows.Forms.Button buttonClearTTS;
        private System.Windows.Forms.ToolStripMenuItem connectToMetrolinxToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonDeleteDVASchedule;
        private System.Windows.Forms.ListBox listBoxSchedules;
        private System.Windows.Forms.Button buttonListDVASchedules;
        private System.Windows.Forms.TextBox textBoxDeviceStatesPollingSeconds;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonTCXC8_DOUT6;
        private System.Windows.Forms.Button buttonTCXC8_DOUT5;
        private System.Windows.Forms.Button buttonTCXC8_DOUT4;
        private System.Windows.Forms.Button buttonTCXC8_DOUT3;
        private System.Windows.Forms.Button buttonTCXC8_DOUT2;
        private System.Windows.Forms.Button buttonTCXC8_DOUT1;
        private System.Windows.Forms.Button buttonTCXC8_DIN8;
        private System.Windows.Forms.Button buttonTCXC8_DIN7;
        private System.Windows.Forms.Button buttonTCXC8_DIN6;
        private System.Windows.Forms.Button buttonTCXC8_DIN5;
        private System.Windows.Forms.Button buttonTCXC8_DIN4;
        private System.Windows.Forms.Button buttonTCXC8_DIN3;
        private System.Windows.Forms.Button buttonTCXC8_DIN2;
        private System.Windows.Forms.Button buttonTCXC8_DIN1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonTCXC1_DOUT6;
        private System.Windows.Forms.Button buttonTCXC1_DOUT5;
        private System.Windows.Forms.Button buttonTCXC1_DOUT4;
        private System.Windows.Forms.Button buttonTCXC1_DOUT3;
        private System.Windows.Forms.Button buttonTCXC1_DOUT2;
        private System.Windows.Forms.Button buttonTCXC1_DOUT1;
        private System.Windows.Forms.Button buttonTCXC1_DIN8;
        private System.Windows.Forms.Button buttonTCXC1_DIN7;
        private System.Windows.Forms.Button buttonTCXC1_DIN6;
        private System.Windows.Forms.Button buttonTCXC1_DIN5;
        private System.Windows.Forms.Button buttonTCXC1_DIN4;
        private System.Windows.Forms.Button buttonTCXC1_DIN3;
        private System.Windows.Forms.Button buttonTCXC1_DIN2;
        private System.Windows.Forms.Button buttonTCXC1_DIN1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonWaratahCSPKR8Monitoring;
        private System.Windows.Forms.Button buttonWaratahCSPKR1Monitoring;
		private System.Windows.Forms.ListBox listBoxPaSinks;
		private System.Windows.Forms.Button buttonListPaSinksIds_PA;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBoxWaratahCommercialRadioInterrupt;
		private System.Windows.Forms.Button buttonWaratahSetCommercialRadioInterrupt;
		public System.Windows.Forms.ListBox listBoxTTSAudioMessageList;
		private System.Windows.Forms.Button buttonPreviewTTS;
		private System.Windows.Forms.TabControl tabControl3;
		private System.Windows.Forms.TabPage tabPageDVA;
		private System.Windows.Forms.TabPage tabPageTTS;
		private System.Windows.Forms.Button buttonGetVocalizerList;
		private System.Windows.Forms.TextBox textBoxTTSText;
		private System.Windows.Forms.ComboBox comboBoxTTSVocalizerList;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private System.Windows.Forms.Button buttonAnnouncementDisplayChangeTemplate;
        private System.Windows.Forms.TextBox textBoxAnnouncementDisplayTemplateName;
        private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.TextBox textBoxAnnouncementDisplayTemplateValue1;
		private System.Windows.Forms.TextBox textBoxAnnouncementDisplayTemplateKey;
        private System.Windows.Forms.TabPage alarmManagementTabPage;
        private System.Windows.Forms.CheckBox alarmManagementCheckBox;
        private System.Windows.Forms.Button alarmAcknowledgeButton;
		private System.Windows.Forms.Button getAlarmsButton;
		private System.Windows.Forms.ListView alarmsListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.Button buttonResetPaSink;
		private System.Windows.Forms.ToolStripMenuItem connectToOttawaLightRailToolStripMenuItem;
		private System.Windows.Forms.TrackBar trackBarCustomPriority;
		private System.Windows.Forms.RadioButton radioButtonCustomPriority;
		private System.Windows.Forms.RadioButton radioButtonSourcePriority;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label labelCustomPriority;
		private System.Windows.Forms.Button buttonGetPaSinkHealth;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox textBoxAnnouncementDisplayTemplateValue3;
		private System.Windows.Forms.TextBox textBoxAnnouncementDisplayTemplateValue2;
        private System.Windows.Forms.Button buttonGetDictionaryContentsURI;
    }
}

