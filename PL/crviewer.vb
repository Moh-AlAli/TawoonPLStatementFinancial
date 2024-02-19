Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Windows.Forms
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports acc = ACCPAC.Advantage
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox
Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports System.Windows.Forms.MonthCalendar
Imports DocumentFormat.OpenXml.Wordprocessing

Friend Class crviewer
    Private rdoc As New ReportDocument
    Private conrpt As New ConnectionInfo()
    Dim server As String = ""
    Dim uid As String = ""
    Dim pass As String = ""
    Dim db As String = ""
    Private ccompid As String
    Private ccompname As String

    Private cfacct As String
    Private ctacct As String
    Private cfdate As Integer
    Private ctdate As Integer
    Private crbram As Boolean
    Private crbgen As Boolean
    Private crbjor As Boolean
    Private crbocj As Boolean
    Private crbleb As Boolean
    Private cftyp As String
    Private cttyp As String
    Private cfsubt As String
    Private ctsubt As String
    Private cfcat As String
    Private ctcat As String


    Friend Property ObjectHandle As String
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
        Dim path As String = ("txtcon\welfcon.txt")
        Dim sr As New StreamReader(path)

        server = sr.ReadLine()
        db = sr.ReadLine()
        uid = sr.ReadLine()
        pass = sr.ReadLine()


        server = Decryption(server, secretkey)
        uid = Decryption(uid, secretkey)
        pass = Decryption(pass, secretkey)

        Dim cons As String = "" '"Data Source =" & server & "; DataBase =" & ccompid & "; User Id =" & uid & "; Password =" & pass & ";"


        Return cons
    End Function
    Public Sub New(ByVal _objectHandle As String, ByVal _sess As acc.Session, ByVal facct As String, ByVal tacct As String, ByVal fdate As Integer, ByVal tdate As Integer, ByVal rbram As Boolean, ByVal rbgen As Boolean, ByVal rbjor As Boolean, ByVal rbocj As Boolean, ByVal rbleb As Boolean, ByVal opttype As String, ByVal optsubt As String, ByVal optcat As String, ByVal toopttype As String, ByVal tooptsubt As String, ByVal tooptcat As String)
        InitializeComponent()
        ObjectHandle = _objectHandle
        ccompid = _sess.CompanyID
        ccompname = _sess.CompanyName
        cfacct = facct
        ctacct = tacct
        cfdate = fdate
        ctdate = tdate

        crbram = rbram
        crbgen = rbgen
        crbjor = rbjor
        crbocj = rbocj
        crbleb = rbleb
        cftyp = opttype
        cfsubt = optsubt
        cfcat = optcat


        cttyp = toopttype
        ctsubt = tooptsubt
        ctcat = tooptcat


    End Sub

    Public Sub New(ByVal _objectHandle As String)
        InitializeComponent()
        ObjectHandle = _objectHandle
    End Sub

    Private Sub crviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim cwvr As New CrystalReportViewer
            cwvr.Dock = DockStyle.Fill
            cwvr.BorderStyle = BorderStyle.None
            cwvr.ExportReport()
            Me.Controls.Add(cwvr)

            Dim entity As String = ""
            If crbram = True Then
                entity = entity + "'RAMDAT',"
            End If
            If crbgen = True Then
                entity = entity + "'GENDAT',"
            End If
            If crbjor = True Then
                entity = entity + "'JORDAT',"
            End If
            If crbocj = True Then
                entity = entity + "'OCJDAT',"
            End If
            If crbleb = True Then
                entity = entity + "'LEBDAT',"
            End If


            entity = entity.Substring(0, entity.Length() - 1)
            If entity <> "" Then
                rdoc.Load("reports\PL.rpt")

                Dim tabs As Tables = rdoc.Database.Tables
                Dim parv As New ParameterValues
                Dim dis As New ParameterDiscreteValue


                Readconnectionstring()
                For Each TAB As CrystalDecisions.CrystalReports.Engine.Table In tabs
                    Dim tablog As TableLogOnInfo = TAB.LogOnInfo
                    conrpt.ServerName = server
                    conrpt.DatabaseName = ccompid
                    conrpt.UserID = uid
                    conrpt.Password = pass
                    tablog.ConnectionInfo = conrpt
                    TAB.ApplyLogOnInfo(tablog)
                Next


                rdoc.SetParameterValue("afacct", "'" & cfacct & "'")
                rdoc.SetParameterValue("atacct", "'" & ctacct & "'")

                rdoc.SetParameterValue("bfdate", cfdate)
                rdoc.SetParameterValue("btdate", ctdate)

                rdoc.SetParameterValue("cfopttype", "'" & cftyp & "'")
                rdoc.SetParameterValue("ctopttype", "'" & cttyp & "'")

                rdoc.SetParameterValue("dfoptsubt", "'" & cfsubt & "'")
                rdoc.SetParameterValue("dtoptsubt", "'" & ctsubt & "'")

                rdoc.SetParameterValue("efoptcat", "'" & cfcat & "'")
                rdoc.SetParameterValue("etoptcat", "'" & ctcat & "'")


                rdoc.SetParameterValue("fentity", " Where hh.Entity in(" & entity & ")")

                cwvr.ReportSource = rdoc
            Else

                MessageBox.Show("Choose one entity at least !")
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message)
            If rdoc Is Nothing Then
                rdoc.Close()
                rdoc.Dispose()
            End If
        End Try
    End Sub


End Class



