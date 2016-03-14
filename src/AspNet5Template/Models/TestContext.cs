using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Infrastructure;

namespace AspNet5Template.Models{
    public class TestContext : DbContext {
        /*
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

        //https://github.com/aspnet/EntityFramework/blob/9e323b58acd1429ce7cdafd53fa043a32161c188/src/EntityFramework.Core/Internal/DbSetSource.cs
        //https://github.com/aspnet/EntityFramework/blob/c90cc183b7a67ac4be5ec1cda6d6a44a16b68551/src/EntityFramework.Core/Internal/InternalDbSet.cs
        //note. 實作LazyLoading

        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        */
    }
}
