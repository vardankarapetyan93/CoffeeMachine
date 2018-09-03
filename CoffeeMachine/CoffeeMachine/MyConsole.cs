using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoffeeMachine
{
    class MyConsole
    {


        Machine machine;

        public MyConsole()
        {
            machine = new Machine();
        }
        public void Start()
        {
            Console.WriteLine("Hi. Welcome to Coffe Machine");
            Thread.Sleep(2500);
            Console.Clear();
            machine.GetData();
            AddMoney();
            ChooseCoffee();

        }
        private void AddMoney()
        {
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please add money to the machine\n");
                Console.WriteLine("50 coins   =   NumPad1    100 coins   =   NumPad2    200 coins   =   NumPad3    500 coins   =   NumPad4\n");
                Console.WriteLine("Continue   =   Enter    Take money   =   Esc\n");
                Console.WriteLine($"Your balance = {machine.Balance}");
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.NumPad1)
                {
                    machine.Balance += 50;
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    machine.Balance += 100;
                }
                else if (key == ConsoleKey.NumPad3)
                {
                    machine.Balance += 200;
                }
                else if (key == ConsoleKey.NumPad4)
                {
                    machine.Balance += 500;
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    machine.Balance = 0;
                }
            }
        }
        private bool TakeMoneyOrTryAgain(ref bool b)
        {
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Your balance = {machine.Balance}\n");
                Console.WriteLine("Try again   =   Enter    Add money   =   NumPad1    Take money   =   Esc");
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                {
                    b = true;
                    return true;
                }
                else if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Thank you for coosing our coffee!!");
                    Thread.Sleep(2500);
                    b = false;
                    break;
                }
                else if (key == ConsoleKey.NumPad1)
                {
                    AddMoney();
                    b = false;
                    return true;
                }
            }

            return false;
        }
        private bool EnoughIngredientsTryCatch(double water, double coffee, double sugar)
        {
            try
            {
                machine.tempWater = water;
                machine.tempCoffee = coffee;
                machine.tempSugar = sugar;
                machine.ingredientsCount[0] -= water;
                machine.ingredientsCount[1] -= coffee;
                machine.ingredientsCount[2] -= sugar;
                if (machine.ingredientsCount[0] < 0 || machine.ingredientsCount[1] < 0 || machine.ingredientsCount[2] < 0)
                {
                    throw new Exception("Sorry there isn't enough ingredients!!!");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2500);

                return false;
            }
            return true;
        }
        private bool ChooseCoffeeTryCatch(int price, double water, double coffee, double sugar, ref bool tryAgainOrAddMoney)
        {
            bool temp = true;
            bool validBalance = true;
            bool temp2 = EnoughIngredientsTryCatch(water, coffee, sugar);

            try
            {
                machine.Balance -= price;
                if (machine.Balance < 0)
                {

                    throw new Exception("Sorry you don't have enough money!!!");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2500);
                validBalance = false;
            }

            if (validBalance == false || temp2 == false)
            {
                machine.Balance += price;
                machine.ingredientsCount[0] += water;
                machine.ingredientsCount[1] += coffee;
                machine.ingredientsCount[2] += sugar;
                temp = TakeMoneyOrTryAgain(ref tryAgainOrAddMoney);
                if (temp == true && tryAgainOrAddMoney == true)
                {
                    ChooseCoffee();
                }
                else if (temp == true && tryAgainOrAddMoney == false)
                {
                    return false;
                }
            }
            return true;
        }
        private void ChooseCoffee()
        {
            bool tryAgainOrAddMoney;

            bool temp = false;
            ConsoleKey key;
            while (true)
            {
                tryAgainOrAddMoney = true;
                Console.Clear();
                Console.WriteLine("Please choose your coffee\n\n");
                Console.WriteLine($"Your balance = {machine.Balance}\n");
                Console.WriteLine($"Water : {machine.ingredientsCount[0]}   Coffee : {machine.ingredientsCount[1]}   Sugar : {machine.ingredientsCount[2]}\n\n");
                Console.WriteLine("Nescafe Classic(50) = NumPad0    Nescafe Gold(100) = NumPad1\n");
                Console.WriteLine("Black coffe(150) = NumPad2    Arabica(200) = NumPad3\n");
                Console.WriteLine("Espresso(250)   =   NumPad4    Americano(300)   =   NumPad5\n");
                Console.WriteLine("Robusta(350) = NumPad6    Starbucks coffee(400) = NumPad7\n");
                Console.WriteLine("Nespresso(450) = NumPad8    Housebrand(500) = NumPad9\n");

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.NumPad0)
                {
                    temp = ChooseCoffeeTryCatch(50, 0.1, 0.2, 0.1, ref tryAgainOrAddMoney);
                }
                else if (key == ConsoleKey.NumPad1)
                {
                    temp = ChooseCoffeeTryCatch(100, 0.2, 0.2, 0.1, ref tryAgainOrAddMoney);
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    temp = ChooseCoffeeTryCatch(150, 0.3, 0.3, 0.2, ref tryAgainOrAddMoney);
                }
                else if (key == ConsoleKey.NumPad3)
                {
                    temp = ChooseCoffeeTryCatch(200, 0.3, 0.3, 0.3, ref tryAgainOrAddMoney);
                }
                else if (key == ConsoleKey.NumPad4)
                {
                    temp = ChooseCoffeeTryCatch(250, 0.2, 0.4, 0.3, ref tryAgainOrAddMoney);

                }
                else if (key == ConsoleKey.NumPad5)
                {
                    temp = ChooseCoffeeTryCatch(300, 0.5, 0.4, 0.3, ref tryAgainOrAddMoney);

                }
                else if (key == ConsoleKey.NumPad6)
                {
                    temp = ChooseCoffeeTryCatch(350, 0.5, 0.5, 0.4, ref tryAgainOrAddMoney);

                }
                else if (key == ConsoleKey.NumPad7)
                {
                    temp = ChooseCoffeeTryCatch(400, 0.7, 0.6, 0.4, ref tryAgainOrAddMoney);

                }
                else if (key == ConsoleKey.NumPad8)
                {
                    temp = ChooseCoffeeTryCatch(450, 0.7, 0.7, 0.5, ref tryAgainOrAddMoney);

                }
                else if (key == ConsoleKey.NumPad9)
                {
                    temp = ChooseCoffeeTryCatch(500, 0.8, 0.7, 0.6, ref tryAgainOrAddMoney);

                }
                if (!temp)
                {
                    continue;
                }
                else if (temp && !tryAgainOrAddMoney)
                {
                    break;
                }
                MakeCoffee(machine.tempWater, machine.tempCoffee, machine.tempSugar);
                if (TakeMoneyOrTryAgain(ref tryAgainOrAddMoney) == false)
                {
                    break;
                }
            }
        }
        private void MakeCoffee(double water, double coffee, double sugar)
        {
            ConsoleKey key;
            Console.Clear();
            Console.WriteLine("Please wait...");
            machine.UpdateData(water, coffee, sugar);
            Thread.Sleep(5000);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Coffee is ready!!!\n");
                Console.WriteLine("Press NumPad0 to continue");
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.NumPad0)
                {
                    break;
                }
            }
        }
    }
}
