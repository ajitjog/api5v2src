using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi5V2.Model
{
    public class City
    {
        public int CityNo { get; set; }
        public string CityName { get; set; }
        public int CityDensity { get; set; }
    }

    public static class CitiesRepository
    {
        private static List<City> cl = new List<City>()
        {
            new City(){ CityNo=1,CityName="Mumbai",CityDensity=3000 },
            new City(){ CityNo=2,CityName="Delhi",CityDensity=7000 },
            new City(){ CityNo=3,CityName="Bangalore",CityDensity=12000 },
            new City(){ CityNo=4,CityName="Pune",CityDensity=9000 }

        };

        public static List<City> GetAll()
        {
            return cl;
        }

        public static City Get(int ctno)
        {
            return cl.SingleOrDefault(ct => ct.CityNo == ctno);
        }


        public static void Add(City ct)
        {
            cl.Add(ct);
        }



    }
}
