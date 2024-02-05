Imports System.Linq
Imports System.Xml.Linq
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System
Imports acc = ACCPAC.Advantage
Imports System.Runtime.InteropServices

Friend Enum RecordType
        Manual
        Automatic
    End Enum

    Friend Class LookupItem
        Private ReadOnly _caption As String
        Friend ReadOnly Property ItemType As String
        Friend ReadOnly Property Field As String
        Friend ReadOnly Property Selection As String()
        Friend ReadOnly Property Hash As Dictionary(Of String, String)

        Friend Sub New(ByVal caption As String)
            _caption = caption
        End Sub

        Friend Sub New(ByVal caption As String, ByVal type As String, ByVal fld As String)
            _caption = caption
            ItemType = type
            Field = fld
        End Sub

        Friend Sub New(ByVal caption As String, ByVal type As String, ByVal fld As String, ByVal [select] As String)
            _caption = caption
            ItemType = type
            Field = fld
            Selection = [select].Split(","c)
        End Sub

        Friend Sub New(ByVal caption As String, ByVal type As String, ByVal fld As String, ByVal [select] As Dictionary(Of String, String))
            _caption = caption
            ItemType = type
            Field = fld
            Hash = [select]
        End Sub

        Public Overrides Function ToString() As String
            Return _caption
        End Function
    End Class

    Friend Class DataItem
        Private _keyValues As List(Of String) = New List(Of String)()
        Private ReadOnly _keyFields As String()
        Friend Property Data As XElement
        Friend Property Empty As Boolean = False
        Friend Property Index As Integer = 1
        Friend Property DataItemType As RecordType

        Friend Sub New()
        End Sub

        Friend Sub New(ByVal keyFields As String(), ByVal record As XElement)
            Dim tmp As XElement = record

            For Each s As String In keyFields
                tmp.Elements(s).ElementAt(0).Value = If(record.Elements(s).ElementAt(0).Value = "-", Index.ToString(), record.Elements(s).ElementAt(0).Value)
                _keyValues.Add(tmp.Elements(s).ElementAt(0).Value)
            Next

            Data = tmp
            _keyFields = keyFields
        End Sub

        Friend Sub New(ByVal keyFields As String(), ByVal record As XElement, ByVal index As Integer)
            index = index
            Dim tmp As XElement = record

            For Each s As String In keyFields
                tmp.Elements(s).ElementAt(0).Value = If(record.Elements(s).ElementAt(0).Value = "-", index.ToString(), record.Elements(s).ElementAt(0).Value)
                _keyValues.Add(tmp.Elements(s).ElementAt(0).Value)
            Next

            Data = tmp
            _keyFields = keyFields
        End Sub

        Friend Sub Update()
            _keyValues.Clear()

            For Each s As String In _keyFields
                _keyValues.Add(If(Data.Elements(s).ElementAt(0).Value = "-", Index.ToString(), Data.Elements(s).ElementAt(0).Value))
            Next
        End Sub

        Friend Function ToList() As List(Of String)
            Return _keyValues
        End Function

        Friend Function ToArray() As String()
            Return _keyValues.ToArray()
        End Function

        Friend Sub AddSerial(ByVal serial As String, ByVal kFld As String)
            Dim sr As XElement
            Dim srExists As Boolean = Data.Elements("serials").Any()

            If Not srExists Then
                sr = New XElement("serials")
            Else
                sr = Data.Elements("serials").FirstOrDefault()
            End If

            sr.Add(New XElement("serial", serial))
            If Not srExists Then Data.Add(sr)
            Data.Elements(kFld).ElementAt(0).SetValue(Integer.Parse(Data.Elements(kFld).ElementAt(0).Value) + 1)
        End Sub

        Friend Sub RemoveSerial(ByVal serial As String, ByVal kFld As String)
            Dim srExists As Boolean = Data.Elements("serials").Any()
            If Not srExists Then Return
            Data.Elements("serials").Descendants("serial").Where(Function(x) x.Value.Trim().Equals(serial)).Remove()
            Data.Elements(kFld).ElementAt(0).SetValue(Integer.Parse(Data.Elements(kFld).ElementAt(0).Value) - 1)
        End Sub
    End Class

    Public Class ERPCompany
        Friend Property Name As String
        Friend Property ID As String

        Friend Sub New(ByVal name As String, ByVal id As String)
        Me.Name = name
        Me.ID = id

    End Sub

        Public Overrides Function ToString() As String
        Return Name

    End Function
    End Class

    Friend Module Util
    Friend Sub FillErrors(ByVal e As Exception, ByVal s As acc.Session, <Out> ByRef ers As List(Of String))
        ers = New List(Of String)()

        If s.Errors Is Nothing Then

            If e.InnerException.Message Is Nothing Then
                ers.Add(e.Message)
            Else
                ers.Add(e.InnerException.Message.Trim())
            End If
        Else
            Dim eCnt As Long = s.Errors.Count

            If eCnt = 0 Then

                If e.Message.Trim() <> "" Then
                    ers.Add(e.Message.Trim())
                Else
                    ers.Add(e.InnerException.Message)
                End If
            Else

                For i As Integer = 0 To eCnt - 1
                    ers.Add(s.Errors(i).Message.Trim())
                Next
            End If
        End If
    End Sub

    Friend Function GetNativeSQL(ByVal entity As String, <Out> ByRef fields As String(), ByVal CHKBXRAM As String, ByVal CHKBXGEN As String, ByVal CHKBXJOR As String, ByVal CHKBXOCJ As String, ByVal CHKBXLEB As String, ByVal Optional setTo250 As Boolean = True) As String
        Dim rt As String = ""
        Dim f As List(Of String) = New List(Of String)()
        Dim ff As String = ""
        Dim fldsdoc As XElement = XElement.Load("vwfields.xml")
        Dim fl = From flds In fldsdoc.Elements(entity) Select flds

        For Each fld In fl.Descendants("field")
            ff += fld.Value.Split(","c)(0) & ","
            f.Add(fld.Value)
        Next

        fields = f.ToArray()

        Select Case entity
            Case "GLAMF"
                rt = If(setTo250 = False, "select distinct * from(select " & ff.Substring(0, ff.Length - 1) & "  from " & CHKBXRAM & "GLAMF union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMF union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMF union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMF union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXLEB & "GLAMF) hh order by hh.ACCTID", "select distinct * from(select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXRAM & "GLAMF union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMF union all select  top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMF union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMF union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXLEB & "GLAMF) hh order by hh.ACCTID")
            Case "OPTFDTYPE"
                rt = If(setTo250 = False, "select distinct * from(select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXRAM & "GLAMFO Where OPTFIELD='LEVEL1' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMFO Where OPTFIELD='LEVEL1' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMFO Where OPTFIELD='LEVEL1' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMFO Where OPTFIELD='LEVEL1' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXLEB & "GLAMFO Where OPTFIELD='LEVEL1') hh order by hh.[VALUE]", "select distinct * from(select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXRAM & "GLAMFO Where OPTFIELD='LEVEL1' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMFO Where OPTFIELD='LEVEL1' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMFO Where OPTFIELD='LEVEL1' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMFO Where OPTFIELD='LEVEL1' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXLEB & "GLAMFO Where OPTFIELD='LEVEL1') hh order by hh.[VALUE]")
            Case "OPTFDSUBTYPE"
                rt = If(setTo250 = False, "select distinct * from(select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXRAM & "GLAMFO Where OPTFIELD='LEVEL2' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMFO Where OPTFIELD='LEVEL2' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMFO Where OPTFIELD='LEVEL2' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMFO Where OPTFIELD='LEVEL2' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXLEB & "GLAMFO Where OPTFIELD='LEVEL2') hh order by hh.[VALUE]", "select distinct * from(select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXRAM & "GLAMFO Where OPTFIELD='LEVEL2' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMFO Where OPTFIELD='LEVEL2' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMFO Where OPTFIELD='LEVEL2' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMFO Where OPTFIELD='LEVEL2' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXLEB & "GLAMFO Where OPTFIELD='LEVEL2') hh order by hh.[VALUE]")
            Case "OPTFDCATEGORY"
                rt = If(setTo250 = False, "select distinct * from(select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXRAM & "GLAMFO Where OPTFIELD='LEVEL3' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMFO Where OPTFIELD='LEVEL3'  union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMFO Where OPTFIELD='LEVEL3' union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMFO Where OPTFIELD='LEVEL3'  union all select " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXLEB & "GLAMFO Where OPTFIELD='LEVEL3') hh order by hh.[VALUE]", "select distinct * from(select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXRAM & "GLAMFO Where OPTFIELD='LEVEL3' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXGEN & "GLAMFO Where OPTFIELD='LEVEL3' union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMFO Where OPTFIELD='LEVEL3'  union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXJOR & "GLAMFO Where OPTFIELD='LEVEL3'  union all select top 250 " & ff.Substring(0, ff.Length - 1) & " from " & CHKBXOCJ & "GLAMFO Where OPTFIELD='LEVEL3') hh order by hh.[VALUE]")
        End Select


        Return rt
    End Function
End Module



