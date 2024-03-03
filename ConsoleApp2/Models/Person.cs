using System.Net.Mail;
using System.Net;

namespace ConsoleApp2.Models;
public class Person
{
    public List<Post> SavedPosts = new();
    public List<Notification> Notifications = new List<Notification>();
    public Person(string _username, string _password)
    {
        Username = _username;
        Password = _password;
    }
    public Person(string _username, string _password, string _mail)
    {
        Username = _username;
        Password = _password;
        Mail = _mail;
    }
    private string username;
    private string password;
    private string mail;
    public static int OTPCode;
    public string Username
    {
        get { return username; }
        set
        {
            if (value.Length < 4)
                throw new Exception("Username length should contain min 4 character");
            username = value;
        }
    }
    public string Password
    {
        get { return password; }
        set
        {
            int count_U = 0;
            int count_L = 0;
            int count_other = 0;
            foreach (var ch in value)
            {
                if (ch >= (char)65 && ch <= (char)90)
                    count_U++;
                else if (ch >= (char)97 && ch <= (char)122)
                    count_L++;
                else
                    count_other++;

            }
            password = value;
        }
    }
    public string Mail
    {
        get { return mail; }
        set
        {
            if (value.Length < 14)
                throw new Exception("Email should contain min 14 character");
            if (!value.EndsWith("@gmail.com"))
                throw new Exception("Mail should end with \"@gmail.com\"");
            mail = value;
        }
    }
    public static void SendMail(string _destinationMail)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("qasimov.vaqif512@gmail.com");
            mail.To.Add(_destinationMail);
            mail.Subject = "**** Step Gram ****";
            Random rand = new Random();
            OTPCode = rand.Next(1000, 10000);
            mail.Body = OTPCode.ToString();
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("qasimov.vaqif512@gmail.com", "mnnc lpwi nzua ocjg");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            Console.WriteLine("Mail sent successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public override string ToString()
    {
        return $@" Username: {Username}
 Email: {Mail}";
    }
    public static void Print(int select)
    {
        Console.Clear();
        if (InstaStep.posts.Count != 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                    ↑\n\n");
            if (select % 2 == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(InstaStep.posts[select]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                    ↓\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press Enter to like or save\nEscape for to go back");
        }
        else Console.WriteLine("No posts found");
    }
    protected void ShowNotification()
    {
        foreach(var n in Notifications)
            Console.WriteLine(n);
    }
    public void ShowAllPosts()
    {
        int select = 0;
        Console.Clear();
        if (InstaStep.posts.Count == 0)
            return;
        InstaStep.posts[select].ViewCount++;
        Print(select);
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (select != InstaStep.posts.Count - 1)
                    select++;
                else
                    continue;
                InstaStep.posts[select].ViewCount++;

            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (select != 0)
                    select--;
                else
                    continue;
                InstaStep.posts[select].ViewCount++;
            }
            else if (key.Key == ConsoleKey.Escape)
                break;
            else if (key.Key == ConsoleKey.Enter)
            {
                if (InstaStep.posts.Count != 0)
                {
                    Console.WriteLine("Press 'S' to Save\nPress 'L' to Like");
                    string? action = Console.ReadLine();
                    action = action?.ToUpper();
                    if (action == "L" || action == "l")
                    {
                        Notification n = new Notification(Username, InstaStep.posts[select].Id);
                        InstaStep.posts[select].LikeCount++;

                        if (InstaStep.posts[select].User == "Admin")
                            InstaStep.admin.Notifications.Add(n);
                        else
                        {
                            bool userFound = false;
                            foreach (Person user in InstaStep.users)
                                if (user.Username == InstaStep.posts[select].User)
                                {
                                    user.Notifications.Add(n);
                                    userFound = true;
                                    break;
                                }
                            if (!userFound)
                                Console.WriteLine("Error: User who made the post not found.");
                        }
                    }
                    else if (action == "S" || action == "s")
                    {
                        SavedPosts.Add(InstaStep.posts[select]);
                        InstaStep.posts[select].SaveCount++;
                    }
                    else
                    {
                        Console.WriteLine("Wrong action");
                        Console.ReadKey(true);
                    }
                }

            }
            Console.Clear();
            Print(select);
        }
    }
    public void AddPost()
    {
        while (true)
        {
            Console.Write("Enter post content: ");
            string content = Console.ReadLine()!;
            if (content.Length < 1)
            {
                Console.WriteLine("Content should contain min 1 character");
                continue;
            }
            Post p = new(Username, content);
            InstaStep.posts.Add(p);
            break;
        }
    }
}
