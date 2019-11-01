using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akel.Domain.Core
{
    public class Chat
    {
        public Chat()
        {
            Users = new List<UserProfileChat>();
            Messages = new List<Message>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid AuditionId { get; set; }
        public Audition Audition { get; set; }
        public List<UserProfileChat> Users { get; set; }
        public List<Message> Messages { get; set; }
        


    }
}
