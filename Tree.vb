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

    Private Sub CycleChecker() 'I need to find all the cycles in the graph
        Dim Cyclestack As New Stack()
        Dim Cycles As New List(Of String)
        Dim Nodes As New List(Of String)
        Dim UnsearchedEdges As New List(Of String)
        Dim currentnode As String
        Dim travelled As Boolean
        For i = 0 To NoOfNodes
            Nodes.Add(Chr(65 + i))
        Next


    End Sub
    Public Function GetMode() As Integer Implements IMenu.GetMode
        Return 3
    End Function

    Public Function GetConstraints() As String(,) Implements IMenu.GetConstraints
        Return UserInputTable
    End Function

    Public Function VariableNames() As List(Of String) Implements IMenu.VariableNames
        For i = 0 To NoOfNodes
            For j = 0 To NoOfNodes

            Next
        Next
    End Function
End Class
