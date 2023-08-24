using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.WebSockets;

namespace Lib_logica_usuario
{
    public class Cls_Usuario
    {
        public string ResultadoNombres { get; private set; }
        public string ResultadoApellidos { get; private set; }
        public string ResultadoDocumento { get; private set; }
        public int ResultadoEdad { get; private set; }
        public string ResultadoGenero { get; private set; }
        public DateTime ResultadoFecha { get; private set; }



        private int id;
        private string nombre;
        private string apellido;
        private string documento;
        private int edad;
        private string genero;
        private DateTime fecha = DateTime.Now;
        private string error;
        private SqlDataReader objReader;

        public Cls_Usuario()
        {
            this.id = 0;
            this.nombre = "";
            this.apellido = "";
            this.documento = "";
            this.edad = 0;
            this.genero = "";
            this.fecha = DateTime.Now;
            this.error = "";
        }

        

        public int gsId { get => id; set => id = value; }
        public string gsNombre { get => nombre; set => nombre = value; }
        public string gsApellido { get => apellido; set => apellido = value; }
        public string gsDocumento { get => documento; set => documento = value; }
        public int gsEdad { get => edad; set => edad = value; }
        public string gsGenero { get => genero; set => genero = value; }
        public DateTime gsFecha { get => fecha; set => fecha = value; }
        public string gsError { get => error; set => error = value; }
        public SqlDataReader ObjReader { get => objReader; set => objReader = value; }

        public bool guardarUsuario()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("server=SANTI\\SQLEXPRESS; database=informacion; integrated security=true;"))
                {
                    conexion.Open();

                    string sentenciaRepetir = "SELECT COUNT(*) FROM paciente WHERE id = @ID";
                    SqlCommand comandoExite = new SqlCommand(sentenciaRepetir, conexion);
                    comandoExite.Parameters.AddWithValue("@ID", id);

                     int siExiste = (int) comandoExite.ExecuteScalar();

                    if(siExiste > 0)
                    {
                        gsError = "Id existente, iungrese otro";
                        return false;
                    }

                    string sentencia = "EXECUTE sp_guardar_pa " + id + ", '" + nombre + "', '" + apellido + "', '" + documento + "', " + edad + ", '" + genero + "', '" + fecha.ToString("yyyy-MM-dd") + "'";
                    SqlCommand comando = new SqlCommand(sentencia, conexion);

                    comando.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                gsError = ex.Message;
                return false;
            }
        }

        public bool eliminarUsuario()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("server=SANTI\\SQLEXPRESS; database=informacion; integrated security=true;"))
                {
                    conexion.Open();


                    string sentencia = "EXECUTE sp_eliminar_pa " + id + "";
                    SqlCommand comando = new SqlCommand(sentencia, conexion);


                    comando.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                gsError = ex.Message;
                return false;
            }
        }

        public bool buscarUsuario()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("server=SANTI\\SQLEXPRESS; database=informacion; integrated security=true;"))
                {
                    conexion.Open();

                    string sentencia = "EXECUTE sp_buscar_pa " + id + " ";
                    SqlCommand comando = new SqlCommand(sentencia, conexion);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ResultadoNombres = reader.GetString(1);
                            ResultadoApellidos = reader.GetString(2);
                            ResultadoDocumento = reader.GetString(3);
                            ResultadoEdad = reader.GetInt32(4);
                            ResultadoGenero = reader.GetString(5);
                            ResultadoFecha = reader.GetDateTime(6);
                            reader.Close();
                            return true;
                        }
                        else
                        {
                            ResultadoNombres = "";
                            ResultadoApellidos = "";
                            ResultadoDocumento = "";
                            ResultadoEdad = 0;
                            ResultadoGenero = "";
                            ResultadoFecha = DateTime.MinValue;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gsError = ex.ToString();
                return false;
            }
        }


        public bool actualizarUsuario()
        {
            using (SqlConnection conexion = new SqlConnection ("server=SANTI\\SQLEXPRESS; database=informacion; integrated security=true;"))
            {
                try
                {
                    conexion.Open();

                    string sentencia = "EXECUTE sp_actualizar_pa " + id + ", '" + nombre + "', '" + apellido + "', '" + documento + "', " + edad + ", '" + genero + "', '" + fecha.ToString("yyyy-MM-dd") + "'";
                    SqlCommand comando = new SqlCommand(sentencia, conexion);

                    comando.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    gsError = ex.Message;
                    return false;
                }


            }
            
        }

    }
}
