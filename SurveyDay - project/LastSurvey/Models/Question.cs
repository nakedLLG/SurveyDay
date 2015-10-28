namespace LastSurvey.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int QuestionID { get; set; }

        public int? SurveyID { get; set; }

        public int? QuestionTypeID { get; set; }


        [Column("Question")]
        [Required]
        [StringLength(100)]
        [Display(Name="Question")]
        public string Question1 { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        
        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public virtual Survey Survey { get; set; }
    }
}
