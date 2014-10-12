namespace miamiPOS
{
    partial class manager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(manager));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coneccionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxCategorias = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.dataGridViewProductos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonNewItem = new System.Windows.Forms.Button();
            this.checkBoxEditMode = new System.Windows.Forms.CheckBox();
            this.buttonSaveItem = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCategoria = new System.Windows.Forms.ComboBox();
            this.checkBoxPesable = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbBarcode = new System.Windows.Forms.TextBox();
            this.tbPLU = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxResumen = new System.Windows.Forms.GroupBox();
            this.textBoxCfinal = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBoxCinicial = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBoxVentas = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxFacturas = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxColaciones = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAnticipos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridViewRetiros = new System.Windows.Forms.DataGridView();
            this.dataGridViewFacturas = new System.Windows.Forms.DataGridView();
            this.dataGridViewVentas = new System.Windows.Forms.DataGridView();
            this.checkBoxPesableventa = new System.Windows.Forms.CheckBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.comboBoxSucursales = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRetiros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFacturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sistemaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(669, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sistemaToolStripMenuItem
            // 
            this.sistemaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coneccionToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.sistemaToolStripMenuItem.Name = "sistemaToolStripMenuItem";
            this.sistemaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.sistemaToolStripMenuItem.Text = "Sistema";
            // 
            // coneccionToolStripMenuItem
            // 
            this.coneccionToolStripMenuItem.Name = "coneccionToolStripMenuItem";
            this.coneccionToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.coneccionToolStripMenuItem.Text = "PaginaWeb";
            this.coneccionToolStripMenuItem.Click += new System.EventHandler(this.coneccionToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 518);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.buttonDelete);
            this.tabPage1.Controls.Add(this.buttonEdit);
            this.tabPage1.Controls.Add(this.dataGridViewProductos);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Productos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.buttonSearch);
            this.groupBox2.Controls.Add(this.comboBoxCategorias);
            this.groupBox2.Controls.Add(this.textBoxSearch);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 69);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Busqueda";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(260, 17);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Buscar";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxCategorias
            // 
            this.comboBoxCategorias.FormattingEnabled = true;
            this.comboBoxCategorias.Location = new System.Drawing.Point(6, 42);
            this.comboBoxCategorias.Name = "comboBoxCategorias";
            this.comboBoxCategorias.Size = new System.Drawing.Size(248, 21);
            this.comboBoxCategorias.TabIndex = 1;
            this.comboBoxCategorias.Leave += new System.EventHandler(this.comboBoxCategorias_Leave);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(6, 19);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(248, 20);
            this.textBoxSearch.TabIndex = 7;
            this.textBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Categoria";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(359, 449);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(130, 37);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Borrar";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(495, 449);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(136, 37);
            this.buttonEdit.TabIndex = 4;
            this.buttonEdit.Text = "Editar";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // dataGridViewProductos
            // 
            this.dataGridViewProductos.AllowUserToAddRows = false;
            this.dataGridViewProductos.AllowUserToDeleteRows = false;
            this.dataGridViewProductos.AllowUserToResizeRows = false;
            this.dataGridViewProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProductos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductos.Cursor = System.Windows.Forms.Cursors.Cross;
            this.dataGridViewProductos.Location = new System.Drawing.Point(359, 7);
            this.dataGridViewProductos.MultiSelect = false;
            this.dataGridViewProductos.Name = "dataGridViewProductos";
            this.dataGridViewProductos.ReadOnly = true;
            this.dataGridViewProductos.RowHeadersVisible = false;
            this.dataGridViewProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductos.ShowEditingIcon = false;
            this.dataGridViewProductos.Size = new System.Drawing.Size(272, 436);
            this.dataGridViewProductos.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.buttonNewItem);
            this.groupBox1.Controls.Add(this.checkBoxEditMode);
            this.groupBox1.Controls.Add(this.buttonSaveItem);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbCategoria);
            this.groupBox1.Controls.Add(this.checkBoxPesable);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbPrice);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.tbBarcode);
            this.groupBox1.Controls.Add(this.tbPLU);
            this.groupBox1.Location = new System.Drawing.Point(3, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 209);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editar Producto";
            // 
            // buttonNewItem
            // 
            this.buttonNewItem.Location = new System.Drawing.Point(185, 178);
            this.buttonNewItem.Name = "buttonNewItem";
            this.buttonNewItem.Size = new System.Drawing.Size(75, 23);
            this.buttonNewItem.TabIndex = 14;
            this.buttonNewItem.Text = "Nuevo";
            this.buttonNewItem.UseVisualStyleBackColor = true;
            this.buttonNewItem.Click += new System.EventHandler(this.buttonNewItem_Click);
            // 
            // checkBoxEditMode
            // 
            this.checkBoxEditMode.AutoSize = true;
            this.checkBoxEditMode.BackColor = System.Drawing.Color.Gold;
            this.checkBoxEditMode.Location = new System.Drawing.Point(6, 178);
            this.checkBoxEditMode.Name = "checkBoxEditMode";
            this.checkBoxEditMode.Size = new System.Drawing.Size(102, 17);
            this.checkBoxEditMode.TabIndex = 13;
            this.checkBoxEditMode.Text = "MODO EDITAR";
            this.checkBoxEditMode.UseVisualStyleBackColor = false;
            this.checkBoxEditMode.CheckedChanged += new System.EventHandler(this.checkBoxEditMode_CheckedChanged);
            // 
            // buttonSaveItem
            // 
            this.buttonSaveItem.Location = new System.Drawing.Point(266, 178);
            this.buttonSaveItem.Name = "buttonSaveItem";
            this.buttonSaveItem.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveItem.TabIndex = 12;
            this.buttonSaveItem.Text = "Guardar";
            this.buttonSaveItem.UseVisualStyleBackColor = true;
            this.buttonSaveItem.Click += new System.EventHandler(this.buttonSaveItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Categoria";
            // 
            // cbCategoria
            // 
            this.cbCategoria.FormattingEnabled = true;
            this.cbCategoria.Location = new System.Drawing.Point(99, 128);
            this.cbCategoria.Name = "cbCategoria";
            this.cbCategoria.Size = new System.Drawing.Size(242, 21);
            this.cbCategoria.TabIndex = 6;
            // 
            // checkBoxPesable
            // 
            this.checkBoxPesable.AutoSize = true;
            this.checkBoxPesable.Location = new System.Drawing.Point(265, 155);
            this.checkBoxPesable.Name = "checkBoxPesable";
            this.checkBoxPesable.Size = new System.Drawing.Size(76, 17);
            this.checkBoxPesable.TabIndex = 10;
            this.checkBoxPesable.Text = "¿Pesable?";
            this.checkBoxPesable.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Precio($)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Descripcion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Barcode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PLU";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(99, 102);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(242, 20);
            this.tbPrice.TabIndex = 3;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(99, 76);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(242, 20);
            this.tbName.TabIndex = 2;
            // 
            // tbBarcode
            // 
            this.tbBarcode.Location = new System.Drawing.Point(99, 50);
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.Size = new System.Drawing.Size(242, 20);
            this.tbBarcode.TabIndex = 1;
            // 
            // tbPLU
            // 
            this.tbPLU.Location = new System.Drawing.Point(99, 23);
            this.tbPLU.Name = "tbPLU";
            this.tbPLU.Size = new System.Drawing.Size(242, 20);
            this.tbPLU.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.comboBoxSucursales);
            this.tabPage2.Controls.Add(this.groupBoxResumen);
            this.tabPage2.Controls.Add(this.dataGridViewRetiros);
            this.tabPage2.Controls.Add(this.dataGridViewFacturas);
            this.tabPage2.Controls.Add(this.dataGridViewVentas);
            this.tabPage2.Controls.Add(this.checkBoxPesableventa);
            this.tabPage2.Controls.Add(this.dateTimePicker);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(637, 492);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ventas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBoxResumen
            // 
            this.groupBoxResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResumen.Controls.Add(this.textBoxCfinal);
            this.groupBoxResumen.Controls.Add(this.label12);
            this.groupBoxResumen.Controls.Add(this.textBox9);
            this.groupBoxResumen.Controls.Add(this.textBoxCinicial);
            this.groupBoxResumen.Controls.Add(this.label11);
            this.groupBoxResumen.Controls.Add(this.textBox7);
            this.groupBoxResumen.Controls.Add(this.textBoxVentas);
            this.groupBoxResumen.Controls.Add(this.label10);
            this.groupBoxResumen.Controls.Add(this.textBoxFacturas);
            this.groupBoxResumen.Controls.Add(this.label9);
            this.groupBoxResumen.Controls.Add(this.textBoxColaciones);
            this.groupBoxResumen.Controls.Add(this.label8);
            this.groupBoxResumen.Controls.Add(this.textBoxAnticipos);
            this.groupBoxResumen.Controls.Add(this.label7);
            this.groupBoxResumen.Controls.Add(this.textBox1);
            this.groupBoxResumen.Location = new System.Drawing.Point(6, 359);
            this.groupBoxResumen.Name = "groupBoxResumen";
            this.groupBoxResumen.Size = new System.Drawing.Size(625, 127);
            this.groupBoxResumen.TabIndex = 15;
            this.groupBoxResumen.TabStop = false;
            this.groupBoxResumen.Text = "Resumen";
            // 
            // textBoxCfinal
            // 
            this.textBoxCfinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxCfinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCfinal.Location = new System.Drawing.Point(387, 45);
            this.textBoxCfinal.Name = "textBoxCfinal";
            this.textBoxCfinal.ReadOnly = true;
            this.textBoxCfinal.Size = new System.Drawing.Size(151, 24);
            this.textBoxCfinal.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(298, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 18);
            this.label12.TabIndex = 13;
            this.label12.Text = "Caja Final";
            // 
            // textBox9
            // 
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Location = new System.Drawing.Point(403, 45);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(151, 13);
            this.textBox9.TabIndex = 12;
            // 
            // textBoxCinicial
            // 
            this.textBoxCinicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxCinicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCinicial.Location = new System.Drawing.Point(387, 19);
            this.textBoxCinicial.Name = "textBoxCinicial";
            this.textBoxCinicial.ReadOnly = true;
            this.textBoxCinicial.Size = new System.Drawing.Size(151, 24);
            this.textBoxCinicial.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(291, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 18);
            this.label11.TabIndex = 10;
            this.label11.Text = "Caja Inicial";
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Location = new System.Drawing.Point(403, 19);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(151, 13);
            this.textBox7.TabIndex = 9;
            // 
            // textBoxVentas
            // 
            this.textBoxVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxVentas.Location = new System.Drawing.Point(105, 19);
            this.textBoxVentas.Name = "textBoxVentas";
            this.textBoxVentas.ReadOnly = true;
            this.textBoxVentas.Size = new System.Drawing.Size(151, 24);
            this.textBoxVentas.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(22, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 18);
            this.label10.TabIndex = 7;
            this.label10.Text = "Facturas";
            // 
            // textBoxFacturas
            // 
            this.textBoxFacturas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBoxFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFacturas.Location = new System.Drawing.Point(105, 96);
            this.textBoxFacturas.Name = "textBoxFacturas";
            this.textBoxFacturas.ReadOnly = true;
            this.textBoxFacturas.Size = new System.Drawing.Size(151, 24);
            this.textBoxFacturas.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 18);
            this.label9.TabIndex = 5;
            this.label9.Text = "Colaciones";
            // 
            // textBoxColaciones
            // 
            this.textBoxColaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxColaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxColaciones.Location = new System.Drawing.Point(105, 71);
            this.textBoxColaciones.Name = "textBoxColaciones";
            this.textBoxColaciones.ReadOnly = true;
            this.textBoxColaciones.Size = new System.Drawing.Size(151, 24);
            this.textBoxColaciones.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 18);
            this.label8.TabIndex = 3;
            this.label8.Text = "Anticipos";
            // 
            // textBoxAnticipos
            // 
            this.textBoxAnticipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxAnticipos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAnticipos.Location = new System.Drawing.Point(105, 45);
            this.textBoxAnticipos.Name = "textBoxAnticipos";
            this.textBoxAnticipos.ReadOnly = true;
            this.textBoxAnticipos.Size = new System.Drawing.Size(151, 24);
            this.textBoxAnticipos.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 18);
            this.label7.TabIndex = 1;
            this.label7.Text = "Ventas";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(105, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 13);
            this.textBox1.TabIndex = 0;
            // 
            // dataGridViewRetiros
            // 
            this.dataGridViewRetiros.AllowUserToAddRows = false;
            this.dataGridViewRetiros.AllowUserToDeleteRows = false;
            this.dataGridViewRetiros.AllowUserToResizeRows = false;
            this.dataGridViewRetiros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRetiros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRetiros.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewRetiros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRetiros.Location = new System.Drawing.Point(345, 206);
            this.dataGridViewRetiros.MultiSelect = false;
            this.dataGridViewRetiros.Name = "dataGridViewRetiros";
            this.dataGridViewRetiros.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRetiros.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewRetiros.RowHeadersVisible = false;
            this.dataGridViewRetiros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRetiros.Size = new System.Drawing.Size(292, 147);
            this.dataGridViewRetiros.TabIndex = 14;
            // 
            // dataGridViewFacturas
            // 
            this.dataGridViewFacturas.AllowUserToAddRows = false;
            this.dataGridViewFacturas.AllowUserToDeleteRows = false;
            this.dataGridViewFacturas.AllowUserToResizeRows = false;
            this.dataGridViewFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFacturas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFacturas.Location = new System.Drawing.Point(345, 32);
            this.dataGridViewFacturas.MultiSelect = false;
            this.dataGridViewFacturas.Name = "dataGridViewFacturas";
            this.dataGridViewFacturas.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFacturas.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewFacturas.RowHeadersVisible = false;
            this.dataGridViewFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFacturas.Size = new System.Drawing.Size(291, 168);
            this.dataGridViewFacturas.TabIndex = 13;
            // 
            // dataGridViewVentas
            // 
            this.dataGridViewVentas.AllowUserToAddRows = false;
            this.dataGridViewVentas.AllowUserToDeleteRows = false;
            this.dataGridViewVentas.AllowUserToResizeRows = false;
            this.dataGridViewVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewVentas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVentas.Location = new System.Drawing.Point(6, 32);
            this.dataGridViewVentas.MultiSelect = false;
            this.dataGridViewVentas.Name = "dataGridViewVentas";
            this.dataGridViewVentas.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewVentas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewVentas.RowHeadersVisible = false;
            this.dataGridViewVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVentas.Size = new System.Drawing.Size(333, 321);
            this.dataGridViewVentas.TabIndex = 12;
            // 
            // checkBoxPesableventa
            // 
            this.checkBoxPesableventa.AutoSize = true;
            this.checkBoxPesableventa.Location = new System.Drawing.Point(214, 8);
            this.checkBoxPesableventa.Name = "checkBoxPesableventa";
            this.checkBoxPesableventa.Size = new System.Drawing.Size(76, 17);
            this.checkBoxPesableventa.TabIndex = 11;
            this.checkBoxPesableventa.Text = "¿Pesable?";
            this.checkBoxPesableventa.UseVisualStyleBackColor = true;
            this.checkBoxPesableventa.CheckedChanged += new System.EventHandler(this.checkBoxPesableventa_CheckedChanged);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(6, 6);
            this.dateTimePicker.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.RightToLeftLayout = true;
            this.dateTimePicker.Size = new System.Drawing.Size(202, 20);
            this.dateTimePicker.TabIndex = 0;
            this.dateTimePicker.Value = new System.DateTime(2014, 8, 1, 16, 42, 9, 0);
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // comboBoxSucursales
            // 
            this.comboBoxSucursales.FormattingEnabled = true;
            this.comboBoxSucursales.Location = new System.Drawing.Point(300, 6);
            this.comboBoxSucursales.Name = "comboBoxSucursales";
            this.comboBoxSucursales.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSucursales.TabIndex = 16;
            // 
            // manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 557);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrador de miamiPOS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.manager_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBoxResumen.ResumeLayout(false);
            this.groupBoxResumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRetiros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFacturas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sistemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCategorias;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewProductos;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbBarcode;
        private System.Windows.Forms.TextBox tbPLU;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCategoria;
        private System.Windows.Forms.CheckBox checkBoxPesable;
        private System.Windows.Forms.Button buttonSaveItem;
        private System.Windows.Forms.CheckBox checkBoxEditMode;
        private System.Windows.Forms.Button buttonNewItem;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem coneccionToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.DataGridView dataGridViewVentas;
        private System.Windows.Forms.CheckBox checkBoxPesableventa;
        private System.Windows.Forms.DataGridView dataGridViewRetiros;
        private System.Windows.Forms.DataGridView dataGridViewFacturas;
        private System.Windows.Forms.GroupBox groupBoxResumen;
        private System.Windows.Forms.TextBox textBoxCfinal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBoxCinicial;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBoxVentas;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxFacturas;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxColaciones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxAnticipos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBoxSucursales;
    }
}