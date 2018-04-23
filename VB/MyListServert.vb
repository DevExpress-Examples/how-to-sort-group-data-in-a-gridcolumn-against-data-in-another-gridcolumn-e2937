Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Data
Imports System.Collections.Generic
Imports DevExpress.Data.Filtering
Imports System.ComponentModel

Namespace DXGrid_EF4_ServerMode
	Public Class MyListServert
		Implements IListServer
		Private list As IListServer
		Private summaryReplacements As IDictionary(Of String, String)
		Public Sub New(ByVal list As IListServer, ByVal summaryReplacements As IDictionary(Of String, String))
			Me.list = list
			Me.summaryReplacements = summaryReplacements
		End Sub

        'Private Sub list_ExceptionThrown(ByVal sender As Object, ByVal e As ServerModeExceptionThrownEventArgs)

        'End Sub

		#Region "IListServer Members"


        Public Event ExceptionThrown As EventHandler(Of ServerModeExceptionThrownEventArgs) Implements IListServer.ExceptionThrown
        Public Event InconsistencyDetected As EventHandler(Of ServerModeInconsistencyDetectedEventArgs) Implements IListServer.InconsistencyDetected

		Private Sub Apply(ByVal filterCriteria As DevExpress.Data.Filtering.CriteriaOperator, ByVal sortInfo As ICollection(Of ServerModeOrderDescriptor), ByVal groupCount As Integer, ByVal groupSummaryInfo As ICollection(Of ServerModeSummaryDescriptor), ByVal totalSummaryInfo As ICollection(Of ServerModeSummaryDescriptor)) Implements IListServer.Apply
			Dim newSortInfo As New List(Of ServerModeOrderDescriptor)()
			If sortInfo IsNot Nothing Then
				For Each item As ServerModeOrderDescriptor In sortInfo
					Dim newItem As ServerModeOrderDescriptor = item
					If TypeOf item.SortExpression Is OperandProperty AndAlso summaryReplacements.ContainsKey((CType(item.SortExpression, OperandProperty)).PropertyName) Then
						newItem = New ServerModeOrderDescriptor(New OperandProperty(summaryReplacements((CType(item.SortExpression, OperandProperty)).PropertyName)), item.IsDesc)
					End If
					newSortInfo.Add(newItem)
				Next item
			End If
			list.Apply(filterCriteria, newSortInfo, groupCount, groupSummaryInfo, totalSummaryInfo)
		End Sub

        Private Function FindIncremental(ByVal expression As DevExpress.Data.Filtering.CriteriaOperator, ByVal value As String, ByVal startIndex As Integer, ByVal searchUp As Boolean, ByVal ignoreStartRow As Boolean, ByVal allowLoop As Boolean) As Integer Implements IListServer.FindIncremental
            Return list.FindIncremental(expression, value, startIndex, searchUp, ignoreStartRow, allowLoop)
        End Function

        Private Function GetAllFilteredAndSortedRows() As System.Collections.IList Implements IListServer.GetAllFilteredAndSortedRows
            Return list.GetAllFilteredAndSortedRows()
        End Function

        Private Function GetGroupInfo(ByVal parentGroup As ListSourceGroupInfo) As List(Of ListSourceGroupInfo) Implements IListServer.GetGroupInfo
            Return list.GetGroupInfo(parentGroup)
        End Function

        Private Function GetRowIndexByKey(ByVal key As Object) As Integer Implements IListServer.GetRowIndexByKey
            Return list.GetRowIndexByKey(key)
        End Function

        Private Function GetRowKey(ByVal index As Integer) As Object Implements IListServer.GetRowKey
            Return list.GetRowKey(index)
        End Function

        Private Function GetTotalSummary() As Dictionary(Of Object, Object) Implements IListServer.GetTotalSummary
            Return list.GetTotalSummary()
        End Function

        Private Function GetUniqueColumnValues(ByVal expression As DevExpress.Data.Filtering.CriteriaOperator, ByVal maxCount As Integer, ByVal includeFilteredOut As Boolean) As Object() Implements IListServer.GetUniqueColumnValues
            Return list.GetUniqueColumnValues(expression, maxCount, includeFilteredOut)
        End Function


        Private Function LocateByValue(ByVal expression As DevExpress.Data.Filtering.CriteriaOperator, ByVal value As Object, ByVal startIndex As Integer, ByVal searchUp As Boolean) As Integer Implements IListServer.LocateByValue
            Return list.LocateByValue(expression, value, startIndex, searchUp)
        End Function

        Private Sub Refresh() Implements IListServer.Refresh
            list.Refresh()
        End Sub

#End Region

#Region "IList Members"

        Private Function Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
            Return list.Add(value)
        End Function

        Private Sub Clear() Implements System.Collections.IList.Clear
            list.Clear()
        End Sub

        Private Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
            Return list.Contains(value)
        End Function

        Private Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
            Return list.IndexOf(value)
        End Function

        Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
            list.Insert(index, value)
        End Sub

        Private ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
            Get
                Return list.IsFixedSize
            End Get
        End Property

        Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
            Get
                Return list.IsReadOnly
            End Get
        End Property

        Private Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
            list.Remove(value)
        End Sub

        Private Sub RemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
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

        Private Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
            list.CopyTo(array, index)
        End Sub

        Private ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
            Get
                Return list.Count
            End Get
        End Property

        Private ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return list.IsSynchronized
            End Get
        End Property

        Private ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return list.SyncRoot
            End Get
        End Property

#End Region

#Region "IEnumerable Members"

        Private Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
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

		Private ReadOnly Property ContainsListCollection() As Boolean Implements IListSource.ContainsListCollection
			Get
				Return False
			End Get
		End Property

		Private Function GetList() As System.Collections.IList Implements IListSource.GetList
			Return New MyListServert(CType(source.GetList(), IListServer), summaryReplacements)
		End Function

		#End Region
	End Class
End Namespace
