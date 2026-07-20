# Syncfusion React Pivot Table with UrlAdaptor: ASP.NET Core Backend

**Binding and Performing CRUD Operations in the Syncfusion React Pivot Table using a REST API and UrlAdaptor**

This sample shows how to connect the [Syncfusion React Pivot Table](https://ej2.syncfusion.com/react/documentation/pivotview/getting-started) to a remote REST API using the [UrlAdaptor](https://ej2.syncfusion.com/react/documentation/data/adaptors/url-adaptor) and perform Create, Read, Update, and Delete operations through an ASP.NET Core backend.

### 🔄 How It Works

The application uses **Syncfusion DataManager** with the **UrlAdaptor** to handle real-time CRUD operations (Create, Read, Update, Delete) on order data through an interactive pivot table interface:

- **React Client (Frontend)**: Renders the Syncfusion Pivot Table and allows users to visualize, analyze, and modify order data
- **DataManager + UrlAdaptor**: Translates Pivot Table actions into HTTP POST requests to user-defined REST endpoints and parses the JSON response
- **ASP.NET Core Backend**: Provides REST API endpoints (`/api/Orders`) that handle all CRUD operations via the **OrdersController**
- **In-Memory Data Store**: The sample uses a static in-memory list (`OrdersDetails.order`) seeded with sample records for demonstration

The complete workflow: User interacts with the Pivot Table → DataManager sends an HTTP POST → OrdersController processes the request → in-memory collection is updated → Response updates the UI instantly.

---

## 📋 Table of Contents

- [Quick Overview](#-quick-overview)
- [✨ Key Features & Capabilities](#-key-features--capabilities)
- [🛠️ Technology Stack](#-technology-stack)
- [🏗️ System Architecture](#-system-architecture)
- [⚙️ Prerequisites](#-prerequisites)
- [📦 Installation & Setup](#-installation--setup)
- [▶️ Run the Application](#-run-the-application)
- [🔗 Backend API Endpoints](#-backend-api-endpoints)
- [📁 Project Structure](#-project-structure)
- [🛠️ Troubleshooting](#-troubleshooting)
- [🚀 Next Steps](#-next-steps)
- [🤝 Contributing](#-contributing)
- [📜 License & Support](#-license--support)

---

## 🎯 Quick Overview

This application provides a clean starting point for integrating the Syncfusion React Pivot Table with a backend service. Instead of static or hard-coded data, users interact with a live pivot table to:

- **Visualize Data**: Organize order data by Order ID, Customer ID, Ship City, Ship Country, and more
- **Perform CRUD Operations**: Add new records, modify existing data, and delete entries directly through the drill-through editor
- **Drill-Through Editing**: Click a pivot cell to view detailed records and edit them in-line
- **Customize Views**: Drag-and-drop fields to reorganize the pivot table layout
- **Real-time Sync**: All changes are immediately reflected in the application using DataManager

### Why Choose This Stack?

- **Separation of concerns**: React handles presentation; ASP.NET Core handles data access and business logic
- **Standardized JSON contract**: The `{ result, count }` response format ensures consistent communication
- **Built-in CRUD support**: Insert, update, and remove operations are wired with minimal code on both sides
- **Easy extensibility**: The in-memory store can be swapped for a real database (SQL Server, PostgreSQL, EF Core, etc.) without changing the client
- **Production-ready patterns**: Property-casing preservation, CORS configuration, and proper API design included

---

## ✨ Key Features & Capabilities

### Core Capabilities

- **Interactive Pivot Table Rendering**: Full Syncfusion EJ2 React Pivot Table with drag-and-drop field configuration
- **Remote Data Binding**: Connects to a REST API through the UrlAdaptor with no custom request handling required
- **Complete CRUD Operations**: Create, read, update, and delete order records directly from the drill-through editing UI
- **UrlAdaptor Integration**: Automatic HTTP POST handling and JSON response parsing for all data operations
- **Drill-Through Editor**: Click a pivot cell to open a detailed grid where records can be added, edited, or deleted
- **Configurable Edit Modes**: Supports `Normal`, `Dialog`, `Batch`, and `CommandColumn` editing modes
- **Primary Key Configuration**: Uses the `beginDrillThrough` event to set `OrderID` as the primary key
- **Date Picker Support**: Date fields (`OrderDate`, `ShippedDate`) are auto-configured with a `datetimepickeredit` editor
- **Property Casing Preservation**: `DefaultContractResolver` keeps the original property casing so field bindings work reliably
- **CORS-Enabled Backend**: Pre-configured to allow the React dev server to call the API in development
- **Single-Endpoint CRUD Option**: Includes a `/CrudUpdate` endpoint as an alternative to separate `Insert/Update/Remove` endpoints
- **Real-time Updates**: The Pivot Table automatically refreshes after every successful CRUD operation
- **Lightweight Sample**: No database required — runs out of the box with seeded sample data

---

## 🛠️ Technology Stack

### Frontend
| Technology | Version | Purpose |
|-----------|---------|---------|
| React | ^19.2.7 | UI framework |
| React DOM | ^19.2.7 | React rendering engine |
| Vite | ^8.1.1 | Build tooling and dev server |
| TypeScript | ~6.0.2 | Static typing for the client |
| @syncfusion/ej2-react-pivotview | ^34.1.31 | Pivot Table component |
| @syncfusion/ej2-tailwind3-theme | ^34.1.30 | Tailwind 3 theme for Syncfusion components |

### Backend
| Technology | Version | Purpose |
|-----------|---------|---------|
| .NET | 8.0+ (target `net10.0` in sample) | Framework for Web APIs |
| ASP.NET Core | 8.0+ | Web application framework |
| Microsoft.AspNetCore.Mvc.NewtonsoftJson | 8.0+ | JSON serialization with original casing |
| Swashbuckle / OpenAPI | included | API documentation |

### Development Tools
| Tool | Purpose |
|-----|---------|
| Node.js / npm | Frontend runtime and package management |
| .NET CLI | Backend project management |
| Visual Studio / VS Code | Code editing and debugging |
| Modern browser (Chrome / Edge) | Running the React client |

---

## 🏗️ System Architecture

### Architecture Overview

```
┌────────────────────────────────────────────────┐
│   React Frontend (Client)                      │
│  ┌────────────────────────────────────────┐   │
│  │  PivotViewComponent                    │   │
│  │   ├── DataManager                      │   │
│  │   └── UrlAdaptor                       │   │
│  └────────────────────────────────────────┘   │
└────────────────────────────────────────────────┘
              ↓
         HTTP POST (REST)
              ↓
┌────────────────────────────────────────────────┐
│   ASP.NET Core Backend (UrlAdaptor)            │
│  ┌────────────────────────────────────────┐   │
│  │  OrdersController                      │   │
│  │   ├── GET   /api/Orders                │   │
│  │   ├── POST  /api/Orders                │   │
│  │   ├── POST  /api/Orders/Insert         │   │
│  │   ├── POST  /api/Orders/Update         │   │
│  │   ├── POST  /api/Orders/Remove         │   │
│  │   └── POST  /api/Orders/CrudUpdate     │   │
│  └────────────────────────────────────────┘   │
└────────────────────────────────────────────────┘
              ↓
         In-Memory Store
              ↓
┌────────────────────────────────────────────────┐
│   OrdersDetails.order (List<OrdersDetails>)    │
│   Seeded with sample records on first request  │
└────────────────────────────────────────────────┘
```

### Data Flow with DataManager + UrlAdaptor

1. **User Interaction**: The user manipulates the Pivot Table (adds, edits, deletes, or reorganizes fields)
2. **DataManager Request**: DataManager detects the change and sends an HTTP POST to the appropriate endpoint (`url`, `insertUrl`, `updateUrl`, or `removeUrl`)
3. **Backend Processing**: `OrdersController` receives the request and dispatches to the appropriate CRUD method
4. **Data Operation**: The in-memory `OrdersDetails.order` list is updated (insert, update, or remove)
5. **Response**: For reads, the API returns `{ result, count }`. For mutations, the API returns `200 OK` and the Pivot Table refreshes automatically
6. **UI Synchronization**: The Pivot Table re-fetches data and the user sees the changes instantly

### Key Integration Points

| Component | Role | Purpose |
|-----------|------|---------|
| **DataManager** | Frontend Bridge | Manages HTTP communication, caching, and data synchronization |
| **UrlAdaptor** | Protocol Adapter | Translates DataManager calls into HTTP POSTs and parses JSON responses |
| **OrdersController** | Backend API | Handles HTTP requests and routes them to the right CRUD method |
| **OrdersDetails** | Data Model | Defines the order record structure and seeds sample data |
| **CRUDModel\<T\>** | Request Envelope | Standard payload shape used by all CRUD operations |
| **beginDrillThrough** | UI Hook | Configures the primary key and editor type in the drill-through grid |

---

## ⚙️ Prerequisites

Before running the sample, ensure the following are installed on your machine:

| Tool | Version | Purpose |
|------|---------|---------|
| Node.js | 18.x or later | Frontend runtime |
| npm | 9.x or later | Frontend package manager |
| .NET SDK | 8.0 or later | Build and run ASP.NET Core |
| Modern browser | Chrome, Edge, or Firefox | Run the React client |

---

## 📦 Installation & Setup

### 1. Clone or download the repository

```bash
git clone <repository-url>
cd url-adaptor-with-pivot-table
```

### 2. Install frontend dependencies

```bash
cd Client
npm install
```

### 3. Restore backend dependencies

```bash
cd ../UrlAdaptor
dotnet restore
```

No additional configuration is required — the sample uses an in-memory data store and the CORS policy is pre-configured for development.

---

## ▶️ Run the Application

### 1. Start the backend

Open a terminal in the project root and run:

```bash
cd UrlAdaptor
dotnet run
```

The backend will start on the URL configured in `UrlAdaptor/Properties/launchSettings.json`. By default, this sample uses:

- `http://localhost:5193` (HTTP profile)
- `https://localhost:7077` and `http://localhost:5193` (HTTPS profile)

Note the actual port from the console output and update the React client if it differs from `5193`.

### 2. Start the frontend

Open a second terminal and run:

```bash
cd Client
npm run dev
```

The React application (Vite dev server) will start on:

- `http://localhost:5173`

### 3. Verify the sample

Once both applications are running:

1. Open `http://localhost:5173` in your browser.
2. The Pivot Table loads data from the backend through the UrlAdaptor.
3. Use the drill-through editor to **add**, **edit**, or **delete** records.
4. The Pivot Table refreshes automatically after each operation.

### 4. Build for production

```bash
cd Client
npm run build
```

This produces an optimized production build in `Client/dist/`.

---

## 🔗 Backend API Endpoints

The backend exposes the following endpoints in `OrdersController`:

| Method | Route | Purpose |
|--------|-------|---------|
| GET    | `/api/Orders`            | Returns all order records (useful for browser verification) |
| POST   | `/api/Orders`            | Returns data in the `{ result, count }` format expected by UrlAdaptor |
| POST   | `/api/Orders/Insert`     | Inserts a new order record |
| POST   | `/api/Orders/Update`     | Updates an existing order record (matched by `OrderID`) |
| POST   | `/api/Orders/Remove`     | Removes an order record (matched by `OrderID`) |
| POST   | `/api/Orders/CrudUpdate` | Single endpoint that dispatches to insert/update/remove based on the `action` field |

If you change the backend port, update the URLs in `Client/src/App.tsx` so the frontend points to the correct API address:

```typescript
const data: DataManager = new DataManager({
    url: 'http://localhost:<port>/api/Orders',
    insertUrl: 'http://localhost:<port>/api/Orders/Insert',
    updateUrl: 'http://localhost:<port>/api/Orders/Update',
    removeUrl: 'http://localhost:<port>/api/Orders/Remove',
    adaptor: new UrlAdaptor()                // Specify UrlAdaptor for custom REST API.
});
```

---

## 📁 Project Structure

### Directory Tree

```text
url-adaptor-with-pivot-table/
├── README.md                              # This file
├── url-adaptor.md                         # Detailed UrlAdaptor reference
│
├── Client/                                # React Frontend (Vite + TypeScript)
│   ├── index.html                         # HTML entry point
│   ├── package.json                       # npm dependencies & scripts
│   ├── tsconfig.json                      # TypeScript configuration (root)
│   ├── tsconfig.app.json                  # TypeScript configuration (app)
│   ├── tsconfig.node.json                  # TypeScript configuration (node/vite)
│   ├── vite.config.ts                     # Vite configuration
│   ├── public/
│   │   ├── favicon.svg
│   │   └── icons.svg
│   └── src/
│       ├── App.tsx                        # Main component (PivotView + DataManager)
│       ├── App.css                        # Component styles
│       ├── main.tsx                       # React root (entry point)
│       ├── index.css                      # Global styles
│       └── assets/
│
└── UrlAdaptor/                            # ASP.NET Core Backend
    ├── Program.cs                         # App startup, JSON & CORS configuration
    ├── UrlAdaptor.csproj                  # Project file with dependencies
    ├── UrlAdaptor.http                    # HTTP test file
    ├── appsettings.json                   # Default configuration
    ├── appsettings.Development.json       # Development configuration
    │
    ├── Controllers/
    │   └── OrdersController.cs            # REST endpoints for CRUD operations
    │
    ├── Models/
    │   └── OrdersDetails.cs               # Order data model + sample data
    │
    ├── Properties/
    │   └── launchSettings.json            # Launch profiles & ports
```

### Key Files & Their Purposes

#### Frontend Key Files

| File | Purpose |
|------|---------|
| `Client/src/App.tsx` | Main React component — PivotView configuration, DataManager setup, edit settings, and `beginDrillThrough` event |
| `Client/src/main.tsx` | React application entry point and DOM mounting |
| `Client/src/App.css` | Component-level styles |
| `Client/vite.config.ts` | Vite build/dev server configuration |
| `Client/tsconfig.json` | TypeScript compiler configuration |
| `Client/package.json` | Frontend dependencies and npm scripts |

#### Backend Key Files

| File | Purpose |
|------|---------|
| `UrlAdaptor/Program.cs` | Application startup, JSON serialization (`DefaultContractResolver`), and CORS configuration |
| `UrlAdaptor/Controllers/OrdersController.cs` | REST API endpoints for CRUD operations and the `CRUDModel<T>` request envelope |
| `UrlAdaptor/Models/OrdersDetails.cs` | Order data model and the `GetAllRecords()` method that seeds sample data |
| `UrlAdaptor/appsettings.json` | Logging and allowed host configuration |
| `UrlAdaptor/Properties/launchSettings.json` | Launch profiles and port assignments |
| `UrlAdaptor/UrlAdaptor.csproj` | .NET project file with NuGet dependencies |

---

## 🛠️ Troubleshooting

### Pivot Table does not load data

- ✅ Verify the backend is running: `dotnet run` in the `UrlAdaptor` folder
- ✅ Confirm the URL in `Client/src/App.tsx` matches the backend port shown in the console
- ✅ Check the browser **Developer Tools → Network** tab for failed requests
- ✅ Verify the response uses the `{ result, count }` shape and uses the original property casing (`OrderID`, not `orderId`)

### CORS error

```
Access to XMLHttpRequest at 'http://localhost:5193/api/Orders' from origin 
'http://localhost:5173' has been blocked by CORS policy.
```

- ✅ Ensure CORS is enabled in `UrlAdaptor/Program.cs`
- ✅ Confirm `app.UseCors()` is called **before** `app.MapControllers()`
- ✅ Restart the backend after changing `Program.cs`

### Port already in use

- ✅ Change the port in `UrlAdaptor/Properties/launchSettings.json`
- ✅ Update the matching URL in `Client/src/App.tsx`
- ✅ On Windows, find the process with `netstat -ano | findstr :5193` and kill it with `taskkill /PID <PID> /F`

### CRUD operations not saving

- ✅ Confirm the primary key (`OrderID`) is correctly set in the `beginDrillThrough` event
- ✅ Check that the `insertUrl`, `updateUrl`, and `removeUrl` values in `App.tsx` match the backend routes exactly
- ✅ Verify the request body contains the expected `CRUDModel<T>` shape (`action`, `keyColumn`, `key`, `value`, `@params`)

### Empty Pivot Table layout

- ✅ Ensure the response property names match the `dataSourceSettings` field names (`OrderID`, `CustomerID`, `Freight`, etc.)
- ✅ Confirm `DefaultContractResolver` is added in `Program.cs` — without it, ASP.NET Core returns camelCase property names by default
- ✅ Check the `Format` property of each value field — a malformed format string can cause rendering issues

### SSL/TLS certificate error

- ✅ On Windows/macOS, run `dotnet dev-certs https --clean` followed by `dotnet dev-certs https --trust`
- ✅ On Linux, manually trust the certificate or use the HTTP profile (`http://localhost:5193`)

---

## 🚀 Next Steps

You can extend this sample by:

- **Connecting to a real database**: Replace the in-memory `OrdersDetails.order` list with Entity Framework Core, Dapper, or any other data access technology (SQL Server, PostgreSQL, SQLite, etc.)
- **Adding more Pivot Table fields**: Configure additional fields such as `ShipCountry`, `Verified`, or `OrderDate` as rows, columns, filters, or values
- **Customizing the editing experience**: Try `Dialog` or `Batch` edit modes for different UX patterns
- **Using the single `CrudUpdate` endpoint**: Replace separate `insertUrl/updateUrl/removeUrl` with a single `crudUrl` and route by `action`
- **Adding filtering, sorting, and grouping**: Extend the API to support server-side filtering and pass `where` conditions from the client
- **Implementing authentication**: Secure the CRUD endpoints with JWT or cookie-based auth
- **Exporting pivot data**: Add PDF, Excel, or CSV export on top of the existing API

---

## 🤝 Contributing

### Contribution Guidelines

We welcome contributions! Here's how to get started:

1. **Fork the repository** (local development)
2. **Create a feature branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make your changes** with clear, descriptive commits
4. **Test thoroughly** before submitting — verify CRUD operations in the Pivot Table
5. **Document your changes** in code comments

### Code Style & Standards

- Follow the existing code style on both client and server
- Write meaningful commit messages (e.g., `feat: add ShipCountry to PivotView columns`)
- Add XML doc comments for new public methods in C#
- Test all CRUD operations after any backend change
- Keep the React component props and DataManager configuration aligned with the API
- Ensure no console errors in the browser

### Pull Request Process

1. Update `README.md` if you add new features or change the setup steps
2. Test the full flow on both frontend and backend
3. Ensure the build passes (`npm run build` and `dotnet build`)
4. Provide a clear PR description of what changed and why

---

## 📜 License & Support

This is a **commercial product** subject to the Syncfusion End User License Agreement (EULA).

**Free Community License** is available for qualifying users/organizations:
- Annual gross revenue < $1 million USD
- 5 or fewer total developers
- 10 or fewer total employees

The community license allows free use in both internal and commercial applications under these conditions.
No registration or approval is required — just comply with the terms.

**Paid Licenses** are required for:
- Larger organizations
- Teams exceeding the community license limits
- Priority support, custom patches, or on-premise deployment options

Purchase options and pricing: https://www.syncfusion.com/sales/products
30-day free trial (full features, no credit card required): https://www.syncfusion.com/downloads/essential-js2
Community License details & FAQ: https://www.syncfusion.com/products/communitylicense
Full EULA: https://www.syncfusion.com/eula/es/

---

## ✅ Summary

This sample provides a simple, practical, and well-structured starting point for integrating the **Syncfusion React Pivot Table** with an **ASP.NET Core backend** using the **UrlAdaptor**. With clean separation between the client and server, configurable CRUD endpoints, and a working drill-through editor, you can use this project as a foundation for production-grade analytics dashboards.

© 2026 Syncfusion, Inc. All Rights Reserved.
