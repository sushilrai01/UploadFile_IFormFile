using System.ComponentModel.DataAnnotations;

namespace UploadFileIForm.Models
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }

    public class SingleFileModel : ResponseModel
    {
        [Required(ErrorMessage = "Please enter file name")]
        public string FileName { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }
    }
    public class MultipleFilesModel : ResponseModel
    {
        [Required(ErrorMessage = "Please select files")]
        public List<IFormFile> Files { get; set; }
    }
}
