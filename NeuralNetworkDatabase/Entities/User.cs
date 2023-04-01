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
        public int Username { get; set; }

        [Required]
        public int Password { get; set; }
	}
}

