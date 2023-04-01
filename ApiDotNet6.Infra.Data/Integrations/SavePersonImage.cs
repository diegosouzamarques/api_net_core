using ApiDotNet6.Domain.Integrations;

namespace ApiDotNet6.Infra.Data.Integrations
{
    public class SavePersonImage : ISavePersonImage
    {
        private readonly string _filePath;

        public SavePersonImage()
        {
            _filePath = "D:/ApiDotNetCore6/arquivos";
        }

        public string Save(byte[] file, string fileExt)
        {
         
            BinaryWriter Writer = null;

            var fileName = Guid.NewGuid().ToString() + "." + fileExt;

            Writer = new BinaryWriter(File.OpenWrite(_filePath + "/" + fileName));               
            Writer.Write(file);
            Writer.Flush();
            Writer.Close();


            return _filePath + "/" + fileName;
        }
    }
}
