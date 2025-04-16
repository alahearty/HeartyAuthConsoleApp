using System;
using System.Collections.Generic;

namespace HeartyAuthConsoleApp
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var loginManager = new LoginManager();
            var isRunning = true;

            while (isRunning)
            {
                DisplayMenu();

                var choice = GetUserInput("Select an option: ");
                switch (choice)
                {
                    case "1":
                        HandleRegistration(loginManager);
                        break;

                    case "2":
                        HandleLogin(loginManager);
                        break;

                    case "3":
                        DisplayRegisteredUsers(loginManager);
                        break;

                    case "4":
                        isRunning = false;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Display Registered Users");
            Console.WriteLine("4. Exit");
        }

        private static string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        private static void HandleRegistration(LoginManager loginManager)
        {
            var username = GetUserInput("Enter username: ");
            var password = GetUserInput("Enter password: ");
            loginManager.Register(username, password);
        }

        private static void HandleLogin(LoginManager loginManager)
        {
            var username = GetUserInput("Enter username: ");
            var password = GetUserInput("Enter password: ");
            loginManager.Login(username, password);
        }

        private static void DisplayRegisteredUsers(LoginManager loginManager)
        {
            Console.WriteLine("\nRegistered Users:");
            var users = loginManager.GetRegisteredUsers().ToList();
            if (users.Count == 0)
            {
                Console.WriteLine("No registered users.");
                return;
            }

            int count = 1;
            foreach (var user in users)
            {
                Console.WriteLine($"{count} - {user}");
                count++;
            }
        }
    }
}