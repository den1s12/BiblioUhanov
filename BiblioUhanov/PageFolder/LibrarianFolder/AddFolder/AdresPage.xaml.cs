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
    /// Логика взаимодействия для AdresPage.xaml
    /// </summary>
    public partial class AdresPage : Page
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;

        ClassFolder.CBClass cbClass;
        public AdresPage()
        {
            InitializeComponent();
            cbClass = new ClassFolder.CBClass();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbClass.LoadRegionCB(IdRegionCB);
            cbClass.LoadCityCB(IdCityCB);
            cbClass.LoadStreetCB(IdStreetCB);
        }

        private void AddRegionBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.RegionPage());
        }

        private void AddCityBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.CityPage());
        }

        private void AddStreetBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFolder.StreetPage());
        }

        private void AddAdresBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IndexAdresTB.Text) ||
               string.IsNullOrWhiteSpace(IdRegionCB.Text) ||
               string.IsNullOrWhiteSpace(IdCityCB.Text) ||
               string.IsNullOrWhiteSpace(IdStreetCB.Text) ||
               string.IsNullOrWhiteSpace(HouseTB.Text) ||
               string.IsNullOrWhiteSpace(FrameTB.Text) ||
               string.IsNullOrWhiteSpace(ApartmentTB.Text))
            {
                MBClass.ErrorMB("Заполните все поля");
            }
            else
            {
                
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert Into dbo.[Adres] " +
                    "(IndexAdres, IdRegion, IdCity, IdStreet, " +
                    "House, Frame, Apartment) " +
                    "Values " +
                    $"('{IndexAdresTB.Text}'," +
                    $"'{IdRegionCB.SelectedValue.ToString()}'," +
                    $"'{IdCityCB.SelectedValue.ToString()}', " +
                    $"'{IdStreetCB.SelectedValue.ToString()}', " +
                    $"'{HouseTB.Text}', " +
                    $"'{FrameTB.Text}', " +
                    $"'{ApartmentTB.Text}')",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB("Адрес успешно добавлен");
              
                sqlConnection.Close();

            }
        }
    }
    
}
