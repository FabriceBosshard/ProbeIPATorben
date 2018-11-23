using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Yatzy.Model;
using Yatzy.Services;

namespace Yatzy.ViewModel
{
  public class HighscoreViewModel : ViewModelBase
  {
    public HighscoreViewModel(IDatabaseService databaseService)
    {
      HighScoreList = databaseService.GetHighScores();
      AppendPositioning();
      BackCommand = new RelayCommand(Back);
    }

    private void AppendPositioning()
    {
      for (var i = 0; i < HighScoreList.Count; i++)
      {
        HighScoreList[i].Position = i+1;
      }
    }

    private void Back()
    {
      var currentView = Application.Current.Windows[0];
      new MainWindow().Show();
      currentView?.Close();
    }

    public ObservableCollection<HighScore> HighScoreList { get; set; }

    public ICommand BackCommand { get; set; }
  }
}