using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2.NET.Factories
{
    internal interface IAmazonS3ClientFactory
    {
        IAmazonS3 GetClient(string clientName, CancellationToken cancellationToken);
    }
}
