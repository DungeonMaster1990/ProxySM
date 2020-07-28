using System.Runtime.Serialization;

namespace Common.Models.VmModels
{
    [DataContract]
    public class VmAuthorizeCredentials
    {
        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
