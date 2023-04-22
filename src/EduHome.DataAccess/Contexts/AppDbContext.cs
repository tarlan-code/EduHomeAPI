using EduHome.Core.Entities;
using EduHome.DataAccess.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduHome.DataAccess.Contexts;
public class AppDbContext:IdentityDbContext
{
	public AppDbContext(DbContextOptions options):base(options) { }

	public DbSet<Course> Courses { get; set; }
	public DbSet<AppUser> AppUsers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
		base.OnModelCreating(modelBuilder);
	}

}
