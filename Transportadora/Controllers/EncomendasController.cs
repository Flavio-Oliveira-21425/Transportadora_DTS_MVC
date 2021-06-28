using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transportadora.Data;
using Transportadora.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Transportadora.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EncomendasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Encomendas
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Encomendas"))
            {
                var applicationDbContext = _context.Encomendas.Include(f => f.Cliente);
                return View(await applicationDbContext.ToListAsync());
            }


            var cliente = _context.Encomendas.Include(f => f.Cliente).Where(c => c.Cliente.UserNameId == _userManager.GetUserId(User));
            return View(await cliente.ToListAsync());
        }

        // GET: Encomendas/Details/5
        /// <summary>
        /// Mostra os detalhes de uma Encomenda, tais como:
        /// o nome do cliente, o assunto, a descrição e a data do dia submetido.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Encomendas = await _context.Encomendas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.Id_encomenda == id);
            if (Encomendas == null)
            {
                return NotFound();
            }

            return View(Encomendas);
        }

        // GET: Encomendas/Create
        /// <summary>
        /// Cria uma Encomenda com os seguintes campos diponiveis:
        /// nome do cliente, assunto, descrição e a data.
        /// </summary>
        public IActionResult Create()
        { 
            return View();
        }

        // POST: Encomendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_encomendas,Nome,Tipo,Descricao,Estado,Morada,CodPostal,DataEnvio,DataEntrega,Altura,Largura,Comprimento,Peso,IdCliente,ListaFuncionarios")] Encomendas Encomendas)
        {

            
            if (ModelState.IsValid)
            {
                var Cliente = _context.Clientes.FirstOrDefault(m => m.UserNameId == _userManager.GetUserId(User));
                Encomendas.IdCliente = Cliente.Id_cliente;
                _context.Add(Encomendas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(Encomendas);
        }

        // GET: Encomendas/Edit/5
        /// <summary>
        /// Edita um formulário com os seguintes campos disponiveis:
        /// nome do cliente, assunto, descrição e a data.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Encomendas = await _context.Encomendas.FindAsync(id);
            if (Encomendas == null)
            {
                return NotFound();
            }
           
            return View(Encomendas);
        }

        // POST: Encomendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_encomendas,Nome,Tipo,Descricao,Estado,Morada,CodPostal,DataEnvio,DataEntrega,Altura,Largura,Comprimento,Peso,IdCliente,ListaFuncionarios")] Encomendas Encomendas)
        {
            if (id != Encomendas.Id_encomenda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Encomendas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncomendasExists(Encomendas.Id_encomenda))
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
            
            return View(Encomendas);
        }

        // GET: Encomendas/Delete/5
        /// <summary>
        /// Elimina uma Encomenda.
        /// </summary>
        [Authorize(Roles = "funcionario")] // apenas o funcionário pode eliminar Encomendas
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Encomendas = await _context.Encomendas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.Id_encomenda == id);
            if (Encomendas == null)
            {
                return NotFound();
            }

            return View(Encomendas);
        }

        // POST: Encomendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "funcionario")] // apenas o funcionário pode eliminar Encomendas
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Encomendas = await _context.Encomendas.FindAsync(id);
            _context.Encomendas.Remove(Encomendas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncomendasExists(int id)
        {
            return _context.Encomendas.Any(e => e.Id_encomenda == id);
        }
    }
}
