using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityPass.Services
{
    public interface IQRCodeScanner
    {
        System.Threading.Tasks.Task<string> ScanAsync();
    }
}
