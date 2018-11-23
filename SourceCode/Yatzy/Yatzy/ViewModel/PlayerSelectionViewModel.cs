using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Yatzy.Services;
using Yatzy.Views;

namespace Yatzy.ViewModel
{
  public class PlayerSelectionViewModel : ViewModelBase
  {
    public PlayerSelectionViewModel()
    {
      CancelCommand = new RelayCommand(Cancel);
      StartCommand = new RelayCommand(Start);
    }

    private void Start()
    {
      if (IsValidPlayerSet())
      {
        DependencyLocator.Instance.InitPlayers(Player1,Player2);

        new YatzyView().Show();
        Application.Current.Windows[0]?.Close();
      }
      else
      {
        MessageBox.Show("Please provide two PlayerViewModel names", "Invalid Players", MessageBoxButton.OK);
      }
    }

    public string Player1 { get; set; }
    public string Player2 { get; set; }

    private bool IsValidPlayerSet()
    {
      return !string.IsNullOrWhiteSpace(Player1) && !string.IsNullOrWhiteSpace(Player2) && !Player1.Equals(Player2);
    }

    private void Cancel()
    {
      var currentView = Application.Current.Windows[0];
      new MainWindow().Show();
      currentView?.Close();
    }

    public ICommand CancelCommand { get; set; }
    public ICommand StartCommand { get; set; }
  }
}