using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Yatzy.ViewModel
{
  public class LeftColumnViewModel : ViewModelBase
  {
    private int _player1Score;
    private int _player2Score;
    private bool _isPlayed;
    private bool _isPlayedPlayerTwo;

    public LeftColumnViewModel()
    {
      ShowScorePlayerOneCommand = new RelayCommand(ShowScorePlayerOne);
      ShowScorePlayerTwoCommand = new RelayCommand(ShowScorePlayerTwo);
    }

    private void ShowScorePlayerOne()
    {
      Player1Score = CalculatePoints();
      IsPlayed = true;
    }

    public ICommand ShowScorePlayerTwoCommand { get; set; }
    public ICommand ShowScorePlayerOneCommand { get; set; }

    private int CalculatePoints()
    {
      if (SelectedDices!=null)
      {
        var validDices = SelectedDices.Where(d => d.Eyes == AllowedNumber);
        return validDices.Select(d => d.Eyes).Sum();
      }

      return 0;
    }

    private void ShowScorePlayerTwo()
    {
      Player2Score = CalculatePoints();
      IsPlayedPlayerTwo = true;
    }

    public int AllowedNumber { get; set; }
    public string Description { get; set; }

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

    public string Image { get; set; }

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

    public List<DiceViewModel> SelectedDices { get; set; }
  }
}