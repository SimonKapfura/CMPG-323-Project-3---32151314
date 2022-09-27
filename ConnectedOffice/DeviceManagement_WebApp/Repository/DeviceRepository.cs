using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        {
        }
        //get an entity from dbcontext by id
        public Device GetDeviceById(Guid? id)
        {
            var device = _context.Set<Device>()
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefault(m => m.DeviceId == id);
            return device;
        }
        //get all from dbcontext
        public IEnumerable<Device> GetAllDevices()
        {
            var devices = _context.Set<Device>()
                .Include(d => d.Category)
                .Include(d => d.Zone);
            return devices;
        }
    }
}
