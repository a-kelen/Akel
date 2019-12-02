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
        public Guid Id { get; set; }
        public Guid AuditionId { get; set; }
        public Audition Audition { get; set; }
        public string Text { get; set; }
        public int LikesCount { get; set; }
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }
        public List<Comment> Comments { get; set; }
        
    }
}
