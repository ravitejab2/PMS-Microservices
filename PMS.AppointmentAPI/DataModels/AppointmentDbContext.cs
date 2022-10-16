using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.AppointmentAPI.DataModels
{
    public class AppointmentDbContext:DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DBConnection"));
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SlotTimings>().HasData(
                new SlotTimings { SlotId=1,SlotStart=new TimeSpan(10,00,00),SlotEnd =new TimeSpan(11, 00, 00), SlotTiming="10AM - 11AM" },
                new SlotTimings { SlotId = 2, SlotStart = new TimeSpan(11, 00, 00), SlotEnd = new TimeSpan(12, 00, 00), SlotTiming = "11 AM - 12 PM" },
                new SlotTimings { SlotId = 3, SlotStart = new TimeSpan(12, 00, 00), SlotEnd = new TimeSpan(13, 00, 00), SlotTiming = "12 PM - 1 PM" },
                new SlotTimings { SlotId = 4, SlotStart = new TimeSpan(13, 00, 00), SlotEnd = new TimeSpan(14, 00, 00), SlotTiming = "1 PM - 2PM" },
                new SlotTimings { SlotId = 5, SlotStart = new TimeSpan(14, 00, 00), SlotEnd = new TimeSpan(15, 00, 00), SlotTiming = "2 PM - 3 PM" },
                new SlotTimings { SlotId = 6, SlotStart = new TimeSpan(15, 00, 00), SlotEnd = new TimeSpan(16, 00, 00), SlotTiming = "3 PM - 4 PM" },
                new SlotTimings { SlotId = 7, SlotStart = new TimeSpan(16, 00, 00), SlotEnd = new TimeSpan(17, 00, 00), SlotTiming = "4 PM - 5 PM" }
                

            );
        }

        public DbSet<AppointmentModel> Appointments { get; set; }
        
        public DbSet<SlotTimings> Slots { get; set; }

    }
}
