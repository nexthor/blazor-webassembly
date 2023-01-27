using Microsoft.AspNetCore.Components;
using System.Xml.XPath;

namespace BlazorProducts.Client.Pages
{
    public partial class CustomNotFound
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        public void NavigateToHome()
        {
            NavigationManager?.NavigateTo("/");
        }
    }
}
