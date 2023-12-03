using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.am.Models;
using MyContacts.am.ModelsView;
using System.Net;
using System.Numerics;

namespace MyContacts.am.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ClientContext _db;

        public ContactsController(ClientContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region One Contact

        [HttpGet]
        public async Task<IActionResult> OneContacts(int Id)
        {
            var contacts = await _db.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();

            return View(contacts);
        }

        #endregion

        #region All Contacts

        [HttpGet]
        public async Task<IActionResult> AllContacts()
        {
            var allUsersContacts = await _db.Users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    FullName = u.Name, 
                    Email = u.Email,
                    Address = u.Address,
                    Phone = u.Phone,
                }).ToListAsync();

            return View(allUsersContacts);
        }

        #endregion

        #region Add Contact

        [HttpGet]
        public async Task<IActionResult> AddContacts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts(UserModel user)
        {
            if (ModelState.IsValid)
            {
                User contact = new User();
                contact.Name = user.FullName;
                contact.Email = user.Email;
                contact.Address = user.Address;
                contact.Phone = user.Phone;

                await _db.Users.AddAsync(contact);
                await _db.SaveChangesAsync();

                return RedirectToAction("AllContacts");
            }

            return View(user);
        }
        #endregion

        #region Edit Contact

        [HttpGet]
        public async Task<IActionResult> EditContacts(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var user = await _db.Users.FindAsync(Id);

            var userModel = new UserModel();
            userModel.Id = user.Id;
            userModel.FullName = user.Name;
            userModel.Email = user.Email;
            userModel.Address = user.Address;
            userModel.Phone = user.Phone;

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditContacts(UserModel user)
        {
            User contact = await _db.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();
            
            if (ModelState.IsValid && contact != null)
            {
                contact.Name = user.FullName;
                contact.Email = user.Email;
                contact.Address = user.Address;
                contact.Phone = user.Phone;
                _db.Entry(contact).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("AllContacts");
            }

            return View(user);
        }

        #endregion

        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteContacts(int? contactId)
        {
            var contact = _db.Users.Find(contactId);
            if (contact == null)
            {
                return NotFound();
            }
            _db.Users.Remove(contact);
            _db.SaveChanges();
            return RedirectToAction("AllContacts");
        }

        #endregion
    }
}
