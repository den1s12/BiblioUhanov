using BiblioUhanov.ClassFolder;
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

namespace BiblioUhanov.PageFolder.LibrarianFolder
{
    /// <summary>
    /// Логика взаимодействия для ListBookPage.xaml
    /// </summary>
    public partial class ListBookPage : Page
    {
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
    }
}
