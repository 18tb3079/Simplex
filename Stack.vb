Public Class Stack 'Only used by the Tree class
    Private Items As List(Of String)
    Private size As Integer
    Public Sub New()
        Items = New List(Of String)
        size = -1
    End Sub

    Public Function Peek()
        Return Items(size)
    End Function

    Public Function Pop()
        Dim temp As String = Items(size)
        Items.RemoveAt(size)
        size -= 1
        Return temp
    End Function

    Public Sub Push(item As String)
        Items.Add(item)
        size += 1
    End Sub

    Public Function GetLength()
        Return size
    End Function

End Class
