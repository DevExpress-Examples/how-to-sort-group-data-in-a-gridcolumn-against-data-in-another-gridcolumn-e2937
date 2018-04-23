Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports DevExpress.Data.Linq
Imports System.Collections.Generic
Imports DevExpress.Data
Imports System.ComponentModel

Namespace DXGrid_EF4_ServerMode
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			'You can use this demo code to generate a large data set, if currently, you don't have a large database for testing.
			Dim dataContext As New NorthwindEntities()

			Dim source As New LinqServerModeSource() With {.ElementType = GetType(Product), .KeyExpression = "ProductID", .QueryableSource = dataContext.Products}

			grid.ItemsSource = source
			grid.Columns.Remove(grid.Columns("SupplierID"))
			grid.Columns("ProductName").SortIndex = 0
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.AutoGenerateColumns = DevExpress.Xpf.Grid.AutoGenerateColumnsMode.None
			Dim replacements As New Dictionary(Of String, String)()
			replacements.Add("ProductName", "SupplierID")
			grid.ItemsSource = New MyListSource(TryCast(grid.ItemsSource, IListSource), replacements)
		End Sub
	End Class
End Namespace