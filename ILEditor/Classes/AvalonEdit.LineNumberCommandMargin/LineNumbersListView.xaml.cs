using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{
    /// <summary>
    /// Interaction logic for LineNumbersListView.xaml
    /// </summary>
    public partial class LineNumbersListView : UserControl
    {

        #region LineNumbers DP

        public static readonly DependencyProperty LineNumbersProperty = DependencyProperty.Register("LineNumbers", typeof(ObservableCollection<LineNumberDisplayModel>), typeof(LineNumbersListView));

        public ObservableCollection<LineNumberDisplayModel> LineNumbers
        {
            get { return (ObservableCollection<LineNumberDisplayModel>)this.GetValue(LineNumbersProperty); }
            set { this.SetValue(LineNumbersProperty, value); }
        }

        #endregion

        public LineNumbersListView()
        {
            InitializeComponent();
            this.LineNumbers = new ObservableCollection<LineNumberDisplayModel>();
        }
    }
}
