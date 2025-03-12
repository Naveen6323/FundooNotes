using DataAcessLayer.Entity;
//using DataAcessLayer.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAcessLayer.Context
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions options):base(options) {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserNotes> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<NoteLabel> NoteLabels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Unique Email Constraint for User

            // ✅ User-Notes (One-to-Many)
            modelBuilder.Entity<UserNotes>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade); // ✅ If a User is deleted, all Notes will also be deleted.

            // ✅ Many-to-Many Relationship (Notes <-> Labels)
            modelBuilder.Entity<NoteLabel>()
                .HasKey(nl => new { nl.NoteId, nl.LabelId }); // ✅ Composite Primary Key

            modelBuilder.Entity<NoteLabel>()
                .HasOne(nl => nl.Note)
                .WithMany(n => n.NoteLabels)
                .HasForeignKey(nl => nl.NoteId)
                .OnDelete(DeleteBehavior.NoAction); // ❌ Prevent cascade delete on Notes

            modelBuilder.Entity<NoteLabel>()
                .HasOne(nl => nl.Label)
                .WithMany(l => l.NoteLabels)
                .HasForeignKey(nl => nl.LabelId)
                .OnDelete(DeleteBehavior.Cascade);
            // ✅ Allow cascade delete on Labels
            //modelBuilder.Entity<UserNotes>()
            //    .Property(e => e.Description)
            //    .HasDefaultValue("");
            //modelBuilder.Entity<UserNotes>()
            //    .Property(e => e.Color)
            //    .HasDefaultValue("");
            //modelBuilder.Entity<UserNotes>()
            //    .Property(e => e.IsTrash)
            //    .HasDefaultValue(false); // Sets default value as FALSE (0)
            //modelBuilder.Entity<UserNotes>()
            //    .Property(e => e.IsArchieved)
            //    .HasDefaultValue(false);
            //modelBuilder.Entity<NoteLabel>()
            //     .HasKey(nl => new { nl.NoteId, nl.LabelId }); // Composite Key
            //modelBuilder.Entity<NoteLabel>()
            //     .HasOne(nl => nl.Note)
            //     .WithMany(n => n.NoteLabels)
            //     .HasForeignKey(nl => nl.NoteId);

            //modelBuilder.Entity<NoteLabel>()
            //    .HasOne(nl => nl.Label)
            //    .WithMany(l => l.NoteLabels)
            //    .HasForeignKey(nl => nl.LabelId);
        }

    }

}
