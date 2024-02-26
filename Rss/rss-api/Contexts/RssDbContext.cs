using Microsoft.EntityFrameworkCore;
using rss_api.Models.Dal;

namespace rss_api.Contexts;

public class RssDbContext(DbContextOptions<RssDbContext> options) : DbContext(options)
{
	public DbSet<RssDalElements> RssElements { get; set; } = null!;
	public DbSet<RssDal> RssFeeds { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//property validation
		modelBuilder.Entity<RssDal>().HasKey(x => x.Id);
		modelBuilder.Entity<RssDal>().Property(x => x.Header).HasMaxLength(255);
		modelBuilder.Entity<RssDal>().Property(x => x.Description).HasMaxLength(30000);

		modelBuilder.Entity<RssDalElements>().HasKey(x => x.Id);
		modelBuilder.Entity<RssDalElements>().Property(x => x.Tag).IsRequired();
		modelBuilder.Entity<RssDalElements>().Property(x => x.CreationDate).IsRequired();

		//relations:
		modelBuilder.Entity<RssDalElements>()
			.HasMany(x => x.RssDalItems)
			.WithOne()
			.HasForeignKey(x => x.RssDalElementsId);

		//indexes:
		modelBuilder.Entity<RssDal>()
			.HasIndex(b => b.Id)
			.IncludeProperties(b => b.Header);

		base.OnModelCreating(modelBuilder);
	}
}
