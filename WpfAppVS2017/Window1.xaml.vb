Imports System.Data
Imports System.Data.SqlClient


Public Class Window1

    Sub LoadData()
        Try
            With OleCn
                If .State <> ConnectionState.Open Then
                    .ConnectionString = StrConnection()
                    .Open()
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        Dim cmd As New SqlCommand("select * from tblPatient", OleCn)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds, "tblPatient")
        DataGrid1.ItemsSource = ds.Tables("tblPatient").AsDataView
        lblRecords.Content = "Records: " & Me.DataGrid1.SelectedIndex + 1 & " of " & Me.DataGrid1.Items.Count.ToString()
    End Sub

    Sub Clear()
        txtPCode.Clear()
        txtPName.Clear()
        txtDisease.Clear()
        txtAddress.Clear()
        txtCity.Clear()
        txtMobile.Clear()
        txtPName.Focus()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        BtnNew.Visibility = Visibility.Hidden
        BtnSave.Visibility = Visibility.Visible
        BtnCancel.Visibility = Visibility.Visible
        BtnClose.Visibility = Visibility.Hidden
        Call Clear()

    End Sub

    Private Sub BtnSave_Click(sender As Object, e As RoutedEventArgs)
        If RequiredEntry() = True Then
            Return
        End If

        Try
            With OleCn
                If .State <> ConnectionState.Open Then
                    .ConnectionString = StrConnection()
                    .Open()
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        Try

            Dim sSQL As String = "insert into tblPatient (PatientName,Disease,Address,City,Mobile) values(@PatientName,@Disease,@Address,@City,@Mobile)"

            Dim cmd As SqlCommand = New SqlCommand(sSQL, OleCn)

            'PatientName
            Dim PatientName As SqlParameter = New SqlParameter("@PatientName", SqlDbType.VarChar, 50)
            PatientName.Value = txtPName.Text.ToString()
            cmd.Parameters.Add(PatientName)

            'Disease
            Dim Disease As SqlParameter = New SqlParameter("@Disease", SqlDbType.VarChar, 50)
            Disease.Value = txtDisease.Text.ToString()
            cmd.Parameters.Add(Disease)

            'Address
            Dim Address As SqlParameter = New SqlParameter("@Address", SqlDbType.VarChar, 300)
            Address.Value = txtAddress.Text.ToString()
            cmd.Parameters.Add(Address)

            'City
            Dim City As SqlParameter = New SqlParameter("@City", SqlDbType.VarChar, 50)
            City.Value = txtCity.Text.ToString()
            cmd.Parameters.Add(City)

            'Mobile
            Dim Mobile As SqlParameter = New SqlParameter("@Mobile", SqlDbType.VarChar, 20)
            Mobile.Value = txtMobile.Text.ToString()
            cmd.Parameters.Add(Mobile)

            Dim temp As Integer = 0
            temp = cmd.ExecuteNonQuery()

            If temp > 0 Then
                OleCn.Close()
                MessageBox.Show("New Record is Added Successfully.", "Record Saved", MessageBoxButton.OK, MessageBoxImage.Information)
                Call Clear()
                'Call OnCancel()
                Call LoadData()
            Else
                OleCn.Close()
                MsgBox("Record Addition Failed ", MsgBoxStyle.Critical, "Addition Failed")
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Data Error")
        End Try

    End Sub

    Sub OnCancel()
        BtnNew.Visibility = Visibility.Visible
        BtnSave.Visibility = Visibility.Hidden
        BtnCancel.Visibility = Visibility.Hidden
        BtnClose.Visibility = Visibility.Visible
    End Sub
    Private Sub BtnCancel_Click(sender As Object, e As RoutedEventArgs)
        Call OnCancel()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs)
        Try

            OleCn.Close()
            End

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Window1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        BtnNew.Visibility = Visibility.Visible
        BtnSave.Visibility = Visibility.Hidden
        BtnCancel.Visibility = Visibility.Hidden
        BtnClose.Visibility = Visibility.Visible
        Call LoadData()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As RoutedEventArgs)
        If BtnSave.Visibility = Visibility.Visible Then
            Call Clear()
        Else
            txtPName.Clear()
            txtDisease.Clear()
            txtAddress.Clear()
            txtCity.Clear()
            txtMobile.Clear()
            txtPName.Focus()
        End If
    End Sub

    Private Function RequiredEntry() As Boolean
        If txtPName.Text = "" Or txtDisease.Text = "" Or txtMobile.Text = "" Then
            MsgBox("Please enter required (*) information....", MsgBoxStyle.Critical, "Attention...")
            Return True
            Exit Function
        End If
    End Function

    Sub RecordSelection()
        On Error Resume Next

        Dim row As DataRowView = DataGrid1.SelectedItem
        txtPCode.Text = row.Row.ItemArray(0).ToString()
        txtPName.Text = row.Row.ItemArray(1).ToString()
        txtDisease.Text = row.Row.ItemArray(2).ToString()
        txtAddress.Text = row.Row.ItemArray(3).ToString()
        txtCity.Text = row.Row.ItemArray(4).ToString()
        txtMobile.Text = row.Row.ItemArray(5).ToString()

        If BtnSave.Visibility = Visibility.Visible Then
            Call OnCancel()
        End If
    End Sub
    Private Sub DataGrid1_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles DataGrid1.MouseDoubleClick
        Call RecordSelection()
    End Sub

    Private Sub DataGrid1_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles DataGrid1.PreviewKeyDown
        If e.Key = Key.Tab Or e.Key = Key.Space Then
            Call RecordSelection()
        End If
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As RoutedEventArgs)
        Dim a As String
        a = InputBox("Enter Patient Code", "Response", 0)
        If a = "" Then
            Exit Sub
        End If

        Try
            With OleCn
                If .State <> ConnectionState.Open Then
                    .ConnectionString = StrConnection()
                    .Open()
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        Try

            Dim dr1 As SqlDataReader
            Dim com As New SqlCommand
            com.CommandText = "select [PatientCode],[PatientName],[Disease],[Address],[City],[Mobile] from tblPatient where PatientCode = " & a & ""
            com.Connection = OleCn
            dr1 = com.ExecuteReader
            If dr1.Read Then

                txtPCode.Text = dr1(0).ToString()
                txtPName.Text = dr1(1).ToString()
                txtDisease.Text = dr1(2).ToString()
                txtAddress.Text = dr1(3).ToString()
                txtCity.Text = dr1(4).ToString()
                txtMobile.Text = dr1(5).ToString()

                If BtnSave.Visibility = Visibility.Visible Then
                    Call OnCancel()
                End If

            Else
                MsgBox("Record Searching Failed!!! ", MsgBoxStyle.Critical, "Searching Failed")
            End If
            OleCn.Close()
            dr1.Close()

        Catch ex As Exception
            MsgBox(ex.Message(), MsgBoxStyle.Critical, "Error...")
        End Try

    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As RoutedEventArgs)
        If BtnSave.Visibility = Visibility.Visible Then
            MsgBox("Record Modification Failed ", MsgBoxStyle.Critical, "Updation Failed")
            Return
        End If

        Dim msg As MessageBoxResult = MessageBox.Show("Do you want to Update this Record? ", "Response", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If (msg = vbYes) Then

            If RequiredEntry() = True Then
                Return
            End If

            Try
                With OleCn
                    If .State <> ConnectionState.Open Then
                        .ConnectionString = StrConnection()
                        .Open()
                    End If
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try

            Try
                Dim cmd2 As New SqlCommand("Update tblPatient Set PatientName=@PatientName,Disease=@Disease,Address=@Address,City=@City,Mobile=@Mobile Where (PatientCode=@PatientCode) ", OleCn)

                'PatientName
                Dim PatientName As SqlParameter = New SqlParameter("@PatientName", SqlDbType.VarChar, 50)
                PatientName.Value = txtPName.Text.ToString()
                cmd2.Parameters.Add(PatientName)

                'Disease
                Dim Disease As SqlParameter = New SqlParameter("@Disease", SqlDbType.VarChar, 50)
                Disease.Value = txtDisease.Text.ToString()
                cmd2.Parameters.Add(Disease)

                'Address
                Dim Address As SqlParameter = New SqlParameter("@Address", SqlDbType.VarChar, 300)
                Address.Value = txtAddress.Text.ToString()
                cmd2.Parameters.Add(Address)

                'City
                Dim City As SqlParameter = New SqlParameter("@City", SqlDbType.VarChar, 50)
                City.Value = txtCity.Text.ToString()
                cmd2.Parameters.Add(City)

                'Mobile
                Dim Mobile As SqlParameter = New SqlParameter("@Mobile", SqlDbType.VarChar, 20)
                Mobile.Value = txtMobile.Text.ToString()
                cmd2.Parameters.Add(Mobile)

                'PatientCode
                Dim PatientCode As SqlParameter = New SqlParameter("@PatientCode", SqlDbType.Int)
                PatientCode.Value = Convert.ToInt32(txtPCode.Text.ToString())
                cmd2.Parameters.Add(PatientCode)

                Dim temp As Integer = 0
                temp = cmd2.ExecuteNonQuery()

                If temp > 0 Then
                    OleCn.Close()
                    MessageBox.Show("Record Update Successfully", "Record Update", MessageBoxButton.OK, MessageBoxImage.Information)
                    Call LoadData()
                Else
                    MsgBox("Record Updation Failed ", MsgBoxStyle.Critical, "Updation Failed")
                    Return
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString(), "Updation Error...")
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As RoutedEventArgs)
        If BtnSave.Visibility = Visibility.Visible Then
            MsgBox("Record Deletion Failed ", MsgBoxStyle.Critical, "Deletion Failed")
            Return
        End If

        Dim msg As MessageBoxResult = MessageBox.Show("Do you want to delete this Record? ", "Response", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If (msg = vbYes) Then

            If RequiredEntry() = True Then
                Return
            End If

            Try
                With OleCn
                    If .State <> ConnectionState.Open Then
                        .ConnectionString = StrConnection()
                        .Open()
                    End If
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try

            Try
                Dim sSQL As String = "Delete From tblPatient where [PatientCode]=@PatientCode"
                Dim cmd2 As SqlCommand = New SqlCommand(sSQL, OleCn)

                'PatientCode
                Dim PatientCode As SqlParameter = New SqlParameter("@PatientCode", SqlDbType.Int)
                PatientCode.Value = Convert.ToInt32(txtPCode.Text.ToString())
                cmd2.Parameters.Add(PatientCode)

                Dim temp As Integer = 0
                temp = cmd2.ExecuteNonQuery()

                If temp > 0 Then
                    OleCn.Close()
                    MessageBox.Show("Record Deleted Successfully", "Record Deleted", MessageBoxButton.OK, MessageBoxImage.Information)
                    Call Clear()
                    Call LoadData()
                Else
                    MsgBox("Record Deletion Failed ", MsgBoxStyle.Critical, "Deletion Failed")
                    Exit Sub
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString(), "Deletion Error")
            End Try
        End If

    End Sub

    Private Sub BtnReferesh_Click(sender As Object, e As RoutedEventArgs)
        Call LoadData()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As TextChangedEventArgs)
        Try
            With OleCn
                If .State <> ConnectionState.Open Then
                    .ConnectionString = StrConnection()
                    .Open()
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        Dim cmd As New SqlCommand("select * from tblPatient Where Disease Like '%" & txtSearch.Text & "%' or PatientName Like '%" & txtSearch.Text & "%' or Mobile Like '%" & txtSearch.Text & "%'", OleCn)

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds, "tblPatient")

        DataGrid1.ItemsSource = ds.Tables("tblPatient").AsDataView
        lblRecords.Content = "Records: " & Me.DataGrid1.SelectedIndex + 1 & " of " & Me.DataGrid1.Items.Count.ToString()
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As RoutedEventArgs)
        PrintWin.Show()
    End Sub

    Private Sub DataGrid1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DataGrid1.SelectionChanged
        lblRecords.Content = "Records: " & Me.DataGrid1.SelectedIndex + 1 & " of " & Me.DataGrid1.Items.Count.ToString()
    End Sub
End Class
