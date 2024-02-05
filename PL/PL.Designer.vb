<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PL
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PL))
        Me.CMDClose = New System.Windows.Forms.Button()
        Me.butexpexc = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.ChLEBDAT = New System.Windows.Forms.CheckBox()
        Me.ChOCJDAT = New System.Windows.Forms.CheckBox()
        Me.ChJORDAT = New System.Windows.Forms.CheckBox()
        Me.ChGENDAT = New System.Windows.Forms.CheckBox()
        Me.ChRAMDAT = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Buttcat = New System.Windows.Forms.Button()
        Me.Txttcat = New System.Windows.Forms.TextBox()
        Me.Butfcat = New System.Windows.Forms.Button()
        Me.Txtfcat = New System.Windows.Forms.TextBox()
        Me.Buttsubt = New System.Windows.Forms.Button()
        Me.Txttsubt = New System.Windows.Forms.TextBox()
        Me.Butfsubt = New System.Windows.Forms.Button()
        Me.Txtfsubt = New System.Windows.Forms.TextBox()
        Me.Butttype = New System.Windows.Forms.Button()
        Me.Txtttype = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Butftype = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txtftype = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btfind = New System.Windows.Forms.Button()
        Me.Txttoacct = New System.Windows.Forms.TextBox()
        Me.bffind = New System.Windows.Forms.Button()
        Me.Txtfrmacct = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CMDClose
        '
        Me.CMDClose.BackColor = System.Drawing.SystemColors.Control
        Me.CMDClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CMDClose.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CMDClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CMDClose.Location = New System.Drawing.Point(288, 566)
        Me.CMDClose.Name = "CMDClose"
        Me.CMDClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMDClose.Size = New System.Drawing.Size(81, 25)
        Me.CMDClose.TabIndex = 8
        Me.CMDClose.Text = "Exit"
        Me.CMDClose.UseVisualStyleBackColor = False
        '
        'butexpexc
        '
        Me.butexpexc.BackColor = System.Drawing.SystemColors.Control
        Me.butexpexc.Cursor = System.Windows.Forms.Cursors.Default
        Me.butexpexc.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.butexpexc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.butexpexc.Location = New System.Drawing.Point(5, 566)
        Me.butexpexc.Name = "butexpexc"
        Me.butexpexc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.butexpexc.Size = New System.Drawing.Size(105, 25)
        Me.butexpexc.TabIndex = 7
        Me.butexpexc.Text = "Export To Excel"
        Me.butexpexc.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ChLEBDAT)
        Me.GroupBox4.Controls.Add(Me.ChOCJDAT)
        Me.GroupBox4.Controls.Add(Me.ChJORDAT)
        Me.GroupBox4.Controls.Add(Me.ChGENDAT)
        Me.GroupBox4.Controls.Add(Me.ChRAMDAT)
        Me.GroupBox4.Font = New System.Drawing.Font("Arial Unicode MS", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(5, 7)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(371, 112)
        Me.GroupBox4.TabIndex = 36
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Entity"
        '
        'ChLEBDAT
        '
        Me.ChLEBDAT.AutoSize = True
        Me.ChLEBDAT.Location = New System.Drawing.Point(40, 79)
        Me.ChLEBDAT.Name = "ChLEBDAT"
        Me.ChLEBDAT.Size = New System.Drawing.Size(88, 23)
        Me.ChLEBDAT.TabIndex = 4
        Me.ChLEBDAT.Text = "LEBDAT"
        Me.ChLEBDAT.UseVisualStyleBackColor = True
        '
        'ChOCJDAT
        '
        Me.ChOCJDAT.AutoSize = True
        Me.ChOCJDAT.Location = New System.Drawing.Point(254, 50)
        Me.ChOCJDAT.Name = "ChOCJDAT"
        Me.ChOCJDAT.Size = New System.Drawing.Size(90, 23)
        Me.ChOCJDAT.TabIndex = 3
        Me.ChOCJDAT.Text = "OCJDAT"
        Me.ChOCJDAT.UseVisualStyleBackColor = True
        '
        'ChJORDAT
        '
        Me.ChJORDAT.AutoSize = True
        Me.ChJORDAT.Location = New System.Drawing.Point(40, 50)
        Me.ChJORDAT.Name = "ChJORDAT"
        Me.ChJORDAT.Size = New System.Drawing.Size(90, 23)
        Me.ChJORDAT.TabIndex = 2
        Me.ChJORDAT.Text = "JORDAT"
        Me.ChJORDAT.UseVisualStyleBackColor = True
        '
        'ChGENDAT
        '
        Me.ChGENDAT.AutoSize = True
        Me.ChGENDAT.Location = New System.Drawing.Point(254, 21)
        Me.ChGENDAT.Name = "ChGENDAT"
        Me.ChGENDAT.Size = New System.Drawing.Size(92, 23)
        Me.ChGENDAT.TabIndex = 1
        Me.ChGENDAT.Text = "GENDAT"
        Me.ChGENDAT.UseVisualStyleBackColor = True
        '
        'ChRAMDAT
        '
        Me.ChRAMDAT.AutoSize = True
        Me.ChRAMDAT.Location = New System.Drawing.Point(40, 21)
        Me.ChRAMDAT.Name = "ChRAMDAT"
        Me.ChRAMDAT.Size = New System.Drawing.Size(93, 23)
        Me.ChRAMDAT.TabIndex = 0
        Me.ChRAMDAT.Text = "RAMDAT"
        Me.ChRAMDAT.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Buttcat)
        Me.GroupBox5.Controls.Add(Me.Txttcat)
        Me.GroupBox5.Controls.Add(Me.Butfcat)
        Me.GroupBox5.Controls.Add(Me.Txtfcat)
        Me.GroupBox5.Controls.Add(Me.Buttsubt)
        Me.GroupBox5.Controls.Add(Me.Txttsubt)
        Me.GroupBox5.Controls.Add(Me.Butfsubt)
        Me.GroupBox5.Controls.Add(Me.Txtfsubt)
        Me.GroupBox5.Controls.Add(Me.Butttype)
        Me.GroupBox5.Controls.Add(Me.Txtttype)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.Butftype)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.Txtftype)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Font = New System.Drawing.Font("Arial Unicode MS", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(5, 322)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(371, 216)
        Me.GroupBox5.TabIndex = 73
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Optional Fileds"
        '
        'Buttcat
        '
        Me.Buttcat.BackColor = System.Drawing.SystemColors.Control
        Me.Buttcat.Cursor = System.Windows.Forms.Cursors.Default
        Me.Buttcat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Buttcat.Image = CType(resources.GetObject("Buttcat.Image"), System.Drawing.Image)
        Me.Buttcat.Location = New System.Drawing.Point(346, 177)
        Me.Buttcat.Name = "Buttcat"
        Me.Buttcat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Buttcat.Size = New System.Drawing.Size(17, 20)
        Me.Buttcat.TabIndex = 42
        Me.Buttcat.TabStop = False
        Me.Buttcat.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Buttcat.UseVisualStyleBackColor = False
        '
        'Txttcat
        '
        Me.Txttcat.Location = New System.Drawing.Point(240, 177)
        Me.Txttcat.Margin = New System.Windows.Forms.Padding(4)
        Me.Txttcat.Multiline = True
        Me.Txttcat.Name = "Txttcat"
        Me.Txttcat.Size = New System.Drawing.Size(119, 20)
        Me.Txttcat.TabIndex = 41
        '
        'Butfcat
        '
        Me.Butfcat.BackColor = System.Drawing.SystemColors.Control
        Me.Butfcat.Cursor = System.Windows.Forms.Cursors.Default
        Me.Butfcat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Butfcat.Image = CType(resources.GetObject("Butfcat.Image"), System.Drawing.Image)
        Me.Butfcat.Location = New System.Drawing.Point(197, 177)
        Me.Butfcat.Name = "Butfcat"
        Me.Butfcat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Butfcat.Size = New System.Drawing.Size(17, 20)
        Me.Butfcat.TabIndex = 39
        Me.Butfcat.TabStop = False
        Me.Butfcat.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Butfcat.UseVisualStyleBackColor = False
        '
        'Txtfcat
        '
        Me.Txtfcat.Location = New System.Drawing.Point(79, 177)
        Me.Txtfcat.Margin = New System.Windows.Forms.Padding(4)
        Me.Txtfcat.Multiline = True
        Me.Txtfcat.Name = "Txtfcat"
        Me.Txtfcat.Size = New System.Drawing.Size(119, 20)
        Me.Txtfcat.TabIndex = 40
        '
        'Buttsubt
        '
        Me.Buttsubt.BackColor = System.Drawing.SystemColors.Control
        Me.Buttsubt.Cursor = System.Windows.Forms.Cursors.Default
        Me.Buttsubt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Buttsubt.Image = CType(resources.GetObject("Buttsubt.Image"), System.Drawing.Image)
        Me.Buttsubt.Location = New System.Drawing.Point(346, 111)
        Me.Buttsubt.Name = "Buttsubt"
        Me.Buttsubt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Buttsubt.Size = New System.Drawing.Size(17, 20)
        Me.Buttsubt.TabIndex = 38
        Me.Buttsubt.TabStop = False
        Me.Buttsubt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Buttsubt.UseVisualStyleBackColor = False
        '
        'Txttsubt
        '
        Me.Txttsubt.Location = New System.Drawing.Point(240, 111)
        Me.Txttsubt.Margin = New System.Windows.Forms.Padding(4)
        Me.Txttsubt.Multiline = True
        Me.Txttsubt.Name = "Txttsubt"
        Me.Txttsubt.Size = New System.Drawing.Size(119, 20)
        Me.Txttsubt.TabIndex = 37
        '
        'Butfsubt
        '
        Me.Butfsubt.BackColor = System.Drawing.SystemColors.Control
        Me.Butfsubt.Cursor = System.Windows.Forms.Cursors.Default
        Me.Butfsubt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Butfsubt.Image = CType(resources.GetObject("Butfsubt.Image"), System.Drawing.Image)
        Me.Butfsubt.Location = New System.Drawing.Point(197, 111)
        Me.Butfsubt.Name = "Butfsubt"
        Me.Butfsubt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Butfsubt.Size = New System.Drawing.Size(17, 20)
        Me.Butfsubt.TabIndex = 35
        Me.Butfsubt.TabStop = False
        Me.Butfsubt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Butfsubt.UseVisualStyleBackColor = False
        '
        'Txtfsubt
        '
        Me.Txtfsubt.Location = New System.Drawing.Point(79, 111)
        Me.Txtfsubt.Margin = New System.Windows.Forms.Padding(4)
        Me.Txtfsubt.Multiline = True
        Me.Txtfsubt.Name = "Txtfsubt"
        Me.Txtfsubt.Size = New System.Drawing.Size(119, 20)
        Me.Txtfsubt.TabIndex = 36
        '
        'Butttype
        '
        Me.Butttype.BackColor = System.Drawing.SystemColors.Control
        Me.Butttype.Cursor = System.Windows.Forms.Cursors.Default
        Me.Butttype.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Butttype.Image = CType(resources.GetObject("Butttype.Image"), System.Drawing.Image)
        Me.Butttype.Location = New System.Drawing.Point(347, 54)
        Me.Butttype.Name = "Butttype"
        Me.Butttype.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Butttype.Size = New System.Drawing.Size(17, 20)
        Me.Butttype.TabIndex = 34
        Me.Butttype.TabStop = False
        Me.Butttype.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Butttype.UseVisualStyleBackColor = False
        '
        'Txtttype
        '
        Me.Txtttype.Location = New System.Drawing.Point(241, 54)
        Me.Txtttype.Margin = New System.Windows.Forms.Padding(4)
        Me.Txtttype.Multiline = True
        Me.Txtttype.Name = "Txtttype"
        Me.Txtttype.Size = New System.Drawing.Size(119, 20)
        Me.Txtttype.TabIndex = 33
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(284, 22)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 15)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "To"
        '
        'Butftype
        '
        Me.Butftype.BackColor = System.Drawing.SystemColors.Control
        Me.Butftype.Cursor = System.Windows.Forms.Cursors.Default
        Me.Butftype.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Butftype.Image = CType(resources.GetObject("Butftype.Image"), System.Drawing.Image)
        Me.Butftype.Location = New System.Drawing.Point(198, 54)
        Me.Butftype.Name = "Butftype"
        Me.Butftype.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Butftype.Size = New System.Drawing.Size(17, 20)
        Me.Butftype.TabIndex = 30
        Me.Butftype.TabStop = False
        Me.Butftype.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Butftype.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(123, 22)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 15)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "From"
        '
        'Txtftype
        '
        Me.Txtftype.Location = New System.Drawing.Point(80, 54)
        Me.Txtftype.Margin = New System.Windows.Forms.Padding(4)
        Me.Txtftype.Multiline = True
        Me.Txtftype.Name = "Txtftype"
        Me.Txtftype.Size = New System.Drawing.Size(119, 20)
        Me.Txtftype.TabIndex = 30
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 182)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 15)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Category"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 116)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 15)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Sub.Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 54)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 15)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Type"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btfind)
        Me.GroupBox2.Controls.Add(Me.Txttoacct)
        Me.GroupBox2.Controls.Add(Me.bffind)
        Me.GroupBox2.Controls.Add(Me.Txtfrmacct)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial Unicode MS", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(5, 127)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(371, 98)
        Me.GroupBox2.TabIndex = 36
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Account Number"
        '
        'btfind
        '
        Me.btfind.BackColor = System.Drawing.SystemColors.Control
        Me.btfind.Cursor = System.Windows.Forms.Cursors.Default
        Me.btfind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btfind.Image = CType(resources.GetObject("btfind.Image"), System.Drawing.Image)
        Me.btfind.Location = New System.Drawing.Point(343, 62)
        Me.btfind.Name = "btfind"
        Me.btfind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btfind.Size = New System.Drawing.Size(17, 20)
        Me.btfind.TabIndex = 29
        Me.btfind.TabStop = False
        Me.btfind.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btfind.UseVisualStyleBackColor = False
        '
        'Txttoacct
        '
        Me.Txttoacct.Location = New System.Drawing.Point(83, 62)
        Me.Txttoacct.Margin = New System.Windows.Forms.Padding(4)
        Me.Txttoacct.Multiline = True
        Me.Txttoacct.Name = "Txttoacct"
        Me.Txttoacct.Size = New System.Drawing.Size(265, 20)
        Me.Txttoacct.TabIndex = 3
        '
        'bffind
        '
        Me.bffind.BackColor = System.Drawing.SystemColors.Control
        Me.bffind.Cursor = System.Windows.Forms.Cursors.Default
        Me.bffind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.bffind.Image = CType(resources.GetObject("bffind.Image"), System.Drawing.Image)
        Me.bffind.Location = New System.Drawing.Point(343, 25)
        Me.bffind.Name = "bffind"
        Me.bffind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.bffind.Size = New System.Drawing.Size(17, 20)
        Me.bffind.TabIndex = 27
        Me.bffind.TabStop = False
        Me.bffind.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.bffind.UseVisualStyleBackColor = False
        '
        'Txtfrmacct
        '
        Me.Txtfrmacct.Location = New System.Drawing.Point(83, 26)
        Me.Txtfrmacct.Margin = New System.Windows.Forms.Padding(4)
        Me.Txtfrmacct.Multiline = True
        Me.Txtfrmacct.Name = "Txtfrmacct"
        Me.Txtfrmacct.Size = New System.Drawing.Size(265, 20)
        Me.Txtfrmacct.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(10, 60)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(22, 15)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "To"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(10, 29)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 15)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "From"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial Unicode MS", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(5, 231)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(371, 89)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(80, 51)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(282, 25)
        Me.DateTimePicker2.TabIndex = 11
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(79, 22)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(285, 25)
        Me.DateTimePicker1.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 59)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "To"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial Unicode MS", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 32)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "From"
        '
        'PL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(386, 600)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.CMDClose)
        Me.Controls.Add(Me.butexpexc)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PL"
        Me.Text = "PL"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents CMDClose As Button
    Public WithEvents butexpexc As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents ChLEBDAT As CheckBox
    Friend WithEvents ChOCJDAT As CheckBox
    Friend WithEvents ChJORDAT As CheckBox
    Friend WithEvents ChGENDAT As CheckBox
    Friend WithEvents ChRAMDAT As CheckBox
    Friend WithEvents GroupBox5 As GroupBox
    Public WithEvents Buttcat As Button
    Friend WithEvents Txttcat As TextBox
    Public WithEvents Butfcat As Button
    Friend WithEvents Txtfcat As TextBox
    Public WithEvents Buttsubt As Button
    Friend WithEvents Txttsubt As TextBox
    Public WithEvents Butfsubt As Button
    Friend WithEvents Txtfsubt As TextBox
    Public WithEvents Butttype As Button
    Friend WithEvents Txtttype As TextBox
    Friend WithEvents Label9 As Label
    Public WithEvents Butftype As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Txtftype As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Public WithEvents btfind As Button
    Friend WithEvents Txttoacct As TextBox
    Public WithEvents bffind As Button
    Friend WithEvents Txtfrmacct As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
End Class
