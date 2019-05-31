using System;
using System.Threading;

namespace TPCIP.CommonServiceFacade
{
   public class AsyncResult<TResult> : IAsyncResult, IDisposable
    {
        private readonly AutoResetEvent _asyncWaitHandle;
        private readonly object locker = new object();
        private bool _isCompleted;

        public AsyncResult()
        {
            _asyncWaitHandle = new AutoResetEvent(false);
        }


        public TResult Result { get; private set; }
        public Exception Error { get; private set; }

        object IAsyncResult.AsyncState
        {
            get { return Error ?? (object)Result; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return _asyncWaitHandle; }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        public bool IsCompleted
        {
            get
            {
                lock (locker)
                {
                    return _isCompleted;
                }
            }
        }

        public void RunAsync(Func<TResult> method)
        {
            new Action(() =>
            {
                try
                {
                    var result = method();
                    lock (locker)
                    {
                        Result = result;
                        _isCompleted = true;
                    }
                }
                catch
                {
                    _isCompleted = true;
                }
                finally
                {
                    _asyncWaitHandle.Set();
                }
            }).BeginInvoke(null, null);
        }


        public void WaitUntilCompleted()
        {
            if (_isCompleted)
            {
                return;
            }
            _asyncWaitHandle.WaitOne();
        }




        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _asyncWaitHandle.Close();
            }
        }
    }
}
