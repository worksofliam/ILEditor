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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{
    /// <summary>
    /// Interaction logic for LineNumberDisplay.xaml
    /// </summary>
    public partial class LineNumberDisplay : UserControl
    {

        #region Model DP

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(LineNumberDisplayModel), typeof(LineNumberDisplay));

        public LineNumberDisplayModel Model
        {
            get { return (LineNumberDisplayModel)this.GetValue(ModelProperty); }
            set { this.SetValue(ModelProperty, value); }
        }

        #endregion

        public LineNumberDisplay()
        {
            InitializeComponent();

        }

        private void lineNumberTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // clicked line number so go to command mode
            var context = this.DataContext as LineNumberDisplayModel;
            context.IsCommandEntryVisible = true;
            lineNumberCommandTextBox.Focus();
        }

        private void lineNumberCommandTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as LineNumberDisplayModel;
            context.IsCommandEntryVisible = false;
        }

        private void lineNumberCommandTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // clicked a command area so go to command mode
            var context = this.DataContext as LineNumberDisplayModel;
            context.IsCommandEntryVisible = true;
            lineNumberCommandTextBox.Focus();
        }


    }



}
