using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib_logica_usuario;

namespace FORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void lim()
        {
            txtId.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDocumento.Text = "";
            txtEdad.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }


        // GUARDAR

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardar()
        {
            Cls_Usuario objUsu = new Cls_Usuario();

            try
            {
                objUsu.gsId = Convert.ToInt32(txtId.Text);
                objUsu.gsNombre = txtNombres.Text;
                objUsu.gsApellido = txtApellidos.Text;
                objUsu.gsDocumento = txtDocumento.Text;
                objUsu.gsEdad = Convert.ToInt32(txtEdad.Text);

                if (radioButton1.Checked)
                {
                    objUsu.gsGenero = "M";
                }

                if (radioButton2.Checked)
                {
                    objUsu.gsGenero = "F";
                }

                objUsu.gsFecha = dateTimePicker1.Value;



            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            if (objUsu.guardarUsuario())
            {
                MessageBox.Show("Se guardó correctamente");
                lim();
            }
            else
            {
                MessageBox.Show(objUsu.gsError);
                return;
            }
        }


        // ELIMINAR

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            if (MessageBox.Show("¿quieres eliminar este usuario?", "message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cls_Usuario objUsu = new Cls_Usuario();

                try
                {
                    objUsu.gsId = Convert.ToInt32(txtId.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                if (!objUsu.eliminarUsuario())
                {
                    MessageBox.Show(objUsu.gsError);
                    return;
                }
                else 
                {
                    MessageBox.Show("se eliminó con exito el usuario");
                    lim();
                }
            


            }



        }

        // BUSCAR

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            Cls_Usuario objUsu = new Cls_Usuario();
            try
            {
                objUsu.gsId = Convert.ToInt32(txtId.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (objUsu.buscarUsuario())
            {
                txtNombres.Text = objUsu.ResultadoNombres;
                txtApellidos.Text = objUsu.ResultadoApellidos;
                txtDocumento.Text = objUsu.ResultadoDocumento;
                txtEdad.Text = objUsu.ResultadoEdad.ToString();
                if (objUsu.ResultadoGenero == "M")
                {
                    radioButton1.Checked = true;
                }
                else if (objUsu.ResultadoGenero == "F")
                {
                    radioButton2.Checked = true;
                }
                dateTimePicker1.Value = objUsu.ResultadoFecha;

                btnGuardar.Enabled = false;

            }
            else
            {
                MessageBox.Show("No se encontró el usuario");
            }

        }



        // LIMPIAR

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDocumento.Text = "";
            txtEdad.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizar();
        }


        private void actualizar()
        {
            Cls_Usuario objUsu = new Cls_Usuario();

            try
            {
                objUsu.gsId = Convert.ToInt32(txtId.Text);
                objUsu.gsNombre = txtNombres.Text;
                objUsu.gsApellido = txtApellidos.Text;
                objUsu.gsDocumento = txtDocumento.Text;
                objUsu.gsEdad = Convert.ToInt32(txtEdad.Text);

                if (radioButton1.Checked)
                {
                    objUsu.gsGenero = "M";
                }

                if (radioButton2.Checked)
                {
                    objUsu.gsGenero = "F";
                }

                objUsu.gsFecha = dateTimePicker1.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (!objUsu.actualizarUsuario())
            {
                MessageBox.Show(objUsu.gsError);
                return;
            }
            else
            {
                MessageBox.Show("Se actualizó correctamente");
                lim();
            }


        }

       
    }


}
