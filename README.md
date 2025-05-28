# Discord_personal_project

Este proyecto es una implementación personal de funcionalidades similares a Discord, desarrollado en C#. Está estructurado en varios servicios que gestionan diferentes aspectos de la aplicación, como canales, servidores y autenticación de usuarios.

## Estructura del Proyecto:
El repositorio se organiza en los siguientes servicios:

- Channels_service: Maneja la creación, edición y eliminación de canales de comunicación.

- Servers_service: Administra la creación y gestión de servidores o grupos de usuarios.

- Login_service: Gestiona la autenticación y autorización de los usuarios.

## Tecnologías Utilizadas

- Lenguaje de Programación: C#

- Contenedorización: Docker

## API Endpoints.

### Channels Service

- GET	/channels	Listar todos los canales
- POST	/channels	Crear un nuevo canal
- PUT	/channels/{id}	Editar un canal
- DELETE	/channels/{id}	Eliminar un canal

### Servers Service
  
- GET	/servers/{server}	Obtener un servidor
- POST	/servers/post	Crear un servidor
- DELETE /servers/delete Borrar un servidor
- POST /servers/postUser/{server} Agregar un usuario a un servidor
- DELETE /servers/deleteUser/{server} Borra a un usuario de un servidor

### Login Service
  
- POST	/login	Autenticación de usuario
- POST	/register	Registro de nuevo usuario
