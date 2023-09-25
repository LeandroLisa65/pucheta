using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion.EF.Models
{

    public class Test
    {
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}

