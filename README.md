# Backend: Task Management System

## 📝 Descripción
Este repositorio contiene el backend para el sistema de gestión de tareas. Proporciona una API RESTful desarrollada en .NET 8/9 que permite gestionar usuarios, roles y tareas. Incluye autenticación basada en JWT, un sistema de autorización con roles y una base de datos SQL Server.

---

## 🚀 Tecnologías Utilizadas
- **Framework**: .NET 8
- **Base de Datos**: SQL Server
- **ORM**: Entity Framework Core
- **Autenticación**: JWT (JSON Web Tokens)
- **Validación**: Data Annotations
- **Documentación API**: Swagger/OpenAPI

---

## 🌐 Endpoints Principales
### Tareas
- **GET** `/api/tasks` → Listar todas las tareas (excluye eliminadas).
- **GET** `/api/tasks/{id}` → Obtener una tarea por ID.
- **POST** `/api/tasks` → Crear una nueva tarea.
  ```json
  {
    "title": "Crear API de tareas",
    "description": "Desarrollar el API CRUD para la entidad Task",
    "status": "Pending",
    "priority": "High",
    "dueDate": "2025-09-10T12:00:00Z",
    "assignedTo": 2
  }
  ```
- **PUT** `/api/tasks/{id}` → Editar una tarea.
- **DELETE** `/api/tasks/{id}` → Eliminar (soft delete) una tarea marcándola como `IsDeleted`.
- **PATCH** `/api/tasks/{id}/assign/{userId}` → Asignar una tarea a un usuario.

### Empleados
- **GET** `/api/tasks/employee/user/{userId}` → Listar tareas asignadas a un empleado (usando el procedimiento almacenado).
- **PATCH** `/api/tasks/employee/task/{id}/status` → Actualizar el estado de una tarea.

---

## 🗄 Base de Datos
- **Tablas principales**: 
  - `Users`
  - `Tasks`
- **Procedimiento almacenado: `GetTasksByUser`**
  - **Propósito**: Obtener tareas activas asignadas a un usuario.
  - **Parámetros**: `UserId`, `IsDeleted`.
  - **Resultado**: Lista de tareas activas.
- **Trigger**:
  - **Tabla**: `Tasks`
  - **Tipo**: AFTER UPDATE
  - **Propósito**: Actualizar automáticamente el campo `UpdatedAt`.

---

## 🔐 Datos de Prueba
- **Admin**: `admin@test.com` / `Admin123!`
- **Employee**: `employee@test.com` / `Employee123!`


## 🛠 Deploy
El backend está desplegado en **Google Cloud Run** conectado a **SQL Server en la nube**. 
