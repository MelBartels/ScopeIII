Public Class FrmTestListView

    Private Sub FrmTestListView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    ' problem with drag and drop is that it requires STA but windows application framework starts in MTA;
    ' starting a new thread with STA results in OS Loader Lock MDA exception

    'Private Sub ListViewItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lv.ItemDrag
    '    lv.DoDragDrop(e.Item, DragDropEffects.Move)
    'End Sub

    'Private Sub ListViewDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lv.DragEnter
    '    e.Effect = DragDropEffects.Move
    'End Sub

    'Private Sub ListViewDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lv.DragDrop
    '    Dim fromItem As ListViewItem
    '    Dim toItem As ListViewItem
    '    Dim toLv As ListView = CType(sender, ListView)
    '    Dim x As Integer = toLv.PointToClient(New System.Drawing.Point(e.X, e.Y)).X
    '    Dim y As Integer = toLv.PointToClient(New System.Drawing.Point(e.X, e.Y)).Y
    '    ' make sure we only handle listViewItem drag and drops
    '    If e.Data.GetDataPresent("System.Windows.Forms.ListViewItem", False) Then
    '        fromItem = CType(e.Data.GetData("System.Windows.Forms.ListViewItem"), ListViewItem)
    '        toItem = CType(sender, ListView).GetItemAt(x, y)
    '        toLv.Items.Insert(toItem.Index, CType(fromItem.Clone, ListViewItem))
    '        fromItem.Remove()
    '    End If
    'End Sub

    ' replace with...

    Private mouseDragging As Boolean
    Private draggingItem As ListViewItem
    Private targetItem As ListViewItem

    Private Sub ListView1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDown
        Dim lvHitTestInfo As ListViewHitTestInfo = CType(sender, ListView).HitTest(e.X, e.Y)
        draggingItem = lvHitTestInfo.Item

        If draggingItem IsNot Nothing Then
            'Debug.WriteLine(draggingItem.Text)
            mouseDragging = True
        End If
    End Sub

    Private Sub ListView1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseMove
        If mouseDragging Then
            If CType(CType(sender, ListView).HitTest(e.X, e.Y), ListViewHitTestInfo).Item Is Nothing Then
                Me.Cursor = System.Windows.Forms.Cursors.No
            Else
                Me.Cursor = System.Windows.Forms.Cursors.UpArrow
            End If
        End If
    End Sub

    Private Sub ListView1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
        If mouseDragging Then
            Dim lvHitTestInfo As ListViewHitTestInfo = CType(sender, ListView).HitTest(e.X, e.Y)
            targetItem = lvHitTestInfo.Item

            If targetItem IsNot Nothing Then
                'Debug.WriteLine(targetItem.Text)
                CType(sender, ListView).Items.Insert(targetItem.Index, CType(draggingItem.Clone, ListViewItem))
                draggingItem.Remove()
            End If

            mouseDragging = False
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If
    End Sub

End Class