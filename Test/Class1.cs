using System;
using System.Threading;

namespace Test
{
    public class Class1
    {
        public void get()
        {
            var result = 0;
            Interlocked.Increment(ref result);
        }
    }
}
