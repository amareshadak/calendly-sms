using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calendly.Core
{
    public interface ISmsClient
    {
        /// <summary>
        /// Basic interface for sending an SMS message
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Send(string to, string message);
    }
}
