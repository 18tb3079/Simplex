Public Class TwoStep
    Inherits Tableau

    'x + 2y
    'x >= 1
    ' x + y <= 2

    Public Sub New(mymenu As IMenu) 'This subprogram creates the simplex tableau
        MyBase.New(mymenu)
    End Sub
    Public Overrides Sub Simplex()
        Dim PivotColumn As Integer
        Dim BottomOfPivotColumn As Integer
        Dim PivotRow As Integer
        Dim RatioTest As Integer
        Do
            'Find the biggest value in the second objective function (if it is zero then end)
            Display()
            PivotColumn = -1
            BottomOfPivotColumn = 0
            For x = 2 To TableLength
                If Tableau(x, TableHeight) > BottomOfPivotColumn Then
                    BottomOfPivotColumn = Tableau(x, TableHeight)
                    PivotColumn = x
                End If
            Next


            If PivotColumn = -1 Then 'Reducing the tableau to a onestep tableau
                Dim xReduced As Integer
                Dim ReducedTableau(TableLength - 1 - artificials, TableHeight - 1) As Double
                Dim ReducedTopRow As New List(Of String)
                For y = 0 To TableHeight - 1
                    xReduced = 0
                    For x = 1 To TableLength
                        If TopRow(x) <> "Q" And Mid(TopRow(x), 1, 1) <> "a" Then
                            If y = 0 Then
                                ReducedTopRow.Add(TopRow(x))
                            End If
                            ReducedTableau(xReduced, y) = Tableau(x, y)
                            xReduced += 1
                        End If
                    Next
                Next

                Dim MyOneStepTableau As New OneStep(ReducedTableau, ReducedTopRow)
                MyOneStepTableau.Simplex()
                Return
            End If
            'Ratio Test
            RatioTest = Integer.MaxValue
            For y = 1 To TableHeight - 1
                Try
                    If Tableau(TableLength, y) / Tableau(PivotColumn, y) < RatioTest And Tableau(TableLength, y) / Tableau(PivotColumn, y) >= 0 Then
                        If Tableau(PivotColumn, y) > 0 Or Tableau(TableLength, y) <> 0 Then
                            PivotRow = y
                            RatioTest = Tableau(TableLength, y) / Tableau(PivotColumn, y)
                        End If
                    End If
                Catch
                End Try
            Next

            'Making Pivot Value 1
            For x = 1 To TableLength
                If x <> PivotColumn Then
                    Tableau(x, PivotRow) /= Tableau(PivotColumn, PivotRow)
                End If
            Next
            Tableau(PivotColumn, PivotRow) = 1

            'Making all the other values in the pivot column 0
            For y = 0 To TableHeight
                If y <> PivotRow Then
                    For x = 1 To TableLength
                        If x <> PivotColumn Then
                            Tableau(x, y) = Tableau(x, y) - Tableau(PivotColumn, y) * Tableau(x, PivotRow)
                        End If
                    Next
                    Tableau(PivotColumn, y) = 0
                End If
            Next
        Loop
    End Sub
End Class
