using System.Collections.ObjectModel;
using System.Linq;

namespace Yatzy.ViewModel
{
  public class ScoreCalculateHelper
  {
    private readonly PlayerViewModel _player1;
    private readonly PlayerViewModel _player2;
    private readonly ObservableCollection<RightColumnViewModel> _rightColumnList;
    private readonly ObservableCollection<LeftColumnViewModel> _leftColumnList;

    public ScoreCalculateHelper(PlayerViewModel player1, PlayerViewModel player2, ObservableCollection<RightColumnViewModel> rightColumnList, ObservableCollection<LeftColumnViewModel> leftColumnList)
    {
      _player1 = player1;
      _player2 = player2;
      _rightColumnList = rightColumnList;
      _leftColumnList = leftColumnList;
    }

    public void UpdateScores()
    {
      _player1.LeftScore = GetLeftScore(1);
      _player2.LeftScore = GetLeftScore(2);

      if (_player1.LeftScore > 62)
      {
        _player1.Bonus = 35;
      }
      else if (_player2.LeftScore > 62)
      {
        _player2.Bonus = 35;
      }

      _player1.RightScore = GetRightScore(1);
      _player2.RightScore = GetRightScore(2);

      _player1.Score = _player1.LeftScore + _player1.RightScore + _player1.Bonus;
      _player2.Score = _player2.LeftScore + _player2.RightScore + _player2.Bonus;
    }

    private int GetRightScore(int id)
    {
      var rightScore = id == 1
        ? _rightColumnList.Select(s => s.Player1Score).Sum()
        : _rightColumnList.Select(s => s.Player2Score).Sum();
      return rightScore;
    }

    private int GetLeftScore(int id)
    {
      var leftScore = id == 1
        ? _leftColumnList.Select(s => s.Player1Score).Sum()
        : _leftColumnList.Select(s => s.Player2Score).Sum();
      return leftScore;
    }
  }
}