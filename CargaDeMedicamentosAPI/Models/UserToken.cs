using System;

namespace CargaDeMedicamentosAPI.Models
{
    public class UserToken
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
