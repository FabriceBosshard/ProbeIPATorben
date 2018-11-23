using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Yatzy.Model;
using Yatzy.ViewModel;

namespace Yatzy.Services
{
  public class DependencyLocator
  {
    public readonly UnityContainer Container;

    public DependencyLocator()
    {
      Container = new UnityContainer();

      Container.RegisterType<IDatabaseService, DatabaseService>(new ContainerControlledLifetimeManager());
    }
    public void InitPlayers(string player1, string player2)
    {
      var session = new Session()
      {
        Player1 = new PlayerViewModel(player1),
        Player2 = new PlayerViewModel(player2)
      };
      Container.RegisterInstance<ISession>(session);
    }

    internal static DependencyLocator Instance =>
      Application.Current.Resources["Locator"] as DependencyLocator;

    public MainViewModel Main => Container.Resolve<MainViewModel>();
    public PlayerSelectionViewModel PlayerSelection => Container.Resolve<PlayerSelectionViewModel>();
    public HighscoreViewModel HighScore => Container.Resolve<HighscoreViewModel>();
    public YatzyViewModel Yatzy => Container.Resolve<YatzyViewModel>();
  }
}