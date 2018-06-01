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
                new Category() {Name = "Części karoserii"},
                new Category() {Name = "Ogrzewanie, wentylacja, klimatyzacja"},
                new Category() {Name = "Układ elektryczny, zapłon"},
                new Category() {Name = "Tuning mechaniczny"},
                new Category() {Name = "Układ hamulcowy"},
                new Category() {Name = "Układ kierowniczy"},
                new Category() {Name = "Układ napędowy"},
                new Category() {Name = "Układ paliwowy"},
                new Category() {Name = "Układ zawieszenia"}
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            foreach (var subcategory in context.Subcategories)
            {
                context.Subcategories.Remove(subcategory);
            }
            var subcategories = new List<Subcategory>()
            {
                new Subcategory() {Name = "Maski", Category = context.Categories.FirstOrDefault(c => c.Name == "Części karoserii")},
                new Subcategory() {Name = "Zderzaki", Category = context.Categories.FirstOrDefault(c => c.Name == "Części karoserii")},

                new Subcategory() {Name = "Klimatyzacja", Category = context.Categories.FirstOrDefault(c => c.Name == "Ogrzewanie, wentylacja, klimatyzacja") },
                new Subcategory() {Name = "Ogrzewanie postojowe", Category = context.Categories.FirstOrDefault(c => c.Name == "Ogrzewanie, wentylacja, klimatyzacja") },
                new Subcategory() {Name = "Ogrzewanie", Category = context.Categories.FirstOrDefault(c => c.Name == "Ogrzewanie, wentylacja, klimatyzacja") },
                new Subcategory() {Name = "Wentylacja", Category = context.Categories.FirstOrDefault(c => c.Name == "Ogrzewanie, wentylacja, klimatyzacja") },

                new Subcategory() {Name = "Akumulatory", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ elektryczny, zapłon")},
                new Subcategory() {Name = "Przekaźniki", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ elektryczny, zapłon")},
                new Subcategory() {Name = "Świece", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ elektryczny, zapłon")},

                new Subcategory() {Name = "Układ wydechowy", Category = context.Categories.FirstOrDefault(c => c.Name == "Tuning mechaniczny")},
                new Subcategory() {Name = "Układ zawieszenia", Category = context.Categories.FirstOrDefault(c => c.Name == "Tuning mechaniczny")},

                new Subcategory() {Name = "Czujniki ABS", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ hamulcowy")},
                new Subcategory() {Name = "Czujniki ESP", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ hamulcowy")},
                new Subcategory() {Name = "Pompy hamulcowe", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ hamulcowy")},

                new Subcategory() {Name = "Przekładnie kierownicze", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ kierowniczy")},
                new Subcategory() {Name = "Pompy wspomagania", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ kierowniczy")},

                new Subcategory() {Name = "Dyferencjały", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ napędowy")},
                new Subcategory() {Name = "Mosty", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ napędowy")},
                new Subcategory() {Name = "Skrzynie biegów", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ napędowy")},
                new Subcategory() {Name = "Sprzęgła", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ napędowy")},

                new Subcategory() {Name = "Gaźniki", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ paliwowy")},
                new Subcategory() {Name = "Pompy paliwa", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ paliwowy")},

                new Subcategory() {Name = "Sprężyny zawieszenia", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ zawieszenia")},
                new Subcategory() {Name = "Wahacze", Category = context.Categories.FirstOrDefault(c => c.Name == "Układ zawieszenia")}

            };

            subcategories.ForEach(s => context.Subcategories.Add(s));
            context.SaveChanges();

            foreach (var product in context.Products)
            {
                context.Products.Remove(product);
            }
            
            var products = new List<Product>()
            {
                new Product() {Name = "MASKA PRZOD PRZEDNIA AUDI Q7 S-LINE LIFT 4L0", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Maski"), Price = 2250.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "FIAT 500 KOMPLETNY PRZÓD BENZYNA MASKA ZDERZAK PAS", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Maski"), Price = 3100.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Maska Opel Signum Vectra C 03-05", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Maski"), Price = 250.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "KRATKA ATRAPA ZDERZAKA SEAT TOLEDO LEON 1M PRAWA", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Zderzaki"), Price = 36.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "VW GOLF 4 IV KRATKI ZAŚLEPKI ATRAPY ZDERZAKA PRZÓD", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Zderzaki"), Price = 20.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "CHŁODNICA KLIMATYZACJI PEUGEOT 407 2004-2011 NOWA", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Klimatyzacja"), Price = 195.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "KLIMATYZACJA SAMOCHODOWA KLIMATYZATOR KLIMA 12V", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Klimatyzacja"), Price = 44.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "PRZEWÓD WĄŻ KLIMATYZACJI CITROEN C2 C3 Oryginał", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Klimatyzacja"), Price = 156.50, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "GRZEJNIK WEBASTO DMUCHAWA NAGRZEWNICA 12V 160W", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Ogrzewanie postojowe"), Price = 43.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Nagrzewnica na szybę,grzejnik samochodowy 12V 150W", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Ogrzewanie postojowe"), Price = 39.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "NAGRZEWNICA AUDI A6 C4 94-97 / 100 C2 C3 C4 76-94", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Ogrzewanie"), Price = 29.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "NAGRZEWNICA Audi 80 B3 B4 86-94", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Ogrzewanie"), Price = 34.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "NAGRZEWNICA JEEP GRAND CHEROKEE WJ WG 1999-2004", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Ogrzewanie"), Price = 194.00    , ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "DMUCHAWA WENTYLATOR NAWIEWU Sprinter VW LT 95-06", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wentylacja"), Price = 123.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "WIATRAK WENTYLATOR SAMOCHODOWY OBROTOWY 12V 5'' ", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wentylacja"), Price = 19.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "AKUMULATOR CENTRA FUTURA 77AH 760A NOWY MODEL", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Akumulatory"), Price = 274.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "AKUMULATOR BOSCH SILVER S5 74AH 750A NAJŚWIEŻSZE", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Akumulatory"), Price = 294.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "PRZEKAŹNIK SAMOCHODOWY 12V 70A 4 PIN HONGFA 9,5mm", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Przekaźniki"), Price = 17.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "BERU STEROWNIK ŚWIEC ŻAROWYCH BMW E46 E60 E90 E87 (7222241513)", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Przekaźniki"), Price = 188.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Świece Iskra 01U Golf I II Octavia Passat Polo", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Przekaźniki"), Price = 11.50, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},

                new Product() {Name = "NGK 4 X ŚWIECE PFR6D-10G SAAB 9-3 B207 1.8T 2.0T", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Świece"), Price = 139.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "DENSO ŚWIECA ZAPŁONOWA K16TT BENZYNA LPG", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Świece"), Price = 10.50, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "ŚWIECA ŻAROWA Z CZUJNIKIEM OPEL INSIGNIA 2.0 CDTI", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Świece"), Price = 327.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "N10579202 N10579201 0100226474 0250202046 BOSCH", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Świece"), Price = 24.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "TŁUMIK SPORTOWY BASOWY Z SILENCEREM- MUGEN fi90 M2", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Układ wydechowy"), Price = 139.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Bazaltowa taśma termoizolacyjna TURBO 10m 1.5mm+op", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Układ wydechowy"), Price = 40.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "TŁUMIK SPORTOWY BASOWY WM 2x80 np. BMW VW + OPASKA", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Układ wydechowy"), Price = 214.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "Gwint Gwintowane BMW E46 GOLD EDITION MTS Eibach", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Układ zawieszenia"), Price = 1188.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "GWINT MTS-technik Audi A3 8P VW Golf 5 Scirocco 3", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Układ zawieszenia"), Price = 1244.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Sprężyny z regulacją coilover HONDA civic 88-00r", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Układ zawieszenia"), Price = 259.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "CZUJNIK ABS TYŁ AUDI 80 B3 B4 COUPE 893927807D", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Czujniki ABS"), Price = 125.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "CZUJNIK ABS TYŁ L = P 34526756376 BMW 5 E39", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Czujniki ABS"), Price = 39.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "PIERŚCIEŃ KORONKA ABS TYŁ BMW E46 E81 E82 E87 E90", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Czujniki ABS"), Price = 22.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "CZUJNIK ABS TYŁ PRAWY Renault Megane Scenic", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Czujniki ABS"), Price = 44.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "CZUJNIK CIŚNIENIA ABS ESP 0 265 005 303 BOSCH", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Czujniki ESP"), Price = 119.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "czujnik ciśnienia abs esp 0265005303 Bosch AUDI A6*", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Czujniki ESP"), Price = 99.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "MODUŁ SENSOR ESP GOLF V TOURAN OCTAVIA 1K090755C", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Czujniki ESP"), Price = 30.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "IVECO DAILY 2012 POMPA ABS 5801312794 0265242097", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy hamulcowe"), Price = 700.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Szczęki recznego sprężynki Opel Vectra B ZZ3", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy hamulcowe"), Price = 39.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA ABS IVECO DAILY FIAT DUCATO 1999-2006r", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy hamulcowe"), Price = 600.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Renault Laguna II Pompa ABS", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy hamulcowe"), Price = 120.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "DAEWOO MATIZ Przekładnia kierownicza Maglownica", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Przekładnie kierownicze"), Price = 209.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "BMW E46 Przekładnia kierownicza maglownica SKAWINA", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Przekładnie kierownicze"), Price = 1000.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "POMPA WSPOMAGANIA BMW E36 E38 E39 E46 LUK LF-30", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy wspomagania"), Price = 230.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA WSPOMAGANIA AUDI A4 A6 A8 100 PASSAT B5 nowa", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy wspomagania"), Price = 249.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA WSPOMAGANIA 7D0422155 VW T4 LT 2,4 D 2,5 TDi", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy wspomagania"), Price = 179.90, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Wspomaganie Punto Grande EPS 12m-cy gw FV NOWE", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy wspomagania"), Price = 800.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA WSPOMAGANIA PEUGEOT 407 GWARANCJA 24 MIES.", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy wspomagania"), Price = 600.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA WSPOMAGANIA Ibiza Toledo Golf Sharan Passat", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy wspomagania"), Price = 158.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA WSPOMAGANIA ELEK. PEUGEOT 407 GW.24mc -200zł", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy wspomagania"), Price = 650.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "iveco dyferencjał wkład mostu mechanizm różnicowy", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Dyferencjały"), Price = 299.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "KOŁO ZĘBATKA REDUKTORA SKRZYNI BMW E83 E53 E70 E71", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Dyferencjały"), Price = 65.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "MERCEDES SPRINTER 906-VW CRAFTER DYFER TYŁ 46:11", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Dyferencjały"), Price = 1900.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "RENAULT MAGNUM DXI MOST MS17X MERITOR 2.85 / 2.64", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Dyferencjały"), Price = 6765.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "Iveco daily most napędowy dyfer 2006- bliźniak 35C", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Mosty"), Price = 2500.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Tylny most dyferencjał mercedes 124,190 Gwar 12Mcy", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Mosty"), Price = 990.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "ELEKTROZAWÓR USZCZELKI SKRZYNI AUTOMAT RENAULT DP0", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Skrzynie biegów"), Price = 267.98, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "MISKA OLEJOWA FILTR SKRZYNI USZCZELKA ZF BMW 6HP26", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Skrzynie biegów"), Price = 349.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "OLEJ ZF LIFEGUARD FLUID 6 MISKA TULEJA SERWIS BMW", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Skrzynie biegów"), Price = 899.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "SPRZĘGŁO DAEWOO TICO MATIZ KOMPLETNE NOWE", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Sprzęgła"), Price = 139.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "KOŁO DWUMASOWE SPRZĘGŁO SACHS AUDI A4 B8 2.0 TDI", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Sprzęgła"), Price = 2199.50, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "ŻUK NYSA UAZ GAZ69 WARSZAWA TARCZA SPRZĘGŁA TANIO!", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Sprzęgła"), Price = 52.50, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "PŁYWAK GAZNIKA FIAT 126P MALUCH", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Gaźniki"), Price = 11.99, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "PODUSZKA GAŹNIKA VW PASSAT 79- GOLF II JETTA II", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Gaźniki"), Price = 72.70, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "RĘCZNA POMPKA POMPA PALIWA GRUSZKA DO OLEJU 8mm", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy paliwa"), Price = 13.45, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA DO PALIWA 12V ELEKTRYCZNA 30l/min", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy paliwa"), Price = 46.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "Vacum Pompa 2.0TDI 03G145209C D Gwarancja!", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy paliwa"), Price = 200.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "POMPA PALIWA VACUM AUDI SEAT VW SKODA 1.9TDI BOSCH", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy paliwa"), Price = 580.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "VW SEAT SKODA 1.9SDI 1.9TDI vacum pompa 038145101B", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Pompy paliwa"), Price = 120.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "SPRĘŻYNY TYŁ VW TRANSPORTER T5 MULTIVAN 2003-", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Sprężyny zawieszenia"), Price = 71.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "ZAWIESZENIE GWINTOWANE GWINT BMW E46 98-06R 2 4D", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Sprężyny zawieszenia"), Price = 799.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "SPRĘŻYNY TYŁ FORD MONDEO MK3 KOMBI 00-07 WZMACNIANE", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Sprężyny zawieszenia"), Price = 66.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                
                new Product() {Name = "WAHACZE ŁĄCZNIKI KOŃC MASTER SPORT PEUGEOT 206", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wahacze"), Price = 225.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
                new Product() {Name = "WAHACZ DOLNY PRAWY AUDI A4 A6 A8 VW PASSAT B5 SKV", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Wahacze"), Price = 85.00, ItemsAvailable = (random.Next(0,5) != 0)?random.Next(1, 20):0},
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
                new DeliveryAddress() {DeliveryAddressId = Guid.NewGuid(), City = "Rzeszow", StreetName = "al. Powstańców Warszawy 12", Postcode = "35-959", UserId = admin.Id },
                new DeliveryAddress() {DeliveryAddressId = Guid.NewGuid(), City = "Rzeszow", StreetName = "Wincentego Pola 2", Postcode = "35-021", UserId = admin.Id }

            };
            addresses.ForEach(a => context.DeliveryAddresses.Add(a));
            context.SaveChanges();
        }
    }
}
