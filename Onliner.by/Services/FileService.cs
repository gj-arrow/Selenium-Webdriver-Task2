using System.Collections.Generic;
using System.IO;

namespace Onliner.Services
{
  public static class FileService
    {
        private const string FormatFile = ".csv";

        public static void WriteFileCsv(List<string> listDatatoFile, string path, string nameFile)
        {
            using (var writerToCsv = File.CreateText(path + nameFile + FormatFile))
            {
                foreach (var item in listDatatoFile)
                {
                    writerToCsv.WriteLine(item);
                }
            }
        }
    }
}
