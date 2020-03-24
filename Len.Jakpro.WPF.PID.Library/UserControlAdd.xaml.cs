using Len.Jakpro.Database.Rendundancy;
using Len.Jakpro.Logging;
using NLog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Len.Jakpro.WPF.PID.Library
{
    /// <summary>
    /// Interaction logic for UserControlAdd.xaml
    /// </summary>
    public partial class UserControlAdd : UserControl
    {
        public event EventHandler<CustomEventArgsAdd> TransferInfoAdd=null;
        public UserControlAdd()
        {
            InitializeComponent();
            var _logging = new NLogConfigurator();
            _logging.Configure();
        }
        public void SetClassObj(ClassObject Obj)
        {
            _Obj = Obj;
        }
        private Connection _ObjCon;
        private ClassObject _Obj;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }        
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show("Insert new text, the textbox cannot null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var _result = MessageBox.Show("Are you sure ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (_result == MessageBoxResult.Yes)
                {
                    _ObjCon = new Connection();
                    _ObjCon.Connected();
                    _ObjCon.SqlCmd = new SqlCommand();
                    try
                    {
                        _ObjCon.SqlCmd.Connection = _ObjCon.SqlCon;
                        _ObjCon.SqlCmd.CommandType = CommandType.StoredProcedure;
                        _ObjCon.SqlCmd.CommandText = "OA_AddValue";
                        _ObjCon.SqlCmd.Parameters.AddWithValue("@value",textBox.Text);
                        _ObjCon.SqlCmd.ExecuteNonQuery();
                        MessageBox.Show("The Value Has been inserted", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        TransferInfoAdd(this, new CustomEventArgsAdd("Done Add"));
                        Window.GetWindow(this).Close();
                    }
                    catch (SqlException ex)
                    {
                        _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
                        MessageBox.Show(ex.Message);
                    }
                    _ObjCon.Disconnected();
                }
            }           
        }
    }
    public class CustomEventArgsAdd : EventArgs
    {
        private string msg;
        public CustomEventArgsAdd(string _msg)
        {
            msg = _msg;
        }
        public string Message
        {
            get { return msg; }
        }
    }
}
