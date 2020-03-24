using System;
using System.Data;
using System.Data.SqlClient;
using Len.Jakpro.Logging;
using Microsoft.Win32;
using NLog;
namespace Len.Jakpro.Database.Rendundancy
{
    /// <summary>
    /// Database Service.
    /// </summary>
    public class Connection
    {
        public SqlCommand SqlCmd;
        public SqlConnection SqlCon;
        public SqlDataReader SqlRead;

        private string _PrimaryServerName;
        private string _SecondaryServerName;
        private string _DbName;
        private string _UserName;
        private string _Password;         
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public Connection()
        {
            var _logging = new NLogConfigurator();
            _logging.Configure();
            //opening the subkey  
            RegistryKey _key = Registry.CurrentUser.OpenSubKey("Software\\Len.Jakpro.OpenAccess\\Server");
            //if it does exist, retrieve the stored values
            if (_key != null)
            {
                _PrimaryServerName= _key.GetValue("ServerPrimary").ToString();
                _SecondaryServerName = _key.GetValue("ServerSecondary").ToString();
                _DbName = _key.GetValue("Database").ToString();
                _UserName = _key.GetValue("Username").ToString();
                _Password = _key.GetValue("Password").ToString();
                _key.Close();
            }
        }
        public void Connected()
        {            
           SqlCon = new SqlConnection("server=" + _PrimaryServerName + "\\ZENON_2012;" +
           "database=" + _DbName + ";" +
           "user id=" + _UserName + ";" +
           "password=" + _Password + ";");
            try
            {
                SqlCon.Open();
            }
            catch (SqlException ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(ex.StackTrace, ex.Message));
            }
        }
        public void Disconnected()
        {
            if (SqlCon != null && SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Close();
            }
            else if (SqlRead != null)
            {
                SqlRead.Close();
            }
            else if (SqlCmd != null)
            {
                SqlCmd.Dispose();
            }          
        }
        ~Connection()
        {
            Console.WriteLine("The instance of" +
                       " Connection class Destroyed");
        }
    }
}
