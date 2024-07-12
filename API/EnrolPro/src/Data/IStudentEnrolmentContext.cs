using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentEnrolmentNew.src.Models;

namespace StudentEnrolmentNew.src.Data
{
    public interface IStudentEnrolmentContext
    {
        DbSet<Students> Student { get; set; }
        DbSet<Courses> Course { get; set; }
        DbSet<Subjects> Subject { get; set; }
        DbSet<CourseMembership> CourseMemberships { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}