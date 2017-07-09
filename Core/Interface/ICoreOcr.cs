using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interface
{
    public interface ICoreOcr
    {
        Task<bool> LoadImage(string filename);

        Task<string> OcrPages(string language, int pages);

        Task<List<TextPage>> DecodeHocr(string page);
    }
}