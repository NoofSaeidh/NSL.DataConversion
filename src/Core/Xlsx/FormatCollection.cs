using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    public abstract class FormatCollection : IFormatCollection
    {
        private readonly Dictionary<int, string> _customFormats;
        private readonly Dictionary<int, string> _defaultFormats;

        protected FormatCollection()
        {
            _defaultFormats = new Dictionary<int, string>();
            _customFormats = new Dictionary<int, string>();
        }

        protected FormatCollection(IEnumerable<KeyValuePair<int, string>> customFormats)
        {
            _defaultFormats = new Dictionary<int, string>();
            _customFormats = new Dictionary<int, string>(customFormats);
        }


        public int[] DefinedIds
        {
            get
            {
                //todo: review this
                return _defaultFormats.Keys.Union(_customFormats.Keys).ToArray();
            }
        }

        public ICollection<KeyValuePair<int, string>> GetAllFormats()
        {
            return _defaultFormats.Union(_customFormats).ToArray();
        }

        public ICollection<KeyValuePair<int, string>> GetCustomFormats()
        {
            return _customFormats.ToArray();
        }

        public ICollection<KeyValuePair<int, string>> GetDefaultFormats()
        {
            return _defaultFormats.ToArray();
        }

        public string GetFormat(int id)
        {
            if (_defaultFormats.ContainsKey(id))
                return _defaultFormats[id];
            if (_customFormats.ContainsKey(id))
                return _customFormats[id];

            throw new KeyNotFoundException();
        }

        public bool IsCustom(int id)
        {
            return _customFormats.ContainsKey(id);
        }

        public bool IsDefault(int id)
        {
            return _defaultFormats.ContainsKey(id);
        }

        public bool IsDefined(int id)
        {
            return IsDefault(id) || IsCustom(id);
        }

        public bool IsDefined(string format, out int id)
        {
            foreach (var item in GetAllFormats())
            {
                if (item.Value == format)
                {
                    id = item.Key;
                    return true;
                }
            }
            id = 0;
            return false;
        }

        public bool TryGetFormat(int id, out string format)
        {
            if (IsDefined(id))
            {
                format = GetFormat(id);
                return true;
            }
            format = null;
            return false;
        }

        public static FormatCollection GetDefaultCollection()
        {
            return new DefaultCollection();
        }

        private class DefaultCollection : FormatCollection
        {

        }
    }
}
