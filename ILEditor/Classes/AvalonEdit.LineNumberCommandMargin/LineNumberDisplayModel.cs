using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.AvalonEdit.LineNumberCommandMargin
{
    public class LineNumberDisplayModel : WPFHelpers.ViewModelBase
    {

        public event Action<object, EventArgs> SubmitAllLineNumberCommands;

        public class LineNumberEventArgs : EventArgs
        {
            public int LineNumber { get; set; }
        }

        public event Action<object, EventArgs> AvalonEditKeyPressShouldCauseTransitionToCommandMode;
        public event Action<object, LineNumberEventArgs> CommandModeKeyPressShouldCauseTransitionToAvalonEditLine;



        public int LineNumber
        {
            get { return this.GetValue(() => this.LineNumber); }
            set { this.SetValue(() => this.LineNumber, value); }
        }

        public string CommandText
        {
            get { return this.GetValue(() => this.CommandText); }
            set {
                this.SetValue(() => this.CommandText, value);
                OnPropertyChanged(nameof(HasCommandText));
            }
        }

        public bool HasCommandText
        {
            get
            {
                return !string.IsNullOrWhiteSpace(CommandText);
            }
        }

        // each line number is a specific height
        public double ControlHeight
        {
            get { return this.GetValue(() => this.ControlHeight); }
            set { this.SetValue(() => this.ControlHeight, value); }
        }


        public bool IsInView
        {
            get { return this.GetValue(() => this.IsInView); }
            set { this.SetValue(() => this.IsInView, value); }
        }

        // this decided wether to show textbox or label for command
        public bool IsCommandEntryVisible
        {
            get { return this.GetValue(() => this.IsCommandEntryVisible); }
            set { this.SetValue(() => this.IsCommandEntryVisible, value); }
        }





        public void signalSubmitAllCommands()
        {
            if( this.SubmitAllLineNumberCommands != null)
            {
                this.SubmitAllLineNumberCommands(this, new EventArgs());
            }
        }



        public void signalAvalonEditArrowKeyShouldCauseCommandMode()
        {
            // user is on a line and they've used the arrow key all the way to the left which should make the cursor jump to command mode
            if( this.AvalonEditKeyPressShouldCauseTransitionToCommandMode != null)
            {
                this.AvalonEditKeyPressShouldCauseTransitionToCommandMode(this, new EventArgs());
            }
        }

        public void signalCommandModeArrowKeyShouldCauseCursorToBeOnAvalonEdit()
        {
            // user was in command mode and they'ved arrowed all the way to the right which should cause command mode to end
            //   - then the cursor should go to the avalonedit line
            if( this.CommandModeKeyPressShouldCauseTransitionToAvalonEditLine != null)
            {
                this.CommandModeKeyPressShouldCauseTransitionToAvalonEditLine(this, new LineNumberEventArgs
                {
                    LineNumber = this.LineNumber
                });
            }
        }


    }
}
