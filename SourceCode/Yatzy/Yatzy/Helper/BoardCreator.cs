using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using Yatzy.ViewModel;

namespace Yatzy.Helper
{
  public class BoardCreator
  {
    public static ObservableCollection<LeftColumnViewModel> InitLeftColumn()
    {
      return new ObservableCollection<LeftColumnViewModel>()
      {
        new LeftColumnViewModel()
        {
          Description = "Aces",
          AllowedNumber = 1,
          Image = @"..\_Assets\dice-1-md.png"
        },
        new LeftColumnViewModel()
        {
          Description = "Twos",
          AllowedNumber = 2,
          Image = @"..\_Assets\dice-2-md.png"
        },
        new LeftColumnViewModel()
        {
          Description = "Threes",
          AllowedNumber = 3,
          Image = @"..\_Assets\dice-3-md.png"
        },
        new LeftColumnViewModel()
        {
          Description = "Fours",
          AllowedNumber = 4,
          Image = @"..\_Assets\dice-4-md.png"
        },
        new LeftColumnViewModel()
        {
          Description = "Fives",
          AllowedNumber = 5,
          Image = @"..\_Assets\dice-5-md.png"
        },
        new LeftColumnViewModel()
        {
          Description = "Sixes",
          AllowedNumber = 6,
          Image = @"..\_Assets\dice-6-md.png"
        },
      };
    }

    public static ObservableCollection<RightColumnViewModel> InitRightColumn()
    {
      return new ObservableCollection<RightColumnViewModel>()
      {
        new RightColumnViewModel()
        {
          Description = "Three of a kind",
          SubDescription = "Total of All Dice",
          RuleSetFunction = (s) =>
          {
            var isTriplet = s.GroupBy(x => x.Eyes).Any(y => y.Count() >= 3);
            return isTriplet ? s.Select(e => e.Eyes).Sum() : 0;
          }
        },
        new RightColumnViewModel()
        {
          Description = "Four of a Kind",
          SubDescription = "Total of All Dice",
          RuleSetFunction = (s) =>
          {
            var isfour = s.GroupBy(x => x.Eyes).Any(y => y.Count() >= 4);
            return isfour ? s.Select(e => e.Eyes).Sum() : 0;
          }
        },
        new RightColumnViewModel()
        {
          Description = "Full House",
          SubDescription = "25 Points",
          RuleSetFunction = (s) =>
          {
            if (s.Count != 5) return 0;
            var groups = s.GroupBy(x => x.Eyes);
            return groups.Count()==2 ? 25 : 0;
          }
        },
        new RightColumnViewModel()
        {
          Description = "Small Straight",
          SubDescription = "30 Points",
          RuleSetFunction = (s) =>
          {
            var continueValue = 1;

            for (var i = 1; i < s.Count; i++)
            {
              if (s[i].Eyes - s[i-1].Eyes == 1)
              {
                continueValue++;
              }
              else
              {
                continueValue = 1;
              }
            }

            return continueValue  >= 4 ? 30 : 0;
          }
        },
        new RightColumnViewModel()
        {
          Description = "Large Straight",
          SubDescription = "40 Points",
          RuleSetFunction = (s) =>
          {
            var continueValue = 1;

            for (var i = 1; i < s.Count; i++)
            {
              if (s[i].Eyes - s[i-1].Eyes == 1)
              {
                continueValue++;
              }
              else
              {
                continueValue = 1;
              }
            }

            return continueValue  == 5 ? 30 : 0;
          }
        },
        new RightColumnViewModel()
        {
          Description = "Yahtzee",
          SubDescription = "50 Points",
          RuleSetFunction = (s) =>
          {
            var isYatzee = s.GroupBy(x => x.Eyes).Any(y => y.Count() == 5);
            return isYatzee ? 50 : 0;
          }
        },
        new RightColumnViewModel()
        {
          Description = "Chance",
          SubDescription = "Total of All Dice",
          RuleSetFunction = (s) => { return s.Select(x => x.Eyes).Sum(); }
        }
      };
    }

    public static ObservableCollection<DiceViewModel> InitDices()
    {
      return new ObservableCollection<DiceViewModel>()
      {
        new DiceViewModel()
        {
          Eyes = 1,
          Image = @"..\_Assets\dice-1-md.png"
        },
        new DiceViewModel()
        {
          Eyes = 2,
          Image = @"..\_Assets\dice-2-md.png"
        },
        new DiceViewModel()
        {
          Eyes = 3,
          Image = @"..\_Assets\dice-3-md.png"
        },
        new DiceViewModel()
        {
          Eyes = 4,
          Image = @"..\_Assets\dice-4-md.png"
        },
        new DiceViewModel()
        {
          Eyes = 5,
          Image = @"..\_Assets\dice-5-md.png"
        },
        new DiceViewModel()
        {
          Eyes = 6,
          Image = @"..\_Assets\dice-6-md.png"
        }
      };
    }
  }
}