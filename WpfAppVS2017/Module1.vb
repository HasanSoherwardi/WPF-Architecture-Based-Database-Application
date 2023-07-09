Imports System.Data
Imports System.Data.SqlClient

Module Module1
    Public OleCn As New SqlConnection()
    Public PrintWin As New MainWindow()

    Public Function StrConnection() As String
        StrConnection = "Server=MOHAMMED; Database=PatientInfo; Integrated Security=true;"
        Return StrConnection
    End Function

End Module
