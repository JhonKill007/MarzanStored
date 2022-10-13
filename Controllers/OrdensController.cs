using MarzanStored.Core.Inteface;
using MarzanStored.Data;
using MarzanStored.Dtos;
using MarzanStored.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Ordenes.Controllers
{
  public class OrdensController : Controller
  {
    private readonly ApplicationDbContext _context;
    private SignInManager<IdentityUser> SignInManager;
    private IOrdenRepository _repository;

    public List<Productos> vm = new List<Productos>();

    public OrdensController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, IOrdenRepository repository)
    {
      _context = context;
      SignInManager = signInManager;
      _repository = repository;
    }

    // GET: Ordens
    [Authorize]
    public async Task<IActionResult> Index()

    {

      SignInManager.IsSignedIn(User);
      var name = User.Identity.Name;
      return View(await _repository.GetOrdenes(name));
    }

    // GET: Ordens/Details/5
    [Authorize]

    public async Task<IActionResult> Details(int? id)
    {
      return View();
    }

    // GET: Ordens/Create
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
      /*await _repository.GetProductos();*/
      vm = await _context.Productos.ToListAsync();

      List<SelectListItem> nombres = vm.ConvertAll(d =>
      {
        return new SelectListItem()
        {
          Text = d.Nombre.ToString(),
          Value = d.Id.ToString(),
          Selected = false
        };
      });

      ViewBag.nombres = nombres;
      return PartialView("Partials/_CreateOrdenPartialView");
    }

    // POST: Ordens/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Create(Orden orden)
    {
      if (ModelState.IsValid)
      {
        _context.Add(orden);
        await _context.SaveChangesAsync();
        await _repository.UpdateProducto(orden.IdProductos, orden.Cantidad);
        return RedirectToAction(nameof(Index));
      }
      return RedirectToAction(nameof(Index));
    }

    // GET: Ordens/Edit/5
    [Authorize]

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var orden = await _context.Ordenes.FindAsync(id);
      if (orden == null)
      {
        return NotFound();
      }
      return View(orden);
    }

    // POST: Ordens/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]

    public async Task<IActionResult> Edit(int id, [Bind("Id,IdCliente,Fecha")] Orden orden)
    {
      if (id != orden.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(orden);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!OrdenExists(orden.Id))
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
      return View(orden);
    }

    // GET: Ordens/Delete/5
    [Authorize]

    public async Task<IActionResult> Search(string search)
    {
      SignInManager.IsSignedIn(User);
      var name = User.Identity.Name;
      var searchOrden = _repository.SearchOrden(search, name);
      ViewBag.Orders = searchOrden;
      return View(searchOrden);
    }

    // POST: Ordens/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize]

    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var orden = await _context.Ordenes.FindAsync(id);
      _context.Ordenes.Remove(orden);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool OrdenExists(int id)
    {
      return _context.Ordenes.Any(e => e.Id == id);
    }
  }
}
