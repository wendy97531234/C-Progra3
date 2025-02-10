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