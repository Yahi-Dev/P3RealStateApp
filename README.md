Nombre del Proyecto: Gestión de Propiedades Inmobiliarias

Descripción:
Este proyecto es una completa solución para la gestión de propiedades inmobiliarias, diseñada para brindar una experiencia intuitiva y eficiente tanto para agentes inmobiliarios como para clientes.
Utilizando tecnologías modernas y las mejores prácticas de desarrollo, esta aplicación ofrece una amplia gama de funcionalidades que abarcan desde la visualización de propiedades hasta la administración
de usuarios y configuraciones del sistema.

Funcionalidades Destacadas:

* Listado de Propiedades: Desde la pantalla inicial, los usuarios pueden explorar todas las propiedades disponibles, con opciones de filtrado y búsqueda para una navegación fácil y personalizada.
* Detalle de Propiedades: Al hacer clic en una propiedad, los usuarios pueden acceder a información detallada, incluidas imágenes, características, detalles de contacto del agente y más.
* Gestión de Usuarios: Los agentes inmobiliarios pueden gestionar sus propiedades y perfiles, mientras que los clientes tienen la capacidad de marcar propiedades como favoritas y acceder a su propio listado personalizado.
* Administración del Sistema: Los administradores pueden supervisar y administrar usuarios, propiedades, tipos de propiedades, tipos de ventas y mejoras, asegurando un control total sobre el funcionamiento de la plataforma.

Funcionalidades del API

Funcionalidades generales

Login y Seguridad:
* El sistema cuenta con dos roles: administrador y desarrollador.
* La seguridad en los endpoints del API se realiza mediante JWT.
* Se crean por defecto los roles de administrador y desarrollador, así como usuarios con cada uno de estos roles.
  
Controlador de cuentas (AccountController):
* Login: Permite autenticarse y obtener el token JWT.
* Registro de usuario desarrollador: Crea un usuario con el rol de desarrollador.
* Registro de usuario administrador: Crea un usuario con el rol de administrador, con validación de usuario logueado y de tipo administrador.
  
Restricciones de acceso:
* Los usuarios deben estar autenticados para acceder a las funcionalidades de administrador o desarrollador.
* Se manejan adecuadamente los errores de acceso no autorizado (401) y acceso prohibido (403).
* Validaciones usando filtros de autorización de Identity y JWT.
* Controlador de propiedades
  
Datos de una propiedad:
* Id, Código, Tipo de propiedad, Tipo de venta, Precio, Tamaño del terreno, Habitaciones, Baños, Descripción, Mejoras, Nombre del agente, Id del agente.
 
Endpoints:
* Listar todas las propiedades.
* Obtener una propiedad por su Id.
* Obtener una propiedad por su Código.
* Controlador de agentes
  
Datos de un agente:
* Id, Nombre, Apellido, Cantidad de propiedades, Correo, Teléfono.
  
Endpoints:
* Listar todos los agentes.
* Obtener un agente por su Id.
* Obtener las propiedades de un agente por su Id.
* Cambiar el estado de un agente (activo/inactivo).
* Mantenimiento de tipo de propiedades
  
Datos de un tipo de propiedad:
* Id, Nombre, Descripción.
  
Endpoints:
* Crear un nuevo tipo de propiedad.
* Actualizar un tipo de propiedad por su Código.
* Listar todos los tipos de propiedades.
* Obtener un tipo de propiedad por su Código.
* Eliminar un tipo de propiedad por su Código.
  
Mantenimiento de tipo de ventas

Datos de un tipo de venta:
* Id, Nombre, Descripción.
  
Endpoints:
* Crear un nuevo tipo de venta.
* Actualizar un tipo de venta por su Id.
* Listar todos los tipos de venta.
* Obtener un tipo de venta por su Id.
* Eliminar un tipo de venta por su Id.
  
Mantenimiento de mejoras

Datos de una mejora:
* Id, Nombre, Descripción.
  
Endpoints:
* Crear una nueva mejora.
* Actualizar una mejora por su Id.
* Listar todas las mejoras.
* Obtener una mejora por su Id.
* Eliminar una mejora por su Id.

  
Requerimientos técnicos
* Uso de ViewModels para validaciones.
* Uso de Entity Framework con Code First para la persistencia de datos.
* Visualmente entendible con Bootstrap.
* Arquitectura Onion aplicada correctamente.* 
* Uso de repositorios y servicios genéricos.
* Uso de Identity para manejo de usuarios.
* Uso de AutoMapper para el mapeo de entidades.
* Seguridad del API con JWT e Identity.
* Implementación de los patrones de diseño CQRS y Mediator.
* Documentación del API con Swagger.
* Precios en peso dominicano.
* Consistencia en filtros de propiedades en pantallas de listado.
* Manejo de inicio de sesión según tipo de usuario en Web App y Web API.


OJO: Al momento de alguien bajar los cambios debe de cambiar solamente del appsettings.json (en el proyecto de WebApi y del WebApp)
el IdentityConnection y el DefaultConnetion con la ruta de su base de datos, luego vas a Package Manager Console y en la opción
que dice Default Projects lo vas a poner primero en la capa de identity y vas a escribir: Update-Database -Context IdentityContext,
Lo mismo vas hacer con la capa de persistence, vas a cambiar de la capa de identity a persistencia y en el panel de escritura vas a escribir:
Update-Database -Context ApplicationContext. Sino haces estos cambios no te va a funcionar la app y te va a dar un error de mapeo con la base de datos.
