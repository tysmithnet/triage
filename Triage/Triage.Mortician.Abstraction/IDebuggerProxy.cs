namespace Triage.Mortician.Abstraction
{
    public interface IDebuggerProxy
    {
        void Dispose();
        string Execute(string cmd);
    }
}