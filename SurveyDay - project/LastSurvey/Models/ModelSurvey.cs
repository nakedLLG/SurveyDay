namespace LastSurvey.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
   

    public partial class ModelSurvey : DbContext
    {
        public ModelSurvey()
            : base("name=ModelSurvey")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .Property(e => e.Answer1)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Question1)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.MaxValue);


            modelBuilder.Entity<Question>()
                .Property(e => e.MinValue);
                

            modelBuilder.Entity<Question>()
                .Property(e => e.SurveyID);
                

            modelBuilder.Entity<QuestionType>()
                .Property(e => e.QuestionType1)
                .IsUnicode(false);

            modelBuilder.Entity<Survey>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordSalt)
                .IsUnicode(false);
        }
    }
}
