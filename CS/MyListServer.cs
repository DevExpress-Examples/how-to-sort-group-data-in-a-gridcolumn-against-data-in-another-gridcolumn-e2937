﻿using System;
using DevExpress.Data;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace DXGrid_EF4_ServerMode {
    public class MyListServer : IListServer {
        IListServer list;
        IDictionary<string, string> summaryReplacements;
        public MyListServer(IListServer list, IDictionary<string, string> summaryReplacements) {
            this.list = list;
            this.summaryReplacements = summaryReplacements;
        }

        void list_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e) {

        }

        #region IListServer Members

        void IListServer.Apply(DevExpress.Data.Filtering.CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo) {
            List<ServerModeOrderDescriptor> newSortInfo = new List<ServerModeOrderDescriptor>();
            if(sortInfo != null) {
                foreach(ServerModeOrderDescriptor item in sortInfo) {
                    ServerModeOrderDescriptor newItem = item;
                    if(item.SortExpression is OperandProperty && summaryReplacements.ContainsKey(((OperandProperty)item.SortExpression).PropertyName)) {
                        newItem = new ServerModeOrderDescriptor(new OperandProperty(summaryReplacements[((OperandProperty)item.SortExpression).PropertyName]), item.IsDesc);
                    }
                    newSortInfo.Add(newItem);
                }
            }
            list.Apply(filterCriteria, newSortInfo, groupCount, groupSummaryInfo, totalSummaryInfo);
        }

        event EventHandler<ServerModeExceptionThrownEventArgs> IListServer.ExceptionThrown {
            add { list.ExceptionThrown += value; }
            remove { list.ExceptionThrown -= value; }
        }

        int IListServer.FindIncremental(DevExpress.Data.Filtering.CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop) {
            return list.FindIncremental(expression, value, startIndex, searchUp, ignoreStartRow, allowLoop);
        }

        System.Collections.IList IListServer.GetAllFilteredAndSortedRows() {
            return list.GetAllFilteredAndSortedRows();
        }

        List<ListSourceGroupInfo> IListServer.GetGroupInfo(ListSourceGroupInfo parentGroup) {
            return list.GetGroupInfo(parentGroup);
        }

        int IListServer.GetRowIndexByKey(object key) {
            return list.GetRowIndexByKey(key);
        }

        object IListServer.GetRowKey(int index) {
            return list.GetRowKey(index);
        }

        List<object> IListServer.GetTotalSummary() {
            return list.GetTotalSummary();
        }

        public bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, System.Threading.CancellationToken cancellationToken) {
            return list.PrefetchRows(groupsToPrefetch, cancellationToken);
        }        

        object[] IListServer.GetUniqueColumnValues(DevExpress.Data.Filtering.CriteriaOperator expression, int maxCount, CriteriaOperator filterExpression, bool includeFilteredOut) {
            return list.GetUniqueColumnValues(expression, maxCount, filterExpression, includeFilteredOut);
        }

        event EventHandler<ServerModeInconsistencyDetectedEventArgs> IListServer.InconsistencyDetected {
            add { list.InconsistencyDetected += value; }
            remove { list.InconsistencyDetected -= value; }
        }

        int IListServer.LocateByValue(DevExpress.Data.Filtering.CriteriaOperator expression, object value, int startIndex, bool searchUp) {
            return list.LocateByValue(expression, value, startIndex, searchUp);
        }

        int DevExpress.Data.IListServer.LocateByExpression(DevExpress.Data.Filtering.CriteriaOperator expression, int startIndex, bool searchUp) {
            throw new NotImplementedException();
        }

        void IListServer.Refresh() {
            list.Refresh();
        }

        #endregion

        #region IList Members

        int System.Collections.IList.Add(object value) {
            return list.Add(value);
        }

        void System.Collections.IList.Clear() {
            list.Clear();
        }

        bool System.Collections.IList.Contains(object value) {
            return list.Contains(value);
        }

        int System.Collections.IList.IndexOf(object value) {
            return list.IndexOf(value);
        }

        void System.Collections.IList.Insert(int index, object value) {
            list.Insert(index, value);
        }

        bool System.Collections.IList.IsFixedSize {
            get { return list.IsFixedSize; }
        }

        bool System.Collections.IList.IsReadOnly {
            get { return list.IsReadOnly; }
        }

        void System.Collections.IList.Remove(object value) {
            list.Remove(value);
        }

        void System.Collections.IList.RemoveAt(int index) {
            list.RemoveAt(index);
        }

        object System.Collections.IList.this[int index] {
            get {
                return list[index];
            }
            set {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection Members

        void System.Collections.ICollection.CopyTo(Array array, int index) {
            list.CopyTo(array, index);
        }

        int System.Collections.ICollection.Count {
            get { return list.Count; }
        }

        bool System.Collections.ICollection.IsSynchronized {
            get { return list.IsSynchronized; }
        }

        object System.Collections.ICollection.SyncRoot {
            get { return list.SyncRoot; }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return list.GetEnumerator();
        }

        #endregion
    }
    public class MyListSource : IListSource {
        IListSource source;
        IDictionary<string, string> summaryReplacements;
        public MyListSource(IListSource source, IDictionary<string, string> summaryReplacements) {
            this.source = source;
            this.summaryReplacements = summaryReplacements;
        }

        #region IListSource Members

        bool IListSource.ContainsListCollection {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList() {
            return new MyListServer((IListServer)source.GetList(), summaryReplacements);
        }

        #endregion
    }
}