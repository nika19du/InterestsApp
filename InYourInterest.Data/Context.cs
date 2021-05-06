using InYourInterest.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InYourInterest.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; } 
        public DbSet<GroupMembers> GroupMembers { get; set; } 
        public DbSet<Event> Events { get; set; } 
        public DbSet<EventUser> EventUsers { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; } 
        public DbSet<Post> Posts { get; set; } 
        public DbSet<PostReaction> PostReactions { get; set; }  
        public DbSet<PostTag> PostsTags { get; set; } 
        public DbSet<Reply> Replies { get; set; } 
        public DbSet<ReplyReaction> ReplyReactions { get; set; } 
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<UserFollower> UsersFollowers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Message>().Ignore(t => t.Author).Ignore(x=>x.Receiver); 
            builder.Entity<Message>()
                .HasOne(x => x.Author)
                .WithMany(x => x.SentMessages).HasForeignKey(x=>x.AuthorId);
            builder.Entity<Message>()
                .HasOne(x => x.Receiver)
                .WithMany(x => x.ReceivedMessages).HasForeignKey(x => x.ReceiverId);
           
            builder.Entity<UserFollower>()
                .HasKey(x => new { x.UserId, x.FollowerId });
            builder.Entity<UserFollower>()
                .HasOne(x => x.User).WithMany(x => x.Followers).HasForeignKey(x => x.UserId);
            //builder.Entity<UserFollower>().HasOne(x => x.Follower).WithMany(x => x.Followers).HasForeignKey(x=>x.FollowerId);

            builder.Entity<PostTag>()
                .HasKey(x => new { x.PostId, x.TagId });
            builder.Entity<PostTag>().HasOne(x => x.Post).WithMany(x => x.Tags).HasForeignKey(x => x.PostId);
            builder.Entity<PostTag>().HasOne(x => x.Tag).WithMany(x => x.Posts).HasForeignKey(x => x.TagId);

            builder.Entity<EventUser>()
                 .HasKey(x => new { x.EventId, x.UserId });
            builder.Entity<EventUser>()
                .HasOne(x => x.Event).WithMany(x => x.EventUser).HasForeignKey(x => x.EventId);
            builder.Entity<EventUser>()
                .HasOne(x => x.User).WithMany(x => x.EventUser).HasForeignKey(x => x.UserId);

            base.OnModelCreating(builder);
            
        }
    }
}
