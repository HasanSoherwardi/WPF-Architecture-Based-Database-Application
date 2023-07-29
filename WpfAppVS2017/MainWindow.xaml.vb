Imports System.Data
Imports System.Data.SqlClient

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports SAPBusinessObjects.WPF.Viewer
Imports WpfAppBL

Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        CrystalReportViewer1.ToggleSidePanel = SAPBusinessObjects.WPF.Viewer.Constants.SidePanelKind.None
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs)
        Try
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

            Dim objBL As BLPatientInfo = New BLPatientInfo()
            Dim dtData As DataTable = New DataTable()

            Try
                dtData = objBL.PrintAllPatientInfo()
                If dtData.Rows.Count > 0 Then
                    cryRpt.SetDataSource(dtData)
                    CrystalReportViewer1.ViewerCore.ReportSource = cryRpt
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString(), "Report DataSet Error")
            End Try

            txtSearch.Focus()
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
            MsgBox("First you should select the print category !!!", MsgBoxStyle.Critical, "Attention...")
            Exit Sub
        End If

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

            Dim objBL As BLPatientInfo = New BLPatientInfo()
            Dim dtData As DataTable = New DataTable()

            Try
                dtData = objBL.PrintPatientInfo(CBSearch.Text, txtSearch.Text)
                If dtData.Rows.Count > 0 Then
                    cryRpt.SetDataSource(dtData)
                    CrystalReportViewer1.ViewerCore.ReportSource = cryRpt
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString(), "Report DataSet Error")
            End Try

            txtSearch.Focus()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox(ex.Message(), MsgBoxStyle.Critical, "Print Report Error...")
        End Try

    End Sub
End Class
