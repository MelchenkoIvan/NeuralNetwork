using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuralNetworkDatabase.Entities
{
	public class User
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserIdentity { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsActive { get; set; } = false;
        
        public ICollection<Symptoms> Symptoms { get; set; }
	}
}

