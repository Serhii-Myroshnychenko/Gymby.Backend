using System.Text;

namespace Gymby.Application.Utils;

public static class UsernameHandler
{
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GenerateUsername()
    {
        var rand = new Random();
        var username = new StringBuilder("user_");
        for (var i = 0; i < rand.Next(6, 6); i++)
        {
            username.Append(AllowedChars[rand.Next(AllowedChars.Length)]);
        }
        return username.ToString();
    }
}
