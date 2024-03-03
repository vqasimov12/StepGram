namespace ConsoleApp2.Models;
public class User : Person
{
    public User(string _username, string _password, string _mail) : base(_username, _password, _mail)
    {

    }
    public User(string _username, string _password) : base(_username, _password)
    {
        UserMenu();
    }
    void DeletePost()
    {
        foreach (Post p in InstaStep.posts)
            if (p.User == Username)
                Console.WriteLine(p);
        Console.Write("\nEnter post id: ");
        string id = Console.ReadLine()!;
        int found = 0;
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
    void UserMenu()
    {
        string[] arr = new string[6] { "Show posts", "Add Post", "Delete your post", "Notifications", "Saved posts", "Exit" };
        int select = 0;
        while (true)
        {
            Console.Clear();
            InstaStep.Show(arr, select);
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.DownArrow)
            {
                select++;
                if (select == 6)
                    select = 0;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                select--;
                if (select == -1)
                    select = 5;
            }
            else if (key.Key == ConsoleKey.Escape)
                break;
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                if (select == 0)
                    ShowAllPosts();
                else if (select == 1)
                    AddPost();
                else if (select == 2)
                    DeletePost();

                else if (select == 3)
                    ShowNotification();

                else if (select == 4)
                    for (int i = 0; i < SavedPosts.Count; i++)
                        Console.WriteLine(SavedPosts[i]);

                else break;

                _ = Console.ReadKey(true);
            }
        }

    }

}
