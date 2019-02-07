using BLL;
using Entidades;
using Registro_Cartas.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Registro_Cartas.UI.Registros
{
    public partial class rCartas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Cartas cuenta = new Cartas();

            if (!Page.IsPostBack)
            {
                LlenarCombos();
                ViewState["Destinatario"] = new Destinatarios();
            }
        }

        private Cartas LlenaClase()
        {
            Cartas cartas = new Cartas();

            cartas.CartaId = Utils.ToInt(cartasIdTextBox.Text);
            cartas.Fecha = Convert.ToDateTime(fechaTextBox.Text).Date;
            cartas.DestinatarioId = Utils.ToInt(DestinatarioDropDownList.SelectedValue);
            cartas.Cuerpo = cuerpoTextBox.Text;

            return cartas;
        }

        private void LlenaCampos(Cartas cartas)
        {
            cartasIdTextBox.Text = cartas.CartaId.ToString();
            fechaTextBox.Text = cartas.Fecha.ToString();
            DestinatarioDropDownList.Text = cartas.DestinatarioId.ToString();
            cuerpoTextBox.Text = cartas.Cuerpo.ToString();
        }

        void LlenarCombos()
        {
            RepositorioBase<Cartas> repositorio = new RepositorioBase<Cartas>();
            DestinatarioDropDownList.DataSource = repositorio.GetList(c => true);
            DestinatarioDropDownList.DataValueField = "DestinatarioId";
            DestinatarioDropDownList.DataTextField = "Nombre";
            DestinatarioDropDownList.DataBind();
            DestinatarioDropDownList.Items.Insert(0, new ListItem("", ""));
        }

        private void Limpiar()
        {
            cartasIdTextBox.Text = "";
            fechaTextBox.Text = "";
            DestinatarioDropDownList.SelectedIndex = 0;
            cuerpoTextBox.Text = "";

        }

        protected void buscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Cartas> repositorio = new RepositorioBase<Cartas>();
            Cartas cartas = repositorio.Buscar(Utils.ToInt(cartasIdTextBox.Text));

            if (cartas != null)
            {
                LlenaCampos(cartas);
            }
            else
            {
                Limpiar();
                Utils.ShowToastr(this, "No Se Encontro En La BD", "Error", "error");

            }
        }

        protected void nuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void guadarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Cartas> repositorio = new BLL.RepositorioBase<Cartas>();
            Cartas cartas = new Cartas();
            bool paso = false;

            cartas = LlenaClase();

            if (cartas.CartaId == 0)
            {
                paso = repositorio.Guardar(cartas);
                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                Limpiar();
            }
            else
            {
                int id = Utils.ToInt(cartasIdTextBox.Text);
                BLL.RepositorioBase<Cartas> repository = new BLL.RepositorioBase<Cartas>();
                cartas = repository.Buscar(id);

                if (cartas != null)
                {
                    paso = repositorio.Modificar(LlenaClase());
                    Utils.ShowToastr(this, "Modificado Correctamente", "Exito", "success");
                }
                else
                    Utils.ShowToastr(this, "Id no existe", "Error", "error");
            }

            if (paso)
            {
                Limpiar();
            }
            else
                Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
        }

        protected void eliminarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Cartas> repositorio = new BLL.RepositorioBase<Cartas>();
            int id = Utils.ToInt(cartasIdTextBox.Text);

            var cuentas = repositorio.Buscar(id);

            if (cuentas == null)
                Utils.ShowToastr(this, "No Se Pudo Elliminar ", "Error", "error");

            else
                repositorio.Eliminar(id);

            Utils.ShowToastr(this, " Eliminado Correctamente ", "Success", "success");

            Limpiar();
        }
    }
}