using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LibraryManagementSystem{
    
    public class Program{

        public static void Main(string[] args){

            Library lib = new Library();
            while (true){

                Console.Write("Login = l / Register = r / exit app = exit: ");
                string LogIn = Console.ReadLine().ToLower();

                if (LogIn == "l"){
                    Console.Clear();
                    lib.WelcolmeScreen();
                    Console.Write("Input username: ");
                    string login = Console.ReadLine();
                    Console.Write("Input user password: ");
                    string password = Console.ReadLine();
                    

                    if (lib.UserLogIn(login, password)){
                        while (true){

                            Console.Write("a = add new book (only admin) / d = deposit / b = borrow / e = Logout: ");
                            string choice = Console.ReadLine();
                            if (choice == "a" && login == "admin" && password == "admin"){
                                lib.AddNewBook();
                            }
                            else if (choice == "a" && login != "admin" && password != "admin"){
                                Console.WriteLine("This function is only for Administrator!");
                            }
                            else if (choice == "b"){
                                Console.Clear();
                                lib.WelcolmeScreen();
                                lib.Borrow(login);
                            }
                            else if (choice == "d"){
                                Console.Clear();
                                lib.WelcolmeScreen();
                                lib.Deposit(login);
                            }
                            else if (choice == "e"){
                                Console.Clear();
                                lib.WelcolmeScreen();
                                break;
                            }
                        }
                    }
                }
                else if (LogIn == "r"){
                    Console.Clear();
                    lib.WelcolmeScreen();
                    Console.Write("Input username: ");
                    string userName = Console.ReadLine();
                    Console.Write("Input password (min 8 char): ");
                    string userPassword = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPassword)){
                        if (userName != userPassword){
                            if (userPassword.Length >= 8){
                                lib.RegisterUser(userName, userPassword);
                                Console.Clear();
                                lib.WelcolmeScreen();
                                Console.WriteLine($"User {userName} successful registered in Library!");
                            }
                            else{
                                Console.WriteLine("Password is too short!");
                            }
                        }
                        else{
                            Console.WriteLine("Login and Password can't be the same!");
                        }

                    }
                    else{
                        Console.WriteLine("Username or password is invalid!");
                    }
                }
                else if (LogIn == "exit"){
                    Console.Clear();
                    lib.WelcolmeScreen();
                    Console.WriteLine("Your every changes will be saves in 'database.json' file...");
                    Console.WriteLine("When you open app again, I will try to load this data...");
                    Console.WriteLine("See you soon! :)");
                    Console.WriteLine("Shutting down application...");
                    break;
                }
            }
        }
    }
}
