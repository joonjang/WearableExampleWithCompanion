using System;
using System.Collections.Generic;
using System.Text;

namespace WearableCompanion
{
    public interface IProviderService
    {
        bool CloseConnection();
        void SendData(string msg);

    }
}
