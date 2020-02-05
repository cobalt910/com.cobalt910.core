namespace ProjectCore.GUI
{
    public interface IWindowController
    {
        WindowView View { get; set; }
        int WindowId { get; }
    }
}