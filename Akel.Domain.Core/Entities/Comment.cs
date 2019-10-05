using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akel.Domain.Core
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
