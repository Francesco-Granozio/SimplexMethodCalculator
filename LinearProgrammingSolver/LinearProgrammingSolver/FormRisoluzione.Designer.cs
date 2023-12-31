﻿namespace LinearProgrammingSolver
{
    partial class FormRisoluzione
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox_standard_form_problem = new System.Windows.Forms.RichTextBox();
            this.richTextBox_original_problem = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_coefficients_matrix = new System.Windows.Forms.Label();
            this.richTextBox_coefficients_matrix = new System.Windows.Forms.RichTextBox();
            this.label_c_transposed = new System.Windows.Forms.Label();
            this.richTextBox_c_transposed = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.richTextBox_c_transposed);
            this.panel1.Controls.Add(this.label_c_transposed);
            this.panel1.Controls.Add(this.richTextBox_coefficients_matrix);
            this.panel1.Controls.Add(this.label_coefficients_matrix);
            this.panel1.Controls.Add(this.richTextBox_standard_form_problem);
            this.panel1.Controls.Add(this.richTextBox_original_problem);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(19, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(931, 599);
            this.panel1.TabIndex = 1;
            // 
            // richTextBox_standard_form_problem
            // 
            this.richTextBox_standard_form_problem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_standard_form_problem.Location = new System.Drawing.Point(480, 89);
            this.richTextBox_standard_form_problem.Name = "richTextBox_standard_form_problem";
            this.richTextBox_standard_form_problem.ReadOnly = true;
            this.richTextBox_standard_form_problem.Size = new System.Drawing.Size(427, 239);
            this.richTextBox_standard_form_problem.TabIndex = 6;
            this.richTextBox_standard_form_problem.Text = "";
            // 
            // richTextBox_original_problem
            // 
            this.richTextBox_original_problem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_original_problem.Location = new System.Drawing.Point(20, 89);
            this.richTextBox_original_problem.Name = "richTextBox_original_problem";
            this.richTextBox_original_problem.ReadOnly = true;
            this.richTextBox_original_problem.Size = new System.Drawing.Size(427, 239);
            this.richTextBox_original_problem.TabIndex = 5;
            this.richTextBox_original_problem.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(555, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(317, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Problem converted in standard form";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(95, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Original problem";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // label_coefficients_matrix
            // 
            this.label_coefficients_matrix.AutoSize = true;
            this.label_coefficients_matrix.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_coefficients_matrix.Location = new System.Drawing.Point(27, 431);
            this.label_coefficients_matrix.Name = "label_coefficients_matrix";
            this.label_coefficients_matrix.Size = new System.Drawing.Size(42, 25);
            this.label_coefficients_matrix.TabIndex = 7;
            this.label_coefficients_matrix.Text = "A =";
            // 
            // richTextBox_coefficients_matrix
            // 
            this.richTextBox_coefficients_matrix.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_coefficients_matrix.Location = new System.Drawing.Point(75, 357);
            this.richTextBox_coefficients_matrix.Name = "richTextBox_coefficients_matrix";
            this.richTextBox_coefficients_matrix.ReadOnly = true;
            this.richTextBox_coefficients_matrix.Size = new System.Drawing.Size(357, 224);
            this.richTextBox_coefficients_matrix.TabIndex = 8;
            this.richTextBox_coefficients_matrix.Text = "";
            // 
            // label_c_transposed
            // 
            this.label_c_transposed.AutoSize = true;
            this.label_c_transposed.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_c_transposed.Location = new System.Drawing.Point(468, 431);
            this.label_c_transposed.Name = "label_c_transposed";
            this.label_c_transposed.Size = new System.Drawing.Size(62, 25);
            this.label_c_transposed.TabIndex = 9;
            this.label_c_transposed.Text = "c^T =";
            // 
            // richTextBox_c_transposed
            // 
            this.richTextBox_c_transposed.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_c_transposed.Location = new System.Drawing.Point(549, 422);
            this.richTextBox_c_transposed.Name = "richTextBox_c_transposed";
            this.richTextBox_c_transposed.ReadOnly = true;
            this.richTextBox_c_transposed.Size = new System.Drawing.Size(349, 50);
            this.richTextBox_c_transposed.TabIndex = 10;
            this.richTextBox_c_transposed.Text = "";
            // 
            // FormRisoluzione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(962, 631);
            this.Controls.Add(this.panel1);
            this.Name = "FormRisoluzione";
            this.Text = "Form Risoluzione";
            this.Load += new System.EventHandler(this.FormRisoluzione_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox_original_problem;
        private System.Windows.Forms.RichTextBox richTextBox_standard_form_problem;
        private System.Windows.Forms.RichTextBox richTextBox_coefficients_matrix;
        private System.Windows.Forms.Label label_coefficients_matrix;
        private System.Windows.Forms.RichTextBox richTextBox_c_transposed;
        private System.Windows.Forms.Label label_c_transposed;
    }
}