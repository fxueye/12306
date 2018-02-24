using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306
{
    public class Passenger
    {
        public string first_letter { get; set; }
        public string isUserSelf { get; set; }
        public string mobile_no { get; set; }
        public string old_passenger_id_no { get; set; }
        public string old_passenger_id_type_code { get; set; }
        public string old_passenger_name { get; set; }
        public string passenger_flag { get; set; }
        public string passenger_id_no { get; set; }
        public string passenger_id_type_code { get; set; }
        public string passenger_id_type_name { get; set; }
        public string passenger_name { get; set; }
        public string passenger_type { get; set; }
        public string passenger_type_name { get; set; }
        public string recordCount { get; set; }
    }
    public class PassengerList
    {
        public List<Passenger> passengerJson { get; set; }
    }
}
