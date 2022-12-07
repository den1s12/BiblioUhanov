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
    /// Логика взаимодействия для AddReaderPage.xaml
    /// </summary>
    public partial class AddReaderPage : Page
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;

        ClassFolder.CBClass cbClass;

        public AddReaderPage()
        {
            InitializeComponent();
            cbClass = new ClassFolder.CBClass();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbClass.LoadAdresCB(IdAdressCB);
        }

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameReaderTB.Text) ||
               string.IsNullOrWhiteSpace(FirstNameReaderTB.Text) ||
               string.IsNullOrWhiteSpace(MiddleNameReaderTB.Text) ||
               string.IsNullOrWhiteSpace(IdAdressCB.Text) ||
               string.IsNullOrWhiteSpace(NumberPhoneHomeTB.Text) ||
               string.IsNullOrWhiteSpace(NumberPhoneWorkTB.Text) ||
               string.IsNullOrWhiteSpace(DateOfBirthTB.Text) ||
               string.IsNullOrWhiteSpace(NumberOfReaderTicketTB.Text))
            {
                MBClass.ErrorMB("Заполните все поля");
            }
            else
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert Into dbo.[Reader] " +
                    "(LastNameReader, FirstNameReader, MiddleNameReader, IdAdres, " +
                    "NumberPhoneHome, NumberPhoneWork, DateOfBirth, NumberOfReaderTicket) " +
                    "Values " +
                    $"('{LastNameReaderTB.Text}', '{FirstNameReaderTB.Text}', '{MiddleNameReaderTB.Text}', " +
                    $"'{IdAdressCB.SelectedValue.ToString()}', " +
                    $"'{NumberPhoneHomeTB.Text}', " +
                    $"'{NumberPhoneWorkTB.Text}', '{DateOfBirthTB.Text}', " +
                    $"'{NumberOfReaderTicketTB.Text}')",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB("Читатель успешно добавлен");
                
                sqlConnection.Close();

            }
        }

        private void AdresCB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.AdresPage());
        }
    }
    
}
