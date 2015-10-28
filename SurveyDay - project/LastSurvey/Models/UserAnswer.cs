namespace LastSurvey.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAnswer")]
    public partial class UserAnswer
    {
        public int UserAnswerID { get; set; }

        public int? AnswerID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SubmitDate { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual User User { get; set; }

        public bool matchDate(DateTime date)
        {
            return this.SubmitDate.Value == date;
        }

        public bool matchUserID(int userId)
        {
            return userId == (int) this.UserID;
        }
    }
}
