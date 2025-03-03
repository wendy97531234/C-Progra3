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
        #endregion
        #region LeftJoin
        public List<AlumnoAsignatura> SelectAlumAsig()
        {
            var consulta = from a in Contexto.Alumnos
                           join m in Contexto.Matriculas on a.Id equals m.AlumnoId
                           join asig in Contexto.Asignaturas on m.AsignaturaId equals asig.Id
                           select new AlumnoAsignatura
                           {
                               nombreAlumno = a.Nombre,
                               nombreasignatura = asig.Nombre
                           };
            return consulta.ToList();
        }
        #endregion


        public List<AlumnoProfesor> AlumnoProfesor(string nombreProfesor)
        {
            var listadoALumno = from a in Contexto.Alumnos
                                join m in Contexto.Matriculas on a.Id equals m.AlumnoId
                                join asig in Contexto.Asignaturas on m.AsignaturaId equals asig.Id
                                where asig.Profesor == nombreProfesor
                                select new AlumnoProfesor
                                {
                                    Id = a.Id,
                                    Dni = a.Dni,
                                    Nombre = a.Nombre,
                                    Direccion = a.Direccion,
                                    Edad = a.Edad,
                                    Email = a.Email,
                                    Asignatura = asig.Nombre
                                };

            return listadoALumno.ToList();
        }
        #region
        public bool update(int id, Alumno actualizar)
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
        #region SelccionarPorDni
        /// <summary>
        /// Este metodo devolvera null si no exiate el DNI indicado, recibe un alumno y apartir de el se extrae el DNI se devuelve el estudiandiante en caso de exito
        /// </summary>
        /// <param name="alumno"> es de tipo Alumno </param>
        /// <returns> Alumno </returns>
        public Alumno DNIAlumno(Alumno alumno)
        {
            var alumnos = Contexto.Alumnos.Where(x => x.Dni == alumno.Dni).FirstOrDefault();
            return alumnos == null ? null : alumnos;
        }
        #endregion
        #region AlumnoMatricula
        public bool InsertarMatricula(Alumno alumno, int idAsing)
        {
            // se utiliza  un bloque con el cual  detectaremos las exepciones que nos pueda dar la inserccion 
            try
            {

                //comprobar si existe el DNI en los alumnos
                var alumnoDNI = DNIAlumno(alumno);
                //si existe solo lo añadimos pero si no lo debemos de insertar
                if (alumnoDNI == null)
                {
                    insertarAlumno(alumno);
                    // si en null creamos el alumno pero ahora debemos de matricular el alumno con el Dni que corresponda
                    var alumnoInsertado = DNIAlumno(alumno);
                    // ahora debemos crear un objeto matricula para poder hacer la insercion de ambas llaves
                    var unirAlumnoMatricula = matriculaAsignaturaALumno(alumno, idAsing);
                    if (unirAlumnoMatricula == false)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    matriculaAsignaturaALumno(alumnoDNI, idAsing);
                    return true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Matriucla
        /// <summary>
        /// Relaciona el Id del alumno con el ID de la matricula 
        /// se definel el id de la asignatura
        /// Para ello el metodo crea una instancia de Matricula he inicializa los campos idAlumno e id Asignatura
        /// si el registro se guarda  devuelve true de lo contrario False
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="idAsignatura"></param>
        /// <returns>  bool</returns>
        public bool matriculaAsignaturaALumno(Alumno alumno, int idAsignatura)
        {
            try
            {
                Matricula matricula = new Matricula();
                //usaremos los campos AlumnoId y asignaturaId
                matricula.AlumnoId = alumno.Id;
                matricula.AsignaturaId = idAsignatura;
                // Guardamos el cambio que se realizo al momento de insertar.
                Contexto.Matriculas.Add(matricula);
                Contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }

}
