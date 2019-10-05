using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akel.Domain.Core
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public Audition Audition { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
