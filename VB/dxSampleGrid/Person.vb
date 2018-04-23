Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading.Tasks

Namespace dxSampleGrid

    Partial Public Class Person
        Public Sub New()

        End Sub
        Public Sub New(ByVal i As Integer)
            FirstName = "FirstName" & i
            LastName = "LastName" & i
            Age = i * 10
        End Sub

        Private _firstName As String
        Public Property LastName() As String
        Private _age As Integer

        Public Property FirstName() As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value

            End Set
        End Property

        Public Property Age() As Integer
            Get
                Return _age
            End Get
            Set(ByVal value As Integer)
                _age = value
            End Set
        End Property
        Public Property CustomSortNumber() As Integer
    End Class


End Namespace
