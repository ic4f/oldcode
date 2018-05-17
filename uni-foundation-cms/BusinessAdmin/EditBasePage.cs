using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Foundation.BusinessAdmin
{
	public abstract class EditBasePage : AddEditBasePage
	{
		protected override string Message { get { return "updated"; } }	

		protected virtual int GetId() {  return Convert.ToInt32(Request["Id"]); }
	}
}
