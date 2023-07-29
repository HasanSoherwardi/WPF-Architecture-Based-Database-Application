Imports System.Data
Imports WpfAppDL
Imports WpfAppModel

Public Class BLPatientInfo
    Public Function GetPatientInfo() As DataTable
        Dim dtData As DataTable = New DataTable()
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            dtData = objDL.GetPatientDetail()
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dtData
    End Function

    Public Function FilterPatientInfo(ByVal Value As String) As DataTable
        Dim dtData As DataTable = New DataTable()
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            dtData = objDL.FilterPatientDetail(Value)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dtData
    End Function

    Public Function InsertPatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            msg = objDL.InsertPatientDetail(patient)
        Catch ex As Exception
            msg = "exception"
        End Try

        Return msg
    End Function

    Public Function SelectPatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        Dim ds As DataSet = New DataSet()
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            msg = objDL.SelectPatientDetail(patient).ToString()
        Catch ex As Exception
            msg = "exception"
        End Try

        Return msg
    End Function

    Public Function UpdatePatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            msg = objDL.UpdatePatientDetail(patient)
        Catch ex As Exception
            msg = "exception"
        End Try

        Return msg
    End Function

    Public Function DeletePatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            msg = objDL.DeletePatientDetail(patient)
        Catch ex As Exception
            msg = "exception"
        End Try

        Return msg
    End Function

    Public Function PrintAllPatientInfo() As DataTable
        Dim dtData As DataTable = New DataTable()
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            dtData = objDL.PrintAllPatientDetail()
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dtData
    End Function

    Public Function PrintPatientInfo(ByVal Column As String, ByVal Value As String) As DataTable
        Dim dtData As DataTable = New DataTable()
        Dim objDL As DLPatientInfo = New DLPatientInfo()

        Try
            dtData = objDL.PrintPatientDetail(Column, Value)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dtData
    End Function
End Class
