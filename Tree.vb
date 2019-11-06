Imports NEA

Public Class Tree
    Implements IMenu
    Private NoOfNodes As Integer
    Private UserInputTable As String(,)
    Private ExistingConnections As New List(Of Integer)
    Private Edges As List(Of String)
    Private NoOfEdges As Integer

    Public Sub New()
        Dim NextEdgeCount As Integer = -1
        NoOfNodes = OneDMenu(New List(Of String)({"Enter the amount of nodes in the graph"}), "", "Integer")(0) - 1
        If NoOfNodes <= 1 Or NoOfNodes >= 19 Then
            Throw New ArgumentException("Number of nodes is out of range (2-19)")
        End If
        For i = 0 To NoOfNodes * (NoOfNodes - 1) + 1
            ExistingConnections.Add(1)
        Next
        ExistingConnections = OneDMenu(VariableNames, "Enter the weights of these edges (Enter 0 if they do not exist)", "Integer") 'Gets the arc weights of the graph
        Edges = VariableNames()
        NoOfEdges = Edges.Count - 1
        CycleChecker()
    End Sub

    'Private Function RecursiveCombinatorics(ListOfNodes As List(Of String), Cycles As List(Of String), count As Integer)
    '    Dim CurrentLetter As String = ListOfNodes(ListOfNodes.Count - count)
    '    count -= 1
    '    If count = 0 Then
    '        Cycles.Add(CurrentLetter)
    '        Return Cycles
    '    Else
    '        Cycles = RecursiveCombinatorics(ListOfNodes, Cycles, count)
    '        Dim NewCycles As New List(Of String)
    '        For Each element In Cycles
    '            For i = 0 To Len(element)
    '                NewCycles.Add(Left(element, i) & CurrentLetter & Right(element, Len(element) - i))
    '            Next
    '            NewCycles.Add(element)
    '        Next
    '        Return NewCycles
    '    End If

    'End Function

    Private Sub CycleChecker() 'I need to find all the cycles in the graph
        Dim NodeStack As New Stack()
        Dim Cycles As New List(Of String)
        Dim ListOfNodes As New List(Of String)

        For i = 0 To NoOfNodes
            ListOfNodes.Add(Chr(65 + i))
        Next
        'Cycles = RecursiveCombinatorics(ListOfNodes, Cycles, ListOfNodes.Count)
        'Cycles.RemoveAll(Function(x) Len(x) <= 2)

        'For Each stringthing In Cycles
        '    Console.WriteLine(stringthing)
        'Next
        'Console.ReadLine()

    End Sub
    Public Function GetMode() As Integer Implements IMenu.GetMode
        Return 3
    End Function

    Public Function GetConstraints() As String(,) Implements IMenu.GetConstraints
        Return UserInputTable
    End Function

    Public Function VariableNames() As List(Of String) Implements IMenu.VariableNames
        Dim variables As New List(Of String)
        For i = 0 To NoOfNodes
            For j = 0 To NoOfNodes
                If i < j Then
                    variables.Add(Chr(65 + i) & Chr(65 + j))
                End If
            Next
        Next
        Return variables
    End Function
End Class
