namespace MonitoringDemo.Services
{
    public interface ILogFileReader
    {
        /// <summary>
        /// The path to the active log file.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Returns the content of the log file <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The path to the log file.</param>
        string ReadLogFile(string filePath);

        /// <summary>
        /// Returns the content of the log file <seealso cref="FilePath"/>.
        /// </summary>
        /// <param name="numberOfLines">Limits the content to the last N lines.</param>
        Task<string> ReadLogFileAsync(long numberOfLines);

        /// <summary>
        /// Returns a list of all log files, including archived/rolling log files.
        /// </summary>
        IEnumerable<string> EnumerateLogFiles();

        /// <summary>
        /// Deletes all log files.
        /// </summary>
        int DeleteLogFiles();
    }
}