using System;
using System.Collections.ObjectModel;
using System.Linq;
using Yatzy.ViewModel;

namespace Yatzy.Helper
{
  public class DiceThrowerHelper
  {
    private readonly ObservableCollection<DiceViewModel> _emptyDiceList;
    private readonly ObservableCollection<DiceViewModel> _availableDiceList;
    private ObservableCollection<DiceViewModel> _diceList;

    public DiceThrowerHelper(ObservableCollection<DiceViewModel> diceList)
    {
      _diceList = diceList;
      _availableDiceList = BoardCreator.InitDices();
      _emptyDiceList = new ObservableCollection<DiceViewModel>()
      {
        new DiceViewModel(),
        new DiceViewModel(),
        new DiceViewModel(),
        new DiceViewModel(),
        new DiceViewModel(),
      };
      _diceList = _emptyDiceList;
    }

    public void RandomDiceThrow()
    {
      if (_diceList.Any(x => x.Eyes == 0))
      {
        var rnd = new Random();
        _diceList = new ObservableCollection<DiceViewModel>();
        for (var i = 0; i < 5; i++)
        {
          var a = rnd.Next(1, 6);
          _diceList.Add(new DiceViewModel(_availableDiceList.First(d => d.Eyes == a)));
        }
      }
      else
      {
        var rnd = new Random();
        for (var i = 0; i < _diceList.Count; i++)
        {
          if (_diceList[i].IsSelected) continue;
          var a = rnd.Next(1, 6);
          _diceList[i] = new DiceViewModel(_availableDiceList.First(d => d.Eyes == a));
        }
      }
    }

    public void Reset()
    {
      _diceList = _emptyDiceList;
    }
  }
}