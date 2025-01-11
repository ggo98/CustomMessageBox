using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomMessageBox.Private
{
    public partial class FormMessageBox : Form
    {
        //Fields
        private Color primaryColor = Color.CornflowerBlue;
        private int borderSize = 2;

        //Properties
        public static bool DefaultModifyPrimaryColor { get; set; }

        public static bool DefaultModifyButtonsColor { get; set; }

        public static Color DefaultButtonsBackColor { get; set; } = System.Drawing.SystemColors.Control;

        public static Color DefaultButtonsForeColor { get; set; } = System.Drawing.SystemColors.ControlText;

        public bool ModifyPrimaryColor { get; set; } = DefaultModifyPrimaryColor;

        public bool ModifyButtonsColor { get; set; } = DefaultModifyButtonsColor;

        /// <summary>
        /// Back color to use for buttons when ModifyButtonsColor is false
        /// </summary>
        public Color ButtonsBackColor { get; set; } = DefaultButtonsBackColor;

        /// <summary>
        /// Fore color to use for buttons when ModifyButtonsColor is false
        /// </summary>
        public Color ButtonsForeColor { get; set; } = DefaultButtonsForeColor;

        public Color PrimaryColor
        {
            get { return primaryColor; }
            set
            {
                primaryColor = value;
                this.BackColor = primaryColor;//Form Border Color
                this.panelTitleBar.BackColor = PrimaryColor;//Title Bar Back Color
            }
        }

        public FormMessageBox()
        {
            InitializeComponent();
            InitializeItems();
            SetPrimaryColor();
            if (!ModifyButtonsColor)
            {
                panelButtons.BackColor = DefaultButtonsBackColor;

                button1.FlatStyle = FlatStyle.Standard;
                button2.FlatStyle = FlatStyle.Standard;
                button3.FlatStyle = FlatStyle.Standard;

                button1.BackColor = ButtonsBackColor;
                button2.BackColor = ButtonsBackColor;
                button3.BackColor = ButtonsBackColor;
                button1.ForeColor = ButtonsForeColor;
                button2.ForeColor = ButtonsForeColor;
                button2.ForeColor = ButtonsForeColor;
                button3.ForeColor = ButtonsForeColor;
            }
        }

        //Constructors
        public FormMessageBox(string text)
            :
            this()
        {
            this.labelMessage.Text = text;
            this.labelCaption.Text = "";
            SetFormSize();
            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);//Set Default Buttons
        }
        public FormMessageBox(string text, string caption)
            :
            this()
        {
            this.labelMessage.Text = text;
            this.labelCaption.Text = caption;
            SetFormSize();
            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);//Set Default Buttons
        }
        public FormMessageBox(string text, string caption, MessageBoxButtons buttons)
            :
            this()
        {
            this.labelMessage.Text = text;
            this.labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, MessageBoxDefaultButton.Button1);//Set [Default Button 1]
        }
        public FormMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            :
            this()
        {
            this.labelMessage.Text = text;
            this.labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, MessageBoxDefaultButton.Button1);//Set [Default Button 1]
            SetIcon(icon);
        }
        public FormMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            :
            this()
        {
            this.labelMessage.Text = text;
            this.labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, defaultButton);
            SetIcon(icon);
        }

        private void SetPrimaryColor()
        {
            if (!ModifyPrimaryColor)
                this.PrimaryColor = PrimaryColor;
        }

        //-> Private Methods
        private void InitializeItems()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(borderSize);//Set border size
            this.labelMessage.MaximumSize = new Size(550, 0);
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.button1.DialogResult = DialogResult.OK;
            this.button1.Visible = false;
            this.button2.Visible = false;
            this.button3.Visible = false;
        }
        private void SetFormSize()
        {
            int widht = this.labelMessage.Width + this.pictureBoxIcon.Width + this.panelBody.Padding.Left;
            int height = this.panelTitleBar.Height + this.labelMessage.Height + this.panelButtons.Height + this.panelBody.Padding.Top;
            this.Size = new Size(widht, height);
        }
        private void SetButtons(MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            int xCenter = (this.panelButtons.Width - button1.Width) / 2;
            int yCenter = (this.panelButtons.Height - button1.Height) / 2;

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    //OK Button
                    button1.Visible = true;
                    button1.Location = new Point(xCenter, yCenter);
                    button1.Text = "Ok";
                    button1.DialogResult = DialogResult.OK;//Set DialogResult

                    //Set Default Button
                    SetDefaultButton(defaultButton);
                    break;
                case MessageBoxButtons.OKCancel:
                    //OK Button
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                    button1.Text = "Ok";
                    button1.DialogResult = DialogResult.OK;//Set DialogResult

                    //Cancel Button
                    button2.Visible = true;
                    button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                    button2.Text = "Cancel";
                    button2.DialogResult = DialogResult.Cancel;//Set DialogResult
                    if (ModifyButtonsColor)
                        button2.BackColor = Color.DimGray;

                    //Set Default Button
                    if (defaultButton != MessageBoxDefaultButton.Button3)//There are only 2 buttons, so the Default Button cannot be Button3
                        SetDefaultButton(defaultButton);
                    else SetDefaultButton(MessageBoxDefaultButton.Button1);
                    break;

                case MessageBoxButtons.RetryCancel:
                    //Retry Button
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                    button1.Text = "Retry";
                    button1.DialogResult = DialogResult.Retry;//Set DialogResult

                    //Cancel Button
                    button2.Visible = true;
                    button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                    button2.Text = "Cancel";
                    button2.DialogResult = DialogResult.Cancel;//Set DialogResult
                    if (ModifyButtonsColor)
                        button2.BackColor = Color.DimGray;

                    //Set Default Button
                    if (defaultButton != MessageBoxDefaultButton.Button3)//There are only 2 buttons, so the Default Button cannot be Button3
                        SetDefaultButton(defaultButton);
                    else SetDefaultButton(MessageBoxDefaultButton.Button1);
                    break;

                case MessageBoxButtons.YesNo:
                    //Yes Button
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                    button1.Text = "Yes";
                    button1.DialogResult = DialogResult.Yes;//Set DialogResult

                    //No Button
                    button2.Visible = true;
                    button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                    button2.Text = "No";
                    button2.DialogResult = DialogResult.No;//Set DialogResult
                    if (ModifyButtonsColor)
                        button2.BackColor = Color.IndianRed;

                    //Set Default Button
                    if (defaultButton != MessageBoxDefaultButton.Button3)//There are only 2 buttons, so the Default Button cannot be Button3
                        SetDefaultButton(defaultButton);
                    else SetDefaultButton(MessageBoxDefaultButton.Button1);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    //Yes Button
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - button1.Width - 5, yCenter);
                    button1.Text = "Yes";
                    button1.DialogResult = DialogResult.Yes;//Set DialogResult

                    //No Button
                    button2.Visible = true;
                    button2.Location = new Point(xCenter, yCenter);
                    button2.Text = "No";
                    button2.DialogResult = DialogResult.No;//Set DialogResult
                    if (ModifyButtonsColor)
                        button2.BackColor = Color.IndianRed;

                    //Cancel Button
                    button3.Visible = true;
                    button3.Location = new Point(xCenter + button2.Width + 5, yCenter);
                    button3.Text = "Cancel";
                    button3.DialogResult = DialogResult.Cancel;//Set DialogResult
                    if (ModifyButtonsColor)
                        button3.BackColor = Color.DimGray;

                    //Set Default Button
                    SetDefaultButton(defaultButton);
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    //Abort Button
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - button1.Width - 5, yCenter);
                    button1.Text = "Abort";
                    button1.DialogResult = DialogResult.Abort;//Set DialogResult
                    if (ModifyButtonsColor)
                        button1.BackColor = Color.Goldenrod;

                    //Retry Button
                    button2.Visible = true;
                    button2.Location = new Point(xCenter, yCenter);
                    button2.Text = "Retry";
                    button2.DialogResult = DialogResult.Retry;//Set DialogResult                    

                    //Ignore Button
                    button3.Visible = true;
                    button3.Location = new Point(xCenter + button2.Width + 5, yCenter);
                    button3.Text = "Ignore";
                    button3.DialogResult = DialogResult.Ignore;//Set DialogResult
                    if (ModifyButtonsColor)
                        button3.BackColor = Color.IndianRed;

                    //Set Default Button
                    SetDefaultButton(defaultButton);
                    break;
            }
        }
        private void SetDefaultButton(MessageBoxDefaultButton defaultButton)
        {
            switch (defaultButton)
            {
                case MessageBoxDefaultButton.Button1://Focus button 1
                    button1.Select();
                    if (ModifyButtonsColor)
                        button1.ForeColor = Color.White;
                    button1.Font = new Font(button1.Font, FontStyle.Underline);
                    break;
                case MessageBoxDefaultButton.Button2://Focus button 2
                    button2.Select();
                    if (ModifyButtonsColor)
                        button2.ForeColor = Color.White;
                    button2.Font = new Font(button2.Font, FontStyle.Underline);
                    break;
                case MessageBoxDefaultButton.Button3://Focus button 3
                    button3.Select();
                    if (ModifyButtonsColor)
                        button3.ForeColor = Color.White;
                    button3.Font = new Font(button3.Font, FontStyle.Underline);
                    break;
            }
        }
        private void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Error: //Error
                    this.pictureBoxIcon.Image = Properties.Resources.error;
                    if (ModifyPrimaryColor)
                        PrimaryColor = Color.FromArgb(224, 79, 95);
                    if (ModifyButtonsColor)
                        this.btnClose.FlatAppearance.MouseOverBackColor = Color.Crimson;
                    break;
                case MessageBoxIcon.Information: //Information
                    this.pictureBoxIcon.Image = Properties.Resources.information;
                    if (ModifyPrimaryColor)
                        PrimaryColor = Color.FromArgb(38, 191, 166);
                    break;
                case MessageBoxIcon.Question://Question
                    this.pictureBoxIcon.Image = Properties.Resources.question;
                    if (ModifyPrimaryColor)
                        PrimaryColor = Color.FromArgb(10, 119, 232);
                    break;
                case MessageBoxIcon.Exclamation://Exclamation
                    this.pictureBoxIcon.Image = Properties.Resources.exclamation;
                    if (ModifyPrimaryColor)
                        PrimaryColor = Color.FromArgb(255, 140, 0);
                    break;
                case MessageBoxIcon.None: //None
                    this.pictureBoxIcon.Image = Properties.Resources.chat;
                    if (ModifyPrimaryColor)
                        PrimaryColor = Color.CornflowerBlue;
                    break;
            }
        }

        //-> Events Methods
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region -> Drag Form
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
    }
}
