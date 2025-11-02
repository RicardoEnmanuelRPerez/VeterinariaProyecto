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
    public partial class ProcedimientoMedicoForm : Form
    {
        private readonly HttpClient httpClient;
        private List<Mascota> mascotasDisponibles;
        private List<Veterinario> veterinariosDisponibles;
        private List<ProcedimientoMedico> procedimientosDisponibles;
        private ProcedimientoMedico procedimientoSeleccionado;
        private string tipoProcedimiento;

        public ProcedimientoMedicoForm(string tipoProcedimiento = "Hospitalizacion")
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7000/api/");
            mascotasDisponibles = new List<Mascota>();
            veterinariosDisponibles = new List<Veterinario>();
            procedimientosDisponibles = new List<ProcedimientoMedico>();
            this.tipoProcedimiento = tipoProcedimiento;
            
            ConfigurarFormulario();
            CargarDatosIniciales();
        }

        private void ConfigurarFormulario()
        {
            // Configurar título según tipo de procedimiento
            switch (tipoProcedimiento)
            {
                case "Hospitalizacion":
                    this.Text = "Gestión de Hospitalizaciones";
                    lblTitulo.Text = "Gestión de Hospitalizaciones";
                    iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Hospital;
                    gbProcedimiento.Text = "Información de Hospitalización";
                    lblTipo.Text = "Tipo: Hospitalización";
                    break;
                case "Cirugia":
                    this.Text = "Gestión de Cirugías";
                    lblTitulo.Text = "Gestión de Cirugías";
                    iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserMd;
                    gbProcedimiento.Text = "Información de Cirugía";
                    lblTipo.Text = "Tipo: Cirugía";
                    break;
            }

            // Configurar campos específicos según tipo
            ConfigurarCamposEspecificos();
        }

        private void ConfigurarCamposEspecificos()
        {
            if (tipoProcedimiento == "Hospitalizacion")
            {
                // Mostrar campos de hospitalización
                lblSala.Visible = true;
                txtSala.Visible = true;
                lblCama.Visible = true;
                txtCama.Visible = true;
                
                // Ocultar campos de cirugía
                lblQuirofano.Visible = false;
                txtQuirofano.Visible = false;
                lblDuracion.Visible = false;
                numDuracion.Visible = false;
                lblAnestesia.Visible = false;
                cmbAnestesia.Visible = false;
            }
            else if (tipoProcedimiento == "Cirugia")
            {
                // Mostrar campos de cirugía
                lblQuirofano.Visible = true;
                txtQuirofano.Visible = true;
                lblDuracion.Visible = true;
                numDuracion.Visible = true;
                lblAnestesia.Visible = true;
                cmbAnestesia.Visible = true;

                // Ocultar campos de hospitalización
                lblSala.Visible = false;
                txtSala.Visible = false;
                lblCama.Visible = false;
                txtCama.Visible = false;

                // Configurar opciones de anestesia
                cmbAnestesia.Items.Clear();
                cmbAnestesia.Items.AddRange(new[] { "General", "Local", "Regional", "Sedación" });
            }
        }

        private async void CargarDatosIniciales()
        {
            await CargarMascotas();
            await CargarVeterinarios();
            await CargarProcedimientos();
            ConfigurarDataGridView();
        }

        private async Task CargarMascotas()
        {
            try
            {
                var response = await httpClient.GetAsync("Mascota");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    mascotasDisponibles = JsonSerializer.Deserialize<List<Mascota>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Mascota>();

                    cmbMascota.DataSource = mascotasDisponibles;
                    cmbMascota.DisplayMember = "Nombre";
                    cmbMascota.ValueMember = "IdMascota";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar mascotas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarVeterinarios()
        {
            try
            {
                var response = await httpClient.GetAsync("Veterinarios");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    veterinariosDisponibles = JsonSerializer.Deserialize<List<Veterinario>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Veterinario>();

                    cmbVeterinario.DataSource = veterinariosDisponibles;
                    cmbVeterinario.DisplayMember = "Nombre";
                    cmbVeterinario.ValueMember = "IdVeterinario";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar veterinarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarProcedimientos()
        {
            try
            {
                var response = await httpClient.GetAsync($"ProcedimientoMedicoes/ByTipo/{tipoProcedimiento}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    procedimientosDisponibles = JsonSerializer.Deserialize<List<ProcedimientoMedico>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<ProcedimientoMedico>();

                    ActualizarDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar procedimientos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvProcedimientos.AutoGenerateColumns = false;
            dgvProcedimientos.Columns.Clear();
            
            dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mascota",
                HeaderText = "Mascota",
                DataPropertyName = "NombreMascota",
                Width = 120
            });
            
            dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Veterinario",
                HeaderText = "Veterinario",
                DataPropertyName = "NombreVeterinario",
                Width = 150
            });
            
            dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Fecha Inicio",
                DataPropertyName = "FechaInicio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            
            dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

            if (tipoProcedimiento == "Hospitalizacion")
            {
                dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Sala",
                    HeaderText = "Sala",
                    DataPropertyName = "Sala",
                    Width = 80
                });
                
                dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Cama",
                    HeaderText = "Cama",
                    DataPropertyName = "Cama",
                    Width = 80
                });
            }
            else if (tipoProcedimiento == "Cirugia")
            {
                dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Quirofano",
                    HeaderText = "Quirófano",
                    DataPropertyName = "Quirofano",
                    Width = 100
                });
                
                dgvProcedimientos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Duracion",
                    HeaderText = "Duración (min)",
                    DataPropertyName = "DuracionEstimada",
                    Width = 100
                });
            }

            // Columna de acciones
            var btnEditar = new DataGridViewButtonColumn
            {
                Name = "Editar",
                HeaderText = "Acciones",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvProcedimientos.Columns.Add(btnEditar);

            dgvProcedimientos.CellClick += DgvProcedimientos_CellClick;
        }

        private void DgvProcedimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProcedimientos.Columns[e.ColumnIndex].Name == "Editar")
            {
                var procedimiento = dgvProcedimientos.Rows[e.RowIndex].DataBoundItem as ProcedimientoMedicoFormData;
                if (procedimiento != null)
                {
                    CargarProcedimientoParaEdicion(procedimiento.IdProcedimiento);
                }
            }
        }

        private async void CargarProcedimientoParaEdicion(int idProcedimiento)
        {
            try
            {
                var response = await httpClient.GetAsync($"ProcedimientoMedicoes/{idProcedimiento}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    procedimientoSeleccionado = JsonSerializer.Deserialize<ProcedimientoMedico>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (procedimientoSeleccionado != null)
                    {
                        LlenarFormularioConDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar procedimiento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarFormularioConDatos()
        {
            if (procedimientoSeleccionado != null)
            {
                cmbMascota.SelectedValue = procedimientoSeleccionado.IdHistoriaNavigation?.IdMascota;
                cmbVeterinario.SelectedValue = procedimientoSeleccionado.IdVeterinario;
                dtpFechaInicio.Value = procedimientoSeleccionado.FechaInicio;
                
                if (procedimientoSeleccionado.FechaFin.HasValue)
                    dtpFechaFin.Value = procedimientoSeleccionado.FechaFin.Value;
                else
                    dtpFechaFin.Checked = false;

                txtDescripcion.Text = procedimientoSeleccionado.Descripcion ?? "";
                txtObservaciones.Text = procedimientoSeleccionado.Observaciones ?? "";
                numCostoEstimado.Value = procedimientoSeleccionado.CostoEstimado ?? 0;
                cmbEstado.SelectedItem = procedimientoSeleccionado.Estado;

                if (tipoProcedimiento == "Hospitalizacion")
                {
                    txtSala.Text = procedimientoSeleccionado.Sala ?? "";
                    txtCama.Text = procedimientoSeleccionado.Cama ?? "";
                }
                else if (tipoProcedimiento == "Cirugia")
                {
                    txtQuirofano.Text = procedimientoSeleccionado.Quirofano ?? "";
                    numDuracion.Value = procedimientoSeleccionado.DuracionEstimada ?? 0;
                    cmbAnestesia.SelectedItem = procedimientoSeleccionado.TipoAnestesia ?? "";
                }
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            try
            {
                var procedimiento = CrearProcedimientoDesdeFormulario();

                string json;
                HttpResponseMessage response;

                if (procedimientoSeleccionado != null)
                {
                    // Actualizar
                    json = JsonSerializer.Serialize(procedimiento, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    var content = new StringContent(json, Encoding.UTF8, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    response = await httpClient.PutAsync($"ProcedimientoMedicoes/{procedimientoSeleccionado.IdProcedimiento}", content);
                }
                else
                {
                    // Crear nuevo
                    json = JsonSerializer.Serialize(procedimiento, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    var content = new StringContent(json, Encoding.UTF8, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    response = await httpClient.PostAsync("ProcedimientoMedicoes", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Procedimiento guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarProcedimientos();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al guardar: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar procedimiento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            if (cmbMascota.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione una mascota.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbVeterinario.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione un veterinario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor ingrese una descripción.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private ProcedimientoMedico CrearProcedimientoDesdeFormulario()
        {
            var procedimiento = new ProcedimientoMedico
            {
                Tipo = tipoProcedimiento,
                FechaInicio = dtpFechaInicio.Value.Date,
                FechaFin = dtpFechaFin.Checked ? dtpFechaFin.Value.Date : null,
                Descripcion = txtDescripcion.Text,
                Observaciones = txtObservaciones.Text,
                CostoEstimado = numCostoEstimado.Value > 0 ? (decimal?)numCostoEstimado.Value : null,
                Estado = cmbEstado.SelectedItem?.ToString() ?? "Programado",
                IdVeterinario = (int)cmbVeterinario.SelectedValue
            };

            if (tipoProcedimiento == "Hospitalizacion")
            {
                procedimiento.Sala = txtSala.Text;
                procedimiento.Cama = txtCama.Text;
            }
            else if (tipoProcedimiento == "Cirugia")
            {
                procedimiento.Quirofano = txtQuirofano.Text;
                procedimiento.DuracionEstimada = (int)numDuracion.Value;
                procedimiento.TipoAnestesia = cmbAnestesia.SelectedItem?.ToString();
            }

            return procedimiento;
        }

        private void LimpiarFormulario()
        {
            cmbMascota.SelectedIndex = -1;
            cmbVeterinario.SelectedIndex = -1;
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Checked = false;
            txtDescripcion.Clear();
            txtObservaciones.Clear();
            numCostoEstimado.Value = 0;
            cmbEstado.SelectedIndex = 0;
            txtSala.Clear();
            txtCama.Clear();
            txtQuirofano.Clear();
            numDuracion.Value = 0;
            cmbAnestesia.SelectedIndex = -1;
            procedimientoSeleccionado = null;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActualizarDataGridView()
        {
            var procedimientosFormData = procedimientosDisponibles.Select(p => new ProcedimientoMedicoFormData
            {
                IdProcedimiento = p.IdProcedimiento,
                NombreMascota = p.IdHistoriaNavigation?.IdMascotaNavigation?.Nombre ?? "N/A",
                NombreVeterinario = p.IdVeterinarioNavigation?.Nombre ?? "N/A",
                FechaInicio = p.FechaInicio,
                Estado = p.Estado,
                Sala = p.Sala,
                Cama = p.Cama,
                Quirofano = p.Quirofano,
                DuracionEstimada = p.DuracionEstimada
            }).ToList();

            dgvProcedimientos.DataSource = procedimientosFormData;
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

    // Clase auxiliar para mostrar datos en el DataGridView
    public class ProcedimientoMedicoFormData
    {
        public int IdProcedimiento { get; set; }
        public string NombreMascota { get; set; } = string.Empty;
        public string NombreVeterinario { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Sala { get; set; }
        public string? Cama { get; set; }
        public string? Quirofano { get; set; }
        public int? DuracionEstimada { get; set; }
    }
}
