# Papara-Bootcamp-Final-Project

## Apartment Management System
In this project, there are two types of users: the admin, who is responsible for managing the apartment complex, and the residents, who own or rent the units in the complex. The administrator can manage resident profiles, assign dues to units, input monthly utility bills, view payments, and monitor debt statuses. Residents, on the other hand, can view assigned dues and utility bills, and make payments.

## Getting Started

Ensure you have the following installed:

- **.NET 8 SDK**: For compiling and running the project.
- **Visual Studio Code or Visual Studio**: For coding and editing.
- **MS SQL Server**: Local or cloud-based.
- **EF Core**: ORM library.
- **Dependencies**: Install from *.csproj file.

### Running the Project

1. **Clone the Repository**: Use Git:

    ```bash
    git clone <repository-url>
    ```

2. **Set up the Database**: Configure connection string in `appsettings.json`.

3. **Install Dependencies**: Using NuGet Package Manager.

4. **Build and Run the Project**: Via .NET CLI or code editor.

5. **Access the Application**: Through web browser or API client.

Follow these steps to have the project up and running.

## Usage

### Admin Role

Upon the initial setup of the system, an admin is automatically created with the following credentials:
- Username: admin@admin.com
- Password: Test1234

The admin role is responsible for the following tasks::
- Creating, updating, and deleting user profiles for apartments.
- Assign monthly dues to apartments collectively or individually.
- Input monthly utility bills for the building, such as electricity, water, and gas.
- View payments made by apartments.
- Check the monthly and yearly debt status for each apartment.
- View users who make regular payments. (Bonus)

### User Role

There are two user roles in the system: administrators and users. Users log in using their ID number and phone number. They can perform the following functions:
- View monthly dues and utility bills.
- Make payments for dues or bills.

## Controllers

### Admin Controller

This controller handles administrative tasks, including:
- Assigning users to apartments.
- Assigning monthly dues to apartments.
- Assigning monthly dues to all apartments.
- Adding monthly bills to all apartments.
- Retrieving monthly debt for a specific apartment.
- Retrieving yearly debt for a specific apartment.

### Apartment Controller

Responsible for managing apartments, this controller provides endpoints for:
- Retrieving all apartments.
- Adding a new apartment.
- Retrieving unpaid bills and dues for all apartments for a specific month.
- Retrieving unpaid bills and dues for a specific apartment for a specific month.
- Retrieving all bills and dues for a specific apartment for a specific month.

### Identity Controller

This controller manages authentication and token creation for both users and admins.

### Payment Controller

Handles payment-related functionalities, including:
- Making payments for monthly dues.
- Making payments for monthly bills.
- Retrieving payments made for a specific apartment.
- Retrieving apartments with regular payments.

### User Controller

Responsible for managing users, including:
- Retrieving all users.
- Creating a new user.
- Updating user information.
- Deleting a user.

