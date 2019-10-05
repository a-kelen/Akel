using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akel.Domain.Core
{
    public class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
        public List<Answer> Answers { get; set; }

    }
}
