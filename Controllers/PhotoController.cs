using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.IO;
using System.Threading.Tasks;
using ReportEase.api.Services; // Update to your namespace
using ReportEase.api.Models; // Update to your namespace

[ApiController]
[Route("api/photos")]
public class PhotoController : ControllerBase
{
    private readonly PhotoService _photoService;

    public PhotoController(PhotoService photoService)
    {
        _photoService = photoService;
    }

   
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile( IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File not selected.");
        }

        try
        {
            using (var stream = file.OpenReadStream())
            {
                var fileId = await _photoService.UploadFileAsync(stream, file.FileName, file.ContentType);
                return Ok(new { FileId = fileId.ToString() });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
        }
    }
 

  
    [HttpGet("download/{id}")]
    public async Task<IActionResult> GetPhotoById(string id)
    {
        if (!ObjectId.TryParse(id, out var photoId))
            return BadRequest("Invalid Photo ID.");

        try
        {
            var stream = await _photoService.GetPhotoStreamByIdAsync(photoId);
            var fileInfo = await _photoService.GetFileInfoAsync(photoId);
            return File(stream, fileInfo.Metadata.GetValue("contentType").AsString, fileInfo.Filename);
            
        }
        catch (FileNotFoundException)
        {
            return NotFound($"Photo with ID {id} not found.");
        }
    }
    
    
    /*  [HttpGet("download/{id}")]
         public async Task<IActionResult> DownloadFile(string id)
         {
             if (!ObjectId.TryParse(id, out ObjectId fileId))
             {
                 return BadRequest("Invalid file ID.");
             }

             try
             {
                 var stream = await _uploadService.DownloadFileAsync(fileId);
                 var fileInfo = await _uploadService.GetFileInfoAsync(fileId);

                 return File(stream, fileInfo.Metadata.GetValue("contentType").AsString, fileInfo.Filename);
             }
             catch (Exception ex)
             {
                 return StatusCode(StatusCodes.Status500InternalServerError, $"Error downloading file: {ex.Message}");
             }
         }*/

    /// <summary>
    /// Deletes a photo by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhoto(string id)
    {
        if (!ObjectId.TryParse(id, out var photoId))
            return BadRequest("Invalid Photo ID.");

        await _photoService.DeletePhotoAsync(id);
        return NoContent();
    }
}
