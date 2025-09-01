using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EpubReaderB.Controllers;

public class UploadController(IStringLocalizer<UploadController> localizer) : Controller
{
    private readonly IStringLocalizer localizer = localizer;

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest(new { success = false, message = localizer["NoUpload"].Value });

        Console.WriteLine($"Uploaded: {file.FileName}");

        if (await EpubInfo.InitializeBook(file.OpenReadStream()))
            return Ok(new { success = true, redirect = Url.Action("Index", "Home") });
        else return BadRequest(new { success = false, message = localizer["InvalidFile"].Value });
    }
}
