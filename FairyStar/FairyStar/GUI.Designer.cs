﻿namespace FairyStar
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
            this.PlayArea_panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PlayArea_panel
            // 
            this.PlayArea_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayArea_panel.Location = new System.Drawing.Point(0, 0);
            this.PlayArea_panel.Margin = new System.Windows.Forms.Padding(0);
            this.PlayArea_panel.Name = "PlayArea_panel";
            this.PlayArea_panel.Size = new System.Drawing.Size(1264, 681);
            this.PlayArea_panel.TabIndex = 0;
            this.PlayArea_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayArea_paint);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.PlayArea_panel);
            this.Name = "GUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PlayArea_panel;
    }
}

