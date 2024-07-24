# Sakanat API - Student Housing Management System

## Overview
Sakanat API serves as the backend service for the Sakanat platform, a centralized system designed to streamline the search for student housing. Developed as a key component of the Sakanat project, this API provides essential services for managing property listings, user accounts, and other critical functionalities.

## Features
### Core Functionalities:
#### User Management
- **Registration**: Users can create accounts to access the platform.
- **Authentication**: Secure login and logout functionality.
- **Profile Management**: Users can update and manage their profiles.

#### Property/Apartment Listings
- **CRUD Operations**: Create, Read, Update, and Delete property listings.

#### Search and Filters
- **Advanced Search**: Search for properties based on various criteria.
- **Filters**: Apply multiple filters to narrow down search results.

#### Favorites
- **Manage Favorites**: Users can save and manage their favorite listings.

#### Communication
- **WhatsApp Integration**:  communication via WhatsApp with owners.

#### Tenants and Contracts Management
- **Tenant Management**: Manage tenant information and details.
- **Contracts Management**: Handle rental contracts and agreements.

#### Discounts Management
- **Discounts Management**: Owners can manage and apply discounts to listings.

#### Follow/Unfollow Owners
- **Follow/Unfollow Owners**: Users can follow or unfollow property owners.

#### Notifications System
- **Notifications System**: Receive notifications for important updates and activities.

#### Detailed Admin Dashboard
- **Admin Dashboard**: Comprehensive dashboard for site management, providing insights and control over platform operations.


## Technologies Used
### Back-End:
- **C# with ASP.NET Core**: Framework used to build the API, offering robust and scalable backend services.
- **Entity Framework Core**: ORM for efficient database management and operations.
- **Microsoft SQL Server**: The chosen relational database system for storing all application data.

## Installation and Setup
### Prerequisites:
- Visual Studio
- .NET Core SDK
- Microsoft SQL Server

### Setup Instructions:
1. **Clone the Repository**: Clone the repository to your local machine using `git clone <repository-url>`.
2. **Database Configuration**: Update the database connection string in the `appsettings.json` file to match your local setup.
3. **Apply Migrations**: Run the following command to apply the database migrations:
   ```bash
   dotnet ef database update
### Usage
To interact with the API, use tools like Postman for testing or integrate it with the front-end application. Ensure to include the JWT token in the request headers for authenticated endpoints.
