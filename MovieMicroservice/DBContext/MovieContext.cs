using Microsoft.EntityFrameworkCore;
using MovieMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMicroservice.DBContext
{
    public class MovieContext:DbContext
    {
        public MovieContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
    }
}
