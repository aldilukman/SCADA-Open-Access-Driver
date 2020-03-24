//#define BUCHAREST_OA0784
//#define WARATAH_OA0979

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NetspireSDKTester
{
	public partial class Form1 : Form
	{
		// Global
		public const int gGUIVersionNumber = 334;
		public static Form1 gMainForm = null;
		public enum EventType
		{
			NONE,
			DEBUG_MESSAGE,
			DEVICE_STATE_EVENT,
			PA_EVENT,
			CALL_EVENT,
			CDR_EVENT,
			TERMINAL_EVENT,
			STATE_EVENT,
			SIGNALLING_EVENT,
			AUDIO_MESSGAE_RETRIVAL_EVENT
		};

		public delegate void addDebugMessageCallBack(EventType eventType, string msg);
		public addDebugMessageCallBack addDeubgMessageDelegate = null;
		public delegate void audioDisconnectedCallBack();
		public audioDisconnectedCallBack audioDisconnectDelegate = null;
		public netspire.AudioServer gAudioServer = new netspire.AudioServer();
		public netspire.AudioServerObserver gAudioServerObserver = null;
		public netspire.PAControllerObserver gPAControllerObserver = null;
		public netspire.CallControllerObserver gCallControllerObserver = null;
		public netspire.PassengerInformationObserver gPassengerInformationServer = null;
		public int mySourceId = 0, myScheduleId = 1, mySWTriggerId = 0;
#if (BUCHAREST_OA0784)
		public delegate void updateBucharestCallBack(netspire.Device device);
		public updateBucharestCallBack updateBucharestDelegate = null;
#endif
#if (WARATAH_OA0979)
		public delegate void updateWaratahCallBack(netspire.Device deviceUpdate, netspire.PaSink paSinkUpdate);
		public updateWaratahCallBack updateWaratahDelegate = null;
#endif

		/*******************************************************************************************************/
		public Form1()
		{
			InitializeComponent();
			this.Text = "Netspire SDK Tester v" + gGUIVersionNumber + " | SDK v" + (gAudioServer == null ? "Audio Server Object not created" : gAudioServer.getSDKRevision());
			timerDeviceStates.Enabled = false;
			timerCallStates.Enabled = false;
			timerEventsPolling.Enabled = false;
			populateDVATabPage();
			addDeubgMessageDelegate = new addDebugMessageCallBack(addNewDebugMessage);
			audioDisconnectDelegate = new audioDisconnectedCallBack(initializeOnAudioDisconnect);
			textBoxDeviceStatesPollingSeconds.Text = "2";
			comboBoxCallStatesPollingSeconds.Text = "5";
			comboBoxEventsPollingSeconds.Text = "5";
			comboBoxSignallingInfoPollingSeconds.Text = "5";
#if (BUCHAREST_OA0784)
			updateBucharestDelegate = new updateBucharestCallBack(updateBucharest);
#else
			tabControl1.TabPages.Remove(bucharestOA0784TabPage);
#endif
#if (WARATAH_OA0979)
			updateWaratahDelegate = new updateWaratahCallBack(updateWaratah);
			textBoxWaratahCommercialRadioInterrupt.Text = "10000";
#else
			tabControl1.TabPages.Remove(waratahOA0979TabPage);
#endif
		}

		/*******************************************************************************************************/
		private void customSortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			int a = int.Parse(e.CellValue1.ToString()), b = int.Parse(e.CellValue2.ToString());
			e.SortResult = a.CompareTo(b);
			e.Handled = true;
		}

		/*******************************************************************************************************/
		private void Form1_Load(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "Application Started");
			connectToolStripMenuItem.Enabled = true;
			disconnectToolStripMenuItem.Enabled = false;
		}

		/*******************************************************************************************************/
		public void addNewDebugMessage(EventType eventType, string msg)
		{
			DateTime dt = DateTime.Now;
			msg = String.Format("{0:d/M/yyyy HH:mm:ss}", dt) + "  >  " + msg;

			switch (eventType)
			{
				case EventType.DEBUG_MESSAGE:
					listBoxDebugMsg.Items.Add(msg);
					// automatically scroll to the bottom of the listbox
					listBoxDebugMsg.SetSelected(listBoxDebugMsg.Items.Count - 1, true);                     //This selects and highlights the last line
					listBoxDebugMsg.SetSelected(listBoxDebugMsg.Items.Count - 1, false);                    //This unhighlights the last line
					break;

				case EventType.DEVICE_STATE_EVENT:
					listBoxDeviceStateEvents.Items.Add(msg);
					// automatically scroll to the bottom of the listbox
					listBoxDeviceStateEvents.SetSelected(listBoxDeviceStateEvents.Items.Count - 1, true);   //This selects and highlights the last line
					listBoxDeviceStateEvents.SetSelected(listBoxDeviceStateEvents.Items.Count - 1, false);  //This unhighlights the last line
					break;

				case EventType.PA_EVENT:
					listBoxPAEvents.Items.Add(msg);
					// automatically scroll to the bottom of the listbox
					listBoxPAEvents.SetSelected(listBoxPAEvents.Items.Count - 1, true);                     //This selects and highlights the last line
					listBoxPAEvents.SetSelected(listBoxPAEvents.Items.Count - 1, false);                    //This unhighlights the last line
					break;

				case EventType.CALL_EVENT:
					listBoxCallEvents.Items.Add(msg);
					// automatically scroll to the bottom of the listbox
					listBoxCallEvents.SetSelected(listBoxCallEvents.Items.Count - 1, true);                 //This selects and highlights the last line
					listBoxCallEvents.SetSelected(listBoxCallEvents.Items.Count - 1, false);                //This unhighlights the last line
					break;

				case EventType.CDR_EVENT:
					listBoxCDREvents.Items.Add(msg);
					// automatically scroll to the bottom of the listbox
					listBoxCDREvents.SetSelected(listBoxCDREvents.Items.Count - 1, true);                   //This selects and highlights the last line
					listBoxCDREvents.SetSelected(listBoxCDREvents.Items.Count - 1, false);                  //This unhighlights the last line
					break;

				case EventType.TERMINAL_EVENT:
					listBoxTerminalEvents.Items.Add(msg);
					// automatically scroll to the bottom of the listbox
					listBoxTerminalEvents.SetSelected(listBoxTerminalEvents.Items.Count - 1, true);         //This selects and highlights the last line
					listBoxTerminalEvents.SetSelected(listBoxTerminalEvents.Items.Count - 1, false);        //This unhighlights the last line
					break;

				case EventType.STATE_EVENT:
					listBoxHMIList.Items.Add(msg);
					break;

				case EventType.AUDIO_MESSGAE_RETRIVAL_EVENT:
					listBoxTTSAudioMessageList.Items.Add(msg);
					break;

				case EventType.SIGNALLING_EVENT:
					listBoxSignallingEvents.Items.Add(msg);
					// automatically scroll to the bottom of the listbox
					listBoxSignallingEvents.SetSelected(listBoxSignallingEvents.Items.Count - 1, true);     //This selects and highlights the last line
					listBoxSignallingEvents.SetSelected(listBoxSignallingEvents.Items.Count - 1, false);    //This unhighlights the last line
					break;

				default:
					break;
			}
		}

		/*******************************************************************************************************/
		private void estalishConnectionToAudioServer(netspire.StringArray serverAddresses, netspire.KeyValueMap configItems)
		{
			try
			{
				// got all connection parameters - create a new server object
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): Trying to connect to the AS...");
				if (gAudioServer == null)
				{
					gAudioServer = new netspire.AudioServer();
				}
				bool usePollingMethod = false;
				if (!usePollingMethod)
				{
					// create a new observer for event-based processing
					if (gAudioServerObserver == null)
					{
						gAudioServerObserver = new asObserverClass();
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): created new observer - asObserver");
					}
					if (gPAControllerObserver == null)
					{
						gPAControllerObserver = new paObserverClass();
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): created new observer - paObserver");
					}
					if (gCallControllerObserver == null)
					{
						gCallControllerObserver = new callObserverClass();
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): created new observer - callObserver");
					}
					if (gPassengerInformationServer == null)
					{
						gPassengerInformationServer = new passengerObserverClass();
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "establishConnectionToAudioServer(): created new observer - passengerInformationObserver");
					}
					gMainForm = this;
					gAudioServer.registerObserver(gAudioServerObserver);
					gAudioServer.getPAController().registerObserver(gPAControllerObserver);
					gAudioServer.getCallController().registerObserver(gCallControllerObserver);
					gAudioServer.getPassengerInformationServer().registerObserver(gPassengerInformationServer);
				}

				// all OK - send connect request to DVA server
				gAudioServer.connect(serverAddresses, configItems);
				if (!usePollingMethod)
				{
					// nothing to do - do stuff when events are received.
				}
				else
				{
					for (short i = 0; i < 100; i++)
					{
						if (gAudioServer.isAudioConnected())
							break;
						System.Threading.Thread.Sleep(500);
					}
					if (!gAudioServer.isAudioConnected())
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): Connection to AS failed");
						return;
					}
					else
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): Connection Established");
					}
				}

				// start timers
				checkBoxDeviceStatesPolling.Checked = false;
				checkBoxCallsPolling.Checked = false;
				checkBoxEventsPolling.Checked = false;
				connectToolStripMenuItem.Enabled = false;
				disconnectToolStripMenuItem.Enabled = true;

			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): exception caught: " + excep.Message);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonConnectestalishConnectionToAudioServer_Click(): exception stack trace: " + excep.StackTrace);
				if (excep.InnerException != null)
				{
					excep = excep.InnerException;
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): exception caught: " + excep.Message);
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "estalishConnectionToAudioServer(): exception stack trace: " + excep.StackTrace);
				}
			}
		}

		/*******************************************************************************************************/
		private void disconnectFromASToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer != null)
				{
					checkBoxDeviceStatesPolling.Checked = false;
					checkBoxCallsPolling.Checked = false;
					listViewDeviceStates.Items.Clear();
					if (mySourceId > 0)
					{
						gAudioServer.getPAController().detachPaSource(mySourceId);
						mySourceId = 0;
					}
					gAudioServer.disconnect();
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "Disconnected from AS");
				}
				connectToolStripMenuItem.Enabled = true;
				disconnectToolStripMenuItem.Enabled = false;
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDisconnect_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void getSystemRevisionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gAudioServer == null || !gAudioServer.isAudioConnected())
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "getSystemRevisionToolStripMenuItem_Click(): Not connected to AS");
			}
			else
			{
				if (gAudioServer.isAudioConnected())
				{
					MessageBox.Show("System Revision - " + gAudioServer.getSystemRevision());
				}
				else
				{
					MessageBox.Show("No connection to Audio Server");
				}
			}
		}

		/*******************************************************************************************************/
		private void isSystemRevisionConsistentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gAudioServer == null || !gAudioServer.isAudioConnected())
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "isSystemRevisionConsistentToolStripMenuItem_Click(): Not connected to AS");
			}
			else
			{
				if (gAudioServer.isAudioConnected())
				{
					MessageBox.Show("System Revision Consistent? " + (gAudioServer.isSystemRevisionConsistent() ? "Yes" : "No"));
				}
				else
				{
					MessageBox.Show("No connection to Audio Server");
				}
			}
		}

		/*******************************************************************************************************/
		private void isCommsEstablishedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gAudioServer == null || !gAudioServer.isAudioConnected())
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "isCommsEstablishedToolStripMenuItem_Click(): Not connected to AS");
			else
				MessageBox.Show("Comms Established: " + (gAudioServer.isCommsEstablished() ? "Yes" : "No"));
		}

		/*******************************************************************************************************/
		private void isASConnectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gAudioServer == null || !gAudioServer.isAudioConnected())
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "isAudioConnectedToolStripMenuItem_Click(): Not connected to AS");
			else
				MessageBox.Show("Audio Server Connected: " + (gAudioServer.isAudioConnected() ? "Yes" : "No"));
		}

		/*******************************************************************************************************/
		private void sdkVersionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Netspire SDK Version: " + (gAudioServer == null ? "Audio Server Object not created" : gAudioServer.getSDKRevision()));
		}

		/*******************************************************************************************************/
		private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();                   // this will call Form1_FormClosed(..) function
		}

		/*******************************************************************************************************/
		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Form1.gMainForm = null;     // user has decided to close the application - hence no need to print any messages from the observer class
			disconnectToolStripMenuItem.PerformClick();
		}

		/*******************************************************************************************************/
		private void populateDVATabPage()
		{
			// build hours in 24-hr format
			for (int i = 0; i < 24; i++)
			{
				comboBoxStartHours.Items.Add(i.ToString("00"));
				comboBoxEndHours.Items.Add(i.ToString("00"));
			}
			comboBoxStartHours.SelectedIndex = 0;
			comboBoxEndHours.SelectedIndex = 0;

			// build minutes
			for (int i = 0; i < 60; i++)
			{
				comboBoxStartMins.Items.Add(i.ToString("00"));
				comboBoxEndMins.Items.Add(i.ToString("00"));
			}
			comboBoxStartMins.SelectedIndex = 0;
			comboBoxEndMins.SelectedIndex = 0;

			// generate day mask
			for (int i = 0; i < 256; i++)
			{
				comboBoxDayMask.Items.Add(i);
			}
			comboBoxDayMask.SelectedIndex = comboBoxDayMask.Items.Count - 1;    // select all days

			// generate frequency (seconds)
			comboBoxFrequency.Items.Add(1800);
			comboBoxFrequency.SelectedIndex = 0;
		}

		/*******************************************************************************************************/
		private void initializeOnAudioDisconnect()
		{
			mySourceId = 0;
			myScheduleId = 1;
			mySWTriggerId = 0;
			listBoxPaSources.Items.Clear();
			listBoxPaZones.Items.Clear();
			listBoxPaAttachedZones.Items.Clear();
			dataGridViewDictionaryChangeset.Rows.Clear();
			dataGridViewDictionaryItems.Rows.Clear();
			dataGridViewDVAPaZoneIds.Rows.Clear();
		}

		/*******************************************************************************************************/
		private void textBoxDeviceStatesPollingSeconds_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}

			// default to 1999999
			String validity = textBoxDeviceStatesPollingSeconds.Text.TrimStart(new Char[] { '0' });
			textBoxDeviceStatesPollingSeconds.Text = validity;
			if (textBoxDeviceStatesPollingSeconds.Text.Length == 0 || (System.Convert.ToDouble(textBoxDeviceStatesPollingSeconds.Text) > 50))
			{
				textBoxDeviceStatesPollingSeconds.Text = "2";
				e.Handled = true;
			}
		}

		/*******************************************************************************************************/
		private void textBoxDeviceStatesPollingSeconds_Leave(object sender, EventArgs e)
		{
			// default to 1999999
			String validity = textBoxDeviceStatesPollingSeconds.Text.TrimStart(new Char[] { '0' });
			textBoxDeviceStatesPollingSeconds.Text = validity;
			if (textBoxDeviceStatesPollingSeconds.Text.Length == 0 || (System.Convert.ToDouble(textBoxDeviceStatesPollingSeconds.Text) > 50))
			{
				textBoxDeviceStatesPollingSeconds.Text = "2";
			}
		}

		/*******************************************************************************************************/
		private void checkBoxDeviceStatesPolling_CheckedChanged(object sender, EventArgs e)
		{
			timerDeviceStates.Enabled = checkBoxDeviceStatesPolling.Checked;
			timerDeviceStates.Interval = int.Parse(textBoxDeviceStatesPollingSeconds.Text) * 1000;
			checkBoxDeviceStatesPolling.Text = (checkBoxDeviceStatesPolling.Checked ? "Stop Polling for Device States" : "Start Polling for Device States");
			checkBoxDeviceStatesPolling.BackColor = (checkBoxDeviceStatesPolling.Checked ? System.Drawing.Color.LightGreen : System.Drawing.Color.Khaki);
			if (timerDeviceStates.Enabled)
			{
				listViewDeviceStates.Items.Clear();
				listViewDeviceStates.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
		}

		/*******************************************************************************************************/
		private void timerDeviceStates_Tick(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerDeviceStates_Tick(): Not connected to AS");
					return;
				}

				// poll for device status
				netspire.DeviceStateArray devStateArray = gAudioServer.getDeviceStates();
				bool adjustColumnWidth = false;
				for (short i = 0; i < devStateArray.Count; i++)
				{
					netspire.Device thisDevice = devStateArray.ElementAt(i);

					// extract device information
					String thisDevice_deviceId = thisDevice.getName();
					String thisDevice_ipaddress = thisDevice.getIP().ToString();
					String thisDevice_dstNo = thisDevice.getDstNo().ToString();
					String thisDevice_swRevision = thisDevice.getSoftwareRevision().ToString();
					String thisDevice_portNo = thisDevice.getPortNo().ToString();
					String thisDevice_stateText = thisDevice.getStateText().ToString();
					String thisDevice_healthStatus = thisDevice.getHealthTestStatus().ToString();
					String thisDevice_dictionarySupport = thisDevice.getDictionarySupport().ToString();
					String thisDevice_dictionaryUpdateStatus = thisDevice.getDictionaryUpdateStatus().ToString();
					String thisDevice_dictionaryVersion = thisDevice.getDictionaryVersion().ToString();

					// get device supplementary fields
					String thisDevice_supplementaryFieldsStr = "";
					netspire.KeyValueMap tmpKeyValueMap = thisDevice.getSupplementaryFields();
					for (int j = 0; j < tmpKeyValueMap.Count; j++)
					{
						thisDevice_supplementaryFieldsStr += "[" + tmpKeyValueMap.ElementAt(j).Key + "," + tmpKeyValueMap.ElementAt(j).Value + "] ";
					}

					// apply filtering rules
					String deviceIdToIgnore = "";
					if (checkBoxDeviceStatesHideFaultyCommsFaultDevices.Checked && (thisDevice_stateText.Equals("COMMSFAULT") || thisDevice_stateText.Equals("FAULTY")))
					{
						deviceIdToIgnore = thisDevice_deviceId;
					}
					if (checkBoxDeviceStatesHideEDI_IDI.Checked && thisDevice.getDeviceClass().ToString().Equals("PASSENGER_INFORMATION_DISPLAY"))
					{
						deviceIdToIgnore = thisDevice_deviceId;
					}

					// if device entry exists in listview then update entry else add a new entry.
					bool deviceEntryExistsInListView = false;
					foreach (ListViewItem itemRow in this.listViewDeviceStates.Items)
					{
						String thisItemRowDeviceId = itemRow.SubItems[0].Text;
						if (thisDevice_deviceId.CompareTo(thisItemRowDeviceId) != 0)
						{
							continue;
						}

						// found entry
						deviceEntryExistsInListView = true;

						// check if device needs to be filtered
						if (deviceIdToIgnore.CompareTo(thisItemRowDeviceId) == 0)
						{
							listViewDeviceStates.Items.Remove(itemRow);
						}

						// update records if different
						//itemRow.SubItems[0].Text = thisDevice_deviceId;
						if (itemRow.SubItems[1].Text.CompareTo(thisDevice_ipaddress) != 0)
						{
							itemRow.SubItems[1].Text = thisDevice_ipaddress;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[2].Text.CompareTo(thisDevice_dstNo) != 0)
						{
							itemRow.SubItems[2].Text = thisDevice_dstNo;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[3].Text.CompareTo(thisDevice_swRevision) != 0)
						{
							itemRow.SubItems[3].Text = thisDevice_swRevision;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[4].Text.CompareTo(thisDevice_portNo) != 0)
						{
							itemRow.SubItems[4].Text = thisDevice_portNo;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[5].Text.CompareTo(thisDevice_stateText) != 0)
						{
							itemRow.SubItems[5].Text = thisDevice_stateText;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[6].Text.CompareTo(thisDevice_healthStatus) != 0)
						{
							itemRow.SubItems[6].Text = thisDevice_healthStatus;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[7].Text.CompareTo(thisDevice_dictionarySupport) != 0)
						{
							itemRow.SubItems[7].Text = thisDevice_dictionarySupport;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[8].Text.CompareTo(thisDevice_dictionaryUpdateStatus) != 0)
						{
							itemRow.SubItems[8].Text = thisDevice_dictionaryUpdateStatus;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[9].Text.CompareTo(thisDevice_dictionaryVersion) != 0)
						{
							itemRow.SubItems[9].Text = thisDevice_dictionaryVersion;
							adjustColumnWidth = true;
						}
						if (itemRow.SubItems[10].Text.CompareTo(thisDevice_supplementaryFieldsStr) != 0)
						{
							itemRow.SubItems[10].Text = thisDevice_supplementaryFieldsStr;
							adjustColumnWidth = true;
						}
						break;
					}

					// add new entry in listview
					if (!deviceEntryExistsInListView && deviceIdToIgnore.Length == 0)
					{
						string[] newItemSubItems = new string[]
						{
							thisDevice_deviceId,
							thisDevice_ipaddress,
							thisDevice_dstNo,
							thisDevice_swRevision,
							thisDevice_portNo,
							thisDevice_stateText,
							thisDevice_healthStatus,
							thisDevice_dictionarySupport,
							thisDevice_dictionaryUpdateStatus,
							thisDevice_dictionaryVersion,
							thisDevice_supplementaryFieldsStr,
						};
						ListViewItem newItem = new ListViewItem(newItemSubItems);
						listViewDeviceStates.Items.Add(newItem);
						adjustColumnWidth = true;
					}
				}

				// auto adjust column widths
				if (adjustColumnWidth)
				{
					listViewDeviceStates.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
					listViewDeviceStates.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerDeviceStates_Tick(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListSources_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListSources_Click(): Not connected to AS");
					return;
				}

				// Get list of available PA sources
				listBoxPaSources.Items.Clear();
				netspire.PaSourceArray paSourceArray = new netspire.PaSourceArray();
				netspire.PAController paC = gAudioServer.getPAController();
				paSourceArray = paC.getPaSources();
				if (paSourceArray.Count > 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListSources_Click(): total PA sources >" + paSourceArray.Count + "<");
					for (int i = 0; i < paSourceArray.Count; i++)
					{
						netspire.PaSource thisPaSource = paSourceArray.ElementAt(i);
						listBoxPaSources.Items.Add(thisPaSource.id + " (" + thisPaSource.location + " " + thisPaSource.label + ")");
						addNewDebugMessage(EventType.PA_EVENT, "buttonListSources_Click(): sourceId >" + thisPaSource.id + "< " +
							thisPaSource.location + ", " +
							thisPaSource.label + ", " +
							thisPaSource.ipAddress + ", " +
							thisPaSource.type.ToString() + ", " +
							thisPaSource.subAddress);
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListSources_Click(): no PA source(s) found");
					return;
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonListSources_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonAttachSource_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): Not connected to AS");
					return;
				}

				if (mySourceId > 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): sourceId >" + mySourceId + "< already attached");
					return;
				}

				// get the current sourceId that the operator is interested in
				if (listBoxPaSources.SelectedIndex == -1)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): no sourceId selected");
					return;
				}

				// Attach PaSource
				netspire.PAController paController = gAudioServer.getPAController();
				String sourceIDStr = listBoxPaSources.SelectedItem.ToString();      // format = sourcedId - sourceLocation sourceLabel
				sourceIDStr = sourceIDStr.Substring(0, sourceIDStr.IndexOf(" "));   // extract just the sourceId
				mySourceId = System.Convert.ToInt32(sourceIDStr, 10);
				if (checkBoxTmp.Checked)
				{
					// do not do anything
					addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): Success (bypassed) in attaching sourceId >" + mySourceId + "<");
				}
				else
				{
					netspire.PaSourceArray allPaSourcesFromAS = paController.getPaSources();
					bool attachSuccess = false;
					for (int i = 0; i < allPaSourcesFromAS.Count; i++)
					{
						if (allPaSourcesFromAS.ElementAt(i).id == mySourceId)
						{
							addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): trying to attach PA sourceId >" + mySourceId + "<. Note that this function call is blocking (max block time is 10seconds)");
							bool retVal = paController.attachPaSource(mySourceId);
							if (retVal)
							{
								addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): Success in attaching sourceId >" + mySourceId + "<");

								// Remove all paZones attached to the source
								addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): removing all sinks from sourceId >" + mySourceId + "<");
								allPaSourcesFromAS.ElementAt(i).detachAllPaZones();

								attachSuccess = true;
								break;
							}
						}
					}

					if (!attachSuccess)
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): failed to attach sourceId >" + mySourceId + "<");
						mySourceId = 0;
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonAttachSource_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonDetachSource_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): Not connected to AS");
					return;
				}

				// detach source if currently attached
				if (mySourceId > 0)
				{
					netspire.PAController paC = gAudioServer.getPAController();
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): detaching PA sourceId >" + mySourceId + "<");
					if (checkBoxTmp.Checked)
					{
						mySourceId = 0;

						// delete software trigger created
						if (mySWTriggerId > 0)
						{
							paC.deleteSwPaTrigger(mySWTriggerId);
							mySWTriggerId = 0;
							addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): Successfully deleted software trigger");
						}
						listBoxPaZones.Items.Clear();           // clear all displayed sinks
						listBoxPaAttachedZones.Items.Clear();   // clear all displayed attached sinks
						listBoxPaHWTriggers.Items.Clear();      // clear all displayed triggers
						listBoxPaSelectors.Items.Clear();       // clear all displayed selectors
					}
					else
					{
						if (paC.detachPaSource(mySourceId))
						{
							mySourceId = 0;

							// delete software trigger created
							if (mySWTriggerId > 0)
							{
								paC.deleteSwPaTrigger(mySWTriggerId);
								mySWTriggerId = 0;
								addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): Successfully deleted software trigger");
							}
							listBoxPaZones.Items.Clear();           // clear all displayed sinks
							listBoxPaAttachedZones.Items.Clear();   // clear all displayed attached sinks
							listBoxPaHWTriggers.Items.Clear();      // clear all displayed triggers
							listBoxPaSelectors.Items.Clear();       // clear all displayed selectors
							addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): Successfully detached source");
						}
						else
						{
							addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): Failed to detach source");
						}
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): No sources attached to detach. Pls attach source");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonDetachSource_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetSourceState_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceState_Click(): Not connected to AS");
					return;
				}

				// get the current sourceId that the operator is interested in
				if (listBoxPaSources.SelectedIndex == -1)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceState_Click(): no sourceId selected");
					return;
				}

				// Enquire source
				String sourceIDStr = listBoxPaSources.SelectedItem.ToString();      // format = sourcedId - sourceLocation sourceLabel
				sourceIDStr = sourceIDStr.Substring(0, sourceIDStr.IndexOf(" "));   // extract just the sourceId
				int tmpSourceId = System.Convert.ToInt32(sourceIDStr, 10);
				netspire.PAController paController = gAudioServer.getPAController();
				netspire.PaSourceArray allPaSourcesFromAS = paController.getPaSources();
				bool foundPaSource = false;
				for (int i = 0; i < allPaSourcesFromAS.Count(); i++)
				{
					if (allPaSourcesFromAS.ElementAt(i).id == tmpSourceId)
					{
						netspire.PaSource.AttachState paSourceState = allPaSourcesFromAS.ElementAt(i).getAttachState();
						addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceState_Click(): sourceId >" + tmpSourceId + "< stat is >" + paSourceState.ToString() + "<");
						foundPaSource = true;
						break;
					}
				}

				if (!foundPaSource)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceState_Click(): could not find PaSource in the list from AS");
					return;
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceState_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void trackBarPaSourceGain_Scroll(object sender, EventArgs e)
		{
			// resAmp accepts increments of 0.01
			// PaSource Gain range is [-96.00, +42.00] dB
			// Hence TrackBar range is 0 to 13800. 13800 = (96 + 42) / 0.01
			// 0.00 dB = 9600 on TrackBar
			double sourceGain = (trackBarPaSourceGain.Value >= 9600) ? ((trackBarPaSourceGain.Value - 9600) * 0.01) : ((trackBarPaSourceGain.Value - 9600) * 0.01);
			labelPaSourceGain.Text = "PaSource Gain "
				+
				((sourceGain == 0) ? " " : (sourceGain > 0) ? "+" : "")
				+
				(String.Format("{0:0.00}", sourceGain) + "dB");
		}

		/*******************************************************************************************************/
		private void buttonSetSourceGain_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonSetSourceGain_Click(): Not connected to AS");
					return;
				}

				if (listBoxPaSources.SelectedItems.Count == 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonSetSourceGain_Click(): No Audio Sources selected");
					return;
				}

				// valid gain range is [-96.00, +42.00] dB
				double userSelectedGainLevel = (trackBarPaSourceGain.Value >= 9600) ? ((trackBarPaSourceGain.Value - 9600) * 0.01) : 0;

				// loop through selected PaSources and set gain
				netspire.PaSourceArray sourceListFromAS = gAudioServer.getPAController().getPaSources();
				for (int i = 0; i < listBoxPaSources.SelectedItems.Count; i++)
				{
					String sourceIDStr = listBoxPaSources.SelectedItem.ToString();      // format = sourcedId - sourceLocation sourceLabel
					sourceIDStr = sourceIDStr.Substring(0, sourceIDStr.IndexOf(" "));   // extract just the sourceId
					int sourceId = System.Convert.ToInt32(sourceIDStr, 10);
					for (int z = 0; z < sourceListFromAS.Count(); z++)
					{
						if (sourceId == sourceListFromAS.ElementAt(z).id)
						{
							sourceListFromAS.ElementAt(z).setGain(userSelectedGainLevel, checkBoxPaSourcePersistenceFlag.Checked);
							addNewDebugMessage(EventType.PA_EVENT, "buttonSetSourceGain_Click(): PaSource Id >" + sourceId + "< gain set to " + String.Format("{0:0.00}", userSelectedGainLevel) + "dB");
						}
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonSetSourceGain_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetSourceGain_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceGain_Click(): Not connected to AS");
					return;
				}

				if (listBoxPaSources.SelectedItems.Count == 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceGain_Click(): No Audio Sinks selected to attach");
					return;
				}

				// loop through selected PaSources and get gain
				netspire.PaSourceArray sourceListFromAS = gAudioServer.getPAController().getPaSources();
				for (int i = 0; i < listBoxPaSources.SelectedItems.Count; i++)
				{
					String sourceIDStr = listBoxPaSources.SelectedItem.ToString();      // format = sourcedId - sourceLocation sourceLabel
					sourceIDStr = sourceIDStr.Substring(0, sourceIDStr.IndexOf(" "));   // extract just the sourceId
					int sourceId = System.Convert.ToInt32(sourceIDStr, 10);
					for (int z = 0; z < sourceListFromAS.Count(); z++)
					{
						if (sourceId == sourceListFromAS.ElementAt(z).id)
						{
							addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceGain_Click(): PaSource Id >" + sourceId + "< Gain >" + sourceListFromAS.ElementAt(z).getGain().ToString() + "<");
						}
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonGetSourceGain_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListPaZoneIds_PA_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListPaZoneIds_PA_Click(): Not connected to AS");
					return;
				}

				// get list of PaZones
				listBoxPaZones.Items.Clear();
				netspire.PaZoneArray paZoneArray = gAudioServer.getPAController().getPaZones();
				if (paZoneArray.Count > 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListPaZoneIds_PA_Click(): total >" + paZoneArray.Count + "< paZones found");
					for (int i = 0; i < paZoneArray.Count; i++)
					{
						listBoxPaZones.Items.Add(paZoneArray.ElementAt(i).id);
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListPaZoneIds_PA_Click(): no sinks found for sourceId >" + mySourceId + "<");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonListPaZoneIds_PA_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonAttachPaZone_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonAttachPaZone_Click(): Not connected to AS");
					return;
				}

				if (mySourceId <= 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonAttachPaZone_Click(): No source attached. Pls attach source.");
					return;
				}

				if (listBoxPaZones.SelectedItems.Count == 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonAttachPaZone_Click(): No Audio Sinks selected to attach");
					return;
				}

				// loop through selected paZones and attach
				netspire.PaSourceArray allPaSourcesFromAS = gAudioServer.getPAController().getPaSources();
				for (int i = 0; i < allPaSourcesFromAS.Count; i++)
				{
					if (allPaSourcesFromAS.ElementAt(i).id == mySourceId)
					{
						for (int z = 0; z < listBoxPaZones.SelectedItems.Count; z++)
						{
							String paZoneIdStr = listBoxPaZones.SelectedItems[z].ToString();
							allPaSourcesFromAS.ElementAt(i).attachPaZone(paZoneIdStr, (checkBoxReplaceExistingSet.Checked ? netspire.PaSource.AttachMode.REPLACE_EXISTING_SET : netspire.PaSource.AttachMode.ADD_TO_EXISTING_SET));
							addNewDebugMessage(EventType.PA_EVENT, "buttonAttachPaZone_Click(): paZoneId >" + paZoneIdStr + "< attached to sourceId >" + mySourceId + "<");
						}
						break;
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonAttachPaZone_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonDetachPaZone_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachPaZone_Click(): Not connected to AS");
					return;
				}

				if (mySourceId <= 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachPaZone_Click(): No source attached. Pls attach source.");
					return;
				}

				if (listBoxPaZones.SelectedItems.Count == 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachPaZone_Click(): No Audio Sinks selected");
					return;
				}

				// loop through selected paZones and detach
				netspire.PaSourceArray allPaSourcesFromAS = gAudioServer.getPAController().getPaSources();
				for (int i = 0; i < allPaSourcesFromAS.Count; i++)
				{
					if (allPaSourcesFromAS.ElementAt(i).id == mySourceId)
					{
						for (int z = 0; z < listBoxPaZones.SelectedItems.Count; z++)
						{
							String paZoneId = listBoxPaZones.SelectedItems[z].ToString();
							allPaSourcesFromAS.ElementAt(i).detachPaZone(paZoneId);
							addNewDebugMessage(EventType.PA_EVENT, "buttonDetachPaZone_Click(): detached paZoneId >" + paZoneId + "< from sourceId >" + mySourceId + "<");
						}
						break;
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonDetachPaZone_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonDetachAllPaZones_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachAllPaZones_Click(): Not connected to AS");
					return;
				}

				if (mySourceId > 0)
				{
					netspire.PaSourceArray allPaSourcesFromAS = gAudioServer.getPAController().getPaSources();
					for (int i = 0; i < allPaSourcesFromAS.Count; i++)
					{
						netspire.PaSource thisPaSource = allPaSourcesFromAS.ElementAt(i);
						if (thisPaSource.id == mySourceId)
						{
							thisPaSource.detachAllPaZones();
							addNewDebugMessage(EventType.PA_EVENT, "buttonDetachAllPaZones_Click(): detached all sinks from sourceId >" + mySourceId + "<");
							break;
						}
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDetachAllPaZones_Click(): No source attached. Pls attach source.");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonDetachAllPaZones_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListAttachedPaZones_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListAttachedPaZones_Click(): Not connected to AS");
					return;
				}

				listBoxPaAttachedZones.Items.Clear();
				if (mySourceId > 0)
				{
					bool foundAttachedPaZones = false;
					netspire.PaSourceArray allPaSourcesFromAS = gAudioServer.getPAController().getPaSources();
					for (int i = 0; i < allPaSourcesFromAS.Count(); i++)
					{
						if (allPaSourcesFromAS.ElementAt(i).id == mySourceId)
						{
							netspire.StringArray attachedPaZones = allPaSourcesFromAS.ElementAt(i).getAttachedPaZones();
							foundAttachedPaZones = (attachedPaZones.Count() > 0);
							for (int z = 0; z < attachedPaZones.Count(); z++)
							{
								listBoxPaAttachedZones.Items.Add(attachedPaZones.ElementAt(z));
							}
						}
					}
					if (!foundAttachedPaZones)
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonListAttachedPaZones_Click(): no attached pa zones for sourceId >" + mySourceId + "<");
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListAttachedPaZones_Click(): No source attached. Pls attach source.");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonListAttachedPaZones_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListPaSinksIds_PA_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListPaSinksIds_PA_Click(): Not connected to AS");
					return;
				}

				// get list of PaSinks
				listBoxPaSinks.Items.Clear();
				netspire.PaSinkArray paSinkArray = gAudioServer.getPAController().getPaSinks();
				if (paSinkArray.Count > 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListPaSinksIds_PA_Click(): total >" + paSinkArray.Count + "< paSinkArray found");
					for (int i = 0; i < paSinkArray.Count; i++)
					{
						netspire.PaSink thisPaSink = paSinkArray.ElementAt(i);
						listBoxPaSinks.Items.Add(thisPaSink.id + " " + thisPaSink.location + " " + thisPaSink.label);       // format = sinkId - sinkLocation sinkLabel
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListPaSinksIds_PA_Click(): no PaSinks found");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonListPaSinksIds_PA_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void trackBarPaSinkGain_Scroll(object sender, EventArgs e)
		{
			// resAmp accepts increments of 0.375
			// PaSink Gain range is [-96.000, 0.000] dB
			// Hence TrackBar range is 0 to 256. 256 = 96 / 0.375
			double gain = (trackBarPaSinkGain.Value - 256) * 0.375;
			labelPaSinkGain.Text = "PaSink Gain " + String.Format("{0:0.000}", gain) + "dB";
		}

		/*******************************************************************************************************/
		private void buttonSetSinkGain_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonSetSinkGain_Click(): Not connected to AS");
					return;
				}

				if (listBoxPaSinks.SelectedItems.Count == 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonSetSinkGain_Click(): No PaSinks selected to attach");
					return;
				}

				// valid gain range is -96.000 to 0.000
				double userSelectedGain = (trackBarPaSinkGain.Value - 256) * 0.375;

				// loop through selected PaSinks and set gain
				netspire.PaSinkArray sinkListFromAS = gAudioServer.getPAController().getPaSinks();
				for (int i = 0; i < listBoxPaSinks.SelectedItems.Count; i++)
				{
					String sinkIdStr = listBoxPaSinks.SelectedItems[i].ToString();  // format = sinkId - sinkLocation sinkLabel
					sinkIdStr = sinkIdStr.Substring(0, sinkIdStr.IndexOf(" "));     // extract sinkId
					int sinkId = System.Convert.ToInt32(sinkIdStr);
					for (int z = 0; z < sinkListFromAS.Count(); z++)
					{
						if (sinkId == sinkListFromAS.ElementAt(z).id)
						{
							sinkListFromAS.ElementAt(z).setGain(new netspire.Gain(userSelectedGain), checkBoxPaSinkPersistenceFlag.Checked);
							addNewDebugMessage(EventType.PA_EVENT, "buttonSetSinkGain_Click(): PaSink Id >" + sinkId + "< gain set to " + String.Format("{0:0.000}", userSelectedGain) + "dB");
						}
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonSetSinkGain_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetSinkGain_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetSinkGain_Click(): Not connected to AS");
					return;
				}

				if (listBoxPaSinks.SelectedItems.Count == 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetSinkGain_Click(): No Audio Sinks selected to attach");
					return;
				}

				// loop through selected sinks and get gain
				netspire.PaSinkArray sinkListFromAS = gAudioServer.getPAController().getPaSinks();
				for (int i = 0; i < listBoxPaSinks.SelectedItems.Count; i++)
				{
					String sinkIdStr = listBoxPaSinks.SelectedItems[i].ToString();  // format = sinkId - sinkLocation sinkLabel
					sinkIdStr = sinkIdStr.Substring(0, sinkIdStr.IndexOf(" "));     // extract sinkId
					int sinkId = System.Convert.ToInt32(sinkIdStr);
					for (int z = 0; z < sinkListFromAS.Count(); z++)
					{
						if (sinkId == sinkListFromAS.ElementAt(z).id)
						{
							addNewDebugMessage(EventType.PA_EVENT, "buttonGetSinkGain_Click(): PaSink Id >" + sinkId + "< Gain >" + sinkListFromAS.ElementAt(z).getGain().getLevel().ToString() + "<");
						}
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonGetSinkGain_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetPaSinkHealth_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetPaSinkHealth_Click(): Not connected to AS");
					return;
				}

				// loop through selected sinks and get health
				netspire.PaSinkArray sinkListFromAS = gAudioServer.getPAController().getPaSinks();
				for (int i = 0; i < listBoxPaSinks.SelectedItems.Count; i++)
				{
					String sinkIdStr = listBoxPaSinks.SelectedItems[i].ToString();  // format = sinkId - sinkLocation sinkLabel
					sinkIdStr = sinkIdStr.Substring(0, sinkIdStr.IndexOf(" "));     // extract sinkId
					int sinkId = System.Convert.ToInt32(sinkIdStr);
					for (int z = 0; z < sinkListFromAS.Count(); z++)
					{
						netspire.PaSink thisPaSink = sinkListFromAS.ElementAt(z);
						if (sinkId == thisPaSink.id)
						{
							addNewDebugMessage(EventType.PA_EVENT, "buttonGetPaSinkHealth_Click(): PaSink Id >" + sinkId + "< Health >" + thisPaSink.healthState.ToString() + "<");
						}
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonGetPaSinkHealth_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonResetPaSink_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonResetPaSink(): Not connected to AS");
					return;
				}

				// loop through selected sinks and send reset command
				netspire.PaSinkArray sinkListFromAS = gAudioServer.getPAController().getPaSinks();
				for (int i = 0; i < listBoxPaSinks.SelectedItems.Count; i++)
				{
					String sinkIdStr = listBoxPaSinks.SelectedItems[i].ToString();  // format = sinkId - sinkLocation sinkLabel
					sinkIdStr = sinkIdStr.Substring(0, sinkIdStr.IndexOf(" "));     // extract sinkId
					int sinkId = System.Convert.ToInt32(sinkIdStr);
					for (int z = 0; z < sinkListFromAS.Count(); z++)
					{
						netspire.PaSink thisPaSink = sinkListFromAS.ElementAt(z);
						if (thisPaSink.id != sinkId)
						{
							continue;
						}

						// found PaSink
						if (thisPaSink.type == netspire.PaSink.Type.NETSPIRE_DCI2 || thisPaSink.type == netspire.PaSink.Type.NETSPIRE_DCI4)
						{
							thisPaSink.reset();
							addNewDebugMessage(EventType.PA_EVENT, "buttonGetSinkGain_Click(): PaSink Id >" + sinkId + "< Reset command sent");
						}
						else
						{
							addNewDebugMessage(EventType.PA_EVENT, "buttonGetSinkGain_Click(): PaSink Id >" + sinkId + "< does not support Reset command");
						}
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonGetSinkGain_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListHWTriggers_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListHWTriggers_Click(): Not connected to AS");
					return;
				}

				if (mySourceId > 0)
				{
					netspire.PaTriggerArray paTriggerArray = new netspire.PaTriggerArray();
					netspire.PAController paC = gAudioServer.getPAController();
					paTriggerArray = paC.getHwPaTriggers(mySourceId);
					listBoxPaHWTriggers.Items.Clear();
					if (paTriggerArray.Count > 0)
					{
						for (int i = 0; i < paTriggerArray.Count; i++)
						{
							netspire.PaTrigger thisPaTrigger = paTriggerArray.ElementAt(i);
							listBoxPaHWTriggers.Items.Add(thisPaTrigger.id);
							addNewDebugMessage(EventType.PA_EVENT, "buttonListHWTriggers_Click(): triggerId >" + thisPaTrigger.id + "<, " +
								thisPaTrigger.label + ", " +
								thisPaTrigger.type.ToString() + ", " +
								thisPaTrigger.subAddress + ", " +
								thisPaTrigger.priority.ToString());
						}
					}
					else
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonListHWTriggers_Click(): no HW triggers found for sourceId >" + mySourceId + "<");
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonListHWTriggers_Click(): No source attached. Pls attach source.");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonListHWTriggers_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetHWTriggerState_Click(object sender, EventArgs e)
		{
			int hwTriggerId = 0;
			netspire.PaTrigger.State triggerState;
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetHWTriggerState_Click(): Not connected to AS");
					return;
				}

				if (mySourceId > 0)
				{
					if (listBoxPaHWTriggers.SelectedIndex == -1)
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonGetHWTriggerState_Click(): no hwTriggerId selected");
					}
					else
					{
						hwTriggerId = System.Convert.ToInt32(listBoxPaHWTriggers.SelectedItem.ToString(), 10);
						netspire.PAController paC = gAudioServer.getPAController();
						triggerState = paC.getHwPaTriggerState(hwTriggerId);
						addNewDebugMessage(EventType.PA_EVENT, "buttonGetHWTriggerState_Click(): hwTriggerId >" + hwTriggerId + "< state is >" + ((netspire.PaTrigger.State)triggerState).ToString() + "<");
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonGetHWTriggerState_Click(): No source attached. Pls attach source.");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonGetHWTriggerState_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListSelectors_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			MessageBox.Show("to be implemented");
			/*addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
            try
            {
                if (gAudioServer == null || !gAudioServer.isAudioConnected())
                {
                    addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectors_Click(): Not connected to AS");
                    return;
                }

                if (mySourceId > 0)
                {
                    listBox6.Items.Clear();
                    netspire.PaSelectorArray paSelectorArray = new netspire.PaSelectorArray();
                    paSelectorArray = gAudioServer.getPaSelectors(mySourceId);
                    if (paSelectorArray.Count > 0)
                    {
                        for (int i = 0; i < paSelectorArray.Count; i++)
                        {
                            listBox6.Items.Add(paSelectorArray.ElementAt(i).id);
                            addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectors_Click(): selectorId >" + paSelectorArray.ElementAt(i).id +
                                ", " + paSelectorArray.ElementAt(i).type.ToString() +
                                ", " + paSelectorArray.ElementAt(i).number +
                                ", " + paSelectorArray.ElementAt(i).label +
                                ", " + paSelectorArray.ElementAt(i).protection.ToString() +
                                ", " + paSelectorArray.ElementAt(i).mode.ToString() +
                                ", " + paSelectorArray.ElementAt(i).dynamicRestriction.ToString());
                        }
                    }
                    else
                    {
                        addNewDebugMessage(EventType.DEBUG_MESSAGE, "(): no selectors found for sourceId >" + mySourceId + "<");
                    }
                }
                else
                {
                    addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectors_Click(): No source attached. Pls attach source");
                }
            }
            catch (System.Exception excep)
            {
                addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectors_Click(): exception caught: " + excep.Message);
            }*/
		}

		/*******************************************************************************************************/
		private void buttonListSelectorSinks_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			MessageBox.Show("to be implemented");
		}

		/*******************************************************************************************************/
		private void buttonGetSelectorState_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			MessageBox.Show("to be implemented");
			/*int selectorId = 0;
            PaSelector.State selectorState;
            addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
            try
            {
                if (gAudioServer == null || !gAudioServer.isAudioConnected())
                {
                    addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectorState_Click(): Not connected to AS");
                    return;
                }

                if (mySourceId > 0)
                {
                    if (listBox6.SelectedIndex == -1)
                    {
                        addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectorState_Click(): no selectorId selected");
                    }
                    else
                    {
                        selectorId = System.Convert.ToInt32(listBox6.SelectedItem.ToString(), 10);
                        selectorState = gAudioServer.getPaSelectorState(selectorId);
                        if (selectorState != 0)
                        {
                            addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectorState_Click(): selectorId >" + selectorId + "< state is >" + ((netspire.PaSelector.State)selectorState).ToString() + "<");
                        }
                        else
                        {
                            addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectorState_Click(): error retriving state for selectorId >" + selectorId + "<");
                        }
                    }
                }
                else
                {
                    addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectorState_Click(): No source attached. Pls attach source.");
                }
            }
            catch (System.Exception excep)
            {
                addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListSelectorState_Click(): exception caught: " + excep.Message);
            }*/
		}

		/*******************************************************************************************************/
		private void buttonEnableSelector_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			MessageBox.Show("to be implemented");
		}

		/*******************************************************************************************************/
		private void buttonDisableSelector_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			MessageBox.Show("to be implemented");
		}

		/*******************************************************************************************************/
		private void buttonCreateSWTrigger_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonCreateSWTrigger_Click(): Not connected to AS");
					return;
				}

				int swPAPriority = int.Parse(textBoxSWPAPriority.Text);
				if (swPAPriority < 0 || swPAPriority > 100)
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonCreateSWTrigger_Click(): SW PA Priority range is 1-100");
					return;
				}

				// create a new SW trigger if one is already not created.
				if (mySourceId > 0)
				{
					if (mySWTriggerId == 0)
					{
						netspire.PAController paC = gAudioServer.getPAController();
						mySWTriggerId = paC.createSwPaTrigger(mySourceId, swPAPriority);
						if (mySWTriggerId > 0)
							addNewDebugMessage(EventType.PA_EVENT, "buttonCreateSWTrigger_Click(): created new SW triggerId >" + mySWTriggerId + "<");
						else
							addNewDebugMessage(EventType.PA_EVENT, "buttonCreateSWTrigger_Click(): failed to create SW triggerId");
					}
					else
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonCreateSWTrigger_Click(): SW Trigger already exists. Nothing to do.");
					}
				}
				else
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateSWTrigger_Click(): No source attached. Pls attach source.");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonCreateSWTrigger_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonDeleteSWTrigger_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDeleteSWTrigger_Click(): Not connected to AS");
					return;
				}

				// delete existing SW trigger Id.
				if (mySourceId > 0)
				{
					if (mySWTriggerId > 0)
					{
						netspire.PAController paC = gAudioServer.getPAController();
						paC.deleteSwPaTrigger(mySWTriggerId);
						addNewDebugMessage(EventType.PA_EVENT, "buttonDeleteSWTrigger_Click(): deleted SW triggerId >" + mySWTriggerId + "<");
						mySWTriggerId = 0;
					}
					else
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonDeleteSWTrigger_Click(): SW trigger not yet created. Nothing to do");
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonDeleteSWTrigger_Click(): No source attached. Pls attach source");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonDeleteSWTrigger_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonPA_PTT_MouseDown(object sender, MouseEventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseDown(): Not connected to AS");
					return;
				}

				if (mySourceId > 0)
				{
					if (mySWTriggerId > 0)
					{
						netspire.PAController paC = gAudioServer.getPAController();
						paC.activateSwPaTrigger(mySWTriggerId);
						addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseDown(): PA announcement started");
					}
					else
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseDown(): No SW trigger. Pls create SW trigger first");
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseDown(): No source attached. Pls attach source");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseDown(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonPA_PTT_MouseUp(object sender, MouseEventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseUp(): Not connected to AS");
					return;
				}

				if (mySourceId > 0)
				{
					if (mySWTriggerId > 0)
					{
						netspire.PAController paC = gAudioServer.getPAController();
						paC.deactivateSwPaTrigger(mySWTriggerId);
						addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseUp(): PA announcement stopped");
					}
					else
					{
						addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseUp(): No SW trigger. Pls create SW trigger first");
					}
				}
				else
				{
					addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseUp(): No source attached. Pls attach source");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "buttonPA_PTT_MouseUp(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void checkBoxPA_PTT_CheckedChanged(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.PA_EVENT, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.PA_EVENT, "checkBoxPA_PTT_CheckedChanged(): Not connected to AS");
					checkBoxPA_PTT.Checked = false;
					return;
				}

				if (mySourceId <= 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "checkBoxPA_PTT_CheckedChanged(): no source attached");
					checkBoxPA_PTT.Checked = false;
					return;
				}

				if (mySWTriggerId <= 0)
				{
					addNewDebugMessage(EventType.PA_EVENT, "checkBoxPA_PTT_CheckedChanged(): SW Trigger not created.");
					checkBoxPA_PTT.Checked = false;
					return;
				}

				netspire.PAController paController = gAudioServer.getPAController();
				if (checkBoxPA_PTT.Checked)
				{
					bool foundAttachedPaZones = false;
					netspire.PaSourceArray allPaSourcesFromAS = paController.getPaSources();
					for (int i = 0; i < allPaSourcesFromAS.Count(); i++)
					{
						if (allPaSourcesFromAS.ElementAt(i).id == mySourceId)
						{
							foundAttachedPaZones = (allPaSourcesFromAS.ElementAt(i).getAttachedPaZones().Count() > 0);
							break;
						}
					}

					// start Announcement
					if (foundAttachedPaZones)
					{
						paController.activateSwPaTrigger(mySWTriggerId);
						checkBoxPA_PTT.BackColor = System.Drawing.Color.Khaki;
						addNewDebugMessage(EventType.PA_EVENT, "checkBoxPA_PTT_CheckedChanged(): PA Announement Active");
					}
					else
					{
						addNewDebugMessage(EventType.PA_EVENT, "checkBoxPA_PTT_CheckedChanged(): no sinks attached for sourceId >" + mySourceId + "<");
						checkBoxPA_PTT.Checked = false;
						return;
					}
				}
				else
				{
					// stop announcement
					paController.deactivateSwPaTrigger(mySWTriggerId);
					addNewDebugMessage(EventType.PA_EVENT, "checkBoxPA_PTT_CheckedChanged(): PA Announement Stopped");
					checkBoxPA_PTT.BackColor = System.Drawing.Color.Transparent;
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.PA_EVENT, "checkBoxPA_PTT_CheckedChanged(): Exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonUpdateDictionary_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUpdateDictionary_Click(): Not connected to AS");
					return;
				}

				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Open Dictionary Package... ";
				openFileDialog.InitialDirectory = "C:\\";
				openFileDialog.Filter = "ZIP File (*.zip)|*.zip";
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					bool ret = gAudioServer.updateDictionary(openFileDialog.FileName.ToString());
					if (ret)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUpdateDictionary_Click(): update package " + openFileDialog.FileName.ToString());
					}
					else
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUpdateDictionary_Click(): failed to start update package " + openFileDialog.FileName.ToString());
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUpdateDictionary_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetDictionaryChangeItems_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryChangeItems_Click(): Not connected to AS");
					return;
				}

				var watch = System.Diagnostics.Stopwatch.StartNew();
				dataGridViewDictionaryChangeset.ColumnCount = 12;
				dataGridViewDictionaryChangeset.Columns[0].Name = "Version Seq";
				dataGridViewDictionaryChangeset.Columns[1].Name = "Operation";
				dataGridViewDictionaryChangeset.Columns[2].Name = "New Version";
				dataGridViewDictionaryChangeset.Columns[3].Name = "Item No";
				dataGridViewDictionaryChangeset.Columns[4].Name = "Device Type";
				dataGridViewDictionaryChangeset.Columns[5].Name = "Description";
				dataGridViewDictionaryChangeset.Columns[6].Name = "Category";
				dataGridViewDictionaryChangeset.Columns[7].Name = "Audio Segment";
				dataGridViewDictionaryChangeset.Columns[8].Name = "Image";
				dataGridViewDictionaryChangeset.Columns[9].Name = "Video";
				dataGridViewDictionaryChangeset.Columns[10].Name = "Display Text";
				dataGridViewDictionaryChangeset.Columns[11].Name = "Metadata";
				dataGridViewDictionaryChangeset.Columns[0].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[1].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[2].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[3].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[4].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[5].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[6].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[7].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[8].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[9].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[10].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[11].ReadOnly = true;
				dataGridViewDictionaryChangeset.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryChangeset.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;     // Do not autosize the display text
				dataGridViewDictionaryChangeset.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dataGridViewDictionaryChangeset.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[4].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[5].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[6].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[7].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[8].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[9].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[10].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Columns[11].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryChangeset.Rows.Clear();
				dataGridViewDictionaryChangeset.SortCompare += customSortCompare;
				watch.Stop();
				var elapsedMs = watch.ElapsedMilliseconds;
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryChangeItems_Click(): " + elapsedMs + "ms to setup the grid");

				watch = System.Diagnostics.Stopwatch.StartNew();
				netspire.DictionaryChangesetItemArray dictionaryChangesetItemList = gAudioServer.getDictionaryChangeset();
				watch.Stop();
				elapsedMs = watch.ElapsedMilliseconds;
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryChangeItems_Click(): " + elapsedMs + "ms to fetch dictionarychangeset");

				watch = System.Diagnostics.Stopwatch.StartNew();
				for (int i = 0; i < dictionaryChangesetItemList.Count; i++)
				{
					netspire.DictionaryChangesetItem thisChangesetItem = dictionaryChangesetItemList.ElementAt(i);
					string[] newItemSubItems = new string[]
					{
						thisChangesetItem.versionSeq.ToString(),
						thisChangesetItem.operationId,
						thisChangesetItem.newVersion.ToString(),
						thisChangesetItem.itemNo.ToString(),
						thisChangesetItem.deviceType,
						thisChangesetItem.description,
						thisChangesetItem.category,
						thisChangesetItem.audioSegment,
						thisChangesetItem.image,
						thisChangesetItem.video,
						thisChangesetItem.displayText,
						thisChangesetItem.metadata
					};
					dataGridViewDictionaryChangeset.Rows.Add(newItemSubItems);
				}
				watch.Stop();
				elapsedMs = watch.ElapsedMilliseconds;
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryChangeItems_Click(): " + elapsedMs + "ms to populate the grid");

				watch = System.Diagnostics.Stopwatch.StartNew();
				dataGridViewDictionaryChangeset.Sort(dataGridViewDictionaryChangeset.Columns[0], ListSortDirection.Ascending);
				dataGridViewDictionaryChangeset.AutoResizeColumns();
				watch.Stop();
				elapsedMs = watch.ElapsedMilliseconds;
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryChangeItems_Click(): " + elapsedMs + "ms to sort the grid");
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryChangeItems_Click(): Got >" + dictionaryChangesetItemList.Count + "< dictionary changeset items from CXS server");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryChangeItems_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetDictionaryItems_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryItems_Click(): Not connected to AS");
					return;
				}

				dataGridViewDictionaryItems.ColumnCount = 2;
				dataGridViewDictionaryItems.Columns[0].Name = "Dictionary Item No";
				dataGridViewDictionaryItems.Columns[1].Name = "Description";
				dataGridViewDictionaryItems.Columns[0].ReadOnly = true;
				dataGridViewDictionaryItems.Columns[1].ReadOnly = true;
				dataGridViewDictionaryItems.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewDictionaryItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dataGridViewDictionaryItems.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryItems.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDictionaryItems.Rows.Clear();
				dataGridViewDictionaryItems.SortCompare += customSortCompare;

				// get a list of dictionary items
				netspire.DictionaryItemArray dictionaryItemList = gAudioServer.getDictionaryItems();
				for (int i = 0; i < dictionaryItemList.Count; i++)
				{
					netspire.DictionaryItem thisDictionaryItem = dictionaryItemList.ElementAt(i);
					string[] newItemSubItems = new string[]
					{
						thisDictionaryItem.itemNo.ToString(),
						thisDictionaryItem.description
					};
					dataGridViewDictionaryItems.Rows.Add(newItemSubItems);
				}
				dataGridViewDictionaryItems.Sort(dataGridViewDictionaryItems.Columns[0], ListSortDirection.Ascending);
				dataGridViewDictionaryItems.AutoResizeColumns();
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryItems_Click(): Got >" + dictionaryItemList.Count + "< dictionary items from CXS server");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryItems_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListPaZoneIds_DVA_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListPaZoneIds_DVA_Click(): Not connected to AS");
					return;
				}

				dataGridViewDVAPaZoneIds.ColumnCount = 1;
				dataGridViewDVAPaZoneIds.Columns[0].Name = "PaZone ID";
				//dataGridViewDVAPaZoneIds.Columns[1].Name = "Description";
				dataGridViewDVAPaZoneIds.Columns[0].ReadOnly = true;
				//dataGridViewDVAPaZoneIds.Columns[1].ReadOnly = true;
				dataGridViewDVAPaZoneIds.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				//dataGridViewDVAPaZoneIds.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dataGridViewDVAPaZoneIds.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
				//dataGridViewDVAPaZoneIds.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridViewDVAPaZoneIds.Rows.Clear();
				//dataGridViewDVAPaZoneIds.SortCompare += customSortCompare;

				// get list of PaZones
				netspire.PaZoneArray paZoneArray = gAudioServer.getPAController().getPaZones();
				for (int i = 0; i < paZoneArray.Count; i++)
				{
					string[] newItemSubItems = new string[]
					{
						paZoneArray.ElementAt(i).id.ToString()
						//"No Description",
					};
					dataGridViewDVAPaZoneIds.Rows.Add(newItemSubItems);
				}
				//dataGridViewDVAPaZoneIds.Sort(dataGridViewDVAPaZoneIds.Columns[0], ListSortDirection.Ascending);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListPaZoneIds_DVA_Click(): Found >" + paZoneArray.Count + "< PaZones");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListPaZoneIds_DVA_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetDictionaryContentsURI_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryContentsURI_Click(): Not connected to AS");
					return;
				}

				// sanity check
				if (dataGridViewDictionaryItems.SelectedRows.Count == 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryContentsURI_Click(): No DVA Items selected for playback");
					return;
				}

				// generate a list of DVA dictionary items
				netspire.NumberArray dvaItems = new netspire.NumberArray();
				foreach (DataGridViewRow row in dataGridViewDictionaryItems.SelectedRows)
				{
					if (row.Cells[0].Value != null)
					{
						dvaItems.Add(System.Convert.ToUInt32(row.Cells[0].Value.ToString()));
					}
				}

				// loop through all dictionary items, find DictionaryItem and call getContentsURI
				netspire.DictionaryItemArray dictionaryItemList = gAudioServer.getDictionaryItems();
				for (int i = 0; i < dictionaryItemList.Count; i++)
				{
					netspire.DictionaryItem thisDictionaryItem = dictionaryItemList.ElementAt(i);
					for (int j = 0; j < dvaItems.Count; j++)
					{
						if (thisDictionaryItem.itemNo == dvaItems[j])
						{
							netspire.StringArray itemContentsURIList = thisDictionaryItem.getContentsURI();
							if (itemContentsURIList.Count > 0)
							{
								addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryContentsURI_Click(): Found " + itemContentsURIList.Count + " Contents URI List for dictionary item " + dvaItems[j]);
								for (int k = 0; k < itemContentsURIList.Count; k++)
								{
									addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryContentsURI_Click(): URI = " + itemContentsURIList[k]);
								}
							}
							else
							{
								addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryContentsURI_Click(): No Contents URI List for dictionary item " + dvaItems[j]);
							}
							break;
						}
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetDictionaryContentsURI_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonPlayDVA_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayDVA_Click(): Not connected to AS");
					return;
				}

				// sanity check
				if (dataGridViewDictionaryItems.SelectedRows.Count == 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayDVA_Click(): No DVA Items selected for playback");
					return;
				}
				if (dataGridViewDVAPaZoneIds.SelectedRows.Count == 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayDVA_Click(): No Zones selected for playback");
				}

				// generate list of selected paZoneIds
				netspire.StringArray targetPaZoneList = new netspire.StringArray();
				foreach (DataGridViewRow row in dataGridViewDVAPaZoneIds.SelectedRows)
				{
					targetPaZoneList.Add(row.Cells[0].Value.ToString());
				}

				// generate a list of DVA dictionary items
				netspire.NumberArray dvaItems = new netspire.NumberArray();
				foreach (DataGridViewRow row in dataGridViewDictionaryItems.SelectedRows)
				{
					if (row.Cells[0].Value != null)
					{
						dvaItems.Add(System.Convert.ToUInt32(row.Cells[0].Value.ToString()));
					}
				}

				netspire.Message newMessage = new netspire.Message();
				netspire.MessagePriority msgPriority = new netspire.MessagePriority();
				msgPriority.setPriorityMode(radioButtonSourcePriority.Checked ? netspire.AnnouncementPriorityMode.APM_RELATIVE_PRIORITY : netspire.AnnouncementPriorityMode.APM_ABSOLUTE_PRIORITY);
				msgPriority.setPrioritylevel(radioButtonSourcePriority.Checked ? 500 : trackBarCustomPriority.Value);
				newMessage.setPriority(msgPriority);
				newMessage.setPreChime(99200);
				newMessage.setGain(trackBarDVAGain.Value - trackBarDVAGain.Maximum);    // slider range is 0 to +96 and allowed gain range is -96 to 0.
				newMessage.setAudioMessageType(netspire.Message.Type.DICTIONARY);
				newMessage.setAudioMessage(dvaItems);
				string requestId = gAudioServer.getPAController().playMessage(targetPaZoneList, newMessage);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayDVA_Click(): command sent to play DVA. requestId: " + requestId);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayDVA_Click(): exception caught: " + excep.Message);
			}

			//addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			//try
			//{
			//	if (gAudioServer == null || !gAudioServer.isAudioConnected())
			//	{
			//		addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDVAPlay_Click(): Not connected to AS");
			//		return;
			//	}
			//
			//	//if (dataGridViewDictionaryItems.SelectedRows.Count == 0)
			//	//{
			//	//    addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDVAPlay_Click(): No DVA Items entered for playback");
			//	//    return;
			//	//}
			//
			//	// generate list of selected paZoneIds
			//	netspire.StringArray targetPaZoneList = new netspire.StringArray();
			//	foreach (DataGridViewRow row in dataGridViewDVAPaZoneIds.SelectedRows)
			//	{
			//		targetPaZoneList.Add(row.Cells[0].Value.ToString());
			//	}
			//
			//	// set visualDevices
			//	netspire.StringArray visualDevicesStringArray = new netspire.StringArray();
			//	if (textBoxPlayMessageVisualDevices.Text.Length > 0)
			//	{
			//		// split visual devices (comma seperated)
			//		string[] visualDevicesList = textBoxPlayMessageVisualDevices.Text.Split(',');
			//		foreach (string token in visualDevicesList)
			//		{
			//			if (token != "")
			//			{
			//				visualDevicesStringArray.Add(token);
			//			}
			//		}
			//	}
			//
			//	// set Gain - Slider range is 0 to +96 and allowed gain range is -96 to 0.
			//	netspire.Gain gain = new netspire.Gain();
			//	gain.setLevel(trackBarDVAGain.Value - trackBarDVAGain.Maximum);
			//
			//	// generate a list of DVA dictionary items
			//	netspire.NumberArray dvaItems = new netspire.NumberArray();
			//	foreach (DataGridViewRow row in dataGridViewDictionaryItems.SelectedRows)
			//	{
			//		if (row.Cells[0].Value != null)
			//		{
			//			dvaItems.Add(System.Convert.ToUInt32(row.Cells[0].Value.ToString()));
			//		}
			//	}
			//	// the following check is not required incase we only want to update the display
			//	//if (dvaItems.Count == 0)
			//	//{
			//	//    addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDVAPlay_Click(): DVA Items not specified");
			//	//    return;
			//	//}
			//
			//	// Set visual Message for display
			//	string visualText = (textBoxPlayMessageVisualText.TextLength > 0 ?
			//		("<?xml version=\"1.0\" encoding=\"UTF-8\"?><screen x='0' y='0' w='96' h='17'><region id='Main' x='0' y='0' w='96' h='17'><control id='id' x='0' y='0' h='16'><text fontid='FONT1'>" + textBoxPlayMessageVisualText.Text + "</text></control></region></screen>")
			//		:
			//		null
			//	);
			//
			//	// send command to Audio Server
			//	UInt32 totalMilliSecond = (System.Convert.ToUInt32(textBoxPlayMessageValidityPeriod.Text) * 1000);
			//	gAudioServer.getPAController().playMessage(targetPaZoneList, visualDevicesStringArray, gain, dvaItems, visualText, false, checkBoxPlayMessageOverrideExisting.Checked, totalMilliSecond, 0, 0);
			//	addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDVAPlay_Click(): Play DVA command sent");
			//}
			//catch (System.Exception excep)
			//{
			//	addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDVAPlay_Click(): exception caught: " + excep.Message);
			//}
		}

		/*******************************************************************************************************/
		private void buttonCreateDVASchedule_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): Not connected to AS");
					return;
				}

				if (dataGridViewDVAPaZoneIds.SelectedRows.Count == 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): No Pa Zones selected to play");
					return;
				}

				// create new schedule
				netspire.ScheduleDefinition schedule = new netspire.ScheduleDefinition();
				schedule.Id = 0;
				schedule.descrption = "This is schedule number >" + myScheduleId + "<";
				schedule.startYear = dateTimePickerStartDate.Value.Year;
				schedule.startMonth = dateTimePickerStartDate.Value.Month;
				schedule.startDay = dateTimePickerStartDate.Value.Day;
				schedule.startHour = System.Convert.ToInt32(comboBoxStartHours.SelectedItem);
				schedule.startMinute = System.Convert.ToInt32(comboBoxStartMins.SelectedItem);
				schedule.endYear = dateTimePickerEndDate.Value.Year;
				schedule.endMonth = dateTimePickerEndDate.Value.Month;
				schedule.endDay = dateTimePickerEndDate.Value.Day;
				schedule.endHour = System.Convert.ToInt32(comboBoxEndHours.SelectedItem);
				schedule.endMinute = System.Convert.ToInt32(comboBoxEndMins.SelectedItem);
				schedule.dayMask = System.Convert.ToInt32(comboBoxDayMask.SelectedItem);
				schedule.frequency = System.Convert.ToInt32(comboBoxFrequency.Text);

				// generate a list of selected PaZone
				foreach (DataGridViewRow row in dataGridViewDVAPaZoneIds.SelectedRows)
				{
					schedule.paZones.Add(row.Cells[0].Value.ToString());
				}

				// generate schedule depending on AnnouncementType
				if (tabControl3.SelectedTab.Text == "DVA")
				{
					if (dataGridViewDictionaryItems.SelectedRows.Count == 0)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): No DVA Items selected for scheduled playback");
						return;
					}

					// prepare schedule parameters
					schedule.announcementType = netspire.AnnouncementType.AT_DVA;
					// generate a list of DVA dictionary items
					foreach (DataGridViewRow row in dataGridViewDictionaryItems.SelectedRows)
					{
						schedule.dictionaryItems.Add(System.Convert.ToUInt32(row.Cells[0].Value.ToString()));
					}
					if (schedule.dictionaryItems.Count == 0)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): DVA Items not specified");
						return;
					}
					schedule.repeatCnt = 2;
				}
				else if (tabControl3.SelectedTab.Text == "TTS")
				{
					if (comboBoxTTSVocalizerList.Text.Length <= 0)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): TTS Language is empty. Cannot create new schedule");
						return;
					}

					String ttsText = textBoxTTSText.Text.Trim();
					if (ttsText.Length <= 0)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): TTS text is empty. Cannot create new schedule");
						return;
					}

					// prepare schedule parameters
					schedule.announcementType = netspire.AnnouncementType.AT_TTS;
					string[] selectedTTSVocalizerTokens = comboBoxTTSVocalizerList.Text.Split('^');
					netspire.TTSVocalizer newTTS = new netspire.TTSVocalizer();
					newTTS.language = selectedTTSVocalizerTokens[0];
					newTTS.voice = selectedTTSVocalizerTokens[1];
					newTTS.text = ttsText.ToString();
					schedule.ttsList.Add(newTTS);
				}
				else
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): Invalid TAB. Looking for DVA or TTS. Operation aborted");
					return;
				}
				schedule.gain = -6;
				schedule.chime = "99200";

				// send message to AS
				gAudioServer.getPAController().createScheduleSync(schedule);
				myScheduleId++;
				netspire.ScheduleDefinitionArray tmpScheduleArray = new netspire.ScheduleDefinitionArray();
				tmpScheduleArray = gAudioServer.getPAController().listSchedules();
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): Total Schedule Messages on AS >" + tmpScheduleArray.Count() + "<");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDVASchedule_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonDeleteDVASchedule_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDeleteDVASchedule_Click(): Not connected to AS");
					return;
				}

				netspire.ScheduleDefinitionArray scheduleArray = new netspire.ScheduleDefinitionArray();
				scheduleArray = gAudioServer.getPAController().listSchedules();
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDeleteDVASchedule_Click(): Total Schedules Found >" + scheduleArray.Count() + "<");
				for (int i = 0; i < scheduleArray.Count(); i++)
				{
					gAudioServer.getPAController().deleteSchedule(scheduleArray.ElementAt(i).Id);
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDeleteDVASchedule_Click(): Deleted Schedule Number >" + scheduleArray.ElementAt(i).Id);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDeleteDVASchedule_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonListDVASchedules_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListDVASchedules_Click(): Not connected to AS");
					return;
				}

				netspire.ScheduleDefinitionArray scheduleArray = new netspire.ScheduleDefinitionArray();
				scheduleArray = gAudioServer.getPAController().listSchedules();
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListDVASchedules_Click(): Total Schedules Found >" + scheduleArray.Count() + "<");
				listBoxSchedules.Items.Clear();
				for (int i = 0; i < scheduleArray.Count(); i++)
				{
					listBoxSchedules.Items.Add(scheduleArray.ElementAt(i).descrption);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonListDVASchedules_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonCreateDestination_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				netspire.StringArray bPartyDestinationStringArray = new netspire.StringArray();
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDestination_Click(): Not connected to AS");
					return;
				}

				if (textBoxCreateDestinationBPartyExtList.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDestination_Click(): B-Party Ext List not specified");
					return;
				}
				else
				{
					// split b-party extention list (comma seperated)
					string[] extList = textBoxCreateDestinationBPartyExtList.Text.Split(',');
					foreach (string ext in extList)
					{
						if (ext != "")
							bPartyDestinationStringArray.Add(ext);
					}
					if (bPartyDestinationStringArray.Count == 0)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDestination_Click(): B-Party List not specified - looking for comma seperated list - e.g. 1234,5678");
						return;
					}
				}

				if (textBoxCreateDestinationCLI.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDestination_Click(): Caller Line Identification not specified");
					return;
				}

				if (textBoxCreateDestinationDeviceID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDestination_Click(): Device ID not specified");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.createDestination(bPartyDestinationStringArray, textBoxCreateDestinationCLI.Text, textBoxCreateDestinationDeviceID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDestination_Click(): New Destination Map for CLI >" + textBoxCreateDestinationCLI.Text + "< on device ID >" + textBoxCreateDestinationDeviceID.Text + "< created");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateDestination_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonResetDestinationMap_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResetDestinationMap_Click(): Not connected to AS");
					return;
				}

				if (textBoxResetDestinationMapDeviceID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResetDestinationMap_Click(): Device ID not specified");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.resetDestinationMap(textBoxResetDestinationMapDeviceID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResetDestinationMap_Click(): Destination Map on device >" + textBoxResetDestinationMapDeviceID.Text + "< has been cleared");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResetDestinationMap_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonCreateCall_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			textBoxCreateCallID.Text = "";
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateCall_Click(): Not connected to AS");
					return;
				}

				if (textBoxCreateCallAPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateCall_Click(): A-Party ID not specified");
					return;
				}

				if (textBoxCreateCallBPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateCall_Click(): B-Party ID not specified");
					return;
				}

				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateCall_Click(): Creating new call. Note that this function call is blocking (max block time is 10seconds)");
				netspire.CallController caC = gAudioServer.getCallController();
				int callReference = caC.createCall(textBoxCreateCallAPartyID.Text, textBoxCreateCallBPartyID.Text);
				if (callReference > 0)
				{
					textBoxCreateCallID.Text = callReference.ToString();
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateCall_Click(): Call created - callReferenceID >" + callReference + "<");
				}
				else
				{
					textBoxCreateCallID.Text = "Error";
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateCall_Click(): Failed to create call");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonCreateCall_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonAnswerCallOnTerminal_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonAnswerCallOnTerminal_Click(): Not connected to AS");
					return;
				}

				if (textBoxAnswerCallOnTerminalBPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonAnswerCallOnTerminal_Click(): B-Party ID is mandatory");
					return;
				}

				if (textBoxAnswerCallOnTerminalCallID.Text == "" && textBoxAnswerCallOnTerminalAPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonAnswerCallOnTerminal_Click(): MUST specify either Call ID or A-Party ID");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.answerCallOnTerminal((textBoxAnswerCallOnTerminalCallID.Text == "" ? 0 : System.Convert.ToInt32(textBoxAnswerCallOnTerminalCallID.Text)), textBoxAnswerCallOnTerminalBPartyID.Text, textBoxAnswerCallOnTerminalAPartyID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonAnswerCallOnTerminal_Click(): Request sent to AS to answer call on terminal ID >" + textBoxAnswerCallOnTerminalBPartyID.Text + "<");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonAnswerCallOnTerminal_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonResumeCall_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCall_Click(): Not connected to AS");
					return;
				}

				if (textBoxResumeCallCalleeID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCall_Click(): B-Party ID not specified");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.resumeCall((textBoxResumeCallID.Text == "" ? 0 : System.Convert.ToInt32(textBoxResumeCallID.Text)), textBoxResumeCallCalleeID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCall_Click(): B-Party Terminal ID >" + textBoxResumeCallCalleeID.Text + "< call has been resumed");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCall_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonResumeCallOnTerminal_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCallOnTerminal_Click(): Not connected to AS");
					return;
				}

				if (textBoxResumeCallOnTerminalBPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCallOnTerminal_Click(): B-Party ID is mandatory");
					return;
				}

				if (textBoxResumeCallOnTerminalCallID.Text == "" && textBoxResumeCallOnTerminalAPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCallOnTerminal_Click(): MUST specify either Call ID or A-Party ID");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.resumeCallOnTerminal((textBoxResumeCallOnTerminalCallID.Text == "" ? 0 : System.Convert.ToInt32(textBoxResumeCallOnTerminalCallID.Text)), textBoxResumeCallOnTerminalBPartyID.Text, textBoxResumeCallOnTerminalAPartyID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCallOnTerminal_Click(): Request sent to AS to resume/unhold call on terminal ID >" + textBoxResumeCallOnTerminalBPartyID.Text + "<");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonResumeCallOnTerminal_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonTransferCall_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTransferCall_Click(): Not connected to AS");
					return;
				}

				if (textBoxTransferCallReferenceID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTransferCall_Click(): Call Reference ID not specified");
					return;
				}

				if (textBoxTransferCallNewBPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTransferCall_Click(): New B-Party ID not specified");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.transferCall(Convert.ToInt32(textBoxTransferCallReferenceID.Text), textBoxTransferCallNewBPartyID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTransferCall_Click(): Call Transferred to new Party ID >" + textBoxTransferCallNewBPartyID.Text + "<");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTransferCall_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonHoldCall_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonHoldCall_Click(): Not connected to AS");
					return;
				}

				if (textBoxHoldCallCalleelID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonHoldCall_Click(): B-Party ID not specified");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.holdCall((textBoxHoldCallID.Text == "" ? 0 : System.Convert.ToInt32(textBoxHoldCallID.Text)), textBoxHoldCallCalleelID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonHoldCall_Click(): B-Party Terminal ID >" + textBoxHoldCallCalleelID.Text + "< has been put on hold");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonHoldCall_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonTerminateCall_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTerminateCall_Click(): Not connected to AS");
					return;
				}

				if (textBoxTerminateCallID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTerminateCall_Click(): Call Reference ID not specified");
					return;
				}

				if (textBoxTerminateCallBPartyID.Text == "")
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTerminateCall_Click(): B-Party ID not specified - terminating all connections");
				}

				netspire.CallController caC = gAudioServer.getCallController();
				caC.terminateCall(System.Convert.ToInt32(textBoxTerminateCallID.Text), textBoxTerminateCallBPartyID.Text);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTerminateCall_Click(): Call Reference ID >" + textBoxTerminateCallID.Text + "< terminated");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonTerminateCall_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetSIPTrunks_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetSIPTrunks_Click(): Not connected to AS");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				netspire.SIPTrunkList sipTrunkList = caC.getSIPTrunks();
				String message = "";
				for (int i = 0; i < sipTrunkList.Count; i++)
				{
					netspire.SIPTrunk thisSIPTrunk = sipTrunkList.ElementAt(i);
					if (message.Length > 0)
					{
						message += "\n";
					}
					message += "Name: " + thisSIPTrunk.getName() +
								", Type: " + thisSIPTrunk.getType() +
								", Device ID: " + thisSIPTrunk.getDeviceId() +
								", Status: " + thisSIPTrunk.getStatus();
				}
				if (message.Length == 0)
				{
					message = "No SIP Trunks found in the system";
				}
				MessageBox.Show(message);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetSIPTrunks_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetISDNTrunks_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetISDNTrunks_Click(): Not connected to AS");
					return;
				}

				netspire.CallController caC = gAudioServer.getCallController();
				netspire.ISDNTrunkList isdnTrunkList = caC.getISDNTrunks();
				String message = "";
				for (int i = 0; i < isdnTrunkList.Count; i++)
				{
					netspire.ISDNTrunk thisISDNTrunk = isdnTrunkList.ElementAt(i);
					if (message.Length > 0)
					{
						message += "\n";
					}
					message += "Name: " + thisISDNTrunk.getName() +
								", Type: " + thisISDNTrunk.getType() +
								", Device ID: " + thisISDNTrunk.getDeviceId() +
								", Layer 1 Status: " + thisISDNTrunk.getLayer1Status() +
								", Layer 2/3 Status: " + thisISDNTrunk.getLayer2_3Status();
				}
				if (message.Length == 0)
				{
					message = "No ISDN Trunks found in the system";
				}
				MessageBox.Show(message);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetISDNTrunks_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void checkBoxStartCallsPolling_CheckedChanged(object sender, EventArgs e)
		{
			timerCallStates.Enabled = checkBoxCallsPolling.Checked;
			timerCallStates.Interval = int.Parse(comboBoxCallStatesPollingSeconds.Text) * 1000;
			checkBoxCallsPolling.Text = (checkBoxCallsPolling.Checked ? "Stop Polling for Call(s)" : "Start Polling for Call(s)");
			checkBoxCallsPolling.BackColor = (checkBoxCallsPolling.Checked ? System.Drawing.Color.LightGreen : System.Drawing.Color.Khaki);
		}

		/*******************************************************************************************************/
		private void timerCallStates_Tick(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerCallStates_Tick(): Not connected to AS");
					return;
				}

				// poll for calls
				netspire.CallController caC = gAudioServer.getCallController();
				netspire.CallInfoArray callInfoArray = caC.getCalls();
				listViewCalls.Items.Clear();
				for (short i = 0; i < callInfoArray.Count; i++)
				{
					netspire.CallInfo thisCallInfo = callInfoArray.ElementAt(i);
					string[] newItemSubItems = new string[]
					{
						thisCallInfo.callId.ToString(),
						thisCallInfo.callStartTime.ToString(),
						thisCallInfo.callDuration.ToString(),
						thisCallInfo.callAnswerTime.ToString(),
						thisCallInfo.callAPartyId.ToString(),
						thisCallInfo.callBPartyId.ToString(),
						thisCallInfo.callTargetIds,
						thisCallInfo.callState.ToString(),
						thisCallInfo.callReleaseCause.ToString()
					};
					ListViewItem newItem = new ListViewItem(newItemSubItems);
					listViewCalls.Items.Add(newItem);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerCallStates_Tick(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void trackBarDVAGain_Scroll(object sender, EventArgs e)
		{
			// Slider range is 0 to +96 and allowed gain range is [-96, 0] dB.
			labelDVAGain.Text = "DVA Gain (" + (trackBarDVAGain.Value - trackBarDVAGain.Maximum) + "dB)";
		}

		/*******************************************************************************************************/
		private void trackBarCustomPriority_Scroll(object sender, EventArgs e)
		{
			// Slider range is 0 to 1000
			labelCustomPriority.Text = trackBarCustomPriority.Value.ToString();
		}

#if (BUCHAREST_OA0784)
        /*******************************************************************************************************
         * Bucharest OA0784 - Rolling Stock specific functions
         *******************************************************************************************************/
        private int cp1DstNo = 2001122, cp2DstNo = 2001822;
        private bool cp1Active = false, cp1AutoInfoActive = false, cp2Active = false, cp2AutoInfoActive = false;
        private static Mutex bucharestMutex = new Mutex();
        private void updateBucharest(netspire.Device device)
        {
            // CP inputs and outpus:
            // CP Active switch : ICI INPUT 5, 2 means active, 1 means inactive;
            // AutoInfo button: ICI INPUT 17;
            // AutoInfo LED: ICI OUTPUT 25
            // Monitor the AutoInfo state of both CPs, if any of them the AutoInfo state is “disabled”, the SdkTester need to disable the DVA, if there is a DVA ongoing, need to clear that DVA.
            // When the CP is active (can either check the ICI INPUT or check the device role), the AutoInfo LED should represent the current CP state stored in SdkTester (ON means AutoInfo disabled, OFF means AutoInfo enabled). Press the AutoInfo button will toggle the AutoInfo state of this CP.
            // When the CP is inactive, the AutoInfo state of SdkTester should be “enabled” and the AutoInfo LED should be OFF.
            bucharestMutex.WaitOne();
            if (device.getDstNo() == cp1DstNo || device.getDstNo() == cp2DstNo)
            {
                if (!device.getInputState(5))   // CP Inactive
                {
                    if (device.getDstNo() == cp1DstNo)
                    {
                        cp1Active = false;
                        buttonCP1ActiveStatus.BackColor = System.Drawing.Color.Red;
                        buttonCP1AutoInfoStatus.BackColor = System.Drawing.Color.Transparent;
                        device.setOutputState(25, 2);           // 1 = close_relay, 2 = open_relay
                    }
                    if (device.getDstNo() == cp2DstNo)
                    {
                        cp2Active = false;
                        buttonCP2ActiveStatus.BackColor = System.Drawing.Color.Red;
                        buttonCP2AutoInfoStatus.BackColor = System.Drawing.Color.Transparent;
                        device.setOutputState(25, 2);           // 1 = close_relay, 2 = open_relay
                    }
                }
                else
                {
                    if (device.getDstNo() == cp1DstNo)
                    {
                        cp1Active = true;
                        buttonCP1ActiveStatus.BackColor = System.Drawing.Color.Transparent;
                        if (device.getInputState(17)) cp1AutoInfoActive = !cp1AutoInfoActive;
                        if (!cp1AutoInfoActive)
                        {
                            buttonCP1AutoInfoStatus.BackColor = System.Drawing.Color.Green;
                            device.setOutputState(25, 1);       // 1 = close_relay, 2 = open_relay
                        }
                        else
                        {
                            buttonCP1AutoInfoStatus.BackColor = System.Drawing.Color.Transparent;
                            device.setOutputState(25, 2);       // 1 = close_relay, 2 = open_relay
                        }
                    }
                    if (device.getDstNo() == cp2DstNo)
                    {
                        cp2Active = true;
                        buttonCP2ActiveStatus.BackColor = System.Drawing.Color.Transparent;
                        if (device.getInputState(17)) cp2AutoInfoActive = !cp2AutoInfoActive;
                        if (!cp2AutoInfoActive)
                        {
                            buttonCP2AutoInfoStatus.BackColor = System.Drawing.Color.Green;
                            device.setOutputState(25, 1);       // 1 = close_relay, 2 = open_relay
                        }
                        else
                        {
                            buttonCP2AutoInfoStatus.BackColor = System.Drawing.Color.Transparent;
                            device.setOutputState(25, 2);       // 1 = close_relay, 2 = open_relay
                        }
                    }
                }
                if ( (!cp1Active && !cp2Active) ||
                     (!cp1Active && cp2Active && cp2AutoInfoActive) ||
                     (!cp2Active && cp1Active && cp1AutoInfoActive) ||
                     (cp1Active && cp1AutoInfoActive && cp2Active && cp2AutoInfoActive)
                   )
                {
                    labelAutoInfoStatus.Text = "DVA Active";
                }
                else
                {
                    labelAutoInfoStatus.Text = "DVA Disabled";
                    //if (gAudioServer != null)
                    //{
                    //    gAudioServer.cancelMessage(0);
                    //}
                }
            }
            bucharestMutex.ReleaseMutex();
        }
#endif

#if (WARATAH_OA0979)
		/*******************************************************************************************************
		* WARATAH OA0979 - Rolling Stock specific functions
		 *******************************************************************************************************/
		private int txc1DstNo = 2001001, tcx8DstNo = 2008001;
		private int cspkr1PaSinkId = 200108101, cspkr8PaSinkId = 200808101;
		private static Mutex waratahMutex = new Mutex();
		private void updateWaratah(netspire.Device deviceUpdate, netspire.PaSink paSinkUpdate)
		{
			waratahMutex.WaitOne();

			// Process onDeviceUpdate - We are only interested in TCXs
			if (deviceUpdate != null && paSinkUpdate == null)
			{
				if (deviceUpdate.getDeviceClass() != netspire.Device.DeviceClass.COMMUNICATIONS_EXCHANGE)
				{
					return;
				}

				if (deviceUpdate.getDstNo() == txc1DstNo)
				{
					buttonTCXC1_DIN1.BackColor = (deviceUpdate.getInputState(1) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DIN2.BackColor = (deviceUpdate.getInputState(2) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DIN3.BackColor = (deviceUpdate.getInputState(3) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DIN4.BackColor = (deviceUpdate.getInputState(4) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DIN5.BackColor = (deviceUpdate.getInputState(5) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DIN6.BackColor = (deviceUpdate.getInputState(6) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DIN7.BackColor = (deviceUpdate.getInputState(7) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DIN8.BackColor = (deviceUpdate.getInputState(8) ? Color.LightBlue : Color.Transparent);

					buttonTCXC1_DOUT1.BackColor = (deviceUpdate.getOutputState(1) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DOUT2.BackColor = (deviceUpdate.getOutputState(2) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DOUT3.BackColor = (deviceUpdate.getOutputState(3) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DOUT4.BackColor = (deviceUpdate.getOutputState(4) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DOUT5.BackColor = (deviceUpdate.getOutputState(5) ? Color.LightBlue : Color.Transparent);
					buttonTCXC1_DOUT6.BackColor = (deviceUpdate.getOutputState(6) ? Color.LightBlue : Color.Transparent);
				}
				else if (deviceUpdate.getDstNo() == tcx8DstNo)
				{
					buttonTCXC8_DIN1.BackColor = (deviceUpdate.getInputState(1) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DIN2.BackColor = (deviceUpdate.getInputState(2) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DIN3.BackColor = (deviceUpdate.getInputState(3) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DIN4.BackColor = (deviceUpdate.getInputState(4) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DIN5.BackColor = (deviceUpdate.getInputState(5) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DIN6.BackColor = (deviceUpdate.getInputState(6) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DIN7.BackColor = (deviceUpdate.getInputState(7) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DIN8.BackColor = (deviceUpdate.getInputState(8) ? Color.LightBlue : Color.Transparent);

					buttonTCXC8_DOUT1.BackColor = (deviceUpdate.getOutputState(1) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DOUT2.BackColor = (deviceUpdate.getOutputState(2) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DOUT3.BackColor = (deviceUpdate.getOutputState(3) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DOUT4.BackColor = (deviceUpdate.getOutputState(4) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DOUT5.BackColor = (deviceUpdate.getOutputState(5) ? Color.LightBlue : Color.Transparent);
					buttonTCXC8_DOUT6.BackColor = (deviceUpdate.getOutputState(6) ? Color.LightBlue : Color.Transparent);
				}
			}

			// Process onPaSinkUpdate - We are only interested in CSPKR
			if (deviceUpdate == null && paSinkUpdate != null)
			{
				if (paSinkUpdate.id == cspkr1PaSinkId)
				{
					buttonWaratahCSPKR1Monitoring.Text = ("CSPKR1 Monitoring " + (paSinkUpdate.getPAMonitoring() ? "ON" : "OFF"));
				}
				else if (paSinkUpdate.id == cspkr8PaSinkId)
				{
					buttonWaratahCSPKR8Monitoring.Text = ("CSPKR8 Monitoring " + (paSinkUpdate.getPAMonitoring() ? "ON" : "OFF"));
				}
			}

			waratahMutex.ReleaseMutex();
		}
#endif

		/*******************************************************************************************************/
		private void buttonUpdateHMIData_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUpdateHMIData_Click(): Not connected to AS");
					return;
				}

				int key = System.Convert.ToInt32(textBoxHMIInputCode.Text, 10);
				String msg = textBoxHMIInputLine.Text + "," + textBoxHMIInputDestination.Text + "," + textBoxHMIInputStartStation.Text + "," + textBoxHMIInputSave.Text;
				gAudioServer.getConfigController().updateUserData1(1, msg);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerCallStates_Tick(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonDeleteHMIContent_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonDeleteHMIContent_Click(): Not connected to AS");
					return;
				}

				int rightArrowPos = listBoxHMIList.SelectedItem.ToString().IndexOf(">");
				int firstCommaPos = listBoxHMIList.SelectedItem.ToString().IndexOf(",");
				String msgKey = listBoxHMIList.SelectedItem.ToString().Substring((rightArrowPos + 2), ((firstCommaPos - 1) - (rightArrowPos + 1)));
				int key = System.Convert.ToInt32(msgKey.Trim(), 10);
				gAudioServer.getConfigController().deleteUserData1(key);
				listBoxHMIList.Items.RemoveAt(listBoxHMIList.SelectedIndex);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerCallStates_Tick(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonUploadFile_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUploadFile_Click(): Not connected to AS");
					return;
				}

				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Upload File... ";
				openFileDialog.InitialDirectory = "C:\\";
				//openFileDialog.Filter = "ZIP File (*.zip)|*.zip";
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					bool ret = gAudioServer.uploadFile(openFileDialog.FileName.ToString());
					if (ret)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUploadFile_Click(): upload file: " + openFileDialog.FileName.ToString());
					}
					else
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUploadFile_Click(): failed to start upload file: " + openFileDialog.FileName.ToString());
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonUploadFile_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void clearDebugMessagesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBoxDebugMsg.Items.Clear();
		}

		/*******************************************************************************************************/
		private void clearDeviceStatesEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBoxDeviceStateEvents.Items.Clear();
		}

		/*******************************************************************************************************/
		private void clearPAEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBoxPAEvents.Items.Clear();
		}

		/*******************************************************************************************************/
		private void clearCallEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBoxCallEvents.Items.Clear();
		}

		/*******************************************************************************************************/
		private void clearCDREvetnsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBoxCDREvents.Items.Clear();
		}

		/*******************************************************************************************************/
		private void clearTerminalEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBoxTerminalEvents.Items.Clear();
		}

		/*******************************************************************************************************/
		private void clearSignallingEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listBoxSignallingEvents.Items.Clear();
		}

		/*******************************************************************************************************/
		private void clearAllMessagesEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			clearDebugMessagesToolStripMenuItem_Click(sender, e);
			clearDeviceStatesEventsToolStripMenuItem_Click(sender, e);
			clearPAEventsToolStripMenuItem_Click(sender, e);
			clearCallEventsToolStripMenuItem_Click(sender, e);
			clearCDREvetnsToolStripMenuItem_Click(sender, e);
			clearTerminalEventsToolStripMenuItem_Click(sender, e);
			clearSignallingEventsToolStripMenuItem_Click(sender, e);
		}

		/*******************************************************************************************************/
		private void listBoxDebugMsg_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStripEvents.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		/*******************************************************************************************************/
		private void listBoxDeviceStateEvents_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStripEvents.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		/*******************************************************************************************************/
		private void listBoxPAEvents_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStripEvents.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		/*******************************************************************************************************/
		private void listBoxCallEvents_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStripEvents.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		/*******************************************************************************************************/
		private void listBoxCDREvents_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStripEvents.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		/*******************************************************************************************************/
		private void listBoxTerminalEvents_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStripEvents.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		/*******************************************************************************************************/
		private void listBoxSignallingEvents_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStripEvents.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		/*******************************************************************************************************/
		private void clearMessagsEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (tabControl2.SelectedIndex)
			{
				case 0:
					clearDebugMessagesToolStripMenuItem_Click(sender, e);
					break;
				case 1:
					clearDeviceStatesEventsToolStripMenuItem_Click(sender, e);
					break;
				case 2:
					clearPAEventsToolStripMenuItem_Click(sender, e);
					break;
				case 3:
					clearCallEventsToolStripMenuItem_Click(sender, e);
					break;
				case 4:
					clearCDREvetnsToolStripMenuItem_Click(sender, e);
					break;
				case 5:
					clearTerminalEventsToolStripMenuItem_Click(sender, e);
					break;
				case 6:
					clearSignallingEventsToolStripMenuItem_Click(sender, e);
					break;
				default:
					break;
			}
		}

		/*******************************************************************************************************/
		private void clear_Click(object sender, EventArgs e)
		{
			listViewEventsPolling.Items.Clear();
		}

		/*******************************************************************************************************/
		private void checkBoxEventsPolling_CheckedChanged(object sender, EventArgs e)
		{
			timerEventsPolling.Enabled = checkBoxEventsPolling.Checked;
			timerEventsPolling.Interval = int.Parse(comboBoxEventsPollingSeconds.Text) * 1000;
			checkBoxEventsPolling.Text = (checkBoxEventsPolling.Checked ? "Stop Polling for Events" : "Start Polling for Events");
			checkBoxEventsPolling.BackColor = (checkBoxEventsPolling.Checked ? System.Drawing.Color.LightGreen : System.Drawing.Color.Khaki);
		}

		/*******************************************************************************************************/
		private void timerEventsPolling_Tick(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerEventsPolling_Tick(): Not connected to AS");
					return;
				}

				netspire.EventItemArray eventItemArray = gAudioServer.getEventList();
				for (int i = 0; i < eventItemArray.Count(); i++)
				{
					String additionalData = "";
					switch (eventItemArray.ElementAt(i).type)
					{
						case netspire.EventItem.EVENT_TYPE.COMMS_LINK_UP:
							// nothing to process
							break;
						case netspire.EventItem.EVENT_TYPE.COMMS_LINK_DOWN:
							// nothing to process
							break;
						case netspire.EventItem.EVENT_TYPE.AUDIO_SERVER_CONNECTED:
							// nothing to process
							break;
						case netspire.EventItem.EVENT_TYPE.AUDIO_SERVER_DISCONNECTED:
							// nothing to process
							break;
						case netspire.EventItem.EVENT_TYPE.DEVICE_STATE_CHANGED:
							netspire.Device device = eventItemArray.ElementAt(i).device;
							additionalData = device.getName() + "," + device.getStateText();
							break;
						case netspire.EventItem.EVENT_TYPE.PA_SOURCE_UPDATED:
						case netspire.EventItem.EVENT_TYPE.PA_SOURCE_DELETED:
							netspire.PaSource paSource = eventItemArray.ElementAt(i).paSource;
							additionalData = paSource.location + "," + paSource.label + "," + paSource.ipAddress;
							break;
						case netspire.EventItem.EVENT_TYPE.PA_SINK_UPDATED:
						case netspire.EventItem.EVENT_TYPE.PA_SINK_DELETED:
							netspire.PaSink paSink = eventItemArray.ElementAt(i).paSink;
							additionalData = paSink.location + "," + paSink.label + "," + paSink.state;
							break;
						case netspire.EventItem.EVENT_TYPE.PA_TRIGGER_UPDATED:
						case netspire.EventItem.EVENT_TYPE.PA_TRIGGER_DELETED:
							netspire.PaTrigger paTrigger = eventItemArray.ElementAt(i).paTrigger;
							additionalData = paTrigger.associatedSourceId + "," + paTrigger.type + "," + paTrigger.state;
							break;
						case netspire.EventItem.EVENT_TYPE.PA_SELECTOR_UPDATED:
						case netspire.EventItem.EVENT_TYPE.PA_SELECTOR_DELETED:
							netspire.PaSelector paSelector = eventItemArray.ElementAt(i).paSelector;
							//additionalData = paTrigger.associatedSourceId + "," + paTrigger.type;
							break;
						case netspire.EventItem.EVENT_TYPE.CALL_UPDATED:
						case netspire.EventItem.EVENT_TYPE.CALL_DELETED:
							break;
						case netspire.EventItem.EVENT_TYPE.CDR_MESSAGE_UPDATED:
						case netspire.EventItem.EVENT_TYPE.CDR_MESSAGE_DELETED:
							netspire.CDRInfo cdrInfo = eventItemArray.ElementAt(i).cdrInfo;
							additionalData = cdrInfo.startDateDay + "," + cdrInfo.startDateMonth + "," + cdrInfo.startDateYear;
							break;
						case netspire.EventItem.EVENT_TYPE.TERMINAL_UPDATED:
						case netspire.EventItem.EVENT_TYPE.TERMINAL_DELETED:
							netspire.TerminalInfo termInfo = eventItemArray.ElementAt(i).termInfo;
							additionalData = termInfo.termAddress + "," + termInfo.termLocation;
							break;
						default:
							break;
					}

					string[] newItemSubItems = new string[]
					{
						eventItemArray.ElementAt(i).dateTime,
						eventItemArray.ElementAt(i).type.ToString(),
						eventItemArray.ElementAt(i).id.ToString(),
						additionalData
					};
					ListViewItem newItem = new ListViewItem(newItemSubItems);

					// limit items in the listViewEventsPolling to 100.
					if (listViewEventsPolling.Items.Count >= 100)
					{
						listViewEventsPolling.Items.Clear();
					}
					listViewEventsPolling.Items.Add(newItem);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerEventsPolling_Tick(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void listViewDeviceStates_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Right)
				{
					contextMenuStripListViewDevices.Enabled = (gAudioServer != null && gAudioServer.isCommsEstablished() && gAudioServer.isAudioConnected());
					contextMenuStripListViewDevices.Show(Cursor.Position);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "listViewDeviceStates_MouseDown(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void setDeviceIsolationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "setDeviceIsolationToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string deviceId = Microsoft.VisualBasic.Interaction.InputBox("Device ID to isolate:", "Isolate Device", "");
				if (deviceId.Length != 0)
				{
					gAudioServer.setDeviceIsolation(deviceId, true);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "setDeviceIsolationToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void deIsolateDeviceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "deIsolateDeviceToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string deviceId = Microsoft.VisualBasic.Interaction.InputBox("Device ID to un-isolate:", "Un-Isolate Device", "");
				if (deviceId.Length != 0)
				{
					gAudioServer.setDeviceIsolation(deviceId, false);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "deIsolateDeviceToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void getDynamicConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "getDynamicConfigurationToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				netspire.KeyValueMap dynamicConfiguration = gAudioServer.getDynamicConfiguration();
				string msgForUser = "";
				for (int i = 0; i < dynamicConfiguration.Count; i++)
				{
					if (msgForUser.Length != 0)
					{
						msgForUser += "\n";
					}
					msgForUser += (dynamicConfiguration.ElementAt(i).Key + " = " + dynamicConfiguration.ElementAt(i).Value);
				}
				MessageBox.Show((msgForUser.Length > 0) ? msgForUser : "No Dynamic Configuration Found", "Dynamic Configuration");
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "deIsolateDeviceToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void setDynamicConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "setDynamicConfigurationToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string dynamicConfigKeyValueString = Microsoft.VisualBasic.Interaction.InputBox("Enter variable_name,value and press OK", "Add/Update Dynamic Configuration", "");
				if (dynamicConfigKeyValueString.Length != 0)
				{
					string[] dynamicConfigKeyValueTokens = dynamicConfigKeyValueString.Split(',');
					if (dynamicConfigKeyValueTokens.Length == 2)
					{
						netspire.KeyValueMap userDefinedDynamicConfig = new netspire.KeyValueMap();
						userDefinedDynamicConfig.Add(dynamicConfigKeyValueTokens[0], dynamicConfigKeyValueTokens[1]);
						gAudioServer.setDynamicConfiguration(userDefinedDynamicConfig);
					}
					else
					{
						MessageBox.Show("Operation Failed - expecting variable_name,value", "Failed");
					}
				}
				else
				{
					MessageBox.Show("Operation Failed - expecting variable_name,value", "Failed");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "setDynamicConfigurationToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void initiateDeviceHealthTestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "initiateDeviceHealthTestToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string deviceId = Microsoft.VisualBasic.Interaction.InputBox("Enter deviceId (blank for all devices) and press OK", "Device Health Test", " ");
				if (deviceId.Length != 0)   // user pressed OK
				{
					if (deviceId.Length == 1)   // default value of 1 space. This is used to differentiate between empty deviceId.
					{
						deviceId = "";
					}
					gAudioServer.initiateDeviceHealthTest(deviceId);
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "initiateDeviceHealthTestToolStripMenuItem_Click(): Command sent to initiate device health test on: " +
						(deviceId.Length > 0 ? deviceId : "All PEIs")
					);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "initiateDeviceHealthTestToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void readDigitalInputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "readDigitalInputToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter deviceId,portNo and press OK", "Read Digital Output", " ");
				if (userInput.Length != 0)
				{
					string[] userInputTokens = userInput.Split(',');
					if (userInputTokens.Length == 2)
					{
						bool foundDevice = false;
						netspire.DeviceStateArray tmpDevices = gAudioServer.getDeviceStates();
						userInputTokens[0] = userInputTokens[0].ToUpper();
						for (int i = 0; i < tmpDevices.Count; i++)
						{
							if (tmpDevices.ElementAt(i).getName() == userInputTokens[0])
							{
								bool result = tmpDevices.ElementAt(i).getInputState(System.Convert.ToInt32(userInputTokens[1]));
								MessageBox.Show(userInputTokens[0] + " Digital Input Port >" + userInputTokens[1] + "< is >" + (result ? "set" : "not-set") + "<", "Result");
								foundDevice = true;
								break;
							}
						}
						if (!foundDevice)
						{
							MessageBox.Show("Operation Failed - deviceId not found", "Failed");
						}
					}
					else
					{
						MessageBox.Show("Operation Failed - expecting deviceId,portNo", "Failed");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "readDigitalInputToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void setDigitalOutputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "setDigitalOutputToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter deviceId,portNo and press OK", "Set Digital Output", "");
				if (userInput.Length != 0)
				{
					string[] userInputTokens = userInput.Split(',');
					if (userInputTokens.Length == 2)
					{
						bool foundDevice = false;
						netspire.DeviceStateArray tmpDevices = gAudioServer.getDeviceStates();
						userInputTokens[0] = userInputTokens[0].ToUpper();
						for (int i = 0; i < tmpDevices.Count; i++)
						{
							if (tmpDevices.ElementAt(i).getName() == userInputTokens[0])
							{
								tmpDevices.ElementAt(i).setOutputState(System.Convert.ToUInt32(userInputTokens[1]), 1);
								foundDevice = true;
								break;
							}
						}
						if (!foundDevice)
						{
							MessageBox.Show("Operation Failed - deviceId not found", "Failed");
						}
					}
					else
					{
						MessageBox.Show("Operation Failed - expecting deviceId,portNo", "Failed");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "setDigitalOutputToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void unsetDigitalOutputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "unsetDigitalOutputToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter deviceId,portNo and press OK", "Unset Digital Output", "");
				if (userInput.Length != 0)
				{
					string[] userInputTokens = userInput.Split(',');
					if (userInputTokens.Length == 2)
					{
						bool foundDevice = false;
						netspire.DeviceStateArray tmpDevices = gAudioServer.getDeviceStates();
						userInputTokens[0] = userInputTokens[0].ToUpper();
						for (int i = 0; i < tmpDevices.Count; i++)
						{
							if (tmpDevices.ElementAt(i).getName() == userInputTokens[0])
							{
								tmpDevices.ElementAt(i).setOutputState(System.Convert.ToUInt32(userInputTokens[1]), 2);
								foundDevice = true;
								break;
							}
						}
						if (!foundDevice)
						{
							MessageBox.Show("Operation Failed - deviceId not found", "Failed");
						}
					}
					else
					{
						MessageBox.Show("Operation Failed - expecting deviceId,portNo", "Failed");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "unsetDigitalOutputToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void readDigitalOutputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "unsetDigitalOutputToolStripMenuItem_Click(): Not connected to AS");
					return;
				}

				// InputBox returns "" string when the cancel button is clicked irrespective of what you set for default value.
				string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter deviceId,portNo and press OK", "Read Digital Output", "");
				if (userInput.Length != 0)
				{
					string[] userInputTokens = userInput.Split(',');
					if (userInputTokens.Length == 2)
					{
						bool foundDevice = false;
						netspire.DeviceStateArray tmpDevices = gAudioServer.getDeviceStates();
						userInputTokens[0] = userInputTokens[0].ToUpper();
						for (int i = 0; i < tmpDevices.Count; i++)
						{
							if (tmpDevices.ElementAt(i).getName() == userInputTokens[0])
							{
								bool result = tmpDevices.ElementAt(i).getOutputState(System.Convert.ToInt32(userInputTokens[1]));
								MessageBox.Show(userInputTokens[0] + " Digital Output Port >" + userInputTokens[1] + "< is >" + (result ? "set" : "not-set") + "<", "Result");
								foundDevice = true;
								break;
							}
						}
						if (!foundDevice)
						{
							MessageBox.Show("Operation Failed - deviceId not found", "Failed");
						}
					}
					else
					{
						MessageBox.Show("Operation Failed - expecting deviceId,portNo", "Failed");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "readDigitalOutputToolStripMenuItem_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void connectToKTMBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("100.0.3.1");
			serverAddresses.Add("100.0.3.2");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToKLMonorailToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("192.168.13.7");
			serverAddresses.Add("192.168.13.8");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");
			configItems.Add("NETSPIRE_SDK_HOST_DST_LIST", "2013007,2013008");   // required as KL-Monorail is fixed + rolling-stock

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToBucharestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("10.33.11.1");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToGCRTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("10.20.1.121");
			serverAddresses.Add("10.20.1.123");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToHKToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("192.168.8.1");
			serverAddresses.Add("192.168.8.2");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");
			configItems.Add("NETSPIRE_SDK_HOST_DST_LIST", "2008001,2008002");   // required as HK is fixed + rolling-stock

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToSLRToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address for Sydney Light Rail
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("10.0.16.2");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToWaratahToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address for Waratah
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("10.0.7.179");
			serverAddresses.Add("10.0.231.179");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToMetrolinxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address for Metrolinx
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("10.194.77.11");
			serverAddresses.Add("10.194.77.12");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToOttawaLightRailToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// set Audio Server address for Ottawa Light Rail
			netspire.StringArray serverAddresses = new netspire.StringArray();
			serverAddresses.Add("10.1.40.100");
			serverAddresses.Add("10.1.40.101");

			// set config items
			netspire.KeyValueMap configItems = new netspire.KeyValueMap();
			//configItems.Add("NETSPIRE_SDK_SOCKET_PORT", "20770");

			// connect to AudioServer
			estalishConnectionToAudioServer(serverAddresses, configItems);
		}

		/*******************************************************************************************************/
		private void connectToASToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addNewDebugMessage(EventType.DEBUG_MESSAGE, "");
			try
			{
				netspire.KeyValueMap configItems = new netspire.KeyValueMap();
				netspire.StringArray serverAddresses = new netspire.StringArray();
				connectDialog connectDialogForm = new connectDialog();
				DialogResult connectDialogResult = connectDialogForm.ShowDialog(this);
				if (!connectDialogForm.okButtonClicked)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): connection request cancelled. Not connecting!");
					return;
				}

				// extract server addresses
				if (connectDialogForm.serverAddressStr.Length == 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): no server addresses found to connect. Not connecting!");
					return;
				}
				else
				{
					System.Net.IPAddress ipAddress = null;
					string[] usrServerAddressList = connectDialogForm.serverAddressStr.Split(',');
					foreach (string usrServerAddress in usrServerAddressList)
					{
						if (usrServerAddress.Length > 0)
						{
							if ((System.Net.IPAddress.TryParse(usrServerAddress, out ipAddress)) == false)
							{
								addNewDebugMessage(EventType.DEBUG_MESSAGE, "Error: Invalid server IP Address >" + usrServerAddress + "<. Not connecting");
								return;
							}
							else
							{
								serverAddresses.Add(usrServerAddress);
							}
						}
					}

					if (serverAddresses.Count == 0)
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): no server addresses found to connect. Not connecting!");
						return;
					}
				}

				// get server type - CXS/TCX Server or NAC Server
				if (!connectDialogForm.connectToCXSServer)
				{
					configItems.Add("NETSPIRE_SDK_INDIRECT_CXS_CONNECTION", "");
					configItems.Add("NETSPIRE_SDK_USE_CONFIG", "");
				}

				// get end-pt port no
				if (connectDialogForm.endPtPortNoStr.Length != 0)
				{
					double Num = 0;
					bool isNum = double.TryParse(connectDialogForm.endPtPortNoStr, out Num);
					if (isNum)
					{
						configItems.Add("NETSPIRE_SDK_SOCKET_PORT", connectDialogForm.endPtPortNoStr);
					}
					else
					{
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): invalid port no: " + connectDialogForm.endPtPortNoStr);
						return;
					}
				}

				// get host destination list - only applicable for KL Monorail Project
				// e.g. 2011001,2011002
				if (connectDialogForm.hostDestinationListStr.Length != 0)
				{
					configItems.Add("NETSPIRE_SDK_HOST_DST_LIST", connectDialogForm.hostDestinationListStr);
				}

				// set logging if enabled
				configItems.Add("NETSPIRE_SDK_SET_LOG_LEVEL", connectDialogForm.logLevel);
				if (connectDialogForm.debugFileName.Length > 0)
				{
					configItems.Add("NETSPIRE_SDK_SET_DEBUG_FILE", connectDialogForm.debugFileName);
				}
				if (connectDialogForm.errorFileName.Length > 0)
				{
					configItems.Add("NETSPIRE_SDK_SET_ERROR_FILE", connectDialogForm.errorFileName);
				}

				// connect to AudioServer
				estalishConnectionToAudioServer(serverAddresses, configItems);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): exception caught: " + excep.Message);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): exception stack trace: " + excep.StackTrace);
				if (excep.InnerException != null)
				{
					excep = excep.InnerException;
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): exception caught: " + excep.Message);
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "connectToASToolStripMenuItem_Click(): exception stack trace: " + excep.StackTrace);
				}
			}
		}

		/*******************************************************************************************************/
		private void checkBoxSignallingInfoPolling_CheckedChanged(object sender, EventArgs e)
		{
			timerSignallingEvents.Enabled = checkBoxSignallingInfoPolling.Checked;
			timerSignallingEvents.Interval = int.Parse(comboBoxSignallingInfoPollingSeconds.Text) * 1000;
			checkBoxSignallingInfoPolling.Text = (checkBoxSignallingInfoPolling.Checked ? "Stop Polling for Passenger Information" : "Start Polling for Passenger Information");
			checkBoxSignallingInfoPolling.BackColor = (checkBoxSignallingInfoPolling.Checked ? System.Drawing.Color.LightGreen : System.Drawing.Color.Khaki);
		}

		/*******************************************************************************************************/
		private void timerSignallingEvents_Tick(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerSignallingEvents_Tick(): Not connected to AS");
					return;
				}

				// poll for passenger information

				// get PIS server status
				bool pisServerStatus = gAudioServer.getPassengerInformationServer().getServerStatus();
				labelPISServerStatus.Text = (pisServerStatus ? "PIS Server Online" : "PIS Server Offline");
				labelPISServerStatus.ForeColor = (pisServerStatus ? Color.Green : Color.Red);

				// get available lines
				netspire.LineList allLines = gAudioServer.getPassengerInformationServer().getLines();
				string lines = "";
				for (int i = 0; i < allLines.Count; i++)
				{
					if (lines.Length > 0)
						lines += ", ";
					lines += "(" + allLines.ElementAt(i).getId().ToString() + "," + allLines.ElementAt(i).getName().ToString() + ")";
				}
				textBoxAvailableLines.Text = (lines.Length > 0 ? lines : "No Lines");

				// get available vehicles
				netspire.VehicleList allVehicles = gAudioServer.getPassengerInformationServer().getVehicles();
				string vehicles = "";
				for (int i = 0; i < allVehicles.Count; i++)
				{
					if (vehicles.Length > 0)
						vehicles += ", ";
					vehicles += "(" + allVehicles.ElementAt(i).getId().ToString() + "," + allVehicles.ElementAt(i).getName().ToString() + ")";
				}
				textBoxAvailableVehicles.Text = (vehicles.Length > 0 ? vehicles : "No Vehicles");

				// get available stations
				netspire.StationList allStations = gAudioServer.getPassengerInformationServer().getStations();
				string stations = "";
				for (int i = 0; i < allStations.Count; i++)
				{
					if (stations.Length > 0)
						stations += ", ";
					stations += "(" + allStations.ElementAt(i).getId().ToString() + "," + allStations.ElementAt(i).getName().ToString() + ")";
				}
				textBoxAvailableStations.Text = (stations.Length > 0 ? stations : "No Stations");

				// get available platforms
				netspire.PlatformInfoList allPlatforms = gAudioServer.getPassengerInformationServer().getPlatforms();
				string platforms = "";
				for (int i = 0; i < allPlatforms.Count; i++)
				{
					netspire.PlatformInfo thisPlatform = allPlatforms.ElementAt(i);
					if (platforms.Length > 0)
						platforms += ", ";
					platforms += "(" + thisPlatform.getId().ToString() + "," + thisPlatform.getStationId().ToString() + "," + thisPlatform.getName().ToString() + ")";
				}
				textBoxAvailablePlatforms.Text = (platforms.Length > 0 ? platforms : "No Platforms");

				// get available services
				netspire.ServiceList allServices = gAudioServer.getPassengerInformationServer().getServices();

				// get available service stops
				netspire.ServiceStopList allServiceStops = gAudioServer.getPassengerInformationServer().getServiceStops();
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "timerSignallingEvents_Tick(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void checkBoxWarathaEnableDisableCommercialRadio_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWarathaEnableDisableCommercialRadio_CheckedChanged(): Not connected to AS");
					return;
				}

				// find device TCX (Active)
				netspire.DeviceStateArray devStateArray = gAudioServer.getDeviceStates();
				for (short i = 0; i < devStateArray.Count; i++)
				{
					// apply filtering rules
					if (devStateArray.ElementAt(i).getDeviceClass().ToString().Equals("COMMUNICATIONS_EXCHANGE"))// && devStateArray.ElementAt(i).getStateText().ToString().Equals("ACTIVE")))
					{
						devStateArray.ElementAt(i).setCondition("CommercialRadioEnabled", checkBoxWarathaEnableDisableCommercialRadio.Checked);
						// update GUI
						checkBoxWarathaEnableDisableCommercialRadio.BackColor = (checkBoxWarathaEnableDisableCommercialRadio.Checked ? System.Drawing.Color.Red : System.Drawing.Color.Green);
						checkBoxWarathaEnableDisableCommercialRadio.Text = (checkBoxWarathaEnableDisableCommercialRadio.Checked ? "Disable Commercial Radio" : "Enable Commercial Radio");
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWarathaEnableDisableCommercialRadio_CheckedChanged(): request sent to " + (checkBoxWarathaEnableDisableCommercialRadio.Checked ? "ENABLE" : "DISABLE") + " commercial radio");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWarathaEnableDisableCommercialRadio_CheckedChanged(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonWaratahInterruptCommercialRadio_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahInterruptCommercialRadio_Click(): Not connected to AS");
					return;
				}

				// find device TCX (Active)
				netspire.DeviceStateArray devStateArray = gAudioServer.getDeviceStates();
				for (short i = 0; i < devStateArray.Count; i++)
				{
					// apply filtering rules
					if (devStateArray.ElementAt(i).getDeviceClass().ToString().Equals("COMMUNICATIONS_EXCHANGE"))// && devStateArray.ElementAt(i).getStateText().ToString().Equals("ACTIVE")))
					{
						devStateArray.ElementAt(i).setCondition("CommercialRadioSoftInterrupt", true);
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahInterruptCommercialRadio_Click(): request sent to activate commercial radio interrupt");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahInterruptCommercialRadio_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonWaratahCancelInterruptionToCommercialRadio_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCancelInterruptionToCommercialRadio_Click(): Not connected to AS");
					return;
				}

				// find device TCX (Active)
				netspire.DeviceStateArray devStateArray = gAudioServer.getDeviceStates();
				for (short i = 0; i < devStateArray.Count; i++)
				{
					// apply filtering rules
					if (devStateArray.ElementAt(i).getDeviceClass().ToString().Equals("COMMUNICATIONS_EXCHANGE"))// && devStateArray.ElementAt(i).getStateText().ToString().Equals("ACTIVE")))
					{
						devStateArray.ElementAt(i).setCondition("CommercialRadioSoftInterruptCancel", true);
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCancelInterruptionToCommercialRadio_Click(): request sent to cancel the commercial radio interrupt");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCancelInterruptionToCommercialRadio_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonWaratahSetCommercialRadioInterrupt_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahSetCommercialRadioInterrupt_Click(): Not connected to AS");
					return;
				}

				// update dynamic configuration
				netspire.KeyValueMap userDefinedDynamicConfig = new netspire.KeyValueMap();
				userDefinedDynamicConfig.Add("Commercial_Radio_Interrupt_Ms", textBoxWaratahCommercialRadioInterrupt.Text);
				gAudioServer.setDynamicConfiguration(userDefinedDynamicConfig);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahSetCommercialRadioInterrupt_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void textBoxWaratahCommercialRadioInterrupt_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}

			// default to 1999999
			String validity = textBoxWaratahCommercialRadioInterrupt.Text.TrimStart(new Char[] { '0' });
			textBoxWaratahCommercialRadioInterrupt.Text = validity;
			if (textBoxWaratahCommercialRadioInterrupt.Text.Length == 0 || (System.Convert.ToDouble(textBoxWaratahCommercialRadioInterrupt.Text) > 36000000))
			{
				textBoxWaratahCommercialRadioInterrupt.Text = "10000";
				e.Handled = true;
			}
		}

		/*******************************************************************************************************/
		private void checkBoxWaratahEXTPASide1_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWaratahEXTPASide1_CheckedChanged(): Not connected to AS");
					return;
				}

				// find device TCX (Active)
				netspire.DeviceStateArray devStateArray = gAudioServer.getDeviceStates();
				for (short i = 0; i < devStateArray.Count; i++)
				{
					// apply filtering rules
					if (devStateArray.ElementAt(i).getDeviceClass().ToString().Equals("COMMUNICATIONS_EXCHANGE"))// && devStateArray.ElementAt(i).getStateText().ToString().Equals("ACTIVE")))
					{
						devStateArray.ElementAt(i).setCondition("DoorOpenSide1", checkBoxWaratahEXTPASide1.Checked);
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWaratahEXTPASide1_CheckedChanged(): request sent to " + (checkBoxWaratahEXTPASide1.Checked ? "OPEN" : "CLOSE") + " Door Side 1");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWaratahEXTPASide1_CheckedChanged(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void checkBoxWaratahEXTPASide2_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWaratahEXTPASide2_CheckedChanged(): Not connected to AS");
					return;
				}

				// find device TCX (Active)
				netspire.DeviceStateArray devStateArray = gAudioServer.getDeviceStates();
				for (short i = 0; i < devStateArray.Count; i++)
				{
					// apply filtering rules
					if (devStateArray.ElementAt(i).getDeviceClass().ToString().Equals("COMMUNICATIONS_EXCHANGE"))// && devStateArray.ElementAt(i).getStateText().ToString().Equals("ACTIVE")))
					{
						devStateArray.ElementAt(i).setCondition("DoorOpenSide2", checkBoxWaratahEXTPASide2.Checked);
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWaratahEXTPASide2_CheckedChanged(): request sent to " + (checkBoxWaratahEXTPASide2.Checked ? "OPEN" : "CLOSE") + " Door Side 2");
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "checkBoxWaratahEXTPASide2_CheckedChanged(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonWaratahCSPKR1Monitoring_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCSPKR1Monitoring_Click(): Not connected to AS");
					return;
				}

				// PaSinkId for CSPKR1 is 200108101
				// find PaSink and enable/disable monitoring
				int cspkr1PaSinkId = 200108101;
				netspire.PaSinkArray allPaSinks = gAudioServer.getAudioSinks();
				for (short i = 0; i < allPaSinks.Count; i++)
				{
					if (allPaSinks.ElementAt(i).id == cspkr1PaSinkId)
					{
						bool monitoringIsON = (buttonWaratahCSPKR1Monitoring.Text.ToString().IndexOf("ON") != -1);
						allPaSinks.ElementAt(i).setPAMonitoring(!monitoringIsON);
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCSPKR1Monitoring_Click(): request sent to turn monitoring to " + (monitoringIsON ? "OFF" : "ON"));
						break;
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCSPKR1Monitoring_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonWaratahCSPKR8Monitoring_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCSPKR8Monitoring_Click(): Not connected to AS");
					return;
				}

				// PaSinkId for CSPKR8 is 200808101
				// find PaSink and enable/disable monitoring
				int cspkr1PaSinkId = 200808101;
				netspire.PaSinkArray allPaSinks = gAudioServer.getAudioSinks();
				for (short i = 0; i < allPaSinks.Count; i++)
				{
					if (allPaSinks.ElementAt(i).id == cspkr1PaSinkId)
					{
						bool monitoringIsON = (buttonWaratahCSPKR8Monitoring.Text.ToString().IndexOf("ON") != -1);
						allPaSinks.ElementAt(i).setPAMonitoring(!monitoringIsON);
						addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCSPKR8Monitoring_Click(): request sent to turn monitoring to " + (monitoringIsON ? "OFF" : "ON"));
						break;
					}
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonWaratahCSPKR8Monitoring_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonGetVocalizerList_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetVocalizerList_Click(): Not connected to AS");
					return;
				}

				// get full list of TTS languages from backend
				netspire.TTSVocalizerArray vocalizerList = gAudioServer.getPAController().getTTSVocalizerList();
				if (vocalizerList.Count > 0)
				{
					for (int i = 0; i < vocalizerList.Count; i++)
					{
						netspire.TTSVocalizer thisVocalizerItem = vocalizerList.ElementAt(i);
						comboBoxTTSVocalizerList.Items.Add(thisVocalizerItem.language + "^" + thisVocalizerItem.voice + "^" + thisVocalizerItem.rate);
					}
					comboBoxTTSVocalizerList.SelectedIndex = 0;
				}
				else
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetVocalizerList_Click(): found 0 TTS languages in the system");
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonGetVocalizerList_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonAnnouncementDisplayChangeTemplate_Click(object sender, EventArgs e)
		{
			try
			{
				// sanity checks
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayTTS_Click(): Not connected to AS");
					return;
				}

				string templateName = textBoxAnnouncementDisplayTemplateName.Text.Trim();
				string key = textBoxAnnouncementDisplayTemplateKey.Text.Trim();
				string value1 = textBoxAnnouncementDisplayTemplateValue1.Text.Trim();
				string value2 = textBoxAnnouncementDisplayTemplateValue2.Text.Trim();
				string value3 = textBoxAnnouncementDisplayTemplateValue3.Text.Trim();
				if (templateName.Length == 0 && key.Length == 0 && value1.Length == 0)
				{
					MessageBox.Show("Nothing to do");
					return;
				}

				if ((key.Length > 0 && value1.Length == 0) || (key.Length == 0 && value1.Length > 0))
				{
					MessageBox.Show("Key Value pair is not complete");
					return;
				}

				// generate list of selected paZoneIds
				netspire.StringArray targetPaZoneList = new netspire.StringArray();
				foreach (DataGridViewRow row in dataGridViewDVAPaZoneIds.SelectedRows)
				{
					targetPaZoneList.Add(row.Cells[0].Value.ToString());
				}
				if (targetPaZoneList.Count <= 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayTTS_Click(): no zones selected");
					return;
				}

				// request change template
				netspire.DisplayVariableValueMap displayVariableList = new netspire.DisplayVariableValueMap();
				netspire.DisplayValueList valueList = new netspire.DisplayValueList();
				if (key.Length > 0 && value1.Length > 0)
				{
					netspire.DISPLAY_VALUE_INFO newValue = new netspire.DISPLAY_VALUE_INFO(value1, 3);
					valueList.Add(newValue);
				}
				if (key.Length > 0 && value2.Length > 0)
				{
					netspire.DISPLAY_VALUE_INFO newValue = new netspire.DISPLAY_VALUE_INFO(value2, 3);
					valueList.Add(newValue);
				}
				if (key.Length > 0 && value3.Length > 0)
				{
					netspire.DISPLAY_VALUE_INFO newValue = new netspire.DISPLAY_VALUE_INFO(value3, 3);
					valueList.Add(newValue);
				}
				if (valueList.Count > 0)
				{
					displayVariableList.Add(key, valueList);
				}

				netspire.Message newMessage = new netspire.Message();
				netspire.MessagePriority msgPriority = new netspire.MessagePriority();
				msgPriority.setPriorityMode(radioButtonSourcePriority.Checked ? netspire.AnnouncementPriorityMode.APM_RELATIVE_PRIORITY : netspire.AnnouncementPriorityMode.APM_ABSOLUTE_PRIORITY);
				msgPriority.setPrioritylevel(radioButtonSourcePriority.Checked ? 500 : trackBarCustomPriority.Value);
				newMessage.setPriority(msgPriority);
				newMessage.setVisualMessageType(netspire.Message.Type.DISPLAY_TEMPLATE);
				int validity = 0;   // until overridden
				newMessage.setVisualMessage(templateName, displayVariableList, validity);
				string requestId = gAudioServer.getPAController().playMessage(targetPaZoneList, newMessage);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonAnnouncementDisplayChangeTemplate_Click(): command sent to change template. requestId: " + requestId);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonAnnouncementDisplayChangeTemplate_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonPlayTTS_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayTTS_Click(): Not connected to AS");
					return;
				}

				// generate list of selected paZoneIds
				netspire.StringArray targetPaZoneList = new netspire.StringArray();
				foreach (DataGridViewRow row in dataGridViewDVAPaZoneIds.SelectedRows)
				{
					targetPaZoneList.Add(row.Cells[0].Value.ToString());
				}
				if (targetPaZoneList.Count <= 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayTTS_Click(): no zones selected");
					return;
				}

				// prepare TTS Text
				String ttsText = textBoxTTSText.Text.Trim();
				if (ttsText.Length <= 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayTTS_Click(): Please enter TTS text");
					return;
				}

				// extract language, voice, rate
				string[] selectedTTSVocalizerTokens = comboBoxTTSVocalizerList.Text.Split('^');
				String ttsLanguage = selectedTTSVocalizerTokens[0];
				String ttsVoice = selectedTTSVocalizerTokens[1];

				// request TTS playback
				netspire.Message newMessage = new netspire.Message();
				netspire.MessagePriority msgPriority = new netspire.MessagePriority();
				msgPriority.setPriorityMode(radioButtonSourcePriority.Checked ? netspire.AnnouncementPriorityMode.APM_RELATIVE_PRIORITY : netspire.AnnouncementPriorityMode.APM_ABSOLUTE_PRIORITY);
				msgPriority.setPrioritylevel(radioButtonSourcePriority.Checked ? 500 : trackBarCustomPriority.Value);
				newMessage.setPriority(msgPriority);
				newMessage.setPreChime(99200);
				newMessage.setAudioMessageType(netspire.Message.Type.TTS);
				//string ttsTextEncoded = Encoding.GetEncoding("iso-8859-1").GetString(Encoding.UTF8.GetBytes(ttsText));
				//string ttsTextEncoded = Encoding.GetEncoding(1252).GetString(Encoding.UTF8.GetBytes(ttsText));
				//newMessage.setAudioMessage(ttsTextEncoded.toString(), ttsLanguage, ttsVoice, netspire.Message.TextEncoding.UTF_8);
				newMessage.setAudioMessage(ttsText.ToString(), ttsLanguage, ttsVoice, netspire.Message.TextEncoding.UTF_8);
				string requestId = gAudioServer.getPAController().playMessage(targetPaZoneList, newMessage);
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayTTS_Click(): command sent to play TTS. requestId: " + requestId);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPlayTTS_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonPreviewTTS_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPreviewTTS_Click(): Not connected to AS");
					return;
				}

				// prepare TTS Text
				String ttsText = textBoxTTSText.Text.Trim();
				if (ttsText.Length <= 0)
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPreviewTTS_Click(): Please enter TTS text");
					return;
				}

				// extract language, voice, rate
				string[] selectedTTSVocalizerTokens = comboBoxTTSVocalizerList.Text.Split('^');
				String ttsLanguage = selectedTTSVocalizerTokens[0];
				String ttsVoice = selectedTTSVocalizerTokens[1];

				// request TTS preview
				netspire.Message newMessage = new netspire.Message();
				newMessage.setAudioMessageType(netspire.Message.Type.TTS);
				//string ttsTextEncoded = Encoding.GetEncoding("iso-8859-1").GetString(Encoding.UTF8.GetBytes(ttsText));
				//string ttsTextEncoded = Encoding.GetEncoding(1252).GetString(Encoding.UTF8.GetBytes(ttsText));
				//newMessage.setAudioMessage(ttsTextEncoded.toString(), ttsLanguage, ttsVoice, netspire.Message.TextEncoding.UTF_8);
				newMessage.setAudioMessage(ttsText.ToString(), ttsLanguage, ttsVoice, netspire.Message.TextEncoding.UTF_8);
				string requestId = newMessage.retrieveAudioMessage();
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPreviewTTS_Click(): command sent to preview Canadian French text. requestId: " + requestId);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "buttonPreviewTTS_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void buttonClearTTS_Click(object sender, EventArgs e)
		{
			textBoxTTSText.Text = "";
		}

		/*******************************************************************************************************/
		private void listBoxTTSAudioMessageList_KeyUp(object sender, KeyEventArgs e)
		{
			if (sender != listBoxTTSAudioMessageList) return;

			if (e.Control && e.KeyCode == Keys.C)
			{
				// Copy Selected Values To Clipboard
				if (listBoxTTSAudioMessageList.SelectedItem.ToString().Length > 0)
				{
					Clipboard.SetText(listBoxTTSAudioMessageList.SelectedItem.ToString());
				}
			}
		}

		/*******************************************************************************************************/
		private void alarmManagementCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					alarmManagementCheckBox.Checked = false;
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "alarmManagementCheckBox_CheckedChanged(): Not connected to AS");
					return;
				}

				// enable/disable alarm management
				if (alarmManagementCheckBox.Checked)
				{
					gAudioServer.enableAlarmManagement();
					alarmManagementCheckBox.Text = "Disable Alarm Management";
				}
				else
				{
					gAudioServer.disableAlarmManagement();
					alarmManagementCheckBox.Text = "Enable Alarm Management";
				}

				// clear alarms listview and adjust column width
				alarmsListView.Items.Clear();
				alarmsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "alarmManagementCheckBox_CheckedChanged(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void radioButtonCustomPriority_CheckedChanged(object sender, EventArgs e)
		{
			trackBarCustomPriority.Visible = radioButtonCustomPriority.Checked;
			labelCustomPriority.Visible = radioButtonCustomPriority.Checked;
		}

		/*******************************************************************************************************/
		private void button5_Click(object sender, EventArgs e)
		{
			MessageBox.Show("NOT USED");
			return;
			/*
			try
			{
				// Specify a name for your top-level folder.
				string folderNameToDelete = @"c:\ottawa";
				System.IO.Directory.Delete(folderNameToDelete, true);
			}
			catch (Exception exp)
			{
			}

			try
			{
				String folderNameCreate = @"c:\ottawa\dictionary\media";
				System.IO.Directory.CreateDirectory(folderNameCreate);

				//string dummyFileName = @"c:\ottawa\dictionary\media\dummy.wav";
				//System.IO.File.Create(dummyFileName);

				string xmlFileName = @"c:\ottawa\dictionary\changeset.xml";
				// Check that the file doesn't already exist. If it doesn't exist, create the file.
				// DANGER: System.IO.File.Create will overwrite the file if it already exists.
				if (!System.IO.File.Exists(xmlFileName))
				{
					//System.IO.File.Create(xmlFileName);
					using (System.IO.StreamWriter file = new System.IO.StreamWriter(xmlFileName, true))
					{
						// xml start
						file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
						file.WriteLine("<dictChangeSet xmlns=\"http://www.oa.com.au/asapi\">");
						// Reset operation
						file.WriteLine("\t<change>");
						file.WriteLine("\t\t<versionSeq>1</versionSeq>");
						file.WriteLine("\t\t<operation>ClearDictionary</operation>");
						file.WriteLine("\t\t<newVersion>1</newVersion>");
						file.WriteLine("\t</change>");
						// Add operation
						String key = "\\\"*^%userMessage^text\\\"";
						//String value = "\\\"";
						String value = "";
						for (int i = 1; i <= 50; i++)
						{
							if (i != 1)
							{
								value += ";";
							}
							value += "1234567_" + i.ToString("00");
						}
						//value += "\\\"";
						//String displayTextStr = "\"" + key + "=" + value + "\"";
						for (int itemNo = 0, versionSeq = 2; itemNo < 500; itemNo++, versionSeq++)
						{
							file.WriteLine("\t<change>");
							file.WriteLine("\t\t<versionSeq>" + versionSeq + "</versionSeq>");
							file.WriteLine("\t\t<operation>UpdateDictionaryItem</operation>");
							file.WriteLine("\t\t<dictItemNo>" + (50000 + itemNo) + "</dictItemNo>");
							file.WriteLine("\t\t<dictItem>");
							file.WriteLine("\t\t\t<deviceType>LED Display</deviceType>");
							file.WriteLine("\t\t\t<description>Display Text " + itemNo + "</description>");
							file.WriteLine("\t\t\t<category>Display</category>");
							String updatedValue = "\\\"" + ("Display Text " + itemNo) + "->" + value + "\\\"";
							String displayTextStr = "\"" + key + "=" + updatedValue + "\"";
							file.WriteLine("\t\t\t<displayText>" + displayTextStr + "</displayText>");
							//< displayText > "\"*^destination1\"=\"Tunney's Pasture\"" ^ "\"*^destination2\"=\"Tunney's Pasture\"" ^ "\"*^destination3\"=\"Tunney's Pasture\"" ^ "\"*^due1\"=\"10 min\"" ^ "\"*^due2\"=\"16 min\"" ^ "\"*^due3\"=\"20 min\"" ^ "\"{SCREEN}\"=\"Departures (next 3)\"" </ displayText >
							file.WriteLine("\t\t\t<metadata>defaultDate</metadata>");
							file.WriteLine("\t\t</dictItem>");
							file.WriteLine("\t</change>");
						}
						// xml end
						file.WriteLine("</dictChangeSet>");
					}
				}
			}
			catch (Exception exp)
			{
			}
			//try
			//{
			//	string folderToZip = @"c:\ottawa\dictionary";

			//	//provide the path and name for the zip file to create
			//	string zipFile = @"c:\ottawa\dictionary\dictionary_1000items.zip";

			//	//call the ZipFile.CreateFromDirectory() method
			//	System.IO.Compression.ZipFile.CreateFromDirectory(folderToZip, zipFile);
			//}
			//catch (Exception exp)
			//{
			//}
			*/
		}

		/*******************************************************************************************************/
		private void getAlarmsButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "getAlarmsButton_Click(): Not connected to AS");
					return;
				}

				// check if alarm management is enabled
				if (!alarmManagementCheckBox.Checked)
				{
					MessageBox.Show("Alarm Management is DISABLED", "Alarm Management");
					return;
				}

				// get alarms from backed
				alarmsListView.Items.Clear();
				netspire.AlarmArray alarmListFromAS = gAudioServer.getAlarms();
				if (alarmListFromAS.Count == 0)
				{
					MessageBox.Show("No alarms found in the system", "Alarm Management");
				}
				else
				{
					// populate listview with all alarms from backend
					for (int i = 0; i < alarmListFromAS.Count; i++)
					{
						netspire.Alarm thisAlarm = alarmListFromAS.ElementAt(i);
						string[] newAlarmItems = new string[]
						{
							thisAlarm.almNo.ToString(),
							thisAlarm.almDstName.ToString(),
							thisAlarm.almFirstUpdateTime.ToString(),
							thisAlarm.almLastUpdateTime.ToString(),
							thisAlarm.almRepeatCount.ToString(),
							thisAlarm.almCurrentState.ToString(),
							thisAlarm.almLevel.ToString(),
							thisAlarm.almErrorNo.ToString(),
							thisAlarm.almErrorText.ToString(),
							thisAlarm.isAlmAcknowledged.ToString(),
							thisAlarm.almAckTimeStamp.ToString(),
							thisAlarm.almAckUserEndPoint.ToString(),
							thisAlarm.almAckUser.ToString(),
							thisAlarm.almAckDescription.ToString()
						};
						ListViewItem newItem = new ListViewItem(newAlarmItems);
						alarmsListView.Items.Add(newItem);
					}

					// auto adjust column width
					alarmsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
					alarmsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "getAlarmsButton_Click(): exception caught: " + excep.Message);
			}
		}

		/*******************************************************************************************************/
		private void alarmAcknowledgeButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (gAudioServer == null || !gAudioServer.isAudioConnected())
				{
					addNewDebugMessage(EventType.DEBUG_MESSAGE, "alarmAcknowledgeButton_Click(): Not connected to AS");
					return;
				}

				// check if alarm management is enabled
				if (!alarmManagementCheckBox.Checked)
				{
					MessageBox.Show("Alarm Management is DISABLED", "Alarm Management");
					return;
				}

				// error if no item selected.
				if (alarmsListView.SelectedItems.Count == 0)
				{
					MessageBox.Show("Please select an alarm from list to acknowledge", "Alarm Management");
					return;
				}

				// get selected alarm. if alarm is not already acknowledged then acknowledge it
				int selectedAlarm_number = System.Convert.ToInt32(alarmsListView.SelectedItems[0].SubItems[0].Text, 10);
				String selectedAlarm_acknowledged = alarmsListView.SelectedItems[0].SubItems[9].Text;
				if (selectedAlarm_acknowledged.Equals("True"))
				{
					MessageBox.Show("Selected alarm is already acknowledged", "Alarm Management");
				}
				else
				{
					// find selected alarm and send acknowledgement.
					bool foundAlarm = false;
					netspire.AlarmArray alarmListFromAS = gAudioServer.getAlarms();
					for (int i = 0; i < alarmListFromAS.Count; i++)
					{
						netspire.Alarm thisAlarm = alarmListFromAS.ElementAt(i);
						if (thisAlarm.almNo != selectedAlarm_number)
						{
							continue;
						}

						foundAlarm = true;
						String endPt = "";
						String operatorName = "admin";
						String ackDescription = "Alarm acknowledged by SDK operator";

						var host = Dns.GetHostEntry(Dns.GetHostName());
						foreach (var ip in host.AddressList)
						{
							if (ip.AddressFamily == AddressFamily.InterNetwork)
							{
								endPt = ip.ToString();
								break;
							}
						}
						thisAlarm.acknowledgeAlarm((endPt.Length > 0 ? endPt : "operator ip address"), operatorName, ackDescription);
						break;
					}

					// cleanup
					MessageBox.Show(
						(foundAlarm
							?
							("Alarm number >" + selectedAlarm_number + "< sent for acknowledgement. Please refresh alarm list")
							:
							("Failed to acknowledge alarm number >" + selectedAlarm_number + "<. Error finding alarm in alarmlist from AS")
						), "Alarm Management"
					);
				}
			}
			catch (System.Exception excep)
			{
				addNewDebugMessage(EventType.DEBUG_MESSAGE, "alarmAcknowledgeButton_Click(): exception caught: " + excep.Message);
			}
		}
	}

	/*******************************************************************************************************
	 *								AudioServer Observer
	 ******************************************************************************************************/
	public class asObserverClass : netspire.AudioServerObserver
	{
		/*******************************************************************************************************/
		public asObserverClass()
		{
		}

		/*******************************************************************************************************/
		public override void onCommsLinkUp()
		{
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, "asObserverClass.onCommsLinkUp()");
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onCommsLinkDown()
		{
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, "asObserverClass.onCommsLinkDown()");
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onAudioConnected()
		{
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, "asObserverClass.onAudioConnected()");
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onAudioDisconnected()
		{
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, "asObserverClass.onAudioDisconnected()");
					Form1.gMainForm.Invoke(Form1.gMainForm.audioDisconnectDelegate);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onDeviceStateChange(netspire.Device device)
		{
			netspire.Device tmpDevice = new netspire.Device(device);
			netspire.KeyValueMap tmpKeyValueMap = tmpDevice.getSupplementaryFields();
			String supplementaryFieldMsg = "";
			String msg = "asObserverClass.onDeviceStateChange() - " +
				"Name >" + tmpDevice.getName() + "< " +
				"State >" + tmpDevice.getState().ToString() + "< " +
				"Dict Status >" + tmpDevice.getDictionaryUpdateStatus() + "< " +
				"Dict VersionSeq >" + tmpDevice.getDictionaryVersion() + "< " +
				"Health Test Status >" + tmpDevice.getHealthTestStatus() + "< " +
				"Supplementary Fields Cnt >" + tmpKeyValueMap.Count + "<";
			for (int i = 0; i < tmpKeyValueMap.Count; i++)
			{
				//if (supplementaryFieldMsg.Length > 0)
				supplementaryFieldMsg = supplementaryFieldMsg +
					"asObserverClass.onDeviceStateChange() - " +
					"device >" + tmpDevice.getName() + "< supplementary key >" + tmpKeyValueMap.ElementAt(i).Key + "< value >" + tmpKeyValueMap.ElementAt(i).Value + "<";
			}
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEVICE_STATE_EVENT, msg);
					if (supplementaryFieldMsg.Length > 0)
					{
						Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEVICE_STATE_EVENT, supplementaryFieldMsg);
					}
				}).Start();
#if (BUCHAREST_OA0784)
				new Thread(delegate()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.updateBucharestDelegate, tmpDevice);
				}).Start();
#endif
#if (WARATAH_OA0979)
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.updateWaratahDelegate, tmpDevice, null);
				}).Start();
#endif
			}
		}

		/*******************************************************************************************************/
		public override void onDeviceDelete(netspire.Device device)
		{
			netspire.Device tmpDevice = new netspire.Device(device);
			netspire.KeyValueMap tmpKeyValueMap = tmpDevice.getSupplementaryFields();
			String msg = "asObserverClass.onDeviceDelete() - Name >" + tmpDevice.getName() + "< ";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEVICE_STATE_EVENT, msg);
				}).Start();
#if (BUCHAREST_OA0784)
				new Thread(delegate()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.updateBucharestDelegate, tmpDevice);
				}).Start();
#endif
			}
		}

		/*******************************************************************************************************/
		public override void onVUMeterUpdate(int level)
		{
			String msg = "asObserverClass.onVUMeterUpdate() - " + level;
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onConfigUpdate(string deviceId, string resId, int instanceId, string progress, string key, string value)
		{
			String msg = "asObserverClass.onConfigUpdate() - " + deviceId + ", " + resId + ", " + instanceId + "," + progress + ", " + key + ", " + value;
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onStateUpdate(bool isImportData, int key, string dataMsg)
		{
			String msg = "asObserverClass.onStateUpdate() - " + isImportData + ", " + key + ", " + dataMsg;
			String msg1 = key + ", " + dataMsg;
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, msg);
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.STATE_EVENT, msg1);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onUserDataUpdate(int elemId, int key, string value)
		{
			String msg = "asObserverClass.onUserDataUpdate() - elemId: " + elemId + ", key: " + key + ", value: " + value;
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onUserDataDelete(int elemId, int key)
		{
			String msg = "asObserverClass.onUserDataDelete() - elemId: " + elemId + ", key: " + key;
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.DEBUG_MESSAGE, msg);
				}).Start();
			}
		}
	}

	/*******************************************************************************************************
	 *										PAControllerObserver
	 ******************************************************************************************************/
	public class paObserverClass : netspire.PAControllerObserver
	{
		/*******************************************************************************************************/
		public paObserverClass()
		{
		}

		/*******************************************************************************************************/
		public override void onPaSourceUpdate(netspire.PaSource source)
		{
			netspire.PaSource tmpPaSource = new netspire.PaSource(source);
			String msg = "paObserverClass.onPaSourceUpdate() - sourceId >" + tmpPaSource.id + "< sourceState >" + tmpPaSource.state.ToString() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaSourceDelete(int sourceId)
		{
			String msg = "paObserverClass.onPaSourceDelete() - sourceId >" + sourceId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaSinkUpdate(netspire.PaSink sink)
		{
			netspire.PaSink tmpPaSink = new netspire.PaSink(sink);
			String msg = "paObserverClass.onPaSinkUpdate() - sinkId >" + tmpPaSink.id + "< " +
					"sinkState >" + tmpPaSink.state.ToString() + "< " +
					"sinkAnnouncementType >" + tmpPaSink.announcementType.ToString() + "< " +
					"sinkOutputGain >" + tmpPaSink.outputGain + "< " +
					"sinkMuteState >" + tmpPaSink.muteState + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
#if (WARATAH_OA0979)
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.updateWaratahDelegate, null, tmpPaSink);
				}).Start();
#endif
			}
		}

		/*******************************************************************************************************/
		public override void onPaSinkDelete(int sinkId)
		{
			String msg = "paObserverClass.onPaSinkDelete() - sinkId >" + sinkId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaTriggerUpdate(netspire.PaTrigger trigger)
		{
			netspire.PaTrigger tmpPaTrigger = new netspire.PaTrigger(trigger);
			String msg = "paObserverClass.onPaTriggerUpdate() - triggerId >" + tmpPaTrigger.id + "< triggerState >" + tmpPaTrigger.state.ToString() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaTriggerDelete(int triggerId)
		{
			String msg = "paObserverClass.onPaTriggerDelete() - triggerId >" + triggerId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaSelectorUpdate(netspire.PaSelector selector)
		{
			netspire.PaSelector tmpPaSelector = new netspire.PaSelector(selector);
			String msg = "paObserverClass.onPaSelectorUpdate() - selectorId >" + tmpPaSelector.id + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaSelectorDelete(int selectorId)
		{
			String msg = "paObserverClass.onPaSelectorDelete() - selectorId >" + selectorId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaZoneUpdate(netspire.PaZone paZone)
		{
			netspire.PaZone tmpPaZone = new netspire.PaZone(paZone);
			String msg = "paObserverClass.onPaZoneUpdate() - zoneId >" + tmpPaZone.id + "< " +
					"zoneActivity >" + tmpPaZone.activityText.ToString() + "< " +
					"zoneAnnouncementType >" + tmpPaZone.announcementTypeText.ToString() + "< ";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onPaZoneDelete(String zoneId)
		{
			String msg = "paObserverClass.onPaZoneDelete() - zoneId >" + zoneId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.PA_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onAudioMessageRetrievalComplete(String uuid, bool success, netspire.PAControllerObserver.MessageRetrievalError errCode, String uri)
		{
			String msg = "UUID: " + uuid + " | URI: " + uri;
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.AUDIO_MESSGAE_RETRIVAL_EVENT, msg);
				}).Start();
			}
		}
	}

	/*******************************************************************************************************
	 *									CallControllerObserver
	 ******************************************************************************************************/
	public class callObserverClass : netspire.CallControllerObserver
	{
		/*******************************************************************************************************/
		public callObserverClass()
		{
		}

		/*******************************************************************************************************/
		public override void onCallUpdate(netspire.CallInfo callInfo)
		{
			netspire.CallInfo tmpCallInfo = new netspire.CallInfo(callInfo);
			String msg = "callObserverClass.onCallUpdate() - " +
					"callId >" + tmpCallInfo.callId + "< callStartTime >" + tmpCallInfo.callStartTime + "< " +
					"callDuration >" + tmpCallInfo.callDuration + "< callAnswerTime >" + tmpCallInfo.callAnswerTime + "< " +
					"callAPartyId >" + tmpCallInfo.callAPartyId + "< callBPartyId >" + tmpCallInfo.callBPartyId + "< callTargetIds >" + tmpCallInfo.callTargetIds + "< " +
					"callState >" + tmpCallInfo.callState + "< callHangupCause >" + tmpCallInfo.callReleaseCause + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.CALL_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onCallDelete(int callId)
		{
			String msg = "callObserverClass.onCallDelete() - callId >" + callId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.CALL_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onCDRMessageUpdate(netspire.CDRInfo cdrInfo)
		{
			netspire.CDRInfo tmpCDRInfo = new netspire.CDRInfo(cdrInfo);
			String msg = "callObserverClass.onCDRMessageUpdate() - cdrId >" + tmpCDRInfo.id + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.CDR_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onCDRMessageDelete(int cdrId)
		{
			String msg = "callObserverClass.onCDRMessageDelete() - cdrId >" + cdrId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.CDR_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onTerminalUpdate(netspire.TerminalInfo termInfo)
		{
			netspire.TerminalInfo tmpTerminalInfo = new netspire.TerminalInfo(termInfo);
			String msg = "callObserverClass.onTerminalUpdate() - " +
					"termId >" + tmpTerminalInfo.termId + "< termAddress >" + tmpTerminalInfo.termAddress + "< " +
					"termLocation > " + tmpTerminalInfo.termLocation + "< termState >" + tmpTerminalInfo.termState + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.TERMINAL_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onTerminalDelete(int termId)
		{
			String msg = "callObserverClass.onTerminalDelete() - termId >" + termId + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.TERMINAL_EVENT, msg);
				}).Start();
			}
		}
	}

	/*******************************************************************************************************
	 *										PassengerInformationServer
	 ******************************************************************************************************/
	public class passengerObserverClass : netspire.PassengerInformationObserver
	{
		/*******************************************************************************************************/
		public passengerObserverClass()
		{
		}

		/*******************************************************************************************************/
		public override void onServerStatusUpdated(bool status)
		{
			String msg = "passengerObserverClass.onServerStatusUpdated(): serverStatus >" + status + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		public override void onLineUpdated(netspire.NamedId line)
		{
			String msg = "passengerObserverClass.onLineUpdated(): lineId >" + line.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a line is removed from the system
		public override void onLineRemoved(netspire.NamedId line)
		{
			String msg = "passengerObserverClass.onLineRemoved(): lineId >" + line.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a new vehicle defined in the system
		public override void onVehicleUpdated(netspire.Vehicle vehicle)
		{
			String msg = "passengerObserverClass.onVehicleUpdated(): vehicleId >" + vehicle.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a vehicle is removed from the system
		public override void onVehicleRemoved(netspire.Vehicle vehicle)
		{
			String msg = "passengerObserverClass.onVehicleRemoved(): vehicleId >" + vehicle.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a new station defined in the system
		public override void onStationUpdated(netspire.Station station)
		{
			String msg = "passengerObserverClass.onStationUpdated(): stationId >" + station.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a station dis removed from the system
		public override void onStationRemoved(netspire.Station station)
		{
			String msg = "passengerObserverClass.onStationRemoved(): stationId >" + station.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a new platform defined in the system
		public override void onPlatformInfoUpdated(netspire.PlatformInfo platformInfo)
		{
			String msg = "passengerObserverClass.onPlatformInfoUpdated(): platformId >" + platformInfo.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a platform is removed from the system
		public override void onPlatformInfoRemoved(netspire.PlatformInfo platformInfo)
		{
			String msg = "passengerObserverClass.onPlatformInfoRemoved(): platformId >" + platformInfo.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a new service defined in the system
		public override void onServiceUpdated(netspire.Service service)
		{
			String msg = "passengerObserverClass.onServiceUpdated(): serviceId >" + service.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}

		/*******************************************************************************************************/
		// This function is called when a service is removed from the system
		public override void onServiceRemoved(netspire.Service service)
		{
			String msg = "passengerObserverClass.onServiceRemoved(): serviceId >" + service.getId() + "<";
			if (Form1.gMainForm != null)
			{
				new Thread(delegate ()
				{
					Form1.gMainForm.Invoke(Form1.gMainForm.addDeubgMessageDelegate, Form1.EventType.SIGNALLING_EVENT, msg);
				}).Start();
			}
		}
	}
}
