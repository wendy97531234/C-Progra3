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
            // El contexto tiene referecniada todos los modelos
            // Dentro de EF tenemos el metodo modelo.ToList<Modelo>
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

    }
}
