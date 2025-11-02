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
    public partial class PagoForm : Form
    {
        private readonly HttpClient httpClient;
        private List<FacturaFormInfo> facturasPendientes;
        private FacturaFormInfo facturaSeleccionada;
        private decimal montoRestante;
        private int? facturaIdEspecifica;
        private decimal? montoFacturaEspecifica;

        // Propiedad para indicar si el pago se procesó exitosamente
        public bool PagoProcesadoExitosamente { get; private set; } = false;

        // Constructor para uso independiente (cargar todas las facturas pendientes)
        public PagoForm()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7000/api/");
            facturasPendientes = new List<FacturaFormInfo>();
            CargarDatosIniciales();
        }

        // Constructor para uso desde FacturaForm (factura específica)
        public PagoForm(int facturaId, decimal montoFactura)
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7000/api/");
            facturasPendientes = new List<FacturaFormInfo>();
            facturaIdEspecifica = facturaId;
            montoFacturaEspecifica = montoFactura;
            CargarDatosIniciales();
        }

        private async void CargarDatosIniciales()
        {
            // Actualizar título si es una factura específica
            if (facturaIdEspecifica.HasValue)
            {
                this.Text = $"Pago de Factura #{facturaIdEspecifica}";
                lblTitulo.Text = $"Pago de Factura #{facturaIdEspecifica}";
            }

            await CargarFacturasPendientes();
            ConfigurarDataGridView();
            ConfigurarControles();
        }

        private async Task CargarFacturasPendientes()
        {
            try
            {
                if (facturaIdEspecifica.HasValue)
                {
                    // Si se especifica una factura, cargar solo esa factura
                    var response = await httpClient.GetAsync($"Facturas/{facturaIdEspecifica}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var factura = JsonSerializer.Deserialize<FacturaFormInfo>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (factura != null)
                        {
                            facturasPendientes = new List<Factura> { factura };
                            cmbFactura.DataSource = facturasPendientes;
                            cmbFactura.DisplayMember = "NumeroFactura";
                            cmbFactura.ValueMember = "IdFactura";
                            cmbFactura.SelectedIndex = 0;
                            cmbFactura.Enabled = false; // Deshabilitar selección ya que es una factura específica
                        }
                    }
                }
                else
                {
                    // Cargar todas las facturas pendientes
                    var response = await httpClient.GetAsync("Facturas");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var todasFacturas = JsonSerializer.Deserialize<List<FacturaFormInfo>>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? new List<FacturaFormInfo>();

                        // Filtrar facturas pendientes (sin pagar o pagos parciales)
                        facturasPendientes = todasFacturas.Where(f => f.EstadoPago != "Pagado").ToList();

                        cmbFactura.DataSource = facturasPendientes;
                        cmbFactura.DisplayMember = "NumeroFactura";
                        cmbFactura.ValueMember = "IdFactura";
                        cmbFactura.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            // Configurar dgvDetallePago
            dgvDetallePago.AutoGenerateColumns = false;
            dgvDetallePago.Columns.Clear();
            dgvDetallePago.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "FechaPago",
                Width = 100
            });
            dgvDetallePago.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Monto",
                HeaderText = "Monto",
                DataPropertyName = "Monto",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgvDetallePago.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MetodoPago",
                HeaderText = "Método de Pago",
                DataPropertyName = "MetodoPago",
                Width = 120
            });
            dgvDetallePago.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Observaciones",
                HeaderText = "Observaciones",
                DataPropertyName = "Observaciones",
                Width = 200
            });
        }

        private void ConfigurarControles()
        {
            // Configurar método de pago
            cmbMetodoPago.Items.AddRange(new[] { "Efectivo", "Tarjeta de Débito", "Tarjeta de Crédito", "Transferencia", "Cheque" });
            cmbMetodoPago.SelectedIndex = 0;

            // Configurar fecha de pago
            dtpFechaPago.Value = DateTime.Now;

            // Si es una factura específica, habilitar controles inmediatamente
            if (facturaIdEspecifica.HasValue)
            {
                HabilitarControles(true);
            }
            else
            {
                // Deshabilitar controles inicialmente para uso independiente
                HabilitarControles(false);
            }
        }

        private void HabilitarControles(bool habilitar)
        {
            txtMontoPago.Enabled = habilitar;
            cmbMetodoPago.Enabled = habilitar;
            dtpFechaPago.Enabled = habilitar;
            txtObservaciones.Enabled = habilitar;
            btnProcesarPago.Enabled = habilitar;
        }

        private void cmbFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFactura.SelectedValue != null)
            {
                facturaSeleccionada = facturasPendientes.FirstOrDefault(f => f.IdFactura == (int)cmbFactura.SelectedValue);
                if (facturaSeleccionada != null)
                {
                    CargarDetalleFactura();
                    CalcularMontoRestante();
                    HabilitarControles(true);
                }
            }
        }

        private void CargarDetalleFactura()
        {
            if (facturaSeleccionada != null)
            {
                lblCliente.Text = $"Cliente: {facturaSeleccionada.ClienteNombre}";
                lblTotalFactura.Text = $"Total Factura: {facturaSeleccionada.Total:C2}";
                lblFechaEmision.Text = $"Fecha Emisión: {facturaSeleccionada.FechaEmision:dd/MM/yyyy}";
                lblFechaVencimiento.Text = $"Fecha Vencimiento: {facturaSeleccionada.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "N/A"}";
                lblEstado.Text = $"Estado: {facturaSeleccionada.EstadoPago}";
            }
        }

        private void CalcularMontoRestante()
        {
            if (facturaSeleccionada != null)
            {
                decimal totalPagado = facturaSeleccionada.Pagos?.Sum(p => p.Monto) ?? 0;
                
                // Usar el monto específico si está disponible, sino usar el total de la factura
                decimal totalFactura = montoFacturaEspecifica ?? facturaSeleccionada.Total;
                
                montoRestante = totalFactura - totalPagado;
                lblMontoRestante.Text = $"Monto Restante: {montoRestante:C2}";
                
                // Establecer el monto máximo que se puede pagar
                txtMontoPago.Text = montoRestante.ToString("F2");
            }
        }

        private void btnProcesarPago_Click(object sender, EventArgs e)
        {
            if (facturaSeleccionada == null)
            {
                MessageBox.Show("Por favor seleccione una factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMontoPago.Text, out decimal montoPago) || montoPago <= 0)
            {
                MessageBox.Show("El monto debe ser un número positivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (montoPago > montoRestante)
            {
                MessageBox.Show($"El monto no puede ser mayor al restante (${montoRestante:F2}).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbMetodoPago.Text))
            {
                MessageBox.Show("Por favor seleccione un método de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var pago = new PagoFormData
                {
                    IdFactura = facturaSeleccionada.IdFactura,
                    Monto = montoPago,
                    FechaPago = dtpFechaPago.Value,
                    MetodoPago = cmbMetodoPago.Text,
                    Observaciones = txtObservaciones.Text,
                    IdEmpleado = 1 // Asumir empleado actual, debería venir del login
                };

                // Aquí se procesaría el pago
                ProcesarPago(pago);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ProcesarPago(Pago pago)
        {
            try
            {
                var json = JsonSerializer.Serialize(pago, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(json, Encoding.UTF8, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                var response = await httpClient.PostAsync("Pagos", content);

                if (response.IsSuccessStatusCode)
                {
                    PagoProcesadoExitosamente = true;
                    MessageBox.Show("Pago procesado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Si es una factura específica, cerrar el formulario
                    if (facturaIdEspecifica.HasValue)
                    {
                        this.Close();
                    }
                    else
                    {
                        LimpiarFormulario();
                        await CargarFacturasPendientes();
                    }
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al procesar el pago: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtMontoPago.Clear();
            txtObservaciones.Clear();
            dtpFechaPago.Value = DateTime.Now;
            cmbMetodoPago.SelectedIndex = 0;
            
            lblCliente.Text = "Cliente: ";
            lblTotalFactura.Text = "Total Factura: $0.00";
            lblFechaEmision.Text = "Fecha Emisión: ";
            lblFechaVencimiento.Text = "Fecha Vencimiento: ";
            lblEstado.Text = "Estado: ";
            lblMontoRestante.Text = "Monto Restante: $0.00";

            dgvDetallePago.DataSource = null;
            
            // Solo habilitar controles si no es una factura específica
            if (!facturaIdEspecifica.HasValue)
            {
                HabilitarControles(false);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMontoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox)?.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
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

    // Clases auxiliares para el formulario (usando modelos del API)
    public class PagoFormData
    {
        public int IdPago { get; set; }
        public int IdFactura { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public int IdEmpleado { get; set; }
    }

    // Clase auxiliar para mostrar información de factura en el formulario
    public class FacturaFormInfo
    {
        public int IdFactura { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Total { get; set; }
        public string EstadoPago { get; set; } = string.Empty;
        public string ClienteNombre { get; set; } = string.Empty;
        public List<PagoFormData> Pagos { get; set; } = new List<PagoFormData>();
    }
}
