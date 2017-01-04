namespace FairyStar
{
    partial class GUI
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.PlayArea_Panel = new FairyStar.DoubleBufferPanel();
            this.SuspendLayout();
            // 
            // PlayArea_Panel
            // 
            this.PlayArea_Panel.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.PlayArea_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayArea_Panel.ForeColor = System.Drawing.SystemColors.Window;
            this.PlayArea_Panel.Location = new System.Drawing.Point(0, 0);
            this.PlayArea_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.PlayArea_Panel.Name = "PlayArea_Panel";
            this.PlayArea_Panel.Size = new System.Drawing.Size(1264, 681);
            this.PlayArea_Panel.TabIndex = 0;
            this.PlayArea_Panel.VisibleChanged += new System.EventHandler(this.PlayArea_Panel_VisibleChanged);
            this.PlayArea_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayArea_Paint);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.PlayArea_Panel);
            this.Name = "GUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GUI_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBufferPanel PlayArea_Panel;
    }
}

