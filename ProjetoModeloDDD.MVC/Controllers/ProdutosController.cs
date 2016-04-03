using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        private readonly IProdutoAppService _produtoAppService;
        private readonly IClienteAppService _clienteAppService;

        public ProdutosController(IProdutoAppService produtoAppService, IClienteAppService clienteAppService)
        {
            _produtoAppService = produtoAppService;
            _clienteAppService = clienteAppService;
        }

        // GET: Produtos
        public ActionResult Index()
        {
            var produtoViewModel =
                Mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoAppService.GetAll());
            return View(produtoViewModel);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int id)
        {
            var produto = _produtoAppService.GetById(id);
            var produtoViewModel = Mapper.Map<Produto, ProdutoViewModel>(produto);
            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            //Criação de lista de clientes onde ID = valor e texto da lista = nome do cliente
            //Ver artigo sobre criação de listas
            ViewBag.ClienteId = new SelectList(_clienteAppService.GetAll(),"ClienteId","Nome");
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var produtoDomain = Mapper.Map<ProdutoViewModel, Produto>(produto);
                _produtoAppService.Add(produtoDomain);

                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int id)
        {
            var produto = _produtoAppService.GetById(id);
            var produtoViewModel = Mapper.Map<Produto, ProdutoViewModel>(produto);
            return View(produtoViewModel);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var produtoDomain = Mapper.Map<ProdutoViewModel, Produto>(produto);
                _produtoAppService.Update(produtoDomain);

                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            var produto = _produtoAppService.GetById(id);
            var produtoViewModel = Mapper.Map<Produto, ProdutoViewModel>(produto);
            return View(produtoViewModel);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var produto = _produtoAppService.GetById(id);
            _produtoAppService.Remove(produto);
            return RedirectToAction("Index");
        }
    }
}