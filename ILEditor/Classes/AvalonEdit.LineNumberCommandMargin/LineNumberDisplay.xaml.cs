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

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(LineNumberDisplayModel), typeof(LineNumberDisplay), new PropertyMetadata { PropertyChangedCallback = new PropertyChangedCallback( OnModelChanged) });

        public LineNumberDisplayModel Model
        {
            get { return (LineNumberDisplayModel)this.GetValue(ModelProperty); }
            set { this.SetValue(ModelProperty, value); }
        }

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var ctrl = d as LineNumberDisplay;
            ctrl.InitModel();
        }

        #endregion

        public LineNumberDisplay()
        {
            InitializeComponent();

        }


        public void InitModel()
        {
            // hookup events and other things
            var model = this.DataContext as LineNumberDisplayModel;

            // different things can signal us to turn on command mode
            model.OnSignalToTurnOnCommandMode += Model_OnSignalToTurnOnCommandMode;
        }

        private void Model_OnSignalToTurnOnCommandMode(object arg1, EventArgs arg2)
        {
            var model = this.DataContext as LineNumberDisplayModel;
            model.IsCommandEntryVisible = true;
            // focus the textbox
            lineNumberCommandTextBox.Focus();
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
            // they may have entered a command that got rid of the line
            if( context != null)
            {
                context.IsCommandEntryVisible = false;
            }
            
        }

        private void lineNumberCommandTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // clicked a command area so go to command mode
            var context = this.DataContext as LineNumberDisplayModel;
            context.IsCommandEntryVisible = true;
            lineNumberCommandTextBox.Focus();
        }

        private void lineNumberCommandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var model = this.DataContext as LineNumberDisplayModel;
            if( e.Key == Key.Return)
            {
                // signal command submitted
                model.signalSubmitAllCommands();
            }
        }

        private void lineNumberCommandTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var model = this.DataContext as LineNumberDisplayModel;
            //arrow keys only available on this event.  They don't show up on KeyDown
            if (e.Key == Key.Right && 
                ( lineNumberCommandTextBox.Text.Length == 0 ||
                lineNumberCommandTextBox.CaretIndex == lineNumberCommandTextBox.Text.Length - 1
                )
                )
            {
                // signal that we should move to avalonedit
                model.IsCommandEntryVisible = false;
                model.signalCommandModeArrowKeyShouldCauseCursorToBeOnAvalonEdit();// I bet we need to pass line number later
            } else if( e.Key == Key.Up)
            {
                // signal that we should move up a line number command
                model.IsCommandEntryVisible = false;
                model.signalUpArrowKeyPressedInCommandTextBox();
            } else if( e.Key == Key.Down)
            {
                // signal that we should move down a line number command
                model.IsCommandEntryVisible = false;
                model.signalDownArrowKeyPressedInCommandTextBox();
            }
        }
    }



}
