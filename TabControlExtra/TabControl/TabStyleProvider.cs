/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
    [System.ComponentModel.ToolboxItem(false)]
	public abstract class TabStyleProvider : Component
	{
		#region Constructor
		
		protected TabStyleProvider(TabControlExtra tabControl){
			this.TabControl = tabControl;
			
			this._FocusColor = Color.Orange;
			
			if (this.TabControl.RightToLeftLayout){
				this._ImageAlign = ContentAlignment.MiddleRight;
			} else {
				this._ImageAlign = ContentAlignment.MiddleLeft;
			}
			
			this.HotTrack = true;

            this.Padding = new Point(6, 3);

		}
		
		#endregion

		#region Factory Methods
		
		public static TabStyleProvider CreateProvider(TabControlExtra tabControl){
			TabStyleProvider provider;
			
			//	Depending on the display style of the tabControl generate an appropriate provider.
			switch (tabControl.DisplayStyle) {
				case TabStyle.None:
					provider = new TabStyleNoneProvider(tabControl);
					break;
					
				case TabStyle.Default:
					provider = new TabStyleDefaultProvider(tabControl);
					break;
					
				case TabStyle.Angled:
					provider = new TabStyleAngledProvider(tabControl);
					break;
					
				case TabStyle.Rounded:
					provider = new TabStyleRoundedProvider(tabControl);
					break;
					
				case TabStyle.VisualStudio:
					provider = new TabStyleVisualStudioProvider(tabControl);
					break;
					
				case TabStyle.Chrome:
					provider = new TabStyleChromeProvider(tabControl);
					break;
					
				case TabStyle.IE8:
					provider = new TabStyleIE8Provider(tabControl);
					break;

				case TabStyle.VS2010:
					provider = new TabStyleVS2010Provider(tabControl);
					break;

                case TabStyle.Rectangular:
                    provider = new TabStyleRectangularProvider(tabControl);
                    break;

				default:
					provider = new TabStyleDefaultProvider(tabControl);
					break;
			}
			
			provider._Style = tabControl.DisplayStyle;
			return provider;
		}
		
		#endregion
		
		#region	Instance variables

        protected TabControlExtra TabControl { get; private set; }

		private Point _Padding;
		private bool _HotTrack;
		private TabStyle _Style = TabStyle.Default;
		
		
		private ContentAlignment _ImageAlign;
		private int _Radius = 1;
		private int _Overlap;
		private bool _FocusTrack;
		private float _Opacity = 1;
		private bool _ShowTabCloser;
        private bool _SelectedTabIsLarger;

        private BlendStyle _BlendStyle = BlendStyle.Normal;

        private Color _BorderColorDisabled = Color.Empty;
        private Color _BorderColorFocused = Color.Empty;
		private Color _BorderColorHighlighted = Color.Empty;
        private Color _BorderColorSelected = Color.Empty;
        private Color _BorderColorUnselected = Color.Empty;

        private Color _CloserColorFocused = SystemColors.ControlDark;
        private Color _CloserColorFocusedActive = SystemColors.ControlDark;
        private Color _CloserColorSelected = SystemColors.ControlDark;
        private Color _CloserColorSelectedActive = SystemColors.ControlDark;
        private Color _CloserColorHighlighted = SystemColors.ControlDark;
        private Color _CloserColorHighlightedActive = SystemColors.ControlDark;
        private Color _CloserColorUnselected = Color.Empty;

        private Color _CloserButtonFillColorFocused = Color.Empty;
        private Color _CloserButtonFillColorFocusedActive = Color.Empty;
        private Color _CloserButtonFillColorSelected = Color.Empty;
        private Color _CloserButtonFillColorSelectedActive = Color.Empty;
        private Color _CloserButtonFillColorHighlighted = Color.Empty;
        private Color _CloserButtonFillColorHighlightedActive = Color.Empty;
        private Color _CloserButtonFillColorUnselected = Color.Empty;

        private Color _CloserButtonOutlineColorFocused = Color.Empty;
        private Color _CloserButtonOutlineColorFocusedActive = Color.Empty;
        private Color _CloserButtonOutlineColorSelected = Color.Empty;
        private Color _CloserButtonOutlineColorSelectedActive = Color.Empty;
        private Color _CloserButtonOutlineColorHighlighted = Color.Empty;
        private Color _CloserButtonOutlineColorHighlightedActive = Color.Empty;
        private Color _CloserButtonOutlineColorUnselected = Color.Empty;

		private Color _FocusColor = Color.Empty;

        private Color _PageBackgroundColorDisabled = Color.Empty;
        private Color _PageBackgroundColorFocused = Color.Empty;
        private Color _PageBackgroundColorHighlighted = Color.Empty;
        private Color _PageBackgroundColorSelected = Color.Empty;
        private Color _PageBackgroundColorUnselected = Color.Empty;

        private Color _TabColorDisabled1 = Color.Empty;
        private Color _TabColorDisabled2 = Color.Empty;
        private Color _TabColorFocused1 = Color.Empty;
        private Color _TabColorFocused2 = Color.Empty;
        private Color _TabColorSelected1 = Color.Empty;
        private Color _TabColorSelected2 = Color.Empty;
        private Color _TabColorUnSelected1 = Color.Empty;
        private Color _TabColorUnSelected2 = Color.Empty;
        private Color _TabColorHighLighted1 = Color.Empty;
        private Color _TabColorHighLighted2 = Color.Empty;

        private Color _TextColorDisabled = Color.Empty;
        private Color _TextColorFocused = Color.Empty;
        private Color _TextColorHighlighted = Color.Empty;
        private Color _TextColorSelected = Color.Empty;
        private Color _TextColorUnselected = Color.Empty;
		
        private Padding _TabPageMargin = new Padding(1);

        private int _TabPageRadius = 0;
		
		#endregion
		
		#region overridable Methods
		
		public virtual void AddTabBorder(GraphicsPath path, Rectangle tabBounds) {
            switch (this.TabControl.Alignment) {
                case TabAlignment.Top:
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    break;
                case TabAlignment.Bottom:
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    break;
                case TabAlignment.Left:
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    break;
                case TabAlignment.Right:
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    break;
            }
        }

        public virtual Rectangle GetTabRect(Rectangle baseTabRect, Rectangle pageBounds, bool tabIsSelected) {
            var tabRect = baseTabRect;

            //	Adjust to meet the tabpage
            switch (this.TabControl.Alignment) {
                case TabAlignment.Top:
                    tabRect.Height += pageBounds.Top - tabRect.Bottom;
                    break;
                case TabAlignment.Bottom:
                    tabRect.Height += tabRect.Top - pageBounds.Bottom;
                    tabRect.Y -= tabRect.Top - pageBounds.Bottom;
                    break;
                case TabAlignment.Left:
                    tabRect.Width += pageBounds.Left - tabRect.Right;
                    break;
                case TabAlignment.Right:
                    tabRect.Width += tabRect.Left - pageBounds.Right;
                    tabRect.X -= tabRect.Left - pageBounds.Right;
                    break;
            }

            if (this.SelectedTabIsLarger) tabRect = EnlargeTab(tabRect, tabIsSelected);

            //	Create Overlap
            if (this.TabControl.Alignment <= TabAlignment.Bottom) {
                tabRect.X -= this._Overlap;
                tabRect.Width += this._Overlap;
            } else {
                tabRect.Y -= this._Overlap;
                tabRect.Height += this._Overlap;
            }

            tabRect = this.EnsureTabIsInView(tabRect, pageBounds);

            return tabRect;
        }

        private Rectangle EnlargeTab(Rectangle tabBounds, bool tabIsSelected) {
            Rectangle newTabBounds = tabBounds;
            int widthIncrement = (int)(tabIsSelected ? 1 : 0);
            int heightIncrement = (int)(tabIsSelected ? 1 : -1);

            switch (this.TabControl.Alignment) {
                case TabAlignment.Top:
                    newTabBounds.Y -= heightIncrement;
                    newTabBounds.Height += heightIncrement;
                    newTabBounds.X -= widthIncrement;
                    newTabBounds.Width += 2 * widthIncrement;
                    break;
                case TabAlignment.Bottom:
                    newTabBounds.Height += heightIncrement;
                    newTabBounds.X -= widthIncrement;
                    newTabBounds.Width += 2 * widthIncrement;
                    break;
                case TabAlignment.Left:
                    newTabBounds.X -= heightIncrement;
                    newTabBounds.Width += heightIncrement;
                    newTabBounds.Y -= widthIncrement;
                    newTabBounds.Height += 2 * widthIncrement;
                    break;
                case TabAlignment.Right:
                    newTabBounds.Width += heightIncrement;
                    newTabBounds.Y -= widthIncrement;
                    newTabBounds.Height += 2 * widthIncrement;
                    break;
            }
            return newTabBounds;
        }

        protected virtual Rectangle EnsureTabIsInView(Rectangle tabBounds, Rectangle pageBounds) {
			//	Adjust tab to fit within the page bounds.
			//	Make sure we only reposition visible tabs, as we may have scrolled out of view.

            if (!this.TabControl.IsTabVisible(tabBounds, pageBounds)) return tabBounds;

            var newTabBounds = tabBounds;

            switch (this.TabControl.Alignment) {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    if (newTabBounds.X <= pageBounds.X + 4) newTabBounds.X = pageBounds.X;
                    newTabBounds.Intersect(new Rectangle(pageBounds.X, tabBounds.Y, pageBounds.Width, tabBounds.Height));
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    if (newTabBounds.Y <= pageBounds.Y + 4) newTabBounds.Y = pageBounds.Y;
                    newTabBounds.Intersect(new Rectangle(tabBounds.X,pageBounds.Y,tabBounds.Width,pageBounds.Height));
                    break;
            }

            return newTabBounds;
		}

        #endregion
		
		#region	Base Properties

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TabStyle DisplayStyle {
			get { return this._Style; }
			set { 
                this._Style = value;
            }
		}

        public BlendStyle BlendStyle {
            get { return this._BlendStyle; }
            set {
                this._BlendStyle = value;
                this.TabControl.Invalidate();
            }
        }

        [Category("Appearance")]
		public ContentAlignment ImageAlign {
			get { return this._ImageAlign; }
			set {
				this._ImageAlign = value;
			}
		}
		
		[Category("Appearance")]
		public Point Padding {
			get { return this._Padding; }
			set {
				this._Padding = value;
				if (this._ShowTabCloser){
					if (value.X + (int)(this._Radius/2) < -TabControlExtra.TabCloserButtonSize){
						((TabControl)this.TabControl).Padding = new Point(0, value.Y);
					} else {
                        ((TabControl)this.TabControl).Padding = new Point(value.X + this._Radius + (int)(TabControlExtra.TabCloserButtonSize + 10) / 2, value.Y);
					}
				} else {
					if (value.X + (int)(this._Radius/2) < 1){
						((TabControl)this.TabControl).Padding = new Point(0, value.Y);
					} else {
                        ((TabControl)this.TabControl).Padding = new Point(value.X + this._Radius, value.Y);
					}
				}
			}
		}


		[Category("Appearance"), DefaultValue(1), Browsable(true)]
		public int Radius {
			get { return this._Radius; }
			set {
				if (value < 1) throw new ArgumentException("The radius cannot be less than 1", "value");
				
				this._Radius = value;
				//	Adjust padding
				this.Padding = this._Padding;
			}
		}

		[Category("Appearance")]
		public int Overlap {
			get { return this._Overlap; }
			set {
				if (value < 0){
					throw new ArgumentException("The tabs cannot have a negative overlap", "value");
				}
				this._Overlap = value;
			}
		}
		
		
		[Category("Appearance")]
		public bool FocusTrack {
			get { return this._FocusTrack; }
			set {
				this._FocusTrack = value;
			}
		}
		
		[Category("Appearance")]
		public bool HotTrack {
			get { return this._HotTrack; }
			set {
				this._HotTrack = value;
				((TabControl)this.TabControl).HotTrack = value;
			}
		}

        [Category("Appearance")]
        public bool SelectedTabIsLarger {
            get { return this._SelectedTabIsLarger; }
			set {
                this._SelectedTabIsLarger = value;
                this.TabControl.Invalidate();
			}
		}
		
            [Category("Appearance")]
		public bool ShowTabCloser {
			get { return this._ShowTabCloser; }
			set {
				this._ShowTabCloser = value;
				//	Adjust padding
				this.Padding = this._Padding;
			}
		}

		[Category("Appearance")]
		public float Opacity {
			get { return this._Opacity; }
			set {
				if (value < 0){
					throw new ArgumentException("The opacity must be between 0 and 1", "value");
				}
				if (value > 1){
					throw new ArgumentException("The opacity must be between 0 and 1", "value");
				}
				this._Opacity = value;
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorDisabled {
            get {
                if (this._BorderColorDisabled.IsEmpty) {
                    return SystemColors.ControlLight;
                } else {
                    return this._BorderColorDisabled;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlLight)) {
                    this._BorderColorDisabled = Color.Empty;
                } else {
                    this._BorderColorDisabled = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorFocused {
            get {
                if (this._BorderColorFocused.IsEmpty) {
                    return ThemedColors.ToolBorder;
                } else {
                    return this._BorderColorFocused;
                }
            }
            set {
                if (!value.Equals(this.BorderColorFocused)) {
                    if (value.Equals(ThemedColors.ToolBorder)) {
                        this._BorderColorFocused = Color.Empty;
                    } else {
                        this._BorderColorFocused = value;
                    }
                }
            }
        }

		[Category("Appearance"), DefaultValue(typeof(Color), "")]
		public Color BorderColorHighlighted
		{
			get {
				if (this._BorderColorHighlighted.IsEmpty){
					return SystemColors.ControlDark;
				} else {
					return this._BorderColorHighlighted;
				}
			}
			set {
				if (value.Equals(SystemColors.ControlDark)){
					this._BorderColorHighlighted = Color.Empty;
				} else {
					this._BorderColorHighlighted = value;
				}
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorSelected {
            get {
                if (this._BorderColorSelected.IsEmpty) {
                    return SystemColors.ControlDark;
                } else {
                    return this._BorderColorSelected;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlDark)) {
                    this._BorderColorSelected = Color.Empty;
                } else {
                    this._BorderColorSelected = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
		public Color BorderColorUnselected
		{
			get {
				if (this._BorderColorUnselected.IsEmpty){
					return SystemColors.ControlDark;
				} else {
					return this._BorderColorUnselected;
				}
			}
			set {
				if (value.Equals(SystemColors.ControlDark)){
					this._BorderColorUnselected = Color.Empty;
				} else {
					this._BorderColorUnselected = value;
				}
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorDisabled {
            get {
                if (this._PageBackgroundColorDisabled.IsEmpty) {
                    return SystemColors.Control;
                } else {
                    return this._PageBackgroundColorDisabled;
                }
            }
            set {
                this._PageBackgroundColorDisabled = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorFocused {
            get {
                if (this._PageBackgroundColorFocused.IsEmpty) {
                    return SystemColors.ControlLight;
                } else {
                    return this._PageBackgroundColorFocused;
                }
            }
            set {
                this._PageBackgroundColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorHighlighted {
            get {
                if (this._PageBackgroundColorHighlighted.IsEmpty) {
                    return PageBackgroundColorUnselected;
                } else {
                    return this._PageBackgroundColorHighlighted;
                }
            }
            set {
                this._PageBackgroundColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorSelected {
            get {
                if (this._PageBackgroundColorSelected.IsEmpty) {
                    return SystemColors.ControlLightLight;
                } else {
                    return this._PageBackgroundColorSelected;
                }
            }
            set {
                this._PageBackgroundColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorUnselected {
            get {
                if (this._PageBackgroundColorUnselected.IsEmpty) {
                    return SystemColors.Control;
                } else {
                    return this._PageBackgroundColorUnselected;
                }
            }
            set {
                this._PageBackgroundColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorDisabled1 {
            get {
                if (this._TabColorDisabled1.IsEmpty) {
                    return PageBackgroundColorDisabled;
                } else {
                    return this._TabColorDisabled1;
                }
            }
            set {
                this._TabColorDisabled1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorDisabled2 {
            get {
                if (this._TabColorDisabled2.IsEmpty) {
                    return TabColorDisabled1;
                } else {
                    return this._TabColorDisabled2;
                }
            }
            set {
                this._TabColorDisabled2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorFocused1 {
            get {
                if (this._TabColorFocused1.IsEmpty) {
                    return PageBackgroundColorFocused;
                } else {
                    return this._TabColorFocused1;
                }
            }
            set {
                this._TabColorFocused1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorFocused2 {
            get {
                if (this._TabColorFocused2.IsEmpty) {
                    return TabColorFocused1;
                } else {
                    return this._TabColorFocused2;
                }
            }
            set {
                this._TabColorFocused2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorSelected1 {
            get {
                if (this._TabColorSelected1.IsEmpty) {
                    return PageBackgroundColorSelected;
                } else {
                    return this._TabColorSelected1;
                }
            }
            set {
                this._TabColorSelected1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorSelected2 {
            get {
                if (this._TabColorSelected2.IsEmpty) {
                    return TabColorSelected1;
                } else {
                    return this._TabColorSelected2;
                }
            }
            set {
                this._TabColorSelected2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorUnSelected1 {
            get {
                if (this._TabColorUnSelected1.IsEmpty) {
                    return PageBackgroundColorUnselected;
                } else {
                    return this._TabColorUnSelected1;
                }
            }
            set {
                this._TabColorUnSelected1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorUnSelected2 {
            get {
                if (this._TabColorUnSelected2.IsEmpty) {
                    return TabColorUnSelected1;
                } else {
                    return this._TabColorUnSelected2;
                }
            }
            set {
                this._TabColorUnSelected2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorHighLighted1 {
            get {
                if (this._TabColorHighLighted1.IsEmpty) {
                    return PageBackgroundColorHighlighted;
                } else {
                    return this._TabColorHighLighted1;
                }
            }
            set {
                this._TabColorHighLighted1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorHighLighted2 {
            get {
                if (this._TabColorHighLighted2.IsEmpty) {
                    return TabColorHighLighted1;
                } else {
                    return this._TabColorHighLighted2;
                }
            }
            set {
                this._TabColorHighLighted2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorDisabled {
            get {
                if (this._TextColorUnselected.IsEmpty) {
                    return SystemColors.ControlDark;
                } else {
                    return this._TextColorDisabled;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlDark)) {
                    this._TextColorDisabled = Color.Empty;
                } else {
                    this._TextColorDisabled = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorFocused {
            get {
                if (this._TextColorFocused.IsEmpty) {
                    return TextColorSelected;
                } else {
                    return this._TextColorFocused;
                }
            }
            set {
                this._TextColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorHighlighted {
            get {
                if (this._TextColorHighlighted.IsEmpty) {
                    return TextColorUnselected;
                } else {
                    return this._TextColorHighlighted;
                }
            }
            set {
                this._TextColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
		public Color TextColorSelected
		{
			get {
				if (this._TextColorSelected.IsEmpty){
					return SystemColors.ControlText;
				} else {
					return this._TextColorSelected;
				}
			}
			set {
				if (value.Equals(SystemColors.ControlText)){
					this._TextColorSelected = Color.Empty;
				} else {
					this._TextColorSelected = value;
				}
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorUnselected {
            get {
                if (this._TextColorUnselected.IsEmpty) {
                    return SystemColors.ControlText;
                } else {
                    return this._TextColorUnselected;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlText)) {
                    this._TextColorUnselected = Color.Empty;
                } else {
                    this._TextColorUnselected = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "Orange")]
		public Color FocusColor
		{
			get { return this._FocusColor; }
			set { this._FocusColor = value;
			}
		}

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorFocused {
            get { return this._CloserColorFocused; }
            set {
                this._CloserColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorFocusedActive {
            get { return this._CloserColorFocusedActive; }
            set {
                this._CloserColorFocusedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorSelected {
            get { return this._CloserColorSelected; }
            set {
                this._CloserColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorSelectedActive {
            get { return this._CloserColorSelectedActive; }
            set {
                this._CloserColorSelectedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorHighlighted {
            get { return this._CloserColorHighlighted; }
            set {
                this._CloserColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorHighlightedActive {
            get { return this._CloserColorHighlightedActive; }
            set {
                this._CloserColorHighlightedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserColorUnselected {
            get { return this._CloserColorUnselected; }
            set {
                this._CloserColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorFocused {
            get { return this._CloserButtonFillColorFocused; }
            set {
                this._CloserButtonFillColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorFocusedActive {
            get { return this._CloserButtonFillColorFocusedActive; }
            set {
                this._CloserButtonFillColorFocusedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorSelected {
            get { return this._CloserButtonFillColorSelected ; }
            set {
                this._CloserButtonFillColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorSelectedActive {
            get { return this._CloserButtonFillColorSelectedActive; }
            set {
                this._CloserButtonFillColorSelectedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorHighlighted {
            get { return this._CloserButtonFillColorHighlighted; }
            set {
                this._CloserButtonFillColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorHighlightedActive {
            get { return this._CloserButtonFillColorHighlightedActive; }
            set {
                this._CloserButtonFillColorHighlightedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorUnselected {
            get { return this._CloserButtonFillColorUnselected; }
            set {
                this._CloserButtonFillColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorFocused {
            get { return this._CloserButtonOutlineColorFocused; }
            set {
                this._CloserButtonOutlineColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorFocusedActive {
            get { return this._CloserButtonOutlineColorFocusedActive; }
            set {
                this._CloserButtonOutlineColorFocusedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorSelected {
            get { return this._CloserButtonOutlineColorSelected; }
            set {
                this._CloserButtonOutlineColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorSelectedActive {
            get { return this._CloserButtonOutlineColorSelectedActive; }
            set {
                this._CloserButtonOutlineColorSelectedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorHighlighted {
            get { return this._CloserButtonOutlineColorHighlighted; }
            set {
                this._CloserButtonOutlineColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorHighlightedActive {
            get { return this._CloserButtonOutlineColorHighlightedActive; }
            set {
                this._CloserButtonOutlineColorHighlightedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorUnselected {
            get { return this._CloserButtonOutlineColorUnselected; }
            set {
                this._CloserButtonOutlineColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Padding), "{1,1,1,1}")]
        public Padding TabPageMargin 
        {
            get {return this._TabPageMargin;}
            set {
                if (value.Left < 0) value.Left = 0;
                if (value.Right< 0) value.Right = 0;
                if (value.Top < 0) value.Top = 0;
                if (value.Bottom < 0) value.Bottom = 0;

                if (value.Left > 4) value.Left = 4;
                if (value.Right > 4) value.Right = 4;
                if (value.Top > 4) value.Top = 4;
                if (value.Bottom > 4) value.Bottom = 4;

                this._TabPageMargin = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(int), "0")]
        public int TabPageRadius {
            get { return this._TabPageRadius; }
            set { 
                if (value < 0) value = 0;
                if (value > 4) value = 4;
                this._TabPageRadius = value;
            }
        }

        #endregion

		#region Painting

        protected internal virtual void DrawTabCloser(GraphicsPath closerPath, GraphicsPath closerButtonPath, Graphics graphics, TabState state, Point mousePosition) {
            bool active = closerButtonPath.GetBounds().Contains(mousePosition);
            switch (state) {
                case TabState.Disabled:
                    DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorUnselected, this.CloserButtonFillColorUnselected, this.CloserButtonOutlineColorUnselected);
                    break;
                case TabState.Focused:
                    if (active) {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorFocusedActive, this.CloserButtonFillColorFocusedActive, this.CloserButtonOutlineColorFocusedActive);
                    } else {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorFocused, this.CloserButtonFillColorFocused, this.CloserButtonOutlineColorFocused);
                    }
                    break;
                case TabState.Highlighted:
                    if (active) {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorHighlightedActive, this.CloserButtonFillColorHighlightedActive, this.CloserButtonOutlineColorHighlightedActive);
                    } else {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorHighlighted, this.CloserButtonFillColorHighlighted, this.CloserButtonOutlineColorHighlighted);
                    }
                    break;
                case TabState.Selected:
                    if (active) {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorSelectedActive, this.CloserButtonFillColorSelectedActive, this.CloserButtonOutlineColorSelectedActive);
                    } else {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorSelected, this.CloserButtonFillColorSelected, this.CloserButtonOutlineColorSelected);
                    }
                    break;
                case TabState.Unselected:
                    DrawTabCloser(closerPath, closerButtonPath, graphics, this.CloserColorUnselected, this.CloserButtonFillColorUnselected, this.CloserButtonOutlineColorUnselected);
                    break;
            }
        }

        private void DrawTabCloser(GraphicsPath closerPath, GraphicsPath closerButtonPath, Graphics graphics, Color closerColor, Color closerFillColor, Color closerOutlineColor) {
            if (closerButtonPath != null) {
                if (closerFillColor != Color.Empty) {
                    graphics.SmoothingMode = SmoothingMode.None;
                    using (Brush closerBrush = new SolidBrush(closerFillColor)) {
                        graphics.FillPath(closerBrush, closerButtonPath);
                    }
                }
                if (closerOutlineColor != Color.Empty) {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen closerPen = new Pen(closerOutlineColor)) {
                        graphics.DrawPath(closerPen, closerButtonPath);
                    }
                }
            }
            if (closerColor != Color.Empty) {
                using (Pen closerPen = new Pen(closerColor)) {
                    closerPen.Width = 1;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.DrawPath(closerPen, closerPath);
                }
            }
        }

        protected internal virtual GraphicsPath GetTabCloserButtonPath(Rectangle closerButtonRect) {
            GraphicsPath closerPath = new GraphicsPath();
            closerPath.AddLine(closerButtonRect.X, closerButtonRect.Y, closerButtonRect.Right, closerButtonRect.Y);
            closerPath.AddLine(closerButtonRect.Right, closerButtonRect.Y, closerButtonRect.Right, closerButtonRect.Bottom);
            closerPath.AddLine(closerButtonRect.Right, closerButtonRect.Bottom, closerButtonRect.X, closerButtonRect.Bottom);
            closerPath.AddLine(closerButtonRect.X, closerButtonRect.Bottom, closerButtonRect.X, closerButtonRect.Y);
            closerPath.CloseFigure();
            return closerPath;
        }

        public void DrawTabCloser(Rectangle closerButtonRect, Graphics graphics, TabState state, Point mousePosition) {
            if (!this._ShowTabCloser) return;
            using (var closerPath = GetTabCloserPath(closerButtonRect)) {
                using (var closerButtonPath = GetTabCloserButtonPath(closerButtonRect)) {
                    DrawTabCloser(closerPath, closerButtonPath, graphics, state, mousePosition);
                }
            }
        }

       protected internal virtual GraphicsPath GetTabCloserPath(Rectangle closerButtonRect) {
           GraphicsPath closerPath = new GraphicsPath();
           closerPath.AddLine(closerButtonRect.X + 4, closerButtonRect.Y + 4, closerButtonRect.Right - 4, closerButtonRect.Bottom - 4);
           closerPath.CloseFigure();
           closerPath.AddLine(closerButtonRect.Right - 4, closerButtonRect.Y + 4, closerButtonRect.X + 4, closerButtonRect.Bottom - 4);
           closerPath.CloseFigure();

           return closerPath;
       }

        public virtual void DrawTabFocusIndicator(GraphicsPath tabpath, TabState state, Graphics graphics) {
            if (this._FocusTrack && state == TabState.Focused) {
                Brush focusBrush = null;
                RectangleF pathRect = tabpath.GetBounds();
                Rectangle focusRect = Rectangle.Empty;
                switch (this.TabControl.Alignment) {
                    case TabAlignment.Top:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, this.FocusColor, SystemColors.Window, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Bottom:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Bottom - 4, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, this.FocusColor, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Left:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, this.FocusColor, SystemColors.ControlLight, LinearGradientMode.Horizontal);
                        break;
                    case TabAlignment.Right:
                        focusRect = new Rectangle((int)pathRect.Right - 4, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, this.FocusColor, LinearGradientMode.Horizontal);
                        break;
                }

                //	Ensure the focus strip does not go outside the tab
                Region focusRegion = new Region(focusRect);
                focusRegion.Intersect(tabpath);
                graphics.FillRegion(focusBrush, focusRegion);
                focusRegion.Dispose();
                focusBrush.Dispose();
            }
        }

        protected internal virtual void PaintTabBackground(GraphicsPath tabBorder, TabState state, Graphics graphics) {
            using (Brush fillBrush = GetTabBackgroundBrush(state, tabBorder)) {
                //	Paint the background
                graphics.FillPath(fillBrush, tabBorder);
            }
        }

        #endregion
		
		#region Background brushes

		public virtual Brush GetPageBackgroundBrush(TabState state){
            Color color = Color.Empty;

            switch (state) {
                case TabState.Disabled:
                    color = this.PageBackgroundColorDisabled;
                    break;
                case TabState.Focused:
                    color = this.PageBackgroundColorFocused;
                    break;
                case TabState.Highlighted:
                    color = this.PageBackgroundColorHighlighted;
                    break;
                case TabState.Selected:
                    color = this.PageBackgroundColorSelected;
                    break;
                case TabState.Unselected:
                    color = this.PageBackgroundColorUnselected;
                    break;
            }
            return new SolidBrush(color);
		}

        protected internal Brush GetTabBackgroundBrush(TabState state, GraphicsPath tabBorder) {
            Color color1 = GetTabBackgroundColor1(state,tabBorder);
            Color color2 = GetTabBackgroundColor2(state, tabBorder);

            return CreateTabBackgroundBrush(color1, color2, state, tabBorder);
        }

        protected internal virtual Brush CreateTabBackgroundBrush(Color color1, Color color2, TabState state, GraphicsPath tabBorder) {
            LinearGradientBrush fillBrush = null;

            //	Get the correctly aligned gradient
            var tabBounds = tabBorder.GetBounds();
            //tabBounds.Inflate(3, 3);
            //tabBounds.X -= 1;
            //tabBounds.Y -= 1;
            switch (this.TabControl.Alignment) {
                case TabAlignment.Top:
                    tabBounds.Height += 1;
                    fillBrush = new LinearGradientBrush(tabBounds, color2, color1, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Bottom:
                    fillBrush = new LinearGradientBrush(tabBounds, color1, color2, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Left:
                    fillBrush = new LinearGradientBrush(tabBounds, color2, color1, LinearGradientMode.Horizontal);
                    break;
                case TabAlignment.Right:
                    fillBrush = new LinearGradientBrush(tabBounds, color1, color2, LinearGradientMode.Horizontal);
                    break;
            }

            //	Add the blend
            fillBrush.Blend = GetBackgroundBlend();
            return fillBrush;
        }

        protected virtual Color GetTabBackgroundColor1(TabState state, GraphicsPath tabBorder) {
            Color color = Color.Empty;

            switch (state) {
                case TabState.Disabled:
                    color = this.TabColorDisabled1;
                    break;
                case TabState.Focused:
                    color = this.TabColorFocused1;
                    break;
                case TabState.Highlighted:
                    color = this.TabColorHighLighted1;
                    break;
                case TabState.Selected:
                    color = this.TabColorSelected1;
                    break;
                case TabState.Unselected:
                    color = this.TabColorUnSelected1;
                    break;
            }
            return color;
        }

        protected virtual Color GetTabBackgroundColor2(TabState state, GraphicsPath tabBorder) {
            Color color = Color.Empty;

            switch (state) {
                case TabState.Disabled:
                    color = this.TabColorDisabled2;
                    break;
                case TabState.Focused:
                    color = this.TabColorFocused2;
                    break;
                case TabState.Highlighted:
                    color = this.TabColorHighLighted2;
                    break;
                case TabState.Selected:
                    color = this.TabColorSelected2;
                    break;
                case TabState.Unselected:
                    color = this.TabColorUnSelected2;
                    break;
            }
            return color;
        }

        protected virtual Blend GetBackgroundBlend() {
            float[] relativeIntensities = new float[] { 0f, 0.7f, 1f };
            float[] relativePositions = new float[] { 0f, 0.6f, 1f };

            //	Glass look to top aligned tabs
            if (this.BlendStyle == BlendStyle.Glass) {
                relativeIntensities = new float[] { 0f, 0.5f, 1f, 1f };
                relativePositions = new float[] { 0f, 0.5f, 0.51f, 1f };
            }

            Blend blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;

            return blend;
        }

        #endregion
		
		#region Tab border and rect

        public GraphicsPath GetTabBorder(Rectangle tabBounds) {
			
			GraphicsPath path = new GraphicsPath();
			
			this.AddTabBorder(path, tabBounds);
			
			path.CloseFigure();
			return path;
		}

		#endregion
		
	}
}
