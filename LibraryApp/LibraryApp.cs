using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;

namespace LibraryManagementSystem{

public class Library{
        public MyDatabase database;

        public Library(){
            WelcolmeScreen();
            Setup();
        }
        public void WelcolmeScreen(){
            string str = 
            @"
            ________________________________________________________
            _____    ____  .___   ____      ____    ____   __    __
            |   |    |  | |    \ |    \    /    \  |    \  \ \  / /
            |   |    |  | |____/ |  __/   | ____ | |  __/   \ \/ /
            |   |___ |  | |    \ |  |\ \  | ____ | |  |\ \   |  |
            |______| |__| |____/ |__| \_\ |_|  |_| |__| \_\  |__|

            This is simple Library application
            Created by Michal Kotlowski using proggraming language C#
            __
            Now, in this application, you can login/register users,
            borrow books, deposit them.
            As a 'admin' you can also borrow/deposit books
            but also you can add new books,
            to do this you have to input:
            Book title, Author, and number of pages.
            ________________________________________________________
            ";

            Console.WriteLine(str);
        }
        private void Setup(){
            string cwd = Directory.GetCurrentDirectory();
            string databasePath = Path.Combine(cwd, "database.json");

            if (File.Exists(databasePath)){
                string json = File.ReadAllText(databasePath);
                database = JsonConvert.DeserializeObject<MyDatabase>(json);
                Console.WriteLine("Library successfully loaded!");
            }
            else if (!File.Exists(databasePath)){
                Console.WriteLine("I can't find database.json file from last session...");
                Console.WriteLine("No problem :) I will create new one!");
                database = new MyDatabase{
                    Books = new List<Book>(),
                    Users = new List<User>() { new User { Login = "admin", Password = "admin" } },
                    Registry = new List<Registry>()
                };
                var json = JsonConvert.SerializeObject(database);
                var newDb = JsonConvert.DeserializeObject<MyDatabase>(json);
                File.WriteAllText(databasePath, json);
                DownloadBooksFromApi();
            }

            Console.WriteLine("Psst! I have hint for you :) there is list of avaliable users, please don't waste it!");
            Console.WriteLine("users data are in format LOGIN | PASSWORD");
            foreach (User user in database.Users){
                Console.WriteLine($"{user.Login} | {user.Password}");
            }
        }

        private async void DownloadBooksFromApi(){
            string url = "https://wolnelektury.pl/api/books/?format=json";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            List<Book> books = new List<Book>();

            if (response.IsSuccessStatusCode){
                var jsonresponse = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<Book>>(jsonresponse);
                var i = 0;
                foreach (var book in books){
                    if (i < 60){
                    (database.Books).Add(new Book { Title = book.Title, Author = book.Author, Pages = book.Pages, Borrow = "No" });
                    OpenJsonFile();
                    i++;
                }
                }
            }
        }

        private void OpenJsonFile(){
            string cwd = Directory.GetCurrentDirectory();
            string databasePath = Path.Combine(cwd, "database.json");
            string json = JsonConvert.SerializeObject(database);
            File.WriteAllText(databasePath, json);
        }

        private List<string> GetAvailableBooksToBorrow(List<string> books){
            Console.Clear();
            WelcolmeScreen();
            Console.WriteLine("Books available to borrow:");
            foreach (Book book in database.Books){
                if (book.Borrow == "No"){
                    Console.WriteLine(book.Title);
                    books.Add(book.Title);
                }
            }
            return books;
        }

        public void Borrow(string user){
            List<string> books = new List<string>();
            if (GetAvailableBooksToBorrow(books).Any()){
                Console.WriteLine("____________________________");
                Console.WriteLine("TYPE 'exit' to return => ...");
                Console.Write("Which book you want to borrow: ");
                string book = Console.ReadLine();
                if (!string.IsNullOrEmpty(book)){

                    if (book == "exit" || book == "EXIT" || book == "e"){
                        Console.Clear();
                        WelcolmeScreen();
                        return;
                    }
                    foreach (Book b in database.Books){
                        if (b.Title == book){
                            b.Borrow = "Yes";
                            OpenJsonFile();
                            database.Registry.Add(new Registry { User = user, Book = book });
                            OpenJsonFile();
                            Console.Clear();
                            WelcolmeScreen();
                            Console.WriteLine($"{user} just borrowed book: {book}");
                            return;
                        }
                    }
                    Console.Clear();
                    WelcolmeScreen();
                    Console.WriteLine($"We don't have book with title: '{book}' check spelling and try again. \nOr wait until admin add book like this :) ");
                }
            }
            else{
                Console.WriteLine("Library is empty, 'admin' should deliver new books or wait maybe some user will return book :)");
            }
        }

        private List<string> GetAvailableBooksToDeposit(string user, List<string> books){
            Console.Clear();
            WelcolmeScreen();
            Console.WriteLine("Books available to deposit:");
            foreach (Registry registry in database.Registry){
                if (registry.User == user){
                    Console.WriteLine(registry.Book);
                    books.Add(registry.Book);
                }
            }
            return books;
        }

        public void Deposit(string user){
            List<string> books = new List<string>();
            if (GetAvailableBooksToDeposit(user, books).Any()){
                Console.WriteLine("____________________________");
                Console.WriteLine("TYPE 'exit' to return => ...");
                Console.Write("Which book you want to return: ");
                string book = Console.ReadLine();
                if (book == "exit" || book == "EXIT" || book == "e"){
                    Console.Clear();
                    WelcolmeScreen();
                    return;
                }
                if (books.Contains(book)){

                    foreach (Registry registry in database.Registry.ToList()){

                        if (registry.Book == book){
                            database.Registry.Remove(registry);
                        }
                    }
                    OpenJsonFile();
                    foreach (Book b in (database.Books)){

                        if (b.Title == book){
                            b.Borrow = "No";
                        }
                    }
                    OpenJsonFile();
                    Console.Clear();
                    WelcolmeScreen();
                    Console.WriteLine($"{user} just deposited book: {book}");
                }
                else{

                    Console.WriteLine($"You don't have book: {book}");
                    Console.Write("Which book you want to return: ");
                    Deposit(user);
                }
            }
            else{

                Console.WriteLine("You don't have any book, please borrow some first :)");
            }
        }
        public bool IsUserRegistered(string login){
            foreach (User user in database.Users){
                if (login == user.Login){
                    return true;
                }
            }
            Console.WriteLine("We don't have user with that login, please register first");
            return false;
        }

        public bool UserLogIn(string login, string password){

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)){

                if (IsUserRegistered(login)){
                    foreach (User user in (database.Users)){

                        if (login == user.Login && password == user.Password){
                            Console.Clear();
                            WelcolmeScreen();
                            if (user.NewUser == "Yes"){
                                Console.WriteLine("Hello there! \nI see you first time here \nThank you for choosing my library! \nHave a NICE DAY!");
                                user.NewUser = "No";
                            }
                            else if (user.NewUser == "No"){
                                Console.WriteLine("Hello there! \nI See you again :) \nLike always => \nHave a NICE DAY!");
                            }
                            OpenJsonFile();
                            return true;
                        }
                        else if (login == user.Login && password != user.Password){
                            Console.WriteLine("Incorrect password, check spelling and try again!");
                        }
                    }
                }
                return false;
            }
            else if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(password)){
                Console.WriteLine("Username and password can't be empty!");
            }
            else if (string.IsNullOrEmpty(login)){
                Console.WriteLine("Username can't be empty!");
            }
            else if (string.IsNullOrEmpty(password)){
                Console.WriteLine("Password can't be empty!");
            }
            return false;
        }

        private bool IsPasswordAvailable(string userPassword){

            foreach (User user in database.Users){

                if (user.Password == userPassword){
                    return false;
                }
            }
            return true;
        }

        private bool IsLoginAvailable(string userName){

            foreach (User user in database.Users){

                if (user.Login == userName){
                    Console.WriteLine("This username is unavaliable...");
                    return false;
                }
            }
            return true;
        }

        public bool RegisterUser(string userName, string userPassword){

            List<string> users = new List<string>();
            if (IsLoginAvailable(userName) && IsPasswordAvailable(userPassword)){

                (database.Users).Add(new User { Login = userName, Password = userPassword, NewUser = "Yes" });
                OpenJsonFile();
                return true;
            }
            return false;
        }

        private bool CanBeAdded(string book){
            foreach (Book b in (database.Books)){
                if (b.Title == book){
                    return false;
                }
            }
            return true;
        }

        public void AddNewBook(){
            Console.Clear();
            WelcolmeScreen();
            Console.Write("Input book title: -to close input 'x' ");
            string bookName = Console.ReadLine();
            while (bookName.ToLower() != "x"){
                if (CanBeAdded(bookName)){

                    Console.Write("Input book author: ");
                    string bookAuthor = Console.ReadLine();
                    Console.Write("Input book pages: ");
                    string bookPages = Console.ReadLine();

                    (database.Books).Add(new Book { Title = bookName, Author = bookAuthor, Pages = bookPages, Borrow = "No" });
                    Console.WriteLine(JsonConvert.SerializeObject(database));
                    OpenJsonFile();

                    Console.WriteLine($"Title:'{bookName}', Author:'{bookAuthor}', Pages:'{bookPages}' - successfully added to Library");

                    AddNewBook();
                }
                else{
                    Console.WriteLine($"This book '{bookName}' is already in Library or has an invalid title");
                    AddNewBook();
                }
                Console.Clear();
                WelcolmeScreen();
                break;
            }
        }
    }
}