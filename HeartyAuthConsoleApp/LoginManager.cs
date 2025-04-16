using BCrypt.Net;
namespace HeartyAuthConsoleApp
{
    public class LoginManager
    {
        private readonly Dictionary<string, string> users = new();
        private const string UserFilePath = "users.txt";
        public LoginManager()
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            if (File.Exists(UserFilePath))
            {
                var lines = File.ReadAllLines(UserFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        users[parts[0]] = parts[1];
                    }
                }
            }
        }

        private void SaveUsers()
        {
            var lines = users.Select(kvp => $"{kvp.Key}:{kvp.Value}");
            File.WriteAllLines(UserFilePath, lines);
        }
        public void Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and password cannot be empty.");
                return;
            }

            if (password.Length < 8)
            {
                Console.WriteLine("Password must be at least 8 characters long.");
                return;
            }

            if (users.ContainsKey(username))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // Fully qualify the method
            users.Add(username, hashedPassword);
            SaveUsers(); // Save after registration
            Console.WriteLine("Registration successful!");
        }

        public void Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and password cannot be empty.");
                return;
            }

            if (users.TryGetValue(username, out string? storedHashedPassword))
            {
                if (BCrypt.Net.BCrypt.Verify(password, storedHashedPassword)) // Fully qualify the method
                {
                    Console.WriteLine("Login successful! Welcome, " + username);
                }
                else
                {
                    Console.WriteLine("Incorrect password.");
                }
            }
            else
            {
                Console.WriteLine("Username not found.");
            }
        }

        internal IEnumerable<string> GetRegisteredUsers()
        {
            return users.Keys;
        }
    }
}