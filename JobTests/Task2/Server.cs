namespace Task2
{
    public static class Server
    {
        private static ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
      //  public static event Action<string> OnOperationCompleted;

        private static int _count = 0;

        public static int GetCount()
        {
            cacheLock.EnterReadLock();
            try
            {
                Console.WriteLine($"GetCount Started by T{Task.CurrentId} at {Environment.TickCount}");
                Thread.Sleep(10);// имитация работы
                return _count;
            }
            finally
            {
                cacheLock.ExitReadLock();
                Console.WriteLine($"GetCount finished by T{Task.CurrentId} at {Environment.TickCount}");
            }
        }

        public static void AddToCount(int value)
        {
            cacheLock.EnterWriteLock();

            try
            {
                _count += value;
                Console.WriteLine($"AddToCount({value}) Started by T{Task.CurrentId} at {Environment.TickCount}");
                Thread.Sleep(50);// имитация работы

            }
            finally
            {
                cacheLock.ExitWriteLock();
                Console.WriteLine($"AddToCount({value}) finished by T{Task.CurrentId} at {Environment.TickCount}");
            }
        }

        //public static int GetCount()
        //{
        //    cacheLock.EnterReadLock();
        //    try
        //    {
        //        OnOperationCompleted?.Invoke($"GetCount Started by T{Task.CurrentId}");
        //        Thread.Sleep(10);// имитация работы
        //        return _count;
        //    }
        //    finally
        //    {
        //        cacheLock.ExitReadLock();
        //        OnOperationCompleted?.Invoke($"GetCount finished by T{Task.CurrentId}");
        //    }
        //}

        //public static void AddToCount(int value)
        //{
        //    cacheLock.EnterWriteLock();

        //    try
        //    {
        //        _count += value;
        //        OnOperationCompleted?.Invoke($"AddToCount({value}) Started by T{Task.CurrentId}");
        //        Thread.Sleep(50);// имитация работы

        //    }
        //    finally
        //    {
        //        cacheLock.ExitWriteLock();
        //        OnOperationCompleted?.Invoke($"AddToCount({value}) finished by T{Task.CurrentId}");
        //    }
        //}
    }
}
