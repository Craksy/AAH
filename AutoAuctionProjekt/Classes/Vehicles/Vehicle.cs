using System;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AutoAuctionProjekt.Classes.Vehicles;

public struct VehicleProps {
    public string Name;
    public double Km;
    public string RegistrationNumber;
    public int Year;
    public decimal NewPrice;
    public bool HasTowbar;
    public double EngineSize;
    public double KmPerLiter;
    public FuelTypeEnum FuelType;
}

public class Vehicle {
    // regex to match (in order): [start of input] [2x any letter] [5x any digit] [end of input]
    private static readonly Regex RegistrationNumberValidationPattern = new(@"^[a-zA-Z]{2}\d{5}$");
    
    private double _km;
    private string _name;
    private uint _id;
    private EnergyClassEnum _energyClassEnum;
    private string _registrationNumber;
    private decimal _newPrice;

    protected Vehicle(
        string name,
        double km,
        string registrationNumber,
        int year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelTypeEnum fuelType)
    {
        Name = name;
        Km = km;
        RegistrationNumber = registrationNumber;
        Year = year;
        NewPrice = newPrice;
        HasTowbar = hasTowbar;
        EngineSize = engineSize;
        KmPerLiter = kmPerLiter;
        FuelType = fuelType;
        EnergyClass = _energyClassEnum;
    }

    public Vehicle(VehicleProps props) 
        : this(props.Name, props.Km, props.RegistrationNumber, props.Year, props.NewPrice, props.HasTowbar, props.EngineSize, props.KmPerLiter, props.FuelType) { }
    
    /// <summary>
    /// Primary key used to identify this vehicle in the database
    /// </summary>
    //REVIEW: Changed to init-only property.
    public uint ID { get; init; }

    /// <summary>
    /// Name of the vehicle
    /// </summary>
    /// <exception cref="ArgumentNullException">If assigned an null or empty string</exception>
    public string Name { 
        get => _name;
        set {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(Name), "Name cannot be empty");
            _name = value;
        } 
    }
        
    //REVIEW: Shouldn't this be "mileage"?
    /// <summary>
    /// Mileage of the vehicle
    /// </summary>
    public double Km { 
        get => _km;
        set {
            if (value < 0.0)
                throw new ArgumentOutOfRangeException(nameof(Km), "Cannot be negative");
            _km = value;
        } 
    }

    /// <summary>
    /// Registration number of the vehicle
    /// </summary>
    public string RegistrationNumber {
        //REVIEW: is this how the spec is to be interpreted? the raw value is still available privately, i guess.
        get => $"**{_registrationNumber.Substring(2, 3)}**";
        set {
            if (!RegistrationNumberValidationPattern.IsMatch(value))
                throw new InvalidRegistrationNumberException(value);
            _registrationNumber = value;
        }
    }

    /// <summary>
    /// Year the vehicle was produced
    /// </summary>
    public int Year { get; init; }

    /// <summary>
    /// New price field and property.
    /// Attempt to assign a negative value will clamp it as 0.0
    /// </summary>
    public decimal NewPrice {
        get => _newPrice;
        set => _newPrice = Math.Max(0m, value);
    }

    /// <summary>
    /// Whether or not the vehicle has a towbar
    /// </summary>
    public bool HasTowbar { get; set; }

    /// <summary>
    /// Engine size of the vehicle, in litres
    /// </summary>
    public virtual double EngineSize { get; set; }

    /// <summary>
    /// Km per liter field and property
    /// </summary>
    public double KmPerLiter { get; set; }

    /// <summary>
    /// Drivers lisence required for this type of vehicle
    /// </summary>
    public DriversLisenceEnum DriversLicense { get; set; }

    /// <summary>
    /// Fuel type
    /// </summary>
    public FuelTypeEnum FuelType { get; set; }

    //TODO: Someone had a stroke here. for one it's basically 2 getters. It also doesn't make sense to delegate to a parameterless method instead of inlining.
    /// <summary>
    /// Energy class of the vehicle
    /// </summary>
    public EnergyClassEnum EnergyClass
    {
        get => _energyClassEnum;
        private set => _energyClassEnum = value;
    }
        
    /// <summary>
    /// Engery class is calculated based on year of the car and the efficiancy in km/L.
    /// </summary>
    /// <returns>
    /// Returns the energy class in EnergyClassEnum (A,B,C,D)
    /// </returns>
    private EnergyClassEnum GetEnergyClass()
    {
        if (FuelType == FuelTypeEnum.Electric || FuelType == FuelTypeEnum.Hydrogen)
            return EnergyClassEnum.A;
        
        if (Year < 2010)
        {
            if (FuelType == FuelTypeEnum.Diesel)
            {
                if (KmPerLiter >= 23)
                    return EnergyClassEnum.A;

                if (KmPerLiter <= 18 && KmPerLiter < 23)
                    return EnergyClassEnum.B;

                if (KmPerLiter >= 13 && KmPerLiter < 18)
                    return EnergyClassEnum.C;

                if (KmPerLiter < 13)
                    return EnergyClassEnum.D;
            }
            else if (FuelType == FuelTypeEnum.Benzin)
            {
                if (KmPerLiter >= 18)
                    return EnergyClassEnum.A;

                if (KmPerLiter >= 14 && KmPerLiter < 18)
                    return EnergyClassEnum.B;

                if (KmPerLiter >= 10 && KmPerLiter < 14)
                    return EnergyClassEnum.C;

                if (KmPerLiter < 10)
                    return EnergyClassEnum.D;
            }
        }
        else if (Year > 2010)
        {
            if (FuelType == FuelTypeEnum.Diesel)
            {
                if (KmPerLiter >= 25)
                    return EnergyClassEnum.A;

                if (KmPerLiter >= 20 && KmPerLiter < 25)
                    return EnergyClassEnum.B;

                if (KmPerLiter >= 15 && KmPerLiter < 20)
                    return EnergyClassEnum.C;

                if (KmPerLiter < 15)
                    return EnergyClassEnum.D;
            } 
            else if (FuelType == FuelTypeEnum.Benzin)
            {
                if (KmPerLiter >= 20)
                    return EnergyClassEnum.A;
            
                if (KmPerLiter >= 16 && KmPerLiter < 20)
                    return EnergyClassEnum.B;
            
                if (KmPerLiter >= 12 && KmPerLiter < 16)
                    return EnergyClassEnum.C;
            
                if (KmPerLiter < 12)
                    return EnergyClassEnum.D;
            }
        }
        throw new Exception("Insufficient information for calculation of the vehicles energy class," +
                                          "or the calculation itself is miswritten. Refer with the assignment" +
                                          "for more information.");
    }
    
    /// <summary>
    /// Returns the vehicle in a string with relivant information.
    /// </summary>
    /// <returns>The Veihcle as a string</returns>
    public override string ToString()
    {
        // `this` gets evaluated at runtime meaning that it will correctly serialize
        // the derived type rather than `Vehicle`.
        dynamic me = this;
        return JsonSerializer.Serialize(me, new JsonSerializerOptions { WriteIndented = true });
    }
    
}

//REVIEW: Is electricity really "fuel" though?
/// <summary>
/// Type of fuel used by a vehicle
/// </summary>
public enum FuelTypeEnum
{
    Diesel,
    Benzin,
    Electric,
    Hydrogen,
}


/// <summary>
/// Drivers licence types
/// </summary>
public enum DriversLisenceEnum
{
    A,
    B,
    C,
    D,
    BE,
    CE,
    DE
}
    
/// <summary>
/// Vehicle energy class
/// </summary>
public enum EnergyClassEnum
{
    A,
    B,
    C,
    D
}

/// <summary>
/// Exception indicating an incorrect registration number
/// </summary>
public class InvalidRegistrationNumberException : Exception {
    //TODO: We could use capture groups in the validation regex to provide a more details about why it failed.
    /// <summary>
    /// Create a new exception from the original input
    /// </summary>
    /// <param name="reg">The registration number that failed validation</param>
    public InvalidRegistrationNumberException(string reg) 
        : base($"Invalid registration number \"{reg}\"\nA registration is two characters followed by five digits."){}
}