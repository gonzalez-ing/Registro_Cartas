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
    public partial class rDestinatario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            totalTextBox.Text = "0";
        }

        private Destinatarios LlenaClase()
        {
            Destinatarios destinatarios = new Destinatarios();

            destinatarios.DestinatarioId = Utils.ToInt(destinatarioIdTextBox.Text);
            destinatarios.Nombre = nombreTextBox.Text;
            destinatarios.Direccion = direccionTextBox.Text;
            destinatarios.Telefono = telefonoTextBox.Text;
            destinatarios.TotalCartas = Utils.ToInt(totalTextBox.Text);

            return destinatarios;

        }
        private void Limpiar()
        {
            destinatarioIdTextBox.Text = "0";
            nombreTextBox.Text = " ";
            direccionTextBox.Text = " ";
            telefonoTextBox.Text = " ";
            totalTextBox.Text = "0";
        }
        private void LlenaCampos(Destinatarios destinatarios)
        {
            destinatarioIdTextBox.Text = destinatarios.DestinatarioId.ToString();
            nombreTextBox.Text = destinatarios.Nombre.ToString();
            telefonoTextBox.Text = destinatarios.Telefono.ToString();
            totalTextBox.Text = destinatarios.TotalCartas.ToString();
        }

        protected void buscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Destinatarios> repositorio = new RepositorioBase<Destinatarios>();
            Destinatarios destinatarios = repositorio.Buscar(Utils.ToInt(destinatarioIdTextBox.Text));

            if (destinatarios != null)
            {
                LlenaCampos(destinatarios);
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
            BLL.RepositorioBase<Destinatarios> repositorio = new BLL.RepositorioBase<Destinatarios>();
            Destinatarios destinatarios = new Destinatarios();
            bool paso = false;

            destinatarios = LlenaClase();

            if (destinatarios.DestinatarioId == 0)
            {
                paso = repositorio.Guardar(destinatarios);
                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                Limpiar();
            }
            else
            {
                int id = Utils.ToInt(destinatarioIdTextBox.Text);
                BLL.RepositorioBase<Destinatarios> repository = new BLL.RepositorioBase<Destinatarios>();
                destinatarios = repository.Buscar(id);

                if (destinatarios != null)
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
            BLL.RepositorioBase<Destinatarios> repositorio = new BLL.RepositorioBase<Destinatarios>();
            int id = Utils.ToInt(destinatarioIdTextBox.Text);

            var destinatarios = repositorio.Buscar(id);

            if (destinatarios == null)
                Utils.ShowToastr(this, "No Se Pudo Elliminar ", "Error", "error");

            else
                repositorio.Eliminar(id);

            Utils.ShowToastr(this, " Eliminado Correctamente ", "Success", "success");

            Limpiar();
        }
    }
}