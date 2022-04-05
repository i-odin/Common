using Microsoft.AspNetCore.Components;

namespace Common.Blazor.Layouts;
public partial class QMain
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public int GridColumn { get; set; } = 12;

    public string CssClass => $"main-col-{GridColumn}";
}