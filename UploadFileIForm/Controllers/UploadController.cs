using Microsoft.AspNetCore.Mvc;
using UploadFileIForm.Models;

namespace UploadFileIForm.Controllers
{
    public class UploadController : Controller
    {
        //GET: Upload/Index ///Upload Single File
        public IActionResult Index()
        {
            SingleFileModel model = new SingleFileModel();
            return View(model);
        }

        //POST: Upload/Upload //Upload Single File
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

        //GET: Upload/MultipleFile
        public IActionResult MultipleFile()
        {
            MultipleFilesModel model = new MultipleFilesModel();
            return View(model);  
        }

        //POST: Upload/MultipleUpload
        public IActionResult MultipleUpload(MultipleFilesModel model)
        {
            if(ModelState.IsValid)
            {
                model.IsResponse=true;
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        string fileNameWithPath = Path.Combine(path, file.FileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    model.IsSuccess = true;
                    model.Message = "Files Uploaded Successfully!!";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Please select files";
                }

            }
            return View("MultipleFile", model);
        }
    }
}
