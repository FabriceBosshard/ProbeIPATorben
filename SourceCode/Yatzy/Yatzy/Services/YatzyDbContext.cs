using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Model;

namespace Yatzy.Services
{
  public class YatzyDbContext : DbContext
  {
    public YatzyDbContext() : base("YatzyDb")
    {
      
    }
    public DbSet<HighScore> HighScores { get; set; }
  }
}
