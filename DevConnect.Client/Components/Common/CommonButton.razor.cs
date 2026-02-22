using Microsoft.AspNetCore.Components;

namespace DevConnect.Client.Components.Common
{
    public partial class CommonButton
    {
        /// <summary>
        /// Theme color:
        /// <list type="bullet">
        ///   <item><description><c>primary</c></description></item>
        ///   <item><description><c>danger</c></description></item>
        ///   <item>
        ///     <description>
        ///     Any valid CSS color (name, hex, rgb, rgba, hsl).
        ///     </description>
        ///   </item>
        /// </list>
        /// </summary>
        [Parameter] public string? Color { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public string ButtonText { get; set; } = "Click Me";
        [Parameter] public bool IsDisabled { get; set; }

        private bool IsCustomColor =>
            !string.IsNullOrWhiteSpace(Color) &&
            Color is not ("primary" or "danger");

        private string BuildClasses()
        {
            var baseClasses = "btn btn-sm fw-semibold rounded text-nowrap shadow-sm";

            if (string.IsNullOrWhiteSpace(Color))
                return $"{baseClasses} bg-surface text-text border-secondary";

            return Color.ToLower() switch
            {
                "primary" => $"{baseClasses} bg-primary text-white border-0",
                "danger" => $"{baseClasses} bg-danger text-white border-0",
                _ => $"{baseClasses} text-white border-0"
            };
        }

        private string? BuildStyle()
        {
            if (!IsCustomColor)
                return null;

            return $"background-color:{Color};";
        }

        private async Task HandleClick()
        {
            if (!IsDisabled)
                await OnClick.InvokeAsync();
        }
    }
}
