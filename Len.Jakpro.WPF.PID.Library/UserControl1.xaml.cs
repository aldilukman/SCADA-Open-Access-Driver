using Len.Jakpro.Database.Rendundancy;
using Len.Jakpro.PID.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
namespace Len.Jakpro.WPF.PID.Library
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public int IntValue { get; set; }
        public UserControl1()
        {
            InitializeComponent();
            var _logging = new NLogConfigurator();
            _logging.Configure();
        }
        public string ObjectValue
        {
            get{return (string)GetValue(ValueADependencyPropertyValue);}
            set{SetValue(ValueADependencyPropertyValue, value);}
        }
        public static readonly DependencyProperty ValueADependencyPropertyValue = DependencyProperty.Register("ObjectValue", typeof(string),
        typeof(UserControl1), new FrameworkPropertyMetadata("Null", new PropertyChangedCallback(OnValueADependencyPropertyChanged)));
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private List<Communication> _DataComms = new List<Communication>();
        private Connection _ObjCon;
        private delegate void AddData(string text);
        // FrameworkPropertyMetadata(0.0) default
        private static void OnValueADependencyPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            UserControl1 control = source as UserControl1;
            if (control != null)
            {
                try
                {
                    control.ObjectValue = (string)e.NewValue;
                    control.GridPID.SelectedItem = control.ObjectValue.ToString();
                }
                catch (Exception ex)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
                }
            }
        }
        private void GridPID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (RbPre.IsChecked == true)
                {
                    Communication classObj = GridPID.SelectedItem as Communication;
                    ObjectValue = Convert.ToString(classObj.MediaAnnoucements);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
        }
        private void TxtObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {               
                if (RbCus.IsChecked == true)
                {
                    ObjectValue = TxtObject.Text.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
        }
        private void GridPID_Loaded(object sender, RoutedEventArgs e)
        {

            _ObjCon = new Connection();
            _ObjCon.Connected();
            _ObjCon.SqlCmd = new SqlCommand();
            try
            {
                _ObjCon.SqlCmd.Connection = _ObjCon.SqlCon;
                _ObjCon.SqlCmd.CommandType = CommandType.StoredProcedure;
                _ObjCon.SqlCmd.CommandText = "OA_PIDLibrary";
                _ObjCon.SqlRead = _ObjCon.SqlCmd.ExecuteReader();
                while (_ObjCon.SqlRead.Read())
                {
                    _DataComms.Add(new Communication()
                    {
                        Mediaid = Convert.ToInt16(_ObjCon.SqlRead.GetValue(0)),
                        MediaAnnoucements = _ObjCon.SqlRead.GetString(1)
                    });
                }
                GridPID.ItemsSource = _DataComms;              
                _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Grid PID loaded....");
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + ex.Message.ToString());
            }            
            _ObjCon.Disconnected();
            RbPre.IsChecked = true;
        }
        private void RbPre_Checked(object sender, RoutedEventArgs e)
        {
            ObjectValue = "";
            GridPID.IsEnabled = true;
            TxtObject.IsEnabled = false;
            GridPID.SelectedIndex = 0;
            TxtObject.Clear();
        }
        private void RbCus_Checked(object sender, RoutedEventArgs e)
        {
            ObjectValue = "";
            TxtObject.IsEnabled = true;
            GridPID.IsEnabled = false;
        }

        private void UserControlEdit_TransferInfoEdit(object sender, CustomEventArgsEdit e)
        {
            Dispatcher.Invoke(DispatcherPriority.Send, new AddData(UpdateData), "");
        }
        private void UserControlAdd_TransferInfoAdd(object sender, CustomEventArgsAdd e)
        {
            Dispatcher.Invoke(DispatcherPriority.Send, new AddData(UpdateData), "");
        }
        public void UpdateData(string data)
        {
            _ObjCon = new Connection();
            _ObjCon.Connected();
            _ObjCon.SqlCmd = new SqlCommand();
            try
            {
                _ObjCon.SqlCmd.Connection = _ObjCon.SqlCon;
                _ObjCon.SqlCmd.CommandType = CommandType.StoredProcedure;
                _ObjCon.SqlCmd.CommandText = "OA_PIDLibrary";
                _ObjCon.SqlRead = _ObjCon.SqlCmd.ExecuteReader();
                _DataComms = new List<Communication>();
                while (_ObjCon.SqlRead.Read())
                {
                    _DataComms.Add(new Communication()
                    {
                        Mediaid = Convert.ToInt16(_ObjCon.SqlRead.GetValue(0)),
                        MediaAnnoucements = _ObjCon.SqlRead.GetString(1)
                    });
                }
                GridPID.ItemsSource = _DataComms;
                _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Grid PID loaded....");
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
            _ObjCon.Disconnected();
            RbPre.IsChecked = true;
        }

        private void MenuAddClick(object sender, RoutedEventArgs e)
        {
            UserControlAdd _userControlAdd = new UserControlAdd();
            _userControlAdd.SetClassObj(_Obj);
            _userControlAdd.TransferInfoAdd += UserControlAdd_TransferInfoAdd;
            Window window = new Window
            {
                Title = "Add New Text",
                Content = _userControlAdd,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                MaxHeight = 150,
                MaxWidth = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize               
            };
            window.ShowDialog();          
        }
        private void MenuEditClick(object sender, RoutedEventArgs e)
        {
            UserControlEdit _userControlEdit = new UserControlEdit();
            _userControlEdit.SetClassObj(_Obj);
            _userControlEdit.TransferInfoEdit += UserControlEdit_TransferInfoEdit;
            Window window = new Window
            {
                Title = "Edit Currrent Text",
                Content = _userControlEdit,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                MaxHeight = 150,
                MaxWidth = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
            };
            window.ShowDialog();
        }
        private void MenuDeleteClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure to delete this text ?", "Question", 
                 MessageBoxButton.YesNo, 
                 MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _ObjCon = new Connection();
                _ObjCon.Connected();
                _ObjCon.SqlCmd = new SqlCommand();
                try
                {
                    _ObjCon.SqlCmd.Connection = _ObjCon.SqlCon;
                    _ObjCon.SqlCmd.CommandType = CommandType.StoredProcedure;
                    _ObjCon.SqlCmd.CommandText = "OA_DeleteValue";
                    _ObjCon.SqlCmd.Parameters.AddWithValue("@id", IntValue);
                    _ObjCon.SqlCmd.ExecuteNonQuery();
                    UpdateData("Refresh");
                    GridPID.SelectedIndex = 0;
                    MessageBox.Show("The Value Has been Deleted !", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
                }
                _ObjCon.Disconnected();
            }
        }
        private void GridPID_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Mediaid"))
            {
                e.Column.Width = 100;
            }
            else if (e.PropertyName.Equals("MediaAnnoucements"))
            {
                e.Column.Width = 700;                
            }
        }
        private ClassObject _Obj;
        private void GridPID_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Communication _classObj = GridPID.SelectedItem as Communication;
                _Obj = new ClassObject
                {
                    Id = Convert.ToInt16(_classObj.Mediaid),
                    Name = Convert.ToString(_classObj.MediaAnnoucements)
                };
                IntValue = _classObj.Mediaid;
            }
            catch(Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
        }
    }
}
