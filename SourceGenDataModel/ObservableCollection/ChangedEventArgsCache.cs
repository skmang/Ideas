using System.Collections.Specialized;
using System.ComponentModel;

namespace ObservableModel
{
    internal static class ChangedEventArgsCache
    {
        internal static readonly NotifyCollectionChangedEventArgs CollectionReset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        internal static readonly PropertyChangedEventArgs DictionaryCount = new PropertyChangedEventArgs("Count");
        internal static readonly PropertyChangedEventArgs DictionaryItems = new PropertyChangedEventArgs("Item[]");
        internal static readonly PropertyChangedEventArgs DictionaryKeys = new PropertyChangedEventArgs("Keys");
        internal static readonly PropertyChangedEventArgs DictionaryValues = new PropertyChangedEventArgs("Values");
    }
}
