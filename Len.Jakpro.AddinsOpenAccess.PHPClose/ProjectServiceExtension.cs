using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Len.Jakpro.AddInSampleLibrary.Subscription;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace Len.Jakpro.AddinsOpenAccess.PHPClose
{
    /// <summary>
    /// OA Service for automatically Close PHP screen after IPPA closed.
    /// </summary>
    [AddInExtension(
        "AddIn OpenAccess PHP Automatic Close",
        "OA Service PHP Auto Close",
        DefaultStartMode =DefaultStartupModes.Auto)]
    public class ProjectServiceExtension : IProjectServiceExtension
    {
        #region IProjectServiceExtension implementation
        private VariableSubscription _Variables;
        private IProject _context;
        public void Start(IProject context, IBehavior behavior)
        {
            this._context = context;
            _Variables = new VariableSubscription(StatusVariableChanged);
            // enter your code which should be executed when starting the SCADA Runtime Service
            List<string> _ZenonVariableList = new List<string>
            {
                // Variable To Initiate CCTV Function
                "OA.PHP.CCTV.Function.Idle"
            };
            _Variables.Start(context, _ZenonVariableList);
        }

        public void Stop()
        {
            // enter your code which should be executed when stopping the SCADA Runtime Service
        }

        private void StatusVariableChanged(IEnumerable<IVariable> obj)
        {
            string UserVariables = _context.VariableCollection["[UserAdministration]Userfullname"].GetValue(0).ToString();
            if (
                UserVariables.Equals("admin") || UserVariables.Equals("Maintenence") ||
                UserVariables.Equals("Facilities"))
            {
                string isiContent = _context.VariableCollection["OA.PHP.CCTV.Function.Idle"].GetValue(0).ToString();
                string lastContent = _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].GetValue(0).ToString();
                if (isiContent.Contains("VLD1") && lastContent.Contains("VLD1"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("VLD2") && lastContent.Contains("VLD2"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("VLD3") && lastContent.Contains("VLD3"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("VLD4") && lastContent.Contains("VLD4"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("VLD5") && lastContent.Contains("VLD5"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("VLD6") && lastContent.Contains("VLD6"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                //kgm
                else if (isiContent.Contains("KGM1") && lastContent.Contains("KGM1"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGM2") && lastContent.Contains("KGM2"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGM3") && lastContent.Contains("KGM3"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGM4") && lastContent.Contains("KGM4"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGM5") && lastContent.Contains("KGM5"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGM6") && lastContent.Contains("KGM6"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }

                //KGB
                else if (isiContent.Contains("KGB1") && lastContent.Contains("KGB1"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGB2") && lastContent.Contains("KGB2"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGB3") && lastContent.Contains("KGB3"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGB4") && lastContent.Contains("KGB4"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGB5") && lastContent.Contains("KGB5"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("KGB6") && lastContent.Contains("KGB6"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }

                //PLM
                else if (isiContent.Contains("PLM1") && lastContent.Contains("PLM1"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("PLM2") && lastContent.Contains("PLM2"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("PLM3") && lastContent.Contains("PLM3"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("PLM4") && lastContent.Contains("PLM4"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("PLM5") && lastContent.Contains("PLM5"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("PLM6") && lastContent.Contains("PLM6"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }

                //EQU
                else if (isiContent.Contains("EQU1") && lastContent.Contains("EQU1"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("EQU2") && lastContent.Contains("EQU2"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("EQU3") && lastContent.Contains("EQU3"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("EQU4") && lastContent.Contains("EQU4"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("EQU5") && lastContent.Contains("EQU5"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("EQU6") && lastContent.Contains("EQU6"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }

                //Depo
                else if (isiContent.Contains("DPT1") && lastContent.Contains("DPT1"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("DPT2") && lastContent.Contains("DPT2"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("DPT3") && lastContent.Contains("DPT3"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("DPT4") && lastContent.Contains("DPT4"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("DPT5") && lastContent.Contains("DPT5"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                else if (isiContent.Contains("DPT6") && lastContent.Contains("DPT6"))
                {
                    _context.FunctionCollection["cctv.php.close"].Execute();
                    _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, " ");
                }
                
            }
            _context.VariableCollection["OA.PHP.CCTV.Function.Idle"].SetValue(0, " ");
            
        }

        #endregion
        // Defining the Destructor 
        // for class ProjectServiceExtension 
        ~ProjectServiceExtension()
        {
          
        }
    }
    
}