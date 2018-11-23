using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Yatzy.ViewModel;

namespace Yatzy.Views
{
  /// <summary>
  /// Interaction logic for YatzyView.xaml
  /// </summary>
  public partial class YatzyView : Window
  {
    public YatzyView()
    {
      InitializeComponent();
    }

    private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      
      var vm = this.DataContext as YatzyViewModel;
      vm?.SetIsSelected.Execute(null);
    }
  }
}
