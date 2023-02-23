var jsFunctions = {};

jsFunctions.calculateSquareRoot = function () {
    const number = prompt("Enter your number");

    // 3 parameters:
    // - assembly name
    // - name of method
    // - parameter
    // the result is a promise
    DotNet.invokeMethodAsync("BlazorProducts.Client", "CalculateSquareRoot", parseInt(number))
        .then(result => {
            var el = document.getElementById("string-result");

            el.innerHTML = result;
        })
}