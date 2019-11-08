using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerLog.Interfaces
{
    public interface IConsumerService
    {
        void StartConsumeData(string topic);
    }
}
