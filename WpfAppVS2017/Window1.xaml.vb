Imports System.Data
Imports System.Data.SqlClient
Imports WpfAppBL
Imports WpfAppModel

Public Class Window1

    Private Sub BindPatientInfo()
        Dim objBL As BLPatientInfo = New BLPatientInfo()
        Dim dtData As DataTable = New DataTable()

        Try
            dtData = objBL.GetPatientInfo()

            If dtData.Rows.Count > 0 Then
                DataGrid1.ItemsSource = dtData.AsDataView
            End If
            lblRecords.Content = "Records: " & Me.DataGrid1.SelectedIndex + 1 & " of " & Me.DataGrid1.Items.Count.ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Load Data Error")
        End Try
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

        Dim patient As Patient = New Patient()
        Dim objBL As BLPatientInfo = New BLPatientInfo()
        Dim msg As String = String.Empty

        Try
            patient.PatientName = txtPName.Text.Trim()
            patient.Disease = txtDisease.Text.Trim()
            patient.Address = txtAddress.Text.Trim()
            patient.City = txtCity.Text.Trim()
            patient.Mobile = txtMobile.Text.Trim()
            msg = objBL.InsertPatientDetail(patient)

            If msg.Equals("success") Then
                MessageBox.Show("New Record is Added Successfully.", "Record Saved", MessageBoxButton.OK, MessageBoxImage.Information)
                Call Clear()
                Call BindPatientInfo()
            Else
                MsgBox("Record Addition Failed ", MsgBoxStyle.Critical, "Addition Failed")
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
            End
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Window1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        BtnNew.Visibility = Visibility.Visible
        BtnSave.Visibility = Visibility.Hidden
        BtnCancel.Visibility = Visibility.Hidden
        BtnClose.Visibility = Visibility.Visible
        Call BindPatientInfo()
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

        Dim patient As Patient = New Patient()
        Dim objBL As BLPatientInfo = New BLPatientInfo()
        Dim msg As String = String.Empty

        Try
            patient.PatientCode = Convert.ToInt32(a)
            msg = objBL.SelectPatientDetail(patient).ToString()

            If msg.Equals("success") Then
                txtPCode.Text = patient.PatientCode.ToString()
                txtPName.Text = patient.PatientName.ToString()
                txtDisease.Text = patient.Disease.ToString()
                txtAddress.Text = patient.Address.ToString()
                txtCity.Text = patient.City.ToString()
                txtMobile.Text = patient.Mobile.ToString()

                If BtnSave.Visibility = Visibility.Visible Then
                    Call OnCancel()
                End If
            Else
                MsgBox("Record Searching Failed!!! ", MsgBoxStyle.Critical, "Searching Failed")
            End If

        Catch ex As Exception
            MsgBox(ex.Message(), MsgBoxStyle.Critical, "Find Error")
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

            Dim patient As Patient = New Patient()
            Dim objBL As BLPatientInfo = New BLPatientInfo()
            Dim msg2 As String = String.Empty

            Try
                patient.PatientCode = txtPCode.Text.Trim()
                patient.PatientName = txtPName.Text.Trim()
                patient.Disease = txtDisease.Text.Trim()
                patient.Address = txtAddress.Text.Trim()
                patient.City = txtCity.Text.Trim()
                patient.Mobile = txtMobile.Text.Trim()
                msg2 = objBL.UpdatePatientDetail(patient)

                If msg2.Equals("success") Then
                    MessageBox.Show("Record Update Successfully", "Record Update", MessageBoxButton.OK, MessageBoxImage.Information)
                    Call BindPatientInfo()
                Else
                    MsgBox("Record Updation Failed ", MsgBoxStyle.Critical, "Updation Failed")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString(), "Updation Error")
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

            Dim patient As Patient = New Patient()
            Dim objBL As BLPatientInfo = New BLPatientInfo()
            Dim msg2 As String = String.Empty

            Try
                patient.PatientCode = txtPCode.Text.Trim()
                msg2 = objBL.DeletePatientDetail(patient)

                If msg2.Equals("success") Then
                    MessageBox.Show("Record Deleted Successfully", "Record Deleted", MessageBoxButton.OK, MessageBoxImage.Information)
                    Call Clear()
                    Call BindPatientInfo()
                Else
                    MsgBox("Record Deletion Failed ", MsgBoxStyle.Critical, "Deletion Failed")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString(), "Deletion Error")
            End Try
        End If
    End Sub

    Private Sub BtnReferesh_Click(sender As Object, e As RoutedEventArgs)
        Call BindPatientInfo()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As TextChangedEventArgs)

        Dim objBL As BLPatientInfo = New BLPatientInfo()
        Dim dtData As DataTable = New DataTable()

        Try
            dtData = objBL.FilterPatientInfo(txtSearch.Text.Trim())

            If dtData.Rows.Count > 0 Then
                DataGrid1.ItemsSource = dtData.AsDataView
            End If
            lblRecords.Content = "Records: " & Me.DataGrid1.SelectedIndex + 1 & " of " & Me.DataGrid1.Items.Count.ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Load Data Error")
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As RoutedEventArgs)
        PrintWin.Show()
    End Sub

    Private Sub DataGrid1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DataGrid1.SelectionChanged
        lblRecords.Content = "Records: " & Me.DataGrid1.SelectedIndex + 1 & " of " & Me.DataGrid1.Items.Count.ToString()
    End Sub
End Class
