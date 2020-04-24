using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Len.Jakpro.AddInSampleLibrary.Subscription;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace Len.Jakpro.AddinsPHPService
{
    /// <summary>
    /// OA Service for automatically show PHP screen.
    /// </summary>
    [AddInExtension(
        "AddIn OpenAccess PHP Automatic Run",
        "OA Service Auto Run",
        DefaultStartMode = DefaultStartupModes.Auto)]

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
                "OA.PHP.CCTV.Function"
            };
            _Variables.Start(context, _ZenonVariableList);
        }

        public void Stop()
        {
            // enter your code which should be executed when stopping the SCADA Runtime Service
        }

        private void StatusVariableChanged(IEnumerable<IVariable> obj)
        {
            try
            {
                string UserVariables = _context.VariableCollection["[UserAdministration]Userfullname"].GetValue(0).ToString();
                string isiContent = _context.VariableCollection["OA.PHP.CCTV.Function"].GetValue(0).ToString();

                if (UserVariables.Equals("admin") || UserVariables.Equals("Maintenence") || UserVariables.Equals("Facilities"))
                {
                    _context.FunctionCollection["MenuPHP"].Execute();
                    if (isiContent.Contains("VLD1"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenVelodrome"].Execute();
                        _context.FunctionCollection["cctv.php.vld.1"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("VLD2"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenVelodrome"].Execute();
                        _context.FunctionCollection["cctv.php.vld.2"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("VLD3"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenVelodrome"].Execute();
                        _context.FunctionCollection["cctv.php.vld.3"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("VLD4"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenVelodrome"].Execute();
                        _context.FunctionCollection["cctv.php.vld.4"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("VLD5"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenVelodrome"].Execute();
                        _context.FunctionCollection["cctv.php.vld.5"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("VLD6"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenVelodrome"].Execute();
                        _context.FunctionCollection["cctv.php.vld.6"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    //kgm
                    else if (isiContent.Contains("KGM1"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOU"].Execute();
                        _context.FunctionCollection["cctv.php.kgm.1"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGM2"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOU"].Execute();
                        _context.FunctionCollection["cctv.php.kgm.2"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGM3"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOU"].Execute();
                        _context.FunctionCollection["cctv.php.kgm.3"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGM4"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOU"].Execute();
                        _context.FunctionCollection["cctv.php.kgm.4"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGM5"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOU"].Execute();
                        _context.FunctionCollection["cctv.php.kgm.5"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGM6"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOU"].Execute();
                        _context.FunctionCollection["cctv.php.kgm.6"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }

                    //KGB
                    else if (isiContent.Contains("KGB1"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOS"].Execute();
                        _context.FunctionCollection["cctv.php.kgb.1"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGB2"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOS"].Execute();
                        _context.FunctionCollection["cctv.php.kgb.2"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGB3"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOS"].Execute();
                        _context.FunctionCollection["cctv.php.kgb.3"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGB4"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOS"].Execute();
                        _context.FunctionCollection["cctv.php.kgb.4"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGB5"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOS"].Execute();
                        _context.FunctionCollection["cctv.php.kgb.5"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("KGB6"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenBOS"].Execute();
                        _context.FunctionCollection["cctv.php.kgb.6"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }

                    //PLM
                    else if (isiContent.Contains("PLM1"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenPulomas"].Execute();
                        _context.FunctionCollection["cctv.php.plm.1"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("PLM2"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenPulomas"].Execute();
                        _context.FunctionCollection["cctv.php.plm.2"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("PLM3"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenPulomas"].Execute();
                        _context.FunctionCollection["cctv.php.plm.3"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("PLM4"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenPulomas"].Execute();
                        _context.FunctionCollection["cctv.php.plm.4"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("PLM5"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenPulomas"].Execute();
                        _context.FunctionCollection["cctv.php.plm.5"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("PLM6"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenPulomas"].Execute();
                        _context.FunctionCollection["cctv.php.plm.6"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }

                    //EQU
                    else if (isiContent.Contains("EQU1"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenEquistrian"].Execute();
                        _context.FunctionCollection["cctv.php.pkd.1"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("EQU2"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenEquistrian"].Execute();
                        _context.FunctionCollection["cctv.php.pkd.2"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("EQU3"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenEquistrian"].Execute();
                        _context.FunctionCollection["cctv.php.pkd.3"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("EQU4"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenEquistrian"].Execute();
                        _context.FunctionCollection["cctv.php.pkd.4"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("EQU5"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenEquistrian"].Execute();
                        _context.FunctionCollection["cctv.php.pkd.5"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("EQU6"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenEquistrian"].Execute();
                        _context.FunctionCollection["cctv.php.pkd.6"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }

                    //Depo
                    else if (isiContent.Contains("DPT1"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenDepo"].Execute();
                        _context.FunctionCollection["cctv.php.dpt.1"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("DPT2"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenDepo"].Execute();
                        _context.FunctionCollection["cctv.php.dpt.2"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("DPT3"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenDepo"].Execute();
                        _context.FunctionCollection["cctv.php.dpt.3"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("DPT4"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenDepo"].Execute();
                        _context.FunctionCollection["cctv.php.dpt.4"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("DPT5"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenDepo"].Execute();
                        _context.FunctionCollection["cctv.php.dpt.5"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                    else if (isiContent.Contains("DPT6"))
                    {
                        _context.FunctionCollection["OA.PHP.ScreenDepo"].Execute();
                        _context.FunctionCollection["cctv.php.dpt.6"].Execute();
                        _context.VariableCollection["OA.PHP.CCTV.Function.LastPHP"].SetValue(0, isiContent);
                    }
                }
                _context.VariableCollection["OA.PHP.CCTV.Function"].SetValue(0, " ");
            }
            catch (Exception ex)
            {

            }
        }
        // Defining the Destructor 
        // for class ProjectServiceExtension 
        ~ProjectServiceExtension()
        {
         
        }
        #endregion
    }
}