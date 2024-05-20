﻿using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Dtos.Account
{
    /// <summary>
    /// Parámetros para realizar la autenticacion del usuario
    /// </summary> 
    public class AuthenticationRequest
    {
        [SwaggerParameter(Description = "Correo del usuario que desea iniciar seccion")]
        public string Email { get; set; }
        [SwaggerParameter(Description = "Contrasenia del usuario que desea iniciar seccion")]
        public string Password { get; set; }
    }
}
