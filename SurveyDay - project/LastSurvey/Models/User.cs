namespace LastSurvey.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="User Name")]
        public string UserNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        [StringLength(50)]
        public string PasswordSalt { get; set; }

        public virtual ICollection<UserAnswer> UserAnswers { get; set; }

        
    }
}
