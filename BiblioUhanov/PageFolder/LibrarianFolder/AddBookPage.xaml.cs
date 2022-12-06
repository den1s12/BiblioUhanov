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
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;

        ClassFolder.CBClass cbClass;
        public AddBookPage()
        {
            InitializeComponent();
            cbClass = new ClassFolder.CBClass();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbClass.LoadFIO(AuthorCB);
            cbClass.LoadCityCB(NameCityCB);
            cbClass.LoadPublishingHouseCB(NamePublishingHouseCB);
        }
        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UniqueCipher.Text) ||
                string.IsNullOrWhiteSpace(NameBook.Text) ||
                string.IsNullOrWhiteSpace(AuthorCB.Text) ||
                string.IsNullOrWhiteSpace(NamePublishingHouseCB.Text) ||
                string.IsNullOrWhiteSpace(NameCityCB.Text) || 
                string.IsNullOrWhiteSpace(YearPublish.Text) ||
                string.IsNullOrWhiteSpace(CountPage.Text) || 
                string.IsNullOrWhiteSpace(PriceBook.Text) ||
                string.IsNullOrWhiteSpace(NumberOfInstances.Text)) 
            {
                MBClass.ErrorMB("Заполните все поля");
            }
            else
            {
                //try
                //{
                    ReadIdAuthor();
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[Book] " +
                        "(UniqueCipher, NameBook, IdAuthor, IdPublishingHouse, " +
                        "IdCity, YearPublish, CountPage, PriceBook, NumberOfInstances) " +
                        "Values " +
                        $"('{UniqueCipher.Text}', '{NameBook.Text}', '{idAuthor}', " +
                        $"'{NamePublishingHouseCB.SelectedValue.ToString()}', " +
                        $"'{NameCityCB.SelectedValue.ToString()}', " +
                        $"'{YearPublish.Text}', '{CountPage.Text}', " +
                        $"'{PriceBook.Text}', " +
                        $"'{NumberOfInstances.Text}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Книга успешно добавлена");
                //}
                //catch(Exception ex)
                //{
                //    MBClass.ErrorMB(ex);
                //}
                //finally
                //{
                    sqlConnection.Close();
               // }
            }
        }            

        private void ReadIdAuthor()
        {
            try
            {
                SplitFIO();
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select IdAuthor " +
                    "From dbo.Author " +
                    $"Where LastNameAuthor='{lastName}' " +
                    $"AND FirstNameAuthor='{firstName}' " +
                    $"OR MiddleNameAuthor='{middleName}'",
                    sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();

                idAuthor = Int32.Parse(sqlDataReader[0].ToString());
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

        int idAuthor;
        string lastName;
        string firstName;
        string middleName;

        private void SplitFIO()
        {
            string fioAuthor=AuthorCB.Text;
            string[] fioAuthorMass=fioAuthor.Split(new char[] {' '});
            lastName=fioAuthorMass[0];
            firstName=fioAuthorMass[1];
            middleName=fioAuthorMass[2];
        }

        private void City_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.CityPage());
        }

        private void PublishingHouse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.PublishingHousePage());
        }

        private void Author_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.AuthorPage());
        }
    }
}
