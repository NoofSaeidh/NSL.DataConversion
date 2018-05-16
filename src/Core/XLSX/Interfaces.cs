// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using DocumentFormat.OpenXml.Packaging;
using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    public interface IXlsxCell : ICell
    {
        string Format { get; }
        XlsxCellType CellType { get; }
        object OriginalValue { get; }
    }

    //public interface IXlsxCellResolver : ICellResolver
    //    , IObjectResolver<IXlsxCell>
    //    , IResolver<object, IXlsxCell>
    //    , IResolver<object[,], IXlsxCell[,]>
    //    , IResolver<IEnumerable<IEnumerable<object>>, IEnumerable<IEnumerable<IXlsxCell>>>
    //    , IResolver<ICellWrapper, IXlsxCell>
    //    , IResolver<ICellWrapper[,], IXlsxCell[,]>
    //{
    //    new IXlsxCell[,] ResolveToArray(IEnumerable<IEnumerable<object>> value);

    //    new IXlsxCell[,] ResolveToArray(object[,] value);

    //    new IList<IList<IXlsxCell>> ResolveToList(IEnumerable<IEnumerable<object>> value);

    //    new IList<IList<IXlsxCell>> ResolveToList(object[,] value);
    //}

    public interface IXlsxFileResolver
    {
        IData Read(string path);

        bool TryRead(string path, out IData result);

        void Save(string path, IData content);
    }

    public interface IFormatCollection
    {
        int[] DefinedIds { get; }
        bool TryGetFormat(int id, out string format);
        string GetFormat(int id);
        bool IsDefined(int id);
        bool IsDefined(string format, out int id);
        bool IsCustom(int id);
        bool IsDefault(int id);


        ICollection<KeyValuePair<int, string>> GetAllFormats();
        ICollection<KeyValuePair<int, string>> GetCustomFormats();
        ICollection<KeyValuePair<int, string>> GetDefaultFormats();
    }
}