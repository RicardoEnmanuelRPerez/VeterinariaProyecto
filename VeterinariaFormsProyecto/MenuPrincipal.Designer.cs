namespace VeterinariaFormsProyecto
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            panel2 = new Panel();
            PanelMenu = new Panel();
            IconProveedores = new FontAwesome.Sharp.IconButton();
            IconProductos = new FontAwesome.Sharp.IconButton();
            IconMascotas = new FontAwesome.Sharp.IconButton();
            IconCitas = new FontAwesome.Sharp.IconButton();
            IconFactura = new FontAwesome.Sharp.IconButton();
            IconEmpleados = new FontAwesome.Sharp.IconButton();
            IconoClientes = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            PanelMenu.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.SpringGreen;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(250, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(550, 91);
            panel2.TabIndex = 3;
            // 
            // PanelMenu
            // 
            PanelMenu.BackColor = Color.SpringGreen;
            PanelMenu.Controls.Add(IconProveedores);
            PanelMenu.Controls.Add(IconProductos);
            PanelMenu.Controls.Add(IconMascotas);
            PanelMenu.Controls.Add(IconCitas);
            PanelMenu.Controls.Add(IconFactura);
            PanelMenu.Controls.Add(IconEmpleados);
            PanelMenu.Controls.Add(IconoClientes);
            PanelMenu.Controls.Add(panel1);
            PanelMenu.Dock = DockStyle.Left;
            PanelMenu.Location = new Point(0, 0);
            PanelMenu.Name = "PanelMenu";
            PanelMenu.Size = new Size(250, 490);
            PanelMenu.TabIndex = 2;
            // 
            // IconProveedores
            // 
            IconProveedores.IconChar = FontAwesome.Sharp.IconChar.TruckFast;
            IconProveedores.IconColor = Color.SpringGreen;
            IconProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconProveedores.IconSize = 40;
            IconProveedores.ImageAlign = ContentAlignment.MiddleLeft;
            IconProveedores.Location = new Point(0, 378);
            IconProveedores.Name = "IconProveedores";
            IconProveedores.Size = new Size(250, 50);
            IconProveedores.TabIndex = 7;
            IconProveedores.Text = "Proveedores";
            IconProveedores.UseVisualStyleBackColor = true;
            // 
            // IconProductos
            // 
            IconProductos.IconChar = FontAwesome.Sharp.IconChar.BoxesPacking;
            IconProductos.IconColor = Color.SpringGreen;
            IconProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconProductos.IconSize = 40;
            IconProductos.ImageAlign = ContentAlignment.MiddleLeft;
            IconProductos.Location = new Point(0, 333);
            IconProductos.Name = "IconProductos";
            IconProductos.Size = new Size(250, 50);
            IconProductos.TabIndex = 6;
            IconProductos.Text = "Productos";
            IconProductos.UseVisualStyleBackColor = true;
            // 
            // IconMascotas
            // 
            IconMascotas.IconChar = FontAwesome.Sharp.IconChar.Paw;
            IconMascotas.IconColor = Color.SpringGreen;
            IconMascotas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconMascotas.IconSize = 40;
            IconMascotas.ImageAlign = ContentAlignment.MiddleLeft;
            IconMascotas.Location = new Point(0, 287);
            IconMascotas.Name = "IconMascotas";
            IconMascotas.Size = new Size(250, 50);
            IconMascotas.TabIndex = 5;
            IconMascotas.Text = "Mascotas";
            IconMascotas.UseVisualStyleBackColor = true;
            // 
            // IconCitas
            // 
            IconCitas.IconChar = FontAwesome.Sharp.IconChar.CalendarCheck;
            IconCitas.IconColor = Color.SpringGreen;
            IconCitas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconCitas.IconSize = 40;
            IconCitas.ImageAlign = ContentAlignment.MiddleLeft;
            IconCitas.Location = new Point(0, 249);
            IconCitas.Name = "IconCitas";
            IconCitas.Size = new Size(250, 45);
            IconCitas.TabIndex = 4;
            IconCitas.Text = "Citas";
            IconCitas.UseVisualStyleBackColor = true;
            // 
            // IconFactura
            // 
            IconFactura.IconChar = FontAwesome.Sharp.IconChar.File;
            IconFactura.IconColor = Color.SpringGreen;
            IconFactura.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconFactura.IconSize = 40;
            IconFactura.ImageAlign = ContentAlignment.MiddleLeft;
            IconFactura.Location = new Point(0, 211);
            IconFactura.Name = "IconFactura";
            IconFactura.Size = new Size(250, 51);
            IconFactura.TabIndex = 3;
            IconFactura.Text = "Factura";
            IconFactura.UseVisualStyleBackColor = true;
            IconFactura.Click += IconFactura_Click;
            // 
            // IconEmpleados
            // 
            IconEmpleados.IconChar = FontAwesome.Sharp.IconChar.User;
            IconEmpleados.IconColor = Color.SpringGreen;
            IconEmpleados.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconEmpleados.IconSize = 40;
            IconEmpleados.ImageAlign = ContentAlignment.MiddleLeft;
            IconEmpleados.Location = new Point(0, 173);
            IconEmpleados.Name = "IconEmpleados";
            IconEmpleados.Size = new Size(250, 52);
            IconEmpleados.TabIndex = 2;
            IconEmpleados.Text = "Empleados";
            IconEmpleados.UseVisualStyleBackColor = true;
            // 
            // IconoClientes
            // 
            IconoClientes.Anchor = AnchorStyles.Left;
            IconoClientes.IconChar = FontAwesome.Sharp.IconChar.PeopleGroup;
            IconoClientes.IconColor = Color.MediumSpringGreen;
            IconoClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconoClientes.IconSize = 42;
            IconoClientes.ImageAlign = ContentAlignment.MiddleLeft;
            IconoClientes.Location = new Point(0, 316);
            IconoClientes.Name = "IconoClientes";
            IconoClientes.Size = new Size(250, 54);
            IconoClientes.TabIndex = 1;
            IconoClientes.Text = "Clientes";
            IconoClientes.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 125);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(250, 125);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 490);
            Controls.Add(panel2);
            Controls.Add(PanelMenu);
            Name = "MenuPrincipal";
            Text = "MenuPrincipal";
            PanelMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Panel PanelMenu;
        private FontAwesome.Sharp.IconButton IconProveedores;
        private FontAwesome.Sharp.IconButton IconProductos;
        private FontAwesome.Sharp.IconButton IconMascotas;
        private FontAwesome.Sharp.IconButton IconCitas;
        private FontAwesome.Sharp.IconButton IconFactura;
        private FontAwesome.Sharp.IconButton IconEmpleados;
        private FontAwesome.Sharp.IconButton IconoClientes;
        private Panel panel1;
        private PictureBox pictureBox1;
    }
}