using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //gets entities linked to device from the database by id
        public Device GetDeviceById(Guid? id)
        {
            var device = _context.Set<Device>()
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefault(m => m.DeviceId == id);
            return device;
        }
        //get all the devices from the database with its likked entities
        public IEnumerable<Device> GetAllDevices()
        {
            var devices = _context.Set<Device>()
                .Include(d => d.Category)
                .Include(d => d.Zone);
            return devices;
        }
        //returns context to make it easier to reference the context when getting data from other entities
        public ConnectedOfficeContext GetContext()
        {
            return _context;
        }
    }
}
