using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Yatzy.Views;

namespace Yatzy.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    public MainViewModel()
    {
      PlayCommand = new RelayCommand(Play);
      QuitCommand = new RelayCommand(() => Environment.Exit(0));
      HighScoreCommand = new RelayCommand(OpenHighScore);
    }

    private static void Play()
    {
      new PlayerSelectionView().Show();
      CloseMain();
    }

    private static void OpenHighScore()
    {
      new HighscoreView().Show();
      CloseMain();
    }

    private static void CloseMain()
    {
      Application.Current.Windows[0]?.Close();
    }

    public ICommand PlayCommand { get; set; }
    public ICommand QuitCommand { get; set; }
    public ICommand HighScoreCommand { get; set; }
  }
}