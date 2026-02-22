using Microsoft.AspNetCore.Components;

namespace DevConnect.Client.Components.Common
{
    public partial class Modal
    {
        [Parameter] public string ButtonText { get; set; } = "Open Modal";
        [Parameter] public string Title { get; set; } = "Modal Title";
        [Parameter] public RenderFragment? Body { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public EventCallback<bool> IsVisibleChanged { get; set; }
        [Parameter] public string ModalButtonColor { get; set; } = "primary";

        private void Show() => IsVisible = true;
        private void Hide() => IsVisible = false;

        private async Task Save()
        {
            await OnSave.InvokeAsync();
            Hide();
        }
    }
}
