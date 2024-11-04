using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ObservableModel
{
    /// <summary>
    /// 可观察列表
    /// </summary>
    public class ObservableList<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        private bool _disableNotifyTemporary;

        public ObservableList()
        {
            base.CollectionChanged += ListenBaseCollectionChanged;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_disableNotifyTemporary)
            {
                base.OnCollectionChanged(e);
            }
        }

        #region 可触发通知的AddRange/ClearAll接口
        /// <summary>
        /// 添加元素列表
        /// </summary>
        public void AddRange(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            // 临时屏蔽通知
            _disableNotifyTemporary = true;
            foreach(T item in list)
            {
                Add(item);
                item.PropertyChanged += ItemChanged;
            }
            // 所有元素添加后统一通知
            _disableNotifyTemporary = false;
            OnCollectionChanged(ChangedEventArgsCache.CollectionReset);
        }

        /// <summary>
        /// 清理集合
        /// </summary>
        public void ClearAll()
        {
            _disableNotifyTemporary = true;
            foreach(T item in Items)
            {
                item.PropertyChanged -= ItemChanged;
            }
            ClearItems();
            _disableNotifyTemporary = false;
            OnCollectionChanged(ChangedEventArgsCache.CollectionReset);
        }
        #endregion

        #region 子元素数据变化时的通知
        private void ListenBaseCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach(T item in e.OldItems)
                {
                    item.PropertyChanged -= ItemChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach(T item in e.NewItems)
                {
                    item.PropertyChanged += ItemChanged;
                }
            }
        }

        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCollectionChanged(ChangedEventArgsCache.CollectionReset);
        }
        #endregion

    }
}
