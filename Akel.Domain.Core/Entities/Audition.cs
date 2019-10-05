using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Akel.Domain.Core
{
    public class Audition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
