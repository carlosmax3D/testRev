Infraestructura que NO deben modificar
Program.cs // Aqui solo para agregar las interfaces nuevas que vayan a usar
App.config (cadena de conexión DefaultConnection, leída por Data/Configuracion.cs)
DatabaseConnection.cs
estructura de carpetas
configuración de DI

Código que SÍ deben desarrollar
Models
Interfaces
Repositories
Services
Forms
SQL

A evaluar:

- Interfaces
- Abstracción
- Encapsulamiento
- Polimorfismo
- Dependency Injection
- Repository Pattern
- Service Layer
- SQL parametrizado
- Separación de responsabilidades
- Manejo de excepciones
- Configuración externa
- Windows Forms
- SQL Server

----------------------------------------------------------------------
PIA - Venta de boletos para partidos (base de datos VenPardios)
----------------------------------------------------------------------

1. En SQL Server Management Studio ejecuta, en orden, los scripts de la
   carpeta Database:
      01_CreacionBaseDatos.sql  (tablas, llaves, índices, carga inicial y diccionario)
      02_Procedimientos.sql     (funciones, vistas, triggers y procedimientos almacenados)
      03_Respaldo.sql           (genera el respaldo .bak; se ejecuta al final)
2. En App.config, en Server=, va el nombre del servidor que aparece en
   "Server name" al conectarte en SSMS. La base de datos es VenPardios.
3. Abre TemplateCSharp.sln y presiona F5.

Usuarios de prueba:
   Administrador  admin@gmail.com      AdminPrueba1!
   Operativo      operativo@gmail.com  Operativo1!
