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

            turnOffCommandEntryTimer.Tick += TurnOffCommandEntryTimer_Tick;
        }

        DispatcherTimer turnOffCommandEntryTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 2)
        };

        private void TurnOffCommandEntryTimer_Tick(object sender, EventArgs e)
        {
            turnOffCommandEntryTimer.Stop();
            var model = this.DataContext as LineNumberDisplayModel;
            model.IsCommandEntryVisible = false;
        }



        private void lineNumberTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            var model = this.DataContext as LineNumberDisplayModel;

            model.IsCommandEntryVisible = true;
        }

        private void lineNumberCommandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // keep command entry going while they are typing
            turnOffCommandEntryTimer.Stop();
            turnOffCommandEntryTimer.Start();
        }
    }



}
