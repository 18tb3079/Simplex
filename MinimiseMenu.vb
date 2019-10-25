Imports NEA

Public Class MinimiseMenu
    Implements IMenu
    Private constraints As String(,)
    Private variables As Integer

    Public Sub New(mycontraints As String(,), myvariables As Integer)
        constraints = mycontraints
        variables = myvariables
    End Sub

    Public Function GetMode() As Integer Implements IMenu.GetMode
        Return 1
    End Function

    Public Function GetConstraints() As String(,) Implements IMenu.GetConstraints
        Return constraints
    End Function

    Public Function VariableNames() As List(Of String) Implements IMenu.VariableNames
        Throw New NotImplementedException()
    End Function
End Class
