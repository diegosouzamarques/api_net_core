
namespace ApiDotNet6.Domain.Integrations
{
    public interface ISavePersonImage
    {
        string Save(byte[] file, string fileExt);
    }
}
