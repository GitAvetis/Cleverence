namespace Task2
{
    public class LogMessage(int? id, long time, string message)
    {
        public string Message { get; } = message;
        public int? ID { get; } = id;
        public long Time { get; } = time;
    }
}
