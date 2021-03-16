using System;
using System.Threading;

namespace NetImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t = new Task<int>(Do);
            bool b = true;
            t.ContinueWith(
                i =>
                    {
                        b = false;
                    });
            t.Start();
            while (b)
            {
                Console.WriteLine("run.....");
            }
            Console.WriteLine(t.Result);
        }

        private static int Do()
        {
            Thread.Sleep(1000 * 3);
            return 10;
        }
    }

    public class Task<T>
    {
        private T _result;

        private Thread _thread;

        private Action<T> _continueAction;

        public Task(Func<T> func)
        {
            _thread = new Thread(
                () =>
                    {
                        _result = func();
                        _continueAction(_result);
                    });
        }

        public T Result
        {
            get
            {
                Wait();
                return _result;
            }
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Wait()
        {
            _thread.Join();
        }

        public void ContinueWith(Action<T> action)
        {
            _continueAction = action;
        }
    }
}
