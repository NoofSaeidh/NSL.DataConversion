using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Helpers;
using NSL.DataConversion.Core.Tests.Mocks;
using NSL.DataConversion.Core.Xlsx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Integration.Xlsx
{
    [Trait("TestType", "Integration")]
    public abstract class IXlsxFileResolverTests<T> : IDisposable
        where T : IXlsxFileResolver
    {
        private IData _TestXlsxSimpleData;

        protected readonly List<string> FilesToDelete = new List<string>();

        protected abstract T GetInstance();

        protected const string TestXlsxSimpleFile = "..\\..\\..\\..\\..\\data\\xlsx\\simple.xlsx";

        protected const string Save_TempFileName = "save_temp_file.xlsx";

        protected IData TestXlsxSimpleData
        {
            get
            {
                if (_TestXlsxSimpleData != null) return _TestXlsxSimpleData;

                ITable table;
                var resolver = new MockTableResolver();
                var result = new Data();


                //todo: mock cells for xlsx

                table = resolver.ResolveTable(new object[,]
                {
                    {"Header", "Date", "Odate" },
                    {"foobar", new DateTime(2017, 01, 17), new DateTime(2484, 04, 18) }
                });

                result.Add("Sheet1", table);

                table = resolver.ResolveTable(new object[,]
                {
                    {"Header_S2", "Date_S2", "Odate_S2" },
                    {"foobar", new DateTime(2017, 01, 17), new DateTime(2484, 04, 18) }
                });

                result.Add("Sheet2", table);

                table = resolver.ResolveTable(new object[,] { });

                result.Add("Sheet3", table);

                return _TestXlsxSimpleData = result;
            }
        }

        [Fact]
        public void Read_Works_ForSimpleFile()
        {
            // Arrange
            var resolver = GetInstance();
            // Act
            var result = resolver.Read(TestXlsxSimpleFile);
            // Assert
            DataAssert.AssertIDataEqual(TestXlsxSimpleData, result);
        }

        [Fact]
        public void Save_SavesTheSameFileAsRead_ForSimpleFile()
        {
            // Arrange
            var resolver = GetInstance();
            //todo: maybe should be different folder
            var path = Path.Combine(Directory.GetCurrentDirectory(), Save_TempFileName);
            if (File.Exists(path))
                File.Delete(path);
            FilesToDelete.Add(path);
            // Act
            resolver.Save(path, TestXlsxSimpleData);
            // Assert
            DataAssert.AssertIDataEqual(TestXlsxSimpleData, resolver.Read(path));
        }

        void IDisposable.Dispose()
        {
            foreach (var file in FilesToDelete)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch { }
            }
        }
    }
}
