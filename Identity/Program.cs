using Identity;
using Microsoft.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        WebHost.CreateDefaultBuilder(args).
            UseStartup<Startup>()
            .Build().Run();
    }
}