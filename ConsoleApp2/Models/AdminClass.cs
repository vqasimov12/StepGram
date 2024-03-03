using ConsoleApp2.Models;
using System.Transactions;

namespace ConsoleApp2.Models;
public class AdminClass : Person
{
    public AdminClass() : base("Admin", "Admin123", "Admin@gmail.com")
    {
    }
    public void AdminMenu()
    {
        string[] arr = new string[8] { "Show all users", "Show all posts", "Delete post", "Delete user", "Add post", "Notifications", "Saved posts", "Exit" };
        int select = 0;
        while (true)
        {
            Console.Clear();
            InstaStep.Show(arr, select);
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.DownArrow)
            {
                select++;
                if (select == 8)
                    select = 0;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                select--;
                if (select == -1)
                    select = 7;
            }
            else if (key.Key == ConsoleKey.Escape)
                return;
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                if (select == 0)
                    foreach (Person user in InstaStep.users)
                        Console.WriteLine(user);
                else if (select == 1)
                    ShowAllPosts();

                else if (select == 2)
                {
                    Console.Write("Enter post id: ");
                    string id = Console.ReadLine()!;
                    int found = -1;
                    if (InstaStep.posts.Count == 0) found = 0;
                    for (int i = 0; i < InstaStep.posts.Count; i++)
                        if (InstaStep.posts[i].Id.ToString() == id)
                        {
                            found = 1;
                            InstaStep.posts.RemoveAt(i);
                            for (int k = 0; k < SavedPosts.Count; k++)
                                if (SavedPosts[i].Id.ToString() == id)
                                    SavedPosts.RemoveAt(k);
                        }
                    if (found == 0)
                        Console.WriteLine("Wrong ID");
                }

                else if (select == 3)
                {
                    Console.Write("Enter User Name: ");
                    string name = Console.ReadLine()!;
                    int found = 0;
                    if (InstaStep.Exist(name))
                        for (int i = 0; i < InstaStep.users.Count; i++)
                            if (InstaStep.users[i].Username == name)
                            {
                                found = 1;
                                InstaStep.users.RemoveAt(i);
                            }
                    if (found == 0)
                        Console.WriteLine("Wrong Username or Invalid username");
                }

                else if (select == 4)
                    AddPost();

                else if (select == 5)
                        for (int i = 0; i < Notifications.Count; i++)
                            Console.WriteLine(Notifications[i]);

                else if (select == 6)
                {
                    if (SavedPosts.Count != 0)
                        for (int i = 0; i < SavedPosts.Count; i++)
                            Console.WriteLine(SavedPosts[i]);
                }

                else return;

                _ = Console.ReadKey(true);
            }
        }
    }

}
