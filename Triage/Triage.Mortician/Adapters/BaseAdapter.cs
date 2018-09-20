using System;

namespace Triage.Mortician.Adapters
{
    internal abstract class BaseAdapter
    {
        /// <inheritdoc />
        protected BaseAdapter(IConverter converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public abstract void Setup();
        internal IConverter Converter { get; set; }
    }
}