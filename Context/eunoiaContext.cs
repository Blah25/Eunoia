using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eunoia
{
    public partial class eunoiaContext : DbContext
    {
        public eunoiaContext()
        {
        }

        public eunoiaContext(DbContextOptions<eunoiaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EunoiaAssessment> EunoiaAssessment { get; set; }
        public virtual DbSet<EunoiaAssessmentAnswer> EunoiaAssessmentAnswer { get; set; }
        public virtual DbSet<EunoiaAssessmentResult> EunoiaAssessmentResult { get; set; }
        public virtual DbSet<EunoiaInterpretation> EunoiaInterpretation { get; set; }
        public virtual DbSet<EunoiaMood> EunoiaMood { get; set; }
        public virtual DbSet<EunoiaOrganization> EunoiaOrganization { get; set; }
        public virtual DbSet<EunoiaQuestion> EunoiaQuestion { get; set; }
        public virtual DbSet<EunoiaQuestionMapping> EunoiaQuestionMapping { get; set; }
        public virtual DbSet<EunoiaRespondent> EunoiaRespondent { get; set; }
        public virtual DbSet<EunoiaSeverity> EunoiaSeverity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;database=eunoia;port=3306;user=root;password=root;treattinyasboolean=true", x => x.ServerVersion("8.0.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<EunoiaAssessment>(entity =>
            {
                entity.HasKey(e => e.AssessmentId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_assessment");

                entity.HasIndex(e => e.RespondentId)
                    .HasName("Resp_to_Assess(FKEY1)_idx");

                entity.Property(e => e.AssessmentId)
                    .HasColumnName("assessmentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssessmentDate)
                    .HasColumnName("assessmentDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.RespondentId)
                    .IsRequired()
                    .HasColumnName("respondentID")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Respondent)
                    .WithMany(p => p.EunoiaAssessment)
                    .HasForeignKey(d => d.RespondentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Resp_to_Assess(FKEY1)");
            });

            modelBuilder.Entity<EunoiaAssessmentAnswer>(entity =>
            {
                entity.HasKey(e => e.AnsDetailId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_assessment_answer");

                entity.HasIndex(e => e.AssessmentId)
                    .HasName("Assess_to_AnsDet(FK1)_idx");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("Question_to_AnsDet(FK2)_idx");

                entity.Property(e => e.AnsDetailId)
                    .HasColumnName("ansDetailID")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AssessmentId).HasColumnName("assessmentID");

                entity.Property(e => e.QuestionId).HasColumnName("questionID");

                entity.Property(e => e.QuestionScore).HasColumnName("questionScore");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.EunoiaAssessmentAnswer)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Assess_to_AnsDet(FK1)");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.EunoiaAssessmentAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Question_to_AnsDet(FK2)");
            });

            modelBuilder.Entity<EunoiaAssessmentResult>(entity =>
            {
                entity.HasKey(e => e.ResultId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_assessment_result");

                entity.HasIndex(e => e.AssessmentId)
                    .HasName("Assess_to_Result(FK1)_idx");

                entity.HasIndex(e => e.InterpreId)
                    .HasName("Interpre_to_ResultI(FK3)_idx");

                entity.HasIndex(e => e.MoodId)
                    .HasName("Mood_to_Result(FK2)_idx");

                entity.Property(e => e.ResultId)
                    .HasColumnName("resultID")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AssessmentId).HasColumnName("assessmentID");

                entity.Property(e => e.InterpreId)
                    .IsRequired()
                    .HasColumnName("InterpreID")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MoodId).HasColumnName("moodID");

                entity.Property(e => e.TotalScore).HasColumnName("totalScore");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.EunoiaAssessmentResult)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Assess_to_Result(FK1)");

                entity.HasOne(d => d.Interpre)
                    .WithMany(p => p.EunoiaAssessmentResult)
                    .HasForeignKey(d => d.InterpreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Interpre_to_ResultI(FK3)");

                entity.HasOne(d => d.Mood)
                    .WithMany(p => p.EunoiaAssessmentResult)
                    .HasForeignKey(d => d.MoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mood_to_Result(FK2)");
            });

            modelBuilder.Entity<EunoiaInterpretation>(entity =>
            {
                entity.HasKey(e => e.InterpreId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_interpretation");

                entity.HasIndex(e => e.MoodId)
                    .HasName("Mood_to_Inter(FKEY1)_idx");

                entity.HasIndex(e => e.SeverityId)
                    .HasName("Sever_to_Inter(FKEY2)_idx");

                entity.Property(e => e.InterpreId)
                    .HasColumnName("InterpreID")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MoodId).HasColumnName("MoodID");

                entity.Property(e => e.Quote)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SeverityId).HasColumnName("SeverityID");

                entity.Property(e => e.Tips)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Mood)
                    .WithMany(p => p.EunoiaInterpretation)
                    .HasForeignKey(d => d.MoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mood_to_Inter(FKEY1)");

                entity.HasOne(d => d.Severity)
                    .WithMany(p => p.EunoiaInterpretation)
                    .HasForeignKey(d => d.SeverityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sever_to_Inter(FKEY2)");
            });

            modelBuilder.Entity<EunoiaMood>(entity =>
            {
                entity.HasKey(e => e.MoodId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_mood");

                entity.HasIndex(e => e.MoodName)
                    .HasName("MoodName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MoodId)
                    .HasColumnName("MoodID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MoodName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<EunoiaOrganization>(entity =>
            {
                entity.HasKey(e => e.OrganizationId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_organization");

                entity.Property(e => e.OrganizationId)
                    .HasColumnName("organizationID")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.OrganizationName)
                    .IsRequired()
                    .HasColumnName("organizationName")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<EunoiaQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_question");

                entity.HasIndex(e => e.MoodId)
                    .HasName("Mood-to-Ques(FKEY1)_idx");

                entity.HasIndex(e => e.QuestionDesc)
                    .HasName("QuestionDesc_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.MoodId).HasColumnName("MoodID");

                entity.Property(e => e.QuestionDesc)
                    .IsRequired()
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Mood)
                    .WithMany(p => p.EunoiaQuestion)
                    .HasForeignKey(d => d.MoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mood-to-Ques(FKEY1)");
            });

            modelBuilder.Entity<EunoiaQuestionMapping>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_question_mapping");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("QuestionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.QuestionColId)
                    .HasColumnName("Question_Col_ID")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Question)
                    .WithOne(p => p.EunoiaQuestionMapping)
                    .HasForeignKey<EunoiaQuestionMapping>(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("QuestionID");
            });

            modelBuilder.Entity<EunoiaRespondent>(entity =>
            {
                entity.HasKey(e => e.RespondentId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_respondent");

                entity.HasIndex(e => e.RespondentEmail)
                    .HasName("respondentEmail_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RespondentFullName)
                    .HasName("respondentFullName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RespondentPassHash)
                    .HasName("respondentPassHash_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.RespondentId)
                    .HasColumnName("respondentID")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.OrganizationName)
                    .HasColumnName("organizationName")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RespondentAge).HasColumnName("respondentAge");

                entity.Property(e => e.RespondentDepartment)
                    .IsRequired()
                    .HasColumnName("respondentDepartment")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RespondentEmail)
                    .IsRequired()
                    .HasColumnName("respondentEmail")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RespondentFullName)
                    .IsRequired()
                    .HasColumnName("respondentFullName")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RespondentGender)
                    .IsRequired()
                    .HasColumnName("respondentGender")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RespondentPassHash)
                    .IsRequired()
                    .HasColumnName("respondentPassHash")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RespondentPosition)
                    .IsRequired()
                    .HasColumnName("respondentPosition")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<EunoiaSeverity>(entity =>
            {
                entity.HasKey(e => e.SeverityId)
                    .HasName("PRIMARY");

                entity.ToTable("eunoia_severity");

                entity.HasIndex(e => e.SeverityName)
                    .HasName("severityName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SeverityId)
                    .HasColumnName("severityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SeverityName)
                    .IsRequired()
                    .HasColumnName("severityName")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
