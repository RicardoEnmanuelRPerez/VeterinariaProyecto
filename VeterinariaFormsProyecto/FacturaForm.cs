using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using FontAwesome.Sharp;
using Veterinaria_Genesis_DB.Models;

namespace VeterinariaFormsProyecto
{
    public partial class FacturaForm : Form
    {
        private readonly HttpClient httpClient;
        private List<Producto> productosDisponibles;
        private List<Cliente> clientesDisponibles;
        private List<FacturaProductoForm> productosFactura;
        private decimal totalFactura;

        public FacturaForm()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7000/api/"); // Ajusta según tu puerto de API
            productosFactura = new List<FacturaProductoForm>();
            totalFactura = 0;
            CargarDatosIniciales();
        }

        private async void CargarDatosIniciales()
        {
            await CargarClientes();
            await CargarProductos();
            ConfigurarDataGridViews();
        }

        private async Task CargarClientes()
        {
            try
            {
                var response = await httpClient.GetAsync("Clientes");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    clientesDisponibles = JsonSerializer.Deserialize<List<Cliente>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Cliente>();

                    cmbCliente.DataSource = clientesDisponibles;
                    cmbCliente.DisplayMember = "Nombres";
                    cmbCliente.ValueMember = "IdCliente";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarProductos()
        {
            try
            {
                var response = await httpClient.GetAsync("Productoes");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    productosDisponibles = JsonSerializer.Deserialize<List<Producto>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Producto>();

                    cmbProducto.DataSource = productosDisponibles.Where(p => p.StockActual > 0).ToList();
                    cmbProducto.DisplayMember = "Nombre";
                    cmbProducto.ValueMember = "IdProducto";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridViews()
        {
            // Configurar dgvProductosFactura
            dgvProductosFactura.AutoGenerateColumns = false;
            dgvProductosFactura.Columns.Clear();
            dgvProductosFactura.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Producto",
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto",
                Width = 200
            });
            dgvProductosFactura.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 80
            });
            dgvProductosFactura.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio Unitario",
                DataPropertyName = "PrecioUnitario",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgvProductosFactura.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // Agregar columna para eliminar
            var btnEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Acción",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvProductosFactura.Columns.Add(btnEliminar);

            dgvProductosFactura.CellClick += DgvProductosFactura_CellClick;
        }

        private void DgvProductosFactura_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductosFactura.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                productosFactura.RemoveAt(e.RowIndex);
                ActualizarDataGridView();
                CalcularTotal();
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue == null || string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Por favor seleccione un producto y especifique la cantidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número entero positivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoSeleccionado = productosDisponibles.FirstOrDefault(p => p.IdProducto == (int)cmbProducto.SelectedValue);
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cantidad > productoSeleccionado.StockActual)
            {
                MessageBox.Show($"No hay suficiente stock. Stock disponible: {productoSeleccionado.StockActual}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var facturaProducto = new FacturaProductoForm
            {
                IdProducto = productoSeleccionado.IdProducto,
                Cantidad = cantidad,
                PrecioUnitario = productoSeleccionado.PrecioVenta,
                Total = cantidad * productoSeleccionado.PrecioVenta,
                NombreProducto = productoSeleccionado.Nombre
            };

            productosFactura.Add(facturaProducto);
            ActualizarDataGridView();
            CalcularTotal();

            // Limpiar campos
            txtCantidad.Clear();
        }

        private void ActualizarDataGridView()
        {
            dgvProductosFactura.DataSource = null;
            dgvProductosFactura.DataSource = productosFactura;
        }

        private void CalcularTotal()
        {
            totalFactura = productosFactura.Sum(p => p.Total);
            lblTotal.Text = $"Total: {totalFactura:C2}";
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue != null)
            {
                var producto = productosDisponibles.FirstOrDefault(p => p.IdProducto == (int)cmbProducto.SelectedValue);
                if (producto != null)
                {
                    lblPrecio.Text = $"Precio: {producto.PrecioVenta:C2}";
                    lblStock.Text = $"Stock: {producto.StockActual}";
                }
            }
        }

        private int facturaGuardadaId = 0; // Variable para almacenar el ID de la factura guardada

        private async void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productosFactura.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var factura = new Factura
                {
                    IdCliente = (int)cmbCliente.SelectedValue,
                    IdEmpleado = 1, // Asumir empleado actual, debería venir del login
                    FechaEmision = DateTime.Now,
                    FechaVencimiento = DateTime.Now.AddDays(30),
                    Total = totalFactura,
                    FacturaProductos = productosFactura.Select(fp => new FacturaProducto
                    {
                        IdProducto = fp.IdProducto,
                        Cantidad = fp.Cantidad,
                        PrecioUnitario = fp.PrecioUnitario,
                        Total = fp.Total
                    }).ToList()
                };

                var json = JsonSerializer.Serialize(factura, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(json, Encoding.UTF8, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                var response = await httpClient.PostAsync("Facturas", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var facturaCreada = JsonSerializer.Deserialize<Factura>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    facturaGuardadaId = facturaCreada?.IdFactura ?? 0;

                    MessageBox.Show("Factura guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Habilitar el botón de procesar pago
                    btnProcesarPago.Enabled = true;
                    btnProcesarPago.Visible = true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al guardar la factura: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcesarPago_Click(object sender, EventArgs e)
        {
            if (facturaGuardadaId == 0)
            {
                MessageBox.Show("No hay una factura guardada para procesar el pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Crear instancia del formulario de pago y pasar la factura
                var pagoForm = new PagoForm(facturaGuardadaId, totalFactura);
                
                // Configurar el formulario como modal
                pagoForm.ShowDialog();

                // Después de cerrar el formulario de pago, verificar si se procesó exitosamente
                if (pagoForm.PagoProcesadoExitosamente)
                {
                    MessageBox.Show("Pago procesado exitosamente. La factura ha sido pagada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Limpiar formulario y cerrar
                    LimpiarFormulario();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario de pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            productosFactura.Clear();
            ActualizarDataGridView();
            CalcularTotal();
            txtCantidad.Clear();
            lblPrecio.Text = "Precio: $0.00";
            lblStock.Text = "Stock: 0";
            facturaGuardadaId = 0;
            btnProcesarPago.Enabled = false;
            btnProcesarPago.Visible = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                httpClient?.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }

    // Clase auxiliar para el formulario (extiende FacturaProducto del API)
    public class FacturaProductoForm
    {
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
    }
}
