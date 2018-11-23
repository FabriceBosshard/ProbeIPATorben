using Yatzy.Model;
using Yatzy.ViewModel;

namespace Yatzy.Services
{
  public class Session : ISession
  {
    public PlayerViewModel Player1 { get; set; }
    public PlayerViewModel Player2 { get; set; }
  }
}