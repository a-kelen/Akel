using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Akel.Domain.Core
{
    public class Subscriber
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
