namespace o2flo_pic_parser
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
            this.button_load_pic_folder = new System.Windows.Forms.Button();
            this.textBox_folder_path = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_start_parse_pic = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.richTextBox_parse = new System.Windows.Forms.RichTextBox();
            this.button_save_2_file = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_load_pic_folder
            // 
            this.button_load_pic_folder.Location = new System.Drawing.Point(6, 31);
            this.button_load_pic_folder.Name = "button_load_pic_folder";
            this.button_load_pic_folder.Size = new System.Drawing.Size(120, 34);
            this.button_load_pic_folder.TabIndex = 0;
            this.button_load_pic_folder.Text = "打开文件夹";
            this.button_load_pic_folder.UseVisualStyleBackColor = true;
            this.button_load_pic_folder.Click += new System.EventHandler(this.button_load_pic_folder_Click);
            // 
            // textBox_folder_path
            // 
            this.textBox_folder_path.Location = new System.Drawing.Point(156, 38);
            this.textBox_folder_path.Name = "textBox_folder_path";
            this.textBox_folder_path.Size = new System.Drawing.Size(554, 25);
            this.textBox_folder_path.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.button_load_pic_folder);
            this.groupBox1.Controls.Add(this.textBox_folder_path);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(743, 209);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择图片文件夹";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(7, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(703, 111);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_save_2_file);
            this.groupBox2.Controls.Add(this.richTextBox_parse);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.button_start_parse_pic);
            this.groupBox2.Location = new System.Drawing.Point(13, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(742, 319);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "解析";
            // 
            // button_start_parse_pic
            // 
            this.button_start_parse_pic.Location = new System.Drawing.Point(6, 24);
            this.button_start_parse_pic.Name = "button_start_parse_pic";
            this.button_start_parse_pic.Size = new System.Drawing.Size(120, 34);
            this.button_start_parse_pic.TabIndex = 1;
            this.button_start_parse_pic.Text = "开始";
            this.button_start_parse_pic.UseVisualStyleBackColor = true;
            this.button_start_parse_pic.Click += new System.EventHandler(this.button_start_parse_pic_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(141, 35);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(372, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // richTextBox_parse
            // 
            this.richTextBox_parse.Location = new System.Drawing.Point(13, 80);
            this.richTextBox_parse.Name = "richTextBox_parse";
            this.richTextBox_parse.Size = new System.Drawing.Size(703, 224);
            this.richTextBox_parse.TabIndex = 3;
            this.richTextBox_parse.Text = "";
            // 
            // button_save_2_file
            // 
            this.button_save_2_file.Location = new System.Drawing.Point(589, 24);
            this.button_save_2_file.Name = "button_save_2_file";
            this.button_save_2_file.Size = new System.Drawing.Size(120, 34);
            this.button_save_2_file.TabIndex = 4;
            this.button_save_2_file.Text = "保存";
            this.button_save_2_file.UseVisualStyleBackColor = true;
            this.button_save_2_file.Click += new System.EventHandler(this.button_save_2_file_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "bmp array file(*.c)|*.c|All Files(*.*)|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 577);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Pic parser";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_load_pic_folder;
        private System.Windows.Forms.TextBox textBox_folder_path;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_start_parse_pic;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox richTextBox_parse;
        private System.Windows.Forms.Button button_save_2_file;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

