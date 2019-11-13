using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenFinal.Data;
using ExamenFinal.Models;

namespace ExamenFinal.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,Nombre,Descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,Nombre,Descripcion")] Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.IdCategoria))
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
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.IdCategoria == id);
        }

        [HttpPost]
        public ActionResult EditField(int id, string field, string value)
        {
            bool status = false; string mensaje = "Valor no establecido";
            Categoria categoria = (from a in _context.Categoria
                                     where a.IdCategoria == id
                                     select a).FirstOrDefault();
            switch (field)
            {
                case "Nombre de Categoria":
                    categoria.Nombre = value.Trim();
                    break;
                case "Descripcion":
                    categoria.Descripcion = value.Trim();
                    break;


            }
            _context.SaveChanges();
            status = true;
            mensaje = "Valor establecido";
            return Json(new { value = value, status = status, mensaje = mensaje });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConAjax(Categoria categoria)
        {
            string mensaje = "Error al crear el registro";
            if (ModelState.IsValid)
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                mensaje = "Registro creado";
            }
            return Json(new { result = false, mensaje = mensaje });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConAjax(Categoria categoria)
        {
            string mensaje = "Error al editar el registro";
            if (ModelState.IsValid)
            {
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                mensaje = "Registro editado";
            }
            return Json(new { result = false, mensaje = mensaje });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConAjax(Categoria categoria)
        {
            string mensaje = "Error al borrar registro";
            Categoria categoriaFind = _context.Categoria.Find(categoria.IdCategoria);
            _context.Categoria.Remove(categoriaFind);
            _context.SaveChanges();
            mensaje = "Registro borrado!";
            return Json(new { result = true, mensaje = mensaje });
        }
    }

}
