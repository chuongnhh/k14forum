using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using K14Forum.CodeHelper;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace K14Forum.Models
{
    [Table("AspNetArticles")]
    public class ApplicationArticle
    {
        public ApplicationArticle()
        {
            Id = GuidComb.GenerateComb();
            Comments = new List<ApplicationComment>();
            Tags = new List<ApplicationTag>();
            DateCreated = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ArticleContent { get; set; }
        public string Description
        {
            get
            {
                return RemoveHtmlTags
                .RemoveHtmlTagsUsingCharArray(ArticleContent
                .Substring(0, ArticleContent.Length >= 500 ? 500 : ArticleContent.Length));
            }

        }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public int Views { get; set; }
        public bool IsLocked { get; set; }

        public virtual IList<ApplicationComment> Comments { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual IList<ApplicationTag> Tags { get; set; }
    }

    [Table("AspNetComments")]
    public class ApplicationComment
    {
        public ApplicationComment()
        {
            Id = GuidComb.GenerateComb();
            DateCreated = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }

        public Guid ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual ApplicationArticle Article { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

    [Table("AspNetTags")]
    public partial class ApplicationTag
    {
        public ApplicationTag()
        {
            Id = GuidComb.GenerateComb();
            Articles = new List<ApplicationArticle>();
        }
        [Key]
        public Guid Id { get; set; }
        [Index(IsUnique = true), MaxLength(50)]
        public string Tag { get; set; }

        public virtual IList<ApplicationArticle> Articles { get; set; }

        public int TagsCount
        {
            get { return Articles.Count(); }
        }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? BirthDate { get; set; }

        public virtual IList<ApplicationArticle> Articles { get; set; }
        public virtual IList<ApplicationComment> Comments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<ApplicationArticle> ApplicationArticles { get; set; }
        public virtual DbSet<ApplicationComment> ApplicationComments { get; set; }
        public virtual DbSet<ApplicationTag> ApplicationTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<ApplicationArticle>()
                .HasMany<ApplicationTag>(x => x.Tags)
                .WithMany(x => x.Articles)
                .Map(x =>
                {
                    x.MapLeftKey("ArticleId");
                    x.MapRightKey("TagId");
                    x.ToTable("AspNetArticleTags");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}