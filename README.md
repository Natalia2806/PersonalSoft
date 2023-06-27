# Servicio RESTful de Poliza de Seguros

Este es un servicio RESTful que ofrece operaciones relacionadas con las polizas de seguros. El servicio utiliza una base de datos local de MongoDB para almacenar los datos de las polizas. A continuación, se proporciona información básica sobre la configuración y el uso del servicio.

## Configuración
Para ejecutar el servicio, asegúrate de tener instalado MongoDB en tu entorno local y realiza los siguientes pasos:

1. Clona el repositorio del servicio desde (https://github.com/Natalia2806/PersonalSoft.git).
2. Abre el archivo appsettings.json y verifica la configuración de conexión a la base de datos MongoDB. Asegúrate de proporcionar la URL correcta y las credenciales de acceso si es necesario.
3. Ejecuta los comandos de migración para configurar la base de datos y crear las colecciones necesarias.
   
## Uso del servicio
El servicio ofrece varias rutas para realizar operaciones con las polizas de seguros. A continuación, se describen las principales rutas y sus funcionalidades:

### Autenticación
- **POST /login:** Permite a un usuario iniciar sesión y obtener un token de acceso para utilizar los demás servicios. Debes proporcionar las credenciales de usuario (correo electrónico y contraseña) en el cuerpo de la solicitud.

### Polizas
- **GET /api/poliza:** Obtiene todas las polizas de seguros almacenadas en la base de datos.
- **GET /api/poliza/{placaVehiculo}/{numPoliza}:** Obtiene los detalles de una poliza específica según su placa o nímero de poliza.
- **POST /api/poliza:** Crea una nueva poliza de seguro. Debes proporcionar los detalles de la poliza en el cuerpo de la solicitud.
- **PUT /api/poliza/{id}:** Actualiza los detalles de una poliza existente según su ID. Debes proporcionar los nuevos detalles de la poliza en el cuerpo de la solicitud.
- **DELETE /api/poliza/{id}:** Elimina una poliza de seguro según su ID.
  
### Pruebas Unitarias
Se han creado tres pruebas unitarias basadas en los controladores para garantizar el correcto funcionamiento del servicio. Las pruebas cubren escenarios clave y verifican el comportamiento esperado de los controladores.

Ejecución de las pruebas unitarias
Para ejecutar las pruebas unitarias, sigue los siguientes pasos:

1. Abre el proyecto de pruebas unitarias en tu entorno de desarrollo.
2. Asegúrate de que el servicio esté en ejecución.
3. Ejecuta las pruebas unitarias desde el entorno de pruebas.
