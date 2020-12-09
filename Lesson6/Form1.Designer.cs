namespace Lesson6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txbx_TexField = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txbx_TexField
            // 
            this.txbx_TexField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbx_TexField.Location = new System.Drawing.Point(0, 0);
            this.txbx_TexField.Name = "txbx_TexField";
            this.txbx_TexField.Size = new System.Drawing.Size(667, 529);
            this.txbx_TexField.TabIndex = 0;
            this.txbx_TexField.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 529);
            this.Controls.Add(this.txbx_TexField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txbx_TexField;
    }
}

