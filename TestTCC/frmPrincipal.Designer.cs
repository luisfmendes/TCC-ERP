namespace TestTCC
{
    partial class frmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.SuperTabGeral = new DevComponents.DotNetBar.Controls.SideNav();
            this.sideNavPanel3 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.reflectionLabel3 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.metroTilePanel3 = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.btnVenda = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnCompra = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnConsultaCompra = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.sideNavPanel2 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.reflectionLabel1 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.metroTilePanel2 = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.btnVerCliente = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnVerFornecedor = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnVerProduto = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.sideNavPanel1 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.reflectionLabel2 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.metroTilePanel1 = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.btnCadCliente = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnCadFornecedor = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.btnCadProduto = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.sideNavItem1 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.separator1 = new DevComponents.DotNetBar.Separator();
            this.tabCadastros = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.tabConsulta = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.tabNegocio = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.separator2 = new DevComponents.DotNetBar.Separator();
            this.metroStatusBar1 = new DevComponents.DotNetBar.Metro.MetroStatusBar();
            this.lblHora = new DevComponents.DotNetBar.LabelItem();
            this.timerHora = new System.Windows.Forms.Timer(this.components);
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuperTabGeral.SuspendLayout();
            this.sideNavPanel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.sideNavPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.sideNavPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(26, 0);
            this.label1.Size = new System.Drawing.Size(774, 25);
            this.label1.Text = "Açai Francatto";
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(11, 276);
            this.btnFechar.Visible = false;
            // 
            // btnSair
            // 
            this.btnSair.Size = new System.Drawing.Size(31, 24);
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.Controls.SetChildIndex(this.label1, 0);
            this.panel1.Controls.SetChildIndex(this.btnMinimizar, 0);
            this.panel1.Controls.SetChildIndex(this.btnSair, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.metroStatusBar1);
            this.panel2.Controls.Add(this.SuperTabGeral);
            this.panel2.Controls.SetChildIndex(this.SuperTabGeral, 0);
            this.panel2.Controls.SetChildIndex(this.metroStatusBar1, 0);
            this.panel2.Controls.SetChildIndex(this.btnFechar, 0);
            this.panel2.Controls.SetChildIndex(this.pictureBox1, 0);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // SuperTabGeral
            // 
            this.SuperTabGeral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SuperTabGeral.Controls.Add(this.sideNavPanel3);
            this.SuperTabGeral.Controls.Add(this.sideNavPanel2);
            this.SuperTabGeral.Controls.Add(this.sideNavPanel1);
            this.SuperTabGeral.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.sideNavItem1,
            this.separator1,
            this.tabCadastros,
            this.tabConsulta,
            this.tabNegocio,
            this.separator2});
            this.SuperTabGeral.Location = new System.Drawing.Point(0, 24);
            this.SuperTabGeral.Name = "SuperTabGeral";
            this.SuperTabGeral.Padding = new System.Windows.Forms.Padding(1);
            this.SuperTabGeral.Size = new System.Drawing.Size(799, 401);
            this.SuperTabGeral.TabIndex = 3;
            this.SuperTabGeral.Text = "sideNav1";
            // 
            // sideNavPanel3
            // 
            this.sideNavPanel3.Controls.Add(this.panel5);
            this.sideNavPanel3.Controls.Add(this.metroTilePanel3);
            this.sideNavPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNavPanel3.Location = new System.Drawing.Point(121, 36);
            this.sideNavPanel3.Name = "sideNavPanel3";
            this.sideNavPanel3.Size = new System.Drawing.Size(673, 364);
            this.sideNavPanel3.TabIndex = 13;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Thistle;
            this.panel5.Controls.Add(this.reflectionLabel3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(673, 78);
            this.panel5.TabIndex = 7;
            // 
            // reflectionLabel3
            // 
            this.reflectionLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.reflectionLabel3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.reflectionLabel3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.reflectionLabel3.ForeColor = System.Drawing.Color.Black;
            this.reflectionLabel3.Location = new System.Drawing.Point(243, 5);
            this.reflectionLabel3.Name = "reflectionLabel3";
            this.reflectionLabel3.Size = new System.Drawing.Size(156, 70);
            this.reflectionLabel3.TabIndex = 4;
            this.reflectionLabel3.Text = "<span style=\"text-align: center;\"><b><font size=\"+15\">Negócios</font></b></span>";
            // 
            // metroTilePanel3
            // 
            this.metroTilePanel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.metroTilePanel3.BackgroundStyle.Class = "MetroTilePanel";
            this.metroTilePanel3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroTilePanel3.ContainerControlProcessDialogKey = true;
            this.metroTilePanel3.DragDropSupport = true;
            this.metroTilePanel3.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnVenda,
            this.btnCompra,
            this.btnConsultaCompra});
            this.metroTilePanel3.Location = new System.Drawing.Point(23, 130);
            this.metroTilePanel3.Name = "metroTilePanel3";
            this.metroTilePanel3.Size = new System.Drawing.Size(643, 128);
            this.metroTilePanel3.TabIndex = 2;
            this.metroTilePanel3.Text = "metroTilePanel3";
            // 
            // btnVenda
            // 
            this.btnVenda.Name = "btnVenda";
            this.btnVenda.Symbol = "59596";
            this.btnVenda.SymbolColor = System.Drawing.Color.Empty;
            this.btnVenda.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnVenda.Text = "Venda";
            this.btnVenda.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnVenda.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnVenda.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVenda.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnVenda.Click += new System.EventHandler(this.btnVenda_Click);
            // 
            // btnCompra
            // 
            this.btnCompra.Name = "btnCompra";
            this.btnCompra.Symbol = "58686";
            this.btnCompra.SymbolColor = System.Drawing.Color.Empty;
            this.btnCompra.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnCompra.Text = "Compra";
            this.btnCompra.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnCompra.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnCompra.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnCompra.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnCompra.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCompra.Click += new System.EventHandler(this.btnCompra_Click);
            // 
            // btnConsultaCompra
            // 
            this.btnConsultaCompra.Name = "btnConsultaCompra";
            this.btnConsultaCompra.Symbol = "";
            this.btnConsultaCompra.SymbolColor = System.Drawing.Color.Empty;
            this.btnConsultaCompra.Text = "Consulta Compra";
            this.btnConsultaCompra.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnConsultaCompra.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnConsultaCompra.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnConsultaCompra.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnConsultaCompra.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConsultaCompra.Click += new System.EventHandler(this.btnConsultaCompra_Click);
            // 
            // sideNavPanel2
            // 
            this.sideNavPanel2.Controls.Add(this.panel4);
            this.sideNavPanel2.Controls.Add(this.metroTilePanel2);
            this.sideNavPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNavPanel2.Location = new System.Drawing.Point(1, 1);
            this.sideNavPanel2.Name = "sideNavPanel2";
            this.sideNavPanel2.Size = new System.Drawing.Size(797, 399);
            this.sideNavPanel2.TabIndex = 6;
            this.sideNavPanel2.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Thistle;
            this.panel4.Controls.Add(this.reflectionLabel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(797, 78);
            this.panel4.TabIndex = 6;
            // 
            // reflectionLabel1
            // 
            this.reflectionLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.reflectionLabel1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.reflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.reflectionLabel1.ForeColor = System.Drawing.Color.Black;
            this.reflectionLabel1.Location = new System.Drawing.Point(305, 5);
            this.reflectionLabel1.Name = "reflectionLabel1";
            this.reflectionLabel1.Size = new System.Drawing.Size(182, 70);
            this.reflectionLabel1.TabIndex = 4;
            this.reflectionLabel1.Text = "<b><font size=\"+15\">Consultas</font></b>";
            // 
            // metroTilePanel2
            // 
            this.metroTilePanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.metroTilePanel2.BackgroundStyle.Class = "MetroTilePanel";
            this.metroTilePanel2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroTilePanel2.ContainerControlProcessDialogKey = true;
            this.metroTilePanel2.DragDropSupport = true;
            this.metroTilePanel2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnVerCliente,
            this.btnVerFornecedor,
            this.btnVerProduto});
            this.metroTilePanel2.Location = new System.Drawing.Point(76, 131);
            this.metroTilePanel2.Name = "metroTilePanel2";
            this.metroTilePanel2.Size = new System.Drawing.Size(638, 128);
            this.metroTilePanel2.TabIndex = 1;
            this.metroTilePanel2.Text = "metroTilePanel2";
            // 
            // btnVerCliente
            // 
            this.btnVerCliente.Name = "btnVerCliente";
            this.btnVerCliente.Symbol = "";
            this.btnVerCliente.SymbolColor = System.Drawing.Color.Empty;
            this.btnVerCliente.Text = "Cliente";
            this.btnVerCliente.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnVerCliente.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnVerCliente.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerCliente.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnVerCliente.Click += new System.EventHandler(this.btnVerCliente_Click);
            // 
            // btnVerFornecedor
            // 
            this.btnVerFornecedor.Name = "btnVerFornecedor";
            this.btnVerFornecedor.Symbol = "58712";
            this.btnVerFornecedor.SymbolColor = System.Drawing.Color.Empty;
            this.btnVerFornecedor.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnVerFornecedor.Text = "Fornecedor";
            this.btnVerFornecedor.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnVerFornecedor.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnVerFornecedor.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnVerFornecedor.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnVerFornecedor.Click += new System.EventHandler(this.btnVerFornecedor_Click);
            // 
            // btnVerProduto
            // 
            this.btnVerProduto.Name = "btnVerProduto";
            this.btnVerProduto.Symbol = "57681";
            this.btnVerProduto.SymbolColor = System.Drawing.Color.Empty;
            this.btnVerProduto.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnVerProduto.Text = "Produto";
            this.btnVerProduto.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnVerProduto.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnVerProduto.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnVerProduto.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnVerProduto.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnVerProduto.Click += new System.EventHandler(this.btnVerProduto_Click);
            // 
            // sideNavPanel1
            // 
            this.sideNavPanel1.Controls.Add(this.panel3);
            this.sideNavPanel1.Controls.Add(this.metroTilePanel1);
            this.sideNavPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNavPanel1.Location = new System.Drawing.Point(1, 1);
            this.sideNavPanel1.Name = "sideNavPanel1";
            this.sideNavPanel1.Size = new System.Drawing.Size(797, 399);
            this.sideNavPanel1.TabIndex = 2;
            this.sideNavPanel1.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Thistle;
            this.panel3.Controls.Add(this.reflectionLabel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(797, 78);
            this.panel3.TabIndex = 5;
            // 
            // reflectionLabel2
            // 
            this.reflectionLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.reflectionLabel2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.reflectionLabel2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.reflectionLabel2.ForeColor = System.Drawing.Color.Black;
            this.reflectionLabel2.Location = new System.Drawing.Point(305, 5);
            this.reflectionLabel2.Name = "reflectionLabel2";
            this.reflectionLabel2.Size = new System.Drawing.Size(182, 70);
            this.reflectionLabel2.TabIndex = 4;
            this.reflectionLabel2.Text = "<b><font size=\"+15\">Cadastros</font></b>";
            // 
            // metroTilePanel1
            // 
            this.metroTilePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.metroTilePanel1.BackgroundStyle.Class = "MetroTilePanel";
            this.metroTilePanel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroTilePanel1.ContainerControlProcessDialogKey = true;
            this.metroTilePanel1.DragDropSupport = true;
            this.metroTilePanel1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCadCliente,
            this.btnCadFornecedor,
            this.btnCadProduto});
            this.metroTilePanel1.Location = new System.Drawing.Point(76, 131);
            this.metroTilePanel1.Name = "metroTilePanel1";
            this.metroTilePanel1.Size = new System.Drawing.Size(638, 128);
            this.metroTilePanel1.TabIndex = 0;
            this.metroTilePanel1.Text = "metroTilePanel1";
            // 
            // btnCadCliente
            // 
            this.btnCadCliente.Name = "btnCadCliente";
            this.btnCadCliente.Symbol = "";
            this.btnCadCliente.SymbolColor = System.Drawing.Color.Empty;
            this.btnCadCliente.Text = "Cliente";
            this.btnCadCliente.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnCadCliente.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnCadCliente.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadCliente.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnCadCliente.Click += new System.EventHandler(this.btnCadCliente_Click);
            // 
            // btnCadFornecedor
            // 
            this.btnCadFornecedor.Name = "btnCadFornecedor";
            this.btnCadFornecedor.Symbol = "58712";
            this.btnCadFornecedor.SymbolColor = System.Drawing.Color.Empty;
            this.btnCadFornecedor.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnCadFornecedor.Text = "Fornecedor";
            this.btnCadFornecedor.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnCadFornecedor.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnCadFornecedor.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnCadFornecedor.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnCadFornecedor.Click += new System.EventHandler(this.btnCadFornecedor_Click);
            // 
            // btnCadProduto
            // 
            this.btnCadProduto.Name = "btnCadProduto";
            this.btnCadProduto.Symbol = "57681";
            this.btnCadProduto.SymbolColor = System.Drawing.Color.Empty;
            this.btnCadProduto.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnCadProduto.Text = "Produto";
            this.btnCadProduto.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Plum;
            // 
            // 
            // 
            this.btnCadProduto.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnCadProduto.TileStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnCadProduto.TileStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.btnCadProduto.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCadProduto.Click += new System.EventHandler(this.btnCadProduto_Click);
            // 
            // sideNavItem1
            // 
            this.sideNavItem1.IsSystemMenu = true;
            this.sideNavItem1.Name = "sideNavItem1";
            this.sideNavItem1.Symbol = "";
            this.sideNavItem1.Text = "Menu";
            // 
            // separator1
            // 
            this.separator1.FixedSize = new System.Drawing.Size(3, 1);
            this.separator1.Name = "separator1";
            this.separator1.Padding.Bottom = 2;
            this.separator1.Padding.Left = 6;
            this.separator1.Padding.Right = 6;
            this.separator1.Padding.Top = 2;
            this.separator1.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            // 
            // tabCadastros
            // 
            this.tabCadastros.Name = "tabCadastros";
            this.tabCadastros.Panel = this.sideNavPanel1;
            this.tabCadastros.Symbol = "57680";
            this.tabCadastros.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.tabCadastros.Text = "Cadastros";
            // 
            // tabConsulta
            // 
            this.tabConsulta.Name = "tabConsulta";
            this.tabConsulta.Panel = this.sideNavPanel2;
            this.tabConsulta.Symbol = "59636";
            this.tabConsulta.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.tabConsulta.Text = "Consultas";
            // 
            // tabNegocio
            // 
            this.tabNegocio.Checked = true;
            this.tabNegocio.Name = "tabNegocio";
            this.tabNegocio.Panel = this.sideNavPanel3;
            this.tabNegocio.Symbol = "";
            this.tabNegocio.Text = "Negócio";
            // 
            // separator2
            // 
            this.separator2.FixedSize = new System.Drawing.Size(3, 1);
            this.separator2.Name = "separator2";
            this.separator2.Padding.Bottom = 2;
            this.separator2.Padding.Left = 6;
            this.separator2.Padding.Right = 6;
            this.separator2.Padding.Top = 2;
            this.separator2.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            // 
            // metroStatusBar1
            // 
            // 
            // 
            // 
            this.metroStatusBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroStatusBar1.ContainerControlProcessDialogKey = true;
            this.metroStatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroStatusBar1.DragDropSupport = true;
            this.metroStatusBar1.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroStatusBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblHora});
            this.metroStatusBar1.Location = new System.Drawing.Point(0, 426);
            this.metroStatusBar1.Name = "metroStatusBar1";
            this.metroStatusBar1.Size = new System.Drawing.Size(798, 22);
            this.metroStatusBar1.TabIndex = 3;
            this.metroStatusBar1.Text = "metroStatusBar1";
            // 
            // lblHora
            // 
            this.lblHora.Name = "lblHora";
            this.lblHora.Text = "Hora";
            // 
            // timerHora
            // 
            this.timerHora.Enabled = true;
            this.timerHora.Interval = 500;
            this.timerHora.Tick += new System.EventHandler(this.timerHora_Tick);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.BackColor = System.Drawing.Color.Lavender;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Perpetua Titling MT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimizar.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnMinimizar.Location = new System.Drawing.Point(734, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(34, 24);
            this.btnMinimizar.TabIndex = 4;
            this.btnMinimizar.Text = "-";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TCCAcai.Properties.Resources.Logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 25);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.Text = "frmPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.SuperTabGeral.ResumeLayout(false);
            this.SuperTabGeral.PerformLayout();
            this.sideNavPanel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.sideNavPanel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.sideNavPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.Controls.SideNav SuperTabGeral;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel1;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel2;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem1;
        private DevComponents.DotNetBar.Separator separator1;
        private DevComponents.DotNetBar.Controls.SideNavItem tabCadastros;
        private DevComponents.DotNetBar.Controls.SideNavItem tabConsulta;
        private DevComponents.DotNetBar.Metro.MetroTilePanel metroTilePanel1;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnCadCliente;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnCadFornecedor;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnCadProduto;
        private DevComponents.DotNetBar.Metro.MetroTilePanel metroTilePanel2;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnVerCliente;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnVerFornecedor;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnVerProduto;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel3;
        private DevComponents.DotNetBar.Controls.SideNavItem tabNegocio;
        private DevComponents.DotNetBar.Separator separator2;
        private DevComponents.DotNetBar.Metro.MetroTilePanel metroTilePanel3;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnVenda;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnCompra;
        private DevComponents.DotNetBar.Metro.MetroStatusBar metroStatusBar1;
        private DevComponents.DotNetBar.LabelItem lblHora;
        private System.Windows.Forms.Timer timerHora;
        public System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel5;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel3;
        private System.Windows.Forms.Panel panel4;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel1;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel2;
        private DevComponents.DotNetBar.Metro.MetroTileItem btnConsultaCompra;
    }
}