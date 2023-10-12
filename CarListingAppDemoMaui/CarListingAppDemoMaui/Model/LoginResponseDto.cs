using System;
namespace CarListingAppDemoMaui.Model
{
    public class LoginResponseDto
    {
        public User data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Username { get; set; }
    public required string Token { get; set; }
}

