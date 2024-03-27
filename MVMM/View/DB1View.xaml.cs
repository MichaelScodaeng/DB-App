using System;
using System.Collections.Generic;
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
    /// Interaction logic for DB1View.xaml
    /// </summary>
    public partial class DB1View : UserControl
    {
        private string nameInsertTextBox;
        private string nameSelectTextBox;
        private string nameUpdateTextBox;
        private string nameSelectColTextBox;
        public DB1View()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // Dont forget to add check and sql functions in this function
        private void sqlButton(string textBoxClassName, TextBox textBoxAppName,string command) 
        {
            textBoxClassName = textBoxAppName.Text;
            if (textBoxClassName != null)
            {
                string[] selectedKeys = textBoxClassName.Split(",");

                
                bool goodKey = false;
                //Do something to Check

                if (goodKey)
                {
                    // Do something to database

                    //Refresh Database

                    MessageBox.Show(command+ " is Completed!!");
                }
                else
                {
                    MessageBox.Show(command + " failed because your input is invalid","Invalid Data", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show(command + " failed because you didn't input any data", "Null Input", MessageBoxButton.OK, MessageBoxImage.Error);
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
            var result = MessageBox.Show("Really?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                //Delete Command
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
            sqlButton(nameSelectColTextBox, NameSelectColTextBox, "Select Columns");

        }

    }
}
