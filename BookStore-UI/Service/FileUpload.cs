using BlazorInputFile;
using BookStore_UI.Contracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _env;
        public  FileUpload(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task Uploadfile(IFileListEntry file,MemoryStream msFile, string picName)
        {
            try
            {
                var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);
                var path = $"{_env.WebRootPath}\\Uploads\\{picName}";
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    msFile.WriteTo(fs);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }

    }
}
