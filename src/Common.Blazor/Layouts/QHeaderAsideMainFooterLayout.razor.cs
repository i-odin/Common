using Microsoft.AspNetCore.Components;

namespace Common.Blazor.Layouts;
public partial class QHeaderAsideMainFooterLayout
{
    
    [Parameter]
    public RenderFragment HeaderTemplate { get; set; }

    [Parameter]
    public RenderFragment AsideTemplate { get; set; }

    [Parameter]
    public RenderFragment MainTemplate { get; set; }

    [Parameter]
    public RenderFragment FooterTemplate { get; set; }
}