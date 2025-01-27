# PruebaTecnicaImagineApp
# Servicio de Gestión de Tareas

## Tecnología Utilizada
- **Framework:** .NET 8.0 (actualizado desde la versión requerida .NET 6.0)
- **Base de Datos:** PostgreSQL
- **Backup de Base de Datos:** Carpeta `/basedatos`
- **Razones para usar .NET 8.0 en lugar de .NET 6.0:**
  - Mejor rendimiento y optimización
  - Características más recientes y mejoradas
  - Mayor soporte a largo plazo (LTS)
  - Nuevas funcionalidades de C# 12
  - Mejoras en la seguridad y estabilidad

## Arquitectura: Orientada a Servicios (SOA)
### Ventajas de SOA
1. **Modularidad**
   - Servicios independientes para gestión de tareas
   - Mantenimiento simplificado
   - Escalabilidad horizontal y vertical

2. **Interoperabilidad**
   - Integración sencilla con otros sistemas
   - API RESTful para comunicación estándar
   - Formato JSON para intercambio de datos

3. **Flexibilidad**
   - Servicios reutilizables para diferentes tipos de tareas
   - Implementación independiente de componentes
   - Facilidad de actualización y mejoras

4. **Reducción de Acoplamiento**
   - Independencia entre módulos de tareas
   - Mayor facilidad de pruebas
   - Mantenimiento más eficiente

## Configuración
- **Servidor Local:** http://localhost:8080
- **Conexión Base de Datos:** Configurada en `appsettings.json`

## Requisitos
- .NET 8.0 SDK
- PostgreSQL
- Visual Studio 2022 (versión 17.8 o superior)

## Instalación
1. Clonar repositorio
2. Restaurar base de datos desde `/basedatos`
3. Configurar cadena de conexión en `appsettings.json`
4. Restaurar paquetes NuGet
5. Ejecutar proyecto

## Funcionalidades Principales
- Creación de tareas
- Actualización de estado de tareas
- Listado de tareas con filtros
- Eliminación de tareas
- Asignación de tareas
