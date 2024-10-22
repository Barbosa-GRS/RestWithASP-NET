using RestWithASP_NET.Data.VO;

namespace RestWithASP_NET.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string filename);

        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
