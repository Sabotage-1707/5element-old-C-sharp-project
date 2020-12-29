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
    /// Логика взаимодействия для ExampleAcsForGadgets.xaml
    /// </summary>
    public partial class ExampleAcsForGadgets : Page
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
                    select = "Select id_Товара, name, price, Image, x.Device_Type, x.Connection_Type, x.Device_Construction From Товар JOIN (SELECT ТИП_Товара.id, Device_Type, Connection_Type, Device_Construction FROM ТИП_Товара JOIN Наушники ON(ТИП_Товара.id = Наушники.id)) x ON(x.id = Товар.id_Type) ORDER BY price";
                    break;
                case 2:
                    select = "Select id_Товара, name, price, Image, x.Device_Type, x.Connection_Type, x.Device_Construction From Товар JOIN (SELECT ТИП_Товара.id, Device_Type, Connection_Type, Device_Construction FROM ТИП_Товара JOIN Наушники ON(ТИП_Товара.id = Наушники.id)) x ON(x.id = Товар.id_Type) ORDER BY price DESC";
                    break;
                case 3:
                    select = "Select id_Товара, name, price, Image, x.Device_Type, x.Connection_Type, x.Device_Construction From Товар JOIN (SELECT ТИП_Товара.id, Device_Type, Connection_Type, Device_Construction FROM ТИП_Товара JOIN Наушники ON(ТИП_Товара.id = Наушники.id)) x ON(x.id = Товар.id_Type) ORDER BY Name";
                    break;
            }

            Initialization(select, NameExampleHeadphones1Y, CodeExampleHeadphones1Y, PriceExampleHeadphones1, KindExampleHeadphones1Y, TypeExampleHeadphones1Y, ConstructionExampleHeadphones1Y, ExampleHeadphones1, 0);
            Initialization(select, NameExampleHeadphones2Y, CodeExampleHeadphones2Y, PriceExampleHeadphones2, KindExampleHeadphones2Y, TypeExampleHeadphones2Y, ConstructionExampleHeadphones2Y, ExampleHeadphones2, 1);
            Initialization(select, NameExampleHeadphones3Y, CodeExampleHeadphones3Y, PriceExampleHeadphones3, KindExampleHeadphones3Y, TypeExampleHeadphones3Y, ConstructionExampleHeadphones3Y, ExampleHeadphones3, 2);
        }
        public ExampleAcsForGadgets()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            select = "Select id_Товара, name, price, Image, x.Device_Type, x.Connection_Type, x.Device_Construction From Товар JOIN (SELECT ТИП_Товара.id, Device_Type, Connection_Type, Device_Construction FROM ТИП_Товара JOIN Наушники ON(ТИП_Товара.id = Наушники.id)) x ON(x.id = Товар.id_Type)";
            Initialization(select, NameExampleHeadphones1Y, CodeExampleHeadphones1Y, PriceExampleHeadphones1, KindExampleHeadphones1Y, TypeExampleHeadphones1Y, ConstructionExampleHeadphones1Y, ExampleHeadphones1, 0);
            Initialization(select, NameExampleHeadphones2Y, CodeExampleHeadphones2Y, PriceExampleHeadphones2, KindExampleHeadphones2Y, TypeExampleHeadphones2Y, ConstructionExampleHeadphones2Y, ExampleHeadphones2, 1);
            Initialization(select, NameExampleHeadphones3Y, CodeExampleHeadphones3Y, PriceExampleHeadphones3, KindExampleHeadphones3Y, TypeExampleHeadphones3Y, ConstructionExampleHeadphones3Y, ExampleHeadphones3, 2);
        }


        private void Initialization(string select, TextBlock Name, TextBlock Code, TextBlock Price, TextBlock Kind, TextBlock Type, TextBlock Construction, Image image, int i)
        {
            DataTable Goods = Select(select);
            Name.Text = Convert.ToString(Goods.Rows[i][1]);
            Code.Text = Convert.ToString(Goods.Rows[i][0]);
            Price.Text = " " + Convert.ToString(Goods.Rows[i][2]);
            Kind.Text = Convert.ToString(Goods.Rows[i][4]);
            Type.Text = Convert.ToString(Goods.Rows[i][5]);
            Construction.Text = Convert.ToString(Goods.Rows[i][6]);

            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            string path1 = Convert.ToString(Goods.Rows[i][3]);
            myBitmapImage.UriSource = new Uri($@"{path1}");
            myBitmapImage.EndInit();
            image.Source = myBitmapImage;
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

        private void BuyExampleHeadphones1(object sender, RoutedEventArgs e)
        {
            buy(NameExampleHeadphones1Y, PriceExampleHeadphones1, ExampleHeadphones1);
        }
        private void BuyExampleHeadphones2(object sender, RoutedEventArgs e)
        {
            buy(NameExampleHeadphones2Y, PriceExampleHeadphones2, ExampleHeadphones2);
        }
        private void BuyExampleHeadphones3(object sender, RoutedEventArgs e)
        {
            buy(NameExampleHeadphones3Y, PriceExampleHeadphones3, ExampleHeadphones3);
        }
    }
}
