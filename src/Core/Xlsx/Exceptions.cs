// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    [Serializable]
    public class XlsxException : Exception
    {
        public XlsxException()
        {
        }

        public XlsxException(string message) : base(message)
        {
        }

        public XlsxException(string message, Exception inner) : base(message, inner)
        {
        }

        protected XlsxException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class XlsxAccessException : Exception
    {
        public XlsxAccessException()
        {
        }

        public XlsxAccessException(string message) : base(message)
        {
        }

        public XlsxAccessException(string message, Exception inner) : base(message, inner)
        {
        }

        protected XlsxAccessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}