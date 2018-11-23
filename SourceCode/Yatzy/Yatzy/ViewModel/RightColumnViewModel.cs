using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Yatzy.ViewModel
{
  public class RightColumnViewModel : ViewModelBase
  {
    public RightColumnViewModel()
    {
      ShowScorePlayerOneCommand = new RelayCommand(ShowScorePlayerOne);
      ShowScorePlayerTwoCommand = new RelayCommand(ShowScorePlayerTwo);
      IsPlayedPlayerTwo = true;
      IsPlayed = true;
    }

    private void ShowScorePlayerTwo()
    {
      if (SelectedDices == null) return;
      Player2Score = RuleSetFunction(SelectedDices);
      NotifyAll();
    }

    private void NotifyAll()
    {
      MessengerInstance.Send(Description);
      IsInPreview = true;
    }

    private void ShowScorePlayerOne()
    {
      if (SelectedDices == null) return;
      Player1Score = RuleSetFunction(SelectedDices);
      NotifyAll();
    }

    private int _player1Score;
    private int _player2Score;
    private bool _isPlayed;
    private bool _isPlayedPlayerTwo;
    private bool _isInPreview;
    public Func<List<DiceViewModel>, int> RuleSetFunction { get; set; }
    public string Description { get; set; }
    public string SubDescription { get; set; }

    public int Player1Score
    {
      get => _player1Score;
      set
      {
        _player1Score = value;
        RaisePropertyChanged();
      }
    }

    public int Player2Score
    {
      get => _player2Score;
      set
      {
        _player2Score = value;
        RaisePropertyChanged();
      }
    }
    public bool IsPlayed
    {
      get => _isPlayed;
      set
      {
        _isPlayed = value;
        RaisePropertyChanged();
      }
    }

    public bool IsPlayedPlayerTwo
    {
      get => _isPlayedPlayerTwo;
      set
      {
        _isPlayedPlayerTwo = value;
        RaisePropertyChanged();
      }
    }

    public ICommand ShowScorePlayerTwoCommand { get; set; }
    public ICommand ShowScorePlayerOneCommand { get; set; }

    public List<DiceViewModel> SelectedDices { get; set; }

    public bool IsInPreview
    {
      get => _isInPreview;
      set
      {
        _isInPreview = value;
        RaisePropertyChanged();
        if (!IsInPreview)
        {
          if (IsPlayed)
          {
            Player1Score = 0;
          }

          if (IsPlayedPlayerTwo)
          {
            Player2Score = 0;
          }
        }
      }
    }
  }
}