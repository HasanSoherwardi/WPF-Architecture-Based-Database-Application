Imports System.Data
Imports System.Data.SqlClient

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports SAPBusinessObjects.WPF.Viewer

Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        CrystalReportViewer1.ToggleSidePanel = SAPBusinessObjects.WPF.Viewer.Constants.SidePanelKind.None
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs)
        Try

            OleCn.Close()
            Me.Close()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnReferesh_Click(sender As Object, e As RoutedEventArgs)
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        Try
            With OleCn
                If .State <> ConnectionState.Open Then
                    .ConnectionString = StrConnection()
                    .Open()
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

        txtSearch.Text = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Try

            cryRpt.Load(My.Application.Info.DirectoryPath & "\Reports\Report1.rpt")

            With crConnectionInfo
                .ServerName = "MOHAMMED"
                .DatabaseName = "PatientInfo"
                .UserID = "sa"
                .Password = "ali123"
            End With

            CrTables = cryRpt.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next


            Dim QueryString As String

            QueryString = "Select * from tblPatient "
            Dim Cmd As New SqlCommand(QueryString, OleCn)

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(Cmd)
            Dim ds As DataSet = New DataSet()
            Adapter.Fill(ds, "tblPatient")

            cryRpt.SetDataSource(ds)
            CrystalReportViewer1.ViewerCore.ReportSource = cryRpt

            txtSearch.Focus()
            OleCn.Close()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox(ex.Message(), MsgBoxStyle.Critical, "Print Report Error...")
        End Try

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As RoutedEventArgs)
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        If CBSearch.Text = "" Then
            MsgBox("First you should select the Print category !!!", MsgBoxStyle.Critical, "Attention...")
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
            MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If CBSearch.Text = "Name" Then
            Try

                cryRpt.Load(My.Application.Info.DirectoryPath & "\Reports\Report1.rpt")

                With crConnectionInfo
                    .ServerName = "MOHAMMED"
                    .DatabaseName = "PatientInfo"
                    .UserID = "sa"
                    .Password = "ali123"
                End With

                CrTables = cryRpt.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next


                Dim QueryString As String
                QueryString = "Select * from tblPatient where PatientName like @PatientName"
                Dim Cmd As New SqlCommand(QueryString, OleCn)
                Cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtSearch.Text

                Dim Adapter As SqlDataAdapter = New SqlDataAdapter(Cmd)
                Dim ds As DataSet = New DataSet()
                Adapter.Fill(ds, "tblPatient")

                cryRpt.SetDataSource(ds)
                CrystalReportViewer1.ViewerCore.ReportSource = cryRpt

                txtSearch.Focus()
                OleCn.Close()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            Catch ex As Exception
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox(ex.Message(), MsgBoxStyle.Critical, "Print Report Error...")
            End Try

            Exit Sub
        End If


        If CBSearch.Text = "Disease" Then
            Try
                cryRpt.Load(My.Application.Info.DirectoryPath & "\Reports\Report1.rpt")

                With crConnectionInfo
                    .ServerName = "MOHAMMED"
                    .DatabaseName = "PatientInfo"
                    .UserID = "sa"
                    .Password = "ali123"
                End With

                CrTables = cryRpt.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next


                Dim QueryString As String
                QueryString = "Select * from tblPatient where Disease like @Disease"

                Dim Cmd As New SqlCommand(QueryString, OleCn)
                Cmd.Parameters.Add("@Disease", SqlDbType.VarChar).Value = txtSearch.Text

                Dim Adapter As SqlDataAdapter = New SqlDataAdapter(Cmd)
                Dim ds As DataSet = New DataSet()
                Adapter.Fill(ds, "tblPatient")

                cryRpt.SetDataSource(ds)
                CrystalReportViewer1.ViewerCore.ReportSource = cryRpt

                txtSearch.Focus()
                OleCn.Close()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            Catch ex As Exception
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox(ex.Message(), MsgBoxStyle.Critical, "Print Report Error...")
            End Try

            Exit Sub
        End If

        If CBSearch.Text = "Mobile" Then
            Try
                cryRpt.Load(My.Application.Info.DirectoryPath & "\Reports\Report1.rpt")

                With crConnectionInfo
                    .ServerName = "MOHAMMED"
                    .DatabaseName = "PatientInfo"
                    .UserID = "sa"
                    .Password = "ali123"
                End With

                CrTables = cryRpt.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next


                Dim QueryString As String
                QueryString = "Select * from tblPatient where Mobile like @Mobile"

                Dim Cmd As New SqlCommand(QueryString, OleCn)
                Cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = txtSearch.Text

                Dim Adapter As SqlDataAdapter = New SqlDataAdapter(Cmd)
                Dim ds As DataSet = New DataSet()
                Adapter.Fill(ds, "tblPatient")

                cryRpt.SetDataSource(ds)
                Me.CrystalReportViewer1.ViewerCore.ReportSource = cryRpt

                Me.txtSearch.Focus()
                OleCn.Close()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            Catch ex As Exception
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox(ex.Message(), MsgBoxStyle.Critical, "Print Report Error...")
            End Try

            Exit Sub
        End If
    End Sub
End Class
