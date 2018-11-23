using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ObjectBuilder2;
using Yatzy.Helper;
using Yatzy.Services;

namespace Yatzy.ViewModel
{
  public class GameBehaviorHelper
  {
    private PlayerViewModel Player1;
    private PlayerViewModel Player2;
    private ObservableCollection<RightColumnViewModel> RightColumnList;
    private ObservableCollection<LeftColumnViewModel> LeftColumnList;
    private PlayerViewModel ActivePlayer;
    private ObservableCollection<DiceViewModel> DiceList;
    private readonly IDatabaseService _databaseService;
    private ScoreCalculateHelper _scoreCalculateHelper;
    private DiceThrowerHelper _diceThrower;
    private bool _isRollEnabled;

    public GameBehaviorHelper(PlayerViewModel player1, PlayerViewModel player2,
      ObservableCollection<RightColumnViewModel> rightColumnList,
      ObservableCollection<LeftColumnViewModel> leftColumnList,
      PlayerViewModel activePlayer, ObservableCollection<DiceViewModel> diceList,
      IDatabaseService databaseService)
    {
      Player1 = player1;
      Player2 = player2;
      RightColumnList = rightColumnList;
      LeftColumnList = leftColumnList;
      ActivePlayer = activePlayer;
      DiceList = diceList;
      _databaseService = databaseService;
      _diceThrower = new DiceThrowerHelper(DiceList);
      _scoreCalculateHelper = new ScoreCalculateHelper(Player1, Player2, RightColumnList, LeftColumnList);
    }
  }
}