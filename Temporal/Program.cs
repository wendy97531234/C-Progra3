using intento1.Models;
using intento1.Repository;
// Abstracción de un objeto Dao
AlumnoDAO alumnoDao = new AlumnoDAO();
// Llamamos al metodo que ceramos en Dao
var alumno = alumnoDao.SelectAll();
// Recorremos la lista
foreach (var item in alumno)
{
    Console.WriteLine(item.Nombre);
}
Console.WriteLine(" ");
// Probamos el select por Id
var selectById = alumnoDao.GetById(10);
Console.WriteLine(selectById?.Nombre);
Console.WriteLine(" ");
// Agregamos un registro
var nuevoAlumno = new Alumno
{
    Direccion = "Nueva Concepcion Chalatenango",
    Dni = "12345",
    Edad = 29,
    Email = "12345@email.com",
    Nombre = "Wendy"
};
var resultado = alumnoDao.insertarAlumno(nuevoAlumno);
Console.WriteLine(resultado);
Console.WriteLine(" ");
// Actualizar un registro
var nuevoAlumno2 = new Alumno
{
    Direccion = "Nueva",
    Dni = "12345",
    Edad = 23,
    Email = "1233345@email.com",
    Nombre = "Jose"
};
var resultado2 = alumnoDao.actualizarAlumno(2, nuevoAlumno2);
Console.WriteLine(resultado2);
Console.WriteLine(" ");
// Borrar un registro
var eliminarAlumno = alumnoDao.borrarAlumno(23);
Console.WriteLine("Se elimino el usuario " + resultado);
