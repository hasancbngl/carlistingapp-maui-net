using System;
namespace CarListingAppDemoMaui.Model
{
    public class RegisterResponse
    {
        public Boolean data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

