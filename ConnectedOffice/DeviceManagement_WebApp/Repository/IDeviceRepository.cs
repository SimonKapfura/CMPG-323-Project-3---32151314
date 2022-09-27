using DeviceManagement_WebApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Device GetDeviceById(Guid? id);
        IEnumerable<Device> GetAllDevices();
    }
}
