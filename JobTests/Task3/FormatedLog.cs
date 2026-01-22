using static Task3.FormatedLog;

namespace Task3
{
    public class FormatedLog(string date, string time, LogLevel level, string? method, string message)
    {
        public enum LogLevel
        {
            INFO,
            WARN,
            ERROR,
            DEBUG,
            PARSE_ERROR
        }

        public string Date { get; } = date;
        public string Time { get; } = time;
        public LogLevel Level { get; } = level;
        public string? Method { get; } = method;
        public string Message { get; } = message;
    }
}
