namespace ConsoleApp2.Models;
public class InstaStep
{
    public static List<Post> posts = new();
    public static List<Person> users = new();
    public static AdminClass admin= new ();
    public static void Show(string[] arr, int select)
    {
        for (int i = 0; i < arr.Length; i++)
            if (i == select)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else Console.WriteLine(arr[i]);
    }
    public static bool Exist(string name)
    {
        string temp = name.ToLower();
        foreach (Person user in users)
            if (user.Username == name || temp == "admin")
                return true;
        return false;
    }
    bool CheckUser(string username, string password)
    {
        foreach (Person user in users)
            if (user.Username == username && user.Password == password)
                return true;
        return false;
    }
    void SignUP()
    {
        Console.Clear();
        while (true)
        {
            try
            {
                Console.Write("Enter your Username: ");
                string username = Console.ReadLine()!;
                Console.Write("Enter your Password:");
                string password = Console.ReadLine()!;
                Console.Write("Enter your Email: ");
                string email = Console.ReadLine()!;
                if (Exist(username))
                {
                    Console.WriteLine("This username has already been used");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
                Person a = new(username, password, email);
                Person.SendMail(email);
                int k = 0;
                while (k < 3)
                {
                    Console.Write("Enter your OTP Code that send to your mail: ");
                    int code = Convert.ToInt32(Console.ReadLine());
                    if (Person.OTPCode == code)
                    {
                        users.Add(a);
                        break;
                    }
                    Console.WriteLine("Otp code is not correct");
                    k++;
                    if (k == 3)
                    {
                        Console.WriteLine("Your sesseion has expired You are blocked!!");
                        return;
                    }
                }
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
    
    void SignIn()
    {
        Console.Clear();
        Console.Write("Enter your Username: ");
        string username = Console.ReadLine()!;
        Console.Write("Enter your Password:");
        string password = Console.ReadLine()!;
        if (username == "Admin" && password == "Admin123")
            admin.AdminMenu();
        else if (Exist(username!))
        {
            if (CheckUser(username!, password!))
                _ = new User(username, password);
            else
            {
                Console.WriteLine("Incorrect password");
                Console.ReadKey(true);
            }
        }
        else
        {
            Console.WriteLine("Invalid Username");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadKey(true);
        }
        Console.Clear();
    }
    public void MenuRegistration()
    {
        string[] arr = new string[3] { "Sign In", "Sign Up", "Exit" };
        int select = 0;
        while (true)
        {
            Console.Clear();
            Show(arr, select);
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.UpArrow)
            {
                select--;
                if (select == -1)
                    select = 2;
            }

            else if (key.Key == ConsoleKey.DownArrow)
            {
                select++;
                if (select == 3)
                    select = 0;
            }

            else if (key.Key == ConsoleKey.Escape)
                break;
            else if (key.Key == ConsoleKey.Enter)
            {
                if (select == 0)
                    SignIn();
                else if (select == 1)
                    SignUP();
                else break;
            }
        }
    }
}

