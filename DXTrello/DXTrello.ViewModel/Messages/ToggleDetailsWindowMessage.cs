using System;
using System.Collections.Generic;
using System.Text;

namespace DXTrello.ViewModel.Messages {
    public class ToggleDetailsWindowMessage {
        public bool Show { get; }
        public ToggleDetailsWindowMessage(bool show) {
            Show = show;
        }
    }
}
