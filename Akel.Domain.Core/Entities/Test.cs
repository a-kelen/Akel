using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akel.Domain.Core
{
    public class Test
    {
        public Test()
        {
            Questions = new List<Question>();
            Results = new List<Result>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public Guid Id { get; set; }
        public Guid AuditionId { get; set; }
        public Audition Audition { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public List<Question> Questions { get; set; }
        public List<Result> Results { get; set; }

    }
}
