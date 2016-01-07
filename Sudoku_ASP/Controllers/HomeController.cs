using SudokuBasis;
using System.Web.Mvc;

namespace SudokuWeb_Week5.Controllers {
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            var game = new SudokuGame();
            var lijstje = game.GetBoard();
            TempData["lijstje"] = lijstje;


            return View(GetGameSession());
        }

        public ActionResult NewGame()
        {
            NewGameSession();
            return RedirectToAction("Index");
        }

        public ActionResult SetValue(short indexx, short indexy, short value)
        {
            SudokuGame sudoku = GetGameSession();
            sudoku.SetValue(new Position() { X = indexx, Y = indexy, Value = value });
            return RedirectToAction("Index");
        }

        public ActionResult Hint()
        {
            SudokuGame sudoku = GetGameSession();
            Position pos = sudoku.GetHint();

            //x en y zijn om de een of andere rede omgekeerd
            //MessageBox.Show(pos.X + "=Y;" + pos.Y + "=X: VALUE = " + pos.Value);

            //tel de hoeveelheid die opgelost moet worden
            int unsolvedCount = 0;
            foreach (Position position in sudoku.GetBoard())
            {
                if (position.Value == 0)
                {
                    unsolvedCount++;
                }
            }

            //Alles behalve 2 moet opgelost worden
            int solveCount = unsolvedCount - 2;
            for (int x = 0; x < solveCount; x++)
            {
                Position position = sudoku.GetHint();
                sudoku.SetValue(position);
            }

            return RedirectToAction("Index");
        }

        private void NewGameSession()
        {
            SudokuGame game = new SudokuGame();
            game.NewGame();

            Session["game"] = game;
        }

        private SudokuGame GetGameSession()
        {
            SudokuGame game = Session["game"] as SudokuGame;
            if (game == null)
            {
                NewGameSession();
                game = Session["game"] as SudokuGame;
            }

            return game;
        }
    }
}