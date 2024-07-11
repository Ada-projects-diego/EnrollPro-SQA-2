using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentEnrolmentNew.src.Models;

namespace StudentEnrolmentNew.src.Data
{
    public class StudentEnrolmentContext : DbContext, IStudentEnrolmentContext
    {
        public StudentEnrolmentContext(DbContextOptions<StudentEnrolmentContext> options)
            : base(options)
        {
        }

        public DbSet<Students> Student { get; set; }
        public DbSet<Courses> Course { get; set; }
        public DbSet<Subjects> Subject { get; set; }
        public DbSet<CourseMembership> CourseMemberships { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public new EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.HasKey(e => e.StudentId);
            });

            modelBuilder.Entity<Courses>(entity =>
            {
                entity.Property(e => e.CourseName).IsRequired();
                entity.Property(e => e.CourseDescription).IsRequired();
                entity.HasKey(e => e.CourseId);

            });

            modelBuilder.Entity<Subjects>(entity =>
            {

                entity.Property(e => e.SubjectName).IsRequired();
                entity.Property(e => e.SubjectDescription).IsRequired();
                entity.HasKey(e => e.SubjectId);
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Subjects);
            });

            modelBuilder.Entity<CourseMembership>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<CourseMembership>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.CourseMemberships)
                .HasForeignKey(sc => sc.StudentId);


            modelBuilder.Entity<CourseMembership>()
                .HasOne(sc => sc.Course)
                .WithMany(s => s.CourseMemberships)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
