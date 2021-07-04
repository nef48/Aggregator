using DataModels;
using Microsoft.EntityFrameworkCore;

namespace AggregatorController.DataAccess
{
    public class ResultsContext : DbContext
    {
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Userdata> Userdata { get; set; }
        public DbSet<Usertopic> Usertopic { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Userarticle> Userarticle { get; set; }
        public DbSet<Topicmap> Topicmap { get; set; }

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

            modelBuilder.Entity<Topicmap>().HasKey(k => new { k.TopicMapID });
        }
    }
}
