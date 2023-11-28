namespace TCCAcai.Apresentacao
{
    partial class frmCompras
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
            DevComponents.DotNetBar.SuperGrid.Style.Background background5 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.Style.Background background6 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.Style.Background background7 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.Style.Background background8 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.Style.BorderColor borderColor2 = new DevComponents.DotNetBar.SuperGrid.Style.BorderColor();
            this.txtDescricao = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCodigo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQtd = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grdProdutos = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.id_produto = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.nome_produto = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.preco_compra = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.quantidade = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.total = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.colunaAlterar = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.colunaExcluir = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbFornecedor = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnGravar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Size = new System.Drawing.Size(1228, 27);
            this.label1.Text = "Compras";
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(4192, 0);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(1228, 450);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGravar);
            this.panel2.Controls.Add(this.cmbFornecedor);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.grdProdutos);
            this.panel2.Controls.Add(this.txtDescricao);
            this.panel2.Controls.Add(this.txtCodigo);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtValor);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnAdicionar);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtQtd);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.None;
            this.panel2.Size = new System.Drawing.Size(1228, 450);
            this.panel2.Controls.SetChildIndex(this.btnFechar, 0);
            this.panel2.Controls.SetChildIndex(this.label6, 0);
            this.panel2.Controls.SetChildIndex(this.label7, 0);
            this.panel2.Controls.SetChildIndex(this.txtTotal, 0);
            this.panel2.Controls.SetChildIndex(this.label5, 0);
            this.panel2.Controls.SetChildIndex(this.txtQtd, 0);
            this.panel2.Controls.SetChildIndex(this.label8, 0);
            this.panel2.Controls.SetChildIndex(this.btnAdicionar, 0);
            this.panel2.Controls.SetChildIndex(this.label4, 0);
            this.panel2.Controls.SetChildIndex(this.txtValor, 0);
            this.panel2.Controls.SetChildIndex(this.label3, 0);
            this.panel2.Controls.SetChildIndex(this.label2, 0);
            this.panel2.Controls.SetChildIndex(this.txtCodigo, 0);
            this.panel2.Controls.SetChildIndex(this.txtDescricao, 0);
            this.panel2.Controls.SetChildIndex(this.grdProdutos, 0);
            this.panel2.Controls.SetChildIndex(this.label9, 0);
            this.panel2.Controls.SetChildIndex(this.cmbFornecedor, 0);
            this.panel2.Controls.SetChildIndex(this.btnGravar, 0);
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
            this.txtDescricao.ForeColor = System.Drawing.Color.Black;
            this.txtDescricao.Location = new System.Drawing.Point(258, 66);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.PreventEnterBeep = true;
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.Size = new System.Drawing.Size(342, 26);
            this.txtDescricao.TabIndex = 19;
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
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(3, 65);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PreventEnterBeep = true;
            this.txtCodigo.Size = new System.Drawing.Size(249, 26);
            this.txtCodigo.TabIndex = 18;
            this.txtCodigo.Validated += new System.EventHandler(this.txtCodigo_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Código do produto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(605, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Valor";
            // 
            // txtValor
            // 
            this.txtValor.DecimalPlaces = 2;
            this.txtValor.Enabled = false;
            this.txtValor.Location = new System.Drawing.Point(606, 65);
            this.txtValor.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.txtValor.Name = "txtValor";
            this.txtValor.ReadOnly = true;
            this.txtValor.Size = new System.Drawing.Size(110, 26);
            this.txtValor.TabIndex = 20;
            this.txtValor.TabStop = false;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(722, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "X";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.BackColor = System.Drawing.Color.Honeydew;
            this.btnAdicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionar.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnAdicionar.Location = new System.Drawing.Point(1117, 64);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(106, 26);
            this.btnAdicionar.TabIndex = 25;
            this.btnAdicionar.Tag = "I";
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = false;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(255, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 16);
            this.label8.TabIndex = 30;
            this.label8.Text = "Descrição do produto";
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(744, 65);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(76, 26);
            this.txtQtd.TabIndex = 21;
            this.txtQtd.ValueChanged += new System.EventHandler(this.txtQtd_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(741, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "Qtd";
            // 
            // txtTotal
            // 
            this.txtTotal.DecimalPlaces = 2;
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(847, 65);
            this.txtTotal.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(110, 26);
            this.txtTotal.TabIndex = 23;
            this.txtTotal.TabStop = false;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(844, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "Total";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(826, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 16);
            this.label6.TabIndex = 28;
            this.label6.Text = "=";
            // 
            // grdProdutos
            // 
            this.grdProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProdutos.BackColor = System.Drawing.Color.White;
            background5.Color1 = System.Drawing.SystemColors.ControlLight;
            this.grdProdutos.DefaultVisualStyles.AlternateRowCellStyles.Default.Background = background5;
            background6.Color1 = System.Drawing.Color.Red;
            this.grdProdutos.DefaultVisualStyles.CellStyles.Selected.Background = background6;
            background7.Color1 = System.Drawing.Color.Wheat;
            this.grdProdutos.DefaultVisualStyles.CellStyles.SelectedMouseOver.Background = background7;
            background8.Color1 = System.Drawing.Color.Wheat;
            background8.Color2 = System.Drawing.Color.Gainsboro;
            this.grdProdutos.DefaultVisualStyles.ColumnHeaderStyles.Default.Background = background8;
            this.grdProdutos.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProdutos.DefaultVisualStyles.ColumnHeaderStyles.Default.TextColor = System.Drawing.SystemColors.ControlText;
            borderColor2.Bottom = System.Drawing.Color.DarkGray;
            borderColor2.Left = System.Drawing.Color.DarkGray;
            borderColor2.Right = System.Drawing.Color.DarkGray;
            borderColor2.Top = System.Drawing.Color.DarkGray;
            this.grdProdutos.DefaultVisualStyles.GridPanelStyle.BorderColor = borderColor2;
            this.grdProdutos.DefaultVisualStyles.GridPanelStyle.HeaderLineColor = System.Drawing.Color.Silver;
            this.grdProdutos.DefaultVisualStyles.GridPanelStyle.HorizontalLineColor = System.Drawing.Color.DarkGray;
            this.grdProdutos.DefaultVisualStyles.GridPanelStyle.VerticalLineColor = System.Drawing.Color.DarkGray;
            this.grdProdutos.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.grdProdutos.ForeColor = System.Drawing.Color.Black;
            this.grdProdutos.Location = new System.Drawing.Point(2, 98);
            this.grdProdutos.Name = "grdProdutos";
            // 
            // 
            // 
            this.grdProdutos.PrimaryGrid.AutoGenerateColumns = false;
            // 
            // 
            // 
            this.grdProdutos.PrimaryGrid.ColumnHeader.RowHeight = 25;
            this.grdProdutos.PrimaryGrid.ColumnHeader.SortImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleRight;
            this.grdProdutos.PrimaryGrid.Columns.Add(this.id_produto);
            this.grdProdutos.PrimaryGrid.Columns.Add(this.nome_produto);
            this.grdProdutos.PrimaryGrid.Columns.Add(this.preco_compra);
            this.grdProdutos.PrimaryGrid.Columns.Add(this.quantidade);
            this.grdProdutos.PrimaryGrid.Columns.Add(this.total);
            this.grdProdutos.PrimaryGrid.Columns.Add(this.colunaAlterar);
            this.grdProdutos.PrimaryGrid.Columns.Add(this.colunaExcluir);
            this.grdProdutos.PrimaryGrid.EnableColumnFiltering = true;
            this.grdProdutos.PrimaryGrid.EnterKeySelectsNextRow = false;
            // 
            // 
            // 
            this.grdProdutos.PrimaryGrid.Footer.RowHeaderVisibility = DevComponents.DotNetBar.SuperGrid.RowHeaderVisibility.Never;
            this.grdProdutos.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.grdProdutos.PrimaryGrid.MultiSelect = false;
            this.grdProdutos.PrimaryGrid.RowHeaderWidth = 15;
            this.grdProdutos.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            // 
            // 
            // 
            this.grdProdutos.PrimaryGrid.Title.Text = "Produtos";
            this.grdProdutos.PrimaryGrid.UseAlternateRowStyle = true;
            this.grdProdutos.ShowCustomFilterHelp = false;
            this.grdProdutos.Size = new System.Drawing.Size(1221, 309);
            this.grdProdutos.TabIndex = 31;
            this.grdProdutos.Text = "superGridControl1";
            this.grdProdutos.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.grdProdutos_CellClick);
            // 
            // id_produto
            // 
            this.id_produto.HeaderText = "Código";
            this.id_produto.Name = "id_produto";
            this.id_produto.ReadOnly = true;
            // 
            // nome_produto
            // 
            this.nome_produto.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.nome_produto.HeaderText = "Nome";
            this.nome_produto.Name = "nome_produto";
            this.nome_produto.ReadOnly = true;
            // 
            // preco_compra
            // 
            this.preco_compra.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.preco_compra.HeaderText = "Preço";
            this.preco_compra.Name = "preco_compra";
            this.preco_compra.ReadOnly = true;
            // 
            // quantidade
            // 
            this.quantidade.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridNumericUpDownEditControl);
            this.quantidade.HeaderText = "Qtd.";
            this.quantidade.Name = "quantidade";
            this.quantidade.ReadOnly = true;
            // 
            // total
            // 
            this.total.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // colunaAlterar
            // 
            this.colunaAlterar.CellStyles.Default.Image = global::TCCAcai.Properties.Resources.update;
            this.colunaAlterar.CellStyles.Default.ImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.colunaAlterar.HeaderText = " ";
            this.colunaAlterar.Name = "colunaAlterar";
            this.colunaAlterar.ReadOnly = true;
            this.colunaAlterar.Width = 40;
            // 
            // colunaExcluir
            // 
            this.colunaExcluir.CellStyles.Default.Image = global::TCCAcai.Properties.Resources.excluir_16x16;
            this.colunaExcluir.CellStyles.Default.ImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.colunaExcluir.HeaderText = " ";
            this.colunaExcluir.Name = "colunaExcluir";
            this.colunaExcluir.ReadOnly = true;
            this.colunaExcluir.Width = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(763, 420);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 33;
            this.label9.Text = "Fornecedor";
            // 
            // cmbFornecedor
            // 
            this.cmbFornecedor.DisplayMember = "nome_contato";
            this.cmbFornecedor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFornecedor.DropDownColumns = "nome_contato";
            this.cmbFornecedor.DropDownColumnsHeaders = "Fornecedor";
            this.cmbFornecedor.ForeColor = System.Drawing.Color.Black;
            this.cmbFornecedor.FormattingEnabled = true;
            this.cmbFornecedor.ItemHeight = 20;
            this.cmbFornecedor.Location = new System.Drawing.Point(847, 413);
            this.cmbFornecedor.Name = "cmbFornecedor";
            this.cmbFornecedor.Size = new System.Drawing.Size(251, 26);
            this.cmbFornecedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.cmbFornecedor.TabIndex = 34;
            this.cmbFornecedor.ValueMember = "id_fornecedor";
            // 
            // btnGravar
            // 
            this.btnGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGravar.BackColor = System.Drawing.Color.Honeydew;
            this.btnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnGravar.Location = new System.Drawing.Point(1104, 410);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(119, 35);
            this.btnGravar.TabIndex = 35;
            this.btnGravar.Tag = "I";
            this.btnGravar.Text = "&Gravar [F12]";
            this.btnGravar.UseVisualStyleBackColor = false;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // frmCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 450);
            this.KeyPreview = true;
            this.Name = "frmCompras";
            this.ProcessTab = true;
            this.Text = "frmCompras";
            this.VisibleChanged += new System.EventHandler(this.frmCompras_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCompras_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtDescricao;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtValor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtQtd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl grdProdutos;
        private DevComponents.DotNetBar.SuperGrid.GridColumn id_produto;
        private DevComponents.DotNetBar.SuperGrid.GridColumn nome_produto;
        private DevComponents.DotNetBar.SuperGrid.GridColumn preco_compra;
        private DevComponents.DotNetBar.SuperGrid.GridColumn quantidade;
        private DevComponents.DotNetBar.SuperGrid.GridColumn total;
        private DevComponents.DotNetBar.SuperGrid.GridColumn colunaAlterar;
        private DevComponents.DotNetBar.SuperGrid.GridColumn colunaExcluir;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFornecedor;
        private System.Windows.Forms.Button btnGravar;
    }
}