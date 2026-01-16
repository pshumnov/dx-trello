using DevExpress.CodeParser;
using DevExpress.Utils.Html;
using DXTrello.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXTrello.WinForms.HTMLTemplates {
    public static class CardHTMLTemplates {
        #region Card Templates
        public const string CardTemplate = """
            <div class='card'>
                <div class='title'>${Title}</div>
                <div class='desc'>${TrimmedDescription}</div>
            </div>
        """;
        public const string UserCardTemplate = """
            <div class='card'>
                <div class='title'>${Title}</div>
                <div class='desc'>${TrimmedDescription}</div>
                <div class='footer'>
                    <div class='date-container'></div>
                    <div class='user-container'>
                        <img class='avatar' src='${AssigneeAvatar}'/>
                        <div class='user'>${AssigneeName}</div>
                    </div>
                </div>
            </div>
        """;
        public const string EndDateCardTemplate = """
            <div class='card'>
                <div class='title'>${Title}</div>
                <div class='desc'>${TrimmedDescription}</div>
                <div class='footer'>
                    <div class='date-container'>
                        <div class='date-badge'>${EndDate}</div>
                    </div>
                    <div class='user-container'></div>
                </div>
            </div>
        """;
        public const string EndDateUserCardTemplate = """
            <div class='card'>
                <div class='title'>${Title}</div>
                <div class='desc'>${TrimmedDescription}</div>
                <div class='footer'>
                    <div class='date-container'>
                        <div class='date-badge'>${EndDate}</div>
                    </div>
                    <div class='user-container'>
                        <img class='avatar' src='${AssigneeAvatar}'/>
                        <div class='user'>${AssigneeName}</div>
                    </div>
                </div>
            </div>
        """;
        #endregion

        public static string GetCardTemplate(ProjectTask? target) {
            if(target == null)
                return string.Empty;
            bool hasEndDate = target.EndDate > DateTime.MinValue;
            bool hasAssignee = target.Assignee != null;
            if (hasEndDate && hasAssignee)
                return EndDateUserCardTemplate;
            else if (hasEndDate)
                return EndDateCardTemplate;
            else if (hasAssignee)
                return UserCardTemplate;
            else
                return CardTemplate;
        }
    }
}
