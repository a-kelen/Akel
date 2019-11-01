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
        
        public Guid Id { get; set; }
        public Guid AuditionId { get; set; }
        public Audition Audition { get; set; }
        public Guid? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
