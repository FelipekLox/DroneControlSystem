using System;
using System.IO;

namespace DroneControlSystem
{
    internal class Program
    {
        private const string FileName = "drones.txt";

        static void Main(string[] args)
        {
            Drone[] drones = new Drone[]
            {
                new Drone("DJI Mini 3", 85),
                new Drone("Autel EVO Lite", 45),
                new Drone("Parrot Anafi", 15),
                new Drone("Syma X5C")
            };

            while (true)
            {
                try
                {
                    Console.WriteLine("\n=== Система управления БПЛА (версия 3.0) ===");
                    Console.WriteLine("1. Показать информацию о всех дронах");
                    Console.WriteLine("2. Взлёт выбранного дрона");
                    Console.WriteLine("3. Подзарядка выбранного дрона");
                    Console.WriteLine("4. Расчёт времени полёта");
                    Console.WriteLine("5. Сохранить состояние в файл");
                    Console.WriteLine("6. Загрузить состояние из файла");
                    Console.WriteLine("0. Выход");
                    Console.Write("Выберите действие: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Ошибка: введите числовое значение.");
                        continue;
                    }

                    if (choice == 0)
                    {
                        break;
                    }

                    switch (choice)
                    {
                        case 1:
                            if (drones.Length == 0)
                            {
                                Console.WriteLine("Список дронов пуст.");
                            }
                            for (int i = 0; i < drones.Length; i++)
                            {
                                Console.WriteLine($"\n[Индекс: {i}]");
                                drones[i].PrintInfo();
                            }
                            break;

                        case 2:
                            Console.Write($"Введите индекс дрона (0 - {drones.Length - 1}): ");
                            if (int.TryParse(Console.ReadLine(), out int takeoffIndex) && takeoffIndex >= 0 && takeoffIndex < drones.Length)
                            {
                                drones[takeoffIndex].TakeOff();
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: неверный индекс дрона.");
                            }
                            break;

                        case 3:
                            Console.Write($"Введите индекс дрона (0 - {drones.Length - 1}): ");
                            if (int.TryParse(Console.ReadLine(), out int chargeIndex) && chargeIndex >= 0 && chargeIndex < drones.Length)
                            {
                                Console.Write("Введите количество процентов для зарядки: ");
                                if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
                                {
                                    drones[chargeIndex].ChargeBattery(amount);
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка: введите положительное число.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: неверный индекс дрона.");
                            }
                            break;

                        case 4:
                            Console.Write($"Введите индекс дрона (0 - {drones.Length - 1}): ");
                            if (int.TryParse(Console.ReadLine(), out int timeIndex) && timeIndex >= 0 && timeIndex < drones.Length)
                            {
                                int flightTime = drones[timeIndex].CalculateFlightTime();
                                if (drones[timeIndex].IsEngineOn)
                                {
                                    Console.WriteLine($"Оставшееся время полёта {drones[timeIndex].Model}: {flightTime} мин.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: неверный индекс дрона.");
                            }
                            break;

                        case 5:
                            SaveDrones(drones, FileName);
                            break;

                        case 6:
                            drones = LoadDrones(FileName);
                            Console.WriteLine($"Загружено дронов: {drones.Length}");
                            break;

                        default:
                            Console.WriteLine("Неизвестная команда. Выберите пункт от 0 до 6.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }

            Console.WriteLine("\nПрограмма завершена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void SaveDrones(Drone[] drones, string fileName)
        {
            string[] lines = new string[drones.Length];
            for (int i = 0; i < drones.Length; i++)
            {
                lines[i] = $"{drones[i].Model};{drones[i].BatteryLevel};{drones[i].Altitude};{drones[i].IsEngineOn}";
            }
            File.WriteAllLines(fileName, lines);
            Console.WriteLine($"Состояние сохранено в файл {fileName}");
        }

        static Drone[] LoadDrones(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден. Будет создан новый массив.");
                return new Drone[0];
            }
            string[] lines = File.ReadAllLines(fileName);
            Drone[] drones = new Drone[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                drones[i] = new Drone(parts[0], int.Parse(parts[1]))
                {
                    Altitude = double.Parse(parts[2]),
                    IsEngineOn = bool.Parse(parts[3])
                };
            }
            return drones;
        }
    }
}
