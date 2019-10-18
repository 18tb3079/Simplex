Public Class MinimiseStep
    Inherits Tableau

    Public Sub New(mymenu As IMenu) 'This subprogram creates the simplex tableau
        MyBase.New(mymenu)
    End Sub
    Public Overrides Sub Simplex()
        Throw New NotImplementedException()
    End Sub
End Class
