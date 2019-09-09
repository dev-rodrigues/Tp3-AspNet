using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteDePerformance.Models {
    public class Pessoa {
        public int Id { get; set; }

        [Display (Name = "Name")]

        public string Nome { get; set; }

        public string SobreNome { get; set; }

    }
}