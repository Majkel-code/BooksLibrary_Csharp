namespace LibraryManagementSystem{

    public class Book{
        public string Title { get; set; }
        public string Author { get; set; }
        public string Pages { get; set; }
        public string Borrow { get; set; }
    }

    public class User{
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewUser { get; set; }
    }

    public class Registry{
        public string User { get; set; }
        public string Book { get; set; }
    }
    public class MyDatabase{
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }
        public List<Registry> Registry { get; set; }
    }
}