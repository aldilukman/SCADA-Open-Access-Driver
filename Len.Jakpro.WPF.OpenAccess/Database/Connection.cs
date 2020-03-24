using System;
using System.Data.SqlClient;
using System.Windows;
namespace Len.Jakpro.WPF.Connections
{
    public class Connection
    {
        public SqlCommand Cmd;
        public SqlConnection Con;
        public SqlDataReader Reader;
        public void Connected()
        {
            Con = new SqlConnection("server=localhost\\ZENON_2012;database=Len.Jakpro.JLRTSCADA;user id=zenOnSrv;password=zen_$2012");
            try
            {
                Con.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        public void Disconnected()
        {
            Con.Close();
            Cmd.Dispose();
            Reader.Close();
        }
    }
}
