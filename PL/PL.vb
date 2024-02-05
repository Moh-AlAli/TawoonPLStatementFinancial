Imports System.Runtime.InteropServices
Imports acc = ACCPAC.Advantage
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports DocumentFormat.OpenXml.Drawing.Diagrams

Public Class PL
    Private server As String
    Private uid As String
    Private pass As String
    Private frmacct As String
    Private Toacct As String
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

    Friend Function createdes(ByVal key As String) As TripleDES
        Dim md5 As MD5 = New MD5CryptoServiceProvider()
        Dim des As TripleDES = New TripleDESCryptoServiceProvider()
        des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key))
        des.IV = New Byte(des.BlockSize \ 8 - 1) {}
        Return des
    End Function
    Friend Function Decryption(ByVal cyphertext As String, ByVal key As String) As String
        Dim b As Byte() = Convert.FromBase64String(cyphertext)
        Dim des As TripleDES = createdes(key)
        Dim ct As ICryptoTransform = des.CreateDecryptor()
        Dim output As Byte() = ct.TransformFinalBlock(b, 0, b.Length)
        Return Encoding.Unicode.GetString(output)
    End Function
    Friend Function Readconnectionstring() As String

        Dim secretkey As String = "Fhghqwjehqwlegtoit123mnk12%&4#"
        Dim path As String = ("txtcon\SQLwelfcon.txt")
        Dim sr As New StreamReader(path)

        server = sr.ReadLine()
        Dim db As String = sr.ReadLine()
        uid = sr.ReadLine()
        pass = sr.ReadLine()


        server = Decryption(server, secretkey)
        uid = Decryption(uid, secretkey)
        pass = Decryption(pass, secretkey)
        compid = ERPSession.CompanyID
        Dim cons As String = "Data Source =" & server & "; DataBase =" & compid & "; User Id =" & uid & "; Password =" & pass & ";"

        Return cons
    End Function
    Private Sub PL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Not ObjectHandle Is Nothing Then
                SessionFromERP(Handle)
            End If

            Me.Text = compid + " - " + "PL"
            Txttoacct.Text = "zzzzzzzzzzzzzzzzzzzzzz"
            Txttsubt.Text = "zzzzzzzzzzzzzzzzzzzzzz"
            Txtttype.Text = "zzzzzzzzzzzzzzzzzzzzzz"
            Txttcat.Text = "zzzzzzzzzzzzzzzzzzzzzz"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Close()
        End Try
    End Sub

    Private Sub bffind_Click(sender As Object, e As EventArgs) Handles bffind.Click

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
            Dim vfnd As FromFinder = New FromFinder("GLAMF", "Accounts", ram, gen, jor, ocj, leb, New String() {"ACCTID", "ACCTDESC"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then
                Txtfrmacct.Text = vfnd.Result.ToArray()(0)
                Txttoacct.Text = vfnd.Result.ToArray()(0)
                fndEditBoxValidate(Txtfrmacct, EventArgs.Empty)
            End If
        End If
    End Sub

    Private Sub btfind_Click(sender As Object, e As EventArgs) Handles btfind.Click
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
            Dim vfnd As FromFinder = New FromFinder("GLAMF", "Accounts", ram, gen, jor, ocj, leb, New String() {"ACCTID", "ACCTDESC"}, ERPSession, "", "")

            Dim r As DialogResult = vfnd.ShowDialog(Me)
            If r = DialogResult.OK Then
                Txttoacct.Text = vfnd.Result.ToArray()(0)

                fndEditBoxValidate(Txttoacct, EventArgs.Empty)
            End If
        End If
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
        '        Txtfrmacct.Text = s(0)
        '    Case "Txttoacct"

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


        '  Txttoacct.Text = s(0)
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
            Dim ds As New DataSet
            Dim conn As New SqlConnection(Readconnectionstring())
            Dim da As New SqlDataAdapter

            Dim iRowCnt As Integer = 0

            Cursor.Current = Cursors.WaitCursor
            Dim sEmpList As String = ""



            Dim xlAppToUpload As New Excel.Application
            Dim excelBook As Excel.Workbook = xlAppToUpload.Workbooks.Add()
            Dim xlWorkSheetToUpload As Excel.Worksheet

            If Txttoacct.Text = Nothing Then
                Toacct = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
            Else
                Toacct = Trim(Txttoacct.Text)
            End If

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

            If ram = Nothing And jor = Nothing And gen = Nothing And ocj = Nothing And leb = Nothing Then
                MessageBox.Show("P&L:Choose At least one Entity")
            Else


                If trim(Txtfrmacct.Text)<=trim(Txttoacct .Text) Then


                If fdate <= tdate Then
                Try
                xlWorkSheetToUpload = CType(excelBook.Worksheets(1), Excel.Worksheet)



                            Dim sSql As String = "select distinct hh.ENTITY,hh.ACCTID,hh.OPTTYPE,hh.OPTSUBTYPE,hh.OPTCAT,hh.ACCTDESC,hh.ACCTTYPE,sum(hh.BegBalance) as BegBalance ,sum(hh.Debit) as Debit,Sum(hh.Credit) as Credit,Sum(hh.NetChange) as NetChange,Sum(hh.EndingBalance)  as EndingBalance   from ("
                            If ram<>Nothing Then
                                sSql += " select m.ORGID as ENTITY,f.ACCTID,(select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) as OPTTYPE ,(select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) as OPTSUBTYPE,(select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID) as OPTCAT , ACCTDESC,(case when f.ACCTTYPE='I' then 'Income statement' when f.ACCTTYPE='B' then 'Balance sheet' when f.ACCTTYPE='R' then 'Retairned Earning'  end) as ACCTTYPE,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & ram & "glpost bb where bb.ACCTID=f.ACCTID and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "' and bb.DOCDATE <" & fdate & "),0) as BegBalance,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)>=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & ram & "glpost bb where bb.ACCTID=f.ACCTID and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'   and bb.DOCDATE between " & fdate & " and " & tdate & "),0) as Debit,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)<=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & ram & "glpost bb where bb.ACCTID=f.ACCTID and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "' and bb.DOCDATE between " & fdate & " and " & tdate & "),0)  as Credit ,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & ram & "glpost bb where bb.ACCTID=f.ACCTID and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "' and bb.DOCDATE between " & fdate & " and " & tdate & "),0)  as NetChange,coalesce((select SUM(coalesce(bb.TRANSAMT,0))  from  " & ram & "glpost bb where bb.ACCTID=f.ACCTID and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "' and bb.DOCDATE between " & fdate & " and " & tdate & "),0)+coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & ram & "glpost bb where bb.ACCTID=f.ACCTID and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "' and bb.DOCDATE <" & fdate & " ),0)  as EndingBalance from " & ram & "glamf f," & ram & "glpost p," & ram & "CSCOM m where m.ORGID=f.AUDTORG and 
  f.ACCTID =p.ACCTID and 
  p.ACCTID between '" & Trim(Txtfrmacct.Text) & "' and '" & Toacct & "' and p.DOCDATE <=  " & tdate & " and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ram & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'
  union all"
                            End If
                           If jor<> Nothing Then
                                sSql += " select m.ORGID as ENTITY,f.ACCTID,(select top 1 [VALUE]  from  " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) as OPTTYPE ,(select top 1 [VALUE]  from   " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID  ) as OPTSUBTYPE,(select top 1 [VALUE]  from   " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID) as OPTCAT,ACCTDESC,(case when f.ACCTTYPE='I' then 'Income statement' when f.ACCTTYPE='B' then 'Balance sheet' when f.ACCTTYPE='R' then 'Retairned Earning'  end) as ACCTTYPE,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & jor & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & " and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0) as BegBalance,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)>=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & jor & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0) as Debit,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)<=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & jor & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as Credit ,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & jor & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as NetChange,coalesce((select SUM(coalesce(bb.TRANSAMT,0))  from  " & jor & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & "  and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)+coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & jor & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & " and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "' ),0)  as EndingBalance from " & jor & "glamf f," & jor & "glpost p," & jor & "CSCOM m where m.ORGID=f.AUDTORG and 
  f.ACCTID =p.ACCTID and 
  p.ACCTID between '" & Trim(Txtfrmacct.Text) & "' and '" & Toacct & "' and p.DOCDATE <=  " & tdate & " and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & jor & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'
  union all"
                            End If

 If gen<>Nothing Then
                                sSql += " select m.ORGID as ENTITY,f.ACCTID,(select top 1 [VALUE]  from  " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) as OPTTYPE ,(select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) as OPTSUBTYPE,(select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID) as OPTCAT , ACCTDESC,(case when f.ACCTTYPE='I' then 'Income statement' when f.ACCTTYPE='B' then 'Balance sheet' when f.ACCTTYPE='R' then 'Retairned Earning'  end) as ACCTTYPE,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & gen & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & " and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0) as BegBalance,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)>=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & gen & "glpost bb where bb.ACCTID=f.ACCTID and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "') between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "' and bb.DOCDATE between " & fdate & " and " & tdate & "),0) as Debit,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)<=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & gen & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as Credit ,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from " & gen & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as NetChange,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from " & gen & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)+coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from " & gen & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & "  and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as EndingBalance from " & gen & "glamf f," & gen & "glpost p ," & gen & "CSCOM m where m.ORGID=f.AUDTORG and 
 f.ACCTID =p.ACCTID and 
 p.ACCTID between '" & Trim(Txtfrmacct.Text) & "' and '" & Toacct & "' and p.DOCDATE <=  " & tdate & " and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & gen & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'
 union all"
                            End If
 If leb<>Nothing Then
                                sSql += " select m.ORGID as ENTITY,f.ACCTID,(select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) as OPTTYPE ,(select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) as OPTSUBTYPE,(select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID) as OPTCAT , ACCTDESC,(case when f.ACCTTYPE='I' then 'Income statement' when f.ACCTTYPE='B' then 'Balance sheet' when f.ACCTTYPE='R' then 'Retairned Earning'  end) as ACCTTYPE,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & leb & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & " and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0) as BegBalance,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)>=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & leb & "glpost bb where bb.ACCTID=f.ACCTID  and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0) as Debit,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)<=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & leb & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as Credit ,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & leb & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as NetChange,coalesce((select SUM(coalesce(bb.TRANSAMT,0))  from  " & leb & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)+coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & leb & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & "  and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as EndingBalance from " & leb & "glamf f," & leb & "glpost p," & leb & "CSCOM m where m.ORGID=f.AUDTORG and 
  f.ACCTID =p.ACCTID and 
  p.ACCTID between '" & Trim(Txtfrmacct.Text) & "' and '" & Toacct & "' and p.DOCDATE <=  " & tdate & " and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & leb & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'
  union all"
                            End If

                            If ocj <> Nothing Then
                                sSql += " select m.ORGID as ENTITY,f.ACCTID,(select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) as OPTTYPE ,(select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) as OPTSUBTYPE,(select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID) as OPTCAT , ACCTDESC,(case when f.ACCTTYPE='I' then 'Income statement' when f.ACCTTYPE='B' then 'Balance sheet'  when f.ACCTTYPE='R' then 'Retairned Earning' end) as ACCTTYPE,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & ocj & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & " and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0) as BegBalance,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)>=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & ocj & "glpost bb where bb.ACCTID=f.ACCTID  and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0) as Debit,coalesce((select SUM(case when coalesce(bb.TRANSAMT,0)<=0 then coalesce(bb.TRANSAMT,0) else 0 end ) from  " & ocj & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as Credit ,coalesce((select SUM(coalesce(bb.TRANSAMT,0)) from  " & ocj & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as NetChange,coalesce((select SUM(coalesce(bb.TRANSAMT,0))  from  " & ocj & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE between " & fdate & " and " & tdate & " and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)+coalesce((select SUM(coalesce(bb.TRANSAMT,0) ) from  " & ocj & "glpost bb where bb.ACCTID=f.ACCTID and bb.DOCDATE <" & fdate & "  and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =bb.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'),0)  as EndingBalance from " & ocj & "glamf f," & ocj & "glpost p," & ocj & "CSCOM m where m.ORGID=f.AUDTORG and 
  f.ACCTID =p.ACCTID and 
  p.ACCTID between '" & Trim(Txtfrmacct.Text) & "' and '" & Toacct & "' and p.DOCDATE <=  " & tdate & " and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL1' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtftype.Text) & "' and N'" & Toopttype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL2' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfsubt.Text) & "' and N'" & Tooptsubtype & "' and (select top 1 [VALUE]  from " & ocj & "GLAMFO o where o.OPTFIELD='LEVEL3' and o.ACCTID =f.ACCTID ) between N'" & Trim(Txtfcat.Text) & "' and N'" & Tooptcat & "'
  union all"
                            End If
                            sSql = sSql.Substring(0, sSql.Length() - 9)

                            sSql +=") hh
  group by hh.ENTITY,hh.ACCTID,hh.OPTTYPE,hh.OPTSUBTYPE,hh.OPTCAT,hh.ACCTDESC,hh.ACCTTYPE
  order by hh.ENTITY,hh.ACCTID "





                            Dim cmd As New SqlCommand(sSql, conn)
                conn.Open()

                cmd.CommandTimeout = 0

                Dim sdrGetEmpDetails As SqlDataReader = cmd.ExecuteReader





                CType(excelBook.Sheets("Sheet1"), Excel.Worksheet).Name = "P&L"

                iRowCnt = 2


                If sdrGetEmpDetails.HasRows Then

                    xlAppToUpload.Visible = True

                    With xlWorkSheetToUpload


                                    .Cells(iRowCnt - 1, 1).Font.Size = 11
                                    .Cells(iRowCnt - 1, 1).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 1).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 1).value = "Entity"

                                    .Cells(iRowCnt - 1, 2).Font.Size = 11
                                    .Cells(iRowCnt - 1, 2).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 2).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 2).value = "Account Number"

                                    .Cells(iRowCnt - 1, 3).Font.Size = 11
                                    .Cells(iRowCnt - 1, 3).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 3).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 3).value = "Type"

                                    .Cells(iRowCnt - 1, 4).Font.Size = 11
                                    .Cells(iRowCnt - 1, 4).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 4).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 4).value = "Sub.Type"

                                    .Cells(iRowCnt - 1, 5).Font.Size = 11
                                    .Cells(iRowCnt - 1, 5).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 5).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 5).value = "Category"

                                    .Cells(iRowCnt - 1, 6).Font.Size = 11
                                    .Cells(iRowCnt - 1, 6).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 6).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 6).value = "Account Name"

                                    .Cells(iRowCnt - 1, 7).Font.Size = 11
                                    .Cells(iRowCnt - 1, 7).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 7).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 7).value = "Statement Type"

                                    .Cells(iRowCnt - 1, 8).Font.Size = 11
                                    .Cells(iRowCnt - 1, 8).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 8).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 8).value = "Beg Balance"

                                    .Cells(iRowCnt - 1, 9).Font.Size = 11
                                    .Cells(iRowCnt - 1, 9).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 9).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 9).value = "Debit"

                                    .Cells(iRowCnt - 1, 10).Font.Size = 11
                                    .Cells(iRowCnt - 1, 10).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 10).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 10).value = "Credit"

                                    .Cells(iRowCnt - 1, 11).Font.Size = 11
                                    .Cells(iRowCnt - 1, 11).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 11).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 11).value = "Net Change"

                                    .Cells(iRowCnt - 1, 12).Font.Size = 11
                                    .Cells(iRowCnt - 1, 12).EntireRow.Font.Bold = True
                                    .Cells(iRowCnt - 1, 12).Font.Name = "Arial"
                                    .Cells(iRowCnt - 1, 12).value = "Ending Balamce"





                                    iRowCnt = 1

                                    '''''



                                    While sdrGetEmpDetails.Read

                            .Cells(iRowCnt + 1, 1).value = sdrGetEmpDetails.Item("ENTITY")
                            .Cells(iRowCnt + 1, 2).value = sdrGetEmpDetails.Item("ACCTID")
                            .Cells(iRowCnt + 1, 3).value = sdrGetEmpDetails.Item("OPTTYPE")
                            .Cells(iRowCnt + 1, 4).value = sdrGetEmpDetails.Item("OPTSUBTYPE")
                            .Cells(iRowCnt + 1, 5).value = sdrGetEmpDetails.Item("OPTCAT")
                            .Cells(iRowCnt + 1, 6).value = sdrGetEmpDetails.Item("ACCTDESC")
                            .Cells(iRowCnt + 1, 7).value = sdrGetEmpDetails.Item("ACCTTYPE")
                            .Cells(iRowCnt + 1, 8).value = sdrGetEmpDetails.Item("BegBalance")
                            .Cells(iRowCnt + 1, 9).value = sdrGetEmpDetails.Item("Debit")
                            .Cells(iRowCnt + 1, 10).value = sdrGetEmpDetails.Item("Credit")
                            .Cells(iRowCnt + 1, 11).value = sdrGetEmpDetails.Item("NetChange")
                            .Cells(iRowCnt + 1, 12).value = sdrGetEmpDetails.Item("EndingBalance")
                            iRowCnt = iRowCnt + 1
                        End While

                    End With
                    conn.Close()

                End If





















            Catch ex As Exception
                MessageBox.Show("P&L:" & ex.Message)
            End Try

                Else
                    MessageBox.Show("P&L: From Date Greater than To Date")
                End If
                Else
                MessageBox.Show("P&L: From Account Number Greater than To Account Number")
            End If         
                End If
            Cursor.Current = Cursors.Default

            MessageBox.Show("P&L Export Finished")


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
