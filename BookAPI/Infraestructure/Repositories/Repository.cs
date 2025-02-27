using Application.Interfaces.Repository;
using System.Runtime.Intrinsics.X86;

namespace Infraestructure.Repositories
{
    public class Repository : IRepository
    {
        //Inyeccion de dependencia del contexto
        //internal: Puede obtener acceso al tipo o miembro cualquier código del mismo
        //ensamblado, pero no de un ensamblado distinto.En otras palabras,
        //se puede acceder a tipos o miembros internal desde el código que forma parte de la misma compilación.
        //Esto evita que otros componente externo manipulen el contexto, protegiente la db
        internal readonly ApplicationContext _context;


        //Ahora context pasa a ser el acceso a la base de datos.
        //El repo se encarga de armas las queries y devolver la data para que otros
        //componentes del sistema no se tengan que preocupar de cómo obtener esa informacion ni de que manera o formato
        //De esta forma de herea el context en lugar de inyectarlo direcamente en cada rapositoy
        //Evita la redundancia de codigo
        //Facilita el mantenimiento
        //Organizacion de codigo (DRY -> Don't Repeat Yourself)
        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {

                return _context.SaveChanges() >= 0;
             }

    }
}
