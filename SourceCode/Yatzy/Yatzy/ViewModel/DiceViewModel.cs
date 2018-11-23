using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace Yatzy.ViewModel
{
  public class DiceViewModel:ViewModelBase
  {
    public DiceViewModel()
    {
      Opacity = 1;
    }

    public DiceViewModel(DiceViewModel diceViewModel)
    {
      Image = diceViewModel.Image;
      Eyes = diceViewModel.Eyes;
      Opacity = diceViewModel.Opacity;
      IsSelected = diceViewModel.IsSelected;
    }

    private bool _isSelected;
    private Color _background;
    private double _opacity;
    private DiceViewModel diceViewModel;

    public string Image { get; set; }
    public int Eyes { get; set; }

    public double Opacity
    {
      get => _opacity;
      set
      {
        _opacity = value;
        RaisePropertyChanged();
      }
    }

    public bool IsSelected
    {
      get => _isSelected;
      set
      {
        _isSelected = value;
        RaisePropertyChanged();
        if (value)
        {
          Opacity = 0.6;
        }
        else
        {
          Opacity = 1;
        }
      }
    }
  }
}