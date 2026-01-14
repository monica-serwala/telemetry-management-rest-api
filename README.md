# Telemetry Management REST API

## Overview
This project is a RESTful Web API built using **ASP.NET Core (.NET 8)** that records and manages telemetry data for automation processes. The API tracks time saved by automations, associates this with cost savings, and groups results by **project** and **client**.

The system exposes CRUD operations for telemetry data and provides aggregated insights into cumulative time and cost savings over a specified period.

---

## Project Background
This project is a reimplementation of a university assignment originally completed during my undergraduate studies.
It has been rebuilt independently for learning, skill improvement, and portfolio purposes.

This repository is not associated with any active academic submission.

The original assignment was required to be submitted using a **private repository** and could not be shared publicly. This repository represents a **rebuilt version** of the project, created independently for **portfolio and learning purposes**.

While the core problem statement is inspired by the academic brief, the implementation, documentation, architectural decisions, and cloud deployment demonstrated in this repository were completed separately to showcase my understanding of:

- RESTful API design
- Entity Framework Core
- Business logic implementation
- Cloud deployment using Microsoft Azure

---

## Features
- CRUD operations for telemetry data
- Retrieval of telemetry records by ID
- Aggregated time and cost savings per **project**
- Aggregated time and cost savings per **client**
- Filtering by date range
- RESTful endpoint design
- Swagger / OpenAPI documentation

---

## Technology Stack
- **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core**
- **Azure SQL Database**
- **Microsoft Azure App Service**
- **Swagger / OpenAPI**
- **GitHub (source control)**

---

## API Structure (Planned)
> This section will be updated as endpoints are implemented.

### Telemetry Endpoints
- `GET /api/telemetry`
- `GET /api/telemetry/{id}`
- `POST /api/telemetry`
- `PATCH /api/telemetry/{id}`
- `DELETE /api/telemetry/{id}`

### Savings Calculation
- `GET /api/telemetry/GetSavingsByProject`
- `GET /api/telemetry/GetSavingsByClient`

---

## Cloud Deployment (Azure)
This API is deployed using **Microsoft Azure App Service (Free Tier)** and connects to an **Azure SQL Database**.

Due to cost considerations, cloud resources may not be permanently active.  
Screenshots of the Azure configuration and deployment are provided in the `/docs` folder to demonstrate successful hosting and integration.

---

## Documentation
- Swagger UI is used for API documentation and endpoint testing
- Deployment and configuration evidence is included in the `/docs` folder

---

## Future Enhancements
- Authentication and authorization
- Improved validation and error handling
- Logging and monitoring
- Pagination and filtering enhancements

---

## Author
**Monica Mmakokong Serwala**  
Information Technology Graduate  
Aspiring Software & Cloud Engineer

---

## License
This project is shared for **educational and portfolio purposes**.
