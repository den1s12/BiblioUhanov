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
    /// Логика взаимодействия для ListBookPage.xaml
    /// </summary>
    public partial class ListBookPage : Page
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;

        DGClass dGClass;
        public ListBookPage()
        {
            InitializeComponent();
            dGClass = new DGClass(ListBookDG);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDg("Select * FROM dbo.[ViewBook]" +
                $"Where FIOAuthor Like '%{SearchTb.Text}%'" +
                $"or NameBook Like '%{SearchTb.Text}%'");

        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDg("Select * FROM dbo.ViewBook");
        }

        private void EditM_Click(object sender, RoutedEventArgs e)
        {
            VariableClass.IdBook = dGClass.SelectId();
            NavigationService.Navigate(new EditBookPage());
            dGClass.LoadDg("Select * From dbo.[Book]");
        }

        private void DeleteM_Click(object sender, RoutedEventArgs e)
        {
            bool resultMB = MBClass.QuestionMB("Вы действительно хотите " +
                "удалить книгу?");

            if (resultMB)
            {
                if (ListBookDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Вы не выбрали строку");
                }
                else
                {
                    try
                    {
                        sqlConnection.Open();
                        var IdBook = dGClass.SelectId();
                        sqlCommand = new SqlCommand("DELETE FROM dbo.[Book]" +
                            $"Where IdBook={IdBook}", sqlConnection);
                        sqlCommand.ExecuteNonQuery();

                        MBClass.InfoMB("Книга была удалена");
                    }
                    catch (Exception ex)
                    {
                        MBClass.ErrorMB(ex);
                    }
                    finally
                    {
                        sqlConnection.Close();
                        dGClass.LoadDg("Select * From dbo.ViewBook");
                    }
                }
            }
        }

        private void ListBookDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListBookDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                VariableClass.IdBook = dGClass.SelectId();
                try
                {
                    NavigationService.Navigate(new EditBookPage());
                    dGClass.LoadDg("Select * From dbo.[Book]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }
    }
}
