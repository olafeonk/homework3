using System.Diagnostics;

namespace homework3;

public static class Program
{
    public static void Main()
    {
        void TestCount()
        {
            var stack = new MyConcurrentStack<string>();
            var thread1 = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                    stack.Push(i.ToString());
            });
            var thread2 = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                    stack.Push(i.ToString());
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Debug.Assert(stack.Count == 200);
        }
        
        void TestTryPop()
        {
            var stack = new MyConcurrentStack<string>();
            var thread1 = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                    stack.Push(i.ToString());
            });
            var thread2 = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    Debug.Assert(stack.TryPop(out _));
                    Debug.Assert(stack.Count == 99 - i);
                }
            });
            thread1.Start();
            thread2.Start();
        }
        
        TestCount();
        TestTryPop();
    }
}