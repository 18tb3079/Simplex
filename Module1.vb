﻿
Module Module1
    Function OneDMenu(Questions As List(Of String), Instruction As String, Type As String) As List(Of Integer)
        If Type <> "Blank" And Instruction <> "" Then Questions.Add("Press enter here to continue")
        Dim NoOfQs As Integer = Questions.Count - 1
        Dim Answers As New List(Of Integer)
        For i = 0 To Questions.Count - 1
            Questions(i) &= ": "
            Answers.Add(0)
        Next
        Dim Line As Integer = 0
        Dim key As ConsoleKey


        Console.Clear()
        Console.WriteLine(Instruction)
        For l = 0 To NoOfQs
            Console.Write(Questions(l))
            If (l <> NoOfQs Or Instruction = "") And Type <> "Blank" Then Console.Write(Answers(l))
            Console.Write(vbCrLf)
        Next
        Do
            Console.SetCursorPosition(Len(Questions(Line)), Line + 1)
            If (Line <> NoOfQs Or Instruction = "") And Type <> "Blank" Then Console.Write(Answers(Line))
            Console.ForegroundColor = ConsoleColor.Red
            Console.Write(" <--")
            Console.ForegroundColor = ConsoleColor.Gray
            Console.SetCursorPosition(Len(Questions(Line)), Line + 1)
            key = Console.ReadKey(True).Key
            Select Case key
                Case ConsoleKey.UpArrow
                    If Line <> 0 Then
                        Console.SetCursorPosition(Len(Questions(Line) & Answers(Line)), Line + 1)
                        Console.Write("          ")
                        Line -= 1
                    End If
                Case ConsoleKey.DownArrow
                    If Line <> NoOfQs Then
                        Console.SetCursorPosition(Len(Questions(Line) & Answers(Line)), Line + 1)
                        Console.Write("          ")
                        Line += 1
                    End If
                Case ConsoleKey.Enter
                    If Type = "Blank" Then
                        Answers.Clear()
                        Answers.Add(Line)
                        Return Answers
                    End If
                    If Instruction = "" Then
                        Return Answers
                    End If
                    If Line = NoOfQs Then
                        Answers.RemoveAt(NoOfQs)
                        Return Answers
                    Else
                        Console.SetCursorPosition(Len(Questions(Line) & Answers(Line)), Line + 1)
                        Console.Write("          ")
                        Line += 1
                    End If
                Case ConsoleKey.Backspace
                    Answers(Line) = Answers(Line) \ 10
                Case Else
                    If Type = "Integer" Then
                        If Line <> NoOfQs Or Instruction = "" Then
                            key = key - 48 'Converts inputted key into an integer
                            If key >= 0 And key <= 9 Then
                                If Answers(Line) <= 9 Then
                                    Answers(Line) = Answers(Line) * 10 + key
                                End If

                            End If
                        End If
                    ElseIf Type = "Boolean" Then '1 or 0
                        If Line <> NoOfQs Or Instruction = "" Then
                            key = key - 48 'Converts inputted key into an integer
                            If key = 0 Or key = 1 Then
                                Answers(Line) = key
                            End If
                        End If
                    Else 'blank (no text)
                    End If
            End Select
        Loop
        Console.ReadKey()
    End Function

    Sub Main()
        Console.CursorVisible = False
        Dim MyMenu As IMenu
        Dim choice As ConsoleKey = 0
        Dim OptimisationProblem As Integer
        Do Until choice >= ConsoleKey.D4
            Console.Clear()
            'Try
            OptimisationProblem = OneDMenu(New List(Of String)({"Custom Constraints", "Maximal Matching", "Maximum Flow"}), "Select Mode", "Blank")(0)
                If OptimisationProblem = 0 Then
                    MyMenu = New SimplexMenu()
                ElseIf OptimisationProblem = 1 Then
                    MyMenu = New Matching()
                ElseIf OptimisationProblem = 2 Then
                    MyMenu = New Flow()
                Else
                    MyMenu = New SimplexMenu()
                End If

                Dim MyTableau As Tableau
                Do Until choice >= ConsoleKey.D2
                    If MyMenu.GetMode = 1 Then
                        MyTableau = New OneStep(MyMenu)
                    ElseIf MyMenu.GetMode = 2 Then
                        MyTableau = New TwoStep(MyMenu)
                    Else
                        MyTableau = New OneStep(MyMenu)
                    End If
                    MyTableau.OutputConstraintsFromTableau()
                    MyTableau.Simplex()
                    Console.WriteLine(vbCrLf & "Please enter your next action") 'THIS CAN SO BE A MENU CHANGE IT TOM
                    Console.WriteLine("1. Run the simplex algorithm again with these constraints")
                    Console.WriteLine("2. Run the simplex algorithm again with different constraints")
                    Console.WriteLine("3. Go back to the main menu")
                    Console.WriteLine("4. Exit")
                    choice = Console.ReadKey(True).Key
                Loop
                ' Catch ex As Exception
            'Console.Clear()
            'Console.WriteLine(ex.Message)
            'Console.WriteLine("Press enter to restart: ")
            'Console.ReadLine()
            'End Try
        Loop
    End Sub

End Module



