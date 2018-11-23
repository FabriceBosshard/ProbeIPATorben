using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Model;
using Yatzy.ViewModel;

namespace Yatzy.Services
{
  public interface IDatabaseService
  {
    ObservableCollection<HighScore> GetHighScores();
    void SetNewHighScore(ICollection<PlayerViewModel> players);
  }
}
