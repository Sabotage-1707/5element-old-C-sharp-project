﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LAB33_KPIAP.Pages.Showcase
{
    /// <summary>
    /// Логика взаимодействия для ExampleTV.xaml
    /// </summary>
    public partial class ExampleTV : Page
    {
        string connectionString;
        string select = "";
        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton rb = sender as System.Windows.Controls.RadioButton;
            if (rb != null)
            {
                string profession = rb.Tag.ToString();
                switch (profession)
                {
                    case "Price":
                        Load(1);
                        break;

                    case "Price2":
                        Load(2);
                        break;
                    case "Name":
                        Load(3);
                        break;
                }
            }
        }
        public void Load(int numb)
        {
            
            switch (numb)
            {
                case 1:
                    select = "Select id_Товара, name, price, Image, Diagonal, Razreshenie, Smart_TV From Товар JOIN ТИП_Товара ON(ТИП_Товара.id = Товар.id_Type) JOIN Телевизоры ON(ТИП_Товара.id = Телевизоры.id) ORDER BY price";
                    break;
                case 2:
                    select = "Select id_Товара, name, price, Image, Diagonal, Razreshenie, Smart_TV From Товар JOIN ТИП_Товара ON(ТИП_Товара.id = Товар.id_Type) JOIN Телевизоры ON(ТИП_Товара.id = Телевизоры.id) ORDER BY price DESC";
                    break;
                case 3:
                    select = "Select id_Товара, name, price, Image, Diagonal, Razreshenie, Smart_TV From Товар JOIN ТИП_Товара ON(ТИП_Товара.id = Товар.id_Type) JOIN Телевизоры ON(ТИП_Товара.id = Телевизоры.id) ORDER BY Name";
                    break;
            }

            Initialization(select, NameExampleTV1Y, CodeExampleTV1Y, PriceExampleTV1, DiagonalExampleTV1Y, RazreshenieExampleTV1Y, SmartExampleTV1Y, ExampleTV1, 0);
            Initialization(select, NameExampleTV2Y, CodeExampleTV2Y, PriceExampleTV2, DiagonalExampleTV2Y, RazreshenieExampleTV2Y, SmartExampleTV2Y, ExampleTV2, 1);
            Initialization(select, NameExampleTV3Y, CodeExampleTV3Y, PriceExampleTV3, DiagonalExampleTV3Y, RazreshenieExampleTV3Y, SmartExampleTV3Y, ExampleTV3, 2);
        }
        public ExampleTV()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            select = "Select id_Товара, name, price, Image, Diagonal, Razreshenie, Smart_TV From Товар JOIN ТИП_Товара ON(ТИП_Товара.id = Товар.id_Type) JOIN Телевизоры ON(ТИП_Товара.id = Телевизоры.id)";
            Initialization(select, NameExampleTV1Y, CodeExampleTV1Y, PriceExampleTV1, DiagonalExampleTV1Y, RazreshenieExampleTV1Y, SmartExampleTV1Y, ExampleTV1, 0);
            Initialization(select, NameExampleTV2Y, CodeExampleTV2Y, PriceExampleTV2, DiagonalExampleTV2Y, RazreshenieExampleTV2Y, SmartExampleTV2Y, ExampleTV2, 1);
            Initialization(select, NameExampleTV3Y, CodeExampleTV3Y, PriceExampleTV3, DiagonalExampleTV3Y, RazreshenieExampleTV3Y, SmartExampleTV3Y, ExampleTV3, 2);

        }

        private void Initialization(string select, TextBlock Name, TextBlock Code, TextBlock Price, TextBlock Diagonal, TextBlock Razreshenie, TextBlock Smart, Image image, int i)
        {
            DataTable Goods = Select(select);
            Name.Text = Convert.ToString(Goods.Rows[i][1]);
            Code.Text = Convert.ToString(Goods.Rows[i][0]);
            Price.Text = " " + Convert.ToString(Goods.Rows[i][2]);
            Diagonal.Text = Convert.ToString(Goods.Rows[i][4]);
            Razreshenie.Text = Convert.ToString(Goods.Rows[i][5]);
            Smart.Text = Convert.ToString(Goods.Rows[i][6]);


            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            string path1 = Convert.ToString(Goods.Rows[i][3]);
            myBitmapImage.UriSource = new Uri($@"{path1}");
            myBitmapImage.EndInit();
            image.Source = myBitmapImage;
        }
        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(selectSQL, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception exec)
            {
                MessageBox.Show(exec.Message);
            }
            return dataTable;

        }
        private void buy(TextBlock Name, TextBlock Price, Image image)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = new SqlConnection(connectionString);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format("INSERT INTO  Korzina( Image, Name , Price ) VALUES (N'{0}', N'{1}' , N'{2}')", image.Source, Name.Text, Price.Text);
            cmd.Connection = connection;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show($"Добавлено в корзину");

        }



        private void BuyExampleTV1(object sender, RoutedEventArgs e)
        {
            buy(NameExampleTV1Y, PriceExampleTV1, ExampleTV1);
        }
        private void BuyExampleTV2(object sender, RoutedEventArgs e)
        {
            buy(NameExampleTV2Y, PriceExampleTV2, ExampleTV2);
        }
        private void BuyExampleTV3(object sender, RoutedEventArgs e)
        {
            buy(NameExampleTV3Y, PriceExampleTV3, ExampleTV3);
        }

    }
}