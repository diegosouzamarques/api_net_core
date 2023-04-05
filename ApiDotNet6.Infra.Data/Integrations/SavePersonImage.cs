using ApiDotNet6.Domain.Integrations;

namespace ApiDotNet6.Infra.Data.Integrations
{
    public class SavePersonImage : ISavePersonImage
    {
        private readonly string _filePath;

        public SavePersonImage()
        {
            // exemplo path
            // aws servie stoge img or idrive implements 
            _filePath = "C:/Users/Public/arquivos";
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
