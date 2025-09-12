using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioConsultorios : Repositorio<Consultorio>, IRepositorioConsultorios // Hereda de la clase genérica Repositorio y especifica la entidad Consultorio
    {
        public RepositorioConsultorios(DientesLimpiosDbContext context) : base(context) // Llama al constructor de la clase base con el contexto de la base de datos
        {
            
        }
    }
}
