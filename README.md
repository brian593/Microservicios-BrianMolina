# Prueba Técnica - Brian Molina

## Descripción del Proyecto

Este repositorio contiene la implementación de una solución para la prueba técnica solicitada, diseñada utilizando una **arquitectura hexagonal**. Esta arquitectura promueve la separación de responsabilidades y permite que la lógica de negocio se mantenga independiente de las tecnologías de presentación y persistencia, facilitando la evolución y el mantenimiento del sistema.

## Estructura de la Solución

La solución está compuesta por dos proyectos principales: `CustomerPersonService` y `AccountTransactionService`, cada uno organizado de manera similar. A continuación se detalla la estructura de cada proyecto:

### CustomerPersonService

- **CustomerPersonService (Project)**
  - **Domain (Lógica de negocio)**
    - **Entities** (Entidades de dominio)
  - **Application (Casos de uso)**
    - **Interfaces** (Puertos, interfaces para interactuar con el core)
  - **Infrastructure (Adaptadores)**
    - **Persistence** (Implementaciones de persistencia)
    - **Database** (Configuración de la base de datos y contextos)
    - **WebApi** (Endpoints)

  
### AccountTransactionService

- **AccountTransactionService (Project)**
 - **Domain (Lógica de negocio)**
    - **Entities** (Entidades de dominio)
  - **Application (Casos de uso)**
    - **Interfaces** (Puertos, interfaces para interactuar con el core)
    - **DTOs** (Clases DTOs para reacciones)
  - **Infrastructure (Adaptadores)**
    - **Persistence** (Implementaciones de persistencia)
    - **Database** (Configuración de la base de datos y contextos)
    - **WebApi** (Endpoints)

## Componentes Clave

- **Domain**: Contiene la lógica de negocio, las entidades de dominio y el manejo de errores. Este es el núcleo de la aplicación, donde reside la funcionalidad principal.
  
- **Application**: Define los casos de uso y las interfaces que actúan como puertos para interactuar con el núcleo.

- **Infrastructure**: Incluye los adaptadores necesarios para la persistencia de datos, la configuración de la base de datos y el mapeo de objetos.

- **WebApi**: Exponen los endpoints de la API, permitiendo la interacción con los servicios desde el exterior.

## Cómo Ejecutar la Solución

Para ejecutar esta solución, asegúrate de tener Docker y Docker Compose instalados. Luego, sigue estos pasos:

1. Clona este repositorio.
2. Navega al directorio del proyecto.
3. Ejecuta `docker-compose up -d` para levantar los contenedores.

## Conclusión

La implementación de esta prueba técnica se realizó con el objetivo de demostrar un entendimiento sólido de la arquitectura hexagonal y las mejores prácticas en el desarrollo de software. Espero que esta solución cumpla con las expectativas y requisitos establecidos.
