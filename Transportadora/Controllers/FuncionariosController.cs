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
using Microsoft.AspNetCore.Hosting;

namespace Transportadora.Controllers
{
    [AllowAnonymous]
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _Usermanager;
        private readonly IWebHostEnvironment _caminho;


        public FuncionariosController(ApplicationDbContext context, UserManager<IdentityUser> Usermanager, IWebHostEnvironment caminho)
        {
            _context = context;
            _Usermanager = Usermanager;
            _caminho = caminho;
        }

        // GET: Funcionarios
        [Authorize(Roles = "funcionario")]
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.ToListAsync());


        }

        // GET: Funcionarios/Details/5
        /// <summary>
        /// Mostra os dados de um funcionario, acedendo aos dados relativos a ele.
        /// </summary>
        /// <param name="id">identificador do funcionário a apresentar os detalhes</param>
        /// <returns></returns>
        [Authorize(Roles = "funcionario")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return LocalRedirect("~/");
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id_funcionario == id);
            if (funcionario == null)
            {
                return LocalRedirect("~/");
            }

            return View(funcionario);
        }


        // GET: Funcionarios/Profile/5
        /// <summary>
        /// Mostra os dados de um funcionario, acedendo aos dados relativos a ele.
        /// </summary>
        /// <param name="id">identificador do funcionário a apresentar os detalhes</param>
        /// <returns></returns>
        [Authorize(Roles = "funcionario")]
        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return LocalRedirect("~/");
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.UserNameId == id);
            if (funcionario == null)
            {
                return LocalRedirect("~/");
            }

            return View(funcionario);
        }


        // GET: Funcionarios/Create
        /// <summary>
        /// Cria funcionarios
        /// </summary>
        public IActionResult Create()
        {
            return Redirect("~/Identity/Account/Registerf");
        }

        // GET: Funcionarios/Edit/5
        /// <summary>
        /// Edita os dados de um funcionário
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios.FindAsync(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_funcionario,Nome,Cargo,Email,Contacto,Morada,CodPostal,NIF,UserNameId")] Funcionarios funcionarios)
        {
            if (id != funcionarios.Id_funcionario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionariosExists(funcionarios.Id_funcionario))
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
            return View(funcionarios);
        }

        // GET: Funcionarios/Delete/5
        /// <summary>
        /// Elimina os dados de um funcionário
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id_funcionario == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionarios = await _context.Funcionarios.FindAsync(id);
            _context.Funcionarios.Remove(funcionarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionariosExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id_funcionario == id);
        }
    }
}
