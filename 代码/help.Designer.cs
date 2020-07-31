namespace index
{
    partial class help
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label1 = new System.Windows.Forms.Label();
            this.Button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label1.Location = new System.Drawing.Point(5, 0);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(615, 162);
            this.Label1.TabIndex = 166;
            this.Label1.Text = "本系统主要用于咖啡厅管理，方便老板及时了解店内商品销售情况，方便收银员收款和结账，方便服务生下订单，达到了省时省力的效果！功能包括系统管理，商品销售额浏览，会员管" +
    "理，服务生管理，预约座位。";
            this.Label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(273, 166);
            this.Button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(98, 46);
            this.Button4.TabIndex = 176;
            this.Button4.Text = "返回";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 220);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "help";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帮助";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button4;
    }
}