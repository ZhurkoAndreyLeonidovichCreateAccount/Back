using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager; 
            _roleManager = roleManager; 
        }

        public  void Initialize()
        {
            _context.Database.EnsureCreated();

            if (!_context.Roles.Any())
            {
                var roleAdmin = new IdentityRole()
                {
                    NormalizedName = "admin",
                    Name = "admin"

                };

                var roleUser = new IdentityRole()
                {
                    Name = "user",
                    NormalizedName = "user",
                };

                //Create roles
                _roleManager.CreateAsync(roleAdmin).GetAwaiter().GetResult();
                _roleManager.CreateAsync(roleUser).GetAwaiter().GetResult();

                if (!_context.Users.Any())
                {
                    var user = new ApplicationUser()
                    {
                        UserName = "user@mail.ru",
                        Email = "user@mail.ru"
                    };
                    var resultUser =  _userManager.CreateAsync(user, "123456").GetAwaiter().GetResult();
                    if (resultUser.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, "user").GetAwaiter().GetResult();
                    }

                    var admin = new ApplicationUser()
                    {
                        UserName = "admin@mail.ru",
                        Email = "admin@mail.ru"
                    };

                    var resultAdmin = _userManager.CreateAsync(admin, "123456").GetAwaiter().GetResult();
                    if (resultAdmin.Succeeded)
                    {
                         _userManager.AddToRoleAsync(admin, "admin").GetAwaiter().GetResult(); 
                    }


                }
            }



            if (!_context.Classics.Any())
            {
                _context.AddRange(new List<Classic>
                {
                    new Classic()
                    {
                        EventName = "Праздничный концерт ко дню открытия Белорусской государственной филармонии",
                        EventType = "Classic",
                        NamePerformer= "Владислав Витушко",
                        Image="Музыкальная капела.jpg",
                        Voicetype="баритон",
                        DateConcert=new DateTime(2022,04,30),
                        LocationConcert="Минск, проспект Независимости, 50",
                        AmountOfTickets=20,
                        //NameOfConcert = "Праздничный концерт ко дню открытия Белорусской государственной филармонии",
                        Tickets=new List<Ticket>()
                        {
                            new Ticket(){Price=20, Sector="Балкон", booked=false},
                            new Ticket(){Price=30, Sector="Балкон", booked=false},
                            new Ticket(){Price=25, Sector="Партер", booked=false},
                            new Ticket(){Price=40, Sector="Партер", booked=false}
                        }

                    },

                     new Classic()
                    {
                        EventName = "Музыкальная капелла Сонорус",
                        EventType = "Classic",
                        NamePerformer= " Александр Данилов",
                        Image="ПраздничКонцерт.jpg",
                        Voicetype="альт",
                        DateConcert=new DateTime(2022,05,01),
                        LocationConcert="Минск, проспект Независимости, 50",
                        AmountOfTickets=10,
                        //NameOfConcert = "Музыкальная капелла Сонорус",
                        Tickets=new List<Ticket>()
                        {
                            new Ticket(){Price=40, Sector="Балкон", booked=false},
                            new Ticket(){Price=35, Sector="Балкон", booked=false},
                            new Ticket(){Price=45, Sector="Партер", booked=false},
                            new Ticket(){Price=20, Sector="Партер", booked=false}
                        }

                    },
                });
            }

            if (!_context.OpenAirs.Any())
            {
                _context.AddRange(new List<OpenAir>()
                {
                    new OpenAir()
                    {
                        EventName = "РОК ЗА БОБРОВ 2020",
                        EventType = "OpenAir",
                        NamePerformer= "LITTLE BIG, ЗВЕРИ, LOUNA, NOIZE MC, J:МОРС, ПОШЛАЯ МОЛЛИ",
                        Image="Рок за бобров.jpg",
                        DateConcert=new DateTime(2022,05,07),
                        LocationConcert="МИНСК, АЭРОДРОМ ЛИПКИ",
                        AmountOfTickets=1000,
                        HowToGet="  10 минут езды от станции метро «Борисовский тракт».",
                        Headliner="LITTLE BIG",

                         Tickets=new List<Ticket>()
                        {
                            new Ticket(){Price=40, Sector="A", booked=false},
                            new Ticket(){Price=35, Sector="A", booked=false},
                            new Ticket(){Price=45, Sector="B", booked=false},
                            new Ticket(){Price=20, Sector="B", booked=false}
                        }
                       

                    },

                    new OpenAir()
                    {
                        
                        EventName = "ВИВА БРАСЛАВ",
                        EventType = "OpenAir",
                        NamePerformer= "БИ-2, Океан Эльзы, Ляпис 98, Сплин, Баста, ЛСП, Мумий Тролль, Дай Дорогу",
                        Image="Вива Браслов.jpg",
                        DateConcert=new DateTime(2022,05,07),
                        LocationConcert="БРАСЛАВ",
                        AmountOfTickets=1000,
                        
                        HowToGet="  Национальный парк «Браславские озера» на северо-западе страны.",
                        Headliner="БИ-2",
                        Tickets=new List<Ticket>()
                        {
                            new Ticket(){Price=40, Sector="A", booked=false},
                            new Ticket(){Price=35, Sector="A", booked=false},
                            new Ticket(){Price=45, Sector="B", booked=false},
                            new Ticket(){Price=20, Sector="B", booked=false}
                        }

                    }

                });


            }

            if (!_context.OpenAirs.Any())
            {
                _context.AddRange(new List<Party>()
                {
                    new Party()
                    {

                          EventName = "Просто ретро",
                          EventType = "Party",
                          NamePerformer = "Dj",
                          Age = 18,
                          DateConcert = new DateTime(2022,06,08),
                          LocationConcert = "Минск, ул. аэрадромная",
                          AmountOfTickets = 100,
                          Image = "Ретро.jpg",
                          Tickets=new List<Ticket>()
                        {
                            new Ticket(){Price=40, Sector="1", booked=false},
                            new Ticket(){Price=35, Sector="1", booked=false},
                            new Ticket(){Price=45, Sector="2", booked=false},
                            new Ticket(){Price=20, Sector="2", booked=false}
                        }



                    },

                     new Party()
                    {
                          EventName = "Бруклин",
                          EventType = "Party",
                          NamePerformer = "MC",
                          Age = 21,
                          DateConcert = new DateTime(2022,07,08),
                          LocationConcert = "Минск, проспект независимости 20",
                          AmountOfTickets = 100,
                          Image = "Бруклин.jpg",
                          Tickets=new List<Ticket>()
                        {
                            new Ticket(){Price=40, Sector="1", booked=false},
                            new Ticket(){Price=35, Sector="1", booked=false},
                            new Ticket(){Price=45, Sector="2", booked=false},
                            new Ticket(){Price=20, Sector="2", booked=false}
                        }


                    }
                });
            }
             _context.SaveChanges();
        }
    }
}
