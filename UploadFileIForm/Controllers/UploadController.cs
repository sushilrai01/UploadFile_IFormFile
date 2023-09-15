using Microsoft.AspNetCore.Mvc;
using UploadFileIForm.Models;

namespace UploadFileIForm.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            SingleFileModel model = new SingleFileModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = model.FileName + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName); 

                using(var stream = new FileStream(fileNameWithPath,FileMode.Create))
                {
                    model.File.CopyTo(stream);  
                }

                model.IsSuccess = true;
                model.Message = "File Uploaded Successfully!";

            }
            return View("Index",model);
        }
    }
}
