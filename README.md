# Buying & Owning Real Estate Nordic

A .NET Console application for managing taxes applied in different municipalities. The app supports adding and retrieving tax records for municipalities based on specific dates. Taxes can be scheduled for different periods.

## Prerequisites

- **.NET SDK** version 8.0 or over
- **Entity Framework Core CLI Tools** (install globally via `dotnet tool install --global dotnet-ef`)

## Getting Started

1. **Clone the Repository**

   ```bash
   git clone <repository_url>
   cd <repository_directory>/danskebank-task
   ```

2. **Restore Dependencies**

   ```bash
   dotnet restore
   ```

3. **Set Up database. Migration is already added, you just need to update**

   ```bash
   dotnet ef database update
   ```

4. **Run**

   ```bash
   dotnet run
   ```

## How to use

### Commands

- **Get Tax record for Municipality on a Specific Date**:

  - Format: `get <municipality> <date>`
  - Example:
    ```bash
    get copenhagen 2016-01-31
    ```

- **Add single Tax for Municipality on a Specific Date**:

  - Format: `add <municipality> <taxType> <taxRate> <startDate> <endDate>`
  - Example:
    ```bash
    add copenhagen year 0.2 2025-01-01 2025-12-31
    ```
  - **OBS:** Always include both startDate and endDate when adding a tax record, even for the 'Day' tax type.

- **Import Tax Data from CSV file**:
  - Format: `import <filePath>`
  - Example:
    ```bash
    import C://taxes.csv
    ```
  - CSV file **must** follow this structure:
    ```csv
    Copenhagen,year,0.2,2016-01-01,2016-12-31
    Copenhagen,month,0.4,2016-05-01,2016-05-31
    Copenhagen,day,0.1,2016-01-01,2016-01-01
    ```
