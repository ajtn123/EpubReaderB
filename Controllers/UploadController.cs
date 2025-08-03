using Microsoft.AspNetCore.Mvc;

namespace EpubReaderB.Controllers;

public class UploadController : Controller
{
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest(new { success = false, message = "No file uploaded" });

        Console.WriteLine($"Uploaded: {file.FileName}");

        if (await EpubInfo.InitializeBook(file.OpenReadStream()))
            return Ok(new { success = true, redirect = Url.Action("Index", "Home") });
        else return BadRequest(new { success = false, message = "Invalid file" });
    }
}
