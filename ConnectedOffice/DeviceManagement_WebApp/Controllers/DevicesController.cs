using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
<<<<<<< Updated upstream
=======
using DeviceManagement_WebApp.Repository;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]//Adds security sothat only people who are logged in can access the site contents
    public class DevicesController : Controller
    {
        private readonly ConnectedOfficeContext _context;

        public DevicesController(ConnectedOfficeContext context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
<<<<<<< Updated upstream
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return View(await connectedOfficeContext.ToListAsync());
=======
            var deviceRepository = _deviceRepository.GetAllDevices();//gets all the devices
            return View(deviceRepository.ToList());
>>>>>>> Stashed changes
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

<<<<<<< Updated upstream
            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
=======
            var device = _deviceRepository.GetById(id);//Gets a device specified by the id
>>>>>>> Stashed changes
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
<<<<<<< Updated upstream
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName");
=======
            ViewData["CategoryId"] = new SelectList(_deviceRepository.GetContext().Category, "CategoryId", "CategoryName");//uses the context to create the dropdowns with information from the the category table
            ViewData["ZoneId"] = new SelectList(_deviceRepository.GetContext().Zone, "ZoneId", "ZoneName");//uses the context to create the dropdowns with information from the the zone table
>>>>>>> Stashed changes
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
<<<<<<< Updated upstream
            _context.Add(device);
            await _context.SaveChangesAsync();
=======
            _deviceRepository.Add(device);//adds a new device
            _deviceRepository.Save();//saves the newly added device
>>>>>>> Stashed changes
            return RedirectToAction(nameof(Index));


        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

<<<<<<< Updated upstream
            var device = await _context.Device.FindAsync(id);
=======
            var device = _deviceRepository.GetById(id);//gets device that will be edited by id
>>>>>>> Stashed changes
            if (device == null)
            {
                return NotFound();
            }
<<<<<<< Updated upstream
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName", device.ZoneId);
=======
            ViewData["CategoryId"] = new SelectList(_deviceRepository.GetContext().Category, "CategoryId", "CategoryName", device.CategoryId);//uses the context to create the dropdowns with information from the the category table
            ViewData["ZoneId"] = new SelectList(_deviceRepository.GetContext().Zone, "ZoneId", "ZoneName", device.ZoneId);//uses the context to create the dropdowns with information from the the zone table
>>>>>>> Stashed changes
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }
            try
            {
<<<<<<< Updated upstream
                _context.Update(device);
                await _context.SaveChangesAsync();
=======
                _deviceRepository.Edit(device);//edits the existing device and saves the newly edited changes
                _deviceRepository.Save();
>>>>>>> Stashed changes
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

<<<<<<< Updated upstream
            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
=======
            var device = _deviceRepository.GetDeviceById(id);//Uses Id to get the device that will be deleted
>>>>>>> Stashed changes
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
<<<<<<< Updated upstream
            var device = await _context.Device.FindAsync(id);
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
=======
            var device = _deviceRepository.GetById(id);//Uses Id to get the device that will be deleted
            _deviceRepository.Remove(device);//deletes the sppecified device and saves changes
            _deviceRepository.Save();
>>>>>>> Stashed changes
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }
    }
}
