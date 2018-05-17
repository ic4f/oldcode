Public Class pageerror
    Inherits System.Web.UI.Page

    Protected ucLayoutHelper1 As LayoutHelper1
    Protected ucLayoutHelper2 As LayoutHelper2

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        ucLayoutHelper1.LoadContent()
        ucLayoutHelper2.LoadContent()
        MyBase.OnLoad(e)
    End Sub


End Class
