using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Contracts
{
    public interface IFileUpload
    {

        public Task Uploadfile(IFileListEntry file,MemoryStream msFile, string picName);
    }
}
