using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteDePerformance.Models;

namespace TesteDePerformance.Controllers {
    public class PessoaController : Controller {

        public ActionResult Index() {
            return View(GetPessoas());
        }

        public static List<Pessoa> GetPessoas() {
            return pessoasList;
        }

        private static List<Pessoa> pessoasList = new List<Pessoa> {
            new Pessoa() {Id = 1, Nome = "Carlos", SobreNome = "Santos"}
        };

        public ActionResult Details(int id) {
            return View(FindById(id));
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try {
                Pessoa p = new Pessoa();
                p.Id = buscaUltimoId();
                p.Nome = collection["Nome"];
                p.SobreNome = collection["SobreNome"];

                pessoasList.Add(p);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        public static int buscaUltimoId() {
            if (pessoasList.Count >= 1) {
                var last = pessoasList[pessoasList.Count - 1];
                return last.Id + 1;
            }
            return 1;
        }

        public ActionResult Edit(int id) {
            return View(FindById(id));
        }

        private static Pessoa FindById(int id) {
            var pessoas = pessoasList[id - 1];
            return pessoas;
        }

        private static List<Pessoa> findByString(string pesquisa) {
            List<Pessoa> localizados = new List<Pessoa>();

            foreach (Pessoa p in pessoasList) {
                var nomeTemp = p.Nome.ToLower();
                var sobreNomeTemp = p.SobreNome.ToLower();
                var pesquisaTemp = pesquisa.ToLower();
                if (nomeTemp.Contains(pesquisaTemp) || sobreNomeTemp.Contains(pesquisaTemp)) {
                    localizados.Add(p);
                }
            }
            return localizados;
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                Pessoa newPessoa = new Pessoa();
                newPessoa.Nome = collection["Nome"];
                newPessoa.SobreNome = collection["SobreNome"];
                EditarPessoa(id, newPessoa);

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        public static void EditarPessoa(int id, Pessoa newPessoa) {
            foreach (Pessoa p in pessoasList) {
                if (p.Id == id) {
                    p.Nome = newPessoa.Nome;
                    p.SobreNome = newPessoa.SobreNome;
                    break;
                }
            }
        }

        public ActionResult Delete(int id) {
            return View(FindById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                ApagarPessoa(id);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        private static void ApagarPessoa(int id) {
            foreach (Pessoa p in pessoasList) {
                if (p.Id == id) {
                    pessoasList.Remove(p);
                    break;
                }
            }
        }

        public ActionResult Buscar() {
            string pesquisa = "";
            return View(findByString(pesquisa));
        }

        [HttpPost]
        public ActionResult Buscar(string pesquisa) {
            try {
                return View(findByString(pesquisa));
            } catch {
                return View();
            }
        }
    }
}