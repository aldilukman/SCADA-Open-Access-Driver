using System;
using System.Diagnostics;
using System.Windows.Forms;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace Len.Jakpro.RestartDriver
{
    /// <summary>
    /// Function for auto restart driver.
    /// </summary>
    [AddInExtension(
        "Driver OA",
        "Restart Driver OA Manually from HMI")]
    public class ProjectWizardExtension : IProjectWizardExtension
    {
        #region IProjectWizardExtension implementation
        string DriverName = "Len.Jakpro.OpenAccessServer";
        string DirectoryExecutable = "C:\\OA DRIVER\\Len.Jakpro.OpenAccessServer.exe";
        public void Run(IProject context, IBehavior behavior)
        {
            // enter your code which should be executed on triggering the function "Execute Project Wizard Extension" in the SCADA Runtime
            try
            {
                Process[]
                proc = Process.GetProcessesByName(DriverName);
                if (proc.Length > 0)
                {
                    foreach (Process prs in proc)
                    {
                        if (prs.ProcessName == DriverName)
                        {
                            //Start Kill Process
                            prs.Kill();
                            //Start New Process
                            Process NewProcess = new Process();
                            NewProcess.StartInfo.FileName = DirectoryExecutable;
                            NewProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            NewProcess.Start();
                        }
                    }
                }
                else
                {
                    // start your process
                    Process NewProcess = new Process();
                    NewProcess.StartInfo.FileName = DirectoryExecutable;
                    NewProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    NewProcess.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        // Defining the Destructor 
        // for class ProjectServiceExtension 
        ~ProjectWizardExtension()
        {
            Console.WriteLine("The instance of" +
                       " ProjectWizardExtension class Destroyed");
        }
    }

}