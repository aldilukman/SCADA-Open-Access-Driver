using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Len.Jakpro.WPF.Connections;
namespace Len.Jakpro.WPF.PID.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControlPID : UserControl
    {
        private List<Communication> _DataComms = new List<Communication>();
        private Connection _ObjCon;
        public string id { get; set; }
        public class Communication
        {
            public string MediaAnnoucements { get; set; }
        }
        public UserControlPID()
        {
            InitializeComponent();            
        }
        private void DatagridView_Loaded(object sender, RoutedEventArgs e)
        {
            _ObjCon = new Connection();
            _ObjCon.Connected();
            _ObjCon.Cmd = new SqlCommand();
            try
            {
                _ObjCon.Cmd.Connection = _ObjCon.Con;
                _ObjCon.Cmd.CommandType = CommandType.Text;
                _ObjCon.Cmd.CommandText = "SELECT AnnoucementsName FROM dbo.TblListDVAs WHERE(MediaType = 'Text')";
                _ObjCon.Reader = _ObjCon.Cmd.ExecuteReader();
                while (_ObjCon.Reader.Read())
                {
                    _DataComms.Add(new Communication()
                    {
                        MediaAnnoucements = _ObjCon.Reader.GetString(0)
                    });
                }
                DatagridView.ItemsSource = _DataComms;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            _ObjCon.Disconnected();
        }

        private void DatagridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Communication classObj = DatagridView.SelectedItem as Communication;
            id = classObj.MediaAnnoucements;
            MessageBox.Show(id);
            try
            {
                ValueA = Convert.ToString(classObj.MediaAnnoucements);
            }
            catch (Exception)
            { }
        }
       


        /// <summary>

        /// Dependency property for ValueA

        /// </summary>

        public static readonly DependencyProperty ValueADependencyProperty =

        DependencyProperty.Register("ValueA", typeof(double),

        typeof(UserControlPID), new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnValueADependencyPropertyChanged)));
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
        private static void OnValueADependencyPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            UserControlPID control = source as UserControlPID;
            if (control != null)
            {
                try
                {
                    control.ValueA = (string)e.NewValue;
                    control.DatagridView.SelectedItem = control.ValueA.ToString();
                }
                catch (Exception)
                { }
            }
        }
    }
}
