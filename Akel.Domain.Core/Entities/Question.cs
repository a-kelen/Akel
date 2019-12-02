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
 
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string Title { get; set; }
        public int Correct { get; set; }
        public Test Test { get; set; }
        public virtual List<Answer> Answers { get; set; }

    }
}
