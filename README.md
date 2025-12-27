Point of Sale (POS) Management System

A robust desktop retail management solution designed with C# and SQL Server, following the N-Tier Architecture to ensure scalability, security, and high performance.



🚀 Key Features (Based on Code Structure)

Layered Architecture: Full implementation of Business Logic Layer (BLL) and Data Access Layer (DAL) for clean code separation.



Comprehensive Invoicing: Manage sales through dynamic modules for Invoices and Invoice Items, allowing for real-time transaction processing.



Inventory \& Product Management: Full control over product catalogs, stock tracking, and pricing.



Customer \& Person Management: Dedicated modules to manage customer data and personal information with centralized logic.



User Security \& Access Control: Secure user management system including Login/Logout functionality and role-based access.



Global Utilities: Specialized classes for common tasks:



clsFormat: For data and currency formatting.



clsValidation: To ensure data integrity across all forms.



clsUtil: General purpose helper methods.



Error Handling: Integrated HandlerErrors project to capture and manage runtime exceptions gracefully.



🛠 Tech Stack

Language: C# (Windows Forms).



Framework: .NET Framework.



Database: Microsoft SQL Server via ADO.NET.



Architecture: 3-Layer Architecture (Presentation, Business, Data Access).



📂 Solution Structure

POS System (Presentation): Contains UI Forms organized by module (Customer, Invoice, Product, Users).



POS\_BusinessLayer: Contains business rules and entities (e.g., clsProduct.cs, clsInvoice.cs).



POS\_DataAccessLayer: Handles all direct database communication and SQL commands.



HandlerErrors: A dedicated project for centralized error logging and handling.



⚙️ Setup \& Installation

Clone the Repository.



Database Setup: Execute the SQL script (if provided) or create the database structure using the DataAccessSettings.cs configuration.



Configuration: Update the connection string in the App.config file within the main project.



Run: Open the .sln file in Visual Studio and press F5 to build and run.

