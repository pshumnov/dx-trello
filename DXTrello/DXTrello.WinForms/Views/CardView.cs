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
using DXTrello.Core.Services;
using System.Net.Http;
using System.IO;
using DXTrello.WinForms.HTMLTemplates;

namespace DXTrello.WinForms {
    public partial class CardView : DevExpress.XtraEditors.XtraUserControl, ICardViewService {
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
            var colAvatar = AddColumn(nameof(ProjectTask.AssigneeAvatar));
            var colDesc = AddColumn(nameof(ProjectTask.Description));

            // Define groups
            var todo = tileView.OptionsKanban.Groups.Add();
            todo.GroupValue = ProjectTaskStatus.ToDo;
            todo.Caption = "To Do";
            var inprogress = tileView.OptionsKanban.Groups.Add();
            inprogress.GroupValue = ProjectTaskStatus.InProgress;
            inprogress.Caption = "In Progress";
            var review = tileView.OptionsKanban.Groups.Add();
            review.GroupValue = ProjectTaskStatus.Review;
            review.Caption = "Review";
            var done = tileView.OptionsKanban.Groups.Add();
            done.GroupValue = ProjectTaskStatus.Done;
            done.Caption = "Done";

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

            // Setting default template and styles.
            tileView.TileHtmlTemplate.Template = CardHTMLTemplates.CardTemplate;
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
                .user-container {
                    display: flex;
                    align-items: center;
                    width: 50%;
                }
                .avatar {
                    width: 16px;
                    height: 16px;
                    border-radius: 50%;
                    flex-shrink: 0;
                }
                .date-container {
                    display: flex;
                    width: 50%;
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
                    overflow: hidden;
                    white-space: normal;
                    text-overflow: ellipsis;
                    text-align: center;
                    width: 100%;
                }
            ";
        }

        void CustomItemTemplate(object sender, TileViewCustomItemTemplateEventArgs e) {
            var task = tileView.GetRow(e.RowHandle) as ProjectTask;
            e.HtmlTemplate.Template = CardHTMLTemplates.GetCardTemplate(task);
        }

        TileViewColumn AddColumn(string fieldName) {
            TileViewColumn col = (TileViewColumn)tileView.Columns.AddVisible(fieldName);
            return col;
        }

        void InitializeBindings() {
            var fluent = mvvmContext.OfType<CardViewModel>();
            fluent.SetBinding(gridControl1, g => g.DataSource, x => x.Tasks);
            fluent.SetTrigger(x => x.Users, LoadAvatars);

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

            fluent.WithEvent(this, "Load")
                .EventToCommand(x => x.OnViewLoad);

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
            fluent.WithEvent<MouseEventArgs>(tileView, "ItemDoubleClick")
                .EventToCommand(
                    x => x.OpenDetailsPanel
                );

            mvvmContext.RegisterService(this);
        }

        void RegisterServices() {
            mvvmContext.RegisterService(this);
            mvvmContext.RegisterService(DialogService.CreateXtraDialogService(this));
        }

        public async void LoadAvatars(IList<TeamMember> users) {
            if(users == null) return;
            var collection = new ImageCollection();
            using HttpClient client = new HttpClient();
            foreach(var user in users) {
                if(string.IsNullOrEmpty(user.AvatarUrl)) continue;
                try {
                    var bytes = await client.GetByteArrayAsync(user.AvatarUrl);
                    using var ms = new MemoryStream(bytes);
                    using var tempImage = Image.FromStream(ms);
                    collection.AddImage(new Bitmap(tempImage), user.AvatarUrl);
                }
                catch { }
            }
            tileView.HtmlImages = collection;
        }
    }
}
