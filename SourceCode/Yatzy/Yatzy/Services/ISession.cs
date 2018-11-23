using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Model;
using Yatzy.ViewModel;

namespace Yatzy.Services
{
  public interface ISession
  {
    PlayerViewModel Player1 { get; set; }
    PlayerViewModel Player2 { get; set; }
  }
}