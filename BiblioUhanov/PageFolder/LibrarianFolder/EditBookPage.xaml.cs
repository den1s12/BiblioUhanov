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
    /// Логика взаимодействия для EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Page
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        ClassFolder.CBClass cbClass;

        public EditBookPage()
        {
            InitializeComponent();
            cbClass = new ClassFolder.CBClass();
        }
        int idAuthor;
        string lastName;
        string firstName;
        string middleName;

        private void SplitFIO()
        {
            string fioAuthor = AuthorCB.Text;
            string[] fioAuthorMass = fioAuthor.Split(new char[] { ' ' });
            lastName = fioAuthorMass[0];
            firstName = fioAuthorMass[1];
            middleName = fioAuthorMass[2];
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbClass.LoadCityCB(NameCityCB);
            cbClass.LoadPublishingHouseCB(NamePublishingHouseCB);
            cbClass.LoadFIO(AuthorCB);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * FROM dbo.Book " +
                    $"Where IdBook = '{VariableClass.IdBook}'",
                    sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                UniqueCipher.Text = sqlDataReader[1].ToString();
                NameBook.Text = sqlDataReader[2].ToString();
                AuthorCB.SelectedValue = sqlDataReader[3].ToString();
                NameCityCB.SelectedValue = sqlDataReader[4].ToString();
                NamePublishingHouseCB.SelectedValue = sqlDataReader[5].ToString();
                YearPublish.Text = sqlDataReader[6].ToString();
                CountPage.Text = sqlDataReader[7].ToString();
                PriceBook.Text = sqlDataReader[8].ToString();
                NumberOfInstances.Text = sqlDataReader[9].ToString();

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

        private void EditBookBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Book]" +
                    $"Set UniqueCipher ='{UniqueCipher.Text}'," +
                    $"NameBook ='{NameBook.Text}'," +
                    $"IdAuthor ='{idAuthor}'," +
                    $"NameCityCB ='{NameCityCB.SelectedValue}'," +
                    $"NamePublishingHouseCB ='{NamePublishingHouseCB.Text}'," +
                    $"YearPublish ='{YearPublish.Text}', " +
                    $"CountPage ='{CountPage.Text}', " +
                    $"PriceBook ='{PriceBook.Text}', " +
                    $"NumberOfInstances ='{NumberOfInstances.Text}' " +
                    $"Where IdBook ='{VariableClass.IdBook}'",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Книга " +
                    $"{NameBook.Text}" +
                    $"успешно отредактирована");
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

        private void Author_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.AuthorPage());
        }

        private void PublishingHouse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.PublishingHousePage());
        }

        private void City_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.CityPage());
        }
    }
}
