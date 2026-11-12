using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControlSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления БПЛА (версия 2.0) ===\n");

            Drone[] drones = new Drone[]
            {
                new Drone("DJI Mini 3", 85),
                new Drone("Autel EVO Lite", 45),
                new Drone("Parrot Anafi", 15)
            };

            foreach (Drone drone in drones)
            {
                drone.PrintInfo();

                if (drone.BatteryLevel < 20)
                {
                    Console.WriteLine("❗ ВНИМАНИЕ: Критический уровень заряда! Взлет запрещен.");
                }

                if (drone.BatteryLevel < 50)
                {
                    Console.WriteLine($"Хотите подзарядить {drone.Model}? (1 - да, 0 - нет):");
                    int response = int.Parse(Console.ReadLine());

                    while (response == 1 && drone.BatteryLevel < 100)
                    {
                        Console.WriteLine("Введите количество процентов для зарядки:");
                        int amount = int.Parse(Console.ReadLine());
                        drone.ChargeBattery(amount);

                        if (drone.BatteryLevel < 100)
                        {
                            Console.WriteLine($"Продолжить зарядку {drone.Model}? (1 - да, 0 - нет):");
                            response = int.Parse(Console.ReadLine());
                        }
                    }
                }

                Console.WriteLine(new string('-', 30));
            }

            Console.WriteLine("\nПрограмма завершена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
