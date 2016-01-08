using SudokuBasis;
using System.Linq;
using System.Web.Mvc;

namespace SudokuWeb_Week5.Controllers {
    public class HomeController : Controller
    {
        private SudokuGame _currentGame;

        public HomeController()
        { }

        public ActionResult Index() {
            RetrieveGameSession();
            return View(_currentGame);
        }

        public ActionResult NewGame() {
            NewGameSession();
            return RedirectToAction("Index");
        }

        public ActionResult Hint() {
            RetrieveGameSession();
            int toSolve = _currentGame.GetBoard().Where(x => x.Value == 0).Count() - 2;
            for (int x = 0; x < toSolve; x++) {
                Position position = _currentGame.GetHint();
                _currentGame.SetValue(position);
            }
            return RedirectToAction("Index");
        }

        #region Ajax

        public void SetValue(short posx, short posy, short value) {
            RetrieveGameSession();
            _currentGame.SetValue(new Position() { X = posx, Y = posy, Value = value });
            if(_currentGame.IsCompleted()) {
                RedirectToAction("Index");
            }
        }

        #endregion

        private void NewGameSession() {
            var game = new SudokuGame();
            game.NewGame();
            Session["game"] = game;
        }

        private void RetrieveGameSession() {
            _currentGame = (SudokuGame)Session["game"];
            if (_currentGame == null) {
                NewGameSession();
                _currentGame = (SudokuGame)Session["game"];
            }
        }
    }
}