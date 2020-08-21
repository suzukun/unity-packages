namespace Promise
{
    using UniRx;
    using System;

    public class Promise <T>
    {
        public delegate T ChainReturnDelegate(T value);
        public delegate void ChainDelegate(T value);
        public delegate void ChainNoParamDelegate();
        public delegate Promise<T> ChainPromiseDelegate(T value);
        public delegate void ExecutorDelegate(ChainDelegate LocalResolve, ChainDelegate LocalReject);
        public delegate void ExecutorNoParamDelegate(ChainNoParamDelegate LocalResolve, ChainNoParamDelegate LocalReject);

        private AsyncSubject<T> _asyncSubject = new AsyncSubject<T>();
        private object lockObject = new object();
        private T _payload;
        private float _delay = 0.0f;
        private bool _isFulfilled = false;
        private bool _isRejected = false;

        public Promise(ExecutorDelegate Executor)
        {
            Executor(_Resolve, _Reject);
        }

        public Promise(ExecutorNoParamDelegate Executor)
        {
            Executor(() => _Resolve(default(T)), () => _Reject(default(T)));
        }

        private Promise(ExecutorDelegate Executor, float delay)
        {
            _delay = delay;
            Executor(_Resolve, _Reject);
        }

        private Promise(ExecutorNoParamDelegate Executor, float delay)
        {
            _delay = delay;
            Executor(() => _Resolve(default(T)), () => _Reject(default(T)));
        }

        public static Promise<T> Reject()
        {
            return new Promise<T>((LocalResolve, LocalReject) => LocalReject());
        }

        public static Promise<T> Reject(T value)
        {
            return new Promise<T>((LocalResolve, LocalReject) => LocalReject(value));
        }

        public static Promise<T> Resolve()
        {
            return new Promise<T>((LocalResolve, LocalReject) => LocalResolve());
        }

        public static Promise<T> Resolve(T value)
        {
            return new Promise<T>((LocalResolve, LocalReject) => LocalResolve(value));
        }

        public static Promise<T> Wait(float delay)
        {
            return new Promise<T>((LocalResolve, LocalReject) => LocalResolve(), delay);
        }

        public static Promise<T> Wait(float delay, T value)
        {
            return new Promise<T>((LocalResolve, LocalReject) => LocalResolve(value), delay);
        }

        public Promise<T> Catch(ChainDelegate Chain)
        {
            return _Catch(_ => {
                Chain(_payload);
                return default(T);
            });
        }

        public Promise<T> Catch(ChainReturnDelegate Chain)
        {
            return _Catch(Chain);
        }

        public Promise<T> Catch(ChainPromiseDelegate ChainPromise)
        {
            return ChainPromise(_payload);
        }

        public Promise<T> Then(ChainDelegate Chain)
        {
            return _Then(_ => {
                Chain(_payload);
                return default(T);
            });
        }

        public Promise<T> Then(ChainReturnDelegate Chain)
        {
            return _Then(Chain);
        }

        public Promise<T> Then(ChainPromiseDelegate ChainPromise)
        {
            return ChainPromise(_payload);
        }

        private Promise<T> _Catch(ChainReturnDelegate Chain)
        {
            lock(lockObject)
            {
                return new Promise<T>((LocalResolve, LocalReject) => {
                    try
                    {
                        _asyncSubject
                            .First()
                            .Where(_ => _isRejected)
                            .Subscribe(_ => {
                                _payload = Chain(_payload);
                                LocalResolve(_payload);
                            });

                        _asyncSubject
                            .First()
                            .Where(_ => _isFulfilled)
                            .Subscribe(_ => {
                                LocalResolve(_payload);
                            });
                    }
                    catch
                    {
                        LocalReject(_payload);
                    }
                });
            }
        }

        private void _Reject(T value)
        {
            lock(lockObject)
            {
                if (_isFulfilled) return;

                _isRejected = true;
                _payload = value;
                _asyncSubject.OnNext(value);
                _asyncSubject.OnCompleted();
            }
        }

        private void _Resolve(T value)
        {
            lock(lockObject)
            {
                if (_isRejected) return;

                _isFulfilled = true;
                _payload = value; 
                _asyncSubject.OnNext(value);
                _asyncSubject.OnCompleted();
            }
        }

        private Promise<T> _Then(ChainReturnDelegate Chain)
        {
            lock(lockObject)
            {
                return new Promise<T>((LocalResolve, LocalReject) => {
                    try
                    {
                        _asyncSubject
                            .First()
                            .Where(_ => _isFulfilled)
                            .Delay(TimeSpan.FromMilliseconds(_delay))
                            .Subscribe(_ => {
                                _payload = Chain(_payload);
                                LocalResolve(_payload);
                            });

                        _asyncSubject
                            .First()
                            .Where(_ => _isRejected)
                            .Subscribe(_ => {
                                LocalReject(_payload);
                            });
                    }
                    catch
                    {
                        LocalReject(_payload);
                    }
                });
            }
        }
    }
}
