using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Web.View_Models;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Core.Interfaces;

namespace Web.Controllers
{
	public class PortfolioController : Controller
	{
		private readonly IUnitOfWork<PortfolioItem> _portfolio;
		private readonly IHostingEnvironment _hosting;

		public PortfolioController(IUnitOfWork<PortfolioItem> portpolio, IHostingEnvironment hosting)
		{
			_portfolio = portpolio;
			_hosting = hosting;
		}

		// GET: Portfolio
		public IActionResult Index()
		{
			return View(_portfolio.Entity.GetAll());
		}

		// GET: Portfolio/Details/5
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var portfolioItem = _portfolio.Entity.GetById(id);
			if (portfolioItem == null)
			{
				return NotFound();
			}

			return View(portfolioItem);
		}

		// GET: Portfolio/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PortfolioViewModel vModel)
		{
			if (ModelState.IsValid)
			{
				if (vModel.File != null)
				{
					string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
					string fullPath = Path.Combine(uploads, vModel.File.FileName);
					vModel.File.CopyTo(new FileStream(fullPath, FileMode.Create));
				}
				PortfolioItem portfolioItem = new PortfolioItem
				{
					Name = vModel.Name,
					Descrption = vModel.Descrption,
					ImageUrl = vModel.File.FileName
				};
				_portfolio.Entity.Insert(portfolioItem);
				_portfolio.Save();
				return RedirectToAction(nameof(Index));
			}
			return View(vModel);
		}

		// GET: Portfolio/Edit/5
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var portfolioItem = _portfolio.Entity.GetById(id);
			if (portfolioItem == null)
			{
				return NotFound();
			}
			PortfolioViewModel vmodel = new PortfolioViewModel
			{
				Id = portfolioItem.Id,
				Name = portfolioItem.Name,
				Descrption = portfolioItem.Descrption,
				ImageUrl = portfolioItem.ImageUrl
			};
			return View(vmodel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, PortfolioViewModel vModel)
		{
			if (id != vModel.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{

				try
				{
					if (vModel.File != null)
					{
						string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
						string fullPath = Path.Combine(uploads, vModel.File.FileName);
						vModel.File.CopyTo(new FileStream(fullPath, FileMode.Create));
					}
					PortfolioItem portfolioItem = new PortfolioItem
					{
						Id = vModel.Id,
						Name = vModel.Name,
						Descrption = vModel.Descrption,
						ImageUrl = vModel.File.FileName
					};
					_portfolio.Entity.Update(portfolioItem);
					_portfolio.Save();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PortfolioItemExists(vModel.Id))
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
			return View(vModel);
		}

		// GET: Portfolio/Delete/5
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var portfolioItem = _portfolio.Entity.GetById(id);
			if (portfolioItem == null)
			{
				return NotFound();
			}

			return View(portfolioItem);
		}

		// POST: Portfolio/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_portfolio.Entity.Delete(id);
			_portfolio.Save();
			return RedirectToAction(nameof(Index));
		}

		private bool PortfolioItemExists(Guid id)
		{
			return _portfolio.Entity.GetAll().Any(e => e.Id == id);
		}
	}
}
