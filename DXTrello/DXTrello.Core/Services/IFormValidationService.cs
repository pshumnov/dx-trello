using System;
using System.Collections.Generic;
using System.Text;

namespace DXTrello.Core.Services {
    public interface IFormValidationService {
        void ValidateForm();
        bool IsFormValid { get; }
    }
}
