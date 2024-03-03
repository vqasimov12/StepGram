using ConsoleApp2.Models;
try
{
    InstaStep a = new();
    a.MenuRegistration();
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
