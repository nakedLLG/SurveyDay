namespace LastSurvey.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionType")]
    public partial class QuestionType
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }

        public int QuestionTypeID { get; set; }

        [Column("QuestionType")]
        [Required]
        [StringLength(50)]
        [Display(Name="Question Type")]
        public string QuestionType1 { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
