using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class LogTransaction
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string IpAddess { get; set; }
        public string IdPk { get; set; }
        public string TypeTransaction { get; set; }
        public string UserTransaction { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
