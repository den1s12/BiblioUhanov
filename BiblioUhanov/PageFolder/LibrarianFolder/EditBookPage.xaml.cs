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
        CBClass cB;
        SqlConnection sqlConnection =
          new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;
        public EditBookPage()
        {
            InitializeComponent();
            cB = new CBClass();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cB.LoadCityCB(NameCityCB);
            cB.LoadPublishingHouseCB(NamePublishingHouseCB);
            cB.LoadFIO(AuthorCB);
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[Book]" +
                    $"Where IdBook = '{VariableClass.IdBook}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                UniqueCipher.Text = dataReader[1].ToString();
                NameBook.Text = dataReader[2].ToString();
                AuthorCB.SelectedValue = dataReader[3].ToString();
                NameCityCB.SelectedValue = dataReader[4].ToString();
                NamePublishingHouseCB.SelectedValue = dataReader[5].ToString();
                YearPublish.Text = dataReader[6].ToString();
                CountPage.Text = dataReader[7].ToString();
                PriceBook.Text = dataReader[8].ToString();
                NumberOfInstances.Text = dataReader[9].ToString();

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
                SqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Book]" +
                    $"Set UniqueCipher ='{UniqueCipher.Text}'," +
                    $"NameBook ='{NameBook.Text}'," +
                    $"AuthorCB ='{AuthorCB.SelectedValue.ToString()}'," +
                    $"NameCityCB ='{NameCityCB.SelectedValue}'," +
                    $"NamePublishingHouseCB ='{NamePublishingHouseCB.Text}'," +
                    $"YearPublish ='{YearPublish.Text}', " +
                    $"CountPage ='{CountPage.Text}', " +
                    $"PriceBook ='{PriceBook.Text}', " +
                    $"NumberOfInstances ='{NumberOfInstances.Text}' " +
                    $"Where IdBook ='{VariableClass.IdBook}'",
                    sqlConnection);
                SqlCommand.ExecuteNonQuery();
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
