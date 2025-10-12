using Fitamon.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;
public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    public DbSet<PostEntity> Posts { get; set; }
    //public DbSet<CommentEntity> Comments { get; set; }
    //public DbSet<TagEntity> Tags { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostEntity>().ToTable("Posts", schema: "blog");
        //modelBuilder.Entity<CommentEntity>().ToTable("Comments", schema: "blog");
        //modelBuilder.Entity<TagEntity>().ToTable("Posts", schema: "blog");
        base.OnModelCreating(modelBuilder);
    }
}