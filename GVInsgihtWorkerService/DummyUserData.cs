using System;
using System.Collections.Generic;
using System.Text;

namespace GVInsgihtWorkerService
{
    public class DummyUserData
    {


        // ----------Start For Module Testing---------------------------
        //    Title,
        //    Name, 
        //    Enabled
        //public DummyUserData(String title,String name,Boolean enabled) {

        //    this.Title = title;
        //    this.Name = name;
        //    this.Enabled = enabled;
        //    // For Module Testing
        //}

        //public String Title { get; set; }
        //public String Name { get; set; }
        //public Boolean Enabled { get; set; }
        // ------------End For Module Testing------------------------
        // ----------Start For Property Testing---------------------------
        public DummyUserData(int id,String name, String address, String lat, String lon, Double size,int year)
        {

            
            this.Name = name;
            this.Address = address;
            this.Latitude = lat;
            this.Longitude = lon;
            this.Size = size;
            this.Year = year;
            this.Id = id;

        }
        public int Id { get; set; }
        public String Address { get; set; }
        public String Name { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public Double Size { get; set; }
        public int Year { get; set; }
        // ------------End For Proprety Testing------------------------

        // public DateTime UserActiveDate { get; set; }

    }
}
