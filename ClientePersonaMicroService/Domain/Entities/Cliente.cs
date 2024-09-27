using System;
namespace ClientePersonaMicroService.Domain.Entities
{
    public class Cliente : Persona
    {
        public int Id { get; set; }  // Clave única
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
    }
}

