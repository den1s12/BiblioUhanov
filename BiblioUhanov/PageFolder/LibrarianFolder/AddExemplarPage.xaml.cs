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

namespace BiblioUhanov.PageFolder.LibrarianFolder
{
    /// <summary>
    /// Логика взаимодействия для AddExemplarPage.xaml
    /// </summary>
    public partial class AddExemplarPage : Page
    {
        SqlConnection sqlConnection =
         new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        ClassFolder.CBClass cbClass;

        public AddExemplarPage()
        {
            InitializeComponent();
            cbClass = new ClassFolder.CBClass();
        }

        private void AddExemplarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BookCB.Text) ||
               string.IsNullOrWhiteSpace(UniqueInventoryNumberTB.Text) ||
               string.IsNullOrWhiteSpace(PlacementTB.Text))

            {
                MBClass.ErrorMB("Заполните все поля");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[Exemplar] " +
                        "(IdBook, UniqueInventoryNumber, Placement) " +
                        $"Values" +
                        $"'{BookCB.SelectedValue.ToString()}'," +
                        $"'{UniqueInventoryNumberTB.Text}'," +
                        $"'{PlacementTB.Text}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Экземпляр успешно добавлен");
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbClass.LoadBookCB(BookCB);
        }
    }
}
