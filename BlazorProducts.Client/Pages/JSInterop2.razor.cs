using Microsoft.JSInterop;

namespace BlazorProducts.Client.Pages
{
    public partial class JSInterop2
    {
        [JSInvokable]
        public static string CalculateSquareRoot(int number)
        {
            var result = Math.Sqrt(number);

            return $"The square root of {number} is {result}";
        }
    }
}
