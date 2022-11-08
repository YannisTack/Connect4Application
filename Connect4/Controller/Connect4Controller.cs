using Connect4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Controller
{
    internal class Connect4Controller
    {
        private GameView _gameView;
        private GameModel _gameModel;
        private Settings _settings;

        public Connect4Controller(GameView gameView)
        {
            _gameView = gameView;
        }

        public void NewGame()
        {
            _settings = new Settings(Settings.GridSize.Medium);
            _gameModel = new GameModel(_gameView, _settings);
            
        }

        public void ChangeValue()
        {
            Console.WriteLine("Button press - Controller");
        }
        
    }
}
