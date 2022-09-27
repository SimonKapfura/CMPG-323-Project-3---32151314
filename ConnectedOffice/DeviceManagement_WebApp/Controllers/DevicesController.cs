using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]//Adds security sothat only people who are logged in can access the site contents
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        public DevicesController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        // GET: Devices
        public IActionResult Index()
        {
            var deviceRepository = _deviceRepository.GetAllDevices();//gets all the devices
            return View(deviceRepository.ToList());
        }

        // GET: Devices/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var device = _deviceRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_deviceRepository.GetContext().Category, "CategoryId", "CategoryName");//uses the context to create the dropdowns with information from the the category table
            ViewData["ZoneId"] = new SelectList(_deviceRepository.GetContext().Zone, "ZoneId", "ZoneName");//uses the context to create the dropdowns with information from the the zone table
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _deviceRepository.Add(device);//adds a new device
            _deviceRepository.Save();//saves the newly added device
            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _deviceRepository.GetById(id);//gets device that will be edited by id
            if (device == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_deviceRepository.GetContext().Category, "CategoryId", "CategoryName", device.CategoryId);//uses the context to create the dropdowns with information from the the category table
            ViewData["ZoneId"] = new SelectList(_deviceRepository.GetContext().Zone, "ZoneId", "ZoneName", device.ZoneId);//uses the context to create the dropdowns with information from the the zone table
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }
            try
            {
                _deviceRepository.Edit(device);//edits the existing device and saves the newly edited changes
                _deviceRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Devices/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _deviceRepository.GetDeviceById(id);//Uses Id to get the device that will be deleted
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var device = _deviceRepository.GetById(id);//Uses Id to get the device that will be deleted
            _deviceRepository.Remove(device);//deletes the sppecified device and saves changes
            _deviceRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(Guid id)
        {
            //checks if the specified id exists and returns true if it exists and false if it does not exist
            var foundID = _deviceRepository.Find(a => a.CategoryId == id);

            if (foundID != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}