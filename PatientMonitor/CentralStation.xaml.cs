using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PatientMonitor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public static string insertRow = "INSERT INTO SessionRecord (staff_id, session_start_time,session_end_time) VALUES (@staff_id, @session_start_time, @session_end_time)";
        public Window1()
        {
            InitializeComponent();

            DatabaseConnection.ConnectionStr = Properties.Settings.Default.PMSDatabaseConnectionString;
            SessionRecord session = new SessionRecord(1, "11/17/2015 21.05.55", "11/17/2015 23.05.55");
            string rows = DatabaseConnection.DatabaseConnectionInstance.InsertNewSession(session, insertRow);
            PatientFactory factory = new PatientFactory();
            Controller controller = new Controller(this, factory);
            controller.RunMonitor();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }


    }
}
