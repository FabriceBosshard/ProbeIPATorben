using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.ViewModel;

namespace Yatzy.Model
{
  public class HighScore
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HighScoreId { get; set; }

    public string Name { get; set; }
    public int Score { get; set; }
  }
}