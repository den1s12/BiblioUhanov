using BiblioUhanov.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace BiblioUhanov.PageFolder.LibrarianFolder.AddFolder
{
    /// <summary>
    /// Логика взаимодействия для StreetPage.xaml
    /// </summary>
    public partial class StreetPage : Page
    {
        SqlConnection sqlConnection =
          new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        public StreetPage()
        {
            InitializeComponent();
        }

        private void AddStreetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameStreetTB.Text))

            {
                MBClass.ErrorMB("Заполните поле");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[Street] " +
                        "(NameStreet) " +
                        $"Values ('{NameStreetTB.Text}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Улица {NameStreetTB.Text} " +
                        $"успешно добавлена");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
