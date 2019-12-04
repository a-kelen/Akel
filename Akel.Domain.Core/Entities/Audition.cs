using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Akel.Domain.Core
{
    public class Audition
    {
        public Audition()
        {
            Posts = new List<Post>();
            Subscribers = new List<Subscriber>();
            Tests = new List<Test>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<Post> Posts { get; set; }
        public List<Test> Tests { get; set; }
        public List<Subscriber> Subscribers { get; set; }
        public Chat Chat { get; set; }

    }
}
