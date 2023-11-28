namespace TCCAcai.Apresentacao
{
    partial class frmProdutos
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtPrecoCompra = new System.Windows.Forms.NumericUpDown();
            this.txtPrecoVenda = new System.Windows.Forms.NumericUpDown();
            this.txtDescricao = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkAtivo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtNome = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecoCompra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecoVenda)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Text = "Produtos";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExcluir);
            this.panel2.Controls.Add(this.btnAlterar);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.btnGravar);
            this.panel2.Controls.Add(this.groupPanel1);
            this.panel2.Controls.SetChildIndex(this.btnFechar, 0);
            this.panel2.Controls.SetChildIndex(this.groupPanel1, 0);
            this.panel2.Controls.SetChildIndex(this.btnGravar, 0);
            this.panel2.Controls.SetChildIndex(this.btnCancelar, 0);
            this.panel2.Controls.SetChildIndex(this.btnAlterar, 0);
            this.panel2.Controls.SetChildIndex(this.btnExcluir, 0);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.MediumPurple;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtPrecoCompra);
            this.groupPanel1.Controls.Add(this.txtPrecoVenda);
            this.groupPanel1.Controls.Add(this.txtDescricao);
            this.groupPanel1.Controls.Add(this.chkAtivo);
            this.groupPanel1.Controls.Add(this.label7);
            this.groupPanel1.Controls.Add(this.label5);
            this.groupPanel1.Controls.Add(this.label4);
            this.groupPanel1.Controls.Add(this.label3);
            this.groupPanel1.Controls.Add(this.label2);
            this.groupPanel1.Controls.Add(this.txtCodigo);
            this.groupPanel1.Controls.Add(this.txtNome);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(17, 48);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(764, 353);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.groupPanel1.Style.BackColor2 = System.Drawing.Color.Plum;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 6;
            // 
            // txtPrecoCompra
            // 
            this.txtPrecoCompra.DecimalPlaces = 2;
            this.highlighter1.SetHighlightOnFocus(this.txtPrecoCompra, true);
            this.txtPrecoCompra.Location = new System.Drawing.Point(48, 258);
            this.txtPrecoCompra.Name = "txtPrecoCompra";
            this.txtPrecoCompra.Size = new System.Drawing.Size(120, 26);
            this.txtPrecoCompra.TabIndex = 5;
            this.txtPrecoCompra.Enter += new System.EventHandler(this.numericUpDown_SelectAll);
            this.txtPrecoCompra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtNumericUpDown_MouseUp);
            // 
            // txtPrecoVenda
            // 
            this.txtPrecoVenda.DecimalPlaces = 2;
            this.highlighter1.SetHighlightOnFocus(this.txtPrecoVenda, true);
            this.txtPrecoVenda.Location = new System.Drawing.Point(48, 206);
            this.txtPrecoVenda.Name = "txtPrecoVenda";
            this.txtPrecoVenda.Size = new System.Drawing.Size(120, 26);
            this.txtPrecoVenda.TabIndex = 4;
            this.txtPrecoVenda.Enter += new System.EventHandler(this.numericUpDown_SelectAll);
            this.txtPrecoVenda.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtNumericUpDown_MouseUp);
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDescricao.Border.Class = "TextBoxBorder";
            this.txtDescricao.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDescricao.DisabledBackColor = System.Drawing.Color.White;
            this.txtDescricao.FocusHighlightEnabled = true;
            this.txtDescricao.ForeColor = System.Drawing.Color.Black;
            this.highlighter1.SetHighlightOnFocus(this.txtDescricao, true);
            this.txtDescricao.Location = new System.Drawing.Point(49, 152);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.PreventEnterBeep = true;
            this.txtDescricao.Size = new System.Drawing.Size(625, 26);
            this.txtDescricao.TabIndex = 3;
            // 
            // chkAtivo
            // 
            this.chkAtivo.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkAtivo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.CheckValue = "Y";
            this.highlighter1.SetHighlightOnFocus(this.chkAtivo, true);
            this.chkAtivo.Location = new System.Drawing.Point(698, 13);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(57, 23);
            this.chkAtivo.TabIndex = 7;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.CheckedChanged += new System.EventHandler(this.chkAtivo_CheckedChanged);
            this.chkAtivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkAtivo_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(45, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Preço de Venda";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(45, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Preço de Compra";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(45, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Descrição";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(45, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(45, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Código";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCodigo.Border.Class = "TextBoxBorder";
            this.txtCodigo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCodigo.DisabledBackColor = System.Drawing.Color.White;
            this.txtCodigo.FocusHighlightEnabled = true;
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.highlighter1.SetHighlightOnFocus(this.txtCodigo, true);
            this.txtCodigo.Location = new System.Drawing.Point(49, 36);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PreventEnterBeep = true;
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(165, 26);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.TabStop = false;
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtNome.Border.Class = "TextBoxBorder";
            this.txtNome.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNome.DisabledBackColor = System.Drawing.Color.White;
            this.txtNome.FocusHighlightEnabled = true;
            this.txtNome.ForeColor = System.Drawing.Color.Black;
            this.highlighter1.SetHighlightOnFocus(this.txtNome, true);
            this.txtNome.Location = new System.Drawing.Point(49, 90);
            this.txtNome.Name = "txtNome";
            this.txtNome.PreventEnterBeep = true;
            this.txtNome.Size = new System.Drawing.Size(625, 26);
            this.txtNome.TabIndex = 2;
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.Color.Crimson;
            this.btnExcluir.Location = new System.Drawing.Point(402, 407);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(93, 35);
            this.btnExcluir.TabIndex = 15;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.BackColor = System.Drawing.Color.Moccasin;
            this.btnAlterar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnAlterar.Location = new System.Drawing.Point(259, 407);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(93, 35);
            this.btnAlterar.TabIndex = 14;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.MistyRose;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Maroon;
            this.btnCancelar.Location = new System.Drawing.Point(588, 407);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(93, 35);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.BackColor = System.Drawing.Color.Honeydew;
            this.btnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnGravar.Location = new System.Drawing.Point(687, 407);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(93, 35);
            this.btnGravar.TabIndex = 12;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = false;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // highlighter1
            // 
            this.highlighter1.ContainerControl = this;
            this.highlighter1.FocusHighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            // 
            // frmProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.KeyPreview = true;
            this.Name = "frmProdutos";
            this.ProcessTab = true;
            this.Text = "frmProdutos";
            this.Load += new System.EventHandler(this.frmProdutos_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecoCompra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecoVenda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDescricao;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAtivo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCodigo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNome;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.NumericUpDown txtPrecoCompra;
        private System.Windows.Forms.NumericUpDown txtPrecoVenda;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
    }
}