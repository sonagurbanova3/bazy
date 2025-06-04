using Microsoft.EntityFrameworkCore;
using Projekt_sbd.Models;



namespace Projekt_sbd.Data
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }

        public DbSet<ClassType> ClassTypes { get; set; }
        public DbSet<ClassGroup> ClassGroups { get; set; }
        public DbSet<GradeCategory> GradeCategories { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentRule> AssessmentRules { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<GradeHistory> GradeHistory { get; set; }
        public DbSet<StudentGroupEnrollment> StudentGroupEnrollments { get; set; }
        public DbSet<StudentRanking> StudentRanking { get; set; }
        public DbSet<SredniePrzedmioty> SredniePrzedmioty { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Główne tabele
            modelBuilder.Entity<Student>().ToTable("STUDENTS").HasKey(s => s.IdStudent);
            modelBuilder.Entity<Teacher>().ToTable("TEACHERS").HasKey(t => t.IdTeacher);
            modelBuilder.Entity<Subject>()
    .HasOne(s => s.Teacher)
    .WithMany(t => t.Subjects)
    .HasForeignKey(s => s.IdTeacher);
            modelBuilder.Entity<Grade>().ToTable("GRADES").HasKey(g => g.IdGrade);
            modelBuilder.Entity<ClassType>().ToTable("CLASS_TYPES").HasKey(ct => ct.IdClassType);
            modelBuilder.Entity<ClassGroup>().ToTable("CLASS_GROUPS").HasKey(cg => cg.IdGroup);
            modelBuilder.Entity<GradeCategory>().ToTable("GRADE_CATEGORIES").HasKey(gc => gc.IdCategory);
            modelBuilder.Entity<Assessment>().ToTable("ASSESSMENTS").HasKey(a => a.IdAssessment);
            modelBuilder.Entity<AssessmentRule>().ToTable("ASSESSMENT_RULES").HasKey(ar => ar.IdRule);
            modelBuilder.Entity<GradeHistory>().ToTable("GRADE_HISTORY").HasKey(h => h.IdHistory);
            modelBuilder.Entity<StudentRanking>().HasNoKey().ToView("V_RANKING_STUDENTOW");
            modelBuilder.Entity<SredniePrzedmioty>().HasNoKey().ToView("V_SREDNIE_PRZEDMIOTY");
           
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id).HasColumnName("ID");
                entity.Property(u => u.Username).HasColumnName("USERNAME");
                entity.Property(u => u.PasswordHash).HasColumnName("PASSWORD_HASH");
                entity.Property(u => u.Role).HasColumnName("ROLE");
            });




            modelBuilder.Entity<LoginLog>().ToTable("LOGIN_LOGS").HasKey(l => l.Id);

            // możesz dodać ograniczenie roli:
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Klucze złożone
            modelBuilder.Entity<StudentResult>()
                .ToTable("STUDENT_RESULTS")
                .HasKey(sr => new { sr.IdStudent, sr.IdSubject });

            modelBuilder.Entity<StudentGroupEnrollment>()
                .ToTable("STUDENT_GROUP_ENROLLMENT")
                .HasKey(e => new { e.IdStudent, e.IdGroup });

            // Relacje - GRADES
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.IdStudent);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.ClassGroup)
                .WithMany(cg => cg.Grades)
                .HasForeignKey(g => g.IdGroup);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.GradeCategory)
                .WithMany(gc => gc.Grades)
                .HasForeignKey(g => g.IdCategory);

            // SUBJECT -> TEACHER (przez ClassGroup)
            modelBuilder.Entity<ClassGroup>()
      .HasOne(cg => cg.Subject)
      .WithMany(s => s.ClassGroups)
      .HasForeignKey(cg => cg.IdSubject);


            modelBuilder.Entity<ClassGroup>()
                .HasOne(cg => cg.Teacher)
                .WithMany()
                .HasForeignKey(cg => cg.IdTeacher);

            modelBuilder.Entity<ClassGroup>()
      .HasOne(cg => cg.ClassType)
      .WithMany(ct => ct.ClassGroups)
      .HasForeignKey(cg => cg.IdClassType);

            // ASSESSMENT
            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.ToTable("ASSESSMENTS");
                entity.HasKey(a => a.IdAssessment);

                entity.Property(a => a.IdAssessment).HasColumnName("ID_ASSESSMENT");
                entity.Property(a => a.IdGroup).HasColumnName("ID_GROUP");
                entity.Property(a => a.IdCategory).HasColumnName("ID_CATEGORY");
                entity.Property(a => a.DataOceny).HasColumnName("DATA_OCENY");
                entity.Property(a => a.Opis).HasColumnName("OPIS");

                entity.HasOne(a => a.Group)
                      .WithMany()
                      .HasForeignKey(a => a.IdGroup);

                entity.HasOne(a => a.Category)
                      .WithMany()
                      .HasForeignKey(a => a.IdCategory);
            });

            // ASSESSMENT_RULE
            modelBuilder.Entity<AssessmentRule>()
                .HasOne(ar => ar.Subject)
                .WithMany()
                .HasForeignKey(ar => ar.IdSubject);

            modelBuilder.Entity<AssessmentRule>()
                .HasOne(ar => ar.ClassType)
                .WithMany()
                .HasForeignKey(ar => ar.IdClassType);

            // STUDENT_RESULT
            modelBuilder.Entity<StudentResult>()
                .HasOne(sr => sr.Student)
                .WithMany()
                .HasForeignKey(sr => sr.IdStudent);

            modelBuilder.Entity<StudentResult>()
                .HasOne(sr => sr.Subject)
                .WithMany()
                .HasForeignKey(sr => sr.IdSubject);

            // STUDENT_GROUP_ENROLLMENT
            modelBuilder.Entity<StudentGroupEnrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.IdStudent);

            modelBuilder.Entity<StudentGroupEnrollment>()
                .HasOne(e => e.Group)
                .WithMany()
                .HasForeignKey(e => e.IdGroup);

            // GRADE_HISTORY
            modelBuilder.Entity<GradeHistory>()
     .HasOne(h => h.Grade)
     .WithMany()
     .HasForeignKey(h => h.IdGrade);

        }

    }
}
