
# Crystal Reports PDF Generator API

This API allows you to dynamically generate PDF reports using **SAP Crystal Reports** templates within a C#.NET application. The project is built using the **ASP.NET Web Application (.NET Framework)** template and leverages **.NET Framework 4.8** for seamless integration with **Crystal Reports**.

## Features

- **Dynamic Report Generation:**  Generate PDF reports based on data passed from the client.
- **Crystal Reports Integration:** Leverages the power and flexibility of Crystal Reports templates.
- **Customizable:** Choose between direct download or in-browser display of the generated PDF.
- **Error Handling:** Includes input validation and basic error response mechanisms to ensure smooth functionality.

## Architecture

The project is divided into three key components:

1.  **Controller**: Handles requests from the client, processes parameters, and orchestrates report generation.
2.  **Model**: Represents the data passed into the controller and the Crystal Report's data source.
3.  **Template**: Contains the pre-defined Crystal Reports (`*.rpt` files), which are used to generate PDF files based on the supplied data.

### Controller

The controller is responsible for:

-   Receiving requests and parameter models from the client.
-   Reading the report template path from the model.
-   Setting the data source for the report based on the provided model.
-   Exporting the generated Crystal Report to a PDF file.

### Model

The model:

-   Contains the parameters and data structure needed for report generation.
-   Includes fields such as template path and data that will be set as the data source for the report.

### Template

Crystal Reports templates (`*.rpt` files) are stored in the `/Templates` directory. These are pre-designed reports that will be populated with data at runtime.

## Prerequisites

Before starting, ensure you have the following:

-   **ASP.NET Web API Project** using **.NET Framework 4.8**.
-   **SAP Crystal Reports Runtime** installed and compatible with your project.
-   Your **Crystal Reports templates** (`*.rpt` files) ready for use.

## Installation

1.  **API Integration**:
    -   Integrate the API endpoint code into your existing ASP.NET Web API project.
2.  **Template Configuration**:
    -   Place your Crystal Reports templates in the designated `/Templates` folder within your project.

## Unit Testing with NUnit

Unit testing for this API is implemented using the **NUnit** framework to ensure the stability and correctness of the report generation logic. The following areas are covered by the unit tests:

1.  **Controller Tests**:
    -   Verify that the controller correctly handles incoming requests.
    -   Ensure that the correct template path is used.
    -   Validate that the correct data is passed to the Crystal Reports engine.
2.  **PDF Generation Tests**:
    -   Check if the Crystal Report is properly generated and exported to PDF.
    -   Test error handling and edge cases, such as missing templates or incorrect data formats.

## License

This project is licensed under the MIT License.
