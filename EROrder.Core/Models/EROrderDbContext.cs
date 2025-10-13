using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EROrder.Core.Models;

public partial class EROrderDbContext : DbContext
{
    public EROrderDbContext(DbContextOptions<EROrderDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
