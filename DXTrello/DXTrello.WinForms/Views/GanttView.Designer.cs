namespace DXTrello.WinForms {
    partial class GanttView {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GanttView));
            ganttControl1 = new DevExpress.XtraGantt.GanttControl();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            bar1 = new DevExpress.XtraBars.Bar();
            taskTableItem = new DevExpress.XtraBars.BarButtonItem();
            todayItem = new DevExpress.XtraBars.BarButtonItem();
            timePeriodItem = new DevExpress.XtraBars.BarListItem();
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)ganttControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).BeginInit();
            SuspendLayout();
            // 
            // ganttControl1
            // 
            ganttControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            ganttControl1.Dock = DockStyle.Fill;
            ganttControl1.Location = new Point(0, 34);
            ganttControl1.Name = "ganttControl1";
            ganttControl1.Size = new Size(992, 670);
            ganttControl1.TabIndex = 0;
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            // 
            // barManager1
            // 
            barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] { bar1 });
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { taskTableItem, todayItem, timePeriodItem });
            barManager1.MaxItemId = 6;
            barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemComboBox1 });
            // 
            // bar1
            // 
            bar1.BarName = "Tools";
            bar1.DockCol = 0;
            bar1.DockRow = 0;
            bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(taskTableItem), new DevExpress.XtraBars.LinkPersistInfo(todayItem), new DevExpress.XtraBars.LinkPersistInfo(timePeriodItem) });
            bar1.Text = "Tools";
            // 
            // taskTableItem
            // 
            taskTableItem.Caption = "barButtonItem1";
            taskTableItem.Id = 0;
            taskTableItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("taskTableItem.ImageOptions.SvgImage");
            taskTableItem.Name = "taskTableItem";
            // 
            // todayItem
            // 
            todayItem.Caption = "Today";
            todayItem.Id = 1;
            todayItem.Name = "todayItem";
            // 
            // timePeriodItem
            // 
            timePeriodItem.Caption = "Day";
            timePeriodItem.Id = 5;
            timePeriodItem.Name = "timePeriodItem";
            timePeriodItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            timePeriodItem.Strings.AddRange(new object[] { "Day", "Week", "Month", "Quarter" });
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Size = new Size(992, 34);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 704);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Size = new Size(992, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 34);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Size = new Size(0, 670);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(992, 34);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Size = new Size(0, 670);
            // 
            // repositoryItemComboBox1
            // 
            repositoryItemComboBox1.AutoHeight = false;
            repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBox1.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            repositoryItemComboBox1.Items.AddRange(new object[] { "Day", "Week", "Month", "Quarter" });
            repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // GanttView
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ganttControl1);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Name = "GanttView";
            Size = new Size(992, 704);
            ((System.ComponentModel.ISupportInitialize)ganttControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraGantt.GanttControl ganttControl1;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem taskTableItem;
        private DevExpress.XtraBars.BarButtonItem todayItem;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarListItem timePeriodItem;
    }
}
