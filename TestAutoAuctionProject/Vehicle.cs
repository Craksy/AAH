using System;
using AutoAuctionProjekt.Classes.Vehicles;
using Xunit;

namespace TestAutoAuctionProject; 

public class Vehicle {
    private Bus _bus;
    Bus.VehicleDimensionsStruct _vd = new (10.0, 20.0, 30.0);
    
    private Bus CreateNewBus()
    {
        return new Bus(
            "car brand", 
            500.0, 
            "DF12745", 
            2018, 
            10000M, 
            false, 
            10.0, 
            20.0, 
            FuelTypeEnum.Diesel, 
            _vd,
            2,
            1,
            true);
        
    }
    
    [Fact]
    public void Bus_DriversLisenceDependOnTowbar() {
        // Arrange
        var bus = CreateNewBus();
        
        // Act
        var noTowbar = bus.DriversLicense;
        bus = new Bus(
            "car brand", 
            500.0, 
            "DF12745", 
            2018, 
            10000M, 
            true, 
            10.0, 
            20.0, 
            FuelTypeEnum.Diesel, 
            _vd,
            2,
            1,
            true);
        var withTowbar = bus.DriversLicense;
        
        // Assert
        Assert.Equal(DriversLisenceEnum.D, noTowbar);
        Assert.Equal(DriversLisenceEnum.DE, withTowbar);
    }

    [Fact]
    public void Bus_RegistrationNumber() {
        var bus = CreateNewBus();
        bus.RegistrationNumber = "AB12345";
        Assert.Equal("**123**", bus.RegistrationNumber);
        Assert.Throws<InvalidRegistrationNumberException>(() => bus.RegistrationNumber = "AA1234");
        Assert.Throws<InvalidRegistrationNumberException>(() => bus.RegistrationNumber = "AA1234B");
        Assert.Throws<InvalidRegistrationNumberException>(() => bus.RegistrationNumber = "1A12345");
    }

    [Fact]
    public void HeavyVehicle_EngineSize() {
        var bus = CreateNewBus();
        bus.EngineSize = 5.5;
        Assert.Throws<ArgumentOutOfRangeException>(() => bus.EngineSize = 3.1415);
        Assert.Throws<ArgumentOutOfRangeException>(() => bus.EngineSize = 42.0);

    }
    
    [Fact]
    public void Vehicle_NameCannotBeEmpty() {
        var bus = CreateNewBus();
        bus.Name = "Herbie";
        Assert.Throws<ArgumentNullException>(() => bus.Name = "");
        Assert.Throws<ArgumentNullException>(() => bus.Name = null);
    }
    
    [Fact]
    public void Vehicle_NegativePriceGetsClamped() {
        var bus = CreateNewBus();
        bus.NewPrice = 0.0001m;
        Assert.Equal(0.0001m, bus.NewPrice);
        bus.NewPrice = -0.2m;
        Assert.Equal(0.0m, bus.NewPrice);
    }

}