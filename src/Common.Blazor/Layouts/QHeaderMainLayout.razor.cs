using Microsoft.AspNetCore.Components;

namespace Common.Blazor.Layouts
{
    public partial class QHeaderMainLayout
    {
        [Parameter]
        public RenderFragment HeaderTemplate { get; set; }

        [Parameter]
        public RenderFragment MainTemplate { get; set; }
    }
}
