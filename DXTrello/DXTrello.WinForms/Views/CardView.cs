using DXTrello.ViewModel.ViewModels;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Columns;
using DXTrello.Core.Models;
using DevExpress.Utils;
using DevExpress.Utils.Html;
using DevExpress.Utils.Html.ViewInfo;
using DXTrello.Core.Enums;
using DXTrello.ViewModel.Services;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraGrid;

namespace DXTrello.WinForms {
    public partial class CardView : DevExpress.XtraEditors.XtraUserControl {
        TileView tileView;

        public CardView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeKanban();
                InitializeBindings();
                RegisterServices();
            }
        }

        void InitializeKanban() {
            // Replace the default View with a TileView
            tileView = new TileView(gridControl1);
            gridControl1.MainView = tileView;
            tileView.CustomItemTemplate += CustomItemTemplate;
            tileView.MouseDown += (s, e) => {
                var hitInfo = tileView.CalcHitInfo(e.Location);
                if(hitInfo.HitTest != DevExpress.XtraEditors.TileControlHitTest.Item) {
                    tileView.FocusedRowHandle = GridControl.InvalidRowHandle;
                }
            };

            // Define Columns
            var colId = AddColumn(nameof(ProjectTask.Id));
            var colStatus = AddColumn(nameof(ProjectTask.Status));
            var colTitle = AddColumn(nameof(ProjectTask.Title));
            var colTrimDesc = AddColumn(nameof(ProjectTask.TrimmedDescription));
            var colEndDate = AddColumn(nameof(ProjectTask.EndDate));
            var colStartDate = AddColumn(nameof(ProjectTask.StartDate));
            var colAssignee = AddColumn(nameof(ProjectTask.AssigneeName));
            var colDesc = AddColumn(nameof(ProjectTask.Description));

            // Configure Kanban Mode
            tileView.OptionsTiles.LayoutMode = TileViewLayoutMode.Kanban;
            tileView.OptionsTiles.VerticalContentAlignment = VertAlignment.Top;
            tileView.ColumnSet.GroupColumn = colStatus;
            tileView.OptionsKanban.GroupFooterButton.Text = "Add new card";
            tileView.OptionsKanban.GroupFooterButton.Visible = DefaultBoolean.True;
            tileView.OptionsKanban.ShowGroupBackground = DefaultBoolean.True;
            tileView.OptionsTiles.IndentBetweenGroups = 20;
            tileView.Appearance.Group.BackColor = SystemColors.ControlLightLight;
            tileView.Appearance.Group.Options.UseBackColor = true;

            // Configuring context button
            SimpleContextButton addButton = new SimpleContextButton();
            addButton.AlignmentOptions.Panel = ContextItemPanel.Center;
            addButton.AlignmentOptions.Position = ContextItemPosition.Far;
            addButton.ImageOptionsCollection.ItemNormal.SvgImage = DevExpress.Utils.Svg.SvgImage.FromFile("Resources/Add.svg");
            addButton.ImageOptionsCollection.ItemNormal.SvgImageSize = new Size(16, 16);
            tileView.OptionsKanban.GroupHeaderContextButtons.Add(addButton);

            // Allow Drag & Drop to change Status
            tileView.OptionsDragDrop.AllowDrag = true;

            // Configure HTML Template
            tileView.OptionsTiles.ItemSize = new Size(300, 0);
            tileView.OptionsHtmlTemplate.ItemAutoHeight = true;

            // Setting HTML template for cards with End Date.
            HtmlTemplate endDateCardTemplate = new HtmlTemplate();
            endDateCardTemplate.Template = @"
                <div class='card'>
                    <div class='title'>${Title}</div>
                    <div class='desc'>${TrimmedDescription}</div>
                    <div class='footer'>
                        <div class='date-badge'>${EndDate}</div>
                        <div class='user'>${AssigneeName}</div>
                    </div>
                </div>";

            // Setting HTML template for cards without End Date.
            HtmlTemplate cardTemplate = new HtmlTemplate();
            cardTemplate.Template = @"
                <div class='card'>
                    <div class='title'>${Title}</div>
                    <div class='desc'>${TrimmedDescription}</div>
                    <div class='footer'>
                        <div class='spacer'></div>
                        <div class='user'>${AssigneeName}</div>
                    </div>
                </div>";

            // Setting default template and styles.
            tileView.TileHtmlTemplate.Template = cardTemplate.Template;
            tileView.TileHtmlTemplate.Styles = @"
                .card {
                    display: flex;
                    flex-direction: column;
                    padding: 8px;
                    background-color: @ControlLightLight;
                    border: 1px solid @ControlLight;
                    border-radius: 4px;
                    min-height: 65px;
                }
                .title {
                    font-size: 8px;
                    font-weight: bold;
                    margin-bottom: 4px;
                }
                .desc {
                    font-size: 8px;
                    flex-grow: 1;
                    color: @DisabledText;
                    overflow: hidden;
                    white-space: normal;
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
                .spacer {
                    padding: 2px 6px;
                }
                .user {
                    font-size: 8px;
                    font-weight: 600;
                    color: @ControlText;
                }
            ";

            tileView.TileHtmlTemplates.AddRange([endDateCardTemplate, cardTemplate]);
        }

        void CustomItemTemplate(object sender, TileViewCustomItemTemplateEventArgs e) {
            var endDateValue = tileView.GetRowCellValue(e.RowHandle, nameof(ProjectTask.EndDate));
            DateTime endDate = endDateValue != null ? (DateTime)endDateValue : DateTime.MinValue;
            HtmlTemplate targetTemplate = endDate != DateTime.MinValue ?
                tileView.TileHtmlTemplates[0] : tileView.TileHtmlTemplates[1];
            e.HtmlTemplate.Template = targetTemplate.Template;
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
                if(task == null) {
                    tileView.FocusedRowHandle = GridControl.InvalidRowHandle;
                    return;
                }
                int rowHandle = tileView.LocateByValue(nameof(ProjectTask.Id), task.Id);
                if (rowHandle != GridControl.InvalidRowHandle && rowHandle != tileView.FocusedRowHandle)
                    tileView.FocusedRowHandle = rowHandle;
            });

            // Handle buttons
            fluent.WithEvent<GroupFooterButtonClickEventArgs>(tileView, "GroupFooterButtonClick")
                .EventToCommand(
                    x => x.AppendCreateNewTask,
                    args => (ProjectTaskStatus)args.GroupValue
                );
            fluent.WithEvent<GroupHeaderContextButtonClickEventArgs>(tileView, "GroupHeaderContextButtonClick")
                .EventToCommand(
                    x => x.PrependCreateNewTask,
                    args => (ProjectTaskStatus)args.GroupValue
                );
            fluent.WithEvent<KeyEventArgs>(tileView, "KeyUp")
                .EventToCommand(
                    x => x.ProcessKey,
                    p => p.KeyValue
                );

            mvvmContext.RegisterService(this);
        }

        void RegisterServices() {
            mvvmContext.RegisterService(this);
            mvvmContext.RegisterService(DialogService.CreateXtraDialogService(this));
        }
    }
}
