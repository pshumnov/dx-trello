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
            ganttControl1 = new DevExpress.XtraGantt.GanttControl();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            ((System.ComponentModel.ISupportInitialize)ganttControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            SuspendLayout();
            // 
            // ganttControl1
            // 
            ganttControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            ganttControl1.Dock = DockStyle.Fill;
            ganttControl1.Location = new Point(0, 0);
            ganttControl1.Name = "ganttControl1";
            ganttControl1.Size = new Size(992, 704);
            ganttControl1.TabIndex = 0;
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            // 
            // GanttView
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ganttControl1);
            Name = "GanttView";
            Size = new Size(992, 704);
            ((System.ComponentModel.ISupportInitialize)ganttControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGantt.GanttControl ganttControl1;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
    }
}
