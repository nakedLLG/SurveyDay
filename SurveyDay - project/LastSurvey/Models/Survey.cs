namespace LastSurvey.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Survey")]
    public partial class Survey
    {
        public Survey()
        {
            Questions = new HashSet<Question>();
        }

        public int SurveyID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name="Start Date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
