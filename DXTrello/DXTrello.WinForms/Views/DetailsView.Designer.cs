namespace DXTrello.WinForms {
    partial class DetailsView {
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
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            titleEdit = new DevExpress.XtraEditors.TextEdit();
            descriptionEdit = new DevExpress.XtraEditors.MemoEdit();
            endDateEdit = new DevExpress.XtraEditors.DateEdit();
            startDateEdit = new DevExpress.XtraEditors.DateEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            userSelection = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)titleEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)descriptionEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endDateEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endDateEdit.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startDateEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startDateEdit.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userSelection.Properties).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(titleEdit);
            layoutControl1.Controls.Add(descriptionEdit);
            layoutControl1.Controls.Add(endDateEdit);
            layoutControl1.Controls.Add(startDateEdit);
            layoutControl1.Controls.Add(userSelection);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(731, 231, 812, 500);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(671, 560);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // titleEdit
            // 
            titleEdit.Location = new Point(14, 34);
            titleEdit.Name = "titleEdit";
            titleEdit.Size = new Size(643, 22);
            titleEdit.StyleController = layoutControl1;
            titleEdit.TabIndex = 0;
            // 
            // descriptionEdit
            // 
            descriptionEdit.Location = new Point(14, 80);
            descriptionEdit.Name = "descriptionEdit";
            descriptionEdit.Size = new Size(643, 413);
            descriptionEdit.StyleController = layoutControl1;
            descriptionEdit.TabIndex = 2;
            // 
            // endDateEdit
            // 
            endDateEdit.EditValue = null;
            endDateEdit.Location = new Point(415, 497);
            endDateEdit.Name = "endDateEdit";
            endDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            endDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            endDateEdit.Size = new Size(242, 22);
            endDateEdit.StyleController = layoutControl1;
            endDateEdit.TabIndex = 5;
            // 
            // startDateEdit
            // 
            startDateEdit.EditValue = null;
            startDateEdit.Location = new Point(92, 497);
            startDateEdit.Name = "startDateEdit";
            startDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            startDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            startDateEdit.Size = new Size(241, 22);
            startDateEdit.StyleController = layoutControl1;
            startDateEdit.TabIndex = 3;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, simpleSeparator1, layoutControlItem2, layoutControlItem4, layoutControlItem3, layoutControlItem5 });
            Root.Name = "Root";
            Root.Size = new Size(671, 560);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = titleEdit;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(647, 46);
            layoutControlItem1.Text = "Title";
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem1.TextSize = new Size(63, 16);
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new Point(0, 509);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new Size(647, 1);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = descriptionEdit;
            layoutControlItem2.Location = new Point(0, 46);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(647, 437);
            layoutControlItem2.Text = "Description";
            layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem2.TextSize = new Size(63, 16);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = startDateEdit;
            layoutControlItem4.Location = new Point(0, 483);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(323, 26);
            layoutControlItem4.Text = "Start date";
            layoutControlItem4.TextSize = new Size(63, 16);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = endDateEdit;
            layoutControlItem3.Location = new Point(323, 483);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(324, 26);
            layoutControlItem3.Text = "End date";
            layoutControlItem3.TextSize = new Size(63, 16);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = userSelection;
            layoutControlItem5.Location = new Point(0, 510);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(647, 26);
            layoutControlItem5.Text = "User";
            layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem5.TextSize = new Size(63, 16);
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            // 
            // userSelection
            // 
            userSelection.EditValue = "";
            userSelection.Location = new Point(92, 524);
            userSelection.Name = "userSelection";
            userSelection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            userSelection.Properties.NullText = "Select user";
            userSelection.Properties.PopupSizeable = false;
            userSelection.Size = new Size(565, 22);
            userSelection.StyleController = layoutControl1;
            userSelection.TabIndex = 4;
            // 
            // DetailsView
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            Name = "DetailsView";
            Size = new Size(671, 560);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)titleEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)descriptionEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)endDateEdit.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)endDateEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)startDateEdit.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)startDateEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)userSelection.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit titleEdit;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.MemoEdit descriptionEdit;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit endDateEdit;
        private DevExpress.XtraEditors.DateEdit startDateEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LookUpEdit userSelection;
    }
}
