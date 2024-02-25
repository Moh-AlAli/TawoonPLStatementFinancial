Imports System.Runtime.InteropServices
Imports acc = ACCPAC.Advantage


Public Class PL
    Private server As String
    Private uid As String
    Private pass As String
    Private frmacct As String
    Private toacct As String
    Private Toopttype As String
    Private Tooptsubtype As String
    Private Tooptcat As String
    Private fdate As Integer
    Private tdate As Integer
    Friend Property ERPSession As acc.Session
    Friend Property Company As ERPCompany
    Friend Property SessionDate As String
    Friend Property ObjectHandle As String
    Friend compid As String
    Private _oldVendNumb As String = ""

    <DllImport("a4wroto.dll", EntryPoint:="rotoSetObjectWindow", CharSet:=CharSet.Ansi)>
    Private Shared Sub rotoSetObjectWindow(
        <MarshalAs(UnmanagedType.I8)> ByVal objectHandle As Long,
        <MarshalAs(UnmanagedType.I8)> ByVal hWnd As Long)
    End Sub
    Public Sub New(ByVal ses As acc.Session, ByVal comp As ERPCompany, ByVal sesDate As String)
        InitializeComponent()
        'ObjectHandle = ""
        ERPSession = ses
        Company = comp
        compid = comp.ID

        SessionDate = sesDate

    End Sub
    Public Sub New(ByVal _objectHandle As String)
        InitializeComponent()
        ObjectHandle = _objectHandle
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub PL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Not ObjectHandle Is Nothing Then
                SessionFromERP(Handle)
            End If

            Me.Text = compid + " - " + "PL"

            Txttsubt.Text = "zzzzzzzzzzzzzzzzzzzzzz"
            Txtttype.Text = "zzzzzzzzzzzzzzzzzzzzzz"
            Txttcat.Text = "zzzzzzzzzzzzzzzzzzzzzz"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Close()
        End Try
    End Sub


    Private Sub CMD_Exit_Click(sender As Object, e As EventArgs) Handles CMDClose.Click
        Close()
    End Sub
    Private Sub fndEditBoxValidate(ByVal sender As Object, ByVal e As EventArgs)


        If CMDClose.Focused Then Return
        Dim txb As TextBox = CType(sender, TextBox)
        If String.IsNullOrEmpty(txb.Text) Then Return
        Dim msg As String = ""
        Dim s As String() = New String() {}

        'Select Case txb.Name.Trim()
        '    Case "Txtfrmacct"

        '        If _oldVendNumb.Trim() <> txb.Text.Trim() Then
        '            msg = getValidationData("select ID=ACCTID,NAM=ACCTDESC from GLAMF where ACCTID='" & txb.Text & "'", s)

        '            If msg <> "" Then
        '                MessageBox.Show(Me, msg, "Consolidated Trans.List", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        '                Return
        '            End If

        '            If s.Length = 0 Then
        '                MessageBox.Show(Me, "Account """ & txb.Text & """ does not exists.", "Consolidated Trans.List", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                txb.Focus()
        '                txb.SelectAll()
        '                Return
        '            End If



        '        End If
        '        cfacct = s(0)
        '    Case "Txtctacct"

        '        If _oldVendNumb.Trim() <> txb.Text.Trim() Then
        '            msg = getValidationData("select ID=ACCTID,NAM=ACCTDESC from GLAMF where ACCTID='" & txb.Text & "'", s)

        '            If msg <> "" Then
        '                MessageBox.Show(Me, msg, "Consolidated Trans.List", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        '                Return
        '            End If

        '            If s.Length = 0 Then
        '                MessageBox.Show(Me, "Account """ & txb.Text & """ does not exists.", "Consolidated Trans.List", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                txb.Focus()
        '                txb.SelectAll()
        '                Return
        '            End If


        '        End If


        '  Txtctacct.Text = s(0)
        ' End If
        ' End Select
    End Sub
    Private Sub SessionFromERP(ByVal frmHwnd As IntPtr)
        Dim lhWnd As Long = Nothing

        Try
            If ERPSession Is Nothing Then ERPSession = New acc.Session()
            If ERPSession.IsOpened Then ERPSession.Dispose()
            ERPSession.Init(ObjectHandle, "XX", "XX0001", "67A")

            If Not Long.TryParse(ObjectHandle, lhWnd) Then
                MessageBox.Show("Invalid Sage 300 ERP object handle.", "Consolidated Trans.List Utility", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                ERPSession.Dispose()
                Return
            End If

            rotoSetObjectWindow(lhWnd, frmHwnd.ToInt64())
            Company = New ERPCompany(ERPSession.CompanyName, ERPSession.CompanyID)
            SessionDate = ERPSession.SessionDate.ToString()
        Catch ex As Exception
            Dim erstr As String = ""
            Dim erlst As List(Of String) = New List(Of String)()
            Util.FillErrors(ex, ERPSession, erlst)

            For Each s As String In erlst
                erstr += s & vbCrLf
            Next

            Dim ms As String = "Sage 300 ERP Error: " & erstr
            ERPSession.Dispose()
            MessageBox.Show(ms, "Consolidated Trans.List", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return
        End Try
    End Sub

    Private Sub Butftype_Click(sender As Object, e As EventArgs) Handles Butftype.Click

        Dim ram As String = ""
        If ChRAMDAT.Checked = True Then
            ram = "RAMDAT.dbo."
        End If
        Dim gen As String = ""
        If ChGENDAT.Checked = True Then
            gen = "GENDAT.dbo."
        End If
        Dim jor As String = ""
        If ChJORDAT.Checked = True Then
            jor = "JORDAT.dbo."
        End If
        Dim ocj As String = ""
        If ChOCJDAT.Checked = True Then
            ocj = "OCJDAT.dbo."
        End If
        Dim leb As String = ""
        If ChLEBDAT.Checked = True Then
            leb = "LEBDAT.dbo."
        End If

        If ram = "" And jor = "" And gen = "" And ocj = "" And leb = "" Then
            MessageBox.Show("Choose At least one entity!")

        Else

            Dim vfnd As FromFinder = New FromFinder("OPTFDTYPE", "Type", ram, gen, jor, ocj, leb, New String() {"VALUE"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then
                Txtftype.Text = vfnd.Result.ToArray()(0)
                Txtttype.Text = vfnd.Result.ToArray()(0)
                fndEditBoxValidate(Txtftype, EventArgs.Empty)
            End If
        End If
    End Sub

    Private Sub Butttype_Click(sender As Object, e As EventArgs) Handles Butttype.Click
        Dim ram As String = ""
        If ChRAMDAT.Checked = True Then
            ram = "RAMDAT.dbo."
        End If
        Dim gen As String = ""
        If ChGENDAT.Checked = True Then
            gen = "GENDAT.dbo."
        End If
        Dim jor As String = ""
        If ChJORDAT.Checked = True Then
            jor = "JORDAT.dbo."
        End If
        Dim ocj As String = ""
        If ChOCJDAT.Checked = True Then
            ocj = "OCJDAT.dbo."
        End If
        Dim leb As String = ""
        If ChLEBDAT.Checked = True Then
            leb = "LEBDAT.dbo."
        End If

        If ram = "" And jor = "" And gen = "" And ocj = "" And leb = "" Then
            MessageBox.Show("Choose At least one entity!")

        Else Dim vfnd As FromFinder = New FromFinder("OPTFDTYPE", "Type", ram, gen, jor, ocj, leb, New String() {"VALUE"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then

                Txtttype.Text = vfnd.Result.ToArray()(0)
                fndEditBoxValidate(Txtttype, EventArgs.Empty)
            End If
        End If

    End Sub

    Private Sub Butfsubt_Click(sender As Object, e As EventArgs) Handles Butfsubt.Click
        Dim ram As String = ""
        If ChRAMDAT.Checked = True Then
            ram = "RAMDAT.dbo."
        End If
        Dim gen As String = ""
        If ChGENDAT.Checked = True Then
            gen = "GENDAT.dbo."
        End If
        Dim jor As String = ""
        If ChJORDAT.Checked = True Then
            jor = "JORDAT.dbo."
        End If
        Dim ocj As String = ""
        If ChOCJDAT.Checked = True Then
            ocj = "OCJDAT.dbo."
        End If
        Dim leb As String = ""
        If ChLEBDAT.Checked = True Then
            leb = "LEBDAT.dbo."
        End If

        If ram = "" And jor = "" And gen = "" And ocj = "" And leb = "" Then
            MessageBox.Show("Choose At least one entity!")

        Else
            Dim vfnd As FromFinder = New FromFinder("OPTFDSUBTYPE", "Sub.Type", ram, gen, jor, ocj, leb, New String() {"VALUE"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then
                Txtfsubt.Text = vfnd.Result.ToArray()(0)
                Txttsubt.Text = vfnd.Result.ToArray()(0)
                fndEditBoxValidate(Txtfsubt, EventArgs.Empty)
            End If
        End If

    End Sub

    Private Sub Buttsubt_Click(sender As Object, e As EventArgs) Handles Buttsubt.Click
        Dim ram As String = ""
        If ChRAMDAT.Checked = True Then
            ram = "RAMDAT.dbo."
        End If
        Dim gen As String = ""
        If ChGENDAT.Checked = True Then
            gen = "GENDAT.dbo."
        End If
        Dim jor As String = ""
        If ChJORDAT.Checked = True Then
            jor = "JORDAT.dbo."
        End If
        Dim ocj As String = ""
        If ChOCJDAT.Checked = True Then
            ocj = "OCJDAT.dbo."
        End If
        Dim leb As String = ""
        If ChLEBDAT.Checked = True Then
            leb = "LEBDAT.dbo."
        End If

        If ram = "" And jor = "" And gen = "" And ocj = "" And leb = "" Then
            MessageBox.Show("Choose At least one entity!")

        Else
            Dim vfnd As FromFinder = New FromFinder("OPTFDSUBTYPE", "Sub.Type", ram, gen, jor, ocj, leb, New String() {"VALUE"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then

                Txttsubt.Text = vfnd.Result.ToArray()(0)
                fndEditBoxValidate(Txttsubt, EventArgs.Empty)
            End If
        End If

    End Sub

    Private Sub Butfcat_Click(sender As Object, e As EventArgs) Handles Butfcat.Click
        Dim ram As String = ""
        If ChRAMDAT.Checked = True Then
            ram = "RAMDAT.dbo."
        End If
        Dim gen As String = ""
        If ChGENDAT.Checked = True Then
            gen = "GENDAT.dbo."
        End If
        Dim jor As String = ""
        If ChJORDAT.Checked = True Then
            jor = "JORDAT.dbo."
        End If
        Dim ocj As String = ""
        If ChOCJDAT.Checked = True Then
            ocj = "OCJDAT.dbo."
        End If
        Dim leb As String = ""
        If ChLEBDAT.Checked = True Then
            leb = "LEBDAT.dbo."
        End If

        If ram = "" And jor = "" And gen = "" And ocj = "" And leb = "" Then
            MessageBox.Show("Choose At least one entity!")

        Else
            Dim vfnd As FromFinder = New FromFinder("OPTFDCATEGORY", "Category", ram, gen, jor, ocj, leb, New String() {"VALUE"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then
                Txtfcat.Text = vfnd.Result.ToArray()(0)
                Txttcat.Text = vfnd.Result.ToArray()(0)
                fndEditBoxValidate(Txtfcat, EventArgs.Empty)
            End If
        End If

    End Sub

    Private Sub Buttcat_Click(sender As Object, e As EventArgs) Handles Buttcat.Click
        Dim ram As String = ""
        If ChRAMDAT.Checked = True Then
            ram = "RAMDAT.dbo."
        End If
        Dim gen As String = ""
        If ChGENDAT.Checked = True Then
            gen = "GENDAT.dbo."
        End If
        Dim jor As String = ""
        If ChJORDAT.Checked = True Then
            jor = "JORDAT.dbo."
        End If
        Dim ocj As String = ""
        If ChOCJDAT.Checked = True Then
            ocj = "OCJDAT.dbo."
        End If
        Dim leb As String = ""
        If ChLEBDAT.Checked = True Then
            leb = "LEBDAT.dbo."
        End If

        If ram = "" And jor = "" And gen = "" And ocj = "" And leb = "" Then
            MessageBox.Show("Choose At least one entity!")

        Else
            Dim vfnd As FromFinder = New FromFinder("OPTFDCATEGORY", "Category", ram, gen, jor, ocj, leb, New String() {"VALUE"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then

                Txttcat.Text = vfnd.Result.ToArray()(0)
                fndEditBoxValidate(Txttcat, EventArgs.Empty)
            End If
        End If

    End Sub

    Private Sub butexpexc_Click(sender As Object, e As EventArgs) Handles butexpexc.Click
        Try
            'Dim ds As New DataSet
            'Dim conn As New SqlConnection(Readconnectionstring())
            'Dim da As New SqlDataAdapter

            'Dim iRowCnt As Integer = 0

            'Cursor.Current = Cursors.WaitCursor
            'Dim sEmpList As String = ""





            If Txtttype.Text = Nothing Then
                Toopttype = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
            Else
                Toopttype = Trim(Txtttype.Text)
            End If

            If Txttsubt.Text = Nothing Then
                Tooptsubtype = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
            Else
                Tooptsubtype = Trim(Txttsubt.Text)
            End If

            If Txttcat.Text = Nothing Then
                Tooptcat = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
            Else
                Tooptcat = Trim(Txttcat.Text)
            End If

            Dim ram As String = ""
            If ChRAMDAT.Checked = True Then
                ram = "RAMDAT.dbo."
            End If
            Dim gen As String = ""
            If ChGENDAT.Checked = True Then
                gen = "GENDAT.dbo."
            End If
            Dim jor As String = ""
            If ChJORDAT.Checked = True Then
                jor = "JORDAT.dbo."
            End If
            Dim ocj As String = ""
            If ChOCJDAT.Checked = True Then
                ocj = "OCJDAT.dbo."
            End If
            Dim leb As String = ""
            If ChLEBDAT.Checked = True Then
                leb = "LEBDAT.dbo."
            End If
            Dim fmonth As String = ""

            If DateTimePicker1.Value.Month < 10 Then
                fmonth = "0" & DateTimePicker1.Value.Month
            Else
                fmonth = DateTimePicker1.Value.Month
            End If

            Dim fday As String = ""

            If DateTimePicker1.Value.Day < 10 Then
                fday = "0" & DateTimePicker1.Value.Day
            Else
                fday = DateTimePicker1.Value.Day
            End If

            Dim tmonth As String = ""


            If DateTimePicker2.Value.Month < 10 Then
                tmonth = "0" & DateTimePicker2.Value.Month
            Else
                tmonth = DateTimePicker2.Value.Month
            End If

            Dim tday As String = ""

            If DateTimePicker2.Value.Day < 10 Then
                tday = "0" & DateTimePicker2.Value.Day
            Else
                tday = DateTimePicker2.Value.Day
            End If

            fdate = DateTimePicker1.Value.Year & fmonth & fday
            tdate = DateTimePicker2.Value.Year & tmonth & tday

            Dim totype As String = ""
            If Txtttype.Text = Nothing Then
                totype = "zzzzzzzzzzzzzzzzzzzzzz"
            Else
                totype = Trim(Txtttype.Text)
            End If

            Dim tosubtype As String = ""
            If Txttsubt.Text = Nothing Then
                tosubtype = "zzzzzzzzzzzzzzzzzzzzzz"
            Else
                tosubtype = Trim(Txttsubt.Text)
            End If

            Dim tocat As String = ""
            If Txttcat.Text = Nothing Then
                tocat = "zzzzzzzzzzzzzzzzzzzzzz"
            Else
                tocat = Trim(Txttcat.Text)
            End If
            If ram = Nothing And jor = Nothing And gen = Nothing And ocj = Nothing And leb = Nothing Then
                MessageBox.Show("P&L:Choose At least one Entity")
            Else





                If fdate <= tdate Then
                        Try

                        Dim f As Form = New crviewer(ObjectHandle, ERPSession, fdate, tdate, ChRAMDAT.Checked, ChGENDAT.Checked, ChJORDAT.Checked, ChOCJDAT.Checked, ChLEBDAT.Checked, Trim(Txtftype.Text), Trim(Txtfsubt.Text), Trim(Txtfcat.Text), totype, tosubtype, tocat)
                        f.Show()

                        Catch ex As Exception
                            MessageBox.Show("P&L:" & ex.Message)
                        End Try

                    Else
                        MessageBox.Show("P&L: From Date Greater than To Date")
                    End If

            End If




        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
