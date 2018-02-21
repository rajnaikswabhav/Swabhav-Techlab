namespace Techlabs.Euphoria.Kernel.Service.SMS
{
    public interface ISmsService
    {
        void Send(string mobileNo, string text);
        
    }

}
