using NSL.DataConversion.Core.Xlsx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Tests.Integration.Xlsx
{
    public class XlsxFileResolverTests : IXlsxFileResolverTests<XlsxFileResolver>
    {
        protected override XlsxFileResolver GetInstance()
        {
            return new XlsxFileResolver();
        }
    }
}
