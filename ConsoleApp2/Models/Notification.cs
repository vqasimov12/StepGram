namespace ConsoleApp2.Models;
public class Notification
{
    DateTime Time;
    string _user;
    Guid Id;
    public Notification(string _username,Guid _id)
    {
        _user = _username;
        Time = DateTime.Now;
        Id = _id;
    }
    public override string ToString()
    {
        return $@"{_user} liked your post 
 {DateTime.Now.Hour-Time.Hour} hours {DateTime.Now.Minute-Time.Minute} minutes {DateTime.Now.Second - Time.Second} seconds ago 
 Post id: {Id}
";
    }
}
