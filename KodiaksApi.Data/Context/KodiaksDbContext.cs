using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KodiaksApi.Data.DbModels;

namespace KodiaksApi.Data.Context
{
    public partial class KodiaksDbContext : DbContext
    {
        public KodiaksDbContext()
        {
        }

        public KodiaksDbContext(DbContextOptions<KodiaksDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignRoleMenu> AssignRoleMenus { get; set; }
        public virtual DbSet<BattingThrowingSide> BattingThrowingSides { get; set; }
        public virtual DbSet<Concept> Concepts { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<MovementType> MovementTypes { get; set; }
        public virtual DbSet<PasswordsHistory> PasswordsHistories { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Roster> Rosters { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignRoleMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AssignRoleMenu", "App");

                entity.HasIndex(e => new { e.RoleId, e.MenuItemId }, "IX_App_AssignRoleMenu_RoleId_MenuItemId")
                    .IsClustered();

                entity.HasIndex(e => new { e.RoleId, e.MenuItemId }, "UK_App_AssignRoleMenu_RolMenu")
                    .IsUnique();

                entity.HasOne(d => d.MenuItem)
                    .WithMany()
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_AssignRoleMenu_MenuItemId");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_AssignRoleMenu_RolId");
            });

            modelBuilder.Entity<BattingThrowingSide>(entity =>
            {
                entity.HasKey(e => e.BtsideId)
                    .HasName("PK_Stats_BattingThrowingSide_Id");

                entity.ToTable("BattingThrowingSides", "Stats");

                entity.Property(e => e.BtsideId).HasColumnName("BTSideId");

                entity.Property(e => e.BtsideDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BTSideDesc");

                entity.Property(e => e.KeyValue)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Concept>(entity =>
            {
                entity.ToTable("Concepts", "Fina");

                entity.HasIndex(e => e.ConceptDesc, "UQ_Fina_Concepts_Type_Desc")
                    .IsUnique();

                entity.HasIndex(e => e.ConceptKey, "UQ_Fina_Concepts_Type_Key")
                    .IsUnique();

                entity.Property(e => e.ConceptDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConceptKey)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Members", "App");

                entity.HasIndex(e => e.CellPhoneNumber, "UQ_Fina_Members_CellPhoneNumber")
                    .IsUnique();

                entity.HasIndex(e => new { e.MemberId, e.UserId }, "UQ_Fina_Members_MemberIdUserId")
                    .IsUnique();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.BtsideId).HasColumnName("BTSideId");

                entity.Property(e => e.CellPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoUrl).IsUnicode(false);

                entity.HasOne(d => d.Btside)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.BtsideId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fina_Members_BTSideId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fina_Members_UserId");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItems", "App");

                entity.HasIndex(e => e.ItemKey, "UQ_App_MenuItems_ItemKey")
                    .IsUnique();

                entity.Property(e => e.IconSource)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemKey)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.ToTable("Movements", "Fina");

                entity.Property(e => e.AdditionalComment)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EvidenceUrl).IsUnicode(false);

                entity.Property(e => e.MovementDate).HasColumnType("datetime");

                entity.HasOne(d => d.Concept)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.ConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fina_Movements_ConceptId");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fina_Movements_UserId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fina_Movements_MemberId");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fina_Movements_MethodId");

                entity.HasOne(d => d.MovementType)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.MovementTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fina_Movements_MovementTypeId");
            });

            modelBuilder.Entity<MovementType>(entity =>
            {
                entity.ToTable("MovementTypes", "Fina");

                entity.HasIndex(e => e.MovementTypeKey, "UQ_MovementTypes_ConceptTypes_Desc")
                    .IsUnique();

                entity.HasIndex(e => e.MovementTypeDesc, "UQ_MovementTypes_ConceptTypes_Key")
                    .IsUnique();

                entity.Property(e => e.MovementTypeDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MovementTypeKey)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PasswordsHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK_Sec_PasswordsHistory_Id");

                entity.ToTable("PasswordsHistory", "Sec");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PasswordsHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sec_PasswordsHistory_UserId");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.MethodId)
                    .HasName("PK_Fina_PaymentMethods_Id");

                entity.ToTable("PaymentMethods", "Fina");

                entity.Property(e => e.MethodDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Positions", "Stats");

                entity.Property(e => e.KeyValue)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.PositionDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "App");

                entity.HasIndex(e => e.RoleDescription, "UQ_App_Roles_RoleDescription")
                    .IsUnique();

                entity.Property(e => e.RoleDescription)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Roster>(entity =>
            {
                entity.ToTable("Roster", "Stats");

                entity.HasIndex(e => new { e.MemberId, e.PositionId }, "UK_Stats_Roster_MemberIdUserId")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Rosters)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stats_Roster_UserId");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Rosters)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stats_Roster_PositionId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Sec");

                entity.HasIndex(e => e.UserName, "PK_Sec_Users_UserName")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sec_Users_RolId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
