namespace DXTrello.WinForms {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(components);
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            bar2 = new DevExpress.XtraBars.Bar();
            userFilterItem = new DevExpress.XtraBars.BarSubItem();
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(components);
            detailsPanel = new DevExpress.Utils.FlyoutPanel();
            flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            detailsView1 = new DetailsView();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)documentManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabbedView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)detailsPanel).BeginInit();
            detailsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)flyoutPanelControl1).BeginInit();
            flyoutPanelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(ViewModel.ViewModels.MainViewModel);
            // 
            // documentManager1
            // 
            documentManager1.ContainerControl = this;
            documentManager1.MenuManager = barManager1;
            documentManager1.View = tabbedView1;
            documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] { tabbedView1 });
            // 
            // barManager1
            // 
            barManager1.AllowQuickCustomization = false;
            barManager1.AllowShowToolbarsPopup = false;
            barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] { bar2 });
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { userFilterItem });
            barManager1.MainMenu = bar2;
            barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            bar2.BarName = "Main menu";
            bar2.DockCol = 0;
            bar2.DockRow = 0;
            bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(userFilterItem) });
            bar2.OptionsBar.MultiLine = true;
            bar2.OptionsBar.UseWholeRow = true;
            bar2.Text = "Main menu";
            bar2.Visible = false;
            // 
            // userFilterItem
            // 
            userFilterItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            userFilterItem.Caption = "Assignee";
            userFilterItem.Id = 0;
            userFilterItem.Name = "userFilterItem";
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Size = new Size(1304, 34);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 726);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Size = new Size(1304, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 34);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Size = new Size(0, 692);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(1304, 34);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Size = new Size(0, 692);
            // 
            // tabbedView1
            // 
            tabbedView1.Style = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Classic;
            // 
            // detailsPanel
            // 
            detailsPanel.Controls.Add(flyoutPanelControl1);
            detailsPanel.Location = new Point(756, 166);
            detailsPanel.Margin = new Padding(4);
            detailsPanel.Name = "detailsPanel";
            detailsPanel.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Right;
            detailsPanel.OptionsButtonPanel.ButtonPanelHeight = 36;
            detailsPanel.OwnerControl = this;
            detailsPanel.Size = new Size(491, 223);
            detailsPanel.TabIndex = 1;
            // 
            // flyoutPanelControl1
            // 
            flyoutPanelControl1.AutoSize = true;
            flyoutPanelControl1.Controls.Add(detailsView1);
            flyoutPanelControl1.Dock = DockStyle.Fill;
            flyoutPanelControl1.FlyoutPanel = detailsPanel;
            flyoutPanelControl1.Location = new Point(0, 0);
            flyoutPanelControl1.Margin = new Padding(4);
            flyoutPanelControl1.Name = "flyoutPanelControl1";
            flyoutPanelControl1.Size = new Size(491, 223);
            flyoutPanelControl1.TabIndex = 0;
            // 
            // detailsView1
            // 
            detailsView1.Dock = DockStyle.Fill;
            detailsView1.Location = new Point(2, 2);
            detailsView1.Margin = new Padding(5);
            detailsView1.Name = "detailsView1";
            detailsView1.Size = new Size(487, 219);
            detailsView1.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1304, 726);
            Controls.Add(detailsPanel);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "MainView";
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)documentManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabbedView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)detailsPanel).EndInit();
            detailsPanel.ResumeLayout(false);
            detailsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)flyoutPanelControl1).EndInit();
            flyoutPanelControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.Utils.FlyoutPanel detailsPanel;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private DetailsView detailsView1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem userFilterItem;
    }
}