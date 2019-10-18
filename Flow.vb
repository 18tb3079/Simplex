Public Class Flow
    Implements IMenu
    Private NoOfNodes As Integer
    Private UserInputTable As String(,)
    Private ExistingConnections As New List(Of Integer)

    Public Sub New()
        NoOfNodes = OneDMenu(New List(Of String)({"Enter the amount of nodes in the graph"}), "", "Integer")(0) - 1
        For i = 0 To NoOfNodes * (NoOfNodes + 1)
            ExistingConnections.Add(1)
        Next
        ExistingConnections = OneDMenu(VariableNames, "Enter the weights of these edges (Enter 0 if they do not exist)", "Integer") 'Gets the arc weights of the graph
        Dim Edges As List(Of String) = VariableNames()
        Dim NoOfEdges As Integer = Edges.Count - 1

        Dim TableLength As Integer = NoOfEdges + 2
        Dim TableHeight As Integer = NoOfNodes + NoOfEdges + 2
        ReDim UserInputTable(TableLength, TableHeight)
        For y = 0 To TableHeight
            For x = 0 To TableLength
                If x = TableLength Then
                    If y <= NoOfNodes + 1 Then 'Less than, equal to or greater than
                        UserInputTable(x, y) = "E"
                    Else
                        UserInputTable(x, y) = "L"
                    End If
                ElseIf x = TableLength - 1 Then 'Value column
                    If y <= NoOfNodes + 1 Then
                        UserInputTable(x, y) = 0
                    Else
                        UserInputTable(x, y) = ExistingConnections(y - NoOfEdges - 2)
                    End If
                ElseIf y = 0 Then
                    UserInputTable(x, y) = ExistingConnections(x)
                ElseIf y <= NoOfNodes + 1 Then
                    If Left(ExistingConnections(x), 1) = Chr(64 + y) Then
                        UserInputTable(x, y) = -1
                    ElseIf Right(ExistingConnections(x), 1) = Chr(64 + y) Then
                        UserInputTable(x, y) = 1
                    Else
                        UserInputTable(x, y) = 0
                    End If
                Else
                    If x = y - NoOfEdges - 2 Then
                        UserInputTable(x, y) = 1
                    End If
                End If
            Next
        Next
    End Sub

    Public Function GetMode() As Integer Implements IMenu.GetMode
        Return 2
    End Function

    Public Function GetConstraints() As String(,) Implements IMenu.GetConstraints
        Return UserInputTable
    End Function

    Public Function VariableNames() As List(Of String) Implements IMenu.VariableNames
        Dim variables As New List(Of String)
        Dim count As Integer = 0
        For i = 0 To NoOfNodes
            For j = 0 To NoOfNodes
                If i <> j Then
                    If ExistingConnections(count) <> 0 Then
                        variables.Add(Chr(65 + i) & Chr(65 + j))
                    End If
                    count += 1
                End If
            Next
        Next
        Return variables
    End Function
End Class
