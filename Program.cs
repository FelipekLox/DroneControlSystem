using System;

namespace DroneControlSystem
{
    internal class Program
    {
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
                            Console.WriteLine("Функция сохранения в файл будет добавлена в следующем задании.");
                            break;

                        case 6:
                            Console.WriteLine("Функция загрузки из файла будет добавлена в следующем задании.");
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
    }
}