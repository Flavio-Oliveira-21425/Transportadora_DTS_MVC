using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transportadora.Data;
using Transportadora.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Transportadora.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        public ClientesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        /// <summary>
        /// Mostra os dados de um cliente, acedendo aos dados relativos a ele,
        /// associados a cada conta de utilizador
        /// </summary>
        /// <param name="id">identificador do cliente a apresentar os detalhes</param>
        /// <returns></returns>
        [Authorize(Roles = "funcionario, cliente")] // ambos os funcionários e os clientes autenticados têm acesso a esta informação
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return LocalRedirect("~/");
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id_cliente == id);
            if (clientes == null)
            {
                return LocalRedirect("~/");
            }

            if (User.IsInRole("funcionario") || clientes.UserNameId == _userManager.GetUserId(User))
            {
                return View(clientes);
            }

            return LocalRedirect("~/");
        }


        // GET: Clientes/Profile/5
        /// <summary>
        /// Mostra os dados de um cliente, acedendo aos dados relativos a ele,
        /// associados a cada conta de utilizador
        /// </summary>
        /// <param name="id">identificador do cliente a apresentar os detalhes</param>
        /// <returns></returns>
        [Authorize(Roles = "cliente")] // apenas clientes autenticados têm acesso a esta informação
        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return LocalRedirect("~/");
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.UserNameId == id);
            if (clientes == null)
            {
                return LocalRedirect("~/");
            }

            if (User.IsInRole("funcionario") || clientes.UserNameId == _userManager.GetUserId(User))
            {
                return View(clientes);
            }

            return LocalRedirect("~/");
        }


        // GET: Clientes/Edit/5
        /// <summary>
        /// Edita os dados de um cliente
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_cliente,Nome,Email,Contacto,Morada,CodPostal,NIF,UserNameId")] Clientes clientes)
        {
            if (id != clientes.Id_cliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientes.Id_cliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (User.IsInRole("cliente"))
                    return RedirectToAction("Details", new { id = clientes.UserNameId });

                return RedirectToAction(nameof(Index));
            }


            return View(clientes);
        }

        // GET: Clientes/Delete/5
        /// <summary>
        /// Elimina os dados de um cliente
        /// </summary>
        [Authorize(Roles = "funcionario")] // apenas o funcionário pode eliminar dados de um cliente
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id_cliente == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [Authorize(Roles = "funcionario")] // apenas o funcionário pode eliminar dados de um cliente

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id_cliente == id);
        }
    }
}
