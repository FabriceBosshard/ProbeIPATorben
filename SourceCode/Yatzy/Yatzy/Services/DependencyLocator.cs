using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;
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
    public void InitPlayers()
    {
      //var session = new Session();
      //Container.RegisterInstance<IAppSession>(session);
    }

    internal static DependencyLocator Instance =>
      Application.Current.Resources["Locator"] as DependencyLocator;

    public MainViewModel Main => Container.Resolve<MainViewModel>();
    public PlayerSelectionViewModel PlayerSelection => Container.Resolve<PlayerSelectionViewModel>();
    public HighscoreViewModel Highscore => Container.Resolve<HighscoreViewModel>();
    public YatzyViewModel Yatzy => Container.Resolve<YatzyViewModel>();
  }
}