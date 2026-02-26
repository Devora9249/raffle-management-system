# Raffle Management System - Client

The frontend application for the Raffle Management System, built with Angular 20 and modern UI component libraries.

## Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Available Scripts](#available-scripts)
- [Environment Configuration](#environment-configuration)
- [UI Screenshots](#ui-screenshots)
- [Development Guidelines](#development-guidelines)

## Overview

This is the client-side application of the Raffle Management System. It provides an intuitive, responsive user interface for managing raffle events, browsing gifts, making purchases, and administering the system based on user roles.

The application is built with Angular and uses a component-based architecture with lazy loading for optimal performance.

## Technologies Used

### Core Framework

- **Angular**: 20.3.0
- **TypeScript**: Latest
- **RxJS**: 7.8.0 (Reactive programming)
- **Zone.js**: 0.15.0

### UI Component Libraries

- **PrimeNG**: 20.4.0 - Primary UI component library
- **ng-zorro-antd**: 20.4.4 - Additional UI components
- **PrimeIcons**: 7.0.0 - Icon library
- **@primeng/themes**: 20.4.0 - Theme system
- **@primeuix/themes**: 2.0.2 - Extended themes

### Additional Libraries

- **canvas-confetti**: 1.9.4 - Celebration animations
- **file-saver**: 2.0.5 - File download functionality
- **jwt-decode**: 4.0.0 - JWT token handling
- **xlsx**: 0.18.5 - Excel export functionality

### Styling

- **SCSS**: For component and global styling
- Prettier configured for consistent code formatting

### Build Tools

- **Angular CLI**: 20.3.8
- **@angular/build**: 20.3.8
- **TypeScript Compiler**: Latest

## Prerequisites

Before you begin, ensure you have the following installed:

- **Node.js**: 22.16.0 or higher
- **npm**: 10.9.2 or higher
- **Angular CLI**: 20.3.13 or higher

Verify your installation:

```bash
node --version
npm --version
ng version
```

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd finalProject/client
```

### 2. Install Dependencies

```bash
npm install
```

This will install all required packages as defined in `package.json`.

### 3. Configure Environment (if needed)

If you need to connect to a different backend API, update the API base URL in your environment configuration files (typically in `src/environments/`).

### 4. Start the Development Server

```bash
npm start
# or
ng serve
```

The application will be available at `http://localhost:4200/`.

The development server supports hot module replacement, so any changes you make to the source files will automatically reload the application in the browser.

### 5. Access the Application

Open your browser and navigate to:

```
http://localhost:4200
```

**Note**: Ensure the backend server is running at the configured API endpoint before using the application.

## Project Structure

```
client/
├── public/                  # Static assets served directly
├── src/
│   ├── app/                # Main application directory
│   │   ├── core/          # Core functionality (guards, interceptors, services)
│   │   ├── features/      # Feature modules (lazy-loaded)
│   │   ├── shared/        # Shared components, directives, pipes
│   │   ├── styles/        # Application-wide styles
│   │   ├── app.config.ts  # Application configuration
│   │   ├── app.routes.ts  # Application routing
│   │   └── app.ts         # Root component
│   ├── index.html         # Main HTML file
│   ├── main.ts           # Application entry point
│   └── styles.scss       # Global styles
├── angular.json          # Angular CLI configuration
├── package.json          # Project dependencies and scripts
├── tsconfig.json         # TypeScript configuration
└── README.md            # This file
```

### Key Directories

- **core/**: Contains singleton services, authentication guards, HTTP interceptors, and core utilities that are used throughout the application
- **features/**: Contains feature-specific modules that are lazy-loaded for better performance
- **shared/**: Contains reusable components, directives, and pipes that are shared across features
- **styles/**: Contains global SCSS variables, mixins, and theme configurations

## Available Scripts

### Development

```bash
npm start
# or
ng serve
```

Starts the development server on `http://localhost:4200`.

### Build

```bash
npm run build
# or
ng build
```

Builds the application for production. The build artifacts will be stored in the `dist/` directory.

### Build (Watch Mode)

```bash
npm run watch
# or
ng build --watch --configuration development
```

Builds the application in watch mode, automatically rebuilding when files change.

### Testing

```bash
npm test
# or
ng test
```

Runs unit tests using Karma test runner.

### Code Generation

Generate new components, services, and other Angular artifacts:

```bash
# Generate a new component
ng generate component component-name

# Generate a new service
ng generate service service-name

# Generate a new module
ng generate module module-name

# See all available schematics
ng generate --help
```

## Environment Configuration

The application can be configured for different environments (development, production, etc.).

### API Configuration

To connect to a different backend API, update the API base URL in your environment files or configuration service.

Typical configuration includes:

- **API Base URL**: The backend API endpoint
- **JWT Token Key**: Local storage key for authentication token
- **Timeout Settings**: HTTP request timeout values

## UI Screenshots

This section showcases the user interface of the Raffle Management System.

### Home Page

> _Screenshot placeholder: Add a screenshot of the home page showing the main landing view_

### Gift Catalog

> _Screenshot placeholder: Add a screenshot of the gift browsing interface with category filters_

### Shopping Cart

> _Screenshot placeholder: Add a screenshot of the shopping cart with gift items_

### User Dashboard

> _Screenshot placeholder: Add a screenshot of the user dashboard showing purchases and wins_

### Admin Panel

> _Screenshot placeholder: Add a screenshot of the admin panel for managing gifts and categories_

### Donor Management

> _Screenshot placeholder: Add a screenshot of the donor interface for contributing gifts_

### Authentication

> _Screenshot placeholder: Add screenshots of login and registration pages_

### Responsive Design

> _Screenshot placeholder: Add screenshots demonstrating mobile responsiveness_

---

**Note**: To add screenshots, replace the placeholders above with actual image files stored in a `screenshots/` or `docs/` folder, then reference them using markdown image syntax:

```markdown
![Alt text](./path/to/screenshot.png)
```

## Development Guidelines

### Code Style

This project uses Prettier for code formatting. The configuration is defined in `package.json`:

- Print width: 100 characters
- Single quotes for strings
- Angular HTML parser for template files

### Component Structure

- Use standalone components where appropriate
- Follow the single responsibility principle
- Keep components small and focused
- Use smart/dumb component pattern (container/presentation)

### State Management

- Use RxJS for reactive state management
- Use services for shared state
- Implement proper subscription management (use `async` pipe or unsubscribe)

### Styling

- Use SCSS for styling
- Follow BEM naming convention for CSS classes
- Leverage PrimeNG theme system for consistent UI
- Keep component styles scoped

### Best Practices

1. **TypeScript**: Enable strict mode and fix all type errors
2. **Performance**: Use `OnPush` change detection strategy where possible
3. **Accessibility**: Ensure proper ARIA labels and keyboard navigation
4. **Lazy Loading**: Load feature modules on demand
5. **Error Handling**: Implement proper error handling and user feedback
6. **Security**: Never store sensitive data in client-side storage without encryption

### Testing

- Write unit tests for components and services
- Aim for meaningful test coverage
- Use mocking for external dependencies
- Test both success and error scenarios

## Troubleshooting

### Port Already in Use

If port 4200 is already in use, specify a different port:

```bash
ng serve --port 4201
```

### Module Not Found Errors

If you encounter module not found errors, try:

```bash
rm -rf node_modules package-lock.json
npm install
```

### Build Errors

Clear the Angular cache and rebuild:

```bash
ng cache clean
npm run build
```

## Additional Resources

- [Angular Documentation](https://angular.dev/)
- [Angular CLI Reference](https://angular.dev/tools/cli)
- [PrimeNG Documentation](https://primeng.org/)
- [ng-zorro-antd Documentation](https://ng.ant.design/)
- [RxJS Documentation](https://rxjs.dev/)

## Support

For issues and questions:

1. Check the [main README](../README.md) for general project information
2. Review the [server README](../server/README.md) for backend-related issues
3. Consult the Angular and component library documentation
4. Check existing issues in the project repository
