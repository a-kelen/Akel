using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Akel.Domain.Core
{
    public class UserProfile
    {
        public UserProfile()
        {
            Auditions = new List<Audition>();
            Friends = new List<Friend>();
            Comments = new List<Comment>();
            Results = new List<Result>();
            Subscribers = new List<Subscriber>();
            Members = new List<Member>();
            Messages = new List<Message>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool Sex { get; set; }
        public byte[] Avatar { get; set; }
        public Guid UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<Audition> Auditions { get; set; }
        public List<Friend> Friends { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Result> Results { get; set; }
        public List<Subscriber> Subscribers { get; set; }
        public List<Member> Members { get; set; }
        public List<Message> Messages { get; set; }
    }
}
