using System;
using System.Threading;

namespace ConcurrencyTest
{
    public class TestTask
    {
        private readonly Random rand;
        private bool isBusy = false;

        public int TaskId { get; private set; }

        public TestTask(int seed)
        {
            rand = new Random(seed);
            TaskId = seed;
        }

        public int ReceiveData()
        {
            if (isBusy)
                return -1;

            isBusy = true;
            var i = rand.Next(0, 1000);
            if (i < 800)
            {
                isBusy = false;
                return -1;
            }
            Thread.Sleep(100);
            isBusy = false;

            return i;
        }
    }
}
