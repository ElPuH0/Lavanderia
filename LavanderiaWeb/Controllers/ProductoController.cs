using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LavanderiaWeb.Models;
using LavanderiaWeb.Data;
using LavanderiaWeb.Utils;
using Microsoft.EntityFrameworkCore;

namespace LavanderiaWeb.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly ApplicationDbContext _context;
        private List<Producto> ListaProductos;
        private Paginador<Producto> _Paginador;
        private readonly int _RegistrosPorPagina = 10;

        public ProductoController(ILogger<ProductoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
                        //var productos=_context.Productos.Include(p=>p.TipoProducto).Include(p=>p.Marca).ToList();
            ListaProductos = _context.Productos.Include(p=>p.Categoria).AsNoTracking().ToList();
        }

        public IActionResult Index()
        {
            var listProductos=_context.Productos.OrderBy(s=>s.Id).ToList();
            return View("List", listProductos);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias=_context.Categorias.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto objProducto){
            //ViewBag.Categorias=_context.Categorias.ToList();
            if(ModelState.IsValid){
                _context.Add(objProducto);
                _context.SaveChanges();
                return View(objProducto);
            }
                return View();
        }
        public IActionResult Edit(int? id){
            ViewBag.Categorias=_context.Categorias.ToList();
            if(id==null){
                return NotFound();
            }
            var Producto=_context.Productos.Find(id);
            if(Producto==null){
                return NotFound();
            }
            return View(Producto);
        }

        [HttpPost]
        public IActionResult Edit(int id, Producto Producto){
            //ViewBag.Categorias=_context.Categorias.ToList();
            if(ModelState.IsValid){
                _context.Update(Producto);
                _context.SaveChanges();
            }
            //return View(Producto);
            return RedirectToAction("List");
        }

        // public IActionResult List()
        // {
        //     var listProductos=_context.Productos.OrderBy(s=>s.Id).ToList();
        //     return View("List", listProductos);
        // }
        
        public IActionResult Delete(int? id){
            var Producto=_context.Productos.Find(id);
            _context.Productos.Remove(Producto);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        
        public IActionResult List(string buscarProductos, int pagina = 1)
        {
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;
            // FILTRO DE BÚSQUEDA
            // using (_context)
            // {
            // Filtramos el resultado por el 'texto de búqueda'
            if (!string.IsNullOrEmpty(buscarProductos))
            {
                foreach (var item in buscarProductos.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))
                {
                    ListaProductos = ListaProductos.Where(x => x.Descripcion.Contains(item) //||
                                                  //x.Precio.Contains(item) 
                                                  //||
                                                  //x.Categorias.Contains(item)
                                                  ).ToList()
                                                  ;
                }
            }
            // }
            // SISTEMA DE PAGINACIÓN
            // using (_context)
            // {
            _TotalRegistros = ListaProductos.Count();
            // Obtenemos la 'página de registros' 
            ListaProductos = ListaProductos.OrderBy(x => x.Id)
                                             .Skip((pagina - 1) * _RegistrosPorPagina)
                                             .Take(_RegistrosPorPagina).ToList();
            // Número total de páginas
            _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            _Paginador = new Paginador<Producto>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                BusquedaActual = buscarProductos,
                Resultado = ListaProductos
            };
            // }
            return View(_Paginador);
        }
        public IActionResult Producto()
        {
            return View();
        }
    }
}
