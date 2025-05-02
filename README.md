# 🏡 Million Properties

**Million Properties** es una aplicación web fullstack para la gestión y visualización de propiedades inmobiliarias. El sistema permite listar, filtrar y consultar en detalle propiedades, junto con sus imágenes asociadas, ofreciendo una experiencia fluida, moderna y desacoplada.

---

## 🧩 Arquitectura General

La solución está compuesta por tres componentes principales:

| Componente        | Descripción                                                                 |
| ----------------- | --------------------------------------------------------------------------- |
| **Frontend**      | Aplicación SPA desarrollada con React + Vite y TypeScript.                  |
| **Backend**       | API RESTful construida con .NET 8 y MongoDB, bajo una arquitectura robusta. |
| **Base de Datos** | MongoDB, con modelo embebido para representar las imágenes de propiedades.  |

La comunicación entre frontend y backend se realiza vía HTTP sobre JSON (REST), y todo el ecosistema puede ser desplegado mediante **Docker Compose** o ejecutado de forma local con configuraciones mínimas.

---

## 🛠️ Tecnologías y Arquitectura del Backend

La API REST está desarrollada sobre .NET 8, siguiendo principios modernos de diseño y separación de responsabilidades:

- **Clean Architecture**  
  Estructura en capas bien definidas (`Domain`, `Application`, `Infrastructure`, `Web.API`) que permite escalabilidad, mantenibilidad y facilidad de pruebas.

- **Arquitectura Hexagonal (Ports & Adapters)**  
  El acceso a infraestructura (bases de datos, servicios externos) se realiza a través de interfaces inyectadas, permitiendo un desacoplamiento completo de la lógica de negocio.

- **CQRS (Command Query Responsibility Segregation)**  
  Las operaciones de lectura y escritura se implementan mediante *Handlers* separados, garantizando claridad y evitando efectos colaterales no deseados.

- **DDD (Domain-Driven Design)**  
  Las entidades como `Property` encapsulan su lógica y se agrupan en contextos bien definidos. El diseño refleja el dominio real.

- **Seeder automático**  
  El backend incluye un seeder que detecta si la base de datos está vacía y genera propiedades de ejemplo en la primera ejecución, incluyendo imágenes públicas.

- **Validación declarativa con FluentValidation**  
  Cada Query y Command puede contar con su clase `Validator` para aplicar validaciones sólidas antes de ejecutar lógica de negocio.

- **MediatR para orquestación de solicitudes**  
  Todos los Queries son manejados mediante MediatR, promoviendo un flujo claro y desacoplado entre capas.

- **Respuestas paginadas y filtradas**  
  Todos los endpoints devuelven datos mediante DTOs estructurados, respetando la arquitectura de CQRS.

---

## 💻 Tecnologías y Arquitectura del Frontend

El frontend está desarrollado como SPA moderna con enfoque modular:

- **React + Vite**  
  Configuración rápida y ligera para desarrollo moderno con módulos ESM y recarga instantánea.

- **React Router DOM**  
  Gestión de rutas cliente para navegación entre páginas de listado, filtros y detalle de propiedades.

- **RTK Query**  
  Capa de acceso a datos basada en Redux Toolkit para gestión eficiente de llamadas HTTP, caché automatizada, invalidación y manejo de estados de carga.

- **Material UI (MUI)**  
  Librería de componentes UI responsivos, accesibles y listos para producción, que mejora la experiencia de usuario.

- **TypeScript**  
  Tipado estricto en todos los componentes y hooks, reduciendo errores y mejorando el desarrollo colaborativo.

- **Variables de entorno con `.env`**  
  Permite personalizar dinámicamente el endpoint de la API (`VITE_API_BASE_URL`) sin modificar el código fuente.

- **Responsive Design con MUI Theme**  
  Se configuró un theme personalizado de MUI que incluye breakpoints y estilos responsivos, garantizando que la aplicación se adapte correctamente a dispositivos móviles, tablets y escritorios.

---

## 📦 Funcionalidades implementadas

- Listado paginado de propiedades
- Filtros por nombre, dirección y rango de precios
- Detalle de propiedad con todas sus imágenes activas
- Seeder automático de propiedades de prueba
- Contenerización completa con Docker Compose

---

## 🌱 Seeder automático de propiedades

Este proyecto no requiere scripts externos ni archivos JSON para insertar datos de prueba.

> Al ejecutar la aplicación por primera vez, el backend detecta si la colección de propiedades en MongoDB está vacía. Si es así, se insertan automáticamente registros de prueba (11 propiedades con imágenes embebidas).

Esto permite que cualquier desarrollador pueda levantar el entorno completo sin configuraciones manuales ni pasos adicionales. Los datos de prueba incluyen:

- 6 apartamentos y 5 casas
- Cada propiedad contiene 2 imágenes (`IsMain = true/false`)
- Datos coherentes para probar listados, filtros y paginación desde el frontend

Las imágenes están almacenadas de forma pública en un repositorio GitHub:  
🔗 [https://github.com/ReybertAPS/million-assets/tree/main/images](https://github.com/ReybertAPS/million-assets/tree/main/images)

Para simular un ambiente real de producción, se utiliza la URL **raw** de cada imagen, permitiendo que el frontend las consuma directamente desde GitHub sin necesidad de montar un servidor de archivos o CDN:

```https://raw.githubusercontent.com/ReybertAPS/million-assets/main/images/{codigo}-0.jpg```

> Esta estrategia facilita pruebas visuales sin tener que gestionar almacenamiento ni autenticación de imágenes.

El seeding se realiza automáticamente desde el backend en `Million.Web.API`, mediante la clase estática `MongoDbSeeder`.

---

## 🧪 Unit Tests

Se implementaron pruebas unitarias con **xUnit**, **Moq** y **FluentAssertions**, enfocadas en validar el comportamiento de los siguientes *QueryHandlers* de la capa `Application`:

### ✅ GetAllPropertiesQueryHandler

- `Handle_ReturnsPagedProperties_WhenPropertiesExist`: verifica que el handler retorna una lista paginada correctamente cuando existen propiedades.
- `Handle_ReturnsEmpty_WhenNoPropertiesExist`: valida que se retorne una respuesta vacía cuando no hay propiedades registradas.

### ✅ GetFilteredPropertiesQueryHandler

- `Handle_ReturnsFilteredProperties_WhenFiltersMatch`: comprueba que los filtros por nombre, dirección y rango de precios funcionen correctamente.
- `Handle_ReturnsEmpty_WhenNoPropertiesMatch`: valida que la respuesta sea vacía si no hay coincidencias con los filtros aplicados.

### ✅ GetPropertyByIdQueryHandler

- `Handle_ReturnsProperty_WhenIdIsValid`: asegura que se retorna la propiedad esperada al consultar por un ID válido.
- `Handle_ThrowsKeyNotFoundException_WhenPropertyNotFound`: valida que se lanza una excepción si no se encuentra una propiedad con el ID solicitado.

Estas pruebas ayudan a garantizar la estabilidad, exactitud de los resultados y el correcto manejo de errores en la lógica de lectura (*read side*) del sistema.

---

## 📬 Colección de Postman para pruebas manuales

En la raíz del proyecto se incluye el archivo `Million.postman_collection.json`, el cual contiene una colección de Postman lista para probar los endpoints de la API REST del backend.

### Endpoints disponibles en la colección:

- **GET /api/properties**: Retorna todas las propiedades paginadas.
- **GET /api/properties/{id}**: Retorna el detalle de una propiedad por su ID.
- **GET /api/properties/filter**: Filtra propiedades por nombre, dirección y rango de precio.

### Instrucciones para usarla

1. Abrir Postman.
2. Importar el archivo `Million.postman_collection.json` desde la raíz del proyecto.
3. Asegurarse de que el backend esté corriendo localmente en `https://localhost:7297`.
4. Ejecutar los requests según se requiera.

> 💡 La colección ya incluye ejemplos de valores para facilitar las pruebas.

---

## 🚀 Ejecución del Proyecto con Docker

Este proyecto ha sido diseñado para ejecutarse de forma local utilizando **Docker Desktop**, conteniendo tres servicios principales: la base de datos MongoDB, la API REST desarrollada en .NET 8, y el frontend web desarrollado con React + Vite.

> Asegúrate de estar ubicado en la raíz del proyecto (`/MillionChallenge`) donde se encuentra el archivo `docker-compose.yml`.

---

### ✅ Requisitos previos

Para ejecutar el entorno completo, necesitas tener instalados los siguientes componentes:

#### 1. [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Descárgalo e instálalo desde el sitio oficial.
- Requiere habilitar la **virtualización** desde BIOS/UEFI (para WSL2).
- Al finalizar la instalación, asegúrate de que Docker esté corriendo (verifica el ícono de la ballena 🐳 en la bandeja del sistema).

#### 2. Subsistema de Windows para Linux (WSL2) *(solo para Windows)*
- Docker Desktop usa WSL2 como backend por defecto en Windows.
- Instala WSL2 con el siguiente comando en PowerShell (como administrador):

```powershell
wsl --install
```

- Reinicia tu equipo si se te solicita.
- Docker lo detectará automáticamente.

---

### 📂 Estructura de carpetas

```bash
/MillionChallenge
│
├── backend                               # API .NET 8 con MongoDB
├── frontend                              # WebApp React + Vite
├── .gitignore
├── docker-compose.yml
├── README.md
└── Million.postman_collection.json       # colección de postman para pruebas de la API
```

### 🧪 Pasos para ejecutar la aplicación

#### 1. Ubícate en la raíz del proyecto (donde está el docker-compose.yml):

```powershell
cd ruta/al/proyecto/MillionChallenge
```

#### 2. Ejecuta el siguiente comando para construir y levantar todos los servicios:

```powershell
docker compose up --build
```

#### Esto hará lo siguiente:

- Construirá la imagen del backend .NET (million-api).
- Construirá el frontend React (million-web).
- Levantará la base de datos MongoDB (million-db).
- Inyectará automáticamente los datos de prueba si la base de datos está vacía (mediante un seeder en el backend).
- Expondrá los servicios en los siguientes puertos:

| Servicio        | URL Local                                              | Puerto |
| --------------- | ------------------------------------------------------ | ------ |
| Web App (React) | [http://localhost:5173](http://localhost:5173)         | 5173   |
| API REST        | [http://localhost:8080/api](http://localhost:8080/api) | 8080   |
| MongoDB         | mongodb://localhost:27017                              | 27017  |

---
### 🐛 ¿Problemas comunes?
- `ERR_EMPTY_RESPONSE`: Ocurre si el backend no está escuchando en `0.0.0.0`. Este proyecto ya lo soluciona mediante la variable `ASPNETCORE_URLS`.
- `ERR_NAME_NOT_RESOLVED`: Asegúrate de que el archivo `.env` en el frontend no use `million-api` si estás accediendo desde el navegador. Usa localhost.
- **No carga nada en el frontend:** Verifica que hayas reconstruido correctamente con docker compose up --build y que el .env tenga correctamente definida la variable `VITE_API_BASE_URL`.

---
### 📦 Variables de entorno

El frontend toma el endpoint base de la API desde el archivo `.env` en la carpeta `/frontend`:

```powershell
VITE_API_BASE_URL=http://localhost:8080/api
```

> Si modificas este archivo, recuerda reconstruir la imagen del frontend para que Vite reinyecte la variable en tiempo de build.

---

### 📄 Notas finales
- La carga inicial de datos se realiza mediante un Seeder que verifica si existen propiedades en la colección. Si está vacía, se insertan automáticamente.
- El frontend ha sido diseñado con enfoque responsivo, utilizando Material UI + Vite, e incluye componentes dinámicos para filtros y navegación de propiedades.
- La comunicación entre frontend y backend está completamente desacoplada y se gestiona vía RTK Query usando la URL definida en el .env.

---

## ⚙️ Ejecución Alternativa del Proyecto (Sin Docker)

Si no cuentas con Docker instalado o prefieres ejecutar el proyecto manualmente, puedes seguir esta alternativa para levantar la aplicación:

### 🧩 Backend (.NET API)

1. Abre la solución en **Visual Studio 2022**.
2. Establece el proyecto `Million.Web.API` como proyecto de inicio.
3. Ejecuta el proyecto con el perfil **HTTPS**.
   - Por defecto, la API se expone en: `https://localhost:7297/api`

> ✅ **Nota importante**: La API incluye un **seeder automático**, por lo tanto, en la primera ejecución se insertarán los registros necesarios para que puedas probar la aplicación de inmediato.

> ⚠️ **Sin embargo**, antes de ejecutar la API, debes asegurarte de lo siguiente:
>
> - Tener **MongoDB** instalado y en ejecución en tu máquina local (o en un servidor accesible).
> - Crear la base de datos `MillionDb`.
> - Crear la colección `properties` dentro de dicha base de datos (el resto lo hará automáticamente el seeder).
> - No olvides editar tu `appsettings.json` para acceder correctamente a Mongo.

---

### 🌐 Frontend (React + Vite)

1. Abre la carpeta `frontend` del proyecto en **Visual Studio Code** (u otro editor de tu preferencia).
2. Crea un archivo `.env` en la raíz del proyecto (junto al `package.json`), y agrega la siguiente variable:

```env
VITE_API_BASE_URL=https://localhost:7297/api
```

3. Abre una terminal en esa carpeta y ejecuta el siguiente comando para levantar el frontend:

```
npm run dev
```

> Esto iniciará la aplicación React en `http://localhost:5173` (por defecto), permitiéndote probar toda la funcionalidad de la plataforma de forma local, sin necesidad de contenedores Docker.

---

## 📘 Decisión de modelado: Imágenes embebidas en `Property`

### 🎯 Contexto del modelo
En el diseño de la solución, cada entidad `Property` puede tener múltiples imágenes (`PropertyImage`). Estas imágenes:

- Son exclusivas de cada propiedad (relación 1:N).
- No se reutilizan entre propiedades.
- Solo se consultan en el contexto de su propiedad.
- No se modifican frecuentemente de forma independiente.

---

### 🧱 Alternativas evaluadas

#### Opción 1: `$lookup` con colección separada `PropertyImages`

- Requiere agregaciones complejas (`$lookup`, `$unwind`, `$match`, `$group`).
- Introduce mayor acoplamiento y lógica en los repositorios.
- Añade latencia por la necesidad de múltiples etapas de consulta.

#### Opción 2: **Modelo embebido** (`List<PropertyImage>` dentro de `Property`)

- Consulta simplificada con `.Find()` directo.
- Acceso más eficiente: sin agregaciones ni joins.
- Relación semántica directa: las imágenes son parte integral del documento `Property`.
- Se mantiene dentro del límite de tamaño de documento de MongoDB (16 MB), ya que se espera un número acotado de imágenes por propiedad.

---

### ✅ Decisión tomada

> Se implementó un **modelo embebido** para las imágenes de una propiedad, manteniendo una colección única `Property` con una lista `Images` de tipo `PropertyImage`.

### ✍️ Justificación técnica

- Basado en el principio de MongoDB: **“modela tus datos según cómo los vas a consultar”**.
- Se favorece la simplicidad, eficiencia y cohesión de los datos.
- Cumple con los requerimientos funcionales del sistema: mostrar una imagen principal en listados y todas las imágenes activas en el detalle.
- Reduce la complejidad del código y el riesgo de errores en las consultas agregadas.

---

### 📌 Consideraciones futuras

- Si en el futuro se espera un crecimiento significativo en el número de imágenes o lógica independiente sobre ellas, se podrá migrar fácilmente a una colección referenciada con `$lookup`.
