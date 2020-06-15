using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebGGSignal.Models
{
    public class ReadingMSSQLModel
    {
        //MSSQL
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required")]
        public string DeviceID { get; set; }
        public string DeviceType { get; set; }
        public string BuildingID { get; set; }
        public string NoOfConnectedSensor { get; set; }
        public string ConnectedSensorID { get; set; }
        public string ConnectedToStationID { get; set; }
        public string ConnectedToSensorID { get; set; }
        public DateTime LastPairingDate { get; set; }
        public string FloorNum { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }

    }

    public class SQLDBContext : DbContext
    {
        public DbSet<ReadingMSSQLModel> SignalTest { get; set; }
    }

}
