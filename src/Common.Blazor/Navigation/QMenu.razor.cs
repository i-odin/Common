using Microsoft.AspNetCore.Components;

namespace Common.Blazor.Navigation;
public partial class QMenu
{
    [Parameter]
    public RenderFragment MenuTemplate { get; set; }
}