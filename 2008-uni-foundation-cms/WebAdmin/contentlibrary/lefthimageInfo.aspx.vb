Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class lefthimageInfo
        Inherits ba.BasePage

        Protected ltrDescription As Literal
        Protected ltrId As Literal
        Protected ltrImage As Literal

        Protected pnlCanDelete As Panel
        Protected WithEvents btnDelete As Button

        Protected pnlDependencies As Panel

        Protected pnlDependMenus As Panel
        Protected ltrDependMenus As Literal
        Protected WithEvents rptDependMenus As Repeater

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnDelete.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete this header image?')"
            Dim myhimage As New dt.HeaderImage(GetId())
            BindInfo(myhimage)
            Dim hid As New dt.HeaderImageData
            If HasDependencies(myhimage, hid) Then
                pnlDependencies.Visible = True
            Else
                pnlCanDelete.Visible = True
            End If
        End Sub

        Private Sub BindInfo(ByVal myhimage As dt.HeaderImage)
            ltrDescription.Text = myhimage.Description
            ltrId.Text = myhimage.Id.ToString
            Dim url As String = ba.UrlHelper.GetHeaderImageUrlLeft(myhimage.Id)
            ltrImage.Text = "<img src='" + url + "'>"
        End Sub

        Private Function HasDependencies(ByVal myhimage As dt.HeaderImage, ByVal hid As dt.HeaderImageData) As Boolean
            Dim depends As Boolean = False

            Dim dtMenus As DataTable = hid.GetDependentMenus(myhimage.Id)
            Dim countMenus As Integer = dtMenus.Rows.Count
            If countMenus > 0 Then
                If countMenus = 1 Then
                    ltrDependMenus.Text = countMenus.ToString + " menus:"
                Else
                    ltrDependMenus.Text = countMenus.ToString + " menu:"
                End If
                rptDependMenus.DataSource = dtMenus
                rptDependMenus.DataBind()
                pnlDependMenus.Visible = True
                depends = True
            End If

            Return depends
        End Function

        Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            File.Delete(Server.MapPath(ba.UrlHelper.GetHeaderImageUrlLeft(GetId())))
            Dim hd As New dt.HeaderImageData
            hd.Delete(GetId())
            Response.Redirect("lefthimageList.aspx")
        End Sub

        Private Sub rptDependMenus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDependMenus.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrDependMenu As Literal = e.Item.FindControl("ltrDependMenu")
                Dim menuId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim text As String = DataBinder.Eval(e.Item.DataItem, "text")
                Dim type As String = DataBinder.Eval(e.Item.DataItem, "type")
                ltrDependMenu.Text = text + " [" + type + " / " + menuId + "]"
            End If
        End Sub

        Private Function GetId() As Integer
            Return Convert.ToInt32(Request("Id"))
        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 113
            End Get
        End Property

    End Class

End Namespace