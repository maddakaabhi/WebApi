using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class NotesDBContext:DbContext
    {
        public NotesDBContext(DbContextOptions dbContext) : base(dbContext) { }
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<NoteEntity> Notebook { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<LabelEntity> LabelsT { get; set; }
    }
}
