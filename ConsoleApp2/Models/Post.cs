namespace ConsoleApp2.Models;
public class Post
{
    public DateTime Date;
    public string User;
    public int LikeCount = 0;
    public int ViewCount = 0;
    public int SaveCount = 0;
    public Guid Id;
    public string Content;
    public Post(string _username, string _content)
    {
        Content = _content;
        User = _username;
        Id = Guid.NewGuid();
    }
    public override string ToString()
    {
        return $@" {User}: {Content} 
    Likes: {LikeCount}   Views: {ViewCount}   Saved: {SaveCount}
    {DateTime.Now.Hour - Date.Hour}:{DateTime.Now.Minute - Date.Minute} 
    Id:{Id.ToString()}

";
    }
}
