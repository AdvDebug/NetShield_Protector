
namespace NetShield_Protector
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.AntiDecompilerCheckBox = new System.Windows.Forms.CheckBox();
            this.AntiVMCheckBox = new System.Windows.Forms.CheckBox();
            this.AntiDebugCheckBox = new System.Windows.Forms.CheckBox();
            this.PackingCheckBox = new System.Windows.Forms.CheckBox();
            this.AntiILDasmCheckBox = new System.Windows.Forms.CheckBox();
            this.INTConfusionCheckBox = new System.Windows.Forms.CheckBox();
            this.RenamingCheckBox = new System.Windows.Forms.CheckBox();
            this.CtrlFlowCheckBox = new System.Windows.Forms.CheckBox();
            this.JunkCheckBox = new System.Windows.Forms.CheckBox();
            this.FakeObfuscatorAttributesCheckBox = new System.Windows.Forms.CheckBox();
            this.AntiDe4DotCheckBox = new System.Windows.Forms.CheckBox();
            this.Base64EncodeCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.HowThisWorksButton = new System.Windows.Forms.Button();
            this.USBComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.USBHWIDCheckBox = new System.Windows.Forms.CheckBox();
            this.WhatThisDoButton = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.GetCurrentHWIDButton = new System.Windows.Forms.Button();
            this.LockToHWIDCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AntiDecompilerCheckBox);
            this.panel1.Controls.Add(this.AntiVMCheckBox);
            this.panel1.Controls.Add(this.AntiDebugCheckBox);
            this.panel1.Controls.Add(this.PackingCheckBox);
            this.panel1.Controls.Add(this.AntiILDasmCheckBox);
            this.panel1.Controls.Add(this.INTConfusionCheckBox);
            this.panel1.Controls.Add(this.RenamingCheckBox);
            this.panel1.Controls.Add(this.CtrlFlowCheckBox);
            this.panel1.Controls.Add(this.JunkCheckBox);
            this.panel1.Controls.Add(this.FakeObfuscatorAttributesCheckBox);
            this.panel1.Controls.Add(this.AntiDe4DotCheckBox);
            this.panel1.Controls.Add(this.Base64EncodeCheckBox);
            this.panel1.Location = new System.Drawing.Point(12, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 590);
            this.panel1.TabIndex = 0;
            // 
            // AntiDecompilerCheckBox
            // 
            this.AntiDecompilerCheckBox.AutoSize = true;
            this.AntiDecompilerCheckBox.Location = new System.Drawing.Point(13, 544);
            this.AntiDecompilerCheckBox.Name = "AntiDecompilerCheckBox";
            this.AntiDecompilerCheckBox.Size = new System.Drawing.Size(147, 24);
            this.AntiDecompilerCheckBox.TabIndex = 12;
            this.AntiDecompilerCheckBox.Text = "Anti Decompiler";
            this.AntiDecompilerCheckBox.UseVisualStyleBackColor = true;
            // 
            // AntiVMCheckBox
            // 
            this.AntiVMCheckBox.AutoSize = true;
            this.AntiVMCheckBox.Location = new System.Drawing.Point(13, 493);
            this.AntiVMCheckBox.Name = "AntiVMCheckBox";
            this.AntiVMCheckBox.Size = new System.Drawing.Size(92, 24);
            this.AntiVMCheckBox.TabIndex = 11;
            this.AntiVMCheckBox.Text = "Anti-VM";
            this.AntiVMCheckBox.UseVisualStyleBackColor = true;
            this.AntiVMCheckBox.MouseHover += new System.EventHandler(this.checkBox14_MouseHover);
            // 
            // AntiDebugCheckBox
            // 
            this.AntiDebugCheckBox.AutoSize = true;
            this.AntiDebugCheckBox.Location = new System.Drawing.Point(13, 440);
            this.AntiDebugCheckBox.Name = "AntiDebugCheckBox";
            this.AntiDebugCheckBox.Size = new System.Drawing.Size(116, 24);
            this.AntiDebugCheckBox.TabIndex = 10;
            this.AntiDebugCheckBox.Text = "Anti-Debug";
            this.AntiDebugCheckBox.UseVisualStyleBackColor = true;
            this.AntiDebugCheckBox.MouseHover += new System.EventHandler(this.checkBox13_MouseHover_1);
            // 
            // PackingCheckBox
            // 
            this.PackingCheckBox.AutoSize = true;
            this.PackingCheckBox.Location = new System.Drawing.Point(13, 393);
            this.PackingCheckBox.Name = "PackingCheckBox";
            this.PackingCheckBox.Size = new System.Drawing.Size(91, 24);
            this.PackingCheckBox.TabIndex = 9;
            this.PackingCheckBox.Text = "Packing";
            this.PackingCheckBox.UseVisualStyleBackColor = true;
            this.PackingCheckBox.MouseHover += new System.EventHandler(this.checkBox12_MouseHover);
            // 
            // AntiILDasmCheckBox
            // 
            this.AntiILDasmCheckBox.AutoSize = true;
            this.AntiILDasmCheckBox.Location = new System.Drawing.Point(13, 348);
            this.AntiILDasmCheckBox.Name = "AntiILDasmCheckBox";
            this.AntiILDasmCheckBox.Size = new System.Drawing.Size(123, 24);
            this.AntiILDasmCheckBox.TabIndex = 8;
            this.AntiILDasmCheckBox.Text = "Anti ILDasm";
            this.AntiILDasmCheckBox.UseVisualStyleBackColor = true;
            this.AntiILDasmCheckBox.MouseHover += new System.EventHandler(this.checkBox11_MouseHover);
            // 
            // INTConfusionCheckBox
            // 
            this.INTConfusionCheckBox.AutoSize = true;
            this.INTConfusionCheckBox.Location = new System.Drawing.Point(13, 296);
            this.INTConfusionCheckBox.Name = "INTConfusionCheckBox";
            this.INTConfusionCheckBox.Size = new System.Drawing.Size(136, 24);
            this.INTConfusionCheckBox.TabIndex = 7;
            this.INTConfusionCheckBox.Text = "INT Confusion";
            this.INTConfusionCheckBox.UseVisualStyleBackColor = true;
            this.INTConfusionCheckBox.MouseHover += new System.EventHandler(this.checkBox10_MouseHover);
            // 
            // RenamingCheckBox
            // 
            this.RenamingCheckBox.AutoSize = true;
            this.RenamingCheckBox.Location = new System.Drawing.Point(13, 248);
            this.RenamingCheckBox.Name = "RenamingCheckBox";
            this.RenamingCheckBox.Size = new System.Drawing.Size(204, 24);
            this.RenamingCheckBox.TabIndex = 5;
            this.RenamingCheckBox.Text = "Rename Methods, etc...";
            this.RenamingCheckBox.UseVisualStyleBackColor = true;
            this.RenamingCheckBox.MouseHover += new System.EventHandler(this.checkBox8_MouseHover);
            // 
            // CtrlFlowCheckBox
            // 
            this.CtrlFlowCheckBox.AutoSize = true;
            this.CtrlFlowCheckBox.Location = new System.Drawing.Point(13, 198);
            this.CtrlFlowCheckBox.Name = "CtrlFlowCheckBox";
            this.CtrlFlowCheckBox.Size = new System.Drawing.Size(230, 24);
            this.CtrlFlowCheckBox.TabIndex = 4;
            this.CtrlFlowCheckBox.Text = "Control Flow Obfouscastion";
            this.CtrlFlowCheckBox.UseVisualStyleBackColor = true;
            this.CtrlFlowCheckBox.MouseHover += new System.EventHandler(this.checkBox7_MouseHover);
            // 
            // JunkCheckBox
            // 
            this.JunkCheckBox.AutoSize = true;
            this.JunkCheckBox.Location = new System.Drawing.Point(13, 146);
            this.JunkCheckBox.Name = "JunkCheckBox";
            this.JunkCheckBox.Size = new System.Drawing.Size(261, 24);
            this.JunkCheckBox.TabIndex = 3;
            this.JunkCheckBox.Text = "Junk Methods and namespaces";
            this.JunkCheckBox.UseVisualStyleBackColor = true;
            this.JunkCheckBox.MouseHover += new System.EventHandler(this.checkBox6_MouseHover);
            // 
            // FakeObfuscatorAttributesCheckBox
            // 
            this.FakeObfuscatorAttributesCheckBox.AutoSize = true;
            this.FakeObfuscatorAttributesCheckBox.Location = new System.Drawing.Point(13, 95);
            this.FakeObfuscatorAttributesCheckBox.Name = "FakeObfuscatorAttributesCheckBox";
            this.FakeObfuscatorAttributesCheckBox.Size = new System.Drawing.Size(235, 24);
            this.FakeObfuscatorAttributesCheckBox.TabIndex = 2;
            this.FakeObfuscatorAttributesCheckBox.Text = "Fake Obfuscastor Attributes";
            this.FakeObfuscatorAttributesCheckBox.UseVisualStyleBackColor = true;
            this.FakeObfuscatorAttributesCheckBox.MouseHover += new System.EventHandler(this.checkBox5_MouseHover);
            // 
            // AntiDe4DotCheckBox
            // 
            this.AntiDe4DotCheckBox.AutoSize = true;
            this.AntiDe4DotCheckBox.Location = new System.Drawing.Point(13, 51);
            this.AntiDe4DotCheckBox.Name = "AntiDe4DotCheckBox";
            this.AntiDe4DotCheckBox.Size = new System.Drawing.Size(121, 24);
            this.AntiDe4DotCheckBox.TabIndex = 1;
            this.AntiDe4DotCheckBox.Text = "Anti-De4dot";
            this.AntiDe4DotCheckBox.UseVisualStyleBackColor = true;
            this.AntiDe4DotCheckBox.MouseHover += new System.EventHandler(this.checkBox2_MouseHover);
            // 
            // Base64EncodeCheckBox
            // 
            this.Base64EncodeCheckBox.AutoSize = true;
            this.Base64EncodeCheckBox.Location = new System.Drawing.Point(13, 10);
            this.Base64EncodeCheckBox.Name = "Base64EncodeCheckBox";
            this.Base64EncodeCheckBox.Size = new System.Drawing.Size(235, 24);
            this.Base64EncodeCheckBox.TabIndex = 0;
            this.Base64EncodeCheckBox.Text = "Encode Strings with Base64";
            this.Base64EncodeCheckBox.UseVisualStyleBackColor = true;
            this.Base64EncodeCheckBox.MouseHover += new System.EventHandler(this.checkBox1_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "File To Protect";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(138, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(533, 26);
            this.textBox1.TabIndex = 2;
            // 
            // BrowseButton
            // 
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseButton.Location = new System.Drawing.Point(677, 9);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(111, 44);
            this.BrowseButton.TabIndex = 3;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(8, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Obfuscastion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(702, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Licensing";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.HowThisWorksButton);
            this.panel2.Controls.Add(this.USBComboBox);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.USBHWIDCheckBox);
            this.panel2.Controls.Add(this.WhatThisDoButton);
            this.panel2.Controls.Add(this.checkBox4);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.GetCurrentHWIDButton);
            this.panel2.Controls.Add(this.LockToHWIDCheckBox);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(364, 101);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 590);
            this.panel2.TabIndex = 5;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(162, 317);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(244, 26);
            this.textBox4.TabIndex = 14;
            this.textBox4.Text = "License.dat";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "License Filename:";
            // 
            // HowThisWorksButton
            // 
            this.HowThisWorksButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HowThisWorksButton.Location = new System.Drawing.Point(233, 454);
            this.HowThisWorksButton.Name = "HowThisWorksButton";
            this.HowThisWorksButton.Size = new System.Drawing.Size(173, 43);
            this.HowThisWorksButton.TabIndex = 12;
            this.HowThisWorksButton.Text = "How this works?";
            this.HowThisWorksButton.UseVisualStyleBackColor = true;
            this.HowThisWorksButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // USBComboBox
            // 
            this.USBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.USBComboBox.FormattingEnabled = true;
            this.USBComboBox.Location = new System.Drawing.Point(72, 462);
            this.USBComboBox.Name = "USBComboBox";
            this.USBComboBox.Size = new System.Drawing.Size(121, 28);
            this.USBComboBox.TabIndex = 11;
            this.USBComboBox.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 465);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "USB:";
            // 
            // USBHWIDCheckBox
            // 
            this.USBHWIDCheckBox.AutoSize = true;
            this.USBHWIDCheckBox.Location = new System.Drawing.Point(23, 527);
            this.USBHWIDCheckBox.Name = "USBHWIDCheckBox";
            this.USBHWIDCheckBox.Size = new System.Drawing.Size(290, 24);
            this.USBHWIDCheckBox.TabIndex = 9;
            this.USBHWIDCheckBox.Text = "Enable USB Hardware ID Activation";
            this.USBHWIDCheckBox.UseVisualStyleBackColor = true;
            this.USBHWIDCheckBox.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // WhatThisDoButton
            // 
            this.WhatThisDoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WhatThisDoButton.Location = new System.Drawing.Point(103, 186);
            this.WhatThisDoButton.Name = "WhatThisDoButton";
            this.WhatThisDoButton.Size = new System.Drawing.Size(218, 46);
            this.WhatThisDoButton.TabIndex = 8;
            this.WhatThisDoButton.Text = "What this do?";
            this.WhatThisDoButton.UseVisualStyleBackColor = true;
            this.WhatThisDoButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(23, 384);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(270, 24);
            this.checkBox4.TabIndex = 7;
            this.checkBox4.Text = "Enable Registration With Licence";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(93, 275);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(313, 26);
            this.textBox3.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "License:";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(6, 440);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(417, 1);
            this.panel4.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(2, 251);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(417, 1);
            this.panel3.TabIndex = 4;
            // 
            // GetCurrentHWIDButton
            // 
            this.GetCurrentHWIDButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetCurrentHWIDButton.Location = new System.Drawing.Point(23, 64);
            this.GetCurrentHWIDButton.Name = "GetCurrentHWIDButton";
            this.GetCurrentHWIDButton.Size = new System.Drawing.Size(284, 47);
            this.GetCurrentHWIDButton.TabIndex = 3;
            this.GetCurrentHWIDButton.Text = "Get Current PC HWID";
            this.GetCurrentHWIDButton.UseVisualStyleBackColor = true;
            this.GetCurrentHWIDButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // LockToHWIDCheckBox
            // 
            this.LockToHWIDCheckBox.AutoSize = true;
            this.LockToHWIDCheckBox.Location = new System.Drawing.Point(23, 137);
            this.LockToHWIDCheckBox.Name = "LockToHWIDCheckBox";
            this.LockToHWIDCheckBox.Size = new System.Drawing.Size(229, 24);
            this.LockToHWIDCheckBox.TabIndex = 2;
            this.LockToHWIDCheckBox.Text = "Lock to the specified HWID";
            this.LockToHWIDCheckBox.UseVisualStyleBackColor = true;
            this.LockToHWIDCheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(82, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(324, 26);
            this.textBox2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "HWID:";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(74, 705);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(155, 39);
            this.button3.TabIndex = 6;
            this.button3.Text = "Protect";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 752);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "NetShield Protector";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button GetCurrentHWIDButton;
        private System.Windows.Forms.CheckBox LockToHWIDCheckBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox FakeObfuscatorAttributesCheckBox;
        private System.Windows.Forms.CheckBox AntiDe4DotCheckBox;
        private System.Windows.Forms.CheckBox Base64EncodeCheckBox;
        private System.Windows.Forms.CheckBox CtrlFlowCheckBox;
        private System.Windows.Forms.CheckBox JunkCheckBox;
        private System.Windows.Forms.CheckBox RenamingCheckBox;
        private System.Windows.Forms.Button WhatThisDoButton;
        private System.Windows.Forms.ComboBox USBComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox USBHWIDCheckBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button HowThisWorksButton;
        private System.Windows.Forms.CheckBox INTConfusionCheckBox;
        private System.Windows.Forms.CheckBox AntiILDasmCheckBox;
        private System.Windows.Forms.CheckBox PackingCheckBox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox AntiVMCheckBox;
        private System.Windows.Forms.CheckBox AntiDebugCheckBox;
        private System.Windows.Forms.CheckBox AntiDecompilerCheckBox;
    }
}