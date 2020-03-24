using Len.Jakpro.Database.Rendundancy;
using Len.Jakpro.PA.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Len.Jakpro.WPF.PA.Library
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
      
        public class Communication
        {
            public string MediaAnnoucements { get; set; }
            public string MediaItems { get; set; }
        }
        public string ValueA
        {
            get
            {
                return (string)GetValue(ValueADependencyProperty);
            }
            set
            {
                SetValue(ValueADependencyProperty, value);
            }
        }
        public static readonly DependencyProperty ValueADependencyProperty = DependencyProperty.Register("ValueA", typeof(string),
        typeof(UserControl1), new FrameworkPropertyMetadata("Null", new PropertyChangedCallback(OnValueADependencyPropertyChanged)));
        // FrameworkPropertyMetadata(0.0) default
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private List<Communication> _DataComms = new List<Communication>();
        private Connection _ObjCon;
        private static void OnValueADependencyPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            UserControl1 control = source as UserControl1;
            if (control != null)
            {
                try
                {
                    control.ValueA = (string)e.NewValue;
                    control.GridPA.SelectedItem = control.ValueA.ToString();
                }
                catch (Exception ex)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
                }
            }
        }
        private void GridPA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Communication classObj = GridPA.SelectedItem as Communication;
                ValueA = Convert.ToString(classObj.MediaItems);
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
        }
        private void GridPA_Loaded(object sender, RoutedEventArgs e)
        {
            var _logging = new NLogConfigurator();
            _logging.Configure();
            _ObjCon = new Connection();
            _ObjCon.Connected();
            _ObjCon.SqlCmd = new SqlCommand();
            try
            {
                _ObjCon.SqlCmd.Connection = _ObjCon.SqlCon;
                _ObjCon.SqlCmd.CommandType = CommandType.StoredProcedure;
                _ObjCon.SqlCmd.CommandText = "OA_PALibrary";
                _ObjCon.SqlRead = _ObjCon.SqlCmd.ExecuteReader();
                while (_ObjCon.SqlRead.Read())
                {
                    _DataComms.Add(new Communication()
                    {
                        MediaAnnoucements = _ObjCon.SqlRead.GetString(0),
                        MediaItems = _ObjCon.SqlRead.GetString(1)
                    });
                }
                GridPA.ItemsSource = _DataComms;
                _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Grid PA loaded....");
            }
            catch (SqlException ex)
            {
                _ObjCon.SqlCmd.Dispose();
                _ObjCon.SqlCon.Dispose();
                _ObjCon.SqlRead.Dispose();
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
            _ObjCon.Disconnected();
            GridPA.SelectedIndex = 0;
        }

        private void GridPA_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("MediaItems"))
            {
                e.Column.Width = 80;
            }
            else if (e.PropertyName.Equals("MediaAnnouncements"))
            {
                e.Column.Width = 700;
            }
        }
    }
}
