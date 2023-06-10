namespace E_CommercePortal.Models
{
    public interface ISettings
    {
        public string BaseApiUrl { get; set; }
    }
    public class Settings: ISettings
    {
       public string BaseApiUrl { get; set; }
    }
}
