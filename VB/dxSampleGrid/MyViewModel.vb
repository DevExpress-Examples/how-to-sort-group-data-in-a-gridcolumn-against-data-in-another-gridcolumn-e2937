Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Input

Namespace dxSampleGrid
    Public Class MyViewModel
        Public Sub New()
            CreateList()
        End Sub
        Public Property ListPerson() As ObservableCollection(Of Person)
        Private Sub CreateList()
            ListPerson = New ObservableCollection(Of Person)()
            For i As Integer = 0 To 3
                Dim p As New Person(i)
                ListPerson.Add(p)
            Next i
            ListPerson(0).CustomSortNumber = 2
            ListPerson(1).CustomSortNumber = 1
            ListPerson(3).CustomSortNumber = 1
            ListPerson(2).CustomSortNumber = 3
        End Sub
    End Class



End Namespace
