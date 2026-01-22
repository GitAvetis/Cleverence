using System.Collections.Concurrent;
using System.Diagnostics;

namespace Task2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Test();

        }

        private static async Task Test()
        {
            int numOfReaders = 5;
            int numOfWriters = 2;

            Task[] readers = new Task[numOfReaders];
            Task[] writers = new Task[numOfWriters];

            for (int i = 0; i < numOfReaders; i++)
            {
                readers[i] = Task.Run(() =>
                {
                    int res = Server.GetCount();
                });
            }

            for (int i = 0; i < numOfWriters; i++)
            {
                int value = 1 + i;
                writers[i] = Task.Run(() =>
                {
                    Server.AddToCount(value);
                });
            }

            await Task.WhenAll(readers);
            await Task.WhenAll(writers);

        }
    }
}
