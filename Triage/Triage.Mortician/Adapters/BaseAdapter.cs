using System;

namespace Triage.Mortician.Adapters
{
    internal class BaseAdapter
    {
        internal IConverter Converter { get; set; }

        /// <inheritdoc />
        public BaseAdapter(IConverter converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }
    }
}