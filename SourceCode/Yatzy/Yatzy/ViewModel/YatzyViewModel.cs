﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ObjectBuilder2;
using Yatzy.Helper;
using Yatzy.Services;
using Yatzy.Views;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Yatzy.ViewModel
{
  public class YatzyViewModel : ViewModelBase
  {
    private bool _isRollEnabled;
    private ObservableCollection<DiceViewModel> _diceList;
    private PlayerViewModel _activePlayer;
    private IDatabaseService _databaseService;
    private ObservableCollection<DiceViewModel> _selectedDices;

    public YatzyViewModel(ISession session, IDatabaseService databaseService)
    {
      _databaseService = databaseService;
      GameStart(session);
      RollCommand = new RelayCommand(Roll);
      PlayCommand = new RelayCommand(Play);
      SetIsSelected= new RelayCommand(SetSelected);
    }

    private void SetSelected()
    {
      var dice = DiceList[SelectedDice];
      dice.IsSelected = !dice.IsSelected;
      var selectedList = DiceList.Where(d => d.IsSelected).ToList();
      foreach (var right in RightColumnList)
      {
        right.SelectedDices = selectedList;
      }

      foreach (var left in LeftColumnList)
      {
        left.SelectedDices = selectedList;
      }
    }

    public int SelectedDice { get; set; }

    public void FinishGame()
    {
      var winner = Player1.Score > Player2.Score ? Player1 : Player2;
      MessageBox.Show("Congratulations:" + winner.Name + "You won everything", "Winner Winner Chicken Dinner",
        MessageBoxButton.OK);
      _databaseService.SetNewHighScore(new List<PlayerViewModel>()
      {
        Player1,
        Player2
      });

      var result = MessageBox.Show("Do  you  want to play  again  with the same  names?", "Replay",
        MessageBoxButton.YesNo);

      if (result == MessageBoxResult.Yes)
      {
        Replay();
      }
      else
      {
        var currentView = Application.Current.Windows[0];
        new MainWindow().Show();
        currentView?.Close();
      }
    }

    private void Replay()
    {
      Player1.RollCount = 3;
      Player1.Score = 0;
      Player1.LeftScore = 0;
      Player1.RightScore = 0;
      Player1.Bonus = 0;

      Player2.RollCount = 3;
      Player2.Score = 0;
      Player2.LeftScore = 0;
      Player2.RightScore = 0;
      Player2.Bonus = 0;

      foreach (var row in LeftColumnList)
      {
        row.Player1Score = 0;
        row.Player2Score = 0;
      }

      foreach (var row in RightColumnList)
      {
        row.Player1Score = 0;
        row.Player2Score = 0;
      }

      DiceList.Clear();
    }

    private void Roll()
    {   
      RandomDiceThrow();

      ActivePlayer.RollCount--;

      if (ActivePlayer.RollCount == 0)
      {
        IsRollEnabled = false;
      }
    }

    private void RandomDiceThrow()
    {
      if (DiceList.Any(x=>x.Eyes==0))
      {
        var rnd = new Random();
        DiceList = new ObservableCollection<DiceViewModel>();
        for (var i = 0; i < 5; i++)
        {
          var a = rnd.Next(1, 6);
          DiceList.Add(new DiceViewModel(AvailableDiceList.First(d => d.Eyes == a)));
        }
      }
      else
      {
        var rnd = new Random();
        for (var i = 0; i < DiceList.Count; i++)
        {
          if (!DiceList[i].IsSelected)
          {
            var a = rnd.Next(1, 6);
            DiceList[i] = new DiceViewModel(AvailableDiceList.First(d => d.Eyes == a));
          }
        }
      }
    }

    public bool IsRollEnabled
    {
      get => _isRollEnabled;
      set
      {
        _isRollEnabled = value;
        RaisePropertyChanged();
      }
    }

    private void Play()
    {
      if (IsPlayAllowed())
      {
        if (AllFieldPlayed())
        {
          FinishGame();
        }
      }
      UpdateScores();
      ActivePlayer = ActivePlayer == Player1 ? Player2 : Player1;
      Player1.IsActive = !Player1.IsActive;
      Player2.IsActive = !Player2.IsActive;
    }

    public void UpdateScores()
    {
      Player1.LeftScore = GetLeftScore(1);
      Player2.LeftScore = GetLeftScore(2);

      if (Player1.LeftScore > 62)
      {
        Player1.Bonus = 35;
      }
      else if (Player2.LeftScore > 62)
      {
        Player2.Bonus = 35;
      }

      Player1.RightScore = GetRightScore(1);
      Player2.RightScore = GetRightScore(2);

      Player1.Score = Player1.LeftScore + Player1.RightScore + Player1.Bonus;
      Player2.Score = Player2.LeftScore + Player2.RightScore + Player2.Bonus;
    }

    private int GetRightScore(int id)
    {
      var rightScore = id == 1
        ? RightColumnList.Select(s => s.Player1Score).Sum()
        : RightColumnList.Select(s => s.Player2Score).Sum();
      return rightScore;
    }

    private int GetLeftScore(int id)
    {
      var leftScore = id == 1
        ? LeftColumnList.Select(s => s.Player1Score).Sum()
        : LeftColumnList.Select(s => s.Player2Score).Sum();
      return leftScore;
    }

    private bool IsPlayAllowed()
    {
      return true;
    }

    private bool AllFieldPlayed()
    {
      return RightColumnList.All(p => p.IsPlayed && p.IsPlayedPlayerTwo) &&
             LeftColumnList.All(a => a.IsPlayed &&  a.IsPlayedPlayerTwo);
    }

    private void GameStart(ISession session)
    {
      InitPlayers(session);
      InitDices();
      InitBoard();
      SelectActivePlayer();
    }

    private void SelectActivePlayer()
    {
      var rnd = new Random().Next(1);
      ActivePlayer = rnd == 0 ? Player1 : Player2;
      ActivePlayer.IsActive = true;
    }

    private void InitDices()
    {
      AvailableDiceList = BoardCreator.InitDices();
      DiceList = new ObservableCollection<DiceViewModel>()
      {
        new DiceViewModel(),
        new DiceViewModel(),
        new DiceViewModel(),
        new DiceViewModel(),
        new DiceViewModel(),
      };
    }

    public ObservableCollection<DiceViewModel> AvailableDiceList { get; set; }

    private void InitPlayers(ISession session)
    {
      Player1 = session.Player1;
      Player2 = session.Player2;
      IsRollEnabled = true;
    }

    private void InitBoard()
    {
      LeftColumnList = BoardCreator.InitLeftColumn();
      RightColumnList = BoardCreator.InitRightColumn();
    }

    public ObservableCollection<DiceViewModel> DiceList
    {
      get => _diceList;
      set
      {
        _diceList = value;
        RaisePropertyChanged();
      }
    }

    public PlayerViewModel ActivePlayer
    {
      get => _activePlayer;
      set
      {
        _activePlayer = value;
        RaisePropertyChanged();
      }
    }

    public ICommand RollCommand { get; set; }
    public ICommand PlayCommand { get; set; }

    public ICommand SetIsSelected { get; set; }
    public PlayerViewModel Player1 { get; set; }
    public PlayerViewModel Player2 { get; set; }

    public ObservableCollection<LeftColumnViewModel> LeftColumnList { get; set; }
    public ObservableCollection<RightColumnViewModel> RightColumnList { get; set; }
  }
}