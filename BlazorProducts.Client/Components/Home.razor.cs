using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace BlazorProducts.Client.Components
{
    public partial class Home
    {
        // EditorRequired make the parameter required for the parent component
        [EditorRequired]
        [Parameter]
        public string? Title { get; set; }

        // set addional paramenter as a collection
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

        [CascadingParameter(Name = "HeadingColor")]
        public string? Color { get; set; }
        [Parameter]
        public RenderFragment? NewContentProperty { get; set; }
        [Inject]
        public IToastService? ToastService { get; set; }

        protected override void OnInitialized()
        {
            ToastService?.ShowSuccess("page loaded");
        }
    }
}
