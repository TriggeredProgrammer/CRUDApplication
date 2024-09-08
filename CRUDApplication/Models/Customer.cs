using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDApplication.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain alphabets.")]
        public string Name { get; set; }

        [Required]
        [Range(0, 255, ErrorMessage = "GenderId must be a valid byte number.")]
        public byte GenderId { get; set; }

        [Required]
        [Range(0, 32767, ErrorMessage = "StateId must be a valid small integer.")]
        public short StateId { get; set; }

        [Required]
        [Range(0, 32767, ErrorMessage = "DistrictId must be a valid small integer.")]
        public short DistrictId { get; set; }
    }
}


