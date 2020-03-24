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
    /// Interaction logic for UserControlEdit.xaml
    /// </summary>
    public partial class UserControlEdit : UserControl
    {

        public event EventHandler<CustomEventArgsEdit> TransferInfoEdit=null;
        public UserControlEdit()
        {
            InitializeComponent();
            var _logging = new NLogConfigurator();
            _logging.Configure();
        }
        public void SetClassObj(ClassObject Obj)
        {
            _Obj = Obj;            
        }
        private int _id;
        private Connection _ObjCon;
        private ClassObject _Obj;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
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
                    _ObjCon.SqlCmd.CommandText = "OA_EditValue";
                    _ObjCon.SqlCmd.Parameters.AddWithValue("@id", _id);
                    _ObjCon.SqlCmd.Parameters.AddWithValue("@value", textBox.Text);
                    _ObjCon.SqlCmd.ExecuteNonQuery();
                    MessageBox.Show("The Value Has been Edited", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    TransferInfoEdit(this, new CustomEventArgsEdit("Done Edit"));
                    Window.GetWindow(this).Close();                    
                }
                catch (SqlException ex)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
                }
                _ObjCon.Disconnected();
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                textBox.Text = _Obj.Name;
                _id = _Obj.Id;
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
        }
    }
    public class CustomEventArgsEdit : EventArgs
    {
        private string msg;
        public CustomEventArgsEdit(string _msg)
        {
            msg = _msg;
        }
        public string Message
        {
            get { return msg; }
        }
    }
}
