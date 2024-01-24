# PetStore

PetStore is a simple console application that interacts with the PetStore API to retrieve and display information about available pets.

## Features

- Fetches data from the PetStore API based on Swagger documentation.
- Displays a list of available pets sorted by category and in reverse order by name.

## Prerequisites

- .NET Core SDK
- (Optional) Visual Studio or any preferred code editor

## Getting Started

1. Clone the repository:

    git clone https://github.com/balaji16464/PetStore.git

2. Navigate to the project directory:

    cd PetStore

3. Build and run the application:

    dotnet run

## Configuration

- The PetService class uses the HttpClientWrapper to make HTTP requests. Ensure that the Swagger documentation or API endpoint URL in the PetService class is correctly set.

## Usage

- Run the application to fetch and display available pets.

## Testing

PetServiceTests project contains unit tests for the PetService class. To run the tests:

dotnet test
