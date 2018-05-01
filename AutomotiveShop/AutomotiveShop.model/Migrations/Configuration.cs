using System.Collections.Generic;

namespace AutomotiveShop.model.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomotiveShop.model.AutomotiveShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutomotiveShop.model.AutomotiveShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Random random = new Random();
            foreach (var category in context.Categories)
            {
                context.Categories.Remove(category);
            }
            var categories = new List<Category>()
            {
                new Category() {Name = "Air Conditioning & Heating Parts"},
                new Category() {Name = "Brakes & Brake Parts"},
                new Category() {Name = "Electrical Components"},
                new Category() {Name = "Emission Systems"},
                new Category() {Name = "Engine Cooling"},
                new Category() {Name = "Engines & Engine Parts"},
                new Category() {Name = "External Lights & Indicators"},
                new Category() {Name = "Ignition Parts"},
                new Category() {Name = "Suspension & Steering Parts"}
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            foreach (var subcategory in context.Subcategories)
            {
                context.Subcategories.Remove(subcategory);
            }
            var subcategories = new List<Subcategory>()
            {
                new Subcategory() {Name = "A/C Compressors & Clutches", Category = context.Categories.FirstOrDefault(c => c.Name == "Air Conditioning & Heating Parts")},
                new Subcategory() {Name = "Heater Parts", Category = context.Categories.FirstOrDefault(c => c.Name == "Air Conditioning & Heating Parts")},

                new Subcategory() {Name = "Brake Discs", Category = context.Categories.FirstOrDefault(c => c.Name == "Brakes & Brake Parts") },
                new Subcategory() {Name = "Brake Drums", Category = context.Categories.FirstOrDefault(c => c.Name == "Brakes & Brake Parts") },
                new Subcategory() {Name = "Break Pads", Category = context.Categories.FirstOrDefault(c => c.Name == "Brakes & Brake Parts") },
                new Subcategory() {Name = "Break Shoes", Category = context.Categories.FirstOrDefault(c => c.Name == "Brakes & Brake Parts") },

                new Subcategory() {Name = "Batteries", Category = context.Categories.FirstOrDefault(c => c.Name == "Electrical Components")},
                new Subcategory() {Name = "Horns", Category = context.Categories.FirstOrDefault(c => c.Name == "Electrical Components")},
                new Subcategory() {Name = "Wires & Cabling", Category = context.Categories.FirstOrDefault(c => c.Name == "Electrical Components")},

                new Subcategory() {Name = "Air Bypass Valves", Category = context.Categories.FirstOrDefault(c => c.Name == "Emission Systems")},
                new Subcategory() {Name = "Smog & Air Pumps", Category = context.Categories.FirstOrDefault(c => c.Name == "Emission Systems")},

                new Subcategory() {Name = "Engine Fans & Fan Parts", Category = context.Categories.FirstOrDefault(c => c.Name == "Engine Cooling")},
                new Subcategory() {Name = "Radiators", Category = context.Categories.FirstOrDefault(c => c.Name == "Engine Cooling")},
                new Subcategory() {Name = "Water Pumps", Category = context.Categories.FirstOrDefault(c => c.Name == "Engine Cooling")},

                new Subcategory() {Name = "Engine Chains", Category = context.Categories.FirstOrDefault(c => c.Name == "Engines & Engine Parts")},
                new Subcategory() {Name = "Engine Valves", Category = context.Categories.FirstOrDefault(c => c.Name == "Engines & Engine Parts")},

                new Subcategory() {Name = "Headlight Assemblies", Category = context.Categories.FirstOrDefault(c => c.Name == "External Lights & Indicators")},
                new Subcategory() {Name = "Indicator Assemblies", Category = context.Categories.FirstOrDefault(c => c.Name == "External Lights & Indicators")},
                new Subcategory() {Name = "Number Plate Lights", Category = context.Categories.FirstOrDefault(c => c.Name == "External Lights & Indicators")},
                new Subcategory() {Name = "Side Marker Lights", Category = context.Categories.FirstOrDefault(c => c.Name == "External Lights & Indicators")},

                new Subcategory() {Name = "Electronic Ignition Kits", Category = context.Categories.FirstOrDefault(c => c.Name == "Ignition Parts")},
                new Subcategory() {Name = "Spark Plugs", Category = context.Categories.FirstOrDefault(c => c.Name == "Ignition Parts")},

                new Subcategory() {Name = "Control Arms & Parts", Category = context.Categories.FirstOrDefault(c => c.Name == "Suspension & Steering Parts")},
                new Subcategory() {Name = "Steering Columns", Category = context.Categories.FirstOrDefault(c => c.Name == "Suspension & Steering Parts")}

            };

            subcategories.ForEach(s => context.Subcategories.Add(s));
            context.SaveChanges();

            foreach (var product in context.Products)
            {
                context.Products.Remove(product);
            }
            
            var products = new List<Product>()
            {
                new Product() {Name = "Car A/C Refrigeration Air Conditioning AC Diagnostic Manifold Gauge Tool Set RB", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "A/C Compressors & Clutches"), Price = 11.32, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Air Conditioning A/C Delphi Compressor 5N0820803 Seat Skoda VW Repair Fix Kit", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "A/C Compressors & Clutches"), Price = 23.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FORD/AUDI/VW/SEAT/SKODA Air Con Compressor And Clutch A/C Assembly", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "A/C Compressors & Clutches"), Price = 99.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "Heater Control Valve For Ford Fiesta KA Puma Street Transit Mazda 121", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Heater parts"), Price = 11.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "SmartSense Heater/Blower Motor Resistor for Vauxhall/Opel Astra G/H, Zafira-A", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Heater parts"), Price = 9.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "FORD FIESTA MK7 1.25 1.4 1.6 & TDCi 2008-2014 FRONT 2 VENTED BRAKE DISCS & PADS", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Brake Discs"), Price = 44.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "BMW E36 E46 316i-328i REAR DISC BRAKE BACK PLATE RIGHT & LEFT HAND A885", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Brake Discs"), Price = 20.85, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "vauxhall astra vectra corsa front brake disc retaining screw m6 stainless steel", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Brake Discs"), Price = 2.89, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "FORD FOCUS MK2 1.4 1.6 1.8 TDCi REAR 2 BRAKE DRUMS & SHOES SET & FITTING KIT", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Brake Drums"), Price = 89.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "VAUXHALL CORSA C REAR HAND DELPHI BRAKE SHOES 2000 - 2006 1.0 1.2 1.4 1.3 CDTI", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Brake Drums"), Price = 16.60, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "GFord Transit TDCi MK7 SWB FWD Rear Brake Discs Pads with ABS RINGS and SENSORS", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Break Pads"), Price = 69.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "RENAULT CLIO RS 197 200 SPORT (05-12) FRONT BRAKE PAD PIN KIT BREMBO BPF1848D", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Break Pads"), Price = 12.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FORD FIESTA MK6 1.25 1.4 1.6 FRONT BRAKE DISCS & PADS SET 258mm ZETEC 2002-2008", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Break Pads"), Price = 34.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "VOLVO XC90 (2002->2013) HANDBRAKE SHOE ADJUSTER KIT BBA219K", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Break Shoes"), Price = 16.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "VAUXHALL VECTRA C (2002>2008) REAR BRAKE CALIPER RETURN SPRINGS PAIR WHC0139X2A", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Break Shoes"), Price = 8.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "BMW LION 075 027 065 CAR BATTERY 60AH 520 CCA 12V HEAVY DUTY O.E.", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Batteries"), Price = 54.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "RENAULT VAUXHALL VOLVO VOLKAWAGEN (VW) Car / Van Battery TYPE 019 -SuperBatt 019", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Batteries"), Price = 69.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "NEW 150 PSI AIR COMPRESSOR WITH 6 LITER TANK FOR AIR HORN TRAIN TRUCK RV PICK UP", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Horns"), Price = 54.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "12V Loud Dual Tone Twin air Snail Horns Car Fittings Truck Van Boat Universal", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Horns"), Price = 6.69, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "BRAND NEW O.E. SNAIL HOOTER HORN & BRACKET FORD TRANSIT MK6 2000-2006", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Horns"), Price = 8.24, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "10m Red Battery Welding Cable 16mm² 110a - Flexible Marine Boat Automotive Wire", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wires & Cabling"), Price = 27.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Land Rover Discovery 2 TD5 & V8 Indicator Bulb Holder to 02 - Bearmach - XBP180", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wires & Cabling"), Price = 4.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "25mm2 Red Flexible PVC Battery Welding Cable 170 A Amps 1M 1 M Lengths Car Auto", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wires & Cabling"), Price = 3.42, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "25 x LARGE YELLOW FEMALE SPADE CRIMP CONNECTORS Wire Alternator Starter Terminal", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wires & Cabling"), Price = 2.49, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "OEM Siemens VDO Air Flap Motor VW Mk5 Golf Tiguan Audi A3 A4 2.0 TDI 03L128063AF", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Air Bypass Valves"), Price = 127.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Genuine Ford Pass Throttle Air By Valve 4383663", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Air Bypass Valves"), Price = 130.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Genuine Ford Throttle Air By Pass Valve 1030996", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Air Bypass Valves"), Price = 141.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "FIT VW BEETLE GOLF JETTA PASSAT TOUAREG SECONDARY AIR INJECTION PUMP 06A959253B", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Smog & Air Pumps"), Price = 35.80, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "NEW SECONDARY AIR PUMP FOR VW GOLF MK4 BORA PASSAT AUDI A3 A4 A6 TT 078906601D", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Smog & Air Pumps"), Price = 49.80, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "BREATHER UNIT Petrol PCV Pressure Relief Control Valve VW 06F129101N EAP", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Smog & Air Pumps"), Price = 19.39, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "12 Inch 80W Electric Radiator Intercooler 12V Slimline Cooling Fan New", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Fans & Fan Parts"), Price = 16.98, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Universal Electric Radiator Fan Fitting Kit Stainless Steel Cooling Mounting", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Fans & Fan Parts"), Price = 7.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FORD TRANSIT 2.4 RWD MK6 MK7 2000 TO 2014 VISCOUS FAN BLADE COUPLING 4406277", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Fans & Fan Parts"), Price = 45.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Universal Electric Cooling Radiator Fan Ties Fitting Kit Classic & Modern Car", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Fans & Fan Parts"), Price = 3.74, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "HONDA CR-V/CRV 2.0 95-20 RADIATOR PETROL 2 YEAR WARRANTY FOR MANUAL VEHICLES NEW", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Radiators"), Price = 29.40, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "PEUGEOT 206 1.1 1.4 1.6 1.9 2.0 1998>ON AUTOMATIC / MANUAL RADIATOR *BRAND NEW*", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Radiators"), Price = 24.50, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Radiator for Mini Cooper One 1.6 Petrol R50 R53 2001>2006 Brazing 26MM in UK", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Radiators"), Price = 38.80, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "LAND ROVER DEFENDER DISCOVERY 1 300TDI WATER PUMP, GASKET & BOLT SET - PEB500090", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Water Pumps"), Price = 20.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "BRAKE VACUUM WATER PUMP GASKET SET FOR FORD TRANSIT LTI TXII LDV CONVOY 2.4", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Water Pumps"), Price = 6.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FOR BMW E46 328i 328 330Ci 330 PETROL ENGINE COOLANT WATER PUMP MEYLE GERMANY", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Water Pumps"), Price = 26.89, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "MITSUBISHI SHOGUN PAJERO 2.8 DT 3.2 TD WATER PUMP FAI OE QUALITY 1994-06 WP6371", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Water Pumps"), Price = 23.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "Febi Bilstein Timing Chain Kit 33985", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Chains"), Price = 122.21, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Blue Print ADT37311 Injection Pump Belt Kit", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Chains"), Price = 72.80, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "FUEL PRESSURE REGULATOR 93188745/0445010033 FOR RENAULT VAUXHALL 1.9/ 2.2/2.5", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Valves"), Price = 23.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FOR RENAULT TRAFIC 7700109099 OE QUALITY TURBO BOOST PRESSURE SOLENOID VALVE", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Valves"), Price = 18.80, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "NEW! BMW 1 3 5 6 7 SERIES X5 X3 TIMING VANOS SOLENOID VALVE CAMSHAFT ADJUSTMENT", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Valves"), Price = 29.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "NEW GENUINE LEXUS IS220D IS250 IS350 EGR VALVE 25620-26102 4 PC KIT C/W GASKETS", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Valves"), Price = 209.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Exhaust Valve fits TOYOTA CAMRY V1 1.8D 83 to 88 1C-TL AE 1371564040 Quality New", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Valves"), Price = 15.82, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "RENAULT CLIO Mk2 1.2 Inlet Valve 2001 on AE 7701472986 8200343407 Quality New", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Valves"), Price = 12.65, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "VW JETTA Mk2 1.8 Valve Guide 86 to 91 AE 027103415 VOLKSWAGEN Quality New", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Valves"), Price = 6.48, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "120W LED Flood Spot Light Bar Driving Work Lamp Off-Road SUV VAN Boat Truck 12V", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Headlight Assemblies"), Price = 29.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "New Hella 5DV 008 290 00 D2S D2R Xenon Headlight Unit Ballast 5DV00829000", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Headlight Assemblies"), Price = 28.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "NAO H7 380W CREE COB LED Headlight Bulb Car Driving Light Kit Replace Halogen UK", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Headlight Assemblies"), Price = 15.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "9012 HIR2 90W 20000LM LED Headlight Kit Bulb High Low Beam Conversion Kit 6000K", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Headlight Assemblies"), Price = 23.89, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "VW Passat Golf MK3 MK4 Fox Polo T5 Bora Sharan Black Side Indicators Repeaters", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Indicator Assemblies"), Price = 12.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Land Rover discovery 3-4 Smoked Side Indicators and bulbs / Repeater lights", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Indicator Assemblies"), Price = 10.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "Opel Vauxhall 24 SMD LED Licence Number Plate Light Insignia Astra H J Vectra C", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Number Plate Lights"), Price = 12.80, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "LED License Number Plate Light Lamp Bulbs For BMW E39 E60 E82 E70 E90 E92 X3/5/6", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Number Plate Lights"), Price = 11.98, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "2x BMW E46 Canbus Error Free License Number Plate Light 3528 SMD LED Lamp Bulbs", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Number Plate Lights"), Price = 12.25, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "HELLA TYPE LED FLUSH FIT KELSA LIGHT BAR MARKER LAMP LIGHT 12v 24v WHITE LAML003", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Side Marker Lights"), Price = 9.50, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "55W HID Xenon Ballast Conversion H7 Kit Car Headlights Lights Bulbs Super Bright", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Side Marker Lights"), Price = 14.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FORD TRANSIT MK6 MK7 SIDE MARKER LIGHT LAMP LENS 2000-2014 1671689", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Side Marker Lights"), Price = 7.92, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "AccuSpark Distributor cap HT Lead rubber boot set X 5 boots", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Electronic Ignition Kits"), Price = 4.95, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Lumenition Fitting Kit-Lucas 43D6,44D6,45D6,48D6,54D6 Distributors 6 Cylinder", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Electronic Ignition Kits"), Price = 9.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "1x Champion Copper Plus Spark Plug RC12YC", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Spark Plugs"), Price = 2.59, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FOUR ( x4 ) GENUINE NGK SPARK PLUGS NGK7822 / BPR6ES", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Spark Plugs"), Price = 6.94, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "1 x CHAMPION SPARK PLUG Part No RDJ8J New Genuine Champion Sparkplug RDJ8J", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Spark Plugs"), Price = 2.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FOUR ( x4 ) GENUINE NGK SPARK PLUGS XX FREE POSTAGE XX NGK 4548 / CR9EK", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Spark Plugs"), Price = 23.78, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "New! Spark Plug Removal Pliers 300mm to Remove Deep-Seated Plugs Sealey VS867", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Spark Plugs"), Price = 11.44, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "Vauxhall Astra H (04-10) Front Lower Wishbone Suspension Arm Ball Joint - Left", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Control Arms & Parts"), Price = 23.40, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "For NISSAN QASHQAI & +2 07-14 FRONT LOWER WISHBONE ARMS & STABILISER LINK BARS", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Control Arms & Parts"), Price = 64.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "BMW ( 330d e46 ) wishbone ball joint track control arm TC1727 FRONT LOWER LEFT", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Control Arms & Parts"), Price = 35.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "GENUINE FORD ESCORT Mk4 RS TURBO S2 XR3i CABRIO~STEERING COLUMN BEARING & BUSH", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Steering Columns"), Price = 8.80, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "LAND ROVER DEFENDER LOWER STEERING SHAFT UNIVERSAL JOINT UJ - NEW JOINT NRC7704", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Steering Columns"), Price = 12.24, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            string password = "P@ssw0rd";

            foreach (var user in context.Users)
            {
                context.Users.Remove(user);
            }
            var admin = new ApplicationUser()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };

            var usr = new ApplicationUser()
            {
                UserName = "user@user.com",
                Email = "user@user.com"
            };

            var createResult1 = userManager.Create(admin, password);
            var createResult2 = userManager.Create(usr, password);

            var roles = new List<string>
            {
                "Administrator",
                "User"
            };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            if (createResult1.Succeeded)
            {
                userManager.AddToRole(admin.Id, roles[0]);
            }

            if (createResult2.Succeeded)
            {
                userManager.AddToRole(usr.Id, roles[1]);
            }

            foreach (var address in context.DeliveryAddresses)
            {
                context.DeliveryAddresses.Remove(address);
            }

            var addresses = new List<DeliveryAddress>()
            {
                new DeliveryAddress() {DeliveryAddressId = Guid.NewGuid(), City = "City", StreetName = "Street", Postcode = "55-555", UserId = admin.Id },
                new DeliveryAddress() {DeliveryAddressId = Guid.NewGuid(), City = "City2", StreetName = "Street2", Postcode = "66-666", UserId = admin.Id }

            };
            addresses.ForEach(a => context.DeliveryAddresses.Add(a));
            context.SaveChanges();
        }
    }
}
