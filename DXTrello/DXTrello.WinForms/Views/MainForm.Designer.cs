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
            navigationPane1 = new DevExpress.XtraBars.Navigation.NavigationPane();
            navigationPage1 = new DevExpress.XtraBars.Navigation.NavigationPage();
            cardView = new CardView();
            navigationPage2 = new DevExpress.XtraBars.Navigation.NavigationPage();
            ganttView = new GanttView();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)navigationPane1).BeginInit();
            navigationPane1.SuspendLayout();
            navigationPage1.SuspendLayout();
            navigationPage2.SuspendLayout();
            SuspendLayout();
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(ViewModel.ViewModels.MainViewModel);
            // 
            // navigationPane1
            // 
            navigationPane1.Controls.Add(navigationPage1);
            navigationPane1.Controls.Add(navigationPage2);
            navigationPane1.Dock = DockStyle.Fill;
            navigationPane1.Location = new Point(0, 0);
            navigationPane1.Name = "navigationPane1";
            navigationPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { navigationPage1, navigationPage2 });
            navigationPane1.RegularSize = new Size(1076, 662);
            navigationPane1.SelectedPage = navigationPage2;
            navigationPane1.Size = new Size(1076, 662);
            navigationPane1.TabIndex = 0;
            navigationPane1.Text = "navigationPane1";
            // 
            // navigationPage1
            // 
            navigationPage1.Caption = "Card View";
            navigationPage1.Controls.Add(cardView);
            navigationPage1.Name = "navigationPage1";
            navigationPage1.Size = new Size(895, 553);
            // 
            // cardView1
            // 
            cardView.Dock = DockStyle.Fill;
            cardView.Location = new Point(0, 0);
            cardView.Name = "cardView1";
            cardView.Size = new Size(895, 553);
            cardView.TabIndex = 0;
            // 
            // navigationPage2
            // 
            navigationPage2.Caption = "Timeline View";
            navigationPage2.Controls.Add(ganttView);
            navigationPage2.Name = "navigationPage2";
            navigationPage2.Size = new Size(895, 553);
            // 
            // ganttView1
            // 
            ganttView.Dock = DockStyle.Fill;
            ganttView.Location = new Point(0, 0);
            ganttView.Name = "ganttView1";
            ganttView.Size = new Size(895, 553);
            ganttView.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1076, 662);
            Controls.Add(navigationPane1);
            Name = "MainForm";
            Text = "MainView";
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)navigationPane1).EndInit();
            navigationPane1.ResumeLayout(false);
            navigationPage1.ResumeLayout(false);
            navigationPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraBars.Navigation.NavigationPane navigationPane1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage1;
        private CardView cardView;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage2;
        private GanttView ganttView;
    }
}