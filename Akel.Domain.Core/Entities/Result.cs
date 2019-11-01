using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akel.Domain.Core
{
    public class Result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
        public Guid? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
