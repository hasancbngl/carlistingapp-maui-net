using System;
using Newtonsoft.Json;

namespace CarListingAppDemoMaui.Model
{
    public class LoginResponseDto
    {
#nullable enable
        [JsonProperty("data", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public User? data { get; set; } = null;
        public bool success { get; set; }
        public string? message { get; set; }
    }
}

public class User
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Username { get; set; }
    public string? Token { get; set; }
}

