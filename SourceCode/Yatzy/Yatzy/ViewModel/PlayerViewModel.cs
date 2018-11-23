using GalaSoft.MvvmLight;

namespace Yatzy.ViewModel
{
  public class PlayerViewModel : ViewModelBase
  {
    private int _score;
    private int _rightScore;
    private int _leftScore;
    private int _bonus;
    private int _rollCount = 3;
    private string _name;
    private bool _isActive;

    public PlayerViewModel(string playerName)
    {
      Name = playerName;
    }

    public int Score
    {
      get => _score;
      set
      {
        _score = value;
        RaisePropertyChanged();
      }
    }

    public int RightScore
    {
      get => _rightScore;
      set
      {
        _rightScore = value;
        RaisePropertyChanged();
      }
    }

    public int LeftScore
    {
      get => _leftScore;
      set
      {
        _leftScore = value;
        RaisePropertyChanged();
      }
    }

    public int Bonus
    {
      get => _bonus;
      set
      {
        _bonus = value;
        RaisePropertyChanged();
      }
    }

    public int RollCount
    {
      get => _rollCount;
      set
      {
        _rollCount = value;
        RaisePropertyChanged();
      }
    }

    public string Name
    {
      get => _name;
      set
      {
        _name = value;
        RaisePropertyChanged();
      }
    }

    public bool IsActive
    {
      get => _isActive;
      set
      {
        _isActive = value;
        RaisePropertyChanged();
      }
    }
  }
}
