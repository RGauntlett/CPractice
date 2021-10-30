using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Data.OleDb;


namespace NETproject
{
    //Create Car Object
    public class Car
    {

        // [Name("id")]
        // // public int Id {get; set;}
        [Name("year")]
        public int Year { get; set; }
        [Name("make")]
        public string Make { get; set; }
        [Name("model")]
        public string Model { get; set; }
        [Name("price")]
        public double Price { get; set; }
        public Car(int year, string make, string model, double price)
        {
            // Id = Id;
            Year = year;
            Make = make;
            Model = model;
            Price = price;
        }
    }

    class Program
    {

        
         static void Main(string[] args) 
         {
            //  call 
                CarLot();         
         }


        // enter a new make of car that has the year the make, model and price. store in a csv file. be able to filter by year. and delete cars from list
        public static void CarLot(){
            // Prompt the user to enter a menu choice, one for entering a car, 2 for searching
             string val;
            Console.Write("Please enter 1 to add a car, 2 to search by year, or 3 to delete a car: ");
            val = Console.ReadLine();
            // convert menu choice to int
            int menuChoice = Int16.Parse(val);

            // Create a List to store car information
            List<Car> carList = new List<Car>();

           // Dummy car data
            // addCar(1,2021,"mercedes","a-class",63504.60);
            // addCar(2,2020,"BMW","A320",45095.34);
            // addCar(3,2019,"toyota","hilux",23456.34);
            // addCar(4,1994,"toyota","g-series",1040.23);
            // addCar(5,2012,"Bentley","A5",54039.23);
            // addCar(6,1950,"Ford","original",120000);

            // check the menu choice
            if (menuChoice == 1) {
               

                // create variable to store year
                Console.Write("Please enter the year of the car: ");
                string enteredYear = Console.ReadLine();

                // convert from str to int
                int yearOfCar = Int16.Parse(enteredYear);

                //check that year is 4 numbers and within reasonable time frame
                if(enteredYear.Length != 4 || yearOfCar < 1900 || yearOfCar > 2021){
                    Console.WriteLine("You have entered an invalid year. Please restart the program");
                } else {
                    // Prompt user for the make of car
                    Console.Write("Please enter the make of the car: ");
                    string enteredMake = Console.ReadLine();

                    // Prompt the user for the model of the car
                    Console.Write("Please enter the model of the car: ");
                    string enteredModel = Console.ReadLine();

                    // Prompt users for the price of the car
                    Console.Write("Please enter the price of the car: ");
                    string enteredPrice = Console.ReadLine();

                    //Convert to double
                    double price = double.Parse(enteredPrice);

                    // Add the new car to the car list
                    addCar(yearOfCar, enteredMake, enteredModel, price, "./carLot.csv");
                }
            } 
            
            // check if the user has entered 2
            else if (menuChoice == 2) {
            

                // Ask the user which year they want to filter by
                Console.Write("Please enter the year of cars that you would like to see: ");

                // Create variable to search
                string enteredSearchYear = Console.ReadLine();

                // Parse to int
                int searchYear = Int16.Parse(enteredSearchYear);



                //Check to make sure the user entered a valid year
                if (enteredSearchYear.Length != 4 || searchYear < 1900 || searchYear > 2021) {
                    Console.Write("You have entered an invalid choice, please restart the program");
                } 
                else 
                {   
                    // Filter the car list by search year
                    filterCars(searchYear);
                }

                
            } 
            // check if the user wants to delete a car
            else if(menuChoice == 3) {

                using (var streamReader = new StreamReader(@"./carLot.csv"))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                           var carLot = csvReader.GetRecords<Car>().ToList();

                            for(int k = 0; k<carLot.Count; k++){
                            Console.WriteLine(k + " " + carLot[k].Year  + " " +  carLot[k].Make  + " " +  carLot[k].Model  + " " +  carLot[k].Price);
                            
                            } Console.Write("Which car would you like to delete? ");

                            // read the users answer and convert to int
                            int carToDelete = Int16.Parse(Console.ReadLine());

                            if(carToDelete < 0 || carToDelete > carLot.Count - 1)
                            {
                                Console.WriteLine("This car does not exist, sorry!");
                            } else 
                            {
                                deleteCar("./carLot.cs", carToDelete);
                            }
                    }
                }
            }
            else if (menuChoice == 4) 
            {
                using (var streamReader = new StreamReader(@"./carLot.csv"))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                           var carLot = csvReader.GetRecords<Car>().ToList();

                            for(int k = 0; k<carLot.Count; k++){
                            Console.WriteLine(k + " " + carLot[k].Year  + " " +  carLot[k].Make  + " " +  carLot[k].Model  + " " +  carLot[k].Price);
                            
                            } Console.Write("Which car would you like to delete? ");

                            // read the users answer and convert to int
                            int carToReplace = Int16.Parse(Console.ReadLine());

                            if(carToReplace < 0 || carToReplace > carLot.Count - 1)
                            {
                                Console.WriteLine("This car does not exist, sorry!");
                            } else 
                            {
                                Console.Write("What is the new price for the car? ");
                                string enteredPrice = Console.ReadLine();

                                double newPrice = double.Parse(enteredPrice);
                                replacePrice(carToReplace, newPrice);
                                Console.WriteLine("Car has been updated");
                            }
                    }
                }
            }
            else Console.Write("You have entered an invalid choice, please restart the program");
        }

              
            // Create function to filter Cars
            public static void filterCars(int filterYear){
                // connect the path to csv using streamreader
               
                using (var streamReader = new StreamReader(@"./carLot.csv"))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                           var carLot = csvReader.GetRecords<Car>().ToList();
                        // loop over the code in Car Lot and check if the Car year is the same as the filtered year. If so print it out
                        foreach (Car Car in carLot) 
                        {
                            
                            if (Car.Year == filterYear) {
                            Console.WriteLine(Car.Year + " " + Car.Make + " " + Car.Model + " " + Car.Price);
                            } 
                        }
                    };
                }  
            }
        
     // Create AddCar function
         public static void addCar(int year, string make, string model, double price, string filepath) {

             // try catch to add errors
             try{

                 // set up the streamwriter witht he file path to write to our csv
                 using(System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true)){

                     //write to our csv
                     file.WriteLine(year + "," + make + "," + model + "," + price);
                 }

                 //Catch errors
             } catch(Exception ex){
                 throw new ApplicationException("an error occured:", ex);
             }
         }

        //Create Function to Delete Cars
        public static void deleteCar(string filepath, int positionOfSearchedCar)
        {
             // get list of cars
            using (var streamReader = new StreamReader(@"./carLot.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {

                    var carLotToFilter = csvReader.GetRecords<Car>().ToList();

                    // get original length of carLot.csv
                    int originalLength = carLotToFilter.Count;
                    // create temporary List to hold all of our data except the deleted one
                    List<Car> tempfile = new List<Car>();

                    try {

                        for(int i = 0; i<originalLength-1; i++) {
                            if (i != positionOfSearchedCar) {
                                tempfile.Add(carLotToFilter[i]);     
                            }              
                        }

                        System.IO.File.Delete("./carLot.csv");
                        // set up the streamwriter witht he file path to write to our csv
                        using(System.IO.StreamWriter file = new System.IO.StreamWriter("./carLot.csv", true))
                        {
                         //write to our csv
                         file.WriteLine("year,make,model,price");
                         
                        }

                        for(int j = 0; j< tempfile.Count; j++) 
                        {
                            addCar(tempfile[j].Year, tempfile[j].Make, tempfile[j].Model, tempfile[j].Price, "./carLot.csv");
                        }
                    
                    } 
                    // catch and print errors
                        catch (Exception e) 
                        {
                            throw new ApplicationException("Error deleting Car: ", e);
                        }
                }
            }
        }

        // function for replacing
        public static void replacePrice(int positionOfCarToReplace, double newPrice)
        {
                 // get list of cars
            using (var streamReader = new StreamReader(@"./carLot.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                      
                        
                            // get the cars in a list
                            var carLot = csvReader.GetRecords<Car>().ToList();

                            // get original length of carLot.csv
                            int originalLength = carLot.Count;

                            // create temporary List to hold all of our data except the deleted one
                            List<Car> tempfile = new List<Car>();

                            // run a for loop to see if the car position equals the selected number
                            for(int i=0; i<originalLength-1; i++)
                            {
                                if (i == positionOfCarToReplace) 
                                {
                                    Car changedPrice = new Car(carLot[i].Year,carLot[i].Make,carLot[i].Model, newPrice);
                                    tempfile.Add(changedPrice);
                                } 
                                else 
                                {
                                    tempfile.Add(carLot[i]);  
                                }

                            }

                            System.IO.File.Delete("./carLot.csv");
                            Console.WriteLine("is this working?");

                            using(System.IO.StreamWriter file = new System.IO.StreamWriter("./carLot.csv", true))
                            {
                                //write to our csv
                                file.WriteLine("year,make,model,price");
                            }
                            


                            for(int j = 0; j< tempfile.Count; j++) 
                            {
                            addCar(tempfile[j].Year, tempfile[j].Make, tempfile[j].Model, tempfile[j].Price, "./carLot.csv");
                            }

                         
                        
                   


                }
            }
        }


        // declare FizzBuzz Function
         static void Fizzbuzz(int num){
             
                for (int i = 1; i<= num; i++) {
                    Boolean f = i % 3 == 0;
                    Boolean b = i % 5 == 0;
                        Console.WriteLine(f ? (b ? "FizzBuzz" : "Fizz") : b ? "Buzz" : i);
                
            }
         }
        
        // do this without reverse - 
         static void StringFlipWithoutReverse(string args){
            //  Convert string to an array
            char[] Arr=args.ToCharArray();

           
              //create an empty string to store the reversed array
              string reversed = String.Empty;

            // run a for loop from the last index of the array to the 0 index
            for (int i = Arr.Length - 1; i > -1; i--) {

              

                // Push each index to the new array in reverse order
                reversed += Arr[i];

               
                
            } 
             // Print the new array
            Console.WriteLine(reversed);
           
            
        }
        // do it without ToCharArray - 
            static void StringFlipNoCharArray(string args) {

        // create empty string to store reversed sentence
        string reversed = string.Empty;

           // for each character in the string push it to the reversed string
            for (int i = args.Length - 1; i >=0 ; i--) {
                reversed += args[i];
            }
            Console.WriteLine(reversed);
    }

        // keep the words in order but reverse all the letters in each word - 
        static void StringWordFlip(string args) {


            // Convert string to List Seperated at each space
            List<string> wordList = new List<string>(args.Split(' '));

            // create string to hold reversed sentence 
            string reversed = String.Empty;

            // Loop over each item in the list and call the reverse function to flip the word
            for (int i = 0; i <= wordList.Count - 1; i++){
                char[] word = wordList[i].ToCharArray();     
                Array.Reverse(word);
                 
                
                // Add the reversed word back to the string
                for (int j = 0; j <= word.Length - 1; j++){
                reversed += word[j];
                
                }
                // Add space after each word
                reversed += ' ';
            }
            Console.WriteLine(reversed);
        }


        static void StringFlip(string args){
            char[] arr=args.ToCharArray();
            Array.Reverse(arr);     
            Console.WriteLine(arr); 
            
        }
    }
}
