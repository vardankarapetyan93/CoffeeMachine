using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CoffeeMachine
{
    class Machine
    {
        private int balance;

        public double[] ingredientsCount = new double[3];
        public double tempWater { get; set; }
        public double tempCoffee { get; set; }
        public double tempSugar { get; set; }
        public int Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
            }
        }

        public void UpdateData(double water, double coffee, double sugar)
        {
            string conStr = "Data Source=DESKTOP-II28MH9;Initial Catalog=CoffeeMachine;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"update Ingredients set count = count - {water} where Id = 1" + 
                    $"update Ingredients set count = count - {coffee} where Id = 2" + 
                    $"update Ingredients set count = count - {sugar} where Id = 3", connection);
                int nNoAdded = command.ExecuteNonQuery();
            }
        }

        public void GetData()
        {
            string conStr = "Data Source=DESKTOP-II28MH9;Initial Catalog=CoffeeMachine;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"select * from Ingredients", connection);
                SqlDataReader reader = command.ExecuteReader();
                for (int i = 0; reader.Read(); i++)
                {
                    ingredientsCount[i] = (double)reader.GetValue(2);
                }
            }
        }

        

       
    }
}
