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

    }
}
