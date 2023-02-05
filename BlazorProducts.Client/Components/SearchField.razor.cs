using Microsoft.AspNetCore.Components;
using System.Threading;

namespace BlazorProducts.Client.Components
{
    public partial class SearchField
    {
        public string? SearchTerm { get; set; }
        private Timer? _timer;
        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        private void SearchChanged()
        {
            if (_timer != null)
                _timer.Dispose();

            _timer = new Timer(OnTimerElapsed, null, 500, 500);
        }

        
        private void OnTimerElapsed(object? sender)
        {
            OnSearchChanged.InvokeAsync(SearchTerm);

            _timer?.Dispose();
        }
    }
}
