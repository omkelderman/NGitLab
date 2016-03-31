using System.Runtime.Serialization;

namespace NGitLab.Models
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember(Name = "login")]
        public string Login;

        [DataMember(Name = "email")]
        public string Email;

        [DataMember(Name = "password")]
        public string Password;
    }
}
