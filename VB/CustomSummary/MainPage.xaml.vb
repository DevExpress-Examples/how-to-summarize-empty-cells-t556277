Imports DevExpress.UI.Xaml.Grid
Imports DevExpress.Data

Public NotInheritable Class MainPage
    Inherits Page
    Private list As List(Of TestData)
    Private selectedValues As New Dictionary(Of Integer, Boolean)()

    Public Sub New()
        Me.InitializeComponent()
        list = New List(Of TestData)()
        For i As Integer = 0 To 99
            list.Add(New TestData() With {.Text = "item " & i, .Number = i})
            If i Mod 3 = 0 Then
                list(i).Number = Nothing
            End If
        Next i
        grid.ItemsSource = list
    End Sub

    Private emptyCellsTotalCount As Integer = 0



    Public Class TestData
        Private privateText As String
        Public Property Text() As String
            Get
                Return privateText
            End Get
            Set(ByVal value As String)
                privateText = value
            End Set
        End Property
        Private privateNumber As Nullable(Of Integer)
        Public Property Number() As Nullable(Of Integer)
            Get
                Return privateNumber
            End Get
            Set(ByVal value As Nullable(Of Integer))
                privateNumber = value
            End Set
        End Property
    End Class

    Private Sub grid_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
        If (CType(e.Item, GridSummaryItem)).FieldName <> "Number" Then
            Return
        End If
        If e.IsTotalSummary Then
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                emptyCellsTotalCount = 0
            End If
            If e.SummaryProcess = CustomSummaryProcess.Calculate Then
                Dim val As Nullable(Of Integer) = CType(e.FieldValue, Nullable(Of Integer))
                If (Not val.HasValue) Then
                    emptyCellsTotalCount += 1
                End If
                e.TotalValue = emptyCellsTotalCount
            End If
        End If

    End Sub
End Class


