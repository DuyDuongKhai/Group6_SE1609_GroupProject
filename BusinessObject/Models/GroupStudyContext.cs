using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BusinessObject.Models
{
    public partial class GroupStudyContext : DbContext
    {
        public GroupStudyContext()
        {
        }

        public GroupStudyContext(DbContextOptions<GroupStudyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<JoinRequest> JoinRequests { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("GroupStudy"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__ChatMess__C87C037CDAE6C606");

                entity.Property(e => e.MessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("MessageID");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.SentAt).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__ChatMessa__SentA__5165187F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ChatMessa__UserI__52593CB8");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("CommentID");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Comments__Create__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__UserID__440B1D61");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("GroupID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GroupAdminId).HasColumnName("GroupAdminID");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.GroupAdmin)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.GroupAdminId)
                    .HasConstraintName("FK__Groups__Descript__38996AB5");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.Property(e => e.GroupMemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("GroupMemberID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.JoinedAt).HasColumnType("datetime");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupMemb__Joine__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupMemb__UserI__3C69FB99");
            });

            modelBuilder.Entity<JoinRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__JoinRequ__33A8519AE9C9AD86");

                entity.Property(e => e.RequestId)
                    .ValueGeneratedNever()
                    .HasColumnName("RequestID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.JoinRequests)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__JoinReque__Statu__4D94879B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JoinRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__JoinReque__UserI__4E88ABD4");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.Property(e => e.MeetingId)
                    .ValueGeneratedNever()
                    .HasColumnName("MeetingID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MeetingDateTime).HasColumnType("datetime");

                entity.Property(e => e.MeetingTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Meetings__Descri__4AB81AF0");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId)
                    .ValueGeneratedNever()
                    .HasColumnName("PostID");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Posts__CreatedAt__3F466844");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Posts__UserID__403A8C7D");

              



            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId)
                    .ValueGeneratedNever()
                    .HasColumnName("TaskID");

                entity.Property(e => e.AssignedToUserId).HasColumnName("AssignedToUserID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssignedToUser)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.AssignedToUserId)
                    .HasConstraintName("FK__Tasks__AssignedT__47DBAE45");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Tasks__Status__46E78A0C");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
