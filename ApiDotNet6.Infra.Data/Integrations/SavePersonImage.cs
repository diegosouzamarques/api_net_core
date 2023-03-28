using ApiDotNet6.Domain.Integrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Integrations
{
    public class SavePersonImage : ISavePersonImage
    {
        private readonly string _filePath;

        public SavePersonImage()
        {
            _filePath = "C:/Users/diego/OneDrive/Imagens/.netcore";
        }

        public string Save(string imageBase64)
        {
            var fileExt = imageBase64.Substring(imageBase64.IndexOf("/") + 1, 
                                                imageBase64.IndexOf(";") - imageBase64.IndexOf("/") -1);

            var base64Code = imageBase64.Substring(imageBase64.IndexOf(",") + 1);
            var imgByte = Convert.FromBase64String(base64Code);
            var fileName = Guid.NewGuid().ToString() + "." + fileExt;

            using(var imageFile = new FileStream(_filePath+"/"+fileName, FileMode.Create))
            {
                imageFile.Write(imgByte, 0, imgByte.Length);
                imageFile.Flush();
            }

            return _filePath + "/" + fileName;
        }
    }
}
