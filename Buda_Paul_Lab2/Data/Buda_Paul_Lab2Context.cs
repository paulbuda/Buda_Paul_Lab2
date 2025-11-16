using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Buda_Paul_Lab2.Models;

namespace Buda_Paul_Lab2.Data
{
    public class Buda_Paul_Lab2Context : DbContext
    {
        public Buda_Paul_Lab2Context (DbContextOptions<Buda_Paul_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Buda_Paul_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Buda_Paul_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    }
}
