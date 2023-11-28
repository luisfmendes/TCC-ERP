namespace TCCAcai.Apresentacao
{
    partial class frmConsultaClientes
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
            DevComponents.DotNetBar.SuperGrid.Style.Background background4 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.Style.Background background5 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.Style.Background background6 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            this.grdDadosClientes = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.id_cliente = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.nome_cliente = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.ativo = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.coluna_visualizar = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.txtNomeFiltro = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rabTodos = new System.Windows.Forms.RadioButton();
            this.rabInativo = new System.Windows.Forms.RadioButton();
            this.rabAtivo = new System.Windows.Forms.RadioButton();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.rabNomeContem = new System.Windows.Forms.RadioButton();
            this.rabNomeInicia = new System.Windows.Forms.RadioButton();
            this.rabNomeTermina = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Text = "Consulta de Clientes";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.btnFiltrar);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.txtNomeFiltro);
            this.panel2.Controls.Add(this.grdDadosClientes);
            this.panel2.Controls.SetChildIndex(this.btnFechar, 0);
            this.panel2.Controls.SetChildIndex(this.grdDadosClientes, 0);
            this.panel2.Controls.SetChildIndex(this.txtNomeFiltro, 0);
            this.panel2.Controls.SetChildIndex(this.groupBox1, 0);
            this.panel2.Controls.SetChildIndex(this.btnFiltrar, 0);
            this.panel2.Controls.SetChildIndex(this.groupBox2, 0);
            // 
            // grdDadosClientes
            // 
            this.grdDadosClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDadosClientes.BackColor = System.Drawing.Color.White;
            this.grdDadosClientes.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.grdDadosClientes.ForeColor = System.Drawing.Color.Black;
            this.grdDadosClientes.Location = new System.Drawing.Point(3, 105);
            this.grdDadosClientes.Name = "grdDadosClientes";
            // 
            // 
            // 
            this.grdDadosClientes.PrimaryGrid.AutoGenerateColumns = false;
            // 
            // 
            // 
            this.grdDadosClientes.PrimaryGrid.ColumnHeader.RowHeight = 25;
            this.grdDadosClientes.PrimaryGrid.Columns.Add(this.id_cliente);
            this.grdDadosClientes.PrimaryGrid.Columns.Add(this.nome_cliente);
            this.grdDadosClientes.PrimaryGrid.Columns.Add(this.ativo);
            this.grdDadosClientes.PrimaryGrid.Columns.Add(this.coluna_visualizar);
            background4.Color1 = System.Drawing.Color.LightSlateGray;
            this.grdDadosClientes.PrimaryGrid.DefaultVisualStyles.CellStyles.Selected.Background = background4;
            background5.Color1 = System.Drawing.Color.Thistle;
            background5.Color2 = System.Drawing.Color.LavenderBlush;
            this.grdDadosClientes.PrimaryGrid.DefaultVisualStyles.ColumnHeaderStyles.Default.Background = background5;
            this.grdDadosClientes.PrimaryGrid.DefaultVisualStyles.GridPanelStyle.TextColor = System.Drawing.Color.Black;
            background6.Color1 = System.Drawing.Color.LavenderBlush;
            this.grdDadosClientes.PrimaryGrid.DefaultVisualStyles.RowStyles.Selected.Background = background6;
            this.grdDadosClientes.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.grdDadosClientes.PrimaryGrid.MultiSelect = false;
            this.grdDadosClientes.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.grdDadosClientes.PrimaryGrid.UseAlternateRowStyle = true;
            this.grdDadosClientes.Size = new System.Drawing.Size(793, 307);
            this.grdDadosClientes.TabIndex = 3;
            this.grdDadosClientes.Text = "superGridControl1";
            this.grdDadosClientes.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.grdDadosClientes_CellClick);
            // 
            // id_cliente
            // 
            this.id_cliente.AllowEdit = false;
            this.id_cliente.CellStyles.Default.TextColor = System.Drawing.Color.Black;
            this.id_cliente.CellStyles.MouseOver.TextColor = System.Drawing.Color.Black;
            this.id_cliente.CellStyles.NotSelectable.TextColor = System.Drawing.Color.Black;
            this.id_cliente.CellStyles.SelectedMouseOver.TextColor = System.Drawing.Color.Black;
            this.id_cliente.HeaderText = "Código";
            this.id_cliente.Name = "id_cliente";
            // 
            // nome_cliente
            // 
            this.nome_cliente.AllowEdit = false;
            this.nome_cliente.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.nome_cliente.CellStyles.Default.TextColor = System.Drawing.Color.Black;
            this.nome_cliente.CellStyles.MouseOver.TextColor = System.Drawing.Color.Black;
            this.nome_cliente.CellStyles.NotSelectable.TextColor = System.Drawing.Color.Black;
            this.nome_cliente.CellStyles.SelectedMouseOver.TextColor = System.Drawing.Color.Black;
            this.nome_cliente.HeaderText = "Nome";
            this.nome_cliente.Name = "nome_cliente";
            // 
            // ativo
            // 
            this.ativo.AllowEdit = false;
            this.ativo.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.ColumnHeader;
            this.ativo.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.ativo.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.ativo.HeaderText = "Ativo";
            this.ativo.Name = "ativo";
            // 
            // coluna_visualizar
            // 
            this.coluna_visualizar.AllowEdit = false;
            this.coluna_visualizar.CellStyles.Default.Image = global::TCCAcai.Properties.Resources.Search_16x16;
            this.coluna_visualizar.CellStyles.Default.ImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.coluna_visualizar.HeaderStyles.Default.ImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.coluna_visualizar.HeaderText = " ";
            this.coluna_visualizar.Name = "coluna_visualizar";
            this.coluna_visualizar.Width = 30;
            // 
            // txtNomeFiltro
            // 
            this.txtNomeFiltro.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtNomeFiltro.Border.Class = "TextBoxBorder";
            this.txtNomeFiltro.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNomeFiltro.DisabledBackColor = System.Drawing.Color.White;
            this.txtNomeFiltro.ForeColor = System.Drawing.Color.Black;
            this.txtNomeFiltro.Location = new System.Drawing.Point(11, 73);
            this.txtNomeFiltro.Name = "txtNomeFiltro";
            this.txtNomeFiltro.PreventEnterBeep = true;
            this.txtNomeFiltro.Size = new System.Drawing.Size(327, 26);
            this.txtNomeFiltro.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rabTodos);
            this.groupBox1.Controls.Add(this.rabInativo);
            this.groupBox1.Controls.Add(this.rabAtivo);
            this.groupBox1.Location = new System.Drawing.Point(344, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 67);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // rabTodos
            // 
            this.rabTodos.AutoSize = true;
            this.rabTodos.Checked = true;
            this.rabTodos.ForeColor = System.Drawing.Color.Black;
            this.rabTodos.Location = new System.Drawing.Point(190, 29);
            this.rabTodos.Name = "rabTodos";
            this.rabTodos.Size = new System.Drawing.Size(71, 24);
            this.rabTodos.TabIndex = 2;
            this.rabTodos.TabStop = true;
            this.rabTodos.Text = "Todos";
            this.rabTodos.UseVisualStyleBackColor = true;
            // 
            // rabInativo
            // 
            this.rabInativo.AutoSize = true;
            this.rabInativo.ForeColor = System.Drawing.Color.Black;
            this.rabInativo.Location = new System.Drawing.Point(102, 29);
            this.rabInativo.Name = "rabInativo";
            this.rabInativo.Size = new System.Drawing.Size(82, 24);
            this.rabInativo.TabIndex = 1;
            this.rabInativo.Text = "Inativos";
            this.rabInativo.UseVisualStyleBackColor = true;
            // 
            // rabAtivo
            // 
            this.rabAtivo.AutoSize = true;
            this.rabAtivo.ForeColor = System.Drawing.Color.Black;
            this.rabAtivo.Location = new System.Drawing.Point(26, 29);
            this.rabAtivo.Name = "rabAtivo";
            this.rabAtivo.Size = new System.Drawing.Size(70, 24);
            this.rabAtivo.TabIndex = 0;
            this.rabAtivo.Text = "Ativos";
            this.rabAtivo.UseVisualStyleBackColor = true;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.Honeydew;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnFiltrar.Image = global::TCCAcai.Properties.Resources.filtro_24x24;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Location = new System.Drawing.Point(694, 52);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(93, 35);
            this.btnFiltrar.TabIndex = 9;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // rabNomeContem
            // 
            this.rabNomeContem.AutoSize = true;
            this.rabNomeContem.Checked = true;
            this.rabNomeContem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabNomeContem.ForeColor = System.Drawing.Color.Black;
            this.rabNomeContem.Location = new System.Drawing.Point(6, 11);
            this.rabNomeContem.Name = "rabNomeContem";
            this.rabNomeContem.Size = new System.Drawing.Size(113, 20);
            this.rabNomeContem.TabIndex = 3;
            this.rabNomeContem.TabStop = true;
            this.rabNomeContem.Text = "Nome contem:";
            this.rabNomeContem.UseVisualStyleBackColor = true;
            // 
            // rabNomeInicia
            // 
            this.rabNomeInicia.AutoSize = true;
            this.rabNomeInicia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabNomeInicia.ForeColor = System.Drawing.Color.Black;
            this.rabNomeInicia.Location = new System.Drawing.Point(125, 11);
            this.rabNomeInicia.Name = "rabNomeInicia";
            this.rabNomeInicia.Size = new System.Drawing.Size(89, 20);
            this.rabNomeInicia.TabIndex = 10;
            this.rabNomeInicia.Text = "Inicia com:";
            this.rabNomeInicia.UseVisualStyleBackColor = true;
            // 
            // rabNomeTermina
            // 
            this.rabNomeTermina.AutoSize = true;
            this.rabNomeTermina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabNomeTermina.ForeColor = System.Drawing.Color.Black;
            this.rabNomeTermina.Location = new System.Drawing.Point(214, 11);
            this.rabNomeTermina.Name = "rabNomeTermina";
            this.rabNomeTermina.Size = new System.Drawing.Size(108, 20);
            this.rabNomeTermina.TabIndex = 11;
            this.rabNomeTermina.Text = "Termina com:";
            this.rabNomeTermina.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rabNomeContem);
            this.groupBox2.Controls.Add(this.rabNomeTermina);
            this.groupBox2.Controls.Add(this.rabNomeInicia);
            this.groupBox2.Location = new System.Drawing.Point(11, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 37);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // frmConsultaClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "frmConsultaClientes";
            this.Text = "frmConsultaClientes";
            this.Load += new System.EventHandler(this.frmConsultaClientes_Load);
            this.VisibleChanged += new System.EventHandler(this.frmConsultaClientes_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl grdDadosClientes;
        private DevComponents.DotNetBar.SuperGrid.GridColumn id_cliente;
        private DevComponents.DotNetBar.SuperGrid.GridColumn nome_cliente;
        private DevComponents.DotNetBar.SuperGrid.GridColumn ativo;
        private DevComponents.DotNetBar.SuperGrid.GridColumn coluna_visualizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rabTodos;
        private System.Windows.Forms.RadioButton rabInativo;
        private System.Windows.Forms.RadioButton rabAtivo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNomeFiltro;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rabNomeContem;
        private System.Windows.Forms.RadioButton rabNomeTermina;
        private System.Windows.Forms.RadioButton rabNomeInicia;
    }
}