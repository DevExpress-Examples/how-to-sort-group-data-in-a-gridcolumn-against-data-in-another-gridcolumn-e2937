Imports System
Imports DevExpress.Data
Imports System.Collections.Generic
Imports DevExpress.Data.Filtering
Imports System.ComponentModel

Namespace DXGrid_EF4_ServerMode
    Public Class MyListServer
        Implements IListServer

        Private list As IListServer
        Private summaryReplacements As IDictionary(Of String, String)
        Public Sub New(ByVal list As IListServer, ByVal summaryReplacements As IDictionary(Of String, String))
            Me.list = list
            Me.summaryReplacements = summaryReplacements
        End Sub

        Private Sub list_ExceptionThrown(ByVal sender As Object, ByVal e As ServerModeExceptionThrownEventArgs)

        End Sub

        #Region "IListServer Members"

        Private Sub IListServer_Apply(ByVal filterCriteria As DevExpress.Data.Filtering.CriteriaOperator, ByVal sortInfo As ICollection(Of ServerModeOrderDescriptor), ByVal groupCount As Integer, ByVal groupSummaryInfo As ICollection(Of ServerModeSummaryDescriptor), ByVal totalSummaryInfo As ICollection(Of ServerModeSummaryDescriptor)) Implements IListServer.Apply
            Dim newSortInfo As New List(Of ServerModeOrderDescriptor)()
            If sortInfo IsNot Nothing Then
                For Each item As ServerModeOrderDescriptor In sortInfo
                    Dim newItem As ServerModeOrderDescriptor = item
                    If TypeOf item.SortExpression Is OperandProperty AndAlso summaryReplacements.ContainsKey(CType(item.SortExpression, OperandProperty).PropertyName) Then
                        newItem = New ServerModeOrderDescriptor(New OperandProperty(summaryReplacements(CType(item.SortExpression, OperandProperty).PropertyName)), item.IsDesc)
                    End If
                    newSortInfo.Add(newItem)
                Next item
            End If
            list.Apply(filterCriteria, newSortInfo, groupCount, groupSummaryInfo, totalSummaryInfo)
        End Sub

        Private Custom Event ExceptionThrown As EventHandler(Of ServerModeExceptionThrownEventArgs) Implements IListServer.ExceptionThrown
            AddHandler(ByVal value As EventHandler(Of ServerModeExceptionThrownEventArgs))
                AddHandler list.ExceptionThrown, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler(Of ServerModeExceptionThrownEventArgs))
                RemoveHandler list.ExceptionThrown, value
            End RemoveHandler
            RaiseEvent()

            End RaiseEvent
            'RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs(Of ServerModeExceptionThrownEventArgs))
            'End RaiseEvent
        End Event

        Private Function IListServer_FindIncremental(ByVal expression As DevExpress.Data.Filtering.CriteriaOperator, ByVal value As String, ByVal startIndex As Integer, ByVal searchUp As Boolean, ByVal ignoreStartRow As Boolean, ByVal allowLoop As Boolean) As Integer Implements IListServer.FindIncremental
            Return list.FindIncremental(expression, value, startIndex, searchUp, ignoreStartRow, allowLoop)
        End Function

        Private Function IListServer_GetAllFilteredAndSortedRows() As System.Collections.IList Implements IListServer.GetAllFilteredAndSortedRows
            Return list.GetAllFilteredAndSortedRows()
        End Function

        Private Function IListServer_GetGroupInfo(ByVal parentGroup As ListSourceGroupInfo) As List(Of ListSourceGroupInfo) Implements IListServer.GetGroupInfo
            Return list.GetGroupInfo(parentGroup)
        End Function

        Private Function IListServer_GetRowIndexByKey(ByVal key As Object) As Integer Implements IListServer.GetRowIndexByKey
            Return list.GetRowIndexByKey(key)
        End Function

        Private Function IListServer_GetRowKey(ByVal index As Integer) As Object Implements IListServer.GetRowKey
            Return list.GetRowKey(index)
        End Function

        Private Function IListServer_GetTotalSummary() As List(Of Object) Implements IListServer.GetTotalSummary
            Return list.GetTotalSummary()
        End Function

        Public Function PrefetchRows(ByVal groupsToPrefetch() As ListSourceGroupInfo, ByVal cancellationToken As System.Threading.CancellationToken) As Boolean Implements IListServer.PrefetchRows
            Return list.PrefetchRows(groupsToPrefetch, cancellationToken)
        End Function

        Private Function IListServer_GetUniqueColumnValues(ByVal expression As DevExpress.Data.Filtering.CriteriaOperator, ByVal maxCount As Integer, ByVal includeFilteredOut As Boolean) As Object() Implements IListServer.GetUniqueColumnValues
            Return list.GetUniqueColumnValues(expression, maxCount, includeFilteredOut)
        End Function

        Private Custom Event InconsistencyDetected As EventHandler(Of ServerModeInconsistencyDetectedEventArgs) Implements IListServer.InconsistencyDetected
            AddHandler(ByVal value As EventHandler(Of ServerModeInconsistencyDetectedEventArgs))
                AddHandler list.InconsistencyDetected, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler(Of ServerModeInconsistencyDetectedEventArgs))
                RemoveHandler list.InconsistencyDetected, value
            End RemoveHandler
            RaiseEvent()
            End RaiseEvent
        End Event

        Private Function IListServer_LocateByValue(ByVal expression As DevExpress.Data.Filtering.CriteriaOperator, ByVal value As Object, ByVal startIndex As Integer, ByVal searchUp As Boolean) As Integer Implements IListServer.LocateByValue
            Return list.LocateByValue(expression, value, startIndex, searchUp)
        End Function

        Private Function IListServer_LocateByExpression(ByVal expression As DevExpress.Data.Filtering.CriteriaOperator, ByVal startIndex As Integer, ByVal searchUp As Boolean) As Integer Implements DevExpress.Data.IListServer.LocateByExpression
            Throw New NotImplementedException()
        End Function

        Private Sub IListServer_Refresh() Implements IListServer.Refresh
            list.Refresh()
        End Sub

        #End Region

        #Region "IList Members"

        Private Function IList_Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
            Return list.Add(value)
        End Function

        Private Sub IList_Clear() Implements System.Collections.IList.Clear
            list.Clear()
        End Sub

        Private Function IList_Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
            Return list.Contains(value)
        End Function

        Private Function IList_IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
            Return list.IndexOf(value)
        End Function

        Private Sub IList_Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
            list.Insert(index, value)
        End Sub

        Private ReadOnly Property IList_IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
            Get
                Return list.IsFixedSize
            End Get
        End Property

        Private ReadOnly Property IList_IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
            Get
                Return list.IsReadOnly
            End Get
        End Property

        Private Sub IList_Remove(ByVal value As Object) Implements System.Collections.IList.Remove
            list.Remove(value)
        End Sub

        Private Sub IList_RemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
            list.RemoveAt(index)
        End Sub

        Public Property IList_Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
            Get
                Return list(index)
            End Get
            Set(ByVal value As Object)
                list(index) = value
            End Set
        End Property

        #End Region

        #Region "ICollection Members"

        Private Sub ICollection_CopyTo(ByVal array As Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
            list.CopyTo(array, index)
        End Sub

        Private ReadOnly Property ICollection_Count() As Integer Implements System.Collections.ICollection.Count
            Get
                Return list.Count
            End Get
        End Property

        Private ReadOnly Property ICollection_IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return list.IsSynchronized
            End Get
        End Property

        Private ReadOnly Property ICollection_SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return list.SyncRoot
            End Get
        End Property

        #End Region

        #Region "IEnumerable Members"

        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return list.GetEnumerator()
        End Function

        #End Region
    End Class
    Public Class MyListSource
        Implements IListSource

        Private source As IListSource
        Private summaryReplacements As IDictionary(Of String, String)
        Public Sub New(ByVal source As IListSource, ByVal summaryReplacements As IDictionary(Of String, String))
            Me.source = source
            Me.summaryReplacements = summaryReplacements
        End Sub

        #Region "IListSource Members"

        Private ReadOnly Property IListSource_ContainsListCollection() As Boolean Implements IListSource.ContainsListCollection
            Get
                Return False
            End Get
        End Property

        Private Function IListSource_GetList() As System.Collections.IList Implements IListSource.GetList
            Return New MyListServer(DirectCast(source.GetList(), IListServer), summaryReplacements)
        End Function

        #End Region
    End Class
End Namespace