using BlazorProducts.Client.HttpRepositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace BlazorProducts.Client.Pages
{
    public partial class CompanyLogo
    {
        [Parameter]
        public string? ImgUrl { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        [Parameter]
        public Guid CompanyId { get; set; }
        [Inject]
        public ICompanyHttpRepository? CompanyRepository { get; set; }

        private async Task HandleSelected(InputFileChangeEventArgs eventArgs)
        {
            var imageFile = eventArgs.File;

            if (imageFile is null)
                return;

            var resizedFile = await imageFile.RequestImageFileAsync("image/png", 300, 500);

            using( var ms = resizedFile.OpenReadStream(resizedFile.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);

                ImgUrl = await CompanyRepository?.UploadCompanyLogo(CompanyId, content)!;

                await OnChange.InvokeAsync(ImgUrl);
            }
        }

    }
}
