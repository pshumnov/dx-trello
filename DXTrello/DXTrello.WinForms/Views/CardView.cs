using DXTrello.ViewModel.ViewModels;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Columns;
using DXTrello.Core.Models;

namespace DXTrello.WinForms {
    public partial class CardView : DevExpress.XtraEditors.XtraUserControl {
        TileView tileView;

        public CardView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeKanban();
                InitializeBindings();
            }
        }

        void InitializeKanban() {
            // Replace the default View with a TileView
            tileView = new TileView(gridControl1);
            gridControl1.MainView = tileView;

            // Define Columns
            var colId = AddColumn(nameof(ProjectTask.Id));
            var colStatus = AddColumn(nameof(ProjectTask.Status));
            var colTitle = AddColumn(nameof(ProjectTask.Title));
            var colDesc = AddColumn(nameof(ProjectTask.Description));
            var colDate = AddColumn(nameof(ProjectTask.EndDate));
            var colAssignee = AddColumn(nameof(ProjectTask.AssigneeName));
            
            // Configure Kanban Mode
            tileView.OptionsTiles.LayoutMode = TileViewLayoutMode.Kanban;
            tileView.OptionsTiles.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Top;
            tileView.ColumnSet.GroupColumn = colStatus;
            
            // Allow Drag & Drop to change Status
            tileView.OptionsDragDrop.AllowDrag = true;
            
            // Configure HTML Template
            tileView.OptionsTiles.ItemSize = new Size(240, 120);
            
            // Check if TileHtmlTemplate property exists or similar. 
            // Assuming TileHtmlTemplate property for the current version logic.
            tileView.TileHtmlTemplate.Template = @"
                <div class='card'>
                    <div class='title'>${Title}</div>
                    <div class='desc'>${Description}</div>
                    <div class='footer'>
                        <div class='date-badge'>${EndDate}</div>
                        <div class='user'>${AssigneeName}</div>
                    </div>
                </div>";
            
            tileView.TileHtmlTemplate.Styles = @"
                .card {
                    display: flex;
                    flex-direction: column;
                    padding: 8px;
                    background-color: @ControlLightLight;
                    border: 1px solid @ControlLight;
                    border-radius: 4px;
                    height: 100%;
                    box-shadow: 0px 1px 2px @ControlDark;
                }
                .title {
                    font-size: 8px;
                    font-weight: bold;
                    margin-bottom: 4px;
                }
                .desc {
                    font-size: 8px;
                    color: @DisabledText;
                    flex-grow: 1;
                    overflow: hidden;
                    text-overflow: ellipsis;
                }
                .footer {
                    display: flex;
                    flex-direction: row;
                    justify-content: space-between;
                    align-items: center;
                    margin-top: 8px;
                }
                .date-badge {
                    background-color: @Highlight;
                    color: @HighlightText;
                    border-radius: 3px;
                    padding: 2px 6px;
                    font-size: 8px;
                }
                .user {
                    font-size: 8px;
                    font-weight: 600;
                    color: @ControlText;
                }
            ";
        }

        TileViewColumn AddColumn(string fieldName) {
            TileViewColumn col = (TileViewColumn)tileView.Columns.AddVisible(fieldName);
            return col;
        }

        void InitializeBindings() {
            var fluent = mvvmContext.OfType<CardViewModel>();
            fluent.SetBinding(gridControl1, g => g.DataSource, x => x.Tasks);

            // Sync Selection View -> ViewModel
            fluent.WithEvent<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs>(
                tileView, "FocusedRowChanged"
            ).SetBinding(x => x.SelectedTask, args => tileView.GetRow(args.FocusedRowHandle) as ProjectTask);

            // Sync Selection ViewModel -> View
            fluent.SetTrigger(x => x.SelectedTask, (task) => {
                if (task == null) return;
                int rowHandle = tileView.LocateByValue(nameof(ProjectTask.Id), task.Id);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle && rowHandle != tileView.FocusedRowHandle)
                    tileView.FocusedRowHandle = rowHandle;
            });
        }
    }
}
