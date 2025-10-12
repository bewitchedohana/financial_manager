# Financial Manager

Financial Manager is a modern web application for personal financial management, built with cutting-edge technologies and following development best practices.

## Project Structure

The project is organized into two main parts:

### Backend (.NET 9.0)

- Clean Architecture
- REST API with ASP.NET Core
- Structured in layers:
  - API: Controllers and application configuration
  - Application: Business logic and DTOs
  - Domain: Entities and domain rules
  - Infrastructure: External services implementation
  - Persistence: Data access and migrations

### Frontend (Next.js 15)

- React Framework with Next.js
- TypeScript for static typing
- TailwindCSS for styling
- Jest for unit testing
- FontAwesome integration
- Forms with React Hook Form and Zod

## Requirements

### Backend
- .NET SDK 9.0 or higher
- A database instance (configurable via appsettings.json)

### Frontend
- Node.js 20 or higher
- NPM or Yarn

## How to Run

### Backend

1. Navigate to the backend folder:
```bash
cd backend
```

2. Restore packages and run the application:
```bash
dotnet restore
dotnet run --project src/API
```

### Frontend

1. Navigate to the frontend folder:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

3. Run the development server:
```bash
npm run dev
```

## Project Management

- The project is managed using Scrum methodology
- Tasks can be tracked on [Taiga](https://tree.taiga.io/project/bewitchedohana-financial-manager/timeline)
- Interface designs can be found on [Figma](https://www.figma.com/design/ww6bOnWIQnTZEvhEahyDOI/Denhero)

## Tests

### Backend
```bash
dotnet test
```

### Frontend
```bash
npm test
```

## Main Technologies

### Backend
- ASP.NET Core 9.0
- Entity Framework Core
- Clean Architecture

### Frontend
- Next.js 15
- React 19
- TypeScript
- TailwindCSS
- Jest
- React Hook Form
- Zod for validation

## Contributing

To contribute to the project:

1. Fork the repository
2. Create a branch for your feature (`git checkout -b feature/NewFeature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/NewFeature`)
5. Create a Pull Request