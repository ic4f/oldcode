Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace websiteorg

    Public Class pageGroups
        Inherits ba.BasePage

        Protected WithEvents dgrCats As cr.MyBaseGrid
        Protected pnlGrig As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrCats.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            dgrCats.Prefix = LinkPrefix
            Dim data As DataTable = New dt.PageCategoryData().GetCategoriesWithMenus()
            dgrCats.DataSource = data
            dgrCats.DataBind()
            If (data.Rows.Count > 0) Then
                pnlGrig.Visible = True
            Else
                pnlGrig.Visible = False
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 97
            End Get
        End Property

    End Class

End Namespace