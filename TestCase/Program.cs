// See https://aka.ms/new-console-template for more information
using TestCase;

var input = string.Empty;
List<Vehicle> vehicles = new List<Vehicle>();
List<Vehicle> vehiclesDone = new List<Vehicle>();
List<int> emptySpot = new List<int>();
Vehicle vh;
var allocation = 0;

Console.WriteLine("=============================================================");
Console.Write("Please Input : ");

while (input != "exit")
{
	input = Console.ReadLine();

	if (input != null)
	{
        if (input.Contains("create_parking_lot ") && allocation == 0)
        {
            var def = input.Split(' ');
            allocation = int.Parse(def[1]);
            for (int i = 1; i <= allocation; i++) emptySpot.Add(i);
            Console.WriteLine();
            Console.WriteLine("Created a parking lot with {0} slots", allocation);
            Console.WriteLine();
        }
        else if (input.Contains("create_parking_lot ") && allocation != 0)
        {
            Console.WriteLine();
            Console.WriteLine("Cannot allocate parking lot anymore.");
            Console.WriteLine();
        }
        else if (input.Contains("park "))
        {
            var def = input.Split(' ');
            var type = def.LastOrDefault();

            if (vehicles.Count < allocation)
            {
                if (type == "Mobil")
                {
                    vh = new Mobil(emptySpot[0], def[2], def[1]);
                    vh.CheckIn = DateTime.Now;
                }
                else
                {
                    vh = new Motor(emptySpot[0], def[2], def[1]);
                    vh.CheckIn = DateTime.Now;
                }
                vehicles.Add(vh);
                emptySpot.RemoveAt(0);
                Console.WriteLine();
                Console.WriteLine("Allocated slot number {0}", vehicles.Count);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Sorry, parking lot is full");
                Console.WriteLine();
            }
        }
        else if (input.Contains("leave ") && vehicles.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("No Vehicle Parked Yet");
            Console.WriteLine();
        }
        else if (input.Contains("leave "))
        {
            var def = input.Split(' ');
            int lot = int.Parse(def[1]);


            var vhDef = vehicles.Where(v => v.Id == lot);
            Vehicle? leave = vhDef == null ? null : vhDef.FirstOrDefault();

            if (leave == null)
            {
                Console.WriteLine();
                Console.WriteLine("No Vehicle Found");
                Console.WriteLine();
            }
            else
            {
                leave.CheckOut = DateTime.Now;

                vehiclesDone.Add(leave);

                Console.WriteLine();
                Console.WriteLine("Allocated slot number {0}, {1} paid for {2} hours", leave.Id, leave.Plat, leave.CheckOut.AddHours(1).Subtract(leave.CheckIn).ToString(@"hh\:mm"));
                Console.WriteLine();

                vehicles.Remove(vehicles.Where(v => v.Id == lot).First());
                emptySpot.Add(lot);
            }

        }
        else if (input.Contains("type_of_vehicles "))
        {
            var def = input.Split(' ');
            var type = def.LastOrDefault();

            Console.WriteLine();
            Console.WriteLine(vehicles.Count(v => v.Type == type));
            Console.WriteLine();
        }
        else if (input.Contains("registration_numbers_for_vehicles_with_odd_plate"))
        {
            string temp = string.Join(",", vehicles.Where(v => v.IsOdd()).Select(v => v.Plat));
            Console.WriteLine(!String.IsNullOrEmpty(temp) ? temp : "Not found ");
        }
        else if (input.Contains("registration_numbers_for_vehicles_with_even_plate"))
        {
            string temp = string.Join(",", vehicles.Where(v => !v.IsOdd()).Select(v => v.Plat));
            Console.WriteLine();
            Console.WriteLine(!String.IsNullOrEmpty(temp) ? temp : "Not found ");
            Console.WriteLine();
        }
        else if (input.Contains("registration_numbers_for_vehicles_with_colour "))
        {
            var def = input.Split(' ');
            var color = def.LastOrDefault();

            string temp = string.Join(",", vehicles.Where(v => v.Color.Equals(color)).Select(v => v.Plat));
            Console.WriteLine();
            Console.WriteLine(!String.IsNullOrEmpty(temp) ? temp : "Not found ");
            Console.WriteLine();
        }
        else if (input.Contains("slot_numbers_for_vehicles_with_colour "))
        {
            var def = input.Split(' ');
            var color = def.LastOrDefault();

            string temp = string.Join(",", vehicles.Where(v => v.Color.Equals(color)).Select(v => v.Id));
            Console.WriteLine();
            Console.WriteLine(!String.IsNullOrEmpty(temp) ? temp : "Not found ");
            Console.WriteLine();
        }
        else if (input.Contains("slot_number_for_registration_number "))
        {
            var def = input.Split(' ');
            var plat = def.LastOrDefault();

            string temp = string.Join(",", vehicles.Where(v => v.Plat.Equals(plat)).Select(v => v.Id));
            Console.WriteLine();
            Console.WriteLine(!String.IsNullOrEmpty(temp) ? temp : "Not found ");
            Console.WriteLine();
        }
        else if (input.Contains("status"))
        {
            var table = new Table();

            table.SetHeaders("Slot", "Registration No.", "Type", "Colour");

            foreach(Vehicle vehicle in vehicles)
            {

                table.AddRow(vehicle.Id.ToString(), vehicle.Plat, vehicle.Type, vehicle.Color);
            }

            Console.WriteLine(table.ToString());
        }
        else if (allocation == 0 && emptySpot.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("Warning , Not yet Allocated");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine();
            Console.Write("Command not found");
            Console.WriteLine();
        }

        vehicles = vehicles.OrderBy(x => x.Id).ToList();
        Console.WriteLine("=============================================================");
        Console.WriteLine("allocation : " + allocation);
        Console.WriteLine("Lot left : " + string.Join(", ", emptySpot.Select(t => t)));
        Console.Write("Please Input : ");
    }
}