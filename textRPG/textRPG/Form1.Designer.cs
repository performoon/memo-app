
namespace textRPG
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_menu = new System.Windows.Forms.Panel();
            this.panel_menu_item = new System.Windows.Forms.Panel();
            this.label_menu_button_item = new System.Windows.Forms.Label();
            this.panel_menu_character = new System.Windows.Forms.Panel();
            this.label_menu_button_character = new System.Windows.Forms.Label();
            this.panel_menu.SuspendLayout();
            this.panel_menu_item.SuspendLayout();
            this.panel_menu_character.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_menu
            // 
            this.panel_menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_menu.Controls.Add(this.panel_menu_item);
            this.panel_menu.Controls.Add(this.panel_menu_character);
            this.panel_menu.Location = new System.Drawing.Point(10, 12);
            this.panel_menu.Name = "panel_menu";
            this.panel_menu.Size = new System.Drawing.Size(95, 98);
            this.panel_menu.TabIndex = 7;
            // 
            // panel_menu_item
            // 
            this.panel_menu_item.Controls.Add(this.label_menu_button_item);
            this.panel_menu_item.Location = new System.Drawing.Point(0, 51);
            this.panel_menu_item.Name = "panel_menu_item";
            this.panel_menu_item.Size = new System.Drawing.Size(95, 42);
            this.panel_menu_item.TabIndex = 17;
            this.panel_menu_item.Tag = "Frame";
            this.panel_menu_item.Click += new System.EventHandler(this.button_menu_item_Click);
            // 
            // label_menu_button_item
            // 
            this.label_menu_button_item.AutoSize = true;
            this.label_menu_button_item.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_menu_button_item.Location = new System.Drawing.Point(23, 15);
            this.label_menu_button_item.Name = "label_menu_button_item";
            this.label_menu_button_item.Size = new System.Drawing.Size(51, 13);
            this.label_menu_button_item.TabIndex = 18;
            this.label_menu_button_item.Text = "アイテム";
            this.label_menu_button_item.Click += new System.EventHandler(this.button_menu_item_Click);
            // 
            // panel_menu_character
            // 
            this.panel_menu_character.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_menu_character.Controls.Add(this.label_menu_button_character);
            this.panel_menu_character.Location = new System.Drawing.Point(0, 3);
            this.panel_menu_character.Name = "panel_menu_character";
            this.panel_menu_character.Size = new System.Drawing.Size(95, 42);
            this.panel_menu_character.TabIndex = 17;
            this.panel_menu_character.Tag = "Frame";
            this.panel_menu_character.Click += new System.EventHandler(this.button_menu_character_Click);
            // 
            // label_menu_button_character
            // 
            this.label_menu_button_character.AutoSize = true;
            this.label_menu_button_character.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_menu_button_character.Location = new System.Drawing.Point(11, 15);
            this.label_menu_button_character.Name = "label_menu_button_character";
            this.label_menu_button_character.Size = new System.Drawing.Size(71, 13);
            this.label_menu_button_character.TabIndex = 20;
            this.label_menu_button_character.Text = "キャラクター";
            this.label_menu_button_character.Click += new System.EventHandler(this.button_menu_character_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(919, 650);
            this.Controls.Add(this.panel_menu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel_menu.ResumeLayout(false);
            this.panel_menu_item.ResumeLayout(false);
            this.panel_menu_item.PerformLayout();
            this.panel_menu_character.ResumeLayout(false);
            this.panel_menu_character.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_menu;
        private System.Windows.Forms.Panel panel_menu_item;
        private System.Windows.Forms.Panel panel_menu_character;
        private System.Windows.Forms.Label label_menu_button_item;
        private System.Windows.Forms.Label label_menu_button_character;
    }
}

