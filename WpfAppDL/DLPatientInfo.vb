
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports WpfAppModel

Public Class DLPatientInfo


    Private con As SqlConnection = New SqlConnection(ConfigurationSettings.AppSettings("conString"))
    Private com As SqlCommand = New SqlCommand()

    Public Function GetPatientDetail() As DataTable
        Dim dtData As DataTable = New DataTable()
        com.Connection = con
        com.CommandText = ("select * from tblPatient")
        com.CommandType = CommandType.Text
        con.Open()

        Try
            Dim da As SqlDataAdapter = New SqlDataAdapter(com)
            da.Fill(dtData)
        Catch ex As Exception
            Dim err As String = ex.Message
        Finally
            con.Close()
        End Try

        Return dtData
    End Function

    Public Function FilterPatientDetail(ByVal Value As String) As DataTable
        Dim dtData As DataTable = New DataTable()
        com.Connection = con
        com.CommandText = ("select * from tblPatient Where Disease Like '%" & Value & "%' or PatientName Like '%" & Value & "%' or Mobile Like '" & Value & "%'")
        com.CommandType = CommandType.Text
        con.Open()

        Try
            Dim da As SqlDataAdapter = New SqlDataAdapter(com)
            da.Fill(dtData)
        Catch ex As Exception
            Dim err As String = ex.Message
        Finally
            con.Close()
        End Try

        Return dtData
    End Function

    Public Function InsertPatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        com.Connection = con
        com.CommandText = "insert into tblPatient (PatientName,Disease,Address,City,Mobile) values(@PatientName,@Disease,@Address,@City,@Mobile)"
        com.CommandType = CommandType.Text

        Try
            'PatientName
            Dim PatientName As SqlParameter = New SqlParameter("@PatientName", SqlDbType.VarChar, 50)
            PatientName.Value = patient.PatientName.ToString()
            com.Parameters.Add(PatientName)

            'Disease
            Dim Disease As SqlParameter = New SqlParameter("@Disease", SqlDbType.VarChar, 50)
            Disease.Value = patient.Disease.ToString()
            com.Parameters.Add(Disease)

            'Address
            Dim Address As SqlParameter = New SqlParameter("@Address", SqlDbType.VarChar, 300)
            Address.Value = patient.Address.ToString()
            com.Parameters.Add(Address)

            'City
            Dim City As SqlParameter = New SqlParameter("@City", SqlDbType.VarChar, 50)
            City.Value = patient.City.ToString()
            com.Parameters.Add(City)

            'Mobile
            Dim Mobile As SqlParameter = New SqlParameter("@Mobile", SqlDbType.VarChar, 20)
            Mobile.Value = patient.Mobile.ToString()
            com.Parameters.Add(Mobile)

            con.Open()
            Dim n As Integer = com.ExecuteNonQuery()

            If n > 0 Then
                msg = "success"
            Else
                msg = "failed"
            End If

            con.Close()
        Catch ex As Exception
            msg = "exception"
        Finally
            com.Dispose()
            con.Dispose()
        End Try

        Return msg
    End Function

    Public Function SelectPatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        com.Connection = con
        com.CommandText = "select [PatientCode],[PatientName],[Disease],[Address],[City],[Mobile] from tblPatient where PatientCode = " & patient.PatientCode & ""
        com.CommandType = CommandType.Text
        Dim dr As SqlDataReader = Nothing

        Try
            con.Open()
            dr = com.ExecuteReader()

            If dr.Read() Then
                msg = "success"
                patient.PatientCode = Convert.ToInt32(dr(0).ToString())
                patient.PatientName = dr(1).ToString()
                patient.Disease = dr(2).ToString()
                patient.Address = dr(3).ToString()
                patient.City = dr(4).ToString()
                patient.Mobile = dr(5).ToString()
            Else
                msg = "failed"
            End If

            con.Close()
        Catch ex As Exception
            msg = "exception"
        Finally
            com.Dispose()
            con.Dispose()
        End Try

        Return msg
    End Function

    Public Function UpdatePatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        com.Connection = con
        com.CommandText = "Update tblPatient Set PatientName=@PatientName,Disease=@Disease,Address=@Address,City=@City,Mobile=@Mobile Where (PatientCode=@PatientCode)"
        com.CommandType = CommandType.Text

        Try
            'PatientName
            Dim PatientName As SqlParameter = New SqlParameter("@PatientName", SqlDbType.VarChar, 50)
            PatientName.Value = patient.PatientName.ToString()
            com.Parameters.Add(PatientName)

            'Disease
            Dim Disease As SqlParameter = New SqlParameter("@Disease", SqlDbType.VarChar, 50)
            Disease.Value = patient.Disease.ToString()
            com.Parameters.Add(Disease)

            'Address
            Dim Address As SqlParameter = New SqlParameter("@Address", SqlDbType.VarChar, 300)
            Address.Value = patient.Address.ToString()
            com.Parameters.Add(Address)

            'City
            Dim City As SqlParameter = New SqlParameter("@City", SqlDbType.VarChar, 50)
            City.Value = patient.City.ToString()
            com.Parameters.Add(City)

            'Mobile
            Dim Mobile As SqlParameter = New SqlParameter("@Mobile", SqlDbType.VarChar, 20)
            Mobile.Value = patient.Mobile.ToString()
            com.Parameters.Add(Mobile)

            'PatientCode
            Dim PatientCode As SqlParameter = New SqlParameter("@PatientCode", SqlDbType.Int)
            PatientCode.Value = Convert.ToInt32(patient.PatientCode.ToString())
            com.Parameters.Add(PatientCode)

            con.Open()
            Dim n As Integer = com.ExecuteNonQuery()

            If n > 0 Then
                msg = "success"
            Else
                msg = "failed"
            End If

            con.Close()
        Catch ex As Exception
            msg = "exception"
        Finally
            com.Dispose()
            con.Dispose()
        End Try

        Return msg
    End Function

    Public Function DeletePatientDetail(ByVal patient As Patient) As String
        Dim msg As String = String.Empty
        com.Connection = con
        com.CommandText = "Delete From tblPatient where [PatientCode]=@PatientCode"
        com.CommandType = CommandType.Text

        Try
            'PatientCode
            Dim PatientCode As SqlParameter = New SqlParameter("@PatientCode", SqlDbType.Int)
            PatientCode.Value = Convert.ToInt32(patient.PatientCode.ToString())
            com.Parameters.Add(PatientCode)

            con.Open()
            Dim n As Integer = com.ExecuteNonQuery()

            If n > 0 Then
                msg = "success"
            Else
                msg = "failed"
            End If

            con.Close()
        Catch ex As Exception
            msg = "exception"
        Finally
            com.Dispose()
            con.Dispose()
        End Try

        Return msg
    End Function

    Public Function PrintAllPatientDetail() As DataTable
        Dim dtData As DataTable = New DataTable()
        com.Connection = con
        com.CommandText = ("select * from tblPatient")
        com.CommandType = CommandType.Text
        con.Open()

        Try
            Dim da As SqlDataAdapter = New SqlDataAdapter(com)
            da.Fill(dtData)
        Catch ex As Exception
            Dim err As String = ex.Message
        Finally
            con.Close()
        End Try

        Return dtData
    End Function

    Public Function PrintPatientDetail(ByVal Column As String, ByVal Value As String) As DataTable

        If Column = "Name" Then
            Column = "PatientName"
        ElseIf Column = "Disease" Then
            Column = "Disease"
        ElseIf Column = "Mobile" Then
            Column = "Mobile"
        End If

        Dim dtData As DataTable = New DataTable()
        com.Connection = con
        com.CommandText = ("select * from tblPatient Where " & Column & " Like '%" & Value & "%'")
        com.CommandType = CommandType.Text
        con.Open()

        Try
            Dim da As SqlDataAdapter = New SqlDataAdapter(com)
            da.Fill(dtData)
        Catch ex As Exception
            Dim err As String = ex.Message
        Finally
            con.Close()
        End Try

        Return dtData
    End Function
End Class
