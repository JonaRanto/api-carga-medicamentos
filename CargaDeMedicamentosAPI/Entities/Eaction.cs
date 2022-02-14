using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class Eaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public string Group { get; set; }
    }
}
