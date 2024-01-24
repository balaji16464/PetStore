using System;
using System.Net.Http;
using System.Threading.Tasks;
using static PetStoreApp.PetService;

namespace PetStoreApp
{
    class Program
    {
        static async Task Main()
        {
            using (var httpClient = new HttpClient())
            {
                var httpClientWrapper = new HttpClientWrapper(httpClient);
                var petService = new PetService(httpClientWrapper);

                try
                {
                    // Call the GetAvailablePets method
                    var availablePets = await petService.DisplayAvailablePets();

                    // Sort the available pets by Category and in reverse order by Name
                    var sortedPets = availablePets
                        .OrderBy(pet => pet.Category?.Name)     // Sort by Category
                        .ThenByDescending(pet => pet.Name)       // Sort in reverse order by Name
                        .ToList();

                    // Display the sorted pets
                    Console.WriteLine("Available Pets (Sorted by Category and in Reverse Order by Name):");
                    foreach (var pet in sortedPets)
                    {
                        Console.WriteLine($"ID: {pet.Id}, Name: {pet.Name}, Status: {pet.Status}, Category: {pet.Category?.Name}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
