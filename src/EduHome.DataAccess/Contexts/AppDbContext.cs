using EduHome.Core.Entities;
using EduHome.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EduHome.DataAccess.Contexts;
public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

	public DbSet<Course> Courses { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
		base.OnModelCreating(modelBuilder);
	}

}
