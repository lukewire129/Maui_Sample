using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Maui_Sample.ViewModels.ValueChangedMessage
{
        public class CheckStateMesage
        {
                public TERSTTYPE type { get; set; }
                public bool state { get; set; }
        }
        public class CheckStateChangedMessage : ValueChangedMessage<CheckStateMesage>
        {
                public CheckStateChangedMessage(CheckStateMesage value) : base(value)
                {
                }
        }
}
