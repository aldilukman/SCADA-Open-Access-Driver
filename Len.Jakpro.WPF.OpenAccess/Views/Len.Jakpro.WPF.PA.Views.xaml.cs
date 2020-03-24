using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using Len.Jakpro.WPF.Connections;
namespace Len.Jakpro.WPF.PA.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControlPA: UserControl
    {
        private List<Communication> _DataComms = new List<Communication>();
        private Connection _ObjCon;
        public class Communication
        {
            public string MediaAnnoucements { get; set; }
            public string MediaItems { get; set; }
        }
        public UserControlPA()
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
                _ObjCon.Cmd.CommandText = "SELECT AnnoucementsName, MediaItem FROM dbo.TblListDVAs WHERE(MediaType = 'Audio')";
                _ObjCon.Reader = _ObjCon.Cmd.ExecuteReader();
                while (_ObjCon.Reader.Read())
                {
                    _DataComms.Add(new Communication()
                    {
                        MediaAnnoucements = _ObjCon.Reader.GetString(0),
                        MediaItems = _ObjCon.Reader.GetString(1)
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
            string id = classObj.MediaItems;
            MessageBox.Show(id);
        }
    }
}
