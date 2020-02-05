namespace com.cobalt910.core.Runtime.Core.GUI
{
    public interface IWindowController
    {
        WindowView View { get; set; }
        int WindowId { get; }
    }
}