﻿Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class AddFaculty
    Dim myconnection As New DTconnection
    Dim objdatapter As New MySqlDataAdapter
    Dim dtable As New DataTable

    Private Sub Subject_load()
        Dim READER As MySqlDataReader
        Try
            Dim query As String
            query = "SELECT * FROM cmsdbx.subject"
            Dim cmd As MySqlCommand
            cmd = New MySqlCommand(query, myconnection.open)
            READER = cmd.ExecuteReader
            While READER.Read
                Dim subName = READER.GetString("subjectname")
                FsubjectCB.Items.Add(subName)
            End While
            myconnection.close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub AddFaculty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Subject_load()
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timelable.Text = DateTime.Now.ToString()
    End Sub

    Private Sub FFirstNameTB_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyz"
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub FLastNameTB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FLastNameTB.KeyPress
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyz"
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub FDOBDateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles FDOBDateTimePicker.ValueChanged
        Dim ts As TimeSpan = DateTime.Now.Date - FDOBDateTimePicker.Value
        FAgeTB.Text = String.Format("{0:n0}", (ts.TotalDays / 365))
    End Sub

    Private Sub FPhoneTB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FPhoneTB.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                MessageBox.Show("Invalid Input ! Enter Number Only.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Handled = True
            End If
        End If

    End Sub
    Private Sub FPhoneTB_Validated(sender As Object, e As EventArgs) Handles FPhoneTB.Validated
        Dim dd As Integer
        dd = Len(FPhoneTB.Text)
        If (dd = 10) Then
            'Do nothing
        Else
            MessageBox.Show("Phone number should be 10 digit ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FPhoneTB.Clear()
        End If
    End Sub
    Private Sub FemailTB_Validating(sender As Object, e As CancelEventArgs) Handles FemailTB.Validating
        Dim pattern As String = "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$"
        Dim match As System.Text.RegularExpressions.Match = Regex.Match(FemailTB.Text.Trim(), pattern, RegexOptions.IgnoreCase)
        If (match.Success) Then
            'MessageBox.Show("Success", "Checking")
        Else
            MessageBox.Show("Please enter a valid Email ID", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FemailTB.Clear()
        End If
    End Sub

    Private Sub FPINCodeTB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FPINCodeTB.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                MessageBox.Show("Invalid Input ! Enter 6 digit PIN Only.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub FPINCodeTB_Validated(sender As Object, e As EventArgs) Handles FPINCodeTB.Validated
        Dim dd As Integer
        dd = Len(FPINCodeTB.Text)
        If (dd = 6) Then
            'Do nothing
        Else
            MessageBox.Show("PIN number should be 6 digit ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FPINCodeTB.Clear()
        End If
    End Sub
    Private Sub FExpTB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FExpTB.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                MessageBox.Show("Invalid Input ! Plese Enter Numbers Only.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub FExpTB_Validating(sender As Object, e As CancelEventArgs) Handles FExpTB.Validating
        Dim dd As Integer
        dd = Len(FExpTB.Text)
        If (dd <= 2) Then
            'Do nothing
        Else
            MessageBox.Show("Please enter numbers < 100 ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FExpTB.Clear()
        End If

    End Sub
    Private Sub FAgeTB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FAgeTB.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                MessageBox.Show("Invalid Input ! Plese Numbers < 100.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub FShowpass_CheckedChanged(sender As Object, e As EventArgs) Handles FShowpass.CheckedChanged
        If FShowpass.Checked = True Then
            FPasswordTB.UseSystemPasswordChar = False
        Else
            FPasswordTB.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub FacultyResetButton_Click(sender As Object, e As EventArgs) Handles FacultyResetButton.Click
        FLastNameTB.Clear()
        FFirstNameTB.Clear()
        FAgeTB.Clear()
        FPhoneTB.Clear()
        FemailTB.Clear()
        FAddressTB.Clear()
        FCityTB.Clear()
        FStateCB.SelectedIndex = -1
        FPINCodeTB.Clear()
        FQualiTB.Clear()
        FExpTB.Clear()
        FPasswordTB.Clear()
        FGenderComboBox.SelectedIndex = -1
    End Sub

    Private Sub FacultySaveButton_Click(sender As Object, e As EventArgs) Handles FacultySaveButton.Click
        Dim filename As String = OpenFileDialog1.FileName + ".jpg"
        Dim FileSize As UInt32
        Dim mstream As New System.IO.MemoryStream()
        PictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim arrImage() As Byte = mstream.GetBuffer()
        FileSize = mstream.Length
        mstream.Close()

        Try
            If FLastNameTB.Text = "" Or
            FFirstNameTB.Text = "" Or
            FAgeTB.Text = " " Or
            FPhoneTB.Text = "" Or
            FemailTB.Text = "" Or
            FAddressTB.Text = "" Or
            FCityTB.Text = "" Or
            FStateCB.Text = "" Or
            FPINCodeTB.Text = "" Or
            FQualiTB.Text = "" Or
            FExpTB.Text = "" Or
            FPasswordTB.Text = "" Then

                MessageBox.Show("Missing Information. Please fill all the fields ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else

                Dim query = "insert into faculties (facultyfirstname, facultylastname, gender, dob, age, contactnumber, emailid, address, city, state, pincode, qualification, experience, subjectname, password, activestatus, joindate, profilepic )values
                                               ('" & FFirstNameTB.Text & "','" & FLastNameTB.Text & "','" & FGenderComboBox.Text & "','" & FDOBDateTimePicker.Text & "','" & FAgeTB.Text & "','" & FPhoneTB.Text & "','" & FemailTB.Text & "','" & FAddressTB.Text & "', '" & FCityTB.Text & "','" & FStateCB.Text & "','" & FPINCodeTB.Text & "','" & FQualiTB.Text & "','" & FExpTB.Text & "','" & FsubjectCB.Text & "','" & FPasswordTB.Text & "','" & "Active" & "','" & timelable.Text & "', @ImageFile ) "

                Dim cmd As MySqlCommand
                cmd = New MySqlCommand(query, myconnection.open)
                cmd.Parameters.AddWithValue("@ImageFile", arrImage)
                cmd.ExecuteNonQuery()
                myconnection.close()
                MsgBox("Successfully Saved in database", MsgBoxStyle.Information, "Record Saved")
                FLastNameTB.Clear()
                FFirstNameTB.Clear()
                FAgeTB.Clear()
                FPhoneTB.Clear()
                FemailTB.Clear()
                FAddressTB.Clear()
                FCityTB.Clear()
                FStateCB.SelectedIndex = -1
                FPINCodeTB.Clear()
                FQualiTB.Clear()
                FExpTB.Clear()
                FPasswordTB.Clear()
                FGenderComboBox.SelectedIndex = -1
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Data Error")
        End Try
    End Sub

    Private Sub AddCloseButton1_Click(sender As Object, e As EventArgs) Handles AddCloseButton1.Click
        Me.Close()
    End Sub

    Private Sub PicSelectButton_Click(sender As Object, e As EventArgs) Handles PicSelectButton.Click
        Try
            With OpenFileDialog1
                'CHECK THE SELECTED FILE IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckFileExists = True

                'CHECK THE SELECTED PATH IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckPathExists = True

                'GET AND SET THE DEFAULT EXTENSION
                .DefaultExt = "jpg"

                'RETURN THE FILE LINKED TO THE LNK FILE
                .DereferenceLinks = True

                'SET THE FILE NAME TO EMPTY 
                .FileName = ""

                'FILTERING THE FILES
                .Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpg)|*.jpg|All files|*.*"
                'SET THIS FOR ONE FILE SELECTION ONLY.
                .Multiselect = False



                'SET THIS TO PUT THE CURRENT FOLDER BACK TO WHERE IT HAS STARTED.
                .RestoreDirectory = True

                'SET THE TITLE OF THE DIALOG BOX.
                .Title = "Select a file to open"

                'ACCEPT ONLY THE VALID WIN32 FILE NAMES.
                .ValidateNames = True

                If .ShowDialog = DialogResult.OK Then
                    Try
                        PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                    Catch fileException As Exception
                        Throw fileException
                    End Try
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub PicClearButton_Click(sender As Object, e As EventArgs) Handles PicClearButton.Click
        PictureBox1.Image = Nothing
    End Sub

End Class