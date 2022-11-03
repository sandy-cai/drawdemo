namespace Lucky_Draw
{
    partial class LD_Setting
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LD_Setting));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTopLeft = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrizeNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnIni = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtJP = new System.Windows.Forms.TextBox();
            this.txtPrizeName = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.DG = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtPrizeOrder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtWhere = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DG)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "右上角标题：";
            // 
            // txtTopLeft
            // 
            this.txtTopLeft.Location = new System.Drawing.Point(81, 12);
            this.txtTopLeft.Name = "txtTopLeft";
            this.txtTopLeft.Size = new System.Drawing.Size(124, 21);
            this.txtTopLeft.TabIndex = 1;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(273, 12);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(234, 21);
            this.txtTitle.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "主标题：";
            // 
            // txtTitle2
            // 
            this.txtTitle2.Location = new System.Drawing.Point(568, 12);
            this.txtTitle2.Name = "txtTitle2";
            this.txtTitle2.Size = new System.Drawing.Size(192, 21);
            this.txtTitle2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(520, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "副标题：";
            // 
            // txtPrizeNumber
            // 
            this.txtPrizeNumber.Location = new System.Drawing.Point(289, 68);
            this.txtPrizeNumber.Name = "txtPrizeNumber";
            this.txtPrizeNumber.Size = new System.Drawing.Size(43, 21);
            this.txtPrizeNumber.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "抽奖个数：";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(332, 114);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 10;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnIni
            // 
            this.btnIni.Location = new System.Drawing.Point(114, 114);
            this.btnIni.Name = "btnIni";
            this.btnIni.Size = new System.Drawing.Size(75, 23);
            this.btnIni.TabIndex = 8;
            this.btnIni.Text = "重置";
            this.btnIni.UseVisualStyleBackColor = true;
            this.btnIni.Click += new System.EventHandler(this.btnIni_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(346, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "奖品：";
            // 
            // txtJP
            // 
            this.txtJP.Location = new System.Drawing.Point(383, 68);
            this.txtJP.Name = "txtJP";
            this.txtJP.Size = new System.Drawing.Size(124, 21);
            this.txtJP.TabIndex = 6;
            // 
            // txtPrizeName
            // 
            this.txtPrizeName.Location = new System.Drawing.Point(81, 68);
            this.txtPrizeName.Name = "txtPrizeName";
            this.txtPrizeName.Size = new System.Drawing.Size(124, 21);
            this.txtPrizeName.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(21, 72);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 28;
            this.label18.Text = "奖项名称：";
            // 
            // DG
            // 
            this.DG.AllowUserToAddRows = false;
            this.DG.AllowUserToDeleteRows = false;
            this.DG.AllowUserToOrderColumns = true;
            this.DG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG.Location = new System.Drawing.Point(17, 143);
            this.DG.Name = "DG";
            this.DG.ReadOnly = true;
            this.DG.RowTemplate.Height = 23;
            this.DG.Size = new System.Drawing.Size(743, 307);
            this.DG.TabIndex = 30;
            this.DG.Click += new System.EventHandler(this.DG_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(441, 114);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtPrizeOrder
            // 
            this.txtPrizeOrder.Location = new System.Drawing.Point(582, 69);
            this.txtPrizeOrder.Name = "txtPrizeOrder";
            this.txtPrizeOrder.Size = new System.Drawing.Size(38, 21);
            this.txtPrizeOrder.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(524, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "奖项顺序：";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(520, 118);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 12);
            this.lblMessage.TabIndex = 34;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(225, 114);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtWhere
            // 
            this.txtWhere.Location = new System.Drawing.Point(691, 70);
            this.txtWhere.Name = "txtWhere";
            this.txtWhere.Size = new System.Drawing.Size(69, 21);
            this.txtWhere.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(633, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "排除条件：";
            // 
            // LD_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(772, 462);
            this.Controls.Add(this.txtWhere);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtPrizeOrder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.DG);
            this.Controls.Add(this.txtPrizeName);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtJP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnIni);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.txtPrizeNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTitle2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTopLeft);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(788, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(788, 500);
            this.Name = "LD_Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "抽奖程序－基本设置";
            this.Load += new System.EventHandler(this.LD_Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTopLeft;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrizeNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnIni;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtJP;
        private System.Windows.Forms.TextBox txtPrizeName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView DG;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtPrizeOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtWhere;
        private System.Windows.Forms.Label label6;
    }
}