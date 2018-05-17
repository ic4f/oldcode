using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using b = Ei.Business;
using d = Ei.Data;
using cc = Core.Cms;

namespace Ei.Website.lineupadmin
{
    public class popupLineupAdmin : System.Web.UI.Page
    {
        protected Label lblLineupViewId;
        protected Label lblCurrentPhotoIndex;
        protected Label lblCurrentPhotoId;
        protected Label lblCurrentPhotoIsSuspect;
        protected Label lblSuspectIsProcessed;

        protected Panel pnlBeginLineup;
        protected TextBox tbxFirstName;
        protected TextBox tbxLastName;
        protected RequiredFieldValidator valFirstName;
        protected RequiredFieldValidator valLastName;
        protected Button btnBeginLineup;

        protected Panel pnlInstructions;
        protected Button btnUnderstand;
        protected Button btnNotUnderstand;

        protected Panel pnlPhotoView;
        protected Label lblPhoto;
        protected Button btnFinishedLooking;

        protected Panel pnlPhotoViewResult;
        protected Button btnYes;
        protected Button btnNotSure;
        protected Button btnNo;

        protected Panel pnlIdentificationConfirmation;
        protected TextBox tbxIdentificationConfirmation;
        protected RequiredFieldValidator valIdentificationConfirmation;
        protected Button btnIdentificationConfirmation;

        protected Panel pnlContinueLineup;
        protected Button btnContinueLineup;

        protected Panel pnlFinalComments;
        protected TextBox tbxFinalComments;
        protected RequiredFieldValidator valFinalComments;
        protected Button btnFinalComments;

        protected Panel pnlFinishLineup;
        protected Button btnFinishLineup;

        private cc.SitePrincipal principal;
        private cc.SiteIdentity identity;
        private static int RESULT_NO = 0;
        private static int RESULT_YES = 1;
        private static int RESULT_NOTSURE = 2;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            btnNotUnderstand.Attributes.Add("onClick", "javascript:alert('Please let the lineup administrator know that you do not understand the procedure and the instructions');");
            btnFinishLineup.Attributes.Add("onClick", "javascript:window.close();");
        }

        private int LineupViewId { get { return Convert.ToInt32(lblLineupViewId.Text); } }

        private int CurrentPhotoIndex
        {
            set { lblCurrentPhotoIndex.Text = value.ToString(); }
            get { return Convert.ToInt32(lblCurrentPhotoIndex.Text); }
        }

        private int CurrentPhotoId
        {
            set { lblCurrentPhotoId.Text = value.ToString(); }
            get { return Convert.ToInt32(lblCurrentPhotoId.Text); }
        }

        private bool CurrentPhotoIsSuspect
        {
            set { lblCurrentPhotoIsSuspect.Text = value.ToString(); }
            get { return Convert.ToBoolean(lblCurrentPhotoIsSuspect.Text); }
        }

        private bool SuspectIsProcessed
        {
            set { lblSuspectIsProcessed.Text = value.ToString(); }
            get { return Convert.ToBoolean(lblSuspectIsProcessed.Text); }
        }


        private int LineupId { get { return Convert.ToInt32(Request["Id"]); } }

        private void btnBeginLineup_Click(object sender, System.EventArgs e)
        {
            if (valFirstName.IsValid && valLastName.IsValid)
            {
                pnlBeginLineup.Visible = false;
                pnlInstructions.Visible = true;

                d.LineupViewData lvd = new d.LineupViewData();
                int lineupViewId = lvd.Create(LineupId, tbxFirstName.Text.Trim(), tbxLastName.Text.Trim(), identity.UserId);
                lblLineupViewId.Text = lineupViewId.ToString();
            }
        }

        private void btnUnderstand_Click(object sender, System.EventArgs e)
        {
            displayNextPhoto();
        }

        private void btnFinishedLooking_Click(object sender, System.EventArgs e)
        {
            pnlPhotoView.Visible = false;
            pnlPhotoViewResult.Visible = true;
        }

        private void btnYes_Click(object sender, System.EventArgs e)
        {
            pnlPhotoViewResult.Visible = false;
            pnlIdentificationConfirmation.Visible = true;
        }

        private void btnNo_Click(object sender, System.EventArgs e)
        {
            processPhotoViewResult(RESULT_NO);
            displayNextPhoto();
        }

        private void btnNotSure_Click(object sender, System.EventArgs e)
        {
            processPhotoViewResult(RESULT_NOTSURE);
            displayNextPhoto();
        }

        private void btnIdentificationConfirmation_Click(object sender, System.EventArgs e)
        {
            if (valIdentificationConfirmation.IsValid)
            {
                processPhotoViewResult(RESULT_YES);
                pnlIdentificationConfirmation.Visible = false;
                pnlContinueLineup.Visible = true;
            }
        }

        private void btnContinueLineup_Click(object sender, System.EventArgs e)
        {
            displayNextPhoto();
        }

        private void btnFinalComments_Click(object sender, System.EventArgs e)
        {
            if (valFinalComments.IsValid)
            {
                d.LineupViewData lvd = new d.LineupViewData();
                lvd.Complete(LineupViewId, tbxFinalComments.Text.Trim());

                pnlFinalComments.Visible = false;
                pnlFinishLineup.Visible = true;
            }
        }

        private void displayNextPhoto()
        {
            pnlInstructions.Visible = false;
            pnlPhotoView.Visible = true;
            pnlPhotoViewResult.Visible = false;
            pnlIdentificationConfirmation.Visible = false;
            pnlContinueLineup.Visible = false;
            loadPhoto();
        }

        private void loadPhoto()
        {
            d.Lineup lineup = new d.Lineup(LineupId);
            DataTable dtPhotos = new d.PhotoData().GetPhotosByLineup(LineupId);
            int lastPhotoPosition = dtPhotos.Rows.Count - 1;
            if (CurrentPhotoIndex > lastPhotoPosition && SuspectIsProcessed)
            {
                pnlPhotoView.Visible = false;
                pnlFinalComments.Visible = true;
            }
            else
            {
                if (!SuspectIsProcessed && lineup.SuspectPhotoPosition == CurrentPhotoIndex)
                {
                    lblPhoto.Text = "<img src='" + b.UrlHelper.GetSuspectImageUrl(lineup.SuspectId, b.ImageHelper.LARGE_PHOTO_SUFFIX) + "' border='1'>";
                    CurrentPhotoId = -1;
                    CurrentPhotoIsSuspect = true;
                }
                else
                {
                    int photoId = Convert.ToInt32(dtPhotos.Rows[CurrentPhotoIndex][0]);
                    lblPhoto.Text = "<img src='" + b.UrlHelper.GetPhotoImageUrl(photoId, b.ImageHelper.LARGE_PHOTO_SUFFIX) + "' border='1'>";
                    CurrentPhotoIndex++;
                    CurrentPhotoId = photoId;
                    CurrentPhotoIsSuspect = false;
                }
            }
        }

        private void processPhotoViewResult(int resultcode)
        {
            string certainty = tbxIdentificationConfirmation.Text.Trim();
            tbxIdentificationConfirmation.Text = "";

            if (CurrentPhotoIsSuspect)
            {
                new d.PhotoViewData().CreateSuspectView(LineupViewId, resultcode, certainty);
                SuspectIsProcessed = true;
            }
            else
                new d.PhotoViewData().CreatePhotoView(LineupViewId, CurrentPhotoId, resultcode, certainty);
        }

        override protected void OnInit(EventArgs e)
        {
            this.btnBeginLineup.Click += new EventHandler(btnBeginLineup_Click);
            this.btnUnderstand.Click += new EventHandler(btnUnderstand_Click);
            this.btnFinishedLooking.Click += new EventHandler(btnFinishedLooking_Click);
            this.btnYes.Click += new EventHandler(btnYes_Click);
            this.btnNo.Click += new EventHandler(btnNo_Click);
            this.btnNotSure.Click += new EventHandler(btnNotSure_Click);
            this.btnIdentificationConfirmation.Click += new EventHandler(btnIdentificationConfirmation_Click);
            this.btnContinueLineup.Click += new EventHandler(btnContinueLineup_Click);
            this.btnFinalComments.Click += new EventHandler(btnFinalComments_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            principal = (cc.SitePrincipal)Context.User;
            identity = (cc.SiteIdentity)principal.Identity;
            base.OnInit(e);
        }
    }
}
