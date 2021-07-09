# technical-test-google-api
API de geolocalización del usuario a traves de GPS o dirección IP.

## Compilación
- Es necesario modificar el **ConnectionString** hacia la base de datos que se encuentra dentro del proyecto 
  **TechnicalTestGoogleApi** en el archivo **appsettings** por la base de datos personal a utilizar.
- Se deben ejecutar las migraciones que estan dentro del proyecto Data. 
  Desde la **Consola del Administrador de paquetes** seleccionar el proyecto **Data** y ejecutar el comando **Update-Database**

## Caracteristicas
- Documentación en Swagger.
- Administración de sesiones con token JWT y refresh session.

## Detalles técnicos
- El API ha sido creado bajo la plataforma .NET 5 por lo que es necesario tenerlo disponible.
- La base de datos MSSQL Server.
