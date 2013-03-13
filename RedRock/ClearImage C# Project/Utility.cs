using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Main
{
    class Utility
    {
        internal static MemoryStream FileToStream(string fileName)
        {
            // Convert file to stream
            FileStream fs = File.OpenRead(fileName);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(b);
            return ms;
        }

        internal static void StreamToFile(Stream stream, string fileOut)
        {
            byte[] buffer = new byte[256];
            FileStream fileStream = new FileStream(fileOut, FileMode.Create, FileAccess.Write);
            int bytesRead = stream.Read(buffer, 0, 256);
            while (bytesRead > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
                bytesRead = stream.Read(buffer, 0, 256);
            }
            stream.Close();
            fileStream.Close();
        }
    }
}
