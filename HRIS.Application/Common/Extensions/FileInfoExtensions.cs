using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HRIS.Application.Common.Extensions
{
    public static class FileInfoExtensions
    {
        public static void Rename(this FileInfo fileInfo, string newName)
        {
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
        }
        
    }
}

