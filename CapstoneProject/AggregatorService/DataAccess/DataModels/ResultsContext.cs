using DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorController.DataAccess
{
    public class ResultsContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Userdata> Users { get; set; }
        public DbSet<Usertopic> UserTopics { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Userarticle> UserArticles { get; set; }

        public ResultsContext(DbContextOptions<ResultsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Userdata>().HasKey(k => new { k.UserID });

            modelBuilder.Entity<Topic>().HasKey(k => new { k.TopicID });

            modelBuilder.Entity<Usertopic>().HasKey(k => new { k.UserTopicID });

            modelBuilder.Entity<Article>().HasKey(k => new { k.ArticleID });

            modelBuilder.Entity<Userarticle>().HasKey(k => new { k.UserArticleID });
        }
    }
}
