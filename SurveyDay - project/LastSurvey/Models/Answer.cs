namespace LastSurvey.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        public Answer()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }

        public Answer(int QuestionID, string answer1)
        {
            this.QuestionID = QuestionID;
            this.Answer1 = answer1;

            UserAnswers = new HashSet<UserAnswer>();
        }

        public int AnswerID { get; set; }

        public int? QuestionID { get; set; }

        [Column("Answer")]
        [Required]
        [StringLength(50)]
        public string Answer1 { get; set; }

        public virtual Question Question { get; set; }

        [Display(Name="UserAnswer")]
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
