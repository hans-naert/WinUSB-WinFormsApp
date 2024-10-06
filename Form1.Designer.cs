namespace WinUSB_WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            connectButton = new Button();
            connectLabel = new Label();
            transferButton = new Button();
            transferTextBox = new TextBox();
            transferLabel = new Label();
            SuspendLayout();
            // 
            // connectButton
            // 
            connectButton.Location = new Point(35, 36);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(94, 29);
            connectButton.TabIndex = 0;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // connectLabel
            // 
            connectLabel.AutoSize = true;
            connectLabel.Location = new Point(175, 40);
            connectLabel.Name = "connectLabel";
            connectLabel.Size = new Size(104, 20);
            connectLabel.TabIndex = 1;
            connectLabel.Text = "not connected";
            // 
            // transferButton
            // 
            transferButton.Location = new Point(35, 128);
            transferButton.Name = "transferButton";
            transferButton.Size = new Size(94, 29);
            transferButton.TabIndex = 2;
            transferButton.Text = "Transfer";
            transferButton.UseVisualStyleBackColor = true;
            transferButton.Click += transferButton_Click;
            // 
            // transferTextBox
            // 
            transferTextBox.Location = new Point(175, 130);
            transferTextBox.Name = "transferTextBox";
            transferTextBox.PlaceholderText = "Put text to transfer";
            transferTextBox.Size = new Size(330, 27);
            transferTextBox.TabIndex = 3;
            // 
            // transferLabel
            // 
            transferLabel.AutoSize = true;
            transferLabel.Location = new Point(175, 190);
            transferLabel.Name = "transferLabel";
            transferLabel.Size = new Size(100, 20);
            transferLabel.TabIndex = 4;
            transferLabel.Text = "Received Text";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(transferLabel);
            Controls.Add(transferTextBox);
            Controls.Add(transferButton);
            Controls.Add(connectLabel);
            Controls.Add(connectButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button connectButton;
        private Label connectLabel;
        private Button transferButton;
        private TextBox transferTextBox;
        private Label transferLabel;
    }
}
