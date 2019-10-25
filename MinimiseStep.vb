Public Class MinimiseStep
    Inherits Tableau
    Public Sub New(mymenu As IMenu) 'This subprogram creates the simplex tableau
        MyBase.New(True)
        'This class is different to the other two as it requires an intermidiate step before creating the simplex tableau
        Dim inputtableau As String(,) = mymenu.GetConstraints
        Dim MatrixLength As Integer = inputtableau.GetLength(0) - 2
        Dim MatrixHeight As Integer = inputtableau.GetLength(1) - 2
        Dim Equalto As Integer = 0
        For y = 0 To inputtableau.GetLength(1) - 2
            If inputtableau(MatrixLength + 1, y) = "E" Then 'Each equal to constraint requires two lines
                Equalto += 1
            End If
        Next
        Dim Matrix(MatrixLength, MatrixHeight + Equalto) As Double
        For y = 0 To MatrixHeight
            For x = 0 To MatrixLength
                If y = 0 Then
                    Matrix(x, MatrixHeight) = inputtableau(x, y)
                Else
                    If inputtableau(MatrixLength + 1, y) = "L" Then
                        Matrix(x, y - 1) = -1 * inputtableau(x, y)
                    ElseIf inputtableau(MatrixLength + 1, y) = "E" Then
                        Matrix(x, y - 1) = -1 * inputtableau(x, y)
                        Matrix(x, MatrixHeight - Equalto) = inputtableau(x, y)
                        If x = MatrixLength Then Equalto -= 1
                    Else
                        Matrix(x, y - 1) = inputtableau(x, y)
                    End If
                End If
            Next
        Next

        Dim TMatrixLength As Integer = Matrix.GetLength(1) - 1
        Dim TMatrix(TMatrixLength, MatrixLength) As Double 'Transpose
        For y = 0 To MatrixLength
            For x = 0 To TMatrixLength
                TMatrix(x, y) = Matrix(y, x)
            Next
        Next

        Dim NewInputtableau(TMatrixLength + 1, MatrixLength + 1) As String 'Formatting matrix into a one step array
        For y = 0 To MatrixLength
            For x = 0 To TMatrixLength + 1
                If x = TMatrixLength + 1 Then
                    NewInputtableau(x, y) = "L"
                Else
                    If y = MatrixLength Then
                        NewInputtableau(x, 0) = TMatrix(x, y)
                    Else
                        NewInputtableau(x, y + 1) = TMatrix(x, y)
                    End If
                End If
            Next
        Next
        Dim MyNewMenu As New MinimiseMenu(NewInputtableau, MatrixLength - 1)
    End Sub
    Public Overrides Sub Simplex()
        Throw New NotImplementedException()
    End Sub
End Class
