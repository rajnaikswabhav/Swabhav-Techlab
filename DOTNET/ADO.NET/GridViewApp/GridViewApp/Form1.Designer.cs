namespace GridViewApp
{
    partial class Form1
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
            this.fillBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.empLabel = new System.Windows.Forms.Label();
            this.deptLabel = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // fillBtn
            // 
            this.fillBtn.Location = new System.Drawing.Point(226, 39);
            this.fillBtn.Name = "fillBtn";
            this.fillBtn.Size = new System.Drawing.Size(75, 23);
            this.fillBtn.TabIndex = 0;
            this.fillBtn.Text = "Submit";
            this.fillBtn.UseVisualStyleBackColor = true;
            this.fillBtn.Click += new System.EventHandler(this.fillBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Hide();
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 94);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(656, 241);
            this.dataGridView1.TabIndex = 1;
            // 
            // empLabel
            // 
            this.empLabel.Hide();
            this.empLabel.AutoSize = true;
            this.empLabel.Location = new System.Drawing.Point(12, 69);
            this.empLabel.Name = "empLabel";
            this.empLabel.Size = new System.Drawing.Size(83, 13);
            this.empLabel.TabIndex = 3;
            this.empLabel.Text = "Employee Table";
            // 
            // deptLabel
            // 
            this.deptLabel.Hide();
            this.deptLabel.AutoSize = true;
            this.deptLabel.Location = new System.Drawing.Point(12, 358);
            this.deptLabel.Name = "deptLabel";
            this.deptLabel.Size = new System.Drawing.Size(92, 13);
            this.deptLabel.TabIndex = 4;
            this.deptLabel.Text = "Department Tabel";
            // 
            // dataGridView2
            // 
            this.dataGridView2.Hide();
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 384);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(343, 150);
            this.dataGridView2.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 556);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.deptLabel);
            this.Controls.Add(this.empLabel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.fillBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fillBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label empLabel;
        private System.Windows.Forms.Label deptLabel;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}

