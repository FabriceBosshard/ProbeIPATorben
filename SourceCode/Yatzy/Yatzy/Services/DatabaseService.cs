using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ObjectBuilder2;
using Yatzy.Model;
using Yatzy.ViewModel;

namespace Yatzy.Services
{
  public class DatabaseService : IDatabaseService
  {
    public ObservableCollection<HighScore> GetHighScores()
    {
      var highScores = new ObservableCollection<HighScore>();
      using (var ctx = new YatzyDbContext())
      {
        try
        {
          highScores = new ObservableCollection<HighScore>(ctx.HighScores.OrderByDescending(h => h.Score).Take(5));
        }
        catch (Exception e)
        {
          MessageBox.Show("There was an error loading the highScore data: " + e.Message, "Database Error",
            MessageBoxButton.OK);
        }
      }
      return highScores;
    }

    public void SetNewHighScore(ICollection<PlayerViewModel> players)
    {
      using (var ctx = new YatzyDbContext())
      {
        try
        {
          players.ForEach(p=> ctx.HighScores.Add(new HighScore()
          {
            Score = p.Score,
            Name = p.Name
          }));
        }
        catch (Exception e)
        {
          MessageBox.Show("There was an error saving the highScore data: " + e.Message, "Database Error",
            MessageBoxButton.OK);
        }
      }
    }
  }
}