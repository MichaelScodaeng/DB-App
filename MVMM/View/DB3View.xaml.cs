using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DB_App.MVMM.View
{
    /// <summary>
    /// Interaction logic for DB3View.xaml
    /// </summary>
    public partial class DB3View : UserControl
    {
        private string nameInsertTextBox;
        private string nameSelectTextBox;
        private string nameUpdateTextBox;
        private string nameSelectColTextBox;
        private string table_name = "employee";
        NpgsqlCommand cmd;
        List<String> colNames;
        NpgsqlConnection connection;
        string sql_select = "*";
        string sql_update;
        string sql_project;
        DataTable dt;
        public DB3View()
        {
            InitializeComponent();

            string connectionString = "Host='ep-yellow-water-a1xiuipf.ap-southeast-1.aws.neon.tech'; Database='Database_2301375'; User Id='Database_2301375_owner'; Password='FBuP8KWX3ZUz';";

            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM " + table_name, connection);

            using NpgsqlDataReader reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);

            int colNum = dt.Columns.Count;
            colNames = new List<string>();
            for (int i = 0; i < colNum; i++)
            {
                colNames.Add(dt.Columns[i].ColumnName);
            }
            DataGridTable.ItemsSource = dt.DefaultView;

            sql_select = "";
            sql_update = "";
            sql_project = "";

        }
        private int Update_Table(NpgsqlDataReader reader)
        {
            dt = new DataTable();
            dt.Load(reader);
            DataGridTable.ItemsSource = dt.DefaultView;
            return 1;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // Dont forget to add check and sql functions in this function
        private void sqlButton(string textBoxClassName, TextBox textBoxAppName, string command)
        {
            textBoxClassName = textBoxAppName.Text;
            string pre_select = sql_select;
            if (command == "Select" && textBoxClassName == "") textBoxClassName = " ";
            if (textBoxClassName != "")
            {
                string[] selectedKeys = textBoxClassName.Split(",");


                bool goodKey = true;

                //Do something to Check
                List<string> keys = new List<string>();
                List<string> values = new List<string>();

                if (command == "Insert")
                {
                    int i = 0;
                    foreach (string part in selectedKeys)//insert
                    {
                        string[] kk = part.Split("=");
                        string key;
                        string value;

                        if (kk.Length == 1)
                        {
                            key = colNames[i];
                            value = kk[0];
                            i++;
                        }
                        else if (kk.Length != 2 || !colNames.Contains(kk[0]))
                        {
                            goodKey = false;
                            break;
                        }
                        else
                        {
                            key = kk[0];
                            value = kk[1];
                        }
                        keys.Add(key);
                        values.Add(value);
                    }
                    if (i > colNames.Count)
                    {
                        goodKey = false;
                    }
                }
                else if (command == "Select")//select  +where clause
                {
                    sql_select = textBoxAppName.Text;
                }
                else if (command == "Update")//update
                {

                }
                else if (command == "Select Columns")//project
                {
                    sql_project = textBoxAppName.Text;
                    foreach (string part in selectedKeys)//insert
                    {
                        if (!colNames.Contains(part))
                        {
                            goodKey = false;
                            break;
                        }
                    }
                    if (sql_project == "*")
                    {
                        goodKey = true;
                    }
                }
                if (goodKey)
                {
                    // Do something to database
                    string preCommand = "";
                    if (command == "Select" || command == "Select Columns")
                    {
                        preCommand = "SELECT ";
                        if (sql_project != "")
                        {
                            preCommand += sql_project;
                        }
                        else if (sql_project == "")
                        {
                            preCommand += "*";
                        }
                        preCommand += " FROM " + table_name;
                        if (sql_select == " ") { sql_select = ""; textBoxAppName.Text = ""; }
                        if (sql_select != "") preCommand += " WHERE " + sql_select;

                    }
                    else if (command == "Insert")
                    {
                        string key = keys[0];
                        string value = values[0];
                        for (int i = 1; i < keys.Count; i++)
                        {
                            key += "," + keys[i];
                            value += "," + values[i];
                        }
                        preCommand = "INSERT INTO " + table_name + "(" + key + ")" + "VALUES (" + value + ")";
                    }
                    else if (command == "Update")
                    {
                        preCommand = "UPDATE " + table_name + " SET " + textBoxClassName;
                        if (sql_select != "") preCommand += " WHERE " + sql_select;
                    }
                    using NpgsqlCommand cmd = new NpgsqlCommand(preCommand, connection);

                    try
                    {
                        using NpgsqlDataReader reader = cmd.ExecuteReader();

                        //Refresh Database
                        int temp = reader.FieldCount;
                        if (command == "Insert" || command == "Update")
                        {
                            MessageBox.Show(command + " complete! please, refresh by click select button");
                        }
                        else
                        {
                            Update_Table(reader);
                            MessageBox.Show(command + " complete!");
                        }


                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(command + " failed because your input is invalid : " + e.Message + " ** with command ** " + preCommand);
                        if (command == "Select")
                        {
                            sql_select = pre_select;
                            textBoxAppName.Text = sql_select;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(command + " failed because your input is invalid", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                string nullBox = "Null text box";
                if (textBoxAppName != null) nullBox = textBoxAppName.Name;
                MessageBox.Show(command + " failed because you didn't input any data" + nullBox, "Null Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //InsertButton
        private void NameInsertButton_Click(object sender, RoutedEventArgs e)
        {
            sqlButton(nameInsertTextBox, NameInsertTextBox, "Insert");

        }

        //SelectButton
        private void NameSelectButton_Click(object sender, RoutedEventArgs e)
        {
            sqlButton(nameSelectTextBox, NameSelectTextBox, "Select");

        }

        //DeleteButton
        private void NameDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Delete " + dt.Rows.Count + " items ?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                //Delete Command
                string preCommand = "DELETE FROM " + table_name;
                if (sql_select != "") preCommand += " WHERE " + sql_select;
                using NpgsqlCommand cmd = new NpgsqlCommand(preCommand, connection);

                try
                {
                    using NpgsqlDataReader reader = cmd.ExecuteReader();

                    //Refresh Database
                    Update_Table(reader);

                    MessageBox.Show("Delete complete!");

                }
                catch (Exception exc)
                {
                    MessageBox.Show("Delete failed because your input is invalid : " + exc.Message + " ** with command ** " + preCommand);
                }


            }
        }

        //UpdateButton
        private void NameUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            sqlButton(nameUpdateTextBox, NameUpdateTextBox, "Update");

        }

        //SelectColButton
        private void NameSelectColButton_Click(object sender, RoutedEventArgs e)
        {
            sqlButton(nameUpdateTextBox, NameSelectColTextBox, "Select Columns");

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
