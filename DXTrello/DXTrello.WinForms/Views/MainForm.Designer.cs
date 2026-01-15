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
            accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(components);
            tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(components);
            dockManager1 = new DevExpress.XtraBars.Docking.DockManager(components);
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)accordionControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)documentManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabbedView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dockManager1).BeginInit();
            SuspendLayout();
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(ViewModel.ViewModels.MainViewModel);
            // 
            // accordionControl1
            // 
            accordionControl1.Dock = DockStyle.Left;
            accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { accordionControlElement1 });
            accordionControl1.Location = new Point(0, 0);
            accordionControl1.Margin = new Padding(2, 3, 2, 3);
            accordionControl1.Name = "accordionControl1";
            accordionControl1.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
            accordionControl1.Size = new Size(40, 611);
            accordionControl1.TabIndex = 0;
            // 
            // accordionControlElement1
            // 
            accordionControlElement1.Name = "accordionControlElement1";
            accordionControlElement1.Text = "Element1";
            // 
            // documentManager1
            // 
            documentManager1.ContainerControl = this;
            documentManager1.View = tabbedView1;
            documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] { tabbedView1 });
            // 
            // tabbedView1
            // 
            tabbedView1.Style = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Classic;
            // 
            // dockManager1
            // 
            dockManager1.Form = this;
            dockManager1.Style = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Light;
            dockManager1.TopZIndexControls.AddRange(new string[] { "DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl", "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl", "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl" });
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1014, 611);
            Controls.Add(accordionControl1);
            Margin = new Padding(2, 3, 2, 3);
            Name = "MainForm";
            Text = "MainView";
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)accordionControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)documentManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabbedView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dockManager1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
    }
}