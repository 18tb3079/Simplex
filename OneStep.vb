Public Class OneStep
    Inherits Tableau

    Public Sub New(mymenu As IMenu) 'This subprogram creates the simplex tableau
        MyBase.New(mymenu)
    End Sub

    Public Sub New(simplextableau As Double(,), MyTopRow As List(Of String))
        MyBase.New(simplextableau, MyTopRow)
    End Sub

    Private Sub CompletedTable() 'This subprogram interprets the table to output the values for the variables
        Console.SetCursorPosition(0, Console.CursorTop - 1)
        Console.WriteLine("Tableau is complete! Values for all the variables are shown below: ")
        Dim basicvariabletest As Double
        Dim isbasic As Boolean
        Dim basicrow As Integer
        TopRow.Remove("Value")
        For Each variable In TopRow
            basicvariabletest = 0
            isbasic = True
            basicrow = 0
            For y = 0 To TableHeight
                If Tableau(TopRow.IndexOf(variable), y) <> 0 Then
                    If basicvariabletest = 0 Then
                        basicvariabletest = Tableau(TopRow.IndexOf(variable), y)
                        basicrow = y
                    Else
                        isbasic = False
                        Exit For
                    End If
                End If
            Next
            If isbasic Then
                Console.Write(variable & " = " & Tableau(TableLength, basicrow) / Tableau(TopRow.IndexOf(variable), basicrow) & " , ")
            Else
                Console.Write(variable & " = 0 , ")
            End If
        Next
        Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop)
        Console.Write("  ")
    End Sub

    Public Overrides Sub Simplex() 'This subprogram executes the simplex algorithm
        Dim PivotColumn As Integer
        Dim TopOfPivotColumn As Integer
        Dim PivotRow As Integer
        Dim RatioTest As Integer
        'Check for negative values and pivot column
        Do
            Display()
            PivotColumn = -1
            TopOfPivotColumn = 0
            For x = 1 To TableLength
                If Tableau(x, 0) < TopOfPivotColumn Then
                    TopOfPivotColumn = Tableau(x, 0)
                    PivotColumn = x
                End If
            Next
            If PivotColumn = -1 Then
                CompletedTable()
                Return
            End If

            'Ratio Test
            PivotRow = -1
            RatioTest = Integer.MaxValue
            For y = 1 To TableHeight
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

            If PivotRow = -1 Then
                Throw New Exception("We should go onto the next variable now but the program doesnt do this yet")
            End If

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
