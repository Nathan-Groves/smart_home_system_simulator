using System.Threading.Tasks;

public static class AuthService
{
    public static async Task<bool> Login(string email, string password)
    {
        // Replace with actual authentication logic (e.g., API call, database check)
        await Task.Delay(1000); // Simulate network delay
        return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
    }
}