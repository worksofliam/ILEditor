/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Permissions;

namespace System.Windows.Forms {

    [ToolboxBitmapAttribute(typeof(TabControl))]
    public class TabControlExtra : TabControl {

        #region constants

        public const int TabCloserButtonSize = 15;

        #endregion

        #region	Construction

        public TabControlExtra() {

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);

            this._BackBuffer = new Bitmap(this.Width, this.Height);
            this._BackBufferGraphics = Graphics.FromImage(this._BackBuffer);
            this._TabBuffer = new Bitmap(this.Width, this.Height);
            this._TabBufferGraphics = Graphics.FromImage(this._TabBuffer);
            
            this.SuspendLayout();
            this.DisplayStyle = TabStyle.Default;
            this.ResumeLayout();

        }

        protected override void OnCreateControl() {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cp = base.CreateParams;
                if (this.EffectiveRightToLeft)
                    cp.ExStyle = cp.ExStyle | NativeMethods.WS_EX_LAYOUTRTL | NativeMethods.WS_EX_NOINHERITLAYOUT;
                return cp;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing) {
                if (this._BackImage != null) {
                    this._BackImage.Dispose();
                }
                if (this._BackBufferGraphics != null) {
                    this._BackBufferGraphics.Dispose();
                }
                if (this._BackBuffer != null) {
                    this._BackBuffer.Dispose();
                }
                if (this._TabBufferGraphics != null) {
                    this._TabBufferGraphics.Dispose();
                }
                if (this._TabBuffer != null) {
                    this._TabBuffer.Dispose();
                }

                if (this._StyleProvider != null) {
                    this._StyleProvider.Dispose();
                }
            }
        }

        #endregion

        #region Private variables

        private Bitmap _BackImage;
        private Bitmap _BackBuffer;
        private Graphics _BackBufferGraphics;
        private Bitmap _TabBuffer;
        private Graphics _TabBufferGraphics;

        private GraphicsPath _PrevTabCloserButtonPath;
        
        private int _oldValue;
        private Point _dragStartPosition = Point.Empty;

        private TabStyle _Style;
        private TabStyleProvider _StyleProvider;

        private List<TabPage> _TabPages;

        private bool _SuspendDrawing;

        #endregion

        #region Public properties

        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TabStyleProvider DisplayStyleProvider {
            get {
                if (this._StyleProvider == null) {
                    this.DisplayStyle = TabStyle.Default;
                }

                return this._StyleProvider;
            }
            set {
                this._StyleProvider = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(TabStyle), "Default"), RefreshProperties(RefreshProperties.All)]
        public TabStyle DisplayStyle {
            get { return this._Style; }
            set {
                if (this._Style != value) {
                    this._Style = value;
                    this._StyleProvider = TabStyleProvider.CreateProvider(this);
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Point MousePosition
        {
            get
            {
                Point loc = this.PointToClient(Control.MousePosition);
                loc = AdjustPointForRightToLeft(loc);
                return loc;
            }
        }

        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public new bool Multiline {
            get {
                return base.Multiline;
            }
            set {
                base.Multiline = value;
            }
        }


        //	Hide the Padding attribute so it can not be changed
        //	We are handling this on the Style Provider
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Point Padding {
            get { return this.DisplayStyleProvider.Padding; }
            set {
                this.DisplayStyleProvider.Padding = value;
            }
        }

        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public override bool RightToLeftLayout {
            get { return base.RightToLeftLayout; }
            set {
                base.RightToLeftLayout = value;
                this.UpdateStyles();
            }
        }

        //	Hide the HotTrack attribute so it can not be changed
        //	We are handling this on the Style Provider
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool HotTrack {
            get { return this.DisplayStyleProvider.HotTrack; }
            set {
                this.DisplayStyleProvider.HotTrack = value;
            }
        }

        [Category("Appearance")]
        public new TabAlignment Alignment {
            get { return base.Alignment; }
            set {
                base.Alignment = value;
                switch (value) {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        this.Multiline = false;
                        break;
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        this.Multiline = true;
                        break;
                }
            }
        }

        //	Hide the Appearance attribute so it can not be changed
        //	We don't want it as we are doing all the painting.
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
        public new TabAppearance Appearance {
            get {
                return base.Appearance;
            }
            set {
                //	Don't permit setting to other appearances as we are doing all the painting
                base.Appearance = TabAppearance.Normal;
            }
        }

        public override Rectangle DisplayRectangle {
            get {
                //	Special processing to hide tabs
                if (this._Style == TabStyle.None) {
                    return new Rectangle(0, 0, Width, Height);
                } else {
                    int tabStripHeight = 0;
                    int itemHeight = 0;

                    if (this.Alignment <= TabAlignment.Bottom) {
                        itemHeight = this.ItemSize.Height;
                    } else {
                        itemHeight = this.ItemSize.Width;
                    }

                    tabStripHeight = 5 + (itemHeight * this.RowCount);

                    Rectangle rect = new Rectangle(4, tabStripHeight, Width - 8, Height - tabStripHeight - 4);
                    switch (this.Alignment) {
                        case TabAlignment.Top:
                            rect = new Rectangle(4, tabStripHeight, Width - 8, Height - tabStripHeight - 4);
                            break;
                        case TabAlignment.Bottom:
                            rect = new Rectangle(4, 4, Width - 8, Height - tabStripHeight - 4);
                            break;
                        case TabAlignment.Left:
                            rect = new Rectangle(tabStripHeight, 4, Width - tabStripHeight - 4, Height - 8);
                            break;
                        case TabAlignment.Right:
                            rect = new Rectangle(4, 4, Width - tabStripHeight - 4, Height - 8);
                            break;
                    }
                    return rect;
                }
            }
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public int GetActiveIndex(Point mousePosition) {
            NativeMethods.TCHITTESTINFO hitTestInfo = new NativeMethods.TCHITTESTINFO(mousePosition);
            int index = NativeMethods.SendMessage(this.Handle, NativeMethods.TCM_HITTEST, IntPtr.Zero, NativeMethods.ToIntPtr(hitTestInfo)).ToInt32();
            if (index == -1) {
                return -1;
            } else {
                if (this.TabPages[index].Enabled) {
                    return index;
                } else {
                    return -1;
                }
            }
        }

        public TabPage GetActiveTab(Point mousePosition) {
            int activeIndex = this.GetActiveIndex(mousePosition);
            if (activeIndex > -1) {
                return this.TabPages[activeIndex];
            } else {
                return null;
            }
        }

        #endregion

        #region	Public methods

        public void HideTab(TabPage page) {
            if (page != null && this.TabPages.Contains(page)) {
                this.BackupTabPages();
                this.TabPages.Remove(page);
            }
        }

        public void HideTab(int index) {
            if (this.IsValidTabIndex(index)) {
                this.HideTab(this._TabPages[index]);
            }
        }

        public void HideTab(string key) {
            if (this.TabPages.ContainsKey(key)) {
                this.HideTab(this.TabPages[key]);
            }
        }

        public void ShowTab(TabPage page) {
            if (page != null) {
                if (this._TabPages != null) {
                    if (!this.TabPages.Contains(page)
                        && this._TabPages.Contains(page)) {

                        //	Get insert point from backup of pages
                        int pageIndex = this._TabPages.IndexOf(page);
                        if (pageIndex > 0) {
                            int start = pageIndex - 1;

                            //	Check for presence of earlier pages in the visible tabs
                            for (int index = start; index >= 0; index--) {
                                if (this.TabPages.Contains(this._TabPages[index])) {

                                    //	Set insert point to the right of the last present tab
                                    pageIndex = this.TabPages.IndexOf(this._TabPages[index]) + 1;
                                    break;
                                }
                            }
                        }

                        //	Insert the page, or add to the end
                        if ((pageIndex >= 0) && (pageIndex < this.TabPages.Count)) {
                            this.TabPages.Insert(pageIndex, page);
                        } else {
                            this.TabPages.Add(page);
                        }
                    }
                } else {

                    //	If the page is not found at all then just add it
                    if (!this.TabPages.Contains(page)) {
                        this.TabPages.Add(page);
                    }
                }
            }
        }

        public void ShowTab(int index) {
            if (this.IsValidTabIndex(index)) {
                this.ShowTab(this._TabPages[index]);
            }
        }

        public void ShowTab(string key) {
            if (this._TabPages != null) {
                TabPage tab = this._TabPages.Find(delegate(TabPage page) { return page.Name.Equals(key, StringComparison.OrdinalIgnoreCase); });
                this.ShowTab(tab);
            }
        }

        public void ResumeDrawing() {
            _SuspendDrawing = false;
        }

        public void SuspendDrawing() {
            _SuspendDrawing = true;
        }

        #endregion

        #region Drag 'n' Drop

        protected override void OnMouseDown(MouseEventArgs e) {
            var mousePosition = new Point(e.X, e.Y);
            int index = this.GetActiveIndex(mousePosition);
            if (!this.DesignMode && index > -1 && this._StyleProvider.ShowTabCloser && this.GetTabCloserButtonRect(index).Contains(mousePosition)) {

                //	If we are clicking on a closer then remove the tab instead of raising the standard mouse down event
                //	But raise the tab closing event first
                TabPage tab = this.GetActiveTab(mousePosition);
                TabControlCancelEventArgs args = new TabControlCancelEventArgs(tab, index, false, TabControlAction.Deselecting);
                this.OnTabClosing(args);
            } else {
                base.OnMouseDown(e);
                if (this.AllowDrop) {
                    this._dragStartPosition = new Point(e.X, e.Y);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            if (this.AllowDrop) {
                this._dragStartPosition = Point.Empty;
            }
        }

        protected override void OnDragOver(DragEventArgs drgevent) {
            base.OnDragOver(drgevent);

            if (drgevent.Data.GetDataPresent(typeof(TabPage))) {

                TabPage dragTab = (TabPage)drgevent.Data.GetData(typeof(TabPage));
                this.Cursor = Cursors.Arrow;
                dragTab.Cursor = Cursors.Arrow;

                if (this.GetActiveTab(new Point(drgevent.X,drgevent.Y)) == dragTab) {
                    return;
                }

                int insertPoint = this.GetActiveIndex(new Point(drgevent.X, drgevent.Y));
                if (insertPoint < 0) return;

                SuspendDrawing();

                //	Remove from current position (could be another tabcontrol)
                ((TabControl)dragTab.Parent).TabPages.Remove(dragTab);

                //	Add to current position
                this.TabPages.Insert(insertPoint, dragTab);
                this.SelectedTab = dragTab;

                ResumeDrawing();

                this.Invalidate();

                //	deal with hidden tab handling?
            } else {
                drgevent.Effect = DragDropEffects.None;
            }
        }

        private void StartDragDrop() {
            if (!this._dragStartPosition.IsEmpty) {
                TabPage dragTab = this.SelectedTab;
                if (dragTab != null) {
                    //	Test for movement greater than the drag activation trigger area
                    Rectangle dragTestRect = new Rectangle(this._dragStartPosition, Size.Empty);
                    dragTestRect.Inflate(SystemInformation.DragSize);
                    Point pt = this.PointToClient(Control.MousePosition);
                    if (!dragTestRect.Contains(pt)) {
                        this.DoDragDrop(dragTab, DragDropEffects.Move);
                        this._dragStartPosition = Point.Empty;
                    }
                }
            }
        }

        #endregion

        #region Events

        [Category("Action")]
        public event ScrollEventHandler HScroll;
        [Category("Action")]
        public event EventHandler<TabControlEventArgs> TabImageClick;
        [Category("Action")]
        public event EventHandler<TabControlCancelEventArgs> TabClosing;
        [Category("Action")]
        public event EventHandler<TabControlEventArgs> TabClosed;

        #endregion

        #region	Base class event processing

        protected override void OnFontChanged(EventArgs e) {
            IntPtr hFont = this.Font.ToHfont();
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETFONT, hFont, (IntPtr)(-1));
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
        }

        private void createGraphicsBuffers()
        {
            //	Recreate the buffer for manual double buffering
            if (this.Width > 0 && this.Height > 0)
            {
                if (this._BackImage != null)
                {
                    this._BackImage.Dispose();
                    this._BackImage = null;
                }
                if (this._BackBufferGraphics != null)
                {
                    this._BackBufferGraphics.Dispose();
                }
                if (this._BackBuffer != null)
                {
                    this._BackBuffer.Dispose();
                }

                this._BackBuffer = new Bitmap(this.Width, this.Height);
                this._BackBufferGraphics = Graphics.FromImage(this._BackBuffer);

                if (this._TabBufferGraphics != null)
                {
                    this._TabBufferGraphics.Dispose();
                }
                if (this._TabBuffer != null)
                {
                    this._TabBuffer.Dispose();
                }

                this._TabBuffer = new Bitmap(this.Width, this.Height);
                this._TabBufferGraphics = Graphics.FromImage(this._TabBuffer);

                if (this._BackImage != null)
                {
                    this._BackImage.Dispose();
                    this._BackImage = null;
                }
            }
        }

        protected override void OnResize(EventArgs e) {
            //var start = DateTime.Now;
            createGraphicsBuffers();
            base.OnResize(e);
            //System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " TabControl " + this.GetHashCode() + " resized: " + DateTime.Now.Subtract(start).TotalMilliseconds + "ms; size: " + this.Size.ToString() + " location: " + this.Location.ToString());
        }

        protected override void OnParentBackColorChanged(EventArgs e) {
            if (this._BackImage != null) {
                this._BackImage.Dispose();
                this._BackImage = null;
            }
            base.OnParentBackColorChanged(e);
        }

        protected override void OnParentBackgroundImageChanged(EventArgs e) {
            if (this._BackImage != null) {
                this._BackImage.Dispose();
                this._BackImage = null;
            }
            base.OnParentBackgroundImageChanged(e);
        }

        protected override void OnSelecting(TabControlCancelEventArgs e) {
            //	Do not allow selecting of disabled tabs
            if (e.Action == TabControlAction.Selecting && e.TabPage != null && !e.TabPage.Enabled) {
                e.Cancel = true;
            }
            base.OnSelecting(e);
        }

        protected override void OnMove(EventArgs e) {
            if (this.Width > 0 && this.Height > 0) {
                if (this._BackImage != null) {
                    this._BackImage.Dispose();
                    this._BackImage = null;
                }
            }
            base.OnMove(e);
            this.Invalidate();
        }

        protected override void OnEnter(EventArgs e) {
            base.OnEnter(e);
            if (this.Visible) {
                this.OnPaint(new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
            }
        }

        protected override void OnLeave(EventArgs e) {
            base.OnLeave(e);
            if (this.Visible) {
                this.OnPaint(new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
            }
        }

        [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessMnemonic(char charCode) {
            foreach (TabPage page in this.TabPages) {
                if (IsMnemonic(charCode, page.Text)) {
                    this.SelectedTab = page;
                    return true;
                }
            }
            return base.ProcessMnemonic(charCode);
        }

        protected override void OnSelectedIndexChanged(EventArgs e) {
            base.OnSelectedIndexChanged(e);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [System.Diagnostics.DebuggerStepThrough()]
        protected override void WndProc(ref Message m) {

            switch (m.Msg) {
                case NativeMethods.WM_HSCROLL:

                    //	Raise the scroll event when the scroller is scrolled
                    base.WndProc(ref m);
                    this.OnHScroll(new ScrollEventArgs(((ScrollEventType)NativeMethods.LoWord(m.WParam)), _oldValue, NativeMethods.HiWord(m.WParam), ScrollOrientation.HorizontalScroll));
                    break;
                default:
                    base.WndProc(ref m);
                    break;

            }
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            int index = this.GetActiveIndex(new Point(e.X, e.Y));

            //	If we are clicking on an image then raise the ImageClicked event before raising the standard mouse click event
            //	if there if a handler.
            if (index > -1 && this.TabImageClick != null
                && TabHasImage(index)
                && this.GetTabImageRect(index).Contains(this.MousePosition)) {
                this.OnTabImageClick(new TabControlEventArgs(this.TabPages[index], index, TabControlAction.Selected));
            }
            //	Fire the base event
            base.OnMouseClick(e);
        }

        protected virtual void OnTabImageClick(TabControlEventArgs e) {
            if (this.TabImageClick != null) {
                this.TabImageClick(this, e);
            }
        }

        protected virtual void OnTabClosed(TabControlEventArgs e) {
            if (this.TabClosed != null) {
                this.TabClosed(this, e);
            }
        }

        protected virtual void OnTabClosing(TabControlCancelEventArgs e) {
            if (e.Cancel)
                return;

            var selectedIndex = this.SelectedIndex;
            this.TabPages.Remove(e.TabPage);
            e.TabPage.Dispose();
            if (selectedIndex == this.TabPages.Count) {
                this.SelectedIndex = selectedIndex - 1;
            } else {
                this.SelectedIndex = selectedIndex;
            }
            if (this.TabClosing != null) {
                this.TabClosing(this, e);
            }

            OnTabClosed(new TabControlEventArgs(e.TabPage, e.TabPageIndex, e.Action));
        }

        protected virtual void OnHScroll(ScrollEventArgs e) {
            //	repaint the moved tabs
            this.Invalidate();

            //	Raise the event
            if (this.HScroll != null) {
                this.HScroll(this, e);
            }

            if (e.Type == ScrollEventType.EndScroll) {
                this._oldValue = e.NewValue;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            var mousePos = this.MousePosition;
            
            if (_PrevTabCloserButtonPath != null && _PrevTabCloserButtonPath.IsVisible(mousePos)) {
                // mouse is still in highlighted tab closer
            } else {
                var needsRepainting = false;
                if (_PrevTabCloserButtonPath != null) {
                    _PrevTabCloserButtonPath.Dispose();
                    _PrevTabCloserButtonPath = null;
                    needsRepainting = true;
                }
                _PrevTabCloserButtonPath = GetTabCloserButtonPathAtPosition(mousePos);
                if (_PrevTabCloserButtonPath != null) needsRepainting = true;
                if (needsRepainting) CustomPaint(mousePos);
            }

            //	Initialise Drag Drop
            if (this.AllowDrop && e.Button == MouseButtons.Left) {
                this.StartDragDrop();
            }
        }

        #endregion

        #region	Basic drawing methods

        protected override void OnPaint(PaintEventArgs e) {
            if (_SuspendDrawing) return;

            //	We must always paint the entire area of the tab control, since our actual tab sizes 
            //  may differ from those of the underlying TabControl, which frequently requests painting 
            //  of just a single tab or the whole row of tabs (ie the clip rectangle supplied in the
            //  event args only covers those areas).

            //  So we create a new Graphics object rather than use the one in the event args, to avoid the clipping.
            var start = DateTime.Now;
            var posn = this.MousePosition;
            this.CustomPaint(posn);
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " TabControl " + this.GetHashCode() + " painted: " + DateTime.Now.Subtract(start).TotalMilliseconds + "ms; size: " + this.Size.ToString() + " location: " + this.Location.ToString() + " clip: " + e.ClipRectangle.ToString());
        }

        private void CustomPaint(Point mousePosition) {
            //	We render into a bitmap that is then drawn in one shot rather than using
            //	double buffering built into the control as the built in buffering
            // 	messes up the background painting.
            //	Equally the .Net 2.0 BufferedGraphics object causes the background painting
            //	to mess up, which is why we use this .Net 1.1 buffering technique.

            //	Buffer code from Gil. Schmidt http://www.codeproject.com/KB/graphics/DoubleBuffering.aspx

            if (this.Width > 0 && this.Height > 0) {
                if (this._BackImage == null) {
                    //	Cached Background Image
                    this._BackImage = new Bitmap(this.Width, this.Height);
                    Graphics backGraphics = Graphics.FromImage(this._BackImage);
                    backGraphics.Clear(Color.Transparent);
                    this.PaintTransparentBackground(backGraphics, this.ClientRectangle);
                }

                this._BackBufferGraphics.Clear(Color.Transparent);
                this._BackBufferGraphics.DrawImageUnscaled(this._BackImage, 0, 0);

                if (this.EffectiveRightToLeft) {
                    var m = new Matrix();
                    m.Translate(this._TabBuffer.Width, 0f);
                    m.Scale(-1f, 1f);
                    this._TabBufferGraphics.Transform = m;
                    m.Dispose();
                }

                this._TabBufferGraphics.Clear(Color.Transparent);

                if (this.TabCount > 0) {

                    //	When top or bottom and scrollable we need to clip the sides from painting the tabs.
                    //	Left and right are always multiline.
                    if (this.Alignment <= TabAlignment.Bottom && !this.Multiline) {
                        var rect = this.ClientRectangle;
                        this._TabBufferGraphics.Clip = new Region(new RectangleF(rect.X + 4 - this._StyleProvider.TabPageMargin.Left,
                                                                                    rect.Y,
                                                                                    rect.Width - 8 + this._StyleProvider.TabPageMargin.Left + this._StyleProvider.TabPageMargin.Right,
                                                                                    rect.Height));
                    }

                    //	Draw each tabpage from right to left.  We do it this way to handle
                    //	the overlap correctly.
                    if (this.Multiline) {
                        for (int row = 0; row < this.RowCount; row++) {
                            for (int index = this.TabCount - 1; index >= 0; index--) {
                                if (index != this.SelectedIndex && (this.RowCount == 1 || this.GetTabRow(index) == row)) {
                                    this.DrawTabPage(index, mousePosition, this._TabBufferGraphics);
                                }
                            }
                        }
                    } else {
                        for (int index = this.TabCount - 1; index >= 0; index--) {
                            if (index != this.SelectedIndex) {
                                this.DrawTabPage(index, mousePosition, this._TabBufferGraphics);
                            }
                        }
                    }

                    //	The selected tab must be drawn last so it appears on top.
                    if (this.SelectedIndex > -1) {
                        this.DrawTabPage(this.SelectedIndex, mousePosition, this._TabBufferGraphics);
                    }
                }
                this._TabBufferGraphics.Flush();

                //	Paint the tabs on top of the background

                // Create a new color matrix and set the alpha value to the required opacity
                ColorMatrix alphaMatrix = new ColorMatrix();
                alphaMatrix.Matrix00 = alphaMatrix.Matrix11 = alphaMatrix.Matrix22 = alphaMatrix.Matrix44 = 1;
                alphaMatrix.Matrix33 = this._StyleProvider.Opacity;

                // Create a new image attribute object and set the color matrix to
                // the one just created
                using (ImageAttributes alphaAttributes = new ImageAttributes()) {

                    alphaAttributes.SetColorMatrix(alphaMatrix);

                    // Draw the original image with the image attributes specified
                    this._BackBufferGraphics.DrawImage(this._TabBuffer,
                                                       new Rectangle(0, 0, this._TabBuffer.Width, this._TabBuffer.Height),
                                                       0, 0, this._TabBuffer.Width, this._TabBuffer.Height, GraphicsUnit.Pixel,
                                                       alphaAttributes);
                }

                this._BackBufferGraphics.Flush();

                //	Now paint this to the screen


                //	We want to paint the whole tabstrip and border every time
                //	so that the hot areas update correctly, along with any overlaps

                //	paint the tabs etc.
                using (var g = this.CreateGraphics()) {
                    if (this.EffectiveRightToLeft) {
                        g.DrawImageUnscaled(this._BackBuffer, -1, 0);
                    } else {
                        g.DrawImageUnscaled(this._BackBuffer, 0, 0);
                    }
                }
            }
        }

        protected void PaintTransparentBackground(Graphics graphics, Rectangle clipRect) {

            if ((this.Parent != null)) {

                //	Set the cliprect to be relative to the parent
                clipRect.Offset(this.Location);

                //	Save the current state before we do anything.
                GraphicsState state = graphics.Save();

                //	Set the graphicsobject to be relative to the parent
                graphics.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                graphics.SmoothingMode = SmoothingMode.HighSpeed;

                //	Paint the parent
                PaintEventArgs e = new PaintEventArgs(graphics, clipRect);
                try {
                    this.InvokePaintBackground(this.Parent, e);
                    this.InvokePaint(this.Parent, e);
                } finally {
                    //	Restore the graphics state and the clipRect to their original locations
                    graphics.Restore(state);
                    clipRect.Offset(-this.Location.X, -this.Location.Y);
                }
            }
        }

        private void DrawTabPage(int index, Point mousePosition, Graphics graphics) {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var baseTabRect = this.GetBaseTabRect(index);
            var pageBounds = this.GetPageBounds(index);
            
            var tabBounds = this._StyleProvider.GetTabRect(baseTabRect, pageBounds, this.SelectedIndex == index);
            var tabContentRect = Rectangle.Intersect(baseTabRect, tabBounds);

            var state = GetTabState(index, mousePosition);
            var isTabEnabled = this.TabPages[index].Enabled;
            var isTabVisible = this._Style != TabStyle.None && this.IsTabVisible(tabBounds, pageBounds);

            using (GraphicsPath tabPageBorder = this.GetTabPageBorder(pageBounds, tabBounds),
                    tabBorder = this._StyleProvider.GetTabBorder(tabBounds)) {

                Rectangle tabCloserButtonRect = Rectangle.Empty;
                if (this._StyleProvider.ShowTabCloser) tabCloserButtonRect = GetTabCloserButtonRect(tabContentRect, tabBorder);

                Image tabImage = null;
                Rectangle tabImageRect = Rectangle.Empty;
                if (this.TabHasImage(index)) {
                    tabImage = GetTabImage(index);
                    tabImageRect = GetTabImageRect(tabContentRect, tabBorder);
                }

                Rectangle tabTextRect = this.GetTabTextRect(tabBorder, tabContentRect, tabCloserButtonRect, tabImageRect);

                //	Paint the background
                using (Brush fillBrush = this._StyleProvider.GetPageBackgroundBrush(state)) {
                    graphics.FillPath(fillBrush, tabPageBorder);
                }

                if (isTabVisible) {
                    //	Paint the tab
                    this.PaintTab(tabBorder, tabCloserButtonRect, state, graphics, mousePosition);

                    //	Draw any image
                    if (tabImageRect != Rectangle.Empty) this.DrawTabImage(tabImage, tabImageRect, graphics, isTabEnabled);

                    //	Draw the text
                    this.DrawTabText(this.TabPages[index].Text, state, graphics, tabTextRect);

                }

                //	Paint the border
                this.DrawTabPageBorder(tabPageBorder, state, graphics);

            }
        }

        private void PaintTab(GraphicsPath tabBorder, Rectangle tabCloserButtonRect, TabState state, Graphics graphics, Point mousePosition) {
            this._StyleProvider.PaintTabBackground(tabBorder, state, graphics);

            //	Paint a focus indication
            this._StyleProvider.DrawTabFocusIndicator(tabBorder, state, graphics);
            //	Paint the closer
            this._StyleProvider.DrawTabCloser(tabCloserButtonRect, graphics, state, mousePosition);
        }

        private void DrawTabPageBorder(GraphicsPath path, TabState state, Graphics graphics) {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color borderColor = Color.Empty;

            switch (state){
            case TabState.Disabled:
                borderColor = this._StyleProvider.BorderColorDisabled;
                break;
            case TabState.Focused:
                borderColor = this._StyleProvider.BorderColorFocused;
                break;
            case TabState.Highlighted:
                borderColor = this._StyleProvider.BorderColorHighlighted;
                break;
            case TabState.Selected:
                borderColor = this._StyleProvider.BorderColorSelected;
                break;
            case TabState.Unselected:
                borderColor = this._StyleProvider.BorderColorUnselected;
                break;
            }

            if (borderColor != Color.Empty) {
                using (Pen borderPen = new Pen(borderColor)) {
                    graphics.DrawPath(borderPen, path);
                }
            }
        }

        private void DrawTabText(string text, TabState state, Graphics graphics, Rectangle textBounds) {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Color textColor = Color.Empty;

            switch (state) {
                case TabState.Disabled:
                    textColor = this._StyleProvider.TextColorDisabled;
                    break;
                case TabState.Focused:
                    textColor = this._StyleProvider.TextColorFocused;
                    break;
                case TabState.Highlighted:
                    textColor = this._StyleProvider.TextColorHighlighted;
                    break;
                case TabState.Selected:
                    textColor = this._StyleProvider.TextColorSelected;
                    break;
                case TabState.Unselected:
                    textColor = this._StyleProvider.TextColorUnselected;
                    break;
            }

            using (Brush textBrush = new SolidBrush(textColor)) {
                using (StringFormat format = this.GetStringFormat()) {
                    if (this.EffectiveRightToLeft) {
                        using (Matrix oldTransform = graphics.Transform, m = new Matrix()) {
                            m.Translate(this.Width - textBounds.Right - textBounds.Left, 0f);
                            graphics.Transform = m;
                            graphics.DrawString(text, this.Font, textBrush, textBounds, format);
                            graphics.Transform = oldTransform;
                        }
                    } else {
                        graphics.DrawString(text, this.Font, textBrush, textBounds, format);
                    }
                }
            }

        }

        private void DrawTabImage(Image tabImage, Rectangle imageRect, Graphics graphics, bool isTabEnabled) {
            if (tabImage == null) return;

            if (this.EffectiveRightToLeft) {
                tabImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
         
            if (isTabEnabled) {
                graphics.DrawImage(tabImage, imageRect);
            } else {
                ControlPaint.DrawImageDisabled(graphics, tabImage, imageRect.X, imageRect.Y, Color.Transparent);
            }
        }

        #endregion

        #region String formatting

        private StringFormat GetStringFormat() {
            StringFormat format = null;

            //	Rotate Text by 90 degrees for left and right tabs
            switch (this.Alignment) {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    format = new StringFormat(StringFormatFlags.NoWrap);
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    format = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.DirectionVertical);
                    break;
            }
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            if (this.FindForm() != null && this.FindForm().KeyPreview) {
                format.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            } else {
                format.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
            }
            if (this.RightToLeft == RightToLeft.Yes) {
                format.FormatFlags = format.FormatFlags | StringFormatFlags.DirectionRightToLeft;
            }
            return format;
        }

        #endregion

        #region Tab borders and bounds properties

        private void AdjustPoint(ref Point point, bool adjustHorizontally, int increment) {
            if (adjustHorizontally) {
                point.X += increment;
            } else {
                point.Y += increment;
            }
        }

        private Point AdjustPointForRightToLeft(Point point) {
            Point newPoint = new Point(point.X, point.Y);
            switch (this.Alignment) {
                case TabAlignment.Bottom:
                case TabAlignment.Top:
                    if (this.EffectiveRightToLeft) newPoint.X = (this.Width - newPoint.X);
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    if (this.EffectiveRightToLeft) newPoint.Y = (this.Height - newPoint.Y);
                    break;
            }
            return newPoint;
        }

        private void BackupTabPages() {
            if (this._TabPages == null) {
                this._TabPages = new List<TabPage>();
                foreach (TabPage page in this.TabPages) {
                    this._TabPages.Add(page);
                }
            }
        }

        private Rectangle AdjustRectangleDimensionsToFitInPath(Rectangle rect, GraphicsPath path) {
            Rectangle newRect = rect;
            int offset;
            switch (this.Alignment) {
                case TabAlignment.Bottom:
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.Right, newRect.Bottom, true, -1, (p) => p.X > newRect.X);
                    newRect.Width += offset;
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.X, newRect.Bottom, true, 1, (p) => p.X < newRect.Right);
                    newRect.X += offset;
                    newRect.Width -= offset;
                    break;
                case TabAlignment.Top:
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.Right, newRect.Y, true, -1, (p) => p.X > newRect.X);
                    newRect.Width += offset;
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.X, newRect.Y, true, 1, (p) => p.X < newRect.Right);
                    newRect.X += offset;
                    newRect.Width -= offset;
                    break;
                case TabAlignment.Left:
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.X, newRect.Bottom, false, -1, (p) => p.Y > newRect.Y);
                    newRect.Height += offset;
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.X, newRect.Top, false, 1, (p) => p.Y < newRect.Bottom);
                    newRect.Y += offset;
                    newRect.Height -= offset;
                    break;
                case TabAlignment.Right:
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.Right, newRect.Bottom, false, -1, (p) => p.Y > newRect.Y);
                    newRect.Height += offset;
                    offset = GetOffsetToEnsurePointIsWithinPath(path, newRect.Right, newRect.Top, false, 1, (p) => p.Y < newRect.Bottom);
                    newRect.Y += offset;
                    newRect.Height -= offset;
                    break;
            }
            return newRect;
        }

        private void AddPageBorder(GraphicsPath path, Rectangle pageBounds, Rectangle tabBounds) {
            var radius = this._StyleProvider.TabPageRadius;

            if (!IsTabVisible(tabBounds, pageBounds)) {
                AddRoundedRectangle(path, pageBounds, radius);
                return;
            }

            var diamX = Math.Min(2 * radius, pageBounds.Width);
            var radX = diamX / 2;
            var diamY = Math.Min(2 * radius, pageBounds.Height);
            var radY = diamY / 2;

            switch (this.Alignment) {
                case TabAlignment.Top:
                    if (tabBounds.Right > pageBounds.Right && tabBounds.Left < pageBounds.Right) {
                    } else if (tabBounds.Right > pageBounds.Right - radX) {
                        path.AddLine(tabBounds.Right, pageBounds.Top, pageBounds.Right, pageBounds.Top + radY);
                    } else {
                        path.AddLine(tabBounds.Right, pageBounds.Top, pageBounds.Right - radX, pageBounds.Top);
                        if (radius != 0) path.AddArc(pageBounds.Right - diamX, pageBounds.Top, diamX, diamY, 270, 90);
                    }

                    path.AddLine(pageBounds.Right, pageBounds.Top + radY, pageBounds.Right, pageBounds.Bottom - radY);
                    if (radius != 0) path.AddArc(pageBounds.Right - diamX, pageBounds.Bottom - diamY, diamX, diamY, 0, 90);
                    path.AddLine(pageBounds.Right - radX, pageBounds.Bottom, pageBounds.Left + radX, pageBounds.Bottom);
                    if (radius != 0) path.AddArc(pageBounds.Left, pageBounds.Bottom - diamY, diamX, diamY, 90, 90);
                    path.AddLine(pageBounds.Left, pageBounds.Bottom - radY, pageBounds.Left, pageBounds.Top + radY);

                    if (tabBounds.Left < pageBounds.Left && tabBounds.Right > pageBounds.Left) {
                    } else if (tabBounds.Left < pageBounds.Left + radX) {
                        path.AddLine(pageBounds.Left, pageBounds.Top + radY, tabBounds.Left, pageBounds.Top);
                    } else {
                        if (radius != 0) path.AddArc(pageBounds.Left, pageBounds.Top, diamX, diamY, 180, 90);
                        path.AddLine(pageBounds.Left + radX, pageBounds.Top, tabBounds.Left, pageBounds.Top);
                    }
                    break;
                case TabAlignment.Bottom:
                    if (tabBounds.Left < pageBounds.Left && tabBounds.Right > pageBounds.Left) {
                    } else if (tabBounds.Left < pageBounds.Left + radX) {
                        path.AddLine(tabBounds.Left, pageBounds.Bottom, pageBounds.Left, pageBounds.Bottom - radY);
                    } else {
                        path.AddLine(tabBounds.Left, pageBounds.Bottom, pageBounds.Left + radX, pageBounds.Bottom);
                        if (radius != 0) path.AddArc(pageBounds.Left, pageBounds.Bottom - diamY, diamX, diamY, 90, 90);
                    }

                    path.AddLine(pageBounds.Left, pageBounds.Bottom - radY, pageBounds.Left, pageBounds.Top + radY);
                    if (radius != 0) path.AddArc(pageBounds.Left, pageBounds.Top, diamX, diamY, 180, 90);
                    path.AddLine(pageBounds.Left + radX, pageBounds.Top, pageBounds.Right - radX, pageBounds.Top);
                    if (radius != 0) path.AddArc(pageBounds.Right - diamX, pageBounds.Top, diamX, diamY, 270, 90);
                    path.AddLine(pageBounds.Right, pageBounds.Top + radY, pageBounds.Right, pageBounds.Bottom - radY);

                    if (tabBounds.Right > pageBounds.Right && tabBounds.Left < pageBounds.Right) {
                    } else if (tabBounds.Right > pageBounds.Right - radX) {
                        path.AddLine(pageBounds.Right, pageBounds.Bottom - radY, tabBounds.Right, pageBounds.Bottom);
                    } else {
                        if (radius != 0) path.AddArc(pageBounds.Right - diamX, pageBounds.Bottom - diamY, diamX, diamY, 0, 90);
                        path.AddLine(pageBounds.Right - radX, pageBounds.Bottom, tabBounds.Right, pageBounds.Bottom);
                    }

                    break;
                case TabAlignment.Left:
                    path.AddLine(pageBounds.Left, tabBounds.Top, pageBounds.Left, pageBounds.Top);
                    path.AddLine(pageBounds.Left, pageBounds.Top, pageBounds.Right, pageBounds.Top);
                    path.AddLine(pageBounds.Right, pageBounds.Top, pageBounds.Right, pageBounds.Bottom);
                    path.AddLine(pageBounds.Right, pageBounds.Bottom, pageBounds.Left, pageBounds.Bottom);
                    path.AddLine(pageBounds.Left, pageBounds.Bottom, pageBounds.Left, tabBounds.Bottom);
                    break;
                case TabAlignment.Right:
                    path.AddLine(pageBounds.Right, tabBounds.Bottom, pageBounds.Right, pageBounds.Bottom);
                    path.AddLine(pageBounds.Right, pageBounds.Bottom, pageBounds.Left, pageBounds.Bottom);
                    path.AddLine(pageBounds.Left, pageBounds.Bottom, pageBounds.Left, pageBounds.Top);
                    path.AddLine(pageBounds.Left, pageBounds.Top, pageBounds.Right, pageBounds.Top);
                    path.AddLine(pageBounds.Right, pageBounds.Top, pageBounds.Right, tabBounds.Top);
                    break;
            }
        }

        private void AddRoundedRectangle(GraphicsPath path, Rectangle pageBounds, int radius) {
            if (radius == 0) {
                path.AddRectangle(pageBounds);
                return;
            }

            var d = new Size(Math.Min(2 * radius, pageBounds.Width), Math.Min(2 * radius, pageBounds.Height));

            path.AddArc(pageBounds.Left, pageBounds.Top, d.Width, d.Height, 180, 90);
            path.AddArc(pageBounds.Right - d.Width, pageBounds.Top, d.Width, d.Height, 270, 90);
            path.AddArc(pageBounds.Right - d.Width, pageBounds.Bottom - d.Height, d.Width, d.Height, 0, 90);
            path.AddArc(pageBounds.Left, pageBounds.Bottom - d.Height, d.Width, d.Height, 90, 90);
        }

        private Rectangle EnsureRectIsInPath(GraphicsPath tabBorder, Rectangle rect, bool increaseCoordinate) {
            Rectangle newRect = rect;

            switch (this.Alignment) {
            case TabAlignment.Top:
                if (increaseCoordinate) {
                    newRect.X += 4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.X, newRect.Y, true, +1, (p) => p.X < this.Width);
                } else {
                    newRect.X += -4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.Right, newRect.Y, true, -1, (p) => p.X > 0);
                }
                break;
            case TabAlignment.Bottom:
                if (increaseCoordinate) {
                    newRect.X += 4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.X, newRect.Bottom, true, +1, (p) => p.X < this.Width);
                } else {
                    newRect.X += -4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.Right, newRect.Bottom, true, -1, (p) => p.X > 0);
                }
                break;
            case TabAlignment.Left:
                if (increaseCoordinate) {
                    newRect.Y += 4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.Left, newRect.Y, false, +1, (p) => p.Y < this.Height);
                } else {
                    newRect.Y += -4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.Left, newRect.Bottom, false, -1, (p) => p.Y > 0);
                }
                break;
            case TabAlignment.Right:
                if (increaseCoordinate) {
                    newRect.Y += 4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.Right, newRect.Y, false, +1, (p) => p.Y < this.Height);
                } else {
                    newRect.Y += -4 + GetOffsetToEnsurePointIsWithinPath(tabBorder, newRect.Right, newRect.Bottom, false, -1, (p) => p.Y > 0);
                }
                break;
            }
            return newRect;
        }

        private bool EffectiveRightToLeft {
            get {
                return ((this.RightToLeft == Forms.RightToLeft.Yes) ||
                            (this.RightToLeft == Forms.RightToLeft.Inherit &&
                                this.Parent.RightToLeft == Forms.RightToLeft.Yes))
                        && this.RightToLeftLayout;
            }
        }

        private Rectangle GetBaseTabRect(int index) {
            var rect = this.GetTabRect(index);
            switch (this.Alignment) {
            case TabAlignment.Top:
            case TabAlignment.Bottom:
                if (this.EffectiveRightToLeft)
                    rect.X = this.Width - rect.Right;
                break;
            case TabAlignment.Left:
            case TabAlignment.Right:
                if (this.EffectiveRightToLeft)
                    rect.Y = this.Height - rect.Bottom;
                break;
            }
            return rect;
        }

        private int GetOffsetToEnsurePointIsWithinPath(GraphicsPath path, int X, int Y, bool adjustHorizontally, int increment, Func<Point, bool> constraint) {
            Point point = new Point(X, Y);
            while (!path.IsVisible(point) && constraint(point))
                AdjustPoint(ref point, adjustHorizontally, increment);
            return (int)(adjustHorizontally ? point.X - X : point.Y - Y);
        }

        public Rectangle GetPageBounds(int index) {
            if (index < 0)
                return new Rectangle();

            Rectangle pageBounds = this.TabPages[index].Bounds;

            pageBounds.Width += this._StyleProvider.TabPageMargin.Left + this._StyleProvider.TabPageMargin.Right - 1;
            pageBounds.Height += this._StyleProvider.TabPageMargin.Top + this._StyleProvider.TabPageMargin.Bottom - 1;
            pageBounds.X -= this._StyleProvider.TabPageMargin.Left;
            pageBounds.Y -= this._StyleProvider.TabPageMargin.Top;

            return pageBounds;
        }

        public Rectangle GetTabBounds(int index) {
            return this._StyleProvider.GetTabRect(base.GetTabRect(index), this.GetPageBounds(index), index == this.SelectedIndex);
        }

        private GraphicsPath GetTabCloserButtonPathAtPosition(Point position) {
            if (this.DesignMode || !this._StyleProvider.ShowTabCloser)
                return null;
            for (int i = 0; i < this.TabCount; i++) {
                var rect = this.GetTabCloserButtonRect(i);
                var closerButtonPath = this._StyleProvider.GetTabCloserButtonPath(rect);
                if (closerButtonPath.IsVisible(position))
                    return closerButtonPath;
            }
            return null;
        }

        public Rectangle GetTabCloserButtonRect(int index) {
            var baseTabRect = this.GetTabRect(index);
            var pageBounds = this.GetPageBounds(index);

            var tabBounds = this._StyleProvider.GetTabRect(baseTabRect, pageBounds, this.SelectedIndex == index);
            var tabContentRect = Rectangle.Intersect(baseTabRect, tabBounds);
            return GetTabCloserButtonRect(tabContentRect, this._StyleProvider.GetTabBorder(tabBounds));
        }

        private Rectangle GetTabCloserButtonRect(Rectangle tabContentRect, GraphicsPath tabBorder) {
            Rectangle closerRect = new Rectangle();
            bool increaseCoordinate = false;

            switch (this.Alignment) {
            case TabAlignment.Top:
            case TabAlignment.Bottom:
                if (this.EffectiveRightToLeft) {
                    closerRect = RectangleUtils.GetRectangleWithinRectangle(tabContentRect, TabCloserButtonSize, ContentAlignment.MiddleLeft);
                    increaseCoordinate = true;
                } else {
                    closerRect = RectangleUtils.GetRectangleWithinRectangle(tabContentRect, TabCloserButtonSize, ContentAlignment.MiddleRight);
                    increaseCoordinate = false;
                }
                break;
            case TabAlignment.Left:
            case TabAlignment.Right:
                if (this.EffectiveRightToLeft) {
                    closerRect = RectangleUtils.GetRectangleWithinRectangle(tabContentRect, TabCloserButtonSize, ContentAlignment.TopCenter);
                    increaseCoordinate = true;
                } else {
                    closerRect = RectangleUtils.GetRectangleWithinRectangle(tabContentRect, TabCloserButtonSize, ContentAlignment.BottomCenter);
                    increaseCoordinate = false;
                }
                break;
            }
            return EnsureRectIsInPath(tabBorder, closerRect, increaseCoordinate);
        }

        private Image GetTabImage(int index) {
            Image tabImage = null;
            if (this.ImageList == null) {
            } else if (this.TabPages[index].ImageIndex > -1 && this.ImageList.Images.Count > this.TabPages[index].ImageIndex) {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageIndex];
            } else if ((!string.IsNullOrEmpty(this.TabPages[index].ImageKey) && !this.TabPages[index].ImageKey.Equals("(none)", StringComparison.OrdinalIgnoreCase))
                       && this.ImageList.Images.ContainsKey(this.TabPages[index].ImageKey)) {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageKey];
            }

            return tabImage;
        }

        private Rectangle GetTabImageRect(int index) {
            var tabRect = this._StyleProvider.GetTabRect(base.GetTabRect(index), this.GetPageBounds(index), index == this.SelectedIndex);
            using (GraphicsPath tabBorderPath = this._StyleProvider.GetTabBorder(tabRect)) {
                return this.GetTabImageRect(tabRect, tabBorderPath);
            }
        }

        private Rectangle GetTabImageRect(Rectangle tabRect, GraphicsPath tabBorderPath) {
            Rectangle imageRect = new Rectangle();
            var imageSize = this.ImageList.ImageSize;

            imageRect = RectangleUtils.GetRectangleWithinRectangle(tabRect, imageSize, this._StyleProvider.ImageAlign);

            var imageAlignment = this._StyleProvider.ImageAlign;
            bool horizontalTabs = (this.Alignment == TabAlignment.Top || this.Alignment == TabAlignment.Bottom);
            bool adjustPosition = (horizontalTabs && (NativeMethods.IsLeftAligned(imageAlignment) || NativeMethods.IsRightAligned(imageAlignment)))
                                || (!horizontalTabs && (NativeMethods.IsBottomAligned(imageAlignment) || NativeMethods.IsTopAligned(imageAlignment)));
            bool increaseCoordinate = (horizontalTabs && NativeMethods.IsLeftAligned(imageAlignment)) || (!horizontalTabs && NativeMethods.IsTopAligned(imageAlignment));
            
            if (adjustPosition) imageRect = EnsureRectIsInPath(tabBorderPath, imageRect, increaseCoordinate);

            if (this._StyleProvider.ShowTabCloser) {
                if (this.EffectiveRightToLeft) {
                    if (horizontalTabs && NativeMethods.IsLeftAligned(imageAlignment)) imageRect.X += TabControlExtra.TabCloserButtonSize + 4;
                    if (!horizontalTabs && NativeMethods.IsTopAligned(imageAlignment)) imageRect.Y += TabControlExtra.TabCloserButtonSize + 4;
                } else {
                    if (horizontalTabs && NativeMethods.IsRightAligned(imageAlignment)) imageRect.X -= TabControlExtra.TabCloserButtonSize + 4;
                    if (!horizontalTabs && NativeMethods.IsBottomAligned(imageAlignment)) imageRect.Y -= TabControlExtra.TabCloserButtonSize + 4;
                }
            }

            return imageRect;
        }

        private GraphicsPath GetTabPageBorder(Rectangle pageBounds, Rectangle tabBounds) {

            GraphicsPath path = new GraphicsPath();
            if (IsTabVisible(tabBounds, pageBounds))
                this._StyleProvider.AddTabBorder(path, tabBounds);
            this.AddPageBorder(path, pageBounds, tabBounds);

            path.CloseFigure();
            return path;
        }

        public Point GetTabPosition(int index) {

            //	If we are not multiline then the column is the index and the row is 0.
            if (!this.Multiline) {
                return new Point(0, index);
            }

            //	If there is only one row then the column is the index
            if (this.RowCount == 1) {
                return new Point(0, index);
            }

            //	We are in a true multi-row scenario
            int row = this.GetTabRow(index);
            Rectangle rect = this.GetTabRect(index);
            int column = -1;

            //	Scan from left to right along rows, skipping to next row if it is not the one we want.
            for (int testIndex = 0; testIndex < this.TabCount; testIndex++) {
                Rectangle testRect = this.GetTabRect(testIndex);
                if (this.Alignment <= TabAlignment.Bottom) {
                    if (testRect.Y == rect.Y) {
                        column += 1;
                    }
                } else {
                    if (testRect.X == rect.X) {
                        column += 1;
                    }
                }

                if (testRect.Location.Equals(rect.Location)) {
                    return new Point(row, column);
                }
            }

            return new Point(0, 0);
        }

        public int GetTabRow(int index) {
            //	All calculations will use this rect as the base point
            //	because the itemsize does not return the correct width.
            Rectangle rect = this.GetTabRect(index);

            int row = -1;

            switch (this.Alignment) {
            case TabAlignment.Top:
                row = (rect.Y - 2) / rect.Height;
                break;
            case TabAlignment.Bottom:
                row = ((this.Height - rect.Y - 2) / rect.Height) - 1;
                break;
            case TabAlignment.Left:
                row = (rect.X - 2) / rect.Width;
                break;
            case TabAlignment.Right:
                row = ((this.Width - rect.X - 2) / rect.Width) - 1;
                break;
            }
            return row;
        }

        private TabState GetTabState(int index, Point mousePosition) {
            if (this.SelectedIndex == index) {
                if (this.ContainsFocus) {
                    return TabState.Focused;
                } else {
                    return TabState.Selected;
                }
            } else if (!this.TabPages[index].Enabled) {
                return TabState.Disabled;
            } else if (this.DisplayStyleProvider.HotTrack && index == this.GetActiveIndex(mousePosition)) {
                return TabState.Highlighted;
            } else {
                return TabState.Unselected;
            }
        }

        private Rectangle GetTabTextRect(GraphicsPath tabBorder, Rectangle tabBounds, Rectangle closerRect, Rectangle imageRect) {
            var left = tabBounds.X + 1;
            var right = tabBounds.Right - 1;
            var top = tabBounds.Y + 1;
            var bottom = tabBounds.Bottom - 1;
            var imageAlignment = this._StyleProvider.ImageAlign;

            switch (this.Alignment) {
            case TabAlignment.Bottom:
            case TabAlignment.Top:
                if (closerRect != Rectangle.Empty) {
                    if (this.EffectiveRightToLeft) {
                        left = closerRect.Right + 4;
                    } else {
                        right = closerRect.X - 4;
                    }
                }
                if (imageRect != Rectangle.Empty) {
                    if (NativeMethods.IsLeftAligned(imageAlignment)) {
                        left = imageRect.Right + 4;
                    } else if (NativeMethods.IsRightAligned(imageAlignment)) {
                        right = imageRect.X - 4;
                    }
                }
                break;
            case TabAlignment.Left:
            case TabAlignment.Right:
                if (closerRect != Rectangle.Empty) {
                    if (this.EffectiveRightToLeft) {
                        top = closerRect.Bottom + 4;
                    } else {
                        bottom = closerRect.Y - 4;
                    }
                }
                if (imageRect != Rectangle.Empty) {
                    if (NativeMethods.IsTopAligned(imageAlignment)) {
                        top = imageRect.Bottom + 4;
                    } else if (NativeMethods.IsBottomAligned(imageAlignment)) {
                        bottom = imageRect.Y - 4;
                    }
                }
                break;
            }

            Rectangle textRect = new Rectangle(left, top, right - left, bottom - top);

            //	Ensure it fits inside the path
            return AdjustRectangleDimensionsToFitInPath(textRect, tabBorder);
        }

        protected internal bool IsTabVisible(Rectangle tabBounds, Rectangle pageBounds) {
            switch (this.Alignment) {
            case TabAlignment.Top:
            case TabAlignment.Bottom:
                return tabBounds.Right > pageBounds.Left + this._StyleProvider.TabPageMargin.Left && tabBounds.Left < pageBounds.Right - this._StyleProvider.TabPageMargin.Right;
            case TabAlignment.Left:
            case TabAlignment.Right:
                return tabBounds.Bottom > pageBounds.Top + this._StyleProvider.TabPageMargin.Top && tabBounds.Top < pageBounds.Bottom - this._StyleProvider.TabPageMargin.Bottom;
            }
            return false;
        }

        private bool IsValidTabIndex(int index) {
            this.BackupTabPages();
            return ((index >= 0) && (index < this._TabPages.Count));
        }

        private bool TabHasImage(int index) {
            return this.ImageList != null &&
                    (this.TabPages[index].ImageIndex > -1 ||
                    (!string.IsNullOrEmpty(this.TabPages[index].ImageKey) && !this.TabPages[index].ImageKey.Equals("(none)", StringComparison.OrdinalIgnoreCase)));
        }

        #endregion

    }
}
