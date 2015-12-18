using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Models{
    public class BloggingContext : DbContext{
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Blog>(entity => {
                entity.HasKey(blog => blog.BlogId);
                entity.Property(e => e.Url).IsRequired();
            });
            
            modelBuilder.Entity<Post>(entity => {
                entity.HasKey(post => post.PostId);
                entity.Property(post => post.Title);
                entity.Property(post => post.Content);
                entity.HasOne(post => post.Blog).WithMany(blog => blog.Post).HasForeignKey(post => post.BlogId);
                entity.HasOne(post => post.Author).WithMany(author => author.Post).HasForeignKey(post => post.AuthorId);
            });

            modelBuilder.Entity<Author>(entity => {
                entity.HasKey(author => author.AuthorId);
                entity.Property(author => author.Name);
            });
        }

        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Author> Author { get; set; }
    }
}
