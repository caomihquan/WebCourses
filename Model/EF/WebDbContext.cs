using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class WebDbContext : DbContext
    {
        public WebDbContext()
            : base("name=WebDbContext")
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogTag> BlogTags { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryBlog> CategoryBlogs { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<JoinedCours> JoinedCourses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<ReviewBlog> ReviewBlogs { get; set; }
        public virtual DbSet<ReviewCourse> ReviewCourses { get; set; }
        public virtual DbSet<ReviewLesson> ReviewLessons { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<BlogTag>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryBlog>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Cours>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Cours>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Credential>()
                .Property(e => e.UserGroupID)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Lesson>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ConfirmPassword)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.GroupID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.ID)
                .IsUnicode(false);
        }
    }
}
