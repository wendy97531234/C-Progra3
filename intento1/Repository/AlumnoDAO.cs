using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using intento1.Context;
using intento1.Models;

namespace intento1.Repository
{
    public class AlumnoDAO
    {
        #region Contex


        public RegistroAlumnoContext Contexto = new RegistroAlumnoContext();
        #endregion
        #region SelectAll

        public List<Alumno> SelectAll()
        {
            // Creamos una variable var que es generica 
            
            var alumno = Contexto.Alumnos.ToList<Alumno>();
            return alumno;
        }
        #endregion

        #region Seleccionamos por ID
        public Alumno? GetById(int id)
        {
            var alumno = Contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }
        #endregion
        #region insertar
        public bool insertarAlumno(Alumno alumno)
        {
            try
            {
                var alum = new Alumno
                {
                    Direccion = alumno.Direccion,
                    Edad = alumno.Edad,
                    Email = alumno.Email,
                    Dni = alumno.Dni,
                    Nombre = alumno.Nombre
                };
                Contexto.Alumnos.Add(alum);
                // Este elemnto en si no nos guardara los datos para ello debemos utilizar el metodo save
                Contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Actualizar
        public bool actualizarAlumno(int id, Alumno actualizar)
        {
            try
            {
                var alumnoUpdate = GetById(id);
                if (alumnoUpdate == null)
                {
                    Console.WriteLine("Alumno es null");
                    return false;
                }
                alumnoUpdate.Direccion = actualizar.Direccion;
                alumnoUpdate.Dni = actualizar.Dni;
                alumnoUpdate.Nombre = actualizar.Nombre;
                alumnoUpdate.Email = actualizar.Email;
                Contexto.Alumnos.Update(alumnoUpdate);
                Contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region  borrar

        public bool borrarAlumno(int id)
        {
            var borrar = GetById(id);
            try
            {
                if (borrar == null)
                {
                    return false;
                }
                else
                {
                    Contexto.Alumnos.Remove(borrar);
                    Contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
    }
}

#endregion



