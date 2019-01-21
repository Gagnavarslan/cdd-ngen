namespace CoreData.Common.HostEnvironment
{
    /// <summary>Shell command - command prompt, PS, etc.</summary>
    /// <typeparam name="T">Command start context</typeparam>
    public class ShellCommand<T>
    {
        public ShellCommand(string command, T startInfo)
        {
            Command = command;
            StartInfo = startInfo;
        }
        public string Command { get; }
        //public Func<T> Init { get; }
        public T StartInfo { get; }
    }
}
