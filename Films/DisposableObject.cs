using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films
{
    public abstract class DisposableObject
    {
        private bool isDisposed;

        ~DisposableObject()
        {
            TryDispose(disposing: false);
        }

        public void Dispose()
        {
            TryDispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        private void TryDispose(bool disposing)
        {
            if(!isDisposed)
            {
                Dispose(disposing);
                isDisposed = true;
            }
        }
    }
}
