using Microsoft.AspNetCore.Components;

namespace Common.Blazor.Navigation;

public partial class QNavBar
{
    [Parameter]
    public RenderFragment NavBarLogoTemplate { get; set; }
    [Parameter]
    public RenderFragment NavBarTemplate { get; set; }
}