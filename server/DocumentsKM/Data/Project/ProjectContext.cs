using DocumentsKM.Model;
using Microsoft.EntityFrameworkCore;

namespace DocumentsKM.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> opt) : base(opt) {}

        public DbSet<Project> Projects { get; set; }
    }
}
