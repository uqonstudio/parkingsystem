/**
OUTPUT EXAMPLE
$ create_parking_lot 6
Created a parking lot with 6 slots

$ park B-1234-XYZ Putih Mobil
Allocated slot number: 1

$ park B-9999-XYZ Putih Motor
Allocated slot number: 2

$ park D-0001-HIJ Hitam Mobil
Allocated slot number: 3

$ park B-7777-DEF Red Mobil
Allocated slot number: 4

$ park B-2701-XXX Biru Mobil
Allocated slot number: 5

$ park B-3141-ZZZ Hitam Motor
Allocated slot number: 6

$ leave 4
Slot number 4 is free



$ status
Slot 	No. 		Type	Registration No Colour
1 		B-1234-XYZ	Mobil	Putih
2 		B-9999-XYZ	Motor	Putih
3 		D-0001-HIJ 	Mobil	Hitam
5 		B-2701-XXX 	Mobil	Biru
6 		B-3141-ZZZ 	Motor	Hitam

$ park B-333-SSS Putih Mobil
Allocated slot number: 4

$ park A-1212-GGG Putih Mobil
Sorry, parking lot is full

$ type_of_vehicles Motor
2

$ type_of_vehicles Mobil
4

$ registration_numbers_for_vehicles_with_ood_plate
B-9999-XYZ, D-0001-HIJ, B-2701-XXX

$ registration_numbers_for_vehicles_with_event_plate
B-1234-XYZ, B-3141-ZZZ

$ registration_numbers_for_vehicles_with_colour Putih
B-1234-XYZ, B-9999-XYZ, B-333-SSS

$ slot_numbers_for_vehicles_with_colour Putih
1, 2, 4

$ slot_number_for_registration_number B-3141-ZZZ
6

$ slot_number_for_registration_number Z-1111-AAA
Not found

$ exit

**/

using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingLotManager? parkingLotManager = null;

            while (true)
            {
                string? command = Console.ReadLine();
                if (command == null || command == "exit") break;

                var parts = command.Split(' ');
                if (parts.Length == 0) continue;

                switch (parts[0])
                {
                    case "create_parking_lot":
                        CreateParkingLot(parts, ref parkingLotManager);
                        // Console.WriteLine("CreateParkingLot");
                        break;

                    case "park":
                        ParkVehicle(parts, parkingLotManager);
                        // Console.WriteLine("ParkVehicle");
                        break;

                    case "leave":
                        LeaveSlot(parts, parkingLotManager);
                        // Console.WriteLine("LeaveSlot");
                        break;

                    case "status":
                        if (parkingLotManager != null)
                        {
                        // Console.WriteLine("parkingLotStatus");
                            parkingLotManager.Status();
                        }
                        else
                        {
                            Console.WriteLine("Parking lot not created.");
                        }
                        break;

                    case "type_of_vehicles":
                        CountVehiclesByType(parts, parkingLotManager);
                        // Console.WriteLine("CountVehiclesByType");
                        break;

                    case "registration_numbers_for_vehicles_with_ood_plate":
                        if (parkingLotManager != null)
                        {
                            parkingLotManager.RegistrationNumbersWithOddPlate();
                        // Console.WriteLine("RegistrationNumbersWithOddPlate");
                        }
                        else
                        {
                            Console.WriteLine("Parking lot not created.");
                        }
                        break;

                    case "registration_numbers_for_vehicles_with_event_plate":
                        if (parkingLotManager != null)
                        {
                            parkingLotManager.RegistrationNumbersWithEvenPlate();
                        // Console.WriteLine("RegistrationNumbersWithEvenPlate");
                        }
                        else
                        {
                            Console.WriteLine("Parking lot not created.");
                        }
                        break;

                    case "registration_numbers_for_vehicles_with_colour":
                        RegistrationNumbersByColor(parts, parkingLotManager);
                        // Console.WriteLine("RegistrationNumbersByColor");
                        break;

                    case "slot_numbers_for_vehicles_with_colour":
                        SlotNumbersByColor(parts, parkingLotManager);
                        // Console.WriteLine("SlotNumbersByColor");
                        break;

                    case "slot_number_for_registration_number":
                        SlotNumberByRegistrationNumber(parts, parkingLotManager);
                        // Console.WriteLine("SlotNumberByRegistrationNumber");
                        break;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }

        static void CreateParkingLot(string[] parts, ref ParkingLotManager? parkingLotManager)
        {
            if (parts.Length > 1 && int.TryParse(parts[1], out int totalLots))
            {
                parkingLotManager = new ParkingLotManager(totalLots);
                Console.WriteLine($"Created a parking lot with {totalLots} slots");
            }
            else
            {
                Console.WriteLine("Invalid command or parameters.");
            }
        }

        static void ParkVehicle(string[] parts, ParkingLotManager? parkingLotManager)
        {
            if (parkingLotManager != null && parts.Length > 3)
            {
                string licensePlate = parts[1];
                string color = parts[2];
                string type = parts[3];
                parkingLotManager.CheckIn(type, licensePlate, color);
            }
            else
            {
                Console.WriteLine("Parking lot not created or invalid parameters.");
            }
        }

        static void LeaveSlot(string[] parts, ParkingLotManager? parkingLotManager)
        {
            if (parkingLotManager != null && parts.Length > 1 && int.TryParse(parts[1], out int slotNumber))
            {
                Console.WriteLine($"slotNumber {slotNumber}");
                parkingLotManager.CheckOutBySlot(slotNumber);
            }
            else
            {
                Console.WriteLine("Parking lot not created or invalid parameters.");
            }
        }

        static void CountVehiclesByType(string[] parts, ParkingLotManager? parkingLotManager)
        {
            if (parkingLotManager != null && parts.Length > 1)
            {
                string vehicleType = parts[1];
                parkingLotManager.CountByType(vehicleType);
            }
            else
            {
                Console.WriteLine("Parking lot not created or invalid parameters.");
            }
        }

        static void RegistrationNumbersByColor(string[] parts, ParkingLotManager? parkingLotManager)
        {
            if (parkingLotManager != null && parts.Length > 1)
            {
                string color = parts[1];
                parkingLotManager.RegistrationNumbersByColor(color);
            }
            else
            {
                Console.WriteLine("Parking lot not created or invalid parameters.");
            }
        }

        static void SlotNumbersByColor(string[] parts, ParkingLotManager? parkingLotManager)
        {
            if (parkingLotManager != null && parts.Length > 1)
            {
                string color = parts[1];
                parkingLotManager.SlotNumbersByColor(color);
            }
            else
            {
                Console.WriteLine("Parking lot not created or invalid parameters.");
            }
        }

        static void SlotNumberByRegistrationNumber(string[] parts, ParkingLotManager? parkingLotManager)
        {
            if (parkingLotManager != null && parts.Length > 1)
            {
                string regNumber = parts[1];
                parkingLotManager.SlotNumberByRegistrationNumber(regNumber);
            }
            else
            {
                Console.WriteLine("Parking lot not created or invalid parameters.");
            }
        }
    }

    public class ParkingLotManager
    {
        private readonly int totalLots;
        private readonly List<Vehicle> parkedVehicles;

        public ParkingLotManager(int totalLots)
        {
            this.totalLots = totalLots;
            this.parkedVehicles = new List<Vehicle>();
        }

        public void CheckIn(string type, string licensePlate, string color)
        {
            if (parkedVehicles.Count >= totalLots)
            {
                Console.WriteLine("Sorry, parking lot is full");
                return;
            }

            if (type != "Mobil" && type != "Motor")
            {
                Console.WriteLine("Only Mobil and Motor are allowed.");
                return;
            }

            // Find the first available slot number
            int slotNumber = 1;
            for (int i = 1; i <= totalLots; i++)
            {
                if (!parkedVehicles.Any(v => v.SlotNumber == i))
                {
                    slotNumber = i;
                    break;
                }
            }

            var vehicle = new Vehicle(type, licensePlate, color, slotNumber);
            parkedVehicles.Add(vehicle);
            Console.WriteLine($"Allocated slot number: {slotNumber}");
        }

        public void CheckOutBySlot(int slotNumber)
        {
            var vehicle = parkedVehicles.FirstOrDefault(v => v.SlotNumber == slotNumber);
            if (vehicle == null)
            {
                Console.WriteLine("Slot number is invalid");
                return;
            }

            parkedVehicles.Remove(vehicle);
            Console.WriteLine($"Slot number {slotNumber} is free");
        }

        public void Status()
        {
            Console.WriteLine("SlotNo. Type Registration No Color");
            foreach (var vehicle in parkedVehicles)
            { 
                Console.WriteLine($"{vehicle.SlotNumber} {vehicle.LicensePlate} {vehicle.Type} {vehicle.Color}");
            }
        }

        public void CountByType(string type)
        {
            var count = parkedVehicles.Count(v => v.Type == type);
            Console.WriteLine(count);
        }

        public void RegistrationNumbersWithOddPlate()
        {
            var oddPlates = parkedVehicles
                .Where(v => !string.IsNullOrEmpty(v.LicensePlate))
                .Where(v =>
                {
                    // Extract the numeric portion of the license plate
                    var numericPart = new string(v.LicensePlate.Where(char.IsDigit).ToArray());
                    // Check if the numeric part is not empty and the last digit is odd
                    return !string.IsNullOrEmpty(numericPart) && 
                        int.Parse(numericPart[^1].ToString()) % 2 != 0;
                })
                .Select(v => v.LicensePlate);

            if (oddPlates.Any())
            {
                Console.WriteLine(string.Join(", ", oddPlates));
            }
            else
            {
                Console.WriteLine("No vehicles with odd-numbered plates found.");
            }
        }

        public void RegistrationNumbersWithEvenPlate()
        {
            var evenPlates = parkedVehicles
                .Where(v => !string.IsNullOrEmpty(v.LicensePlate))
                .Where(v =>
                {
                    // Extract the numeric portion of the license plate
                    var numericPart = new string(v.LicensePlate.Where(char.IsDigit).ToArray());
                    // Check if the numeric part is not empty and the last digit is even
                    return !string.IsNullOrEmpty(numericPart) && 
                        int.Parse(numericPart[^1].ToString()) % 2 == 0;
                })
                .Select(v => v.LicensePlate);

            if (evenPlates.Any())
            {
                Console.WriteLine(string.Join(", ", evenPlates));
            }
            else
            {
                Console.WriteLine("No vehicles with even-numbered plates found.");
            }
        }

        public void RegistrationNumbersByColor(string color)
        {
            var regNumbers = parkedVehicles
                .Where(v => v.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(v => v.LicensePlate);

            Console.WriteLine(string.Join(", ", regNumbers));
        }

        public void SlotNumbersByColor(string color)
        {
            var slots = parkedVehicles
                .Where(v => v.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(v => v.SlotNumber);

            if (slots.Any())
            {
                Console.WriteLine(string.Join(", ", slots));
            }
            else
            {
                Console.WriteLine($"No vehicles with color {color} found.");
            }
        }

        public void SlotNumberByRegistrationNumber(string regNumber)
        {
            var vehicle = parkedVehicles
                .FirstOrDefault(v => v.LicensePlate.Equals(regNumber, StringComparison.OrdinalIgnoreCase));

            if (vehicle != null)
            {
                Console.WriteLine(vehicle.SlotNumber);
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }
    }

    public class Vehicle
    {
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public DateTime CheckInTime { get; set; }
        public int SlotNumber { get; set; }

        public Vehicle(string type, string licensePlate, string color, int slotNumber)
        {
            Type = type;
            LicensePlate = licensePlate;
            Color = color;
            CheckInTime = DateTime.Now;
            SlotNumber = slotNumber;
        }
    }
}