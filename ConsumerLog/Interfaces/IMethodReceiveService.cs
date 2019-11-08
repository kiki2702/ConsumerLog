using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerLog.Interfaces
{
    public interface IMethodReceiveService
    {
        void Consume(string topic, Action<string> processingMethod, Action<Exception> errorHandlingMethod);
    }
}
