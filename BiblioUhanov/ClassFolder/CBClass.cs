using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BiblioUhanov.ClassFolder
{
    class CBClass
    {
        SqlConnection sqlConnection =
             new SqlConnection(@"Data Source=10.128.14.64\SQLEXPRESS;
                            Initial Catalog=user158;
                            User ID=user158;
                            Password=wsruser158");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        SqlDataAdapter sqlDataAdapter;
        DataSet dataSet;

        public void LoadFIO(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From dbo.Author " +
                    "Order by IdAuthor ASC", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    comboBox.Items.Add(sqlDataReader[1].ToString()+" "+
                        sqlDataReader[2].ToString()+" "+
                        sqlDataReader[3].ToString());
                }
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
        public void LoadCityCB(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * From " +
                    "dbo.City Order by IdCity ASC", sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "City");
                comboBox.ItemsSource = 
                    dataSet.Tables["City"].DefaultView;
                comboBox.DisplayMemberPath = 
                    dataSet.Tables["City"].Columns["NameCity"].ToString();
                comboBox.SelectedValuePath = 
                    dataSet.Tables["City"].Columns["IdCity"].ToString();


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

        public void LoadPublishingHouseCB(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * From " +
                    "dbo.PublishingHouse Order by IdPublishingHouse ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "PublishingHouse");
                comboBox.ItemsSource =
                    dataSet.Tables["PublishingHouse"].DefaultView;
                comboBox.DisplayMemberPath = 
                    dataSet.Tables["PublishingHouse"].Columns["NamePublishingHouse"].ToString();
                comboBox.SelectedValuePath = 
                    dataSet.Tables["PublishingHouse"].Columns["IdPublishingHouse"].ToString();


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

        public void LoadBookCB(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * From " +
                    "dbo.Book Order by IdBook ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Book");
                comboBox.ItemsSource =
                    dataSet.Tables["Book"].DefaultView;
                comboBox.DisplayMemberPath =
                    dataSet.Tables["Book"].Columns["NameBook"].ToString();
                comboBox.SelectedValuePath =
                    dataSet.Tables["Book"].Columns["IdBook"].ToString();


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

        public void LoadAdresCB(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * From " +
                    "dbo.Adres Order by IdAdres ASC", sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Adres");
                comboBox.ItemsSource =
                    dataSet.Tables["Adres"].DefaultView;
                comboBox.DisplayMemberPath =
                    dataSet.Tables["Adres"].Columns["IndexAdres"].ToString();
                comboBox.SelectedValuePath =
                    dataSet.Tables["Adres"].Columns["IdAdres"].ToString();


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

        public void LoadRegionCB(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * From " +
                    "dbo.Region Order by IdRegion ASC", sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Region");
                comboBox.ItemsSource =
                    dataSet.Tables["Region"].DefaultView;
                comboBox.DisplayMemberPath =
                    dataSet.Tables["Region"].Columns["NameRegion"].ToString();
                comboBox.SelectedValuePath =
                    dataSet.Tables["Region"].Columns["IdRegion"].ToString();


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

        public void LoadStreetCB(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * From " +
                    "dbo.Street Order by IdStreet ASC", sqlConnection);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Street");
                comboBox.ItemsSource =
                    dataSet.Tables["Street"].DefaultView;
                comboBox.DisplayMemberPath =
                    dataSet.Tables["Street"].Columns["NameStreet"].ToString();
                comboBox.SelectedValuePath =
                    dataSet.Tables["Street"].Columns["IdStreet"].ToString();


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
